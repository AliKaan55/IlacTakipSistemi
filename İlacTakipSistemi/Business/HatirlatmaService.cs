using System;
using System.Collections.Generic;
using İlacTakipSistemi.DataAccess;
using İlacTakipSistemi.Entity_Layer;

namespace İlacTakipSistemi.Business
{
    public class HatirlatmaService
    {
        private readonly HatirlatmaRepository _hatirlatmaRepo;

        public HatirlatmaService(string connStr)
        {
            _hatirlatmaRepo = new HatirlatmaRepository(connStr);
        }

        public bool HatirlatmaEkle(Hatirlatma h)
        {
            if (_hatirlatmaRepo.AyniZamanVarMi(h.IlacId, h.HatirlatmaZamani))
                return false;

            _hatirlatmaRepo.Ekle(h);
            return true;
        }


        public bool HatirlatmaGuncelle(Hatirlatma h)
        {
            if (_hatirlatmaRepo.AyniZamanVarMi(h.IlacId, h.HatirlatmaZamani, h.Id))
                return false;

            _hatirlatmaRepo.Guncelle(h);
            return true;
        }

        public void HatirlatmaSil(int id)
        {
            _hatirlatmaRepo.Sil(id);
        }

        public List<Hatirlatma> HatirlatmalariListele()
        {
            return _hatirlatmaRepo.GetAll();
        }


        public List<Hatirlatma> AktifHatirlatmalarGetir(DateTime simdi)
        {
            return _hatirlatmaRepo.GetAktifHatirlatmalar(simdi);
        }


        public void AlarmSonrasiIslem(Hatirlatma h)
        {
            if (h.Tekrarli && h.TekrarAraligiGun > 0)
            {
                DateTime yeniZaman = h.HatirlatmaZamani.AddDays(h.TekrarAraligiGun);
                _hatirlatmaRepo.ZamaniGuncelle(h.Id, yeniZaman);
            }
            else
            {
                _hatirlatmaRepo.Sil(h.Id);
            }
        }

        public bool KullaniciyaAitHatirlatmaVarMi(int kullaniciId)
        {
            return _hatirlatmaRepo.KullaniciyaAitHatirlatmaVarMi(kullaniciId);
        }
    }
}
