namespace WBPlugin
{
    public interface IPointInputRetriever
    {
        WBPoint3d GetUserInput(string prompt);
        WBPoint3d GetUserInput(string prompt, bool allowNone);
    }
}