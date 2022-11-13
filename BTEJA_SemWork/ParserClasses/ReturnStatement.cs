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
        public Expression Expression { get; set; }

        public ReturnStatement(Expression expression)
        {
            Expression = expression;
        }

        public override void Execute(MyExecutionContext executionContext)
        {
            throw new Exception("This returnstatement is called directly.[interpreting]");
        }
    }
}
