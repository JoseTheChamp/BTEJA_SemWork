﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTEJA_SemWork.ParserClasses
{
    public class AssignStatement : Statement
    {
        public string Ident { get; set; }
        public Expression Expression { get; set; }
        public AssignStatement(string ident, Expression expression)
        {
            Ident = ident;
            Expression = expression;
        }
    }
}