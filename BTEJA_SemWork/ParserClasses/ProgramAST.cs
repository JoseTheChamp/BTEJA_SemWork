using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTEJA_SemWork.ParserClasses
{
    public class ProgramAST
    {
        public List<Statement> Statements { get; set; }

        public ProgramAST()
        {
            Statements = new List<Statement>();
        }
    }
}
