using System;
using System.Collections.Generic;
using static OOPExample.ExpressionCalculator;

namespace OOPExample
{
    public static class FunctionController
    {
        private static readonly Dictionary<string, int> Functions = new Dictionary<string, int>()
        {
            {"+", 2}, {"-", 2}, {"*", 2}, {"/", 2}, {"min", 2}, {"max", 2}, {"pow", 2},
            {"sin", 1}, {"cos", 1}, {"tg", 1}
        };

        public static double EvaluateException(Token function, double[] parameters)
        {
            switch (function.Value)
            {
                case "+": return parameters[0] + parameters[1];
                case "-": return parameters[1] - parameters[0];
                case "*": return parameters[0] * parameters[1];
                case "/": return parameters[1] / parameters[0];
                case "min": return parameters[0] > parameters[1] ? parameters[1] : parameters[0];
                case "max": return parameters[0] > parameters[1] ? parameters[0] : parameters[1];
                case "sin": return Math.Sin(parameters[0]);
                case "cos": return Math.Cos(parameters[0]);
                case "tg": return Math.Tan(parameters[0]);
                case "pow": return Math.Pow(parameters[1], parameters[0]);
                default: return 0;
            }
        }

        public static int GetNumberOfArguments(string function)
        {
            return Functions[function];
        }

        public static bool Contains(string function)
        {
            return Functions.ContainsKey(function);
        }
    }
}