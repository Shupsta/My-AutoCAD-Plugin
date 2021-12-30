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

        public static int GetColorForZone(int zoneNumber)
        {
            List<int> DefaultColors = new List<int>() { 1, 116, 30, 5, 40, 190 };

            
            if (zoneNumber % 6 == 0)
                zoneNumber = 6;
            else
                zoneNumber %= 6;
            return DefaultColors[zoneNumber - 1]; //6 colors, in a 0 indexed array. so zone 10 % 6 = 4 - 1 = index 3 which is the fourth color
        }

        public static int GetColor(WBObjectId WBid)
        {
            Database db = Active.Database;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                ObjectId id = WBid.GetId();
                Entity ent = (Entity)id.GetObject(OpenMode.ForRead, false);
                return ent.Color.ColorIndex;
            }
        }
    }
}