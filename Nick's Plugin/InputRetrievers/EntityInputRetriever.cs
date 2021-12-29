using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBPlugin.Utilities;

namespace WBPlugin
{
    public class EntityInputRetriever
    {
        public static WBEntity GetUserInput(String prompt)//todo change entity to WBEntity
        {
            Editor ed = Active.Editor;

            PromptEntityOptions promptEntityOptions = new PromptEntityOptions(prompt) { 
                AllowObjectOnLockedLayer = true };


            PromptEntityResult per = ed.GetEntity(promptEntityOptions);
            if (per.Status != PromptStatus.OK)
            {
                ed.WriteMessage("\n*Cancel*");
                return new WBEntity(new WBObjectId(0));
            }

            if (per.Status == PromptStatus.None)
            {
                return new WBEntity(new WBObjectId(0));
            }

            
            return Create(per.ObjectId);
        }

        private static WBEntity Create(ObjectId rawId)
        {
            WBEntity newEntity = new WBEntity(new WBObjectId(rawId));
            
            return newEntity;
        }
    }
}
