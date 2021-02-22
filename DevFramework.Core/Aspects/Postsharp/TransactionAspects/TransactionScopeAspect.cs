using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DevFramework.Core.Aspects.Postsharp.TransactionAspects
{
    [Serializable]
    public class TransactionScopeAspect : OnMethodBoundaryAspect
    {
        private TransactionScopeOption _option; //farklı parametrelerle transaction kullanmak istenirse
        public TransactionScopeAspect(TransactionScopeOption option) 
        {
            //using(TransactionScope scope = new TransactionScope()) { } // Buradan gönderilir parametreler
        }
        public TransactionScopeAspect()//parametresiz constructor vasıtasıyla transactionaspect çalıştırılır, herhangi bir değer girilmediğinde bu geçerli
        {

        }

        public override void OnEntry(MethodExecutionArgs args) //metoda girildiğinde transaction scope açılması gerekiyor
        {
            args.MethodExecutionTag = new TransactionScope(_option);//option varsa parametreli constructor çalışır yoksa parametresiz
        }
        public override void OnSuccess(MethodExecutionArgs args)
        {
            ((TransactionScope)args.MethodExecutionTag).Complete();
        }
        public override void OnExit(MethodExecutionArgs args) //Başarılı değilse, yani tryi atlarsa
        {
            ((TransactionScope)args.MethodExecutionTag).Dispose();

        }
    }
}
