using DevFramework.Core.CrossCuttingConcerns.Logging;
using DevFramework.Core.CrossCuttingConcerns.Logging.Log4Net;
using PostSharp.Aspects;
using PostSharp.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.Aspects.Postsharp.LogAspects
{
    [Serializable]
    [MulticastAttributeUsage(MulticastTargets.Method, TargetMemberAttributes = MulticastAttributes.Instance)]//Namespace içinde en başa yazıldığında constructorlarda bu aspecten etkilenir onların etkilenmemesi sadece nesne instancelarının metotları etkilensin derken
    public class LogAspect : OnMethodBoundaryAspect
    {
        private Type _loggerType;

        //Gelecek loggerların türü burada belli oluyor(database, FileStyleUriParser vs)
        private LoggerService _loggerService;

        public LogAspect(Type loggerType)
        {
            _loggerType = loggerType;
        }

        public override void RuntimeInitialize(MethodBase method)
        {
            if (_loggerType.BaseType!=typeof(LoggerService)) //logger Logger service tipinde değilse yanlış tip hatası
            {
                throw new Exception("Wrong logger type");
            }
            _loggerService = (LoggerService)Activator.CreateInstance(_loggerType); //sıkıntı yoksa loggerservice instanceı yaratma
            base.RuntimeInitialize(method);
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            //Sadece info logu yapılacak, yani kim ne zaman nasıl girdi gibi bilgiler
            if (!_loggerService.IsInfoEnabled)
                return;
            try
            {
            var logParameters = args.Method.GetParameters().Select((t, i) => new LogParameter
            {
                Name = t.Name, // t:type, log parametresinin ismi (Şehir)
                Type = t.ParameterType.Name, // tipi(string)
                Value = args.Arguments.GetArgument(i) // i:iterator, her select için iterate ediyor 
            }).ToList();

            var logDetail = new LogDetail
            {
                FullName = args.Method.DeclaringType == null ? null : args.Method.DeclaringType.Name,
                MethodName = args.Method.Name,
                Paramaters = logParameters
            };

            _loggerService.Info(logDetail); //loglamayı kaydetme

            }
            catch(Exception)
            {
            }
        }
    }
}
