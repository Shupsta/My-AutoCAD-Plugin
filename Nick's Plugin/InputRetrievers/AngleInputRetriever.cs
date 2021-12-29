using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.EditorInput;
using WBPlugin;
using WBPlugin.Utilities;

namespace WBPlugin
{
    public class AngleInputRetriever : IAngleInputRetriever
    {
        public double GetUserInput()
        {
            return GetUserInput("\nPick direction :");
        }

        public double GetUserInput(string prompt)
        {
            return GetUserInput(prompt, new WBPoint3d());
        }

        public virtual double GetUserInput(string prompt, WBPoint3d point)
        {
            Editor ed = Active.Editor;

            PromptAngleOptions pao = new PromptAngleOptions(prompt);
            pao.AllowNone = false;
            pao.BasePoint = point.GetPoint();
            pao.UseBasePoint = true;
            pao.UseDashedLine = true;

            PromptDoubleResult pdr = ed.GetAngle(pao);
            if (pdr.Status != PromptStatus.OK)
            {
                ed.WriteMessage("\n*Cancel*");
                return -9999;
            }

            return pdr.Value;
        }
    }
}
