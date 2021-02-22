using DevFramework.Core.CrossCuttingConcerns.Catching;
using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.Aspects.Postsharp.CacheAspects
{
    [Serializable]
    public class CacheAspect : MethodInterceptionAspect
    {
        //Hangi cache typeı: redis, microsoft vs
        private Type _cacheType;
        //Cache süresi
        private int _cacheByMinute;
        //Cache mekanizmasını atayabilme
        private ICacheManager _cacheManager;

        public CacheAspect(Type cacheType, int cacheByMinute = 60)
        {
            _cacheByMinute = cacheByMinute;
            _cacheType = cacheType;
        }

        //Hangi Cache mekanizması gönderildiyse onu initialize etmemiz gerekiyor

        public override void RuntimeInitialize(MethodBase method)
        {
            if (typeof(ICacheManager).IsAssignableFrom(_cacheType)==false) //Gönderilen cache manager geçerli bir cachemanager türünde değilse
            {
                throw new Exception("Wrong cache manager..."); 

            }
            _cacheManager = (ICacheManager)Activator.CreateInstance(_cacheType); //Activator ile bir sınıf örneği oluşturabiliyoruz, reflection
            base.RuntimeInitialize(method); //Bu işlemden sonra datayı cache ekleme kısmına geçmiş oluyoruz, burada yapacağımız şey method çalıştırılmadan önce çalıştırılmaya çalışılan metodun sonucu cache de var mı dememiz gerekiyor
        }

        public override void OnInvoke(MethodInterceptionArgs args) //Bu metot çalıştırıldığında bu uygulansın ilk
        {
            //İlk önce çalıştırılacak metodun parametresi, namespaceyle beraber bir key oluşturulmalı
            var methodName = string.Format("{0}.{1}.{2}",
                args.Method.ReflectedType.Namespace,  
                args.Method.ReflectedType.Name, 
                args.Method.Name); //Namespace ismi, sınıf ismi metot ismi

            //Output caching yapmak istediğimiz için parametrelere de ulaşmamız gerekiyor
            var arguments = args.Arguments.ToList();

            //Key oluşturma
            var key = string.Format("{0}({1})", methodName, string.Join(",", arguments.Select(x => x != null ? x.ToString() : "<Null>")));

            //Eğer bu metot cachede varsa ekleme bana keyi getir
            if (_cacheManager.IsAdd(key))
            {
                args.ReturnValue = _cacheManager.Get<object>(key);//metot biter direk return döner
            }
            base.OnInvoke(args);
            _cacheManager.Add(key, args.ReturnValue, _cacheByMinute);
        }

    }
}
