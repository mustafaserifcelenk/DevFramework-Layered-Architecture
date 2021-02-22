using log4net.Core;
using log4net.Layout;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.CrossCuttingConcerns.Logging.Log4Net.Layouts
{
    //Loglamaya konu olacak datanın loglanma formatının tanımlanması
    public class JsonLayout : LayoutSkeleton //Bunun bir log4net layout olabilmesi için layout iskeletinden implement ettik
    {
        public override void ActivateOptions()
        {
            throw new NotImplementedException();
        }

        public override void Format(TextWriter writer, LoggingEvent loggingEvent)
        {
            //Biz zaten serializable log event oluşturmuştuk
            var logEvent = new SerializableLogEvent(loggingEvent);
            //loggingeventi yani loglanacak datayı serializable olsun diye gönderdik

            var json = JsonConvert.SerializeObject(logEvent, Formatting.Indented); //Intendent : Klasik Json formatı

            writer.WriteLine(json); //writer: yazılacak nesneyi içerir
        }
    }
}
