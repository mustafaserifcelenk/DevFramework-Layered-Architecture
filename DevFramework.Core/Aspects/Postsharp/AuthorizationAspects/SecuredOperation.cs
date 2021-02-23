using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.Aspects.Postsharp.AuthorizationAspects
{
    [Serializable]
    public class SecuredOperation : OnMethodBoundaryAspect //Metotun hemen başında çalışacağından
    {
        public string Roles { get; set; }

        public override void OnEntry(MethodExecutionArgs args)
        {
            //Kullanıcı bu işlemi yapma yetkisine sahip rolde ise işlemi yapmasına izin ver
            string[] roles = Roles.Split(','); //Kullanıcılar virgülle verilecek onları ayırıyoruz ve diziye atıyoruz
            bool isAuthorized = false;

            for (int i = 0; i < roles.Length; i++)
            {
                if (System.Threading.Thread.CurrentPrincipal.IsInRole(roles[i])) //Current principal: .Net framework ile gelen herhangi bir uygulama bağımsız, bizim mevcut kullanıcıyı ve bu kullanıcıya ait bilgileri tutabildiğimiz rol bazlı güvenlik sistemlerinde  kullandığımız bir yapı, bu uygulama bağımsız olduğundan bu kullanılmalıdır
                {
                    isAuthorized = true;
                }
            }

            if (isAuthorized==false)
            {
                throw new SecurityException("You are not authorized!");
            }
        }
    }
}
