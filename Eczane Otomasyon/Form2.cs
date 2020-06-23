using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eczane_Otomasyon
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dbMethods.yoneticiLogin(textBox1.Text, textBox2.Text) == true)
            {
                Form4 form4 = new Form4();
                form4.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Böyle bir kullanici mevcut degil veya hatali sifre!");
            }
        }
    }
}
