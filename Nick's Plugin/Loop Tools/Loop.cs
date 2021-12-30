using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;
using WBPlugin.Tube_Tools;

namespace WBPlugin.Loop_Tools
{
     public class Loop
    {
        public List<Tube> Tubes { get; private set; }
        
        public Loop(Tube selectedTube)
        {
            Tubes = GetConnectedTubes(selectedTube);
        }

        private List<Tube> GetConnectedTubes(Tube selectedTube)
        {
            TubeManager manager = new TubeManager();
            List<Tube> allTubes = manager.GetAllTubes();

            if (allTubes == null) return null;
            allTubes.Remove(selectedTube);

            List<Tube> connectedTubes = new List<Tube>
            {
                selectedTube
            };

            FillTubeList(ref allTubes, ref connectedTubes);
            return connectedTubes;
        }

        private void FillTubeList(ref List<Tube> allTubes, ref List<Tube> connectedTubes)
        {
            bool finished = false;
            
            foreach(Tube potentialTube in allTubes)
            {
                foreach(Tube connectedTube in connectedTubes)
                {
                    if (connectedTube.IsConnected(potentialTube))
                    {
                        connectedTubes.Add(potentialTube);
                        allTubes.Remove(potentialTube);
                        
                        FillTubeList(ref allTubes, ref connectedTubes);
                        finished = true;
                        break;
                    }
                    
                }
                if (finished) break;
            }

        }

        public double GetLength()
        {
            double total = 0;

            foreach(Tube tube in Tubes)
            {
                total += tube.Length;
            }

            total /= 12;
            total += 9;//took from the original code, unclear why adding 9 rounds the number out
            
            return total + (5 - (total % 5));
        }
    }
}
