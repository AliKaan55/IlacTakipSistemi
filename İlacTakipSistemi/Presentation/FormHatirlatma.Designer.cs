namespace İlacTakipSistemi.Presentation
{
    partial class FormHatirlatma
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
            cmbIlac = new System.Windows.Forms.ComboBox();
            cmbKullanici = new System.Windows.Forms.ComboBox();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            dtpTarihSaat = new System.Windows.Forms.DateTimePicker();
            chkTekrarla = new System.Windows.Forms.CheckBox();
            label4 = new System.Windows.Forms.Label();
            numTekrarAraligi = new System.Windows.Forms.NumericUpDown();
            label5 = new System.Windows.Forms.Label();
            btnHatirlatmaSil = new System.Windows.Forms.Button();
            btnHatirlatmaEkle = new System.Windows.Forms.Button();
            btnHatirlatmaDuzenle = new System.Windows.Forms.Button();
            dgvHatirlatmalar = new System.Windows.Forms.DataGridView();
            btnGeriDon = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)numTekrarAraligi).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvHatirlatmalar).BeginInit();
            SuspendLayout();

            // label1 - İlaç
            label1.Location = new System.Drawing.Point(20, 20);
            label1.Size = new System.Drawing.Size(80, 25);
            label1.Text = "İlaç Seç:";

            // cmbIlac
            cmbIlac.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbIlac.FormattingEnabled = true;
            cmbIlac.Location = new System.Drawing.Point(140, 18);
            cmbIlac.Size = new System.Drawing.Size(200, 23);

            // label2 - Kullanıcı
            label2.Location = new System.Drawing.Point(20, 60);
            label2.Size = new System.Drawing.Size(100, 25);
            label2.Text = "Kullanıcı Seç:";

            // cmbKullanici
            cmbKullanici.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbKullanici.FormattingEnabled = true;
            cmbKullanici.Location = new System.Drawing.Point(140, 58);
            cmbKullanici.Size = new System.Drawing.Size(200, 23);

            // label3 - Tarih/Saat
            label3.Location = new System.Drawing.Point(20, 100);
            label3.Size = new System.Drawing.Size(110, 25);
            label3.Text = "Tarih ve Saat:";

            // dtpTarihSaat - Tarih + Saat birlikte
            dtpTarihSaat.Location = new System.Drawing.Point(140, 98);
            dtpTarihSaat.Size = new System.Drawing.Size(180, 23);
            dtpTarihSaat.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            dtpTarihSaat.CustomFormat = "dd.MM.yyyy HH:mm";
            dtpTarihSaat.ShowUpDown = false;
            dtpTarihSaat.Value = System.DateTime.Now.AddHours(1);

            // chkTekrarla
            chkTekrarla.Location = new System.Drawing.Point(140, 130);
            chkTekrarla.Size = new System.Drawing.Size(150, 24);
            chkTekrarla.Text = "Tekrarlayan hatırlatma";
            chkTekrarla.UseVisualStyleBackColor = true;
            chkTekrarla.CheckedChanged += chkTekrarla_CheckedChanged;

            // label4 - "Her"
            label4.Location = new System.Drawing.Point(20, 160);
            label4.Size = new System.Drawing.Size(30, 25);
            label4.Text = "Her";

            // numTekrarAraligi
            numTekrarAraligi.Location = new System.Drawing.Point(55, 158);
            numTekrarAraligi.Size = new System.Drawing.Size(60, 23);
            numTekrarAraligi.Minimum = 1;
            numTekrarAraligi.Maximum = 365;
            numTekrarAraligi.Value = 1;
            numTekrarAraligi.Enabled = false;

            // label5 - "günde bir"
            label5.Location = new System.Drawing.Point(120, 160);
            label5.Size = new System.Drawing.Size(100, 25);
            label5.Text = "günde bir";

            // btnHatirlatmaEkle
            btnHatirlatmaEkle.Location = new System.Drawing.Point(140, 195);
            btnHatirlatmaEkle.Size = new System.Drawing.Size(130, 30);
            btnHatirlatmaEkle.Text = "Hatırlatma Ekle";
            btnHatirlatmaEkle.UseVisualStyleBackColor = true;
            btnHatirlatmaEkle.Click += btnHatirlatmaEkle_Click;

            // btnHatirlatmaDuzenle
            btnHatirlatmaDuzenle.Location = new System.Drawing.Point(280, 195);
            btnHatirlatmaDuzenle.Size = new System.Drawing.Size(130, 30);
            btnHatirlatmaDuzenle.Text = "Düzenle";
            btnHatirlatmaDuzenle.UseVisualStyleBackColor = true;
            btnHatirlatmaDuzenle.Enabled = false;
            btnHatirlatmaDuzenle.Click += btnHatirlatmaDuzenle_Click;

            // btnHatirlatmaSil
            btnHatirlatmaSil.Location = new System.Drawing.Point(420, 195);
            btnHatirlatmaSil.Size = new System.Drawing.Size(130, 30);
            btnHatirlatmaSil.Text = "Hatırlatıcı Kaldır";
            btnHatirlatmaSil.UseVisualStyleBackColor = true;
            btnHatirlatmaSil.BackColor = System.Drawing.Color.IndianRed;
            btnHatirlatmaSil.ForeColor = System.Drawing.Color.White;
            btnHatirlatmaSil.Click += btnHatirlatmaSil_Click;

            // dgvHatirlatmalar
            dgvHatirlatmalar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvHatirlatmalar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHatirlatmalar.Location = new System.Drawing.Point(20, 240);
            dgvHatirlatmalar.Size = new System.Drawing.Size(755, 210);
            dgvHatirlatmalar.ReadOnly = true;
            dgvHatirlatmalar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvHatirlatmalar.MultiSelect = false;
            dgvHatirlatmalar.SelectionChanged += dgvHatirlatmalar_SelectionChanged;

            // btnGeriDon
            btnGeriDon.Location = new System.Drawing.Point(677, 460);
            btnGeriDon.Size = new System.Drawing.Size(111, 41);
            btnGeriDon.Text = "Geri Dön";
            btnGeriDon.UseVisualStyleBackColor = true;
            btnGeriDon.Click += btnGeriDon_Click;

            // FormHatirlatma
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 515);
            Text = "Hatırlatma Yönetimi";
            Controls.Add(label1);
            Controls.Add(cmbIlac);
            Controls.Add(label2);
            Controls.Add(cmbKullanici);
            Controls.Add(label3);
            Controls.Add(dtpTarihSaat);
            Controls.Add(chkTekrarla);
            Controls.Add(label4);
            Controls.Add(numTekrarAraligi);
            Controls.Add(label5);
            Controls.Add(btnHatirlatmaEkle);
            Controls.Add(btnHatirlatmaDuzenle);
            Controls.Add(btnHatirlatmaSil);
            Controls.Add(dgvHatirlatmalar);
            Controls.Add(btnGeriDon);

            ((System.ComponentModel.ISupportInitialize)numTekrarAraligi).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvHatirlatmalar).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbIlac;
        private System.Windows.Forms.ComboBox cmbKullanici;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpTarihSaat;
        private System.Windows.Forms.CheckBox chkTekrarla;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numTekrarAraligi;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnHatirlatmaEkle;
        private System.Windows.Forms.Button btnHatirlatmaDuzenle;
        private System.Windows.Forms.Button btnHatirlatmaSil;
        private System.Windows.Forms.DataGridView dgvHatirlatmalar;
        private System.Windows.Forms.Button btnGeriDon;
    }
}
