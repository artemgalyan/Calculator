using System.Windows.Forms;

namespace OOPExample
{
    public class ExpressionInputTextBoxHandler : TextBoxHandler
    {
        private const string Characters = "0123456789+-*"; 
        public ExpressionInputTextBoxHandler(TextBox textBox) : base(textBox) {}
        public override void Push(string expression)
        {
            if (!IsMathExpression(expression))
                Erase(1);
        }
        static private bool IsMathExpression(string s)
        {
            foreach (var i in s)
            {
                if (!Characters.Contains(i.ToString()))
                {
                    return false;
                }
            }
            return true;
        }
    }
}