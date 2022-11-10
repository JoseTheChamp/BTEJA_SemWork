using BTEJA_SemWork.ParserClasses.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTEJA_SemWork.ParserClasses
{
    public interface IEvaluable
    {
        public object Evaluate(MyExecutionContext executionContext);
    }
}
