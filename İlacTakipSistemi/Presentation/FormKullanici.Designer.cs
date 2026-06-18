namespace İlacTakipSistemi.Presentation
{
    partial class FormKullanici
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
            label1 = new System.Windows.Forms.Label();
            txtAdSoyad = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            txtTelefon = new System.Windows.Forms.TextBox();
            btnKullaniciEkle = new System.Windows.Forms.Button();
            btnKullaniciSil = new System.Windows.Forms.Button();
            dgvKullanicilar = new System.Windows.Forms.DataGridView();
            btnGeriDon = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)dgvKullanicilar).BeginInit();
            SuspendLayout();

            // label1 - Ad Soyad
            label1.Location = new System.Drawing.Point(20, 20);
            label1.Size = new System.Drawing.Size(90, 25);
            label1.Text = "Ad Soyad:";

            // txtAdSoyad
            txtAdSoyad.Location = new System.Drawing.Point(120, 18);
            txtAdSoyad.Size = new System.Drawing.Size(220, 23);

            // label2 - Telefon
            label2.Location = new System.Drawing.Point(20, 60);
            label2.Size = new System.Drawing.Size(90, 25);
            label2.Text = "Telefon:";

            // txtTelefon
            txtTelefon.Location = new System.Drawing.Point(120, 58);
            txtTelefon.Size = new System.Drawing.Size(220, 23);

            // btnKullaniciEkle
            btnKullaniciEkle.Location = new System.Drawing.Point(120, 100);
            btnKullaniciEkle.Size = new System.Drawing.Size(110, 30);
            btnKullaniciEkle.Text = "Kullanıcı Ekle";
            btnKullaniciEkle.UseVisualStyleBackColor = true;
            btnKullaniciEkle.Click += btnKullaniciEkle_Click;

            // btnKullaniciSil
            btnKullaniciSil.Location = new System.Drawing.Point(240, 100);
            btnKullaniciSil.Size = new System.Drawing.Size(110, 30);
            btnKullaniciSil.Text = "Kullanıcı Sil";
            btnKullaniciSil.BackColor = System.Drawing.Color.IndianRed;
            btnKullaniciSil.ForeColor = System.Drawing.Color.White;
            btnKullaniciSil.UseVisualStyleBackColor = false;
            btnKullaniciSil.Click += btnKullaniciSil_Click;

            // dgvKullanicilar
            dgvKullanicilar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvKullanicilar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvKullanicilar.Location = new System.Drawing.Point(20, 150);
            dgvKullanicilar.Size = new System.Drawing.Size(550, 230);
            dgvKullanicilar.ReadOnly = true;
            dgvKullanicilar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvKullanicilar.MultiSelect = false;
            dgvKullanicilar.SelectionChanged += dgvKullanicilar_SelectionChanged;

            // btnGeriDon
            btnGeriDon.Location = new System.Drawing.Point(459, 400);
            btnGeriDon.Size = new System.Drawing.Size(111, 41);
            btnGeriDon.Text = "Geri Dön";
            btnGeriDon.UseVisualStyleBackColor = true;
            btnGeriDon.Click += btnGeriDon_Click;

            // FormKullanici
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(590, 460);
            Text = "Kullanıcı Yönetimi";
            Controls.Add(label1);
            Controls.Add(txtAdSoyad);
            Controls.Add(label2);
            Controls.Add(txtTelefon);
            Controls.Add(btnKullaniciEkle);
            Controls.Add(btnKullaniciSil);
            Controls.Add(dgvKullanicilar);
            Controls.Add(btnGeriDon);

            ((System.ComponentModel.ISupportInitialize)dgvKullanicilar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAdSoyad;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTelefon;
        private System.Windows.Forms.Button btnKullaniciEkle;
        private System.Windows.Forms.Button btnKullaniciSil;
        private System.Windows.Forms.DataGridView dgvKullanicilar;
        private System.Windows.Forms.Button btnGeriDon;
    }
}
