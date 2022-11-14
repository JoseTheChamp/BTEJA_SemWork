using BTEJA_SemWork.ParserClasses.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTEJA_SemWork.ParserClasses
{
    public class FunctionStatement : Statement
    {
        public string Ident { get; set; }
        public List<Parameter> Parameters { get; set; }
        public DataType? ReturnType { get; set; }
        public List<Statement> Statements { get; set; }

        public FunctionStatement(string ident)
        {
            Ident = ident;
            Parameters = new List<Parameter>();
            Statements = new List<Statement>();
        }

        public override object? Execute(MyExecutionContext executionContext)
        {
            executionContext.ProgramContext.AddFunction(new Function(Ident,ReturnType,Parameters,Statements));
            return null;
        }
    }
}
