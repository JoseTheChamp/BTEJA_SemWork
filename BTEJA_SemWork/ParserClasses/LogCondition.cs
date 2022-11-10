using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTEJA_SemWork.ParserClasses
{
    public class LogCondition : Condition
    {
        public List<Condition> Conditions { get; set; }
        public LogCondition()
        {
            Conditions = new List<Condition>();
        }
    }
}
