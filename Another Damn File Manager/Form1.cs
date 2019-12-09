using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Another_Damn_File_Manager{
    public partial class Form1 : Form{

        private string path;

        private string pathFrom;//путь, откуда копировать
        private string pathTo;//путь, куда копировать

        public Form1(){
            InitializeComponent();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e){
            Close();
        }

        private void aboutProgramToolStripMenuItem_Click(object sender, EventArgs e){
            var form = new aboutProgram();
            form.ShowDialog();
            //MessageBox.Show("Another Damn File Manager - Ещё один чёртов файловый менеджер.\n\nПроизведено ручками d1maz. - Дмитрий Якимов.\nДля свободного использования.\n\nРаспространяется по лицензии GPL-3.0\n\nd1maz.ru/adfm\ngithub.com/MYZT3RY/adfm\n\n2019 - 2019", "О прорамме");
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
                else if(extension == ".cs" || extension == ".c" || extension == ".cpp" || extension == ".pas" || extension == ".h" || extension == ".java" || extension == ".js" || extension == ".lua" || extension == ".php" || extension == ".py"){
                    listView.Items.Add(element.Substring(element.LastIndexOf("\\") + 1), imageList.Images.Count - 6);
                }
                else if(extension == ".dll"){
                    listView.Items.Add(element.Substring(element.LastIndexOf("\\") + 1), imageList.Images.Count - 7);
                }
                else if(extension == ".txt"){
                    listView.Items.Add(element.Substring(element.LastIndexOf("\\") + 1), imageList.Images.Count - 8);
                }
                else if(extension == ".mp4" || extension == ".avi" || extension == ".mkv"){
                    listView.Items.Add(element.Substring(element.LastIndexOf("\\") + 1), imageList.Images.Count - 9);
                }
                else if (extension == ".mp3" || extension == ".flac" || extension == ".m3u" || extension == ".ogg"){
                    listView.Items.Add(element.Substring(element.LastIndexOf("\\") + 1), imageList.Images.Count - 10);
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
                initListView(tmpPath, listView1, imageList1);
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

        private void copyToolStripMenuItem_Click(object sender, EventArgs e){
            ListView.SelectedIndexCollection indexCollection = this.listView1.SelectedIndices;
            try{
                if (indexCollection[0].ToString().Length > 0){
                    MessageBox.Show($"{listView1.Items[indexCollection[0]].Text}");
                }
            }
            catch{
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e){
            ListView.SelectedIndexCollection indexCollection = this.listView1.SelectedIndices;
            try{
                if(listView1.Items[indexCollection[0]].Text.Length == 0){
                    copyToolStripMenuItem.Enabled = false;
                }
                else{
                    copyToolStripMenuItem.Enabled = true;
                }
            }
            catch{

            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e){
            ListView.SelectedIndexCollection indexCollection = this.listView1.SelectedIndices;
            try{
                string path = Directory.GetCurrentDirectory() + "\\" + listView1.Items[indexCollection[0]].Text;
                if (Directory.Exists(path)){
                    Directory.Delete(path);
                    MessageBox.Show($"папка удалена {path}");
                }
                else{
                    File.Delete(path);
                    MessageBox.Show($"файл удалён {path}");
                }
            }
            catch{
                MessageBox.Show("Сначала нужно выбрать объект!","Ошибка!");
            }
        }
    }
}
