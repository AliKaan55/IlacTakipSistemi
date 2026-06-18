namespace İlacTakipSistemi.Presentation
{
    partial class FormMain
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            btnIlacEkle = new System.Windows.Forms.Button();
            txtAciklama = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            numSiklik = new System.Windows.Forms.NumericUpDown();
            label2 = new System.Windows.Forms.Label();
            txtIlacAd = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            dgvİlaclar = new System.Windows.Forms.DataGridView();
            btnIlacSil = new System.Windows.Forms.Button();
            btnIlacGuncelle = new System.Windows.Forms.Button();
            btnHatirlatmaFormu = new System.Windows.Forms.Button();
            btnKullaniciFormu = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)numSiklik).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvİlaclar).BeginInit();
            SuspendLayout();

            // label1
            label1.Location = new System.Drawing.Point(20, 20);
            label1.Size = new System.Drawing.Size(80, 25);
            label1.Text = "İlaç Adı:";

            // txtIlacAd
            txtIlacAd.Location = new System.Drawing.Point(120, 18);
            txtIlacAd.Size = new System.Drawing.Size(200, 23);

            // label2
            label2.Location = new System.Drawing.Point(20, 60);
            label2.Size = new System.Drawing.Size(100, 25);
            label2.Text = "Kullanım Sıklığı:";

            // numSiklik
            numSiklik.Location = new System.Drawing.Point(120, 58);
            numSiklik.Size = new System.Drawing.Size(80, 23);
            numSiklik.Minimum = 1;
            numSiklik.Maximum = 365;
            numSiklik.Value = 1;

            // label3
            label3.Location = new System.Drawing.Point(20, 100);
            label3.Size = new System.Drawing.Size(80, 25);
            label3.Text = "Açıklama:";

            // txtAciklama
            txtAciklama.Location = new System.Drawing.Point(120, 98);
            txtAciklama.Size = new System.Drawing.Size(200, 23);

            // btnIlacEkle
            btnIlacEkle.Location = new System.Drawing.Point(120, 140);
            btnIlacEkle.Size = new System.Drawing.Size(100, 30);
            btnIlacEkle.Text = "İlaç Ekle";
            btnIlacEkle.UseVisualStyleBackColor = true;
            btnIlacEkle.Click += btnIlacEkle_Click;

            // btnIlacSil
            btnIlacSil.Location = new System.Drawing.Point(226, 140);
            btnIlacSil.Size = new System.Drawing.Size(100, 30);
            btnIlacSil.Text = "İlaç Kaldır";
            btnIlacSil.BackColor = System.Drawing.Color.IndianRed;
            btnIlacSil.ForeColor = System.Drawing.Color.White;
            btnIlacSil.UseVisualStyleBackColor = false;
            btnIlacSil.Click += btnIlacSil_Click;

            // btnIlacGuncelle
            btnIlacGuncelle.Location = new System.Drawing.Point(332, 140);
            btnIlacGuncelle.Size = new System.Drawing.Size(100, 30);
            btnIlacGuncelle.Text = "İlaç Güncelle";
            btnIlacGuncelle.UseVisualStyleBackColor = true;
            btnIlacGuncelle.Click += btnIlacGuncelle_Click;

            // dgvİlaclar
            dgvİlaclar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvİlaclar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvİlaclar.Location = new System.Drawing.Point(20, 200);
            dgvİlaclar.Size = new System.Drawing.Size(755, 205);
            dgvİlaclar.MultiSelect = false;
            dgvİlaclar.ReadOnly = true;
            dgvİlaclar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvİlaclar.SelectionChanged += dgvİlaclar_SelectionChanged;

            // btnHatirlatmaFormu
            btnHatirlatmaFormu.Location = new System.Drawing.Point(677, 415);
            btnHatirlatmaFormu.Size = new System.Drawing.Size(111, 33);
            btnHatirlatmaFormu.Text = "Hatırlatıcı";
            btnHatirlatmaFormu.UseVisualStyleBackColor = true;
            btnHatirlatmaFormu.Click += btnHatirlatmaFormu_Click;

            // btnKullaniciFormu
            btnKullaniciFormu.Location = new System.Drawing.Point(560, 415);
            btnKullaniciFormu.Size = new System.Drawing.Size(111, 33);
            btnKullaniciFormu.Text = "Kullanıcılar";
            btnKullaniciFormu.UseVisualStyleBackColor = true;
            btnKullaniciFormu.Click += btnKullaniciFormu_Click;

            // FormMain
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 460);
            Text = "İlaç Takip Sistemi";
            Controls.Add(label1);
            Controls.Add(txtIlacAd);
            Controls.Add(label2);
            Controls.Add(numSiklik);
            Controls.Add(label3);
            Controls.Add(txtAciklama);
            Controls.Add(btnIlacEkle);
            Controls.Add(btnIlacSil);
            Controls.Add(btnIlacGuncelle);
            Controls.Add(dgvİlaclar);
            Controls.Add(btnHatirlatmaFormu);
            Controls.Add(btnKullaniciFormu);
            Load += FormMain_Load;

            ((System.ComponentModel.ISupportInitialize)numSiklik).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvİlaclar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btnIlacEkle;
        private System.Windows.Forms.TextBox txtAciklama;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numSiklik;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIlacAd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvİlaclar;
        private System.Windows.Forms.Button btnIlacSil;
        private System.Windows.Forms.Button btnIlacGuncelle;
        private System.Windows.Forms.Button btnHatirlatmaFormu;
        private System.Windows.Forms.Button btnKullaniciFormu;
    }
}