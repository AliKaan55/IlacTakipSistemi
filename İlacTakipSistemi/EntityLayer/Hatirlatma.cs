using System;

namespace İlacTakipSistemi.Entity_Layer
{
    public class Hatirlatma
    {
        public int Id { get; set; }
        public int IlacId { get; set; }
        public int KullaniciId { get; set; }
        public DateTime HatirlatmaZamani { get; set; }


        public bool Tekrarli { get; set; }
        public int TekrarAraligiGun { get; set; }

        public string IlacAdi { get; set; }
        public string KullaniciAdi { get; set; }

        public string TekrarBilgisi => Tekrarli
            ? (TekrarAraligiGun == 1 ? "Her gün" : $"Her {TekrarAraligiGun} günde bir")
            : "Tekrarsız";
    }
}   