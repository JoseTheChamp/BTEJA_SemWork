﻿using BTEJA_SemWork.ParserClasses.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTEJA_SemWork.ParserClasses
{
    public class IfStatement : Statement
    {
        public IfStatement(int line, int token,Condition condition) : base(line, token)
        {
            Condition = condition;
            Statements = new List<Statement>();
            ElseStatements = new List<Statement>();
        }

        public Condition Condition { get; set; }
        public List<Statement> Statements { get; set; }
        public List<Statement> ElseStatements { get; set; }


        public override object? Execute(MyExecutionContext executionContext)
        {
            if (Convert.ToBoolean(Condition.Evaluate(executionContext)))
            {
                MyExecutionContext innerExecutionContext = (MyExecutionContext)executionContext.Clone();
                foreach (Statement statement in Statements)
                {
                    object? result = statement.Execute(innerExecutionContext);
                    if (result != null) return result;
                }
            }
            else {
                MyExecutionContext innerExecutionContext = (MyExecutionContext)executionContext.Clone();
                foreach (Statement statement in ElseStatements)
                {
                    object? result = statement.Execute(innerExecutionContext);
                    if (result != null) return result;
                }
            }
            return null;
        }
    }
}
