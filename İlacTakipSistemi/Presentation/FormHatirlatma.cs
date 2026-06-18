using System;
using System.Configuration;
using System.Windows.Forms;
using İlacTakipSistemi.Business;
using İlacTakipSistemi.DataAccess;
using İlacTakipSistemi.Entity_Layer;

namespace İlacTakipSistemi.Presentation
{
    public partial class FormHatirlatma : Form
    {
        private readonly HatirlatmaService _hatirlatmaService;
        private readonly IlacRepository _ilacRepo;
        private readonly KullaniciRepository _kullaniciRepo;

        private readonly Form _ownerForm;

        private int _duzenlenenId = 0;

        public FormHatirlatma(Form ownerForm)
        {
            InitializeComponent();
            _ownerForm = ownerForm;

            string connStr = ConfigurationManager.ConnectionStrings["İlacTakipDb"].ConnectionString;
            _hatirlatmaService = new HatirlatmaService(connStr);
            _ilacRepo = new IlacRepository(connStr);
            _kullaniciRepo = new KullaniciRepository(connStr);

            IlaclariYukle();
            KullanicilariYukle();
            HatirlatmalariListele();
            TekrarAlanlariniGuncelle();
        }

        private void IlaclariYukle()
        {
            cmbIlac.DataSource = _ilacRepo.GetAll();
            cmbIlac.DisplayMember = "Ad";
            cmbIlac.ValueMember = "Id";
        }

        private void KullanicilariYukle()
        {
            cmbKullanici.DataSource = _kullaniciRepo.GetAll();
            cmbKullanici.DisplayMember = "AdSoyad";
            cmbKullanici.ValueMember = "Id";
        }

        private void HatirlatmalariListele()
        {
            dgvHatirlatmalar.DataSource = null;
            dgvHatirlatmalar.DataSource = _hatirlatmaService.HatirlatmalariListele();

            if (dgvHatirlatmalar.Columns.Count > 0)
            {
                dgvHatirlatmalar.Columns["Id"].Visible = false;
                dgvHatirlatmalar.Columns["IlacId"].Visible = false;
                dgvHatirlatmalar.Columns["KullaniciId"].Visible = false;
                dgvHatirlatmalar.Columns["Tekrarli"].Visible = false;
                dgvHatirlatmalar.Columns["TekrarAraligiGun"].Visible = false;
                dgvHatirlatmalar.Columns["IlacAdi"].HeaderText = "İlaç";
                dgvHatirlatmalar.Columns["KullaniciAdi"].HeaderText = "Kullanıcı";
                dgvHatirlatmalar.Columns["HatirlatmaZamani"].HeaderText = "Tarih / Saat";
                dgvHatirlatmalar.Columns["TekrarBilgisi"].HeaderText = "Tekrar";
            }
        }

        private void chkTekrarla_CheckedChanged(object sender, EventArgs e)
        {
            TekrarAlanlariniGuncelle();
        }

        private void TekrarAlanlariniGuncelle()
        {
            numTekrarAraligi.Enabled = chkTekrarla.Checked;
        }

        private void dgvHatirlatmalar_SelectionChanged(object sender, EventArgs e)
        {
            var row = dgvHatirlatmalar.CurrentRow;
            if (row == null) return;

            _duzenlenenId = (int)row.Cells["Id"].Value;
            cmbIlac.SelectedValue = (int)row.Cells["IlacId"].Value;
            cmbKullanici.SelectedValue = (int)row.Cells["KullaniciId"].Value;
            dtpTarihSaat.Value = (DateTime)row.Cells["HatirlatmaZamani"].Value;

            bool tekrarli = (bool)row.Cells["Tekrarli"].Value;
            chkTekrarla.Checked = tekrarli;
            numTekrarAraligi.Value = tekrarli ? (int)row.Cells["TekrarAraligiGun"].Value : 1;

            btnHatirlatmaDuzenle.Enabled = true;
        }

        private bool InputGecerliMi()
        {
            if (cmbIlac.SelectedValue == null)
            {
                MessageBox.Show("Lütfen bir ilaç seçiniz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cmbKullanici.SelectedValue == null)
            {
                MessageBox.Show("Lütfen bir kullanıcı seçiniz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (dtpTarihSaat.Value <= DateTime.Now)
            {
                MessageBox.Show("Lütfen gelecekte bir tarih ve saat seçiniz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (chkTekrarla.Checked && numTekrarAraligi.Value < 1)
            {
                MessageBox.Show("Tekrar aralığı en az 1 gün olmalıdır.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private Hatirlatma FormdanHatirlatmaOlustur(int id)
        {
            return new Hatirlatma
            {
                Id = id,
                IlacId = (int)cmbIlac.SelectedValue,
                KullaniciId = (int)cmbKullanici.SelectedValue,
                HatirlatmaZamani = dtpTarihSaat.Value,
                Tekrarli = chkTekrarla.Checked,
                TekrarAraligiGun = chkTekrarla.Checked ? (int)numTekrarAraligi.Value : 0
            };
        }

        private void FormuTemizle()
        {
            _duzenlenenId = 0;
            chkTekrarla.Checked = false;
            numTekrarAraligi.Value = 1;
            dtpTarihSaat.Value = DateTime.Now.AddHours(1);
            btnHatirlatmaDuzenle.Enabled = false;
            dgvHatirlatmalar.ClearSelection();
        }

        private void btnHatirlatmaEkle_Click(object sender, EventArgs e)
        {
            if (!InputGecerliMi()) return;

            var yeniHatirlatma = FormdanHatirlatmaOlustur(0);

            bool eklendi = _hatirlatmaService.HatirlatmaEkle(yeniHatirlatma);

            if (eklendi)
                MessageBox.Show("Hatırlatma başarıyla eklendi!", "Başarılı",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Bu ilaç için seçilen tarih ve saatte zaten bir hatırlatma var!",
                    "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            HatirlatmalariListele();
            FormuTemizle();
        }

        private void btnHatirlatmaDuzenle_Click(object sender, EventArgs e)
        {
            if (_duzenlenenId == 0)
            {
                MessageBox.Show("Lütfen düzenlemek istediğiniz hatırlatmayı listeden seçiniz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!InputGecerliMi()) return;

            var guncellenenHatirlatma = FormdanHatirlatmaOlustur(_duzenlenenId);

            bool guncellendi = _hatirlatmaService.HatirlatmaGuncelle(guncellenenHatirlatma);

            if (guncellendi)
                MessageBox.Show("Hatırlatma başarıyla güncellendi!", "Başarılı",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Bu ilaç için seçilen tarih ve saatte zaten başka bir hatırlatma var!",
                    "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            HatirlatmalariListele();
            FormuTemizle();
        }

        private void btnHatirlatmaSil_Click(object sender, EventArgs e)
        {
            if (dgvHatirlatmalar.CurrentRow == null)
            {
                MessageBox.Show("Lütfen silmek istediğiniz hatırlatmayı seçiniz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var onay = MessageBox.Show("Seçili hatırlatmayı silmek istediğinize emin misiniz?",
                "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (onay != DialogResult.Yes) return;

            int id = (int)dgvHatirlatmalar.CurrentRow.Cells["Id"].Value;
            _hatirlatmaService.HatirlatmaSil(id);
            MessageBox.Show("Hatırlatma başarıyla silindi!", "Başarılı",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            HatirlatmalariListele();
            FormuTemizle();
        }

        private void btnGeriDon_Click(object sender, EventArgs e)
        {
            _ownerForm.Show();
            this.Close();
        }
    }
}
