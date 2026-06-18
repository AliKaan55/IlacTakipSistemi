using İlacTakipSistemi.Entity_Layer;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace İlacTakipSistemi.DataAccess
{
    public class KullaniciRepository
    {
        private readonly string _connectionString;

        public KullaniciRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Ekle(Kullanici k)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("INSERT INTO Kullanici (AdSoyad, Telefon) VALUES (@AdSoyad,@Telefon)", conn);
                cmd.Parameters.AddWithValue("@AdSoyad", k.AdSoyad);
                cmd.Parameters.AddWithValue("@Telefon", k.Telefon);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Kullanici> GetAll()
        {
            var list = new List<Kullanici>();
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("SELECT Id, AdSoyad, Telefon FROM Kullanici", conn);
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Kullanici
                    {
                        Id = reader.GetInt32(0),
                        AdSoyad = reader.GetString(1),
                        Telefon = reader.GetString(2)
                    });
                }
            }
            return list;
        }

        public void Sil(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("DELETE FROM Kullanici WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
