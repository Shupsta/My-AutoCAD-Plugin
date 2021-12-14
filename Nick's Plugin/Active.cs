using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;

namespace WBPlugin
{
    public static class Active
    {
        public static Document Document
        {
            get { return Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument; }
        }

        public static DocumentCollection DocumentManager
        {
            get { return Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager; }
        }

        public static Editor Editor
        {
            get { return Document.Editor; }
        }

        public static Database Database
        {
            get { return Document.Database; }
        }

        public static void WriteMessage(string message)
        {
            Editor.WriteMessage(message);
        }
    }
}
