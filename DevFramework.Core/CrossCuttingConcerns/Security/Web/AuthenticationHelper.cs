using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace DevFramework.Core.CrossCuttingConcerns.Security.Web
{
    public class AuthenticationHelper
    {
        //Form Authentication implementasyonu gerçekleştirelecek, formauthentication kullanıcı bilgilerinin bir veritabanında bir veri kaynağında olduğu ve kullanıcı adı ve şifresinin veritabanından sorgulandığı sistemlere denir

        // Formaauthenticationda setouthcookie de login bilgilerini felan tutabiliriz ama custom datayı(rolleri vs) tutamıyoruz, bunun için kendi authenticationhelperımızı yazıyoruz, formauthonticationu kullancağız ama onu ezeceğiz

        public static void CreateAuthCookie(Guid id, string userName, string email, DateTime expiration, string[] roles, bool rememberMe, string firstName, string lastName) //expiration : disable olma zamanı
        {
            //formauthenticationda mantık şu: bir ticket oluşturuluyor ve bu ticket cookie olarak şifreli bir şekilde tutuluyor
            var authTicket = new FormsAuthenticationTicket(1, userName, DateTime.Now, expiration, rememberMe, CreateAuthTags(email, roles, firstName, lastName, id)); //custom datayı bir formatta tutmak için metot oluşturduk

            //strigi encrypt edeceğiz
            string encTicket = FormsAuthentication.Encrypt(authTicket);

            //Http cookie olarak cookielere eklenmesini sağlayacağız
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket)); //namei standart verdik değeride şifreli gönderdik ve cookiye bunları bastık
        }

        private static string CreateAuthTags(string email, string[] roles, string firstName, string lastName, Guid id)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(email);
            stringBuilder.Append("|");
            for (int i = 0; i < roles.Length; i++)
            {
                stringBuilder.Append(roles[i]);
                if (i < roles.Length - 1)
                {
                    stringBuilder.Append(",");
                }
            }
            stringBuilder.Append("|");
            stringBuilder.Append(firstName);
            stringBuilder.Append("|");
            stringBuilder.Append(lastName);
            stringBuilder.Append("|");
            stringBuilder.Append(id);

            return stringBuilder.ToString();
        }
    }
}
