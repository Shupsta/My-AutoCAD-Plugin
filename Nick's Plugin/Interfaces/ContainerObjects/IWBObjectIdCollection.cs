using System.Collections.Generic;

namespace WBPlugin
{
    public interface IWBObjectIdCollection
    {
        List<IWBObjectId> IdCollection { get; }

        bool IsNull();
    }
}