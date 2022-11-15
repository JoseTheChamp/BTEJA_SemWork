using BTEJA_SemWork.ParserClasses.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTEJA_SemWork.ParserClasses
{
    public class ReturnStatement : Statement
    {
        public ReturnStatement(int line, int token, Expression expression) : base(line, token)
        {
            Expression = expression;
        }
        public Expression Expression { get; set; }
        public override object? Execute(MyExecutionContext executionContext)
        {
            return Expression.Evaluate(executionContext);
        }
    }
}
