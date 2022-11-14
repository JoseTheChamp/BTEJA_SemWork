using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTEJA_SemWork.ParserClasses.Context
{
    public class Function
    {
        public string Ident { get; set; }
        public DataType? ReturnType { get; set; }
        public List<Parameter> Parameters { get; set; }
        public List<Statement> Statements { get; set; }

        public Function(string ident, DataType? returnType, List<Parameter> parameters, List<Statement> statements)
        {
            Ident = ident;
            ReturnType = returnType;
            Parameters = parameters;
            Statements = statements;
        }

        public object? Execute(MyExecutionContext executionContext,List<Expression> paramss) {
            //Execution kontex obohatit o parametry
            foreach (Statement statement in Statements)
            {
                object? result = statement.Execute(executionContext);
                if (result != null)
                {
                    switch (Type.GetTypeCode(result.GetType()))
                    {
                        case TypeCode.Int32:
                            if (ReturnType == DataType.Int)
                            {
                                return Convert.ToInt32(result);
                            }
                            else {
                                throw new Exception("Function: " + Ident + "- this function was supposed to return int.[Interpreting]");
                            }
                        case TypeCode.Double:
                            if (ReturnType == DataType.Double)
                            {
                                return Convert.ToDouble(result);
                            }
                            else
                            {
                                throw new Exception("Function: " + Ident + "- this function was supposed to return double.[Interpreting]");
                            }
                        case TypeCode.String:
                            if (ReturnType == DataType.String)
                            {
                                return Convert.ToString(result);
                            }
                            else
                            {
                                throw new Exception("Function: " + Ident + "- this function was supposed to return string.[Interpreting]");
                            }
                        default:
                            throw new Exception("Function: unexpected error.[Interpreting]");
                    }
                }
            }
            if (ReturnType != null)
            {
                throw new Exception("Function: Expected return statement in function with return type.[Interpreting]");
            }
            return null;
        }
    }
}
