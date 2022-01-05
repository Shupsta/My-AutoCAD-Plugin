using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBPlugin.Retrievers;

namespace WBPlugin.Joist_Tool
{
    public class NewJoists
    {
        public static void RunJoists()
        {
            double joistSpacing = NewDoubleInputRetriever.Get("\nJoist Spacing: ", 16);
            if (joistSpacing == 0) return;


        }

        
    }
}
