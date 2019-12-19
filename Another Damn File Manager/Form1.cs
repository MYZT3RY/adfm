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

        private string pastPath;

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
            initListView(Directory.GetCurrentDirectory(), listView1, imageList1);
        }

        private void initListView(string path, ListView listView, ImageList imageList){
            listView.Clear();
            currentDirectoryTextBox.Text = path;
            foreach (var element in Directory.GetDirectories(path)){
                string dirName = Path.GetFileName(element);
                listView.Items.Add(dirName, 10);
            }
            foreach (var element in Directory.GetFiles(path)){
                string extension = Path.GetExtension(element);
                string fileName = Path.GetFileName(element);
                switch (extension){
                    case ".png": case ".ico":{
                            listView.Items.Add(fileName, 9);
                            break;
                        }
                    case ".exe":{
                            listView.Items.Add(fileName, 11);
                            break;
                        }
                    case ".zip":{
                            listView.Items.Add(fileName, 7);
                            break;
                        }
                    case ".cs": case ".c": case ".cpp": case ".pas": case ".h": case ".java": case ".js": case ".lua": case ".php": case ".py":{
                            listView.Items.Add(fileName, 6);
                            break;
                        }
                    case ".dll":{
                            listView.Items.Add(fileName, 5);
                            break;
                        }
                    case ".txt":{
                            listView.Items.Add(fileName, 4);
                            break;
                        }
                    case ".mp4": case ".avi": case ".mkv":{
                            listView.Items.Add(fileName, 3);
                            break;
                        }
                    case ".mp3": case ".flac": case ".m3u": case ".ogg":{
                            listView.Items.Add(fileName, 2);
                            break;
                        }
                    default:{
                            listView.Items.Add(fileName, 8);
                            break;
                        }

                }
            }
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e){
            path = Directory.GetCurrentDirectory() + "\\" + e.Item.Text;
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e){
            if (Directory.Exists(path)){
                Directory.SetCurrentDirectory(path);
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
                Directory.SetCurrentDirectory(tmpPath);
                initListView(tmpPath, listView1, imageList1);
            }
        }

        private void arrayLeftButton_Click(object sender, EventArgs e){
            try{
                var tmpPath = Directory.GetParent(Directory.GetCurrentDirectory());
                pastPath = Directory.GetCurrentDirectory();
                arrayRightButton.Enabled = true;
                Directory.SetCurrentDirectory(tmpPath.ToString());
                initListView(tmpPath.ToString(), listView1, imageList1);
            }
            catch(NullReferenceException){
                MessageBox.Show("Недопустимая директория!");
            }
            catch (Exception exp){
                MessageBox.Show($"{exp}");
            }
        }

        private void refreshButton_Click(object sender, EventArgs e){
            initListView(Directory.GetCurrentDirectory(), listView1, imageList1);
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
                if (Directory.Exists(pathTo)){
                    Directory.Move(pathFrom, pathTo);
                }
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
                    MessageBox.Show($"{exp.Message}","Ошибка!");
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

        private void renameToolStripMenuItem_Click(object sender, EventArgs e){
            ListView.SelectedIndexCollection indexCollection = this.listView1.SelectedIndices;
            if(indexCollection.Count != 0){
                string tmpPath = Path.Combine(Directory.GetCurrentDirectory(), listView1.Items[indexCollection[0]].Text);
                inputForm form = new inputForm();
                if (Directory.Exists(tmpPath)){
                    form.label1.Text = "Введите новое название для папки:";
                    form.typeOfNewFile = 3;
                }
                else if(File.Exists(tmpPath)){
                    form.label1.Text = "Введите новое название для файла:";
                    form.typeOfNewFile = 4;
                }
                form.oldPath = tmpPath;
                form.button1.Text = "Переименовать";
                form.Show();
            }
            else{
                MessageBox.Show("Сначала нужно выбрать объект!","Ошибка!");
            }
        }

        private void arrayRightButton_Click(object sender, EventArgs e){
            if (pastPath.Length != 0){
                if (Directory.Exists(pastPath)){
                    Directory.SetCurrentDirectory(pastPath.ToString());
                    initListView(pastPath.ToString(), listView1, imageList1);
                    pastPath = "";
                    arrayRightButton.Enabled = false;
                }
            }
        }
    }
}