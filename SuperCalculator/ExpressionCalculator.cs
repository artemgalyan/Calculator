using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace OOPExample
{
    public class ExpressionCalculator : IExpressionCalculator
    {
        private static readonly string[] SupportedOperations = new[] {"+", "-", "*", "/"};


        public enum TokenType
        {
            Function,
            Number,
            Operation,
            Comma,
            OpenBrace,
            CloseBrace,
            Unknown,
            Space
        }

        public class Token
        {
            public string Value { get; }
            public TokenType Type { get; }

            public Token(string value, TokenType type)
            {
                Value = value;
                Type = type;
            }
        }

        private static readonly Lazy<ExpressionCalculator> Instance = new Lazy<ExpressionCalculator>(
            () => new ExpressionCalculator());

        private ExpressionCalculator()
        {
        }

        public static ExpressionCalculator GetInstance() => Instance.Value;

        private static TokenType GetTokenType(string expression)
        {
            int temporary;
            if (expression == ",")
                return TokenType.Comma;
            else if (expression == "(")
                return TokenType.OpenBrace;
            else if (expression == ")")
                return TokenType.CloseBrace;
            else if (expression == " ")
                return TokenType.Space;
            else if (SupportedOperations.Contains(expression))
                return TokenType.Operation;
            else if (int.TryParse(expression, out temporary))
                return TokenType.Number;
            else if (FunctionController.Contains(expression))
                return TokenType.Function;
            else
                return TokenType.Unknown;
        }

        private static int GetOperationPriority(Token token)
        {
            switch (token.Value)
            {
                case "(": return 12;
                case "^": return 7;
                case "*": return 5;
                case "/": return 5;
                case "+": return 2;
                case "-": return 2;
                default: return 0;
            }
        }

        string ProcessExpressionString(in string input)
        {
            string result = input;
            var oldResult = result;
            do
            {
                oldResult = result;
                result = result.Replace(", -", ", 0-");
                result = result.Replace(", +", ", ");
                result = result.Replace("++", "+");
                result = result.Replace("-+", "-");
                result = result.Replace("+-", "-");
                result = result.Replace("--", "+");
                result = result.Replace("(-", "(0-");
                result = result.Replace("(+", "(");
            } while (oldResult != result);

            if (result[0] == '+' || result[0] == '-')
                result = "0" + result;
            return result;
        }

        public async Task<string> EvaluateExpression(string expression)
        {
            return await Task.Run(() =>
            {
                try
                {
                    string processedExpression = ProcessExpressionString(expression);
                    var tokens = SplitExpressionIntoTokens(processedExpression);
                    var reversePolNotation = PrepareExpressionForCalculating(tokens);
                    double result = buttonCalculateOpn(reversePolNotation);
                    return result.ToString(CultureInfo.InvariantCulture);
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            });
        }

        Queue<Token> PrepareExpressionForCalculating(List<Token> tokens)
        {
            Stack<Token> stack = new Stack<Token>();
            Queue<Token> outString = new Queue<Token>();
            foreach (var token in tokens)
            {
                switch (token.Type)
                {
                    case TokenType.Number:
                        outString.Enqueue(token);
                        break;
                    case TokenType.Function:
                        stack.Push(token);
                        break;
                    case TokenType.Comma:
                        while (stack.Count > 0 && stack.Peek().Type != TokenType.OpenBrace)
                        {
                            outString.Enqueue(stack.Pop());
                        }

                        if (stack.Count == 0)
                            throw new Exception("'(' or ',' is missing");
                        break;
                    case TokenType.OpenBrace:
                        stack.Push(token);
                        break;
                    case TokenType.CloseBrace:
                        while (stack.Count > 0 && stack.Peek().Type != TokenType.OpenBrace)
                        {
                            outString.Enqueue(stack.Pop());
                        }

                        if (stack.Count == 0)
                            throw new Exception("'(' is missing");
                        stack.Pop();
                        if (stack.Count > 0 && stack.Peek().Type == TokenType.Function)
                            outString.Enqueue(stack.Pop());
                        break;
                    case TokenType.Operation:
                        int thisPriority = GetOperationPriority(token);
                        while (stack.Count > 0 && GetOperationPriority(stack.Peek()) >= thisPriority
                                               && stack.Peek().Type != TokenType.OpenBrace
                                               && stack.Peek().Type != TokenType.CloseBrace)
                            outString.Enqueue(stack.Pop());
                        stack.Push(token);
                        break;
                }
            }

            while (stack.Count > 0)
            {
                if (stack.Peek().Type == TokenType.OpenBrace)
                    throw new Exception("Wrong number of braces!");
                outString.Enqueue(stack.Pop());
            }

            return outString;
        }

        private List<Token> SplitExpressionIntoTokens(string expression)
        {
            var tokens = new List<Token>();
            string current = "";
            for (int i = 0; i < expression.Length; ++i)
            {
                if (current == "")
                {
                    current += expression[i];
                    continue;
                }

                TokenType currentType = GetTokenType(current);
                TokenType thisCharType = GetTokenType(expression[i].ToString());
                if (currentType == TokenType.OpenBrace || currentType == TokenType.CloseBrace)
                {
                    tokens.Add(new Token(current, currentType));
                    current = expression[i].ToString();
                }
                else if (currentType == thisCharType
                         || currentType == TokenType.Function && thisCharType == TokenType.Unknown
                         || currentType == TokenType.Unknown && thisCharType == TokenType.Function)
                    current += expression[i];
                else
                {
                    if (currentType == TokenType.Unknown)
                        throw new Exception("Expression " + current + " can't be processed!");
                    if (current != " ")
                    {
                        tokens.Add(new Token(current, currentType));
                    }

                    current = expression[i].ToString();
                }
            }

            if (current.Length != 0 && current != " ")
                tokens.Add(new Token(current, GetTokenType(current)));
            return tokens;
        }

        private double buttonCalculateOpn(Queue<Token> outString)
        {
            var numbers = new Stack<double>();
            while (outString.Count > 0)
            {
                var token = outString.Dequeue();
                if (token.Type == TokenType.Number)
                    numbers.Push(double.Parse(token.Value));
                else
                {
                    int numberOfArgs = FunctionController.GetNumberOfArguments(token.Value);
                    var array = new double[numberOfArgs];
                    for (int i = 0; i < numberOfArgs; ++i)
                    {
                        if (numbers.Count == 0)
                            throw new Exception("Wrong expression!");
                        array[i] = numbers.Pop();
                    }

                    numbers.Push(FunctionController.EvaluateException(token, array));
                }
            }

            return numbers.Peek();
        }
    }
}