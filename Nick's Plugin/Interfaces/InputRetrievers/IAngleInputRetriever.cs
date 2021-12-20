namespace WBPlugin
{
    public interface IAngleInputRetriever
    {
        double GetUserInput();
        double GetUserInput(string prompt);
        double GetUserInput(string prompt, WBPoint3d point);
    }
}