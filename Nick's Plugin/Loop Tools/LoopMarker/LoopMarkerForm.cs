using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;
using WBPlugin.Utilities;

namespace WBPlugin.Loop_Tools
{
    public partial class LoopMarkerForm : Form
    {
        public LoopMarkerForm()
        {
            InitializeComponent();
        }

        private void LoopMarkerForm_Load(object sender, EventArgs e)
        {
            textManifold.Text = GetManifoldNum().ToString();
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {

        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private int GetManifoldNum()
        {
            int answer = 0;
            Active.Database.ForEach<BlockReference>(block =>
            {
                if (block.BlockName == LoopMarker.LoopMarkerBlockName)
                {
                    Active.WriteMessage("found a loop marker!");
                }



            });
            return answer;
        }
    }
}
