using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.EditorInput;

namespace WBPlugin
{
    public class StringInputRetriever
    {
        public static string GetUserInput(string prompt)
        {
            Editor ed = Active.Editor;

            PromptStringOptions promptStringOptions = new PromptStringOptions(prompt);

            PromptResult promptStringResult = ed.GetString(promptStringOptions);

            if (promptStringResult.Status != PromptStatus.OK)
            {
                ed.WriteMessage("\n*Cancel*");
                return "-99999";
            }

            return promptStringResult.StringResult;

        }

        public static string GetUserInput(string prompt, string defaultValue)
        {
            Editor ed = Active.Editor;

            PromptStringOptions promptStringOptions = new PromptStringOptions(prompt) { DefaultValue = defaultValue };                       
            
            PromptResult promptStringResult = ed.GetString(promptStringOptions);

            if (promptStringResult.Status != PromptStatus.OK)
            {
                ed.WriteMessage("\n*Cancel*");
                return "-99999";
            }

            return promptStringResult.StringResult;
        }
    }
}
