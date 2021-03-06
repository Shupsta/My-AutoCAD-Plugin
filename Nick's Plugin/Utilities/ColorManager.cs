// (C) Copyright 2021 by  
//
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Autodesk.AutoCAD.DatabaseServices;

namespace WBPlugin.Utilities
{
    public class ColorManager
    {
        
        
        public static void ChangeColor(WBObjectId inputObject, int colorToChangeTo)
        {
            ObjectId id = inputObject.GetId();

            using(Transaction tr = id.Database.TransactionManager.StartTransaction())
            {
                var zoneLine = (Entity)id.GetObject(OpenMode.ForWrite, false, true);
                zoneLine.Color = Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, (short)colorToChangeTo);

                tr.Commit();
            }
        }

        public static void ChangeColors(WBObjectIdCollection inputObjects, int colorToChangeTo)
        {            
            using (Transaction tr = Active.Database.TransactionManager.StartTransaction())
            {
                foreach(WBObjectId wbId in inputObjects.IdCollection)
                {
                    ObjectId id = wbId.GetId();
                    var zoneLine = (Entity)id.GetObject(OpenMode.ForWrite, false, true);
                    zoneLine.Color = Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, (short)colorToChangeTo);
                }
                

                tr.Commit();
            }
        }

        public static int GetColorForZone(int zoneNumber)
        {
            List<int> DefaultColors = new List<int>() { 1, 116, 30, 5, 40, 190 };

            
            if (zoneNumber % 6 == 0)
                zoneNumber = 6;
            else
                zoneNumber %= 6;
            return DefaultColors[zoneNumber - 1]; //6 colors, in a 0 indexed array. so zone 10 % 6 = 4 - 1 = index 3 which is the fourth color
        }

        
    }
}