using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTEJA_SemWork.ParserClasses.Context
{
    public class Variables
    {
        public List<Variable> Vars { get; set; }

        public Variables()
        {
            Vars = new List<Variable>();
        }
        public Variables(List<Variable> vars)
        {
            Vars = vars;
        }
        public void AddVariable(Variable variable) {
            foreach (var item in Vars)
            {
                if (item.Ident == variable.Ident)
                {
                    throw new Exception("Proměná už byla definovaná [" + variable.Ident + "].");
                }
            }
            Vars.Add(variable);
        }
        public object Get(string ident)
        {

            foreach (var var in Vars)
            {
                if (var.Ident == ident)
                {
                    return var.Value;
                }
            }
            throw new Exception("Proměná nebyla definovaná [" + ident + "].");
        }

        public void Set(string ident, object value)
        {
            foreach (var var in Vars)
            {
                if (var.Ident == ident)
                {
                    if (var.IsVal == false)
                    {
                        switch (var.DataType)
                        {
                            case DataType.String:
                                if (value.GetType() == typeof(string))
                                {
                                    var.Value = value;
                                    return;
                                }
                                else {
                                    throw new Exception("You cannot enter this value into string: " + value.ToString());
                                }
                                break;
                            case DataType.Int:
                                if (value.GetType() == typeof(Int32))
                                {
                                    var.Value = value;
                                    return;
                                }
                                else
                                {
                                    throw new Exception("You cannot enter this value into string: " + value.ToString());
                                }
                                break;
                            case DataType.Double:
                                if (value.GetType() == typeof(double))
                                {
                                    var.Value = value;
                                    return;
                                }
                                else
                                {
                                    throw new Exception("You cannot enter this value into string: " + value.ToString());
                                }
                                break;
                            default:
                                throw new Exception("Unexpected exception.");
                        }
                    }
                    else
                    {
                        throw new Exception("Nemůžete zapisovat do konstanty.");
                    }
                }
            }
            throw new Exception("Tato proměnná nebyla definovaná: " + ident);
        }
    }
}
