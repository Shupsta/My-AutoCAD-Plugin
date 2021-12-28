using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;
using WBPlugin.Utilities;

namespace WBPlugin
{
    public class WBEntity : IEquatable<WBEntity>
    {
        public WBObjectId ObjectId { get; private set; }
        public string TypeName { get; set; }
        public string Layer { get; set; }
        
        public WBEntity(WBObjectId id)
        {
            ObjectId = id;
            Initialize();
        }

        private void Initialize()
        {
            using (Transaction tr = Active.Database.TransactionManager.StartTransaction())
            {
                ObjectId rawId = ObjectIdTranslator.Decode(ObjectId);
                Entity ent = (Entity)rawId.GetObject(OpenMode.ForRead, false);
                this.TypeName = ent.GetType().Name;
                this.Layer = ent.Layer.ToUpper();

            }
        }

        public bool IsNull()
        {
            return this.ObjectId.IsNull();
        }

        public bool Equals(WBEntity other)
        {
            if (this.ObjectId.Equals(other.ObjectId)) return true;
            return false;
        }

        public static implicit operator WBObjectId(WBEntity e) => e.ObjectId;
    }
}
