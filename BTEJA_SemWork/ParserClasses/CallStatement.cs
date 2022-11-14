using BTEJA_SemWork.ParserClasses.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTEJA_SemWork.ParserClasses
{
    public class CallStatement : Statement
    {
        public string Ident { get; set; }
        public List<Expression> Params { get; set; }

        public CallStatement(string ident)
        {
            Ident = ident;
            Params = new List<Expression>();
        }

        public override object? Execute(MyExecutionContext executionContext)
        {
            executionContext.ProgramContext.Call(Ident,executionContext,Params);
            return null;
        }
    }
}
