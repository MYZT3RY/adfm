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

        private int typeOfPaste=0;//состояние для действия вставки

        private string exp;//для вывода исключений

        public Form1(){
            InitializeComponent();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e){
            Close();
        }

        private void aboutProgramToolStripMenuItem_Click(object sender, EventArgs e){
            var form = new aboutProgram();
            form.ShowDialog();
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
                pathFrom = Directory.GetCurrentDirectory() + "\\" + listView1.Items[indexCollection[0]].Text;
                typeOfPaste = 1;
            }
            catch{
                MessageBox.Show("Сначала нужно выбрать объект!", "Ошибка!");
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e){
            ListView.SelectedIndexCollection indexCollection = this.listView1.SelectedIndices;
            try{
                if(indexCollection.Count == 0){
                    addNewToolStripMenuItem.Visible = true;
                    toolStripSeparator2.Visible = false;
                    if (typeOfPaste == 0){
                        pasteToolStripMenuItem.Visible = false;
                        beforeCopyStripSeparator1.Visible = false;
                    }
                    else{
                        pasteToolStripMenuItem.Visible = true;
                        beforeCopyStripSeparator1.Visible = true;
                    }
                    renameToolStripMenuItem.Visible = false;
                    deleteToolStripMenuItem.Visible = false;
                    cutToolStripMenuItem.Visible = false;
                    copyToolStripMenuItem.Visible = false;
                }
                else{
                    pasteToolStripMenuItem.Visible = true;
                    if (typeOfPaste == 0){
                        pasteToolStripMenuItem.Enabled = false;
                    }
                    else{
                        pasteToolStripMenuItem.Enabled = true;
                    }
                    addNewToolStripMenuItem.Visible = false;
                    toolStripSeparator2.Visible = false;
                    beforeCopyStripSeparator1.Visible = true;
                    renameToolStripMenuItem.Visible = true;
                    deleteToolStripMenuItem.Visible = true;
                    cutToolStripMenuItem.Visible = true;
                    copyToolStripMenuItem.Visible = true;
                }
            }
            catch (Exception exp){
                MessageBox.Show($"{exp.Message}");
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e){
            ListView.SelectedIndexCollection indexCollection = this.listView1.SelectedIndices;
            try{
                string path = Directory.GetCurrentDirectory() + "\\" + listView1.Items[indexCollection[0]].Text;
                if (Directory.Exists(path)){
                    Directory.Delete(path);
                    MessageBox.Show("Папка была успешно удалена!","Уведомление");
                }
                else{
                    File.Delete(path);
                    MessageBox.Show("Файл был успешно удалён!","Уведомление");
                }
            }
            catch (Exception exp){
                MessageBox.Show($"{exp.Message}","Ошибка!");
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e){
            if(typeOfPaste == 1){//копировать
                string pathTo = Directory.GetCurrentDirectory() + "\\";
                //if (Directory.Exists(pathTo)){
                    File.Copy(pathFrom, pathTo);
                //}
            }
            else if(typeOfPaste == 2){//вырезать
                string pathTo = Directory.GetCurrentDirectory() + "\\";
                try{
                    if (!Directory.Exists(pathTo)){
                        Directory.Move(pathFrom, pathTo);
                    }
                    else if(!File.Exists(pathTo)){
                        File.Move(pathFrom, pathTo);
                    }
                }
                catch (Exception exp){
                    MessageBox.Show($"{exp.Message}");
                }
                finally{
                    MessageBox.Show("Файл был успешно перемещён!", "Уведомление");
                }
            }
            else{
                MessageBox.Show("Сначала нужно выбрать объект!", "Ошибка!");
            }
            pathFrom = "";
            typeOfPaste = 0;
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e){
            ListView.SelectedIndexCollection indexCollection = this.listView1.SelectedIndices;
            try{
                pathFrom = Path.Combine(Directory.GetCurrentDirectory(), listView1.Items[indexCollection[0]].Text);
                typeOfPaste = 2;
            }
            catch{
                MessageBox.Show("Сначала нужно выбрать объект!", "Ошибка!");
            }
        }

        private void newFolderToolStripMenuItem_Click(object sender, EventArgs e) {
            inputForm form = new inputForm();
            form.label1.Text = "Введите название для новой папки:";
            form.button1.Text = "Создать папку";
            form.typeOfNewFile = 1;
            form.Show();
        }

        private void newTextFileToolStripMenuItem_Click(object sender, EventArgs e){
            
            inputForm form = new inputForm();
            form.label1.Text = "Введите название для нового текстового файла:";
            form.button1.Text = "Создать текстовый файл";
            form.typeOfNewFile = 2;
            form.Show();
        }
    }
}