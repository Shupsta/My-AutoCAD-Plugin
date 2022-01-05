using System;
using Autodesk.AutoCAD.EditorInput;

namespace WBPlugin.Retrievers
{
    internal class NewDoubleInputRetriever
    {
        internal static double Get(string prompt)
        {
            return Get(prompt, 0);
        }

        internal static double Get(string prompt, double defaultValue)
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