using BTEJA_SemWork.ParserClasses.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTEJA_SemWork.ParserClasses
{
    public class DefinitionStatement : Statement
    {
        public bool IsVal { get; set; }
        public string Ident { get; set; }
        public DataType DataType { get; set; }
        public object Value { get; set; }

        public override void Execute(MyExecutionContext executionContext)
        {
            executionContext.Variables.AddVariable(new Variable(Ident,IsVal,DataType,Value));
        }
    }
}
