using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eczane_Otomasyon
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        static OleDbConnection baglantiNesnesiv2 = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\Baran\Desktop\Eczane Otomasyon\eczane.mdb");

        private void Form4_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(label10.Text))
            {
                button11.Visible = true;
            }
            else
            {
                button11.Visible = false;
            }

            if (!string.IsNullOrEmpty(label17.Text))
            {
                button12.Visible = true;
                button13.Visible = true;
            }
            else
            {
                button12.Visible = false;
                button13.Visible = false;
            }

            comboBox10.DataSource = dbMethods.receteTuruDoldur();
            comboBox10.ValueMember = "receteTuruID";
            comboBox10.DisplayMember = "receteTuru";

            label2.Text = null;
            dataGridView1.DataSource = dbMethods.sehirDoldur();
            dataGridView5.DataSource = dbMethods.ilacDoldur();

            comboBox2.DataSource = dbMethods.sehirDoldur();
            comboBox2.ValueMember = "sehirID";
            comboBox2.DisplayMember = "sehir";

            comboBox4.DataSource = dbMethods.sehirDoldur();
            comboBox4.ValueMember = "sehirID";
            comboBox4.DisplayMember = "sehir";

            comboBox3.DataSource = dbMethods.ilceDoldur(Convert.ToInt32(comboBox4.SelectedValue));
            comboBox3.ValueMember = "ilceID";
            comboBox3.DisplayMember = "ilce";

            comboBox1.DataSource = dbMethods.sehirDoldur();
            comboBox1.ValueMember = "sehirID";
            comboBox1.DisplayMember = "sehir";

            dataGridView4.DataSource = dbMethods.hastaDoldur();

            dataGridView3.DataSource = dbMethods.eczaneDoldur(Convert.ToInt32(comboBox3.SelectedValue));

            if (comboBox3.SelectedIndex == -1)
            {
                textBox3.Visible = false;
                button3.Visible = false;
                comboBox3.Visible = false;
            }
            else
            {
                textBox3.Visible = true;
                button3.Visible = true;
                comboBox3.Visible = true;
            }
            if (dbMethods.sehirDoldur().Rows.Count != 0)
                comboBox2.SelectedIndex = 0;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                OleDbCommand komutNesnesi = new OleDbCommand("INSERT INTO sehirler (sehir) VALUES(@sehirler)", baglantiNesnesiv2);
                komutNesnesi.Parameters.AddWithValue("@sehirler", textBox1.Text);
                baglantiNesnesiv2.Open();
                komutNesnesi.ExecuteNonQuery();
                baglantiNesnesiv2.Close();

                textBox1.Text = null;
                dataGridView1.DataSource = dbMethods.sehirDoldur();
            }
            else
            {
                MessageBox.Show("Eksik kısımları doldurunuz.");
            }
            Update();
        }

        private void Update()
        {
            comboBox10.DataSource = dbMethods.receteTuruDoldur();
            comboBox10.ValueMember = "receteTuruID";
            comboBox10.DisplayMember = "receteTuru";

            label2.Text = null;
            dataGridView1.DataSource = dbMethods.sehirDoldur();
            dataGridView5.DataSource = dbMethods.ilacDoldur();

            comboBox2.DataSource = dbMethods.sehirDoldur();
            comboBox2.ValueMember = "sehirID";
            comboBox2.DisplayMember = "sehir";

            comboBox4.DataSource = dbMethods.sehirDoldur();
            comboBox4.ValueMember = "sehirID";
            comboBox4.DisplayMember = "sehir";

            comboBox3.DataSource = dbMethods.ilceDoldur(Convert.ToInt32(comboBox4.SelectedValue));
            comboBox3.ValueMember = "ilceID";
            comboBox3.DisplayMember = "ilce";

            comboBox1.DataSource = dbMethods.sehirDoldur();
            comboBox1.ValueMember = "sehirID";
            comboBox1.DisplayMember = "sehir";

            dataGridView4.DataSource = dbMethods.hastaDoldur();

            dataGridView3.DataSource = dbMethods.eczaneDoldur(Convert.ToInt32(comboBox3.SelectedValue));
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1) return;

            label2.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
        }

       

        private void tabPage1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dbMethods.sehirDoldur();

            comboBox2.DataSource = dbMethods.sehirDoldur();
            comboBox2.ValueMember = "sehirID";
            comboBox2.DisplayMember = "sehir";
            tabPage1.Refresh();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox4.Text))
            {
                OleDbCommand komutNesnesi = new OleDbCommand("UPDATE sehirler SET sehir=@sehir WHERE sehirID=@sehirID", baglantiNesnesiv2);
                komutNesnesi.Parameters.AddWithValue("@sehir", textBox4.Text);
                komutNesnesi.Parameters.AddWithValue("@sehirID", label2.Text);
                baglantiNesnesiv2.Open();
                komutNesnesi.ExecuteNonQuery();
                baglantiNesnesiv2.Close();

                label2.Text = null;
                textBox4.Text = null;
                dataGridView1.DataSource = dbMethods.sehirDoldur();
                comboBox2.DataSource = dbMethods.sehirDoldur();
                comboBox2.ValueMember = "sehirID";
                comboBox2.DisplayMember = "sehir";
            }
            else
            {
                MessageBox.Show("Eksik kısımları doldurunuz.");
            }
            Update();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OleDbCommand komutNesnesi = new OleDbCommand("delete from sehirler where sehir=@sehir and sehirID=@sehirID", baglantiNesnesiv2);
            komutNesnesi.Parameters.AddWithValue("@sehir", textBox4.Text);
            komutNesnesi.Parameters.AddWithValue("@sehirID", label2.Text);
            baglantiNesnesiv2.Open();
            komutNesnesi.ExecuteNonQuery();
            baglantiNesnesiv2.Close();

            label2.Text = null;
            textBox4.Text = null;
            dataGridView1.DataSource = dbMethods.sehirDoldur();

            comboBox2.DataSource = dbMethods.sehirDoldur();
            comboBox2.ValueMember = "sehirID";
            comboBox2.DisplayMember = "sehir";

            Update();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                OleDbCommand komutNesnesi = new OleDbCommand("INSERT INTO ilceler (ilce,sehirID) VALUES(@ilce,@sehirID)", baglantiNesnesiv2);
                komutNesnesi.Parameters.AddWithValue("@ilce", textBox2.Text);
                komutNesnesi.Parameters.AddWithValue("@sehirID", Convert.ToInt32(comboBox2.SelectedValue));
                baglantiNesnesiv2.Open();
                komutNesnesi.ExecuteNonQuery();
                baglantiNesnesiv2.Close();

                label3.Text = null;
                textBox5.Text = null;
                dataGridView2.DataSource = dbMethods.ilceDoldur(Convert.ToInt32(comboBox2.SelectedValue));
            }
            else
            {
                MessageBox.Show("Eksik kısımları doldurunuz.");
            }

            Update();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox5.Text))
            {
                OleDbCommand komutNesnesi = new OleDbCommand("UPDATE ilceler SET ilce=@ilce WHERE ilceID=@ilceID", baglantiNesnesiv2);
                komutNesnesi.Parameters.AddWithValue("@ilce", textBox5.Text);
                komutNesnesi.Parameters.AddWithValue("@ilceID", OleDbType.Decimal).Value=label3.Text;
                baglantiNesnesiv2.Open();
                komutNesnesi.ExecuteNonQuery();
                baglantiNesnesiv2.Close();

                label3.Text = null;
                textBox5.Text = null;
                dataGridView2.DataSource = dbMethods.ilceDoldur(Convert.ToInt32(comboBox2.SelectedValue));
            }
            else
            {
                MessageBox.Show("Eksik kısımları doldurunuz.");
            }

            Update();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OleDbCommand komutNesnesi = new OleDbCommand("delete from ilceler where ilceID=@ilceID", baglantiNesnesiv2);
            komutNesnesi.Parameters.AddWithValue("@ilceID", label3.Text);
            baglantiNesnesiv2.Open();
            komutNesnesi.ExecuteNonQuery();
            baglantiNesnesiv2.Close();

            label3.Text = null;
            textBox5.Text = null;
            dataGridView2.DataSource = dbMethods.ilceDoldur(Convert.ToInt32(comboBox2.SelectedValue));

            Update();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            tabPage2.Refresh();
            dataGridView2.DataSource = dbMethods.ilceDoldur(Convert.ToInt32(comboBox2.SelectedValue));

            comboBox2.DataSource = dbMethods.sehirDoldur();
            comboBox2.ValueMember = "sehirID";
            comboBox2.DisplayMember = "sehir";

            comboBox1.DataSource = dbMethods.sehirDoldur();
            comboBox1.ValueMember = "sehirID";
            comboBox1.DisplayMember = "sehir";
        }

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1) return;

            label3.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
            textBox5.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
            comboBox1.SelectedValue = dataGridView2.SelectedRows[0].Cells[2].Value;
        }

        static int a = 0;
        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {

            a++;
            if (a > 1)
            {
                dataGridView2.DataSource = dbMethods.ilceDoldur(Convert.ToInt32(comboBox2.SelectedValue));
            }
        }

        static int b = 0;
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            b++;
            if (b > 1)
            {
                comboBox3.DataSource = dbMethods.ilceDoldur(Convert.ToInt32(comboBox4.SelectedValue));
                dataGridView3.DataSource = dbMethods.eczaneDoldur(Convert.ToInt32(comboBox3.SelectedValue));
                comboBox3.ValueMember = "ilceID";
                comboBox3.DisplayMember = "ilce";
                if (comboBox3.SelectedIndex == -1)
                {
                    textBox3.Visible = false;
                    button3.Visible = false;
                    comboBox3.Visible = false;
                }
                else
                {
                    textBox3.Visible = true;
                    button3.Visible = true;
                    comboBox3.Visible = true;
                }
            }
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {
            dataGridView3.DataSource = dbMethods.eczaneDoldur(Convert.ToInt32(comboBox3.SelectedValue));
            tabPage3.Refresh();
            if (comboBox3.SelectedIndex == -1)
            {
                textBox3.Visible = false;
                button3.Visible = false;
                comboBox3.Visible = false;
            }
            else
            {
                textBox3.Visible = true;
                button3.Visible = true;
                comboBox3.Visible = true;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabPage1.Refresh();
            tabPage2.Refresh();
            tabPage3.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox3.Text))
            {
                OleDbCommand komutNesnesi = new OleDbCommand("INSERT INTO eczaneler (eczane,ilceID,kullaniciAdi,sifre) VALUES(@eczane,@ilceID,@kullaniciAdi,@sifre)", baglantiNesnesiv2);
                komutNesnesi.Parameters.AddWithValue("@eczane", textBox3.Text);
                komutNesnesi.Parameters.AddWithValue("@ilceID", Convert.ToInt32(comboBox3.SelectedValue));
                komutNesnesi.Parameters.AddWithValue("@kullaniciAdi", textBox3.Text);
                komutNesnesi.Parameters.AddWithValue("@sifre", textBox3.Text + "123");
                baglantiNesnesiv2.Open();
                komutNesnesi.ExecuteNonQuery();
                baglantiNesnesiv2.Close();

                textBox3.Text = null;
                dataGridView3.DataSource = dbMethods.eczaneDoldur(Convert.ToInt32(comboBox3.SelectedValue));
                comboBox3.DataSource = dbMethods.ilceDoldur(Convert.ToInt32(comboBox4.SelectedValue));
                comboBox3.ValueMember = "ilceID";
                comboBox3.DisplayMember = "ilce";
            }
            else
            {
                MessageBox.Show("Eksik kısımları doldurunuz.");
            }

            Update();
        }

        static int c = 0;
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            c++;
            if (c > 1)
            {
                dataGridView3.DataSource = dbMethods.eczaneDoldur(Convert.ToInt32(comboBox3.SelectedValue));
            }
        }

        private void dataGridView3_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1) return;

            comboBox5.DataSource = dbMethods.sehirDoldur();
            comboBox5.ValueMember = "sehirID";
            comboBox5.DisplayMember = "sehir";

            comboBox6.DataSource = dbMethods.ilceDoldur(Convert.ToInt32(comboBox5.SelectedValue));
            comboBox6.ValueMember = "ilceID";
            comboBox6.DisplayMember = "ilce";

            label9.Text = dataGridView3.SelectedRows[0].Cells[0].Value.ToString();
            textBox6.Text = dataGridView3.SelectedRows[0].Cells[1].Value.ToString();
            comboBox5.SelectedValue = dataGridView3.SelectedRows[0].Cells[4].Value;
            comboBox6.SelectedValue = dataGridView3.SelectedRows[0].Cells[2].Value;
        }

        static int d = 0;
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            d++;
            if (d > 2)
            {
                comboBox6.DataSource = dbMethods.ilceDoldur(Convert.ToInt32(comboBox5.SelectedValue));
                comboBox6.ValueMember = "ilceID";
                comboBox6.DisplayMember = "ilce";

                if (dbMethods.ilceDoldur(Convert.ToInt32(comboBox5.SelectedValue)).Rows.Count < 1)
                {
                    button9.Visible = false;
                    button8.Visible = false;
                }
                else
                {
                    button8.Visible = true;
                    button9.Visible = true;
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            OleDbCommand komutNesnesi = new OleDbCommand("delete from eczaneler where eczaneID=@eczaneID", baglantiNesnesiv2);
            komutNesnesi.Parameters.AddWithValue("@eczaneID", label9.Text);
            baglantiNesnesiv2.Open();
            komutNesnesi.ExecuteNonQuery();
            baglantiNesnesiv2.Close();

            label9.Text = null;
            textBox6.Text = null;
            comboBox5.SelectedValue = -1;
            dataGridView3.DataSource = dbMethods.eczaneDoldur(Convert.ToInt32(comboBox3.SelectedValue));

            Update();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox6.Text))
            {
                OleDbCommand komutNesnesi = new OleDbCommand("UPDATE eczaneler SET eczane='"+ textBox6.Text + "',kullaniciAdi='"+textBox6.Text+"',sifre='"+textBox6.Text+"123"+"',ilceID='"+Convert.ToDecimal(comboBox6.SelectedValue)+"' WHERE eczaneID=@eczaneID", baglantiNesnesiv2);
                komutNesnesi.Parameters.AddWithValue("@eczaneID", OleDbType.Decimal).Value = label9.Text;
                baglantiNesnesiv2.Open();
                komutNesnesi.ExecuteNonQuery();
                baglantiNesnesiv2.Close();

                label9.Text = null;
                textBox6.Text = null;
                comboBox5.SelectedValue = -1;
                dataGridView3.DataSource = dbMethods.eczaneDoldur(Convert.ToInt32(comboBox3.SelectedValue));
            }
            else
            {
                MessageBox.Show("Eksik kısımları doldurunuz.");
            }

            Update();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            OleDbCommand komutNesnesi = new OleDbCommand("INSERT INTO hastalar (TC,adi,soyadi,sigortaID) VALUES(?,?,?,?)", baglantiNesnesiv2);
            komutNesnesi.Parameters.AddWithValue("@TC", rakamUret(11));
            komutNesnesi.Parameters.AddWithValue("@adi", kelimeUret(6));
            komutNesnesi.Parameters.AddWithValue("@soyadi", kelimeUret2(6));
            Random random = new Random();
            int a = random.Next(1, 4);
            komutNesnesi.Parameters.AddWithValue("@sigortaID", a);
            baglantiNesnesiv2.Open();
            komutNesnesi.ExecuteNonQuery();
            baglantiNesnesiv2.Close();

            dataGridView4.DataSource = dbMethods.hastaDoldur();
            if (!string.IsNullOrEmpty(label10.Text))
            {
                button11.Visible = true;
            }
            else
            {
                button11.Visible = false;
            }

            Update();
        }

        private string kelimeUret(int _length)
        {
            int length = _length;

            // creating a StringBuilder object()
            StringBuilder str_build = new StringBuilder();
            Random random = new Random();

            char letter;

            for (int i = 0; i < length; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);
            }
            return str_build.ToString();
        }

        private string kelimeUret2(int _length)
        {
            int length = _length;

            // creating a StringBuilder object()
            StringBuilder str_build = new StringBuilder();
            Random random2 = new Random();

            char letter;

            for (int i = 0; i < length; i++)
            {
                double flt = random2.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);
            }
            return str_build.ToString();
        }

        private string rakamUret(int _length)
        {
            int length = _length;

            // creating a StringBuilder object()
            StringBuilder str_build = new StringBuilder();
            Random random = new Random();


            for (int i = 0; i < length; i++)
            {
                int number = random.Next(0, 9);
                str_build.Append(number);
            }
            return str_build.ToString();
        }

        private void dataGridView4_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1) return;

            label11.Text = dataGridView4.SelectedRows[0].Cells[0].Value.ToString();
            label10.Text = dataGridView4.SelectedRows[0].Cells[1].Value.ToString() + " " + dataGridView4.SelectedRows[0].Cells[2].Value.ToString() + " " + dataGridView4.SelectedRows[0].Cells[3].Value.ToString();
            if (!string.IsNullOrEmpty(label10.Text))
            {
                button11.Visible = true;
                comboBox8.DataSource = dbMethods.ilacDoldur();
                comboBox8.ValueMember = "ilacID";
                comboBox8.DisplayMember = "ilac";

                comboBox9.SelectedIndex = 0;
            }
            else
            {
                button11.Visible = false;
                comboBox8.Visible = false;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(label10.Text))
            {
                button11.Visible = true;
                comboBox8.DataSource = dbMethods.ilacDoldur();
                comboBox8.ValueMember = "ilacID";
                comboBox8.DisplayMember = "ilac";
            }
            else
            {
                button11.Visible = false;
                comboBox8.Visible = false;
            }

            OleDbCommand komutNesnesi = new OleDbCommand("INSERT INTO receteler (receteKodu,receteTarihi,hastaID,ilacID,adet,sonuc,durum,eczaneID) VALUES(@receteKodu,@receteTarihi,@hastaID,@ilacID,@adet,@sonuc,@durum,@eczaneID)", baglantiNesnesiv2);

            komutNesnesi.Parameters.AddWithValue("@receteKodu", kelimeUret(6));
            komutNesnesi.Parameters.AddWithValue("@receteTarihi", dateTimePicker1.Value.ToShortDateString());
            komutNesnesi.Parameters.AddWithValue("@hastaID", Convert.ToInt32(label11.Text));
            komutNesnesi.Parameters.AddWithValue("@ilacID", comboBox8.SelectedValue);
            komutNesnesi.Parameters.AddWithValue("@adet", Convert.ToInt32(comboBox9.SelectedItem.ToString()));
            komutNesnesi.Parameters.AddWithValue("@sonuc", textBox7.Text);
            komutNesnesi.Parameters.AddWithValue("@durum", 0);
            komutNesnesi.Parameters.AddWithValue("@eczaneID", 0);

            baglantiNesnesiv2.Open();
            komutNesnesi.ExecuteNonQuery();
            baglantiNesnesiv2.Close();

            label10.Text = null;
            label11.Text = null;
            dataGridView6.DataSource = dbMethods.receteDoldur();

            Update();
        }

        private void dataGridView5_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            comboBox11.DataSource = dbMethods.receteTuruDoldur();
            comboBox11.ValueMember = "receteTuruID";
            comboBox11.DisplayMember = "receteTuru";

            label17.Text = dataGridView5.SelectedRows[0].Cells[0].Value.ToString();
            textBox8.Text = dataGridView5.SelectedRows[0].Cells[1].Value.ToString();
            textBox11.Text = dataGridView5.SelectedRows[0].Cells[2].Value.ToString();
            comboBox11.SelectedValue = Convert.ToInt32(dataGridView5.SelectedRows[0].Cells[3].Value.ToString());
            if (!string.IsNullOrEmpty(label17.Text))
            {
                button12.Visible = true;
                button13.Visible = true;
            }
            else
            {
                button12.Visible = false;
                button13.Visible = false;
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox9.Text) && !string.IsNullOrEmpty(textBox10.Text))
            {
                OleDbCommand komutNesnesi = new OleDbCommand("INSERT INTO ilaclar (ilac,fiyat,receteTuruID) VALUES(@ilac,@fiyat,@receteTuruID)", baglantiNesnesiv2);
                komutNesnesi.Parameters.AddWithValue("@ilac", textBox9.Text);
                komutNesnesi.Parameters.AddWithValue("@fiyat", Convert.ToDouble(textBox10.Text));
                komutNesnesi.Parameters.AddWithValue("@receteTuruID", Convert.ToInt32(comboBox10.SelectedValue));
                baglantiNesnesiv2.Open();
                komutNesnesi.ExecuteNonQuery();
                baglantiNesnesiv2.Close();

                label17.Text = null;
                textBox9.Text = null;
                textBox10.Text = null;
                dataGridView5.DataSource = dbMethods.ilacDoldur();
                if (!string.IsNullOrEmpty(label17.Text))
                {
                    button12.Visible = true;
                    button13.Visible = true;
                }
                else
                {
                    button12.Visible = false;
                    button13.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("Eksik kısımları doldurunuz.");
            }
            Update();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox11.Text) && !string.IsNullOrEmpty(textBox8.Text))
            {
                OleDbCommand komutNesnesi = new OleDbCommand("UPDATE ilaclar SET ilac='"+textBox8.Text+"',fiyat='"+textBox11.Text+"',receteTuruID='"+Convert.ToDecimal(comboBox11.SelectedValue)+"' WHERE ilacID=@ilacID", baglantiNesnesiv2);
                komutNesnesi.Parameters.AddWithValue("@ilacID", OleDbType.Decimal).Value=Convert.ToDecimal(label17.Text);
                baglantiNesnesiv2.Open();
                komutNesnesi.ExecuteNonQuery();
                baglantiNesnesiv2.Close();

                label17.Text = null;
                textBox9.Text = null;
                textBox10.Text = null;
                textBox8.Text = null;
                textBox11.Text = null;
                dataGridView5.DataSource = dbMethods.ilacDoldur();
                if (!string.IsNullOrEmpty(label17.Text))
                {
                    button12.Visible = true;
                    button13.Visible = true;
                }
                else
                {
                    button12.Visible = false;
                    button13.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("Eksik kısımları doldurunuz");
            }
            Update();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            OleDbCommand komutNesnesi = new OleDbCommand("delete from ilaclar WHERE ilacID=@ilacID", baglantiNesnesiv2);
            komutNesnesi.Parameters.AddWithValue("@ilacID", Convert.ToInt32(label17.Text));
            baglantiNesnesiv2.Open();
            komutNesnesi.ExecuteNonQuery();
            baglantiNesnesiv2.Close();

            label17.Text = null;
            textBox9.Text = null;
            textBox10.Text = null;
            textBox8.Text = null;
            textBox11.Text = null;
            dataGridView5.DataSource = dbMethods.ilacDoldur();
            if (!string.IsNullOrEmpty(label17.Text))
            {
                button12.Visible = true;
                button13.Visible = true;
            }
            else
            {
                button12.Visible = false;
                button13.Visible = false;
            }
            Update();
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ',';
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ',';

        }
    }
}
