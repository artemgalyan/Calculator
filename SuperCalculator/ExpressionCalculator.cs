using System;

namespace OOPExample
{
    public class ExpressionCalculator : IExpressionCalculator
    {
        private const int MaxAnswer = 500;
        private Random _randomizer = new Random();
        public double EvaluateExpression(string expression)
        {
            return MaxAnswer*_randomizer.NextDouble();
        }
    }
}