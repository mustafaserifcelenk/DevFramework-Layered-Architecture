using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.CrossCuttingConcerns.Logging.Log4Net
{
    //Logları tutma şeklimiz Json formatında olduğundan onların serialize edilmesi
    [Serializable]
    public class SeriazlizableLogEvent
    {
        //Buraya loglama bilgisini göndereceğiz
        private LoggingEvent _loggingEvent;

        public SeriazlizableLogEvent(LoggingEvent loggingEvent)
        {
            _loggingEvent = loggingEvent;
        }

        //loglama sebep olan kişi, log datasının mesajla ilgili olan kısmını hangi metot hangi parametrelerle çalıştırıldı
        public string UserName => _loggingEvent.UserName;
        public object MessageObject => _loggingEvent.MessageObject;
    }
}
