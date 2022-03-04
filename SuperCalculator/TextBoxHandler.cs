using System.Windows.Forms;

namespace OOPExample
{
    public class TextBoxHandler : ITextBoxHandler
    {
        private readonly TextBox _textBox;
        private string _text;
        public TextBoxHandler(TextBox textBox)
        {
            _textBox = textBox;
            _text = "";
        }
        public virtual void Push(string expression)
        {
            _text += expression;
            UpdateText();
        }

        public void Erase(int numberOfSymbolsToRemove = 1)
        {
            if (_text.Length > numberOfSymbolsToRemove)
            {
                _text.Remove(_text.Length - numberOfSymbolsToRemove + 1, numberOfSymbolsToRemove);
                UpdateText();
            }
            else if (_text.Length == numberOfSymbolsToRemove)
            {
                Clear();
            }
        }

        private void UpdateText()
        {
            _textBox.Text = _text;
        }

        public string GetExpression()
        {
            return _textBox.Text;
        }

        public void Set(string expression)
        {
            _text = expression;
            UpdateText();
        }

        public void Clear()
        {
            _text = "";
            UpdateText();
        }
    }
}