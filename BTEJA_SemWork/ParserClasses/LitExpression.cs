using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTEJA_SemWork.ParserClasses
{
    public abstract class LitExpression : Expression
    {
        public Object Value { get; set; }
    }
}
