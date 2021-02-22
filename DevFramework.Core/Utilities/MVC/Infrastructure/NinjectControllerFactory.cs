using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace DevFramework.Core.Utilities.MVC.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {

        //Businesste oluşturduğumuz ninject injectionu entegre etme
        private IKernel _kernel;

        public NinjectControllerFactory(INinjectModule module)
        {
            _kernel = new StandardKernel(module);
        }
         
        //productcontrollerın bir çözümlemeye ihtiyacı varsa git onu Kerneldan çözümle
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            //Controller varsa onu IControllera çevir, kernele controllerı ver o da çözümlesin
            return controllerType == null ? null : (IController)_kernel.Get(controllerType);
        }
    }
}
