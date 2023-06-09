using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MAPControl;
using AsMuchAsNeeded;
using output;

namespace Truck_routes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MAPControl.Form1 input = new MAPControl.Form1();
            input.ShowDialog();
           // this.button3.Enabled = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
           bool success=false;
           Cursor.Current = Cursors.WaitCursor;
               success= AsMuchAsNeeded.geogbased.calc();
         //      button3.Enabled = success;
               if (success == false) MessageBox.Show("Δεν βρέθηκε λύση.Ξαναδoκιμάστε νεα ομαδοποίηση");
               if (success == true)  MessageBox.Show("H oμαδοποίηση & η δρομολόγηση ολοκληρώθηκαν επιτυχώς.Η αναφορά ανανεώθηκε");

               Cursor.Current = Cursors.Default;


        }

        private void button3_Click(object sender, EventArgs e)
        {
            output.Form1 outpt = new output.Form1();  
            outpt.ShowDialog();
        }
    }
}
