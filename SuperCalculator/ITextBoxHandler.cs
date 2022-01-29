namespace OOPExample
{
    public interface ITextBoxHandler
    {
        void Push(string expression);
        void Erase(int numberOfSymbols = 1);
        string GetExpression();
        void Set(string expression);
        void Clear();
    }
}