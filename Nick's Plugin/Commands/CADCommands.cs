// (C) Copyright 2021 by  
//
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using WBPlugin.Utilities;
using WBPlugin.Zone_Tools;
using WBPlugin;

// This line is not mandatory, but improves loading performances
[assembly: CommandClass(typeof(WBPlugin.CADCommands))]

namespace WBPlugin
{
    // This class is instantiated by AutoCAD for each document when
    // a command is called by the user the first time in the context
    // of a given document. In other words, non static data in this class
    // is implicitly per-document!
    public class CADCommands
    {

        #region given commands
        // The CommandMethod attribute can be applied to any public  member 
        // function of any public class.
        // The function should take no arguments and return nothing.
        // If the method is an intance member then the enclosing class is 
        // intantiated for each document. If the member is a static member then
        // the enclosing class is NOT intantiated.
        //
        // NOTE: CommandMethod has overloads where you can provide helpid and
        // context menu.

        // Modal Command with localized name
        [CommandMethod("MyGroup", "MyCommand", "MyCommandLocal", CommandFlags.Modal)]
        public void MyCommand() // This method can have any name
        {
            // Put your command code here
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Editor ed;
            if (doc != null)
            {
                ed = doc.Editor;
                ed.WriteMessage("Hello, this is your first command.");

            }
        }

        // Modal Command with pickfirst selection
        [CommandMethod("MyGroup", "MyPickFirst", "MyPickFirstLocal", CommandFlags.Modal | CommandFlags.UsePickSet)]
        public void MyPickFirst() // This method can have any name
        {
            PromptSelectionResult result = Application.DocumentManager.MdiActiveDocument.Editor.GetSelection();
            if (result.Status == PromptStatus.OK)
            {
                // There are selected entities
                // Put your command using pickfirst set code here
            }
            else
            {
                // There are no selected entities
                // Put your command code here
            }
        }

        // Application Session Command with localized name
        [CommandMethod("MyGroup", "MySessionCmd", "MySessionCmdLocal", CommandFlags.Modal | CommandFlags.Session)]
        public void MySessionCmd() // This method can have any name
        {
            // Put your command code here
        }

        //// LispFunction is similar to CommandMethod but it creates a lisp 
        //// callable function. Many return types are supported not just string
        //// or integer.
        //[LispFunction("MyLispFunction", "MyLispFunctionLocal")]
        //public int MyLispFunction(ResultBuffer args) // This method can have any name
        //{
        //    // Put your command code here

        //    // Return a value to the AutoCAD Lisp Interpreter
        //    return 1;
        //}
        #endregion

        [CommandMethod("WarmboardTools", "WBJoist", CommandFlags.Modal)]
        public static void WBJoist() // This method can have any name
        {
            
            Joists joistObj = new Joists(new DoubleInputRetriever(),
                new PointInputRetriever(),
                new AngleInputRetriever(),
                new BoundaryInputRetriever(),
                new JoistGenerator());
            
        }

        [CommandMethod("WBZones", "WBZone", CommandFlags.Modal)]
        public static void WBZone() // This method can have any name
        {
            ZoneCreater.AddZone();
        }

        [CommandMethod("WBZones", "WBZoneCheck", CommandFlags.Modal)]
        public static void WBZoneCheck() // This method can have any name
        {
            ZoneChecker.CheckZone();
        }

        [CommandMethod("WBZones", "WBZoneRenumber", CommandFlags.Modal)]
        public static void WBZoneRenumber() // This method can have any name
        {
            ZoneReNumber.Renumber();
        }

    }

}
