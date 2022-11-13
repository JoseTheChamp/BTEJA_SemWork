﻿using BTEJA_SemWork.ParserClasses.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTEJA_SemWork.ParserClasses
{
    public class LesserEqualCond : BinaryCondition
    {
        public LesserEqualCond(Expression left, Expression right) : base(left, right)
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
                        if (Convert.ToInt32(leftValue) <= Convert.ToInt32(Right.Evaluate(executionContext))) return true;
                        return false;
                    }
                    else
                    {
                        throw new Exception("SmallerEqualCond: both operands must be of the same datatype.[Interpreting]");
                    }
                case TypeCode.Double:
                    if (Right.GetType() == Left.GetType())
                    {
                        if (Convert.ToDouble(leftValue) <= Convert.ToDouble(Right.Evaluate(executionContext))) return true;
                        return false;
                    }
                    else
                    {
                        throw new Exception("SmallerEqualCond: both operands must be of the same datatype.[Interpreting]");
                    }
                case TypeCode.String:
                    throw new Exception("SmallerEqualCond: does not support string.[Interpreting]");
                default:
                    break;
            }
            throw new Exception("SmallerEqualCond: Unexpected error.[Interpreting]");
        }
    }
}
