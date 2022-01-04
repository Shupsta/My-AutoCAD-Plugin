using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            textManifold.Text = LoopMarker.LastManifold;
            textLoop.Text = (LoopMarker.LastLoop + 1).ToString();
            textAddLength.Text = LoopMarker.AdditionalLength.ToString();
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            try
            {
                LoopMarker.LastManifold = textManifold.Text.ToUpper();
                LoopMarker.LastLoop = Convert.ToInt32(textLoop.Text);
                LoopMarker.AdditionalLength = Convert.ToInt32(textAddLength.Text);
                LoopMarker.RoomName = textRoom.Text;

                this.Hide();
            }
            catch(Exception ex)
            {
                Active.WriteMessage("\nError occured in after Clicking Ok, possibly bad input format or character?" + ex.Message);
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        
    }
}
