using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTEJA_SemWork.ParserClasses
{
    public abstract class BinaryCondition : Condition
    {
        public Expression Left { get; set; }
        public Expression Right { get; set; }

        protected BinaryCondition(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }
    }
}
