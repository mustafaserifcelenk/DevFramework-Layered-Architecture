using DevFramework.Core.CrossCuttingConcerns.Validation.FluentValidation;
using FluentValidation;
using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.Aspects.Postsharp
{
    [Serializable] //Compile time de serilize edilmesi gerekiyor
    public class FluentValidationAspect : OnMethodBoundaryAspect
    {
        Type _validatorType;
        public FluentValidationAspect(Type validatorType) //productvalidator gelecek
        {
            _validatorType = validatorType;
        }
        public override void OnEntry(MethodExecutionArgs args) //methoda girildiği an, fluentvalidationu kullanan kimse(ProductManager), methodun girişinde bu aspect devreye girecektir
        {
            //Ne ile validate edeceğimin bilgisi var ama instanceı yok
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            var entityType = _validatorType.BaseType.GetGenericArguments()[0]; //Basetype: abstractvalidator, onun içinde product type var onu al
            var entities = args.Arguments.Where(t => t.GetType() == entityType); //Çalıştığım metotun parametrelerini gez ve tipi product olanları yakala

            foreach (var entity in entities)
            {
                ValidatorTool.FluentValidate(validator, entity);
            }
        }
    }
}
//onexception : Methot hata verdiği zaman
//onexit : Methottan çıkıldığı zaman
//onsuccess : Methot başarılı olduğu zaman, try catchdeki tryın içi