using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.CrossCuttingConcerns.Logging
{
    public class LogDetail
    {
        public string FullName { get; set; } //Namespace ismi
        public string MethodName { get; set; } // O Namespacedeki methodun ismi(update vs)
        public List<LogParameter> Paramaters { get; set; } //Methodun parametreleri birden fazla olabilir
    }
}
