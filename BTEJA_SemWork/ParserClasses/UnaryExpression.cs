using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTEJA_SemWork.ParserClasses
{
    public abstract class UnaryExpression : Expression
    {
        protected UnaryExpression(int line, int token,Expression expression) : base(line, token)
        {
            Expression = expression;
        }

        public Expression Expression { get; set; }
    }
}
