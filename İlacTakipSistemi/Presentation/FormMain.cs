using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;
using İlacTakipSistemi.Business;
using İlacTakipSistemi.DataAccess;
using İlacTakipSistemi.Entity_Layer;

namespace İlacTakipSistemi.Presentation
{
    public partial class FormMain : Form
    {
        private readonly IlacRepository _ilacRepo;
        private readonly HatirlatmaService _hatirlatmaService;

        private readonly HashSet<int> _alarmGosterildi = new HashSet<int>();
        private System.Windows.Forms.Timer _alarmTimer;

        public FormMain()
        {
            InitializeComponent();
            string connStr = ConfigurationManager.ConnectionStrings["İlacTakipDb"].ConnectionString;
            _ilacRepo = new IlacRepository(connStr);
            _hatirlatmaService = new HatirlatmaService(connStr);

            AlarmTimerBaslat();
        }

        private void AlarmTimerBaslat()
        {
            _alarmTimer = new System.Windows.Forms.Timer();
            _alarmTimer.Interval = 30_000;
            _alarmTimer.Tick += AlarmTimer_Tick;
            _alarmTimer.Start();
        }

        private void AlarmTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                DateTime simdi = DateTime.Now;
                int dakikaAnahtari = simdi.Year * 100000000
                                   + simdi.Month * 1000000
                                   + simdi.Day * 10000
                                   + simdi.Hour * 100
                                   + simdi.Minute;

                if (_alarmGosterildi.Contains(dakikaAnahtari)) return;

                var aktifler = _hatirlatmaService.AktifHatirlatmalarGetir(simdi);
                if (aktifler.Count > 0)
                {
                    _alarmGosterildi.Add(dakikaAnahtari);
                    foreach (var h in aktifler)
                    {
                        var alarmForm = new Form
                        {
                            TopMost = true,
                            ShowInTaskbar = false,
                            Size = new System.Drawing.Size(1, 1),
                            Location = new System.Drawing.Point(-2000, -2000)
                        };
                        alarmForm.Show();
                        alarmForm.Focus();

                        MessageBox.Show(
                            alarmForm,
                            $"💊 İlaç Saatiniz Geldi!\n\n" +
                            $"İlaç      : {h.IlacAdi}\n" +
                            $"Kullanıcı : {h.KullaniciAdi}\n" +
                            $"Saat      : {h.HatirlatmaZamani:dd.MM.yyyy HH:mm}",
                            "İlaç Hatırlatıcı",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        alarmForm.Dispose();

                        _hatirlatmaService.AlarmSonrasiIslem(h);
                    }
                }
            }
            catch {  }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            this.Text = "İlaç Takip Sistemi";
            RefreshData();
        }

        private void dgvİlaclar_SelectionChanged(object sender, EventArgs e)
        {
            var row = dgvİlaclar.CurrentRow;
            if (row == null) return;

            txtIlacAd.Text = row.Cells["Ad"].Value?.ToString() ?? "";
            numSiklik.Value = Convert.ToDecimal(row.Cells["KullanimSikligi"].Value);
            txtAciklama.Text = row.Cells["Aciklama"].Value?.ToString() ?? "";
        }

        private void RefreshData()
        {
            dgvİlaclar.DataSource = null;
            dgvİlaclar.DataSource = _ilacRepo.GetAll();

            if (dgvİlaclar.Columns.Count > 0)
            {
                dgvİlaclar.Columns["Id"].Visible = false;
                dgvİlaclar.Columns["Ad"].HeaderText = "İlaç Adı";
                dgvİlaclar.Columns["KullanimSikligi"].HeaderText = "Kullanım Sıklığı (gün)";
                dgvİlaclar.Columns["Aciklama"].HeaderText = "Açıklama";
            }
        }

        private bool InputGecerliMi()
        {
            if (string.IsNullOrWhiteSpace(txtIlacAd.Text))
            {
                MessageBox.Show("İlaç adı boş bırakılamaz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtIlacAd.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtAciklama.Text))
            {
                MessageBox.Show("Açıklama boş bırakılamaz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAciklama.Focus();
                return false;
            }
            return true;
        }

        private void btnIlacEkle_Click(object sender, EventArgs e)
        {
            if (!InputGecerliMi()) return;

            var yeniIlac = new Ilac
            {
                Ad = txtIlacAd.Text.Trim(),
                KullanimSikligi = (int)numSiklik.Value,
                Aciklama = txtAciklama.Text.Trim()
            };
            _ilacRepo.Ekle(yeniIlac);
            MessageBox.Show("İlaç başarıyla eklendi!", "Başarılı",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtIlacAd.Clear();
            txtAciklama.Clear();
            numSiklik.Value = 1;
            RefreshData();
        }

        private void btnIlacSil_Click(object sender, EventArgs e)
        {
            var row = dgvİlaclar.CurrentRow;
            if (row == null)
            {
                MessageBox.Show("Lütfen silmek istediğiniz ilacı seçiniz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var onay = MessageBox.Show("Seçili ilacı silmek istediğinize emin misiniz?",
                "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (onay != DialogResult.Yes) return;

            int ilacId = (int)row.Cells["Id"].Value;
            _ilacRepo.Sil(ilacId);
            MessageBox.Show("İlaç başarıyla silindi!", "Başarılı",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtIlacAd.Clear();
            txtAciklama.Clear();
            numSiklik.Value = 1;
            RefreshData();
        }

        private void btnIlacGuncelle_Click(object sender, EventArgs e)
        {
            var row = dgvİlaclar.CurrentRow;
            if (row == null)
            {
                MessageBox.Show("Lütfen güncellemek istediğiniz ilacı seçiniz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!InputGecerliMi()) return;

            int ilacId = (int)row.Cells["Id"].Value;
            var updatedIlac = new Ilac
            {
                Id = ilacId,
                Ad = txtIlacAd.Text.Trim(),
                KullanimSikligi = (int)numSiklik.Value,
                Aciklama = txtAciklama.Text.Trim()
            };
            _ilacRepo.Guncelle(updatedIlac);
            MessageBox.Show("İlaç başarıyla güncellendi!", "Başarılı",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshData();
        }

        private void btnHatirlatmaFormu_Click(object sender, EventArgs e)
        {

            FormHatirlatma frm = new FormHatirlatma(this);
            frm.Show();
            this.Hide();
        }

        private void btnKullaniciFormu_Click(object sender, EventArgs e)
        {

            FormKullanici frm = new FormKullanici(this);
            frm.Show();
            this.Hide();
        }
    }
}