using İlacTakipSistemi.Entity_Layer;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace İlacTakipSistemi.DataAccess
{
    public class IlacRepository
    {
        private readonly string _connectionString;

        public IlacRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Ekle(Ilac ilac)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("INSERT INTO Ilac (Ad, KullanimSikligi, Aciklama) VALUES (@Ad,@KullanimSikligi,@Aciklama)", conn);
                cmd.Parameters.AddWithValue("@Ad", ilac.Ad);
                cmd.Parameters.AddWithValue("@KullanimSikligi", ilac.KullanimSikligi);
                cmd.Parameters.AddWithValue("@Aciklama", ilac.Aciklama);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void Sil(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("DELETE FROM Ilac WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void Guncelle(Ilac ilac)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("UPDATE Ilac SET Ad = @Ad, KullanimSikligi = @KullanimSikligi, Aciklama = @Aciklama WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", ilac.Id);
                cmd.Parameters.AddWithValue("@Ad", ilac.Ad);
                cmd.Parameters.AddWithValue("@KullanimSikligi", ilac.KullanimSikligi);
                cmd.Parameters.AddWithValue("@Aciklama", ilac.Aciklama);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Ilac> GetAll()
        {
            var list = new List<Ilac>();
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("SELECT Id, Ad, KullanimSikligi, Aciklama FROM Ilac", conn);
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Ilac
                    {
                        Id = reader.GetInt32(0),
                        Ad = reader.GetString(1),
                        KullanimSikligi = reader.GetInt32(2),
                        Aciklama = reader.GetString(3)
                    });
                }
            }
            return list;
        }
    }
}
