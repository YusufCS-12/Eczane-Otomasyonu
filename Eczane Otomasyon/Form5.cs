using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eczane_Otomasyon
{
    public partial class Form5 : Form
    {
        int _eczaneID = 0;
        public Form5(int eczaneID)
        {
            _eczaneID = eczaneID;
            InitializeComponent();
        }


        static OleDbConnection bg2 = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\Baran\Desktop\Eczane Otomasyon\eczane.mdb");

        static OleDbConnection bg3 = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\Baran\Desktop\Eczane Otomasyon\eczane.mdb");

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            double fiyat = Convert.ToDouble(dataGridView1.SelectedRows[0].Cells[6].Value.ToString());
            int adet = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[7].Value.ToString());
            double sigortaOran = Convert.ToDouble(dataGridView1.SelectedRows[0].Cells[8].Value);
            double toplam = adet * fiyat;
            label8.Text = toplam.ToString() + " TL";
            label7.Text = (toplam * sigortaOran).ToString() + " TL";
            label6.Text = ((1 - sigortaOran) * toplam).ToString() + " TL";
            if (!string.IsNullOrEmpty(label8.Text))
            {
                button1.Visible = true;
            }
            else
            {
                button1.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label8.Text = null;
            label7.Text = null;
            label6.Text = null;
            button1.Visible = false;

            OleDbCommand komutNesnesi = new OleDbCommand("UPDATE receteler SET durum=1,eczaneID=? WHERE receteID=?", bg2);
            komutNesnesi.Parameters.AddWithValue("eczaneID",_eczaneID);
            komutNesnesi.Parameters.AddWithValue("receteID", Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
            bg2.Open();
            komutNesnesi.ExecuteNonQuery();
            bg2.Close();

            dataGridView1.DataSource = dbMethods.receteDoldur2(textBox1.Text, textBox2.Text);
            dataGridView2.DataSource = dbMethods.receteDoldur3(textBox3.Text, textBox4.Text);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            label16.Text = null;
            label15.Text = null;
            button2.Visible = false;

            OleDbCommand komutNesnesi = new OleDbCommand("UPDATE receteler SET durum=0,eczaneID=0 WHERE receteID=?", bg2);
            komutNesnesi.Parameters.AddWithValue("receteID", Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[0].Value));
            bg2.Open();
            komutNesnesi.ExecuteNonQuery();
            bg2.Close();


            dataGridView1.DataSource = dbMethods.receteDoldur2(textBox1.Text, textBox2.Text);
            dataGridView2.DataSource = dbMethods.receteDoldur3(textBox3.Text, textBox4.Text);

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            double fiyat = Convert.ToDouble(dataGridView2.SelectedRows[0].Cells[6].Value.ToString());
            int adet = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[7].Value.ToString());
            double sigortaOran = Convert.ToDouble(dataGridView2.SelectedRows[0].Cells[8].Value);
            double toplam = adet * fiyat;
            label16.Text = (toplam * sigortaOran).ToString() + " TL";
            label15.Text = ((1 - sigortaOran) * toplam).ToString() + " TL";
            if (!string.IsNullOrEmpty(label16.Text))
            {
                button2.Visible = true;
            }
            else
            {
                button2.Visible = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OleDbCommand komutNesnesi = new OleDbCommand("UPDATE eczaneler SET kullaniciAdi=?,sifre=? WHERE eczaneID=?", bg2);
            komutNesnesi.Parameters.AddWithValue("kullaniciAdi", textBox7.Text);
            komutNesnesi.Parameters.AddWithValue("sifre", textBox5.Text);
            komutNesnesi.Parameters.AddWithValue("eczaneID", _eczaneID);
            bg2.Open();
            komutNesnesi.ExecuteNonQuery();
            bg2.Close();
            Form3 form3 = new Form3();
            MessageBox.Show("Yeniden giriş yapınız.");
            this.Close();
            form3.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OleDbCommand komutNesnesi = new OleDbCommand("INSERT INTO siparisler (eczaneID,ilacID,adet,depoID) VALUES(@eczaneID,@ilacID,@adet,@depoID)", bg2);
            komutNesnesi.Parameters.AddWithValue("@eczaneID", _eczaneID);
            komutNesnesi.Parameters.AddWithValue("@ilacID", Convert.ToInt32(comboBox2.SelectedValue));
            komutNesnesi.Parameters.AddWithValue("@adet", Convert.ToInt32(comboBox3.SelectedItem.ToString()));
            komutNesnesi.Parameters.AddWithValue("@depoID", Convert.ToInt32(comboBox1.SelectedValue));
            bg2.Open();
            komutNesnesi.ExecuteNonQuery();
            bg2.Close();
            dataGridView5.DataSource = dbMethods.siparisDoldur();

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
        }

        private void dataGridView5_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            panel1.Visible = true;
            button7.Visible = true;
            label27.Text = dataGridView5.SelectedRows[0].Cells[0].Value.ToString();
            comboBox4.SelectedValue = Convert.ToInt32(dataGridView5.SelectedRows[0].Cells[4].Value);
            comboBox5.SelectedValue = Convert.ToInt32(dataGridView5.SelectedRows[0].Cells[2].Value);
            comboBox6.SelectedIndex = Convert.ToInt32(dataGridView5.SelectedRows[0].Cells[3].Value) - 1;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            button7.Visible = false;

            OleDbCommand komutNesnesi = new OleDbCommand("UPDATE siparisler SET ilacID='"+ Convert.ToInt32(comboBox5.SelectedValue) + "',adet='"+ Convert.ToInt32(comboBox6.SelectedItem.ToString()) + "',depoID='"+ Convert.ToInt32(comboBox4.SelectedValue) + "' WHERE eczaneID=?", bg2);
            komutNesnesi.Parameters.AddWithValue("@eczaneID", OleDbType.Decimal).Value = _eczaneID;
            bg2.Open();
            komutNesnesi.ExecuteNonQuery();
            bg2.Close();
            dataGridView5.DataSource = dbMethods.siparisDoldur();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            button7.Visible = false;
            OleDbCommand komutNesnesi = new OleDbCommand("delete from siparisler WHERE siparisID=?", bg2);
            komutNesnesi.Parameters.AddWithValue("@siparisID", OleDbType.Decimal).Value = label27.Text;
            bg2.Open();
            komutNesnesi.ExecuteNonQuery();
            bg2.Close();

            label27.Text = null;
            dataGridView5.DataSource = dbMethods.siparisDoldur();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dbMethods.receteDoldur2(textBox1.Text, textBox2.Text);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dbMethods.receteDoldur2(textBox1.Text, textBox2.Text);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            dataGridView2.DataSource = dbMethods.receteDoldur3(textBox4.Text, textBox3.Text);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            dataGridView2.DataSource = dbMethods.receteDoldur3(textBox4.Text, textBox3.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox8.Text) && textBox8.TextLength == 11)
            {
                bg2.Close();
                OleDbCommand komutNesnesi = new OleDbCommand("select receteler.receteTarihi from ((receteler inner join hastalar on hastalar.hastaID=receteler.hastaID) inner join ilaclar on ilaclar.ilacID=receteler.ilacID) where hastalar.TC=? and ilaclar.ilacID=? and ilaclar.receteTuruID NOT IN (1)", bg2);
                komutNesnesi.Parameters.AddWithValue("hastalar.TC", textBox8.Text);
                komutNesnesi.Parameters.AddWithValue("ilaclar.ilacID", Convert.ToInt32(comboBox8.SelectedValue));

                bg2.Open();
                OleDbDataReader reader = komutNesnesi.ExecuteReader();


                Boolean a = false;
                Boolean z = false;

                Boolean rec2 = false;
                Boolean rec = false;


                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (DateTime.Now.Year > Convert.ToDateTime(reader[0]).Year)
                        {
                            if (a == false)
                            {
                                a = true;
                            }
                        }
                        else if (DateTime.Now.Year == Convert.ToDateTime(reader[0]).Year)
                        {
                            if (DateTime.Now.Month > Convert.ToDateTime(reader[0]).Month + 1)
                            {
                                if (a == false)
                                {
                                    a = true;
                                }
                            }
                            else if (DateTime.Now.Month == Convert.ToDateTime(reader[0]).Month + 1 && DateTime.Now.Day + 30 > Convert.ToDateTime(reader[0]).Day + 15)
                            {
                                if (a == false)
                                {
                                    a = true;
                                }
                            }
                            else if (DateTime.Now.Month == Convert.ToDateTime(reader[0]).Month && DateTime.Now.Day > Convert.ToDateTime(reader[0]).Day + 15)
                            {
                                if (a == false)
                                {
                                    a = true;
                                }
                            }
                            else
                            {
                                rec2 = true;
                            }
                        }
                        else
                        {
                            rec2 = true;
                        }
                    }
                    bg2.Close();
                    OleDbCommand komutNesnesi4 = new OleDbCommand("select tarih from recetesizler inner join ilaclar on ilaclar.ilacID=recetesizler.ilacID where TC=? and ilaclar.ilacID=? and ilaclar.receteTuruID NOT IN (1)", bg2);
                    komutNesnesi4.Parameters.AddWithValue("@TC", textBox8.Text);
                    komutNesnesi4.Parameters.AddWithValue("ilaclar.ilacID", Convert.ToInt32(comboBox8.SelectedValue));

                    bg2.Open();
                    OleDbDataReader reader3 = komutNesnesi4.ExecuteReader();



                    if (reader3.HasRows)
                    {

                        while (reader3.Read())
                        {

                            if (DateTime.Now.Year > Convert.ToDateTime(reader3[0]).Year)
                            {
                                if (z == false)
                                {
                                    z = true;
                                }
                            }
                            else if (DateTime.Now.Year == Convert.ToDateTime(reader3[0]).Year)
                            {
                                if (DateTime.Now.Month > Convert.ToDateTime(reader3[0]).Month + 1)
                                {
                                    if (z == false)
                                    {
                                        z = true;
                                    }
                                }
                                else if (DateTime.Now.Month == Convert.ToDateTime(reader3[0]).Month + 1 && DateTime.Now.Day + 30 > Convert.ToDateTime(reader3[0]).Day + 15)
                                {
                                    if (z == false)
                                    {
                                        z = true;
                                    }
                                }
                                else if (DateTime.Now.Month == Convert.ToDateTime(reader3[0]).Month && DateTime.Now.Day > Convert.ToDateTime(reader3[0]).Day + 15)
                                {
                                    if (z == false)
                                    {
                                        z = true;
                                    }
                                }
                                else
                                {
                                    rec = true;
                                }
                            }
                            else
                            {
                                rec = true;
                            }
                        }
                        bg2.Close();
                    }
                }
                else
                {
                    bg2.Close();
                    OleDbCommand komutNesnesi4 = new OleDbCommand("select tarih from recetesizler inner join ilaclar on ilaclar.ilacID=recetesizler.ilacID where TC=? and ilaclar.ilacID=? and ilaclar.receteTuruID NOT IN (1)", bg2);
                    komutNesnesi4.Parameters.AddWithValue("TC", textBox8.Text);
                    komutNesnesi4.Parameters.AddWithValue("ilaclar.ilacID", Convert.ToInt32(comboBox8.SelectedValue));


                    bg2.Open();
                    OleDbDataReader reader3 = komutNesnesi4.ExecuteReader();



                    if (reader3.HasRows)
                    {

                        while (reader3.Read())
                        {

                            if (DateTime.Now.Year > Convert.ToDateTime(reader3[0]).Year)
                            {
                                if (z == false)
                                {
                                    z = true;
                                }
                            }
                            else if (DateTime.Now.Year == Convert.ToDateTime(reader3[0]).Year)
                            {
                                if (DateTime.Now.Month > Convert.ToDateTime(reader3[0]).Month + 1)
                                {
                                    if (z == false)
                                    {
                                        z = true;
                                    }
                                }
                                else if (DateTime.Now.Month == Convert.ToDateTime(reader3[0]).Month + 1 && DateTime.Now.Day + 30 > Convert.ToDateTime(reader3[0]).Day + 15)
                                {
                                    if (z == false)
                                    {
                                        z = true;
                                    }
                                }
                                else if (DateTime.Now.Month == Convert.ToDateTime(reader3[0]).Month && DateTime.Now.Day > Convert.ToDateTime(reader3[0]).Day + 15)
                                {
                                    if (z == false)
                                    {
                                        z = true;
                                    }
                                }
                                else
                                {
                                    rec = true;
                                }
                            }
                        }
                        bg2.Close();
                    }
                }

                if (rec == false && rec2 == false)
                {
                    if (Convert.ToInt32(comboBox7.SelectedItem.ToString()) < 2)
                    {
                        OleDbCommand komutNesnesi2 = new OleDbCommand("INSERT INTO recetesizler (TC,ilacID,adet,tarih,eczaneID) VALUES(@TC,@ilacID,@adet,@tarih,@eczaneID)", bg3);
                        komutNesnesi2.Parameters.AddWithValue("@TC", textBox8.Text);
                        komutNesnesi2.Parameters.AddWithValue("@ilacID", Convert.ToInt32(comboBox8.SelectedValue));
                        komutNesnesi2.Parameters.AddWithValue("@adet", Convert.ToInt32(comboBox7.SelectedItem.ToString()));
                        komutNesnesi2.Parameters.AddWithValue("@tarih", DateTime.Now.ToShortDateString());

                        komutNesnesi2.Parameters.AddWithValue("@eczaneID", _eczaneID);
                        bg3.Open();
                        komutNesnesi2.ExecuteNonQuery();
                        bg3.Close();
                        dataGridView3.DataSource = dbMethods.recetesizlerDoldur(textBox6.Text);

                        OleDbCommand komutNesnesi3 = new OleDbCommand("select * from ilaclar where ilacID=?", bg3);
                        komutNesnesi3.Parameters.AddWithValue("ilacID", Convert.ToInt32(comboBox8.SelectedValue));
                        bg3.Open();
                        OleDbDataReader reader2 = komutNesnesi3.ExecuteReader();
                        double fiyat = 0;
                        while (reader2.Read())
                        {
                            fiyat = Convert.ToDouble(reader2[2]);
                        }

                        label23.Text = (fiyat * Convert.ToInt32(comboBox7.SelectedItem.ToString())).ToString() + " TL";

                        comboBox8.SelectedIndex = 0;
                        comboBox7.SelectedIndex = 0;
                        textBox8.Text = null;
                        MessageBox.Show("İlaç verildi.");
                        bg3.Close();
                        label23.Text = null;
                    }
                    else
                    {
                        MessageBox.Show("Bu ilaç için maksimum adet 1 olabilir.");
                        comboBox8.SelectedIndex = 0;
                        comboBox7.SelectedIndex = 0;
                        textBox8.Text = null;
                        label23.Text = null;
                    }
                }
                else
                {
                    MessageBox.Show("15 gün geçmeden bu ilaçtan alınamaz.");
                    comboBox8.SelectedIndex = 0;
                    comboBox7.SelectedIndex = 0;
                    textBox8.Text = null;
                    label23.Text = null;
                }
            }
            else
            {
                MessageBox.Show("TC No Yanlış!");
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            dataGridView3.DataSource = dbMethods.recetesizlerDoldur(textBox6.Text);
        }

        static int a = 0;
        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            a++;
            if (a > 1)
            {
                bg2.Close();
                OleDbCommand komutNesnesi3 = new OleDbCommand("select * from ilaclar where ilacID=?", bg2);
                komutNesnesi3.Parameters.AddWithValue("ilacID", Convert.ToInt32(comboBox8.SelectedValue));
                bg2.Open();
                OleDbDataReader reader2 = komutNesnesi3.ExecuteReader();
                double fiyat = 0;
                while (reader2.Read())
                {
                    fiyat = Convert.ToDouble(reader2[2]);
                }

                label23.Text = (fiyat * Convert.ToInt32(comboBox7.SelectedItem.ToString())).ToString() + " TL";
                bg2.Close();
            }
        }

        static int b = 0;
        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            b++;
            if (b > 1)
            {
                bg2.Close();
                OleDbCommand komutNesnesi3 = new OleDbCommand("select * from ilaclar where ilacID=?", bg2);
                komutNesnesi3.Parameters.AddWithValue("ilacID", Convert.ToInt32(comboBox8.SelectedValue));
                bg2.Open();
                OleDbDataReader reader2 = komutNesnesi3.ExecuteReader();
                double fiyat = 0;
                while (reader2.Read())
                {
                    fiyat = Convert.ToDouble(reader2[2]);
                }

                label23.Text = (fiyat * Convert.ToInt32(comboBox7.SelectedItem.ToString())).ToString() + " TL";

                bg2.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int a = Convert.ToInt32(dataGridView3.SelectedRows[0].Cells[0].Value);
            OleDbCommand komutNesnesi = new OleDbCommand("delete from recetesizler where recetesizID=?", bg3);
            komutNesnesi.Parameters.AddWithValue("recetesizID", a);
            bg3.Open();
            komutNesnesi.ExecuteNonQuery();
            bg3.Close();
            dataGridView3.DataSource = dbMethods.recetesizlerDoldur(textBox6.Text);
            button3.Visible = false;
            label14.Text = null;
            MessageBox.Show("İlaç satışı iptal edildi.");
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            button3.Visible = true;


            OleDbCommand komutNesnesi3 = new OleDbCommand("select * from ilaclar where ilacID=?", bg3);
            komutNesnesi3.Parameters.AddWithValue("ilacID", Convert.ToInt32(dataGridView3.SelectedRows[0].Cells[2].Value));
            bg3.Open();
            OleDbDataReader reader2 = komutNesnesi3.ExecuteReader();
            double fiyat = 0;
            while (reader2.Read())
            {
                fiyat = Convert.ToDouble(reader2[2]);
            }
            bg3.Close();
            label14.Text = (fiyat * Convert.ToInt32(dataGridView3.SelectedRows[0].Cells[3].Value)).ToString() + " TL";
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            {
                a = 0;
                b = 0;
                button3.Visible = false;

                dataGridView3.DataSource = dbMethods.recetesizlerDoldur("");

                dataGridView1.DataSource = dbMethods.receteDoldur2(textBox1.Text, textBox2.Text);
                dataGridView2.DataSource = dbMethods.receteDoldur3(textBox3.Text, textBox4.Text);

                button1.Visible = false;
                button2.Visible = false;

                comboBox7.SelectedIndex = 0;

                comboBox8.DataSource = dbMethods.ilacDoldur();
                comboBox8.ValueMember = "ilacID";
                comboBox8.DisplayMember = "ilac";

                comboBox2.DataSource = dbMethods.ilacDoldur();
                comboBox2.ValueMember = "ilacID";
                comboBox2.DisplayMember = "ilac";

                comboBox1.DataSource = dbMethods.eczaneDepoDoldur();
                comboBox1.ValueMember = "depoID";
                comboBox1.DisplayMember = "depo";

                comboBox5.DataSource = dbMethods.ilacDoldur();
                comboBox5.ValueMember = "ilacID";
                comboBox5.DisplayMember = "ilac";

                comboBox4.DataSource = dbMethods.eczaneDepoDoldur();
                comboBox4.ValueMember = "depoID";
                comboBox4.DisplayMember = "depo";

                comboBox3.SelectedIndex = 0;

                dataGridView5.DataSource = dbMethods.siparisDoldur();

                OleDbCommand komutNesnesi3 = new OleDbCommand("select * from ilaclar where ilacID=@ilacID", bg2);
                komutNesnesi3.Parameters.AddWithValue("@ilacID", OleDbType.Decimal).Value = comboBox8.SelectedValue;
                bg2.Open();
                OleDbDataReader reader2 = komutNesnesi3.ExecuteReader();
                double fiyat = 0;
                while (reader2.Read())
                {
                    fiyat = Convert.ToDouble(reader2[2]);
                }

                label23.Text = (fiyat * Convert.ToInt32(comboBox7.SelectedItem.ToString())).ToString() + " TL";
                bg2.Close();
            }
        }
    }
}
