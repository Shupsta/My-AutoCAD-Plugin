namespace WBPlugin
{
    public interface IPointInputRetriever
    {
        WBPoint3d getUserInput(string prompt);
        WBPoint3d getUserInput(string prompt, bool allowNone);
        WBPoint3d getUserInput(string prompt, WBPoint3d defaultValue);
    }
}