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
    public partial class aboutProgram : Form{
        public aboutProgram(){
            InitializeComponent();
            label1.Text = "Another Damn File Manager - Ещё один чёртов файловый менеджер.";
            label2.Text = "Произведено ручками d1maz. - Дмитрий Якимов.\n\nДля свободного использования.\nРаспространяется по лицензии GPL-3.0";
            label3.Text = "Copyright (c) 2019-2019 d1maz.";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e){
            System.Diagnostics.Process.Start("https://d1maz.ru/adfm");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e){
            System.Diagnostics.Process.Start("https://github.com/MYZT3RY/adfm");
        }
    }
}
