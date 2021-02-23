using DevFramework.Northwind.Business.DependecyResolvers.Ninject;
using DevFramework.Core.Utilities.MVC.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using DevFramework.Core.CrossCuttingConcerns.Security.Web;
using System.Security.Principal;
using System.Threading;

namespace DevFramework.Northwind.Mvc.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory(new BusinessModule()));
        }

            //Asp'nin yaþam döngüsünü yakalayýp orada iþlem yapmak için global asax
            //Asp.net kullanýcýnýn identity bilgileri eriþilebilir olduðu zaman biz ona hem ulaþabilir hem de onu set edebiliriz manasýna gelen komutu yazacaðýz ama ilk olarak bizim bu eventi oluþturacak ortamý oluþturmamýz lazým
            public override void Init()
        {
            PostAuthenticateRequest += MvcApplication_PostAuthenticateRequest;
            base.Init();
        }

        //Kiþinin authentication bilgilerinin ulaþýlabilir olduðu zamana denk gelen metot
        private void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
        {
            //sisteme hata verdirmeye çalýþan hackleme iþlemleri vs için defensive programing
            try
            {
                var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

                if (authCookie == null)
                {
                    return;
                }

                var encTicket = authCookie.Value;
                if (string.IsNullOrEmpty(encTicket))
                {
                    return;
                }

                var ticket = FormsAuthentication.Decrypt(encTicket);

                //Þimdi bu ticketi alýp securityutilitiesi kullanarak identitye çevirmemiz lazým
                var securityUtilities = new SecurityUtilities();
                var identity = securityUtilities.FormsAuthTicketToIdentity(ticket);
                //Kullanýýcý için bir id oluþturma?
                var principal = new GenericPrincipal(identity, identity.Roles);

                //Mvc için User
                HttpContext.Current.User = principal;

                //Backendde bunu kullanabilsin diye(mesela business için)
                Thread.CurrentPrincipal = principal;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
