using BTEJA_SemWork.ParserClasses.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTEJA_SemWork.ParserClasses
{
    public class Minus : BinaryExpression
    {
        public override object Evaluate(MyExecutionContext executionContext)
        {
            object leftValue = Left.Evaluate(executionContext);
            switch (Type.GetTypeCode(leftValue.GetType()))
            {
                case TypeCode.Int32:
                    if (Right.Evaluate(executionContext).GetType() == Left.Evaluate(executionContext).GetType())
                    {
                        return (int)(Convert.ToInt32(leftValue) - Convert.ToInt32(Right.Evaluate(executionContext)));
                    }
                    else
                    {
                        throw new Exception("Subtract: both operands must be of the same datatype.[Interpreting]");
                    }
                case TypeCode.Double:
                    if (Right.Evaluate(executionContext).GetType() == Left.Evaluate(executionContext).GetType())
                    {
                        return Convert.ToDouble(leftValue) - Convert.ToDouble(Right.Evaluate(executionContext));
                    }
                    else
                    {
                        throw new Exception("Subtract: both operands must be of the same datatype.[Interpreting]");
                    }
                case TypeCode.String:
                    throw new Exception("Subtract: Substracting strings is not supported.[Interpreting]");
                default:
                    break;
            }
            throw new Exception("Subtract: Unexpected error.[Interpreting]");
        }
    }
}