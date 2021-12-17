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
        public double getUserInput()
        {
            return getUserInput("\nPick direction :");
        }

        public double getUserInput(string prompt)
        {
            return getUserInput(prompt, new WBPoint3d());
        }

        public virtual double getUserInput(string prompt, WBPoint3d point)
        {
            Editor ed = Active.Editor;

            PromptAngleOptions pao = new PromptAngleOptions(prompt);
            pao.AllowNone = false;
            pao.BasePoint = PointGenerator.GetPoint(point);
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
