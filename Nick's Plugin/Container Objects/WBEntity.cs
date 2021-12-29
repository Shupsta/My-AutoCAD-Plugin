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
        private string _layer;
        private string _typeName;
        public WBObjectId ObjectId { get; private set; }
        public string TypeName { get
            {
                if (_typeName != null) return _typeName;
                return GetTypeName();
            } }
        public string Layer { get
            {
                if (_layer != null) return _layer;
                return GetLayer();
            } }
        
        public WBEntity(WBObjectId id)
        {
            ObjectId = id;
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

        public string GetLayer()
        {
            using (Transaction tr = Active.Database.TransactionManager.StartTransaction())
            {
                ObjectId rawId = ObjectId.GetId();
                Entity ent = (Entity)rawId.GetObject(OpenMode.ForRead, false);
                string layer = ent.Layer.ToUpper();
                this._layer = layer;
                return layer;

            }
        }

        public string GetTypeName()
        {
            using (Transaction tr = Active.Database.TransactionManager.StartTransaction())
            {
                ObjectId rawId = ObjectId.GetId();
                Entity ent = (Entity)rawId.GetObject(OpenMode.ForRead, false);
                string typeName = ent.GetType().Name.ToUpper();
                this._typeName = typeName;
                return typeName;

            }


        }

        public static implicit operator WBObjectId(WBEntity e) => e.ObjectId;
    }
}
