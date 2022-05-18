
/* Creador: Dina Rodríguez 
 * 
 * Programa: Monitors a folder
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
        //GLOBALES
        FileStream v;
        string dir;

        /*
        static void Main()
        {
            
        } //End MAIN
        */
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
                //Gets address
                textBox1.Text = Path.GetFullPath(opfolder.SelectedPath);
            }
            dir = Convert.ToString(textBox1.Text);

            FileInfo fi = new FileInfo(dir);
            // Get file extension   
            string extn = fi.Extension;
            textBox2.Text = extn;

            //Monitor
            FileSystemWatcher watcher = new FileSystemWatcher(@dir);

            watcher.NotifyFilter = NotifyFilters.Attributes
                             | NotifyFilters.CreationTime
                             | NotifyFilters.DirectoryName
                             | NotifyFilters.FileName
                             | NotifyFilters.LastAccess
                             | NotifyFilters.LastWrite
                             | NotifyFilters.Security
                             | NotifyFilters.Size;
            
            watcher.EnableRaisingEvents = true;
        

            watcher.Changed += Action2FolderPath;
            watcher.Created += Action2FolderPath;
            watcher.Deleted += Action2FolderPath;
            watcher.Renamed += Action2FolderPath;


        }

        private void Action2FolderPath(object sender, FileSystemEventArgs e)
        {
            var result = MessageBox.Show("Change", "Message",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
