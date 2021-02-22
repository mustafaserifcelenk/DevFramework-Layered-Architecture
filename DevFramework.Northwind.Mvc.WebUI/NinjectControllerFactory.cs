using DevFramework.Northwind.Business.DependecyResolvers.Ninject;
using System.Web.Mvc;

namespace DevFramework.Northwind.Mvc.WebUI
{
    internal class NinjectControllerFactory : IControllerFactory
    {
        private BusinessModule businessModule;

        public NinjectControllerFactory(BusinessModule businessModule)
        {
            this.businessModule = businessModule;
        }
    }
}