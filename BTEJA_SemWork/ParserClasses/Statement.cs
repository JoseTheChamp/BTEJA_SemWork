using BTEJA_SemWork.ParserClasses.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTEJA_SemWork.ParserClasses
{
    public abstract class Statement : Position
    {
        protected Statement(int line, int token) : base(line, token)
        {
        }

        public abstract object? Execute(MyExecutionContext executionContext);
    }
}
