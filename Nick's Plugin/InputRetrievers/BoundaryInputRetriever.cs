using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using WBPlugin;
using WBPlugin.Utilities;

namespace WBPlugin
{
    public class BoundaryInputRetriever : IBoundaryInputRetriever
    {
        public virtual WBObjectIdCollection GetUserInput(string prompt)
        {
            Editor ed = Active.Editor;
            Document doc = Active.Document;

            using (DocumentLock dimlock = doc.LockDocument())//TODO might not need this lock
            {
                try
                {
                    PromptSelectionResult psr = GetSelectionResult(prompt, ed);

                    if (psr.Status != PromptStatus.OK)
                    {
                        ed.WriteMessage("\nNothing selected.");

                        return new WBObjectIdCollection(new List<WBObjectId>());
                    }

                    ObjectIdCollection idCollection = ProcessObjectIds(psr.Value);

                    

                    return new WBObjectIdCollection(ObjectIdTranslator.Encode(idCollection));
                }
                catch (Autodesk.AutoCAD.Runtime.Exception ex)
                {

                    ed.WriteMessage("\nProblem occured in BoundaryInputRetriever because " + ex.Message.ToString());
                    return new WBObjectIdCollection(new List<WBObjectId>());
                }
            }
        }

        private PromptSelectionResult GetSelectionResult(string prompt, Editor ed)
        {
            TypedValue[] values = { new TypedValue((int)DxfCode.Start, "ARC,LINE,LWPOLYLINE,CIRCLE") };
            SelectionFilter sFilter = new SelectionFilter(values);

            PromptSelectionOptions pso = new PromptSelectionOptions() { MessageForAdding = prompt, SingleOnly = false };

            return ed.GetSelection(pso, sFilter);

        }

        private ObjectIdCollection ProcessObjectIds(SelectionSet input)
        {
            ObjectIdCollection returnObj = new ObjectIdCollection();
            ObjectId[] idArray = input.GetObjectIds();

            foreach (ObjectId oID in idArray)
            {
                returnObj.Add(oID);
            }

            return returnObj;

        }
    }
}
