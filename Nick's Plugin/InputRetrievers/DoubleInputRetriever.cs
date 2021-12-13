﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.EditorInput;

namespace Nick_s_Plugin.InputRetrievers
{
    public class DoubleInputRetriever : IUserInputRetriever<Double>
    {
        public double getUserInput(String prompt, Double defaultValue)
        {
            Editor ed = Active.Editor;

            PromptDoubleOptions pdo = new PromptDoubleOptions(prompt);
            pdo.DefaultValue = defaultValue;
            pdo.AllowNone = true;
            pdo.AllowNegative = false;

            PromptDoubleResult pdr = ed.GetDouble(pdo);
            if (pdr.Status != PromptStatus.OK)
            {
                ed.WriteMessage("\n*Cancel*");
                return 0;
            }

            if (pdr.Status == PromptStatus.None)
            {
                return defaultValue;
            }

            return pdr.Value;
        }
    }
}
