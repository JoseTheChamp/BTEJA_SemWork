using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTEJA_SemWork.ParserClasses
{
    public class GreaterEqualCond : BinaryCondition
    {
        public GreaterEqualCond(Expression left, Expression right) : base(left, right)
        {
        }
    }
}
