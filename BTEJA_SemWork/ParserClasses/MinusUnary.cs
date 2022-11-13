using BTEJA_SemWork.ParserClasses.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTEJA_SemWork.ParserClasses
{
    public class MinusUnary : UnaryExpression
    {
        public override object Evaluate(MyExecutionContext executionContext)
        {
            object value = Expression.Evaluate(executionContext);
            switch (Type.GetTypeCode(value.GetType()))
            {
                case TypeCode.Int32:
                    return -Convert.ToInt32(value);
                case TypeCode.Double:
                    return -Convert.ToDouble(value);
                case TypeCode.String:
                    throw new Exception("MinusUnary: -string.[Interpreting]");
            }
            throw new Exception("MinusUnary: Unexpected error.[Interpreting]");
        }
    }
}
