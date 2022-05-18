
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
//using Spire.Xls;
using Aspose.Cells;


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
        string dir, newdir, newdirexcel, newfile, processedfolder;
        int cont = 0;
        Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
        Microsoft.Office.Interop.Excel.Workbook MASTERwb;
        /*
        static void Main()
        {
            
        } //End MAIN
        */
        private void button1_Click(object sender, EventArgs e)
        { //select the file
            OpenFileDialog opfile = new OpenFileDialog();
            FolderBrowserDialog opfolder = new FolderBrowserDialog();
            
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

            ProcessDirectory(dir);
            MASTERwb.Close();
            xlApp.Quit();

            ProcessExcels(processedfolder);
            /*
        var result1 = MessageBox.Show(cont.ToString(), "Cuenta",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question);
            */

        } //end Open Folder

        private void Action2FolderPath(object sender, FileSystemEventArgs e)
        {/*
            var result = MessageBox.Show("Change", "Message",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);
            */
            ProcessDirectory(dir);
            MASTERwb.Close();
            xlApp.Quit();

            ProcessExcels(processedfolder);

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

        public void ProcessExcels(string pathexcel) //Combines excels
        {
            FileInfo fi1 = new FileInfo(pathexcel);
            // Get file extension   
            string extn = fi1.Extension;
            //string filename = fi1.Name;

            string[] fileEntries = Directory.GetFiles(pathexcel);
            Workbook SourceBook1 = new Workbook(newdirexcel);

            foreach (string fileName in fileEntries)
            {
                if (fileName != newdirexcel) //No añade repeticion de MASTERwb
                {
                    Workbook SourceBook2 = new Workbook(fileName);
                    SourceBook1.Combine(SourceBook2);
                    // newWB.Worksheets.AddCopy(sheet, WorksheetCopyType.CopyAll);

                    SourceBook1.Save(newdirexcel);

                }

            }

            //newWB.SaveToFile(@newdirexcel, ExcelVersion.Version2013);

        }

        public void ProcessFile(string path) //process once files are checked
        {
            FileInfo fi = new FileInfo(path);
            // Get file extension   
            string extn = fi.Extension;
            string filename = fi.Name;
            textBox2.Text = extn;

            if (extn != ".xls" && extn != ".xlsx") //compares extension
            {
                folderCreateMove(dir, "NotApplicable", filename, path);
            }
            else
            {
                folderCreateMove(dir, "Processed", filename, path);
                processedfolder = newdir;
                if (xlApp == null) //checks is excel is installed
                {
                    MessageBox.Show("Excel is not properly installed!!");
                    return;
                }
                else //if the file is xsl type
                {
                    MASTERwb = xlApp.Workbooks.Add(Type.Missing); //creates new workbook
                                                                  //
                    newdirexcel = System.IO.Path.Combine(newdir, "MASTERwb.xlsx"); //Creates excel path
                    
                    //Changes excel name
                    object missing = System.Reflection.Missing.Value;
                    try
                    {
                        MASTERwb.SaveAs(@newdirexcel, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, missing, missing,
                        false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                        missing, missing, missing, missing, missing);

                    }
                    catch
                    {
                        //File Exists, will not create
                    }

                    //MASTERwb.Save(); //Saves file addition
                }
            }
        }
        //Creates folder
        private void folderCreateMove(string OGaddress, string FolderName, string FileName, string filepath)
        {
            //obtains new address for new file in new folder
            newdir = System.IO.Path.Combine(OGaddress, FolderName);
            //
            newfile = System.IO.Path.Combine(newdir, FileName); //creates new address

            System.IO.Directory.CreateDirectory(newdir); //creates folder if not existing
            System.IO.File.Move(filepath, newfile);

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}