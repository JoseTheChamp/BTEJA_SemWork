﻿using BTEJA_SemWork.ParserClasses.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTEJA_SemWork.ParserClasses
{
    public class Divide : BinaryExpression
    {
        public override object Evaluate(MyExecutionContext executionContext)
        {
            object leftValue = Left.Evaluate(executionContext);
            switch (Type.GetTypeCode(leftValue.GetType()))
            {
                case TypeCode.Int32:
                    if (Right.GetType() == Left.GetType())
                    {
                        return (int)(Convert.ToInt32(leftValue) / Convert.ToInt32(Right.Evaluate(executionContext)));
                    }
                    else
                    {
                        throw new Exception("Dividing: both operands must be of the same datatype.");
                    }
                case TypeCode.Double:
                    if (Right.GetType() == Left.GetType())
                    {
                        return Convert.ToDouble(leftValue) / Convert.ToDouble(Right.Evaluate(executionContext));
                    }
                    else {
                        throw new Exception("Dividing: both operands must be of the same datatype.");
                    }
                case TypeCode.String:
                    throw new Exception("Dividing: Dividing strings is not supported.");
                default:
                    break;
            }


            return null;
        }
    }
}