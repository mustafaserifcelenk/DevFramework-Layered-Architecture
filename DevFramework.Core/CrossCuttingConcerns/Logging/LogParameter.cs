using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.CrossCuttingConcerns.Logging
{
    public class LogParameter
    {
        public string Name { get; set; } 
        public string Type { get; set; }
        public object Value { get; set; }
        //Mesela updatedeki product için ismi product, tipi Product, içeriği productta ne varsa, bunları LogParameter içinde tutacağız
    }
}
