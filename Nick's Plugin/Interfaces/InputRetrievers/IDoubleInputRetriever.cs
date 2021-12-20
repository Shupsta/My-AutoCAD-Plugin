namespace WBPlugin
{
    public interface IDoubleInputRetriever
    {
        double GetUserInput(string prompt);
        double GetUserInput(string prompt, double defaultValue);
    }
}