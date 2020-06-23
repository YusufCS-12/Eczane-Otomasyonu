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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dbMethods.eczaneLogin(textBox1.Text, textBox2.Text) == true)
            {
                int eczaneID = dbMethods.eczaneIDCek(textBox1.Text, textBox2.Text);
                Form5 form5 = new Form5(eczaneID);
                form5.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Böyle bir kullanici mevcut degil veya hatali sifre!");
            }
        }
    }
}
