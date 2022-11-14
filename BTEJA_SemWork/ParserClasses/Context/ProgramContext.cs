﻿namespace BTEJA_SemWork.ParserClasses.Context
{
    public class ProgramContext
    {
        public List<Function> Functions { get; set; }

        public ProgramContext()
        {
            Functions = new List<Function>();
        }

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
        public object? Call(string ident, MyExecutionContext executionContext, List<Expression> paramss) {
            foreach (Function fun in Functions)
            {
                if (fun.Ident == ident)
                {
                    return fun.Execute(executionContext,paramss);  
                }
            }
            if (ident == "print")
            {
                print(Convert.ToString(paramss[0]?.Evaluate(executionContext)));
                return null;
            }
            throw new Exception("This method was not defined. [" + ident + "]");
        }

        private void print(string str) {
            Console.WriteLine(str);
        }
    }
}