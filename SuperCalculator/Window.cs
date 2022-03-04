using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOPExample
{
    public partial class Form1 : Form
    {
        private readonly TextBoxHandler _expressionBox;
        private readonly TextBoxHandler _answerBox;
        private bool _expressionFieldNeedsToBeCleared;

        public Form1()
        {
            InitializeComponent();
            _expressionBox = new TextBoxHandler(ExpressionTextBox);
            _answerBox = new TextBoxHandler(AnswerTextBox);
            _expressionFieldNeedsToBeCleared = false;
        }

        private void ClearTextFieldIfNeeded()
        {
            if (_expressionFieldNeedsToBeCleared)
            {
                _expressionBox.Clear();
                _expressionFieldNeedsToBeCleared = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearTextFieldIfNeeded();
            _expressionBox.Push("1");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearTextFieldIfNeeded();
            _expressionBox.Push("2");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClearTextFieldIfNeeded();
            _expressionBox.Push("3");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ClearTextFieldIfNeeded();
            _expressionBox.Push("4");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ClearTextFieldIfNeeded();
            _expressionBox.Push("5");
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            ClearTextFieldIfNeeded();
            _expressionBox.Push("6");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ClearTextFieldIfNeeded();
            _expressionBox.Push("7");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ClearTextFieldIfNeeded();
            _expressionBox.Push("8");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ClearTextFieldIfNeeded();
            _expressionBox.Push("9");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ClearTextFieldIfNeeded();
            _expressionBox.Push("0");
        }

        private void buttonPlus_Click(object sender, EventArgs e)
        {
            ClearTextFieldIfNeeded();
            _expressionBox.Push("+");
        }

        private void buttonMinus_Click(object sender, EventArgs e)
        {
            ClearTextFieldIfNeeded();
            _expressionBox.Push("-");
        }

        private void buttonMultiply_Click(object sender, EventArgs e)
        {
            ClearTextFieldIfNeeded();
            _expressionBox.Push("*");
        }

        private async void buttonEquals_Click(object sender, EventArgs e)
        {
            string expression = _expressionBox.GetExpression();
            var calculator = ExpressionCalculator.GetInstance();
            string result;
            result = await calculator.EvaluateExpression(expression);
            _answerBox.Set(result);
            _expressionFieldNeedsToBeCleared = true;
        }

        private void buttonOpenBracket_Click(object sender, EventArgs e)
        {
            ClearTextFieldIfNeeded();
            _expressionBox.Push("(");
        }

        private void buttonCloseBracket_Click(object sender, EventArgs e)
        {
            ClearTextFieldIfNeeded();
            _expressionBox.Push(")");
        }

        private void buttonDivision_Click(object sender, EventArgs e)
        {
            ClearTextFieldIfNeeded();
            _expressionBox.Push("/");
        }

        private void buttonMin_Click(object sender, EventArgs e)
        {
            ClearTextFieldIfNeeded();
            _expressionBox.Push("min(");
        }

        private void buttonMax_Click(object sender, EventArgs e)
        {
            ClearTextFieldIfNeeded();
            _expressionBox.Push("max(");
        }

        private void buttonComma_Click(object sender, EventArgs e)
        {
            ClearTextFieldIfNeeded();
            _expressionBox.Push(", ");
        }

        private void buttonPowFunction_Click(object sender, EventArgs e)
        {
            ClearTextFieldIfNeeded();
            _expressionBox.Push("pow(");
        }

        private void buttonCosFunction_Click(object sender, EventArgs e)
        {
            ClearTextFieldIfNeeded();
            _expressionBox.Push("cos(");
        }

        private void buttonSinFunction_Click(object sender, EventArgs e)
        {
            ClearTextFieldIfNeeded();
            _expressionBox.Push("sin(");
        }

        private void buttonTgFunction_Click(object sender, EventArgs e)
        {
            ClearTextFieldIfNeeded();
            _expressionBox.Push("tg(");
        }
    }
}