using BTEJA_SemWork.ParserClasses.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTEJA_SemWork.ParserClasses
{
    public class WhileStatement : Statement
    {
        public Condition Condition { get; set; }
        public List<Statement> Statements { get; set; }

        public WhileStatement(Condition condition)
        {
            Condition = condition;
            Statements = new List<Statement>();
        }

        public override void Execute(MyExecutionContext executionContext)
        {
            throw new NotImplementedException();
        }
    }
}
