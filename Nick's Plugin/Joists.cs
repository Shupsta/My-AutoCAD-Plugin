using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nick_s_Plugin
{
    class Joists
    {
        private const double defaultJoistSpcaing = 16;

        public double JoistSpacing { get; set; }

        public Joists()
        {
            JoistSpacing = defaultJoistSpcaing;
        }

        public static void runJoists()
        {

        }

        private void GetSpacing()
        {
            JoistSpacing = 0;
        }
    }
}
