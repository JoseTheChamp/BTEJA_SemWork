﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTEJA_SemWork.ParserClasses
{
    public class LesserCond : BinaryCondition
    {
        public LesserCond(Expression left, Expression right) : base(left, right)
        {
        }
    }
}
