using BTEJA_SemWork.ParserClasses.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTEJA_SemWork.ParserClasses
{
    public abstract class Expression : IEvaluable
    {
        public abstract object Evaluate(MyExecutionContext executionContext);
    }
}
