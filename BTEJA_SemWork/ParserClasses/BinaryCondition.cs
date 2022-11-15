using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTEJA_SemWork.ParserClasses
{
    public abstract class BinaryCondition : Condition
    {
        protected BinaryCondition(int line, int token, Expression left, Expression right) : base(line, token)
        {
            Left = left;
            Right = right;
        }

        public Expression Left { get; set; }
        public Expression Right { get; set; }

       
    }
}
