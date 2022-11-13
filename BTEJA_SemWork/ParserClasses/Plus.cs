using BTEJA_SemWork.ParserClasses.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTEJA_SemWork.ParserClasses
{
    public class Plus : BinaryExpression
    {
        public override object Evaluate(MyExecutionContext executionContext)
        {
            object leftValue = Left.Evaluate(executionContext);
            Console.WriteLine("Left type: " + Left.GetType());
            Console.WriteLine("Leftvalue type: " + leftValue.GetType());
            switch (Type.GetTypeCode(leftValue.GetType()))
            {
                case TypeCode.Int32:
                    if (Right.Evaluate(executionContext).GetType() == Left.GetType())
                    {
                        return (int)(Convert.ToInt32(leftValue) + Convert.ToInt32(Right.Evaluate(executionContext)));
                    }
                    else
                    {
                        throw new Exception("Addition: both operands must be of the same datatype.[Interpreting]");
                    }
                case TypeCode.Double:
                    if (Right.Evaluate(executionContext).GetType() == Left.GetType())
                    {
                        return Convert.ToDouble(leftValue) + Convert.ToDouble(Right.Evaluate(executionContext));
                    }
                    else
                    {
                        throw new Exception("Addition: both operands must be of the same datatype.[Interpreting]");
                    }
                case TypeCode.String:
                    throw new Exception("Addition: Adding strings is not supported.[Interpreting]");
                default:
                    break;
            }
            throw new Exception("Addition: Unexpected error.[Interpreting]");
        }
    }
}
