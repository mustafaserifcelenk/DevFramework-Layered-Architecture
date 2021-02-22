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

        public CacheAspect(int cacheByMinute, Type cacheType)
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

    }
}
