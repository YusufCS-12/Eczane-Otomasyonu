using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eczane_Otomasyon
{
    class dbMethods
    {
        static OleDbConnection baglantiNesnesiv2 = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\YCS\Desktop\Eczane Otomasyon\eczane.mdb");
        public static Boolean yoneticiLogin(string kullaniciAdi, string password)
        {
            OleDbCommand komutNesnesi = new OleDbCommand("select * from yoneticiler where kullaniciAdi=@ka and sifre=@s", baglantiNesnesiv2);
            komutNesnesi.Parameters.AddWithValue("@ka", kullaniciAdi);
            komutNesnesi.Parameters.AddWithValue("@s", password);
            OleDbDataAdapter adaptor = new OleDbDataAdapter(komutNesnesi);
            DataTable dt = new DataTable();
            baglantiNesnesiv2.Open();
            adaptor.Fill(dt);
            baglantiNesnesiv2.Close();
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Boolean eczaneLogin(string kullaniciAdi, string password)
        {
            OleDbCommand komutNesnesi = new OleDbCommand("select * from eczaneler where kullaniciAdi=@ka and sifre=@s", baglantiNesnesiv2);
            komutNesnesi.Parameters.AddWithValue("@ka", kullaniciAdi);
            komutNesnesi.Parameters.AddWithValue("@s", password);
            OleDbDataAdapter adaptor = new OleDbDataAdapter(komutNesnesi);
            DataTable dt = new DataTable();
            baglantiNesnesiv2.Open();
            adaptor.Fill(dt);
            baglantiNesnesiv2.Close();
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int eczaneIDCek(string kullaniciAdi, string password)
        {
            OleDbCommand komutNesnesi = new OleDbCommand("select eczaneID from eczaneler where kullaniciAdi=@ka and sifre=@s", baglantiNesnesiv2);
            komutNesnesi.Parameters.AddWithValue("@ka", kullaniciAdi);
            komutNesnesi.Parameters.AddWithValue("@s", password);
            baglantiNesnesiv2.Open();
            OleDbDataReader reader = komutNesnesi.ExecuteReader();
            while (reader.Read())
            {
                return Convert.ToInt32(reader[0]);
            }
            baglantiNesnesiv2.Close();
            return 0;
        }

        public static DataTable sehirDoldur()
        {
            OleDbCommand komutNesnesi = new OleDbCommand("select * from sehirler", baglantiNesnesiv2);
            OleDbDataAdapter adaptor = new OleDbDataAdapter(komutNesnesi);
            DataTable dt = new DataTable();
            baglantiNesnesiv2.Open();
            adaptor.Fill(dt);
            baglantiNesnesiv2.Close();
            return dt;
        }

        public static DataTable ilceDoldur(int sehirID)
        {
            OleDbCommand komutNesnesi = new OleDbCommand("select * from ilceler where sehirID=@sehirID", baglantiNesnesiv2);
            komutNesnesi.Parameters.AddWithValue("@sehirID", sehirID);
            OleDbDataAdapter adaptor = new OleDbDataAdapter(komutNesnesi);
            DataTable dt = new DataTable();
            baglantiNesnesiv2.Open();
            adaptor.Fill(dt);
            baglantiNesnesiv2.Close();
            return dt;
        }

        public static DataTable eczaneDoldur(int ilceID)
        {
            OleDbCommand komutNesnesi = new OleDbCommand("select eczaneler.eczaneID,eczaneler.eczane,ilceler.ilceID,ilceler.ilce,sehirler.sehirID from ((eczaneler inner join ilceler on ilceler.ilceID=eczaneler.ilceID) inner join sehirler on ilceler.sehirID=sehirler.sehirID) where eczaneler.ilceID=@ilceID", baglantiNesnesiv2);
            komutNesnesi.Parameters.AddWithValue("@ilceID", ilceID);
            OleDbDataAdapter adaptor = new OleDbDataAdapter(komutNesnesi);
            DataTable dt = new DataTable();
            baglantiNesnesiv2.Open();
            adaptor.Fill(dt);
            baglantiNesnesiv2.Close();
            return dt;
        }

        public static DataTable eczaneDoldur()
        {
            OleDbCommand komutNesnesi = new OleDbCommand("select * from eczaneler", baglantiNesnesiv2);
            OleDbDataAdapter adaptor = new OleDbDataAdapter(komutNesnesi);
            DataTable dt = new DataTable();
            baglantiNesnesiv2.Open();
            adaptor.Fill(dt);
            baglantiNesnesiv2.Close();
            return dt;
        }

        public static DataTable hastaDoldur()
        {
            OleDbCommand komutNesnesi = new OleDbCommand("select * from hastalar", baglantiNesnesiv2);
            OleDbDataAdapter adaptor = new OleDbDataAdapter(komutNesnesi);
            DataTable dt = new DataTable();
            baglantiNesnesiv2.Open();
            adaptor.Fill(dt);
            baglantiNesnesiv2.Close();
            return dt;
        }

        public static DataTable ilacDoldur()
        {
            OleDbCommand komutNesnesi = new OleDbCommand("select * from ilaclar", baglantiNesnesiv2);
            OleDbDataAdapter adaptor = new OleDbDataAdapter(komutNesnesi);
            DataTable dt = new DataTable();
            baglantiNesnesiv2.Open();
            adaptor.Fill(dt);
            baglantiNesnesiv2.Close();
            return dt;
        }

        public static DataTable receteDoldur()
        {
            OleDbCommand komutNesnesi = new OleDbCommand("select * from receteler", baglantiNesnesiv2);
            OleDbDataAdapter adaptor = new OleDbDataAdapter(komutNesnesi);
            DataTable dt = new DataTable();
            baglantiNesnesiv2.Open();
            adaptor.Fill(dt);
            baglantiNesnesiv2.Close();
            return dt;
        }

        public static DataTable eczaneDepoDoldur()
        {
            OleDbCommand komutNesnesi = new OleDbCommand("select * from eczaneDepo", baglantiNesnesiv2);
            OleDbDataAdapter adaptor = new OleDbDataAdapter(komutNesnesi);
            DataTable dt = new DataTable();
            baglantiNesnesiv2.Open();
            adaptor.Fill(dt);
            baglantiNesnesiv2.Close();
            return dt;
        }
        public static DataTable siparisDoldur()
        {
            OleDbCommand komutNesnesi = new OleDbCommand("select * from ((siparisler inner join eczaneDepo on eczaneDepo.depoID=siparisler.depoID) inner join ilaclar on ilaclar.ilacID=siparisler.ilacID)", baglantiNesnesiv2);
            OleDbDataAdapter adaptor = new OleDbDataAdapter(komutNesnesi);
            DataTable dt = new DataTable();
            baglantiNesnesiv2.Open();
            adaptor.Fill(dt);
            baglantiNesnesiv2.Close();
            return dt;
        }

        public static DataTable receteDoldur2(string TC, string receteKodu)
        {
            OleDbCommand komutNesnesi = new OleDbCommand("select receteler.receteID,receteTuru.receteTuru,receteTarihi,receteKodu,ilaclar.ilacID,ilaclar.ilac,ilaclar.fiyat,adet,sigortalar.odeme,receteler.eczaneID,hastalar.TC from ((((receteler inner join hastalar on hastalar.hastaID=receteler.hastaID) inner join sigortalar on sigortalar.sigortaID=hastalar.sigortaID) inner join ilaclar on ilaclar.ilacID=receteler.ilacID) inner join receteTuru on receteTuru.receteTuruID=ilaclar.receteTuruID) where receteler.durum=0 and hastalar.TC like '"+'%'+TC+'%'+ "' and receteler.receteKodu like '" + '%' + receteKodu + '%' + "'", baglantiNesnesiv2);
            komutNesnesi.Parameters.AddWithValue("@hastalar.TC", TC);
            komutNesnesi.Parameters.AddWithValue("@receteler.receteKodu", receteKodu);
            OleDbDataAdapter adaptor = new OleDbDataAdapter(komutNesnesi);
            DataTable dt = new DataTable();
            baglantiNesnesiv2.Open();
            adaptor.Fill(dt);
            baglantiNesnesiv2.Close();
            return dt;
        }

        public static DataTable receteDoldur3(string TC, string receteKodu)
        {
            OleDbCommand komutNesnesi = new OleDbCommand("select receteler.receteID,receteTuru.receteTuru,receteTarihi,receteKodu,ilaclar.ilacID,ilaclar.ilac,ilaclar.fiyat,adet,sigortalar.odeme,receteler.eczaneID,hastalar.TC from ((((receteler inner join hastalar on hastalar.hastaID=receteler.hastaID) inner join sigortalar on sigortalar.sigortaID=hastalar.sigortaID) inner join ilaclar on ilaclar.ilacID=receteler.ilacID) inner join receteTuru on receteTuru.receteTuruID=ilaclar.receteTuruID) where receteler.durum=1 and hastalar.TC like '" + '%' + TC + '%' + "' and receteler.receteKodu like '" + '%' + receteKodu + '%' + "'", baglantiNesnesiv2);
            komutNesnesi.Parameters.AddWithValue("@TC", TC);
            komutNesnesi.Parameters.AddWithValue("@receteKodu", receteKodu);
            OleDbDataAdapter adaptor = new OleDbDataAdapter(komutNesnesi);
            DataTable dt = new DataTable();
            baglantiNesnesiv2.Open();
            adaptor.Fill(dt);
            baglantiNesnesiv2.Close();
            return dt;
        }

        public static DataTable receteTuruDoldur()
        {
            OleDbCommand komutNesnesi = new OleDbCommand("select * from receteTuru", baglantiNesnesiv2);
            OleDbDataAdapter adaptor = new OleDbDataAdapter(komutNesnesi);
            DataTable dt = new DataTable();
            baglantiNesnesiv2.Open();
            adaptor.Fill(dt);
            baglantiNesnesiv2.Close();
            return dt;
        }

        public static DataTable recetesizlerDoldur(string TC)
        {
            baglantiNesnesiv2.Close();
            OleDbCommand komutNesnesi = new OleDbCommand("select * from recetesizler where TC like '%'+@TC+'%'", baglantiNesnesiv2);
            komutNesnesi.Parameters.AddWithValue("@TC", TC);
            OleDbDataAdapter adaptor = new OleDbDataAdapter(komutNesnesi);
            DataTable dt = new DataTable();
            baglantiNesnesiv2.Open();
            adaptor.Fill(dt);
            baglantiNesnesiv2.Close();
            return dt;
        }
    }
}
