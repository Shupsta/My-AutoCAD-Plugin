namespace WBPlugin
{
    public interface IDoubleInputRetriever
    {
        double getUserInput(string prompt);
        double getUserInput(string prompt, double defaultValue);
    }
}