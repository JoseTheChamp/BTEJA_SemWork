using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTEJA_SemWork.ParserClasses.Context
{
    public class Function
    {
        public string Ident { get; set; }
        public List<Statement> Statements { get; set; }
        public Function(string ident, List<Statement> statements)
        {
            Ident = ident;
            Statements = statements;
        }
        public void Execute(MyExecutionContext executionContext,List<string> paramss) { 
        
        }
    }
}
