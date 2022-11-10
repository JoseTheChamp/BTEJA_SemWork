using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTEJA_SemWork.ParserClasses
{
    public class OrCondition : Condition
    {
        public Condition Condition { get; set; }

        public OrCondition(Condition condition)
        {
            Condition = condition;
        }
    }
}
