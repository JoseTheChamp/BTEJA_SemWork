using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTEJA_SemWork.ParserClasses
{
    public class AndCondition : Condition
    {
        public Condition Condition { get; set; }

        public AndCondition(Condition condition)
        {
            Condition = condition;
        }

        public override object Evaluate()
        {
            return Condition.Evaluate();
        }
    }
}
