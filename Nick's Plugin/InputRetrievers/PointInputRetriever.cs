using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.EditorInput;

namespace WBPlugin
{
    public class PointInputRetriever : IPointInputRetriever
    {
        public WBPoint3d GetUserInput(string prompt)
        {
            return GetUserInput(prompt, false);
        }

        public virtual WBPoint3d GetUserInput(string prompt, bool allowNone)
        {
            Editor ed = Active.Editor;
            PromptPointOptions opts = new PromptPointOptions(prompt);
            opts.AllowNone = allowNone;

            PromptPointResult ppr = ed.GetPoint(opts);

            if (ppr.Status != PromptStatus.OK)
                return new WBPoint3d();
            else
                return new WBPoint3d(ppr.Value.X, ppr.Value.Y);
        }
    }
}
