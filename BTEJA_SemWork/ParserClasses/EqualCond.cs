using BTEJA_SemWork.ParserClasses.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTEJA_SemWork.ParserClasses
{
    public class EqualCond : BinaryCondition
    {
        public EqualCond(Expression left, Expression right) : base(left, right)
        {
        }

        public override object Evaluate(MyExecutionContext executionContext)
        {
            object leftValue = Left.Evaluate(executionContext);
            switch (Type.GetTypeCode(leftValue.GetType()))
            {
                case TypeCode.Int32:
                    if (Right.GetType() == Left.GetType())
                    {
                        if(Convert.ToInt32(leftValue) == Convert.ToInt32(Right.Evaluate(executionContext))) return true;
                        return false;
                    }
                    else
                    {
                        throw new Exception("EqualCond: both operands must be of the same datatype.[Interpreting]");
                    }
                case TypeCode.Double:
                    if (Right.GetType() == Left.GetType())
                    {
                        if (Convert.ToDouble(leftValue) == Convert.ToDouble(Right.Evaluate(executionContext))) return true;
                        return false;
                    }
                    else
                    {
                        throw new Exception("EqualCond: both operands must be of the same datatype.[Interpreting]");
                    }
                case TypeCode.String:
                    if (Right.GetType() == Left.GetType())
                    {
                        if (Convert.ToString(leftValue) == Convert.ToString(Right.Evaluate(executionContext))) return true;
                        return false;
                    }
                    else
                    {
                        throw new Exception("EqualCond: both operands must be of the same datatype.[Interpreting]");
                    }
                default:
                    break;
            }
            throw new Exception("EqualCond: Unexpected error.[Interpreting]");
        }
    }
}
