using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTEJA_SemWork.ParserClasses
{
    public class Parameter
    {
        public string Ident { get; set; }
        public DataType DataType { get; set; }
        public Parameter(string ident, DataType dataType)
        {
            Ident = ident;
            DataType = dataType;
        }
    }
}
