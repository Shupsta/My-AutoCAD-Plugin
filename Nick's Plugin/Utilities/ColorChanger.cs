﻿// (C) Copyright 2021 by  
//
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Autodesk.AutoCAD.DatabaseServices;

namespace WBPlugin.Utilities
{
    public class ColorChanger
    {
        
        
        public static void ChangeColor(WBObjectId inputObject, int colorToChangeTo)
        {
            ObjectId id = ObjectIdTranslator.Decode(inputObject);

            using(Transaction tr = id.Database.TransactionManager.StartTransaction())
            {
                var zoneLine = (Entity)id.GetObject(OpenMode.ForWrite, false, true);
                zoneLine.Color = Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, (short)colorToChangeTo);

                tr.Commit();
            }
        }

        public static int GetColorForZone(string zoneString)
        {
            List<int> DefaultColors = new List<int>() { 1, 116, 30, 5, 40, 190 };

            int zoneNumber = Convert.ToInt32(Regex.Match(zoneString, @"\d+").Value);
            if (zoneNumber % 6 == 0)
                zoneNumber = 6;
            else
                zoneNumber = zoneNumber % 6;
            return DefaultColors[zoneNumber - 1]; //6 colors, in a 0 indexed array. so zone 10 % 6 = 4 - 1 = index 3 which is the fourth color
        }
    }
}