namespace WBPlugin
{
    public interface IBoundaryInputRetriever
    {
        WBObjectIdCollection getUserInput(string prompt);
    }
}