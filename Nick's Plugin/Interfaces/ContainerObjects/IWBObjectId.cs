using Autodesk.AutoCAD.DatabaseServices;

namespace WBPlugin
{
    public interface IWBObjectId
    {
        long Handle { get; }

        bool Equals(IWBObjectId other);
        bool IsNull();

        //ObjectId GetId();
    }
}