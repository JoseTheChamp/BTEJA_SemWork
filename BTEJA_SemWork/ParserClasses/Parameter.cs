using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTEJA_SemWork.ParserClasses
{
    public class Parameter : Position
    {
        public Parameter(int line, int token,string ident,DataType dataType) : base(line, token)
        {
            
            Ident = ident;
            DataType = dataType;
            
        }

        public string Ident { get; set; }
        public DataType DataType { get; set; }
        
    }
}
