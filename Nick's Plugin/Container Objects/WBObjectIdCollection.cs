using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBPlugin
{
    public class WBObjectIdCollection
    {
        private readonly List<long> _idCollection;

        public WBObjectIdCollection()
        {
            _idCollection = null;
        }

        public WBObjectIdCollection(List<long> incomingCollection)
        {
            _idCollection = incomingCollection;
        }

        public bool IsNull()
        {
            if (_idCollection != null)
                return _idCollection.Count == 0;
            else
                return true;
        }

        public List<long> IdCollection { get => _idCollection; }
    }
}
