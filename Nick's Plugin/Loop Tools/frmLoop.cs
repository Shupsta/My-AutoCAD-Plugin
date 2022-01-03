using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace HNCWarmboard
{
    public partial class frmLoop : Form
    {
        //public Int32 Manifold
        //{
        //    get;
        //    set;
        //}

        //public  Int32 Zone
        //{
        //    get;
        //    set;
        //}

        //public  Int32 Loop
        //{
        //    get;
        //    set;
        //}

        public frmLoop()
        {
            InitializeComponent();
            txtaddlen.Text = "0";
        }

        private void frmLoop_Load(object sender, EventArgs e)
        {
            txtManifold.Text = Tubes.LoopTag.Manifold;
            //txtRoom.Text = Tubes.LoopTag.Room.ToString();
            
            txtLoop.Text = Convert.ToString(Tubes.LoopTag.Loop + 1);
        }

        private void frmLoop_Activated(object sender, EventArgs e)
        {
            
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            try
            {   
                
                Tubes.LoopTag.Manifold  = txtManifold.Text;
                Tubes.LoopTag.Room = txtRoom.Text;
                Tubes.LoopTag.Length_Add = Convert.ToInt32(txtaddlen.Text);
                Tubes.LoopTag.Loop  = Convert.ToInt32(txtLoop.Text);

                this.Hide();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error saving values as integers. Error is  " + ex.Message);
                new WarmboardToolsException("Error saving values as integers. in frmLoops.cmdOk_Click", ex);
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        
    }
}
