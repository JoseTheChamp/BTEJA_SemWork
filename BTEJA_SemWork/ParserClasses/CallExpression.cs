using BTEJA_SemWork.ParserClasses.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTEJA_SemWork.ParserClasses
{
    public class CallExpression : Expression
    {
        public string Ident { get; set; }
        public List<Expression> Parameters { get; set; }

        public CallExpression(string ident)
        {
            Ident = ident;
            Parameters = new List<Expression>();
        }

        public override object Evaluate(MyExecutionContext executionContext)
        {
            return executionContext.ProgramContext.Call(Ident,executionContext,Parameters);
        }
    }
}
