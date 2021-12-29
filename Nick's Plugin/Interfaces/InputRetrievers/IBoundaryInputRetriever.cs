namespace WBPlugin
{
    public interface IBoundaryInputRetriever
    {
        IWBObjectIdCollection GetUserInput(string prompt);
    }
}