
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
        int cont = 0;

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


            //Monitor
            FileSystemWatcher watcher = new FileSystemWatcher(@dir);
             //Selects filters for notification 
            watcher.NotifyFilter = NotifyFilters.Attributes
                             | NotifyFilters.CreationTime
                             /*
                             | NotifyFilters.DirectoryName 
                             | NotifyFilters.FileName
                             | NotifyFilters.LastAccess
                             | NotifyFilters.LastWrite
                             | NotifyFilters.Security */
                             | NotifyFilters.Size;
            
            watcher.EnableRaisingEvents = true;
        

            watcher.Changed += Action2FolderPath;
            watcher.Created += Action2FolderPath;
            /*
            watcher.Deleted += Action2FolderPath;
            watcher.Renamed += Action2FolderPath;
            */
            ProcessDirectory(dir);
            var result1 = MessageBox.Show(cont.ToString(), "Cuenta",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);


        } //end Open Folder

        private void Action2FolderPath(object sender, FileSystemEventArgs e)
        {
            var result = MessageBox.Show("Change", "Message",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);


        }
        // Process all files in the directory passed in, recurse on any directories
        // that are found, and process the files they contain.
        public void ProcessDirectory(string targetDirectory)
        {   
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
            {
                ProcessFile(fileName);
               // cont++; checking it co
            }

        }
        public void ProcessFile(string path) //process once files are checked
        {
            FileInfo fi = new FileInfo(path);
            // Get file extension   
            string extn = fi.Extension;
            string filename = fi.Name;
            textBox2.Text = extn;
            
            if (extn != ".txt") //compares extension
            {
                //string newfoldername = @"\NotApplicable";
                //obtains new address for new file
                folderCreateMove(dir, "NotApplicable", filename, path);
            }
            else
            {
                folderCreateMove(dir, "Processed", filename, path);
            }
        }
        //Creates folder
        private void folderCreateMove(string OGaddress, string FolderName, string FileName, string filepath )
        {
            string newdir = System.IO.Path.Combine(OGaddress, FolderName);
            string newfile = System.IO.Path.Combine(newdir, FileName); //creates new address

            System.IO.Directory.CreateDirectory(newdir); //creates folder if not existing
            System.IO.File.Move(filepath, newfile);

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
