﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTEJA_SemWork.ParserClasses
{
    public class NegCondition : Condition
    {
        public Condition Condition { get; set; }

        public NegCondition(Condition condition)
        {
            Condition = condition;
        }
    }
}
