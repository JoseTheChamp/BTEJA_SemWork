﻿using BTEJA_SemWork.ParserClasses.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTEJA_SemWork.ParserClasses
{
    public class IdentExpression : Expression
    {
        public string Ident { get; set; }

        public override object Evaluate(MyExecutionContext executionContext)
        {
            return executionContext.Variables.Get(Ident);
        }
    }
}
