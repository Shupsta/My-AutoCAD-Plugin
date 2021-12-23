using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.EditorInput;

namespace WBPlugin
{
    public class KeywordInputRetriever
    {
        public static string GetUserInput(string prompt, params string[] keyWords)
        {
            Editor ed = Active.Editor;

            PromptKeywordOptions promptKeywordOptions = new PromptKeywordOptions(prompt);

            foreach(string keyword in keyWords)
            {
                promptKeywordOptions.Keywords.Add(keyword);
            }

            promptKeywordOptions.AllowNone = false;

            PromptResult promptKeywordResult = ed.GetKeywords(promptKeywordOptions);

            if (promptKeywordResult.Status != PromptStatus.OK)
            {
                ed.WriteMessage("\n*Cancel*");
                return "-99999";
            }

            return promptKeywordResult.StringResult;

        }

        
    }
}
