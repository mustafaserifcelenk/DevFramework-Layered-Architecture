using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers
{
    public class DatabaseLogger : LoggerService //Daha önce yazdığımız logger service
    {
        //public DatabaseLogger(ILog log) : base(log) //base bir ILog istyor bizden
        public DatabaseLogger() : base(LogManager.GetLogger("DatabaseLogger")) //Logger bilgisini konfigürasyon dosyasında yazacağım databaselogger isimli loggerdan al
        {
            //biz database logger için logmanagerdan getlogger'ın getirdiği DataBase loggerı çağırarak, oradaki implementasyonu kullanıp onu basee yani logger service'e yolluyoruz ve orada enable olana değerlere göre ilgili databaselere loglamayı yapar
        }
    }
}
