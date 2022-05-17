
/* Creador: Dina Rodríguez 
 * 
 * Programa: 
 * */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace GenpactTestDeveloper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        FileStream v;
        string dir;



        private void button1_Click(object sender, EventArgs e)
        { //select the file
            OpenFileDialog opfile = new OpenFileDialog();
            FolderBrowserDialog opfolder = new FolderBrowserDialog();
           /* if (opfile.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = Path.GetFullPath(opfile.FileName);
            }
            */
            if (opfolder.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = Path.GetFullPath(opfolder.SelectedPath);
            }
            dir = Convert.ToString(textBox1.Text);

            FileInfo fi = new FileInfo(dir);
            // Get file extension   
            string extn = fi.Extension;
            textBox2.Text = extn;
            

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
