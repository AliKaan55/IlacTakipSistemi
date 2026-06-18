using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using İlacTakipSistemi.Entity_Layer;

namespace İlacTakipSistemi.DataAccess
{
    public class HatirlatmaRepository
    {
        private readonly string _connectionString;

        public HatirlatmaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Ekle(Hatirlatma h)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand(
                    "INSERT INTO Hatirlatma (IlacId, KullaniciId, HatirlatmaZamani, TekrarliMi, TekrarAraligiGun) " +
                    "VALUES (@IlacId, @KullaniciId, @HatirlatmaZamani, @TekrarliMi, @TekrarAraligiGun)", conn);
                cmd.Parameters.AddWithValue("@IlacId", h.IlacId);
                cmd.Parameters.AddWithValue("@KullaniciId", h.KullaniciId);
                cmd.Parameters.AddWithValue("@HatirlatmaZamani", h.HatirlatmaZamani);
                cmd.Parameters.AddWithValue("@TekrarliMi", h.Tekrarli);
                cmd.Parameters.AddWithValue("@TekrarAraligiGun", h.Tekrarli ? h.TekrarAraligiGun : 0);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public void Guncelle(Hatirlatma h)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand(
                    "UPDATE Hatirlatma SET IlacId = @IlacId, KullaniciId = @KullaniciId, " +
                    "HatirlatmaZamani = @HatirlatmaZamani, TekrarliMi = @TekrarliMi, " +
                    "TekrarAraligiGun = @TekrarAraligiGun WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", h.Id);
                cmd.Parameters.AddWithValue("@IlacId", h.IlacId);
                cmd.Parameters.AddWithValue("@KullaniciId", h.KullaniciId);
                cmd.Parameters.AddWithValue("@HatirlatmaZamani", h.HatirlatmaZamani);
                cmd.Parameters.AddWithValue("@TekrarliMi", h.Tekrarli);
                cmd.Parameters.AddWithValue("@TekrarAraligiGun", h.Tekrarli ? h.TekrarAraligiGun : 0);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public void ZamaniGuncelle(int id, DateTime yeniZaman)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand(
                    "UPDATE Hatirlatma SET HatirlatmaZamani = @Zaman WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Zaman", yeniZaman);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public bool AyniZamanVarMi(int ilacId, DateTime zaman, int excludeId = 0)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM Hatirlatma " +
                    "WHERE IlacId = @IlacId " +
                    "AND CONVERT(VARCHAR(16), HatirlatmaZamani, 120) = CONVERT(VARCHAR(16), @Zaman, 120) " +
                    "AND Id <> @ExcludeId",
                    conn);
                cmd.Parameters.AddWithValue("@IlacId", ilacId);
                cmd.Parameters.AddWithValue("@Zaman", zaman);
                cmd.Parameters.AddWithValue("@ExcludeId", excludeId);
                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }


        public List<Hatirlatma> GetAktifHatirlatmalar(DateTime simdi)
        {
            var list = new List<Hatirlatma>();
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand(
                    "SELECT h.Id, h.IlacId, h.KullaniciId, h.HatirlatmaZamani, h.TekrarliMi, h.TekrarAraligiGun, " +
                    "       i.Ad AS IlacAdi, k.AdSoyad AS KullaniciAdi " +
                    "FROM Hatirlatma h " +
                    "JOIN Ilac i ON i.Id = h.IlacId " +
                    "JOIN Kullanici k ON k.Id = h.KullaniciId " +
                    "WHERE CONVERT(VARCHAR(16), h.HatirlatmaZamani, 120) = CONVERT(VARCHAR(16), @Simdi, 120)",
                    conn);
                cmd.Parameters.AddWithValue("@Simdi", simdi);
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Hatirlatma
                    {
                        Id = reader.GetInt32(0),
                        IlacId = reader.GetInt32(1),
                        KullaniciId = reader.GetInt32(2),
                        HatirlatmaZamani = reader.GetDateTime(3),
                        Tekrarli = reader.GetBoolean(4),
                        TekrarAraligiGun = reader.GetInt32(5),
                        IlacAdi = reader.GetString(6),
                        KullaniciAdi = reader.GetString(7)
                    });
                }
            }
            return list;
        }

        public void Sil(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("DELETE FROM Hatirlatma WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public bool KullaniciyaAitHatirlatmaVarMi(int kullaniciId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM Hatirlatma WHERE KullaniciId = @KullaniciId", conn);
                cmd.Parameters.AddWithValue("@KullaniciId", kullaniciId);
                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public List<Hatirlatma> GetAll()
        {
            var list = new List<Hatirlatma>();
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand(
                    "SELECT h.Id, h.IlacId, h.KullaniciId, h.HatirlatmaZamani, h.TekrarliMi, h.TekrarAraligiGun, " +
                    "       i.Ad AS IlacAdi, k.AdSoyad AS KullaniciAdi " +
                    "FROM Hatirlatma h " +
                    "JOIN Ilac i ON i.Id = h.IlacId " +
                    "JOIN Kullanici k ON k.Id = h.KullaniciId " +
                    "ORDER BY h.HatirlatmaZamani",
                    conn);
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Hatirlatma
                    {
                        Id = reader.GetInt32(0),
                        IlacId = reader.GetInt32(1),
                        KullaniciId = reader.GetInt32(2),
                        HatirlatmaZamani = reader.GetDateTime(3),
                        Tekrarli = reader.GetBoolean(4),
                        TekrarAraligiGun = reader.GetInt32(5),
                        IlacAdi = reader.GetString(6),
                        KullaniciAdi = reader.GetString(7)
                    });
                }
            }
            return list;
        }
    }
}
