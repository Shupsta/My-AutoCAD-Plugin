namespace WBPlugin
{
    public interface IBoundaryInputRetriever
    {
        WBObjectIdCollection GetUserInput(string prompt);
    }
}