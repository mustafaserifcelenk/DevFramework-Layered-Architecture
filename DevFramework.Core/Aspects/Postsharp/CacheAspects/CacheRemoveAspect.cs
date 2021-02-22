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
    public class CacheRemoveAspect : OnMethodBoundaryAspect
    {
        private string _pattern;


        private Type _cacheType;
        private ICacheManager _cacheManager;

        //Sadece cachetype a göre silme
        public CacheRemoveAspect(Type cacheType)
        {
            _cacheType = cacheType;
        }

        //string pattern ve cache type a göre
        public CacheRemoveAspect(string pattern, Type cacheType)
        {
            _pattern = pattern;
            _cacheType = cacheType;
        }

        public override void RuntimeInitialize(MethodBase method)
        {
            if (typeof(ICacheManager).IsAssignableFrom(_cacheType) == false) //Gönderilen cache manager geçerli bir cachemanager türünde değilse
            {
                throw new Exception("Wrong cache manager...");

            }
            _cacheManager = (ICacheManager)Activator.CreateInstance(_cacheType); //Activator ile bir sınıf örneği oluşturabiliyoruz, reflection
            base.RuntimeInitialize(method); //Bu işlemden sonra datayı cache ekleme kısmına geçmiş oluyoruz, burada yapacağımız şey method çalıştırılmadan önce çalıştırılmaya çalışılan metodun sonucu cache de var mı dememiz gerekiyor
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            _cacheManager.RemoveByPattern(string.IsNullOrEmpty(_pattern)
                ? string.Format("{0}.{1}.*", args.Method.ReflectedType.Namespace, args.Method.ReflectedType.Name)
                : _pattern);
        }
    }
}
