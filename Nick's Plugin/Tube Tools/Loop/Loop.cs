using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace WBPlugin.Tube_Tools
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
            allTubes.RemoveAll(t => t.Entity.ObjectId == selectedTube.Entity.ObjectId);
            
            List<Tube> connectedTubes = new List<Tube>();
            connectedTubes.Add(selectedTube);

            FillTubeList(ref allTubes, ref connectedTubes);
            
        }

        private void FillTubeList(ref List<Tube> allTubes, ref List<Tube> connectedTubes)//TODO TEST! recursive!
        {
            
            foreach(Tube potentialTube in allTubes)
            {
                foreach(Tube connectedTube in connectedTubes)
                {
                    if (connectedTube.IsConnected(potentialTube))
                    {
                        connectedTubes.Add(potentialTube);
                        allTubes.Remove(potentialTube);
                        FillTubeList(ref allTubes, ref connectedTubes);
                    }
                    
                }
            }

        }
    }
}
