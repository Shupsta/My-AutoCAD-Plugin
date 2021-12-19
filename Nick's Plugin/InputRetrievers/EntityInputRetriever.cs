using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBPlugin
{
    class EntityInputRetriever
    {
        public ObjectId getUserInput(String prompt)//todo change entity to WBEntity
        {
            Editor ed = Active.Editor;

            PromptEntityOptions promptEntityOptions = new PromptEntityOptions(prompt) { 
                AllowObjectOnLockedLayer = true };


            PromptEntityResult per = ed.GetEntity(promptEntityOptions);
            if (per.Status != PromptStatus.OK)
            {
                ed.WriteMessage("\n*Cancel*");
                return new ObjectId();//TODO remove null
            }

            if (per.Status == PromptStatus.None)
            {
                return new ObjectId();//TODO
            }

            return per.ObjectId;
        }
    }
}
