using BTEJA_SemWork.ParserClasses.Context;
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

        public override object Evaluate(MyExecutionContext executionContext)
        {
            bool result = Convert.ToBoolean(Conditions[0].Evaluate(executionContext));
            for (int i = 1; i < Conditions.Count; i++)
            {
                if (Conditions[i].GetType() == typeof(OrCondition))
                {
                    result = result || Convert.ToBoolean(Conditions[i].Evaluate(executionContext));
                }
                else {
                    result = result && Convert.ToBoolean(Conditions[i].Evaluate(executionContext));
                }
            }
            return result;
        }
    }
}
