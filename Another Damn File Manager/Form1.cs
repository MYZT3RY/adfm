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
        public Form1(){
            InitializeComponent();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e){
            Close();
        }

        private void aboutProgramToolStripMenuItem_Click(object sender, EventArgs e){
            MessageBox.Show("Another Damn File Manager - Ещё один чёртов файловый менеджер.\n\nПроизведено ручками d1maz. - Дмитрий Якимов.\nДля свободного использования.\n\nРаспространяется по лицензии Apache 2.0\n\nd1maz.ru/adfm\ngithub.com/MYZT3RY/adfm\n\n2019 - 2019", "О прорамме");
        }
    }
}
