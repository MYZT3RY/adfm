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
    public partial class inputForm : Form{

        public int typeOfNewFile=0;
        public inputForm(){
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e){
            if (inputTextBox.Text.Length != 0) {
                string path = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), inputTextBox.Text.ToString());
                switch (typeOfNewFile){
                    case 1:{//новая папка
                            if (System.IO.Directory.Exists(path)){
                                MessageBox.Show("Каталог с данным названием уже существует!", "Ошибка");
                                inputTextBox.Text = "";
                            }
                            else{
                                System.IO.Directory.CreateDirectory(path);
                            }
                            break;
                        }
                    case 2:{//текстовый файл
                            if (System.IO.File.Exists(path)){
                                MessageBox.Show("Файл с данным названием уже существует!","Ошибка");
                                inputTextBox.Text = "";
                            }
                            else{
                                path += ".txt";
                                System.IO.File.Create(path);
                            }
                            break;
                        }
                }
                Close();
            }
            else{
                inputTextBox.Text = "";
            }
        }
    }
}
