using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.CrossCuttingConcerns.Logging.Log4Net
{
    public class LoggerService
    {
        private ILog _log;

        public LoggerService(ILog log)
        {
            _log = log;
        }

        //Loglama ortamında bilgi seviyesindeki loglar açık mı?
        //Bu kişi şu saatte şunu bu parametrelerle çağırdı gibi bilgiler için 
        public bool IsInfoEnabled => _log.IsDebugEnabled;

        //Loglama ortamında debug logları açık mı?
        public bool IsDebugEnabled => _log.IsDebugEnabled;

        //Uyarı Hata Fatal gibi kontrolleri yapmak için
        public bool IsWarnEnabled => _log.IsWarnEnabled;
        public bool IsFatalEnabled => _log.IsFatalEnabled;
        public bool IsErrorEnabled => _log.IsErrorEnabled;

        //Bize bir info ver ben onu veritabanı veya nereye loglayacaksam loglayayım
        public void Info(object logMessage)
        {
            if (IsInfoEnabled)
            {
                _log.Info(logMessage); //is info enabled ise onu logla
            }
        }

        public void Debug(object logMessage)
        {
            if (IsDebugEnabled)
            {
                _log.Debug(logMessage); 
            }
        }

        public void Warn(object logMessage)
        {
            if (IsWarnEnabled)
            {
                _log.Warn(logMessage);
            }
        }

        public void Fatal(object logMessage)
        {
            if (IsFatalEnabled)
            {
                _log.Fatal(logMessage);
            }
        }

        public void Error(object logMessage)
        {
            if (IsErrorEnabled)
            {
                _log.Error(logMessage);
            }
        }
    }
}
