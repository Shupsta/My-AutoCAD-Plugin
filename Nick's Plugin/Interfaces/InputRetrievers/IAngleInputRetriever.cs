namespace WBPlugin
{
    public interface IAngleInputRetriever
    {
        double getUserInput();
        double getUserInput(string prompt);
        double getUserInput(string prompt, WBPoint3d point);
    }
}