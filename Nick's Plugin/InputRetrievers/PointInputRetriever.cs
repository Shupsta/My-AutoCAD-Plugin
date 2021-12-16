using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.EditorInput;

namespace WBPlugin
{
    public class PointInputRetriever : IUserInputRetriever<WBPoint3d>
    {
        public WBPoint3d getUserInput(string prompt)
        {
            return getUserInput(prompt, false);
        }

        public virtual WBPoint3d getUserInput(string prompt, bool allowNone)
        {
            Editor ed = Active.Editor;
            PromptPointOptions opts = new PromptPointOptions(prompt);
            opts.AllowNone = allowNone;

            PromptPointResult ppr = ed.GetPoint(opts);

            if (ppr.Status != PromptStatus.OK)
                return new WBPoint3d();
            else
                return new WBPoint3d(ppr.Value);
        }
    }
}
