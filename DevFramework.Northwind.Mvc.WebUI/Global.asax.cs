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

            //Asp'nin ya�am d�ng�s�n� yakalay�p orada i�lem yapmak i�in global asax
            //Asp.net kullan�c�n�n identity bilgileri eri�ilebilir oldu�u zaman biz ona hem ula�abilir hem de onu set edebiliriz manas�na gelen komutu yazaca��z ama ilk olarak bizim bu eventi olu�turacak ortam� olu�turmam�z laz�m
            public override void Init()
        {
            PostAuthenticateRequest += MvcApplication_PostAuthenticateRequest;
            base.Init();
        }

        //Ki�inin authentication bilgilerinin ula��labilir oldu�u zamana denk gelen metot
        private void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
        {
            //sisteme hata verdirmeye �al��an hackleme i�lemleri vs i�in defensive programing
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

                //�imdi bu ticketi al�p securityutilitiesi kullanarak identitye �evirmemiz laz�m
                var securityUtilities = new SecurityUtilities();
                var identity = securityUtilities.FormsAuthTicketToIdentity(ticket);
                //Kullan��c� i�in bir id olu�turma?
                var principal = new GenericPrincipal(identity, identity.Roles);

                //Mvc i�in User
                HttpContext.Current.User = principal;

                //Backendde bunu kullanabilsin diye(mesela business i�in)
                Thread.CurrentPrincipal = principal;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
