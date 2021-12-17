using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace WBPlugin
{
    public class WBLayerTableRecord : IWBLayerTableRecord
    {
        private readonly LayerTableRecord _layerTableRecord;

        public WBLayerTableRecord(LayerTableRecord ltr)
        {
            _layerTableRecord = ltr;
        }

        public bool isNull()
        {
            return _layerTableRecord == null ? true : false;
        }
    }
}
