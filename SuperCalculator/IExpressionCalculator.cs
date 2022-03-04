using System.Threading.Tasks;

namespace OOPExample
{
    public interface IExpressionCalculator
    {
        Task<string> EvaluateExpression(string expression);
    }
}