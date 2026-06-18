using System;
using System.Configuration;
using System.Windows.Forms;
using İlacTakipSistemi.Business;
using İlacTakipSistemi.DataAccess;
using İlacTakipSistemi.Entity_Layer;

namespace İlacTakipSistemi.Presentation
{
    public partial class FormKullanici : Form
    {
        private readonly KullaniciRepository _kullaniciRepo;
        private readonly HatirlatmaService _hatirlatmaService;

        private readonly Form _ownerForm;

        public FormKullanici(Form ownerForm)
        {
            InitializeComponent();
            _ownerForm = ownerForm;

            string connStr = ConfigurationManager.ConnectionStrings["İlacTakipDb"].ConnectionString;
            _kullaniciRepo = new KullaniciRepository(connStr);
            _hatirlatmaService = new HatirlatmaService(connStr);

            KullanicilariListele();
        }

        private void KullanicilariListele()
        {
            dgvKullanicilar.DataSource = null;
            dgvKullanicilar.DataSource = _kullaniciRepo.GetAll();

            if (dgvKullanicilar.Columns.Count > 0)
            {
                dgvKullanicilar.Columns["Id"].Visible = false;
                dgvKullanicilar.Columns["AdSoyad"].HeaderText = "Ad Soyad";
                dgvKullanicilar.Columns["Telefon"].HeaderText = "Telefon";
            }
        }

        private bool InputGecerliMi()
        {
            if (string.IsNullOrWhiteSpace(txtAdSoyad.Text))
            {
                MessageBox.Show("Ad Soyad boş bırakılamaz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAdSoyad.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTelefon.Text))
            {
                MessageBox.Show("Telefon boş bırakılamaz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefon.Focus();
                return false;
            }
            return true;
        }

        private void btnKullaniciEkle_Click(object sender, EventArgs e)
        {
            if (!InputGecerliMi()) return;

            var yeniKullanici = new Kullanici
            {
                AdSoyad = txtAdSoyad.Text.Trim(),
                Telefon = txtTelefon.Text.Trim()
            };
            _kullaniciRepo.Ekle(yeniKullanici);
            MessageBox.Show("Kullanıcı başarıyla eklendi!", "Başarılı",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            txtAdSoyad.Clear();
            txtTelefon.Clear();
            KullanicilariListele();
        }

        private void btnKullaniciSil_Click(object sender, EventArgs e)
        {
            var row = dgvKullanicilar.CurrentRow;
            if (row == null)
            {
                MessageBox.Show("Lütfen silmek istediğiniz kullanıcıyı seçiniz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int kullaniciId = (int)row.Cells["Id"].Value;

            if (_hatirlatmaService.KullaniciyaAitHatirlatmaVarMi(kullaniciId))
            {
                MessageBox.Show(
                    "Bu kullanıcıya ait hatırlatma kayıtları bulunuyor. " +
                    "Kullanıcıyı silmeden önce ilgili hatırlatmaları siliniz.",
                    "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var onay = MessageBox.Show("Seçili kullanıcıyı silmek istediğinize emin misiniz?",
                "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (onay != DialogResult.Yes) return;

            _kullaniciRepo.Sil(kullaniciId);
            MessageBox.Show("Kullanıcı başarıyla silindi!", "Başarılı",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            txtAdSoyad.Clear();
            txtTelefon.Clear();
            KullanicilariListele();
        }

        private void dgvKullanicilar_SelectionChanged(object sender, EventArgs e)
        {
            var row = dgvKullanicilar.CurrentRow;
            if (row == null) return;

            txtAdSoyad.Text = row.Cells["AdSoyad"].Value?.ToString() ?? "";
            txtTelefon.Text = row.Cells["Telefon"].Value?.ToString() ?? "";
        }

        private void btnGeriDon_Click(object sender, EventArgs e)
        {
            _ownerForm.Show();
            this.Close();
        }
    }
}
