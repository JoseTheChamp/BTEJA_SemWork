namespace BTEJA_SemWork.ParserClasses.Context
{
    public class ProgramContext
    {
        public List<Function> Functions { get; set; }

        public ProgramContext(List<Function> functions)
        {
            Functions = functions;
        }
        public void AddFunction(Function function) {
            foreach (var fun in Functions)
            {
                if (fun.Ident == function.Ident)
                {
                    throw new Exception("This function is already defined. [" + function.Ident + "].");
                }
            }
            Functions.Add(function);
        }
        public void Call(string ident, MyExecutionContext executionContext, List<string> paramss) {
            foreach (Function fun in Functions)
            {
                if (fun.Ident == ident)
                {
                    fun.Execute(executionContext,paramss);
                    return;
                }
            }
            throw new Exception("This method was not defined. [" + ident + "]");
        }
    }
}