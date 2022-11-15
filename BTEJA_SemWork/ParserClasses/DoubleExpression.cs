using BTEJA_SemWork.ParserClasses.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTEJA_SemWork.ParserClasses
{
    public class DoubleExpression : LitExpression
    {
        public DoubleExpression(int line, int token, object value) : base(line, token, value)
        {
        }

        public override object Evaluate(MyExecutionContext executionContext)
        {
            return Convert.ToDouble(Value);
        }
    }
}
