using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Another_Damn_File_Manager{
    public partial class Form1 : Form{

        private string path;
        public Form1(){
            InitializeComponent();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e){
            Close();
        }

        private void aboutProgramToolStripMenuItem_Click(object sender, EventArgs e){
            MessageBox.Show("Another Damn File Manager - Ещё один чёртов файловый менеджер.\n\nПроизведено ручками d1maz. - Дмитрий Якимов.\nДля свободного использования.\n\nРаспространяется по лицензии GPL-3.0\n\nd1maz.ru/adfm\ngithub.com/MYZT3RY/adfm\n\n2019 - 2019", "О прорамме");
        }

        private void Form1_Load(object sender, EventArgs e){
            initListView(System.IO.Directory.GetCurrentDirectory(), listView1, imageList1);
        }

        private void initListView(string path, ListView listView, ImageList imageList){
            listView.Clear();
            currentDirectoryTextBox.Text = path;
            foreach (var element in System.IO.Directory.GetDirectories(path)){
                listView.Items.Add(element.Substring(element.LastIndexOf("\\") + 1), imageList.Images.Count - 2);
            }
            foreach (var element in System.IO.Directory.GetFiles(path)){
                string extension = System.IO.Path.GetExtension(element);
                if(extension == ".png" || extension == ".ico"){
                    listView.Items.Add(element.Substring(element.LastIndexOf("\\") + 1), imageList.Images.Count - 3);
                }
                else if (extension == ".exe"){
                    listView.Items.Add(element.Substring(element.LastIndexOf("\\") + 1), imageList.Images.Count - 1);
                }
                else if (extension == ".zip"){
                    listView.Items.Add(element.Substring(element.LastIndexOf("\\") + 1), imageList.Images.Count - 5);
                }
                else if(extension == ".cs" || extension == ".c" || extension == ".cpp" || extension == ".pas"){
                    listView.Items.Add(element.Substring(element.LastIndexOf("\\") + 1), imageList.Images.Count - 6);
                }
                else{
                    listView.Items.Add(element.Substring(element.LastIndexOf("\\") + 1), imageList.Images.Count - 4);
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e){
            
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e){
            path = System.IO.Directory.GetCurrentDirectory() + "\\" + e.Item.Text;
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e){
            if (System.IO.Directory.Exists(path)){
                System.IO.Directory.SetCurrentDirectory(path);
                initListView(path, listView1, imageList1);
            }
            else{
                System.Diagnostics.Process.Start(path);
            }
        }

        private void currentDirectoryTextBox_KeyDown(object sender, KeyEventArgs e){
            if(e.KeyCode == Keys.Enter){
                string tmpPath;
                tmpPath = currentDirectoryTextBox.Text;
                System.IO.Directory.SetCurrentDirectory(tmpPath);
                initListView(path, listView1, imageList1);
            }
        }

        private void arrayLeftButton_Click(object sender, EventArgs e){
            var tmpPath = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory());
            System.IO.Directory.SetCurrentDirectory(tmpPath.ToString());
            initListView(tmpPath.ToString(), listView1, imageList1);
        }

        private void refreshButton_Click(object sender, EventArgs e){
            initListView(System.IO.Directory.GetCurrentDirectory(), listView1, imageList1);
        }
    }
}
