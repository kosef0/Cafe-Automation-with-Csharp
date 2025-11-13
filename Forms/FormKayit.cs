using System;
using System.Drawing;
using System.Windows.Forms;
using CafeOtomasyon.Database;
using CafeOtomasyon.Models;

namespace CafeOtomasyon
{
    public partial class FormKayit : Form
    {
        private KullaniciRepository kullaniciRepo;
        private TextBox txtKullaniciAdi = null!;
        private TextBox txtSifre = null!;
        private TextBox txtSifreTekrar = null!;
        private TextBox txtAdSoyad = null!;
        private TextBox txtEmail = null!;
        private Button btnKayit = null!;

        public FormKayit()
        {
            kullaniciRepo = new KullaniciRepository();
            InitializeComponent();
        }

        private void FormKayit_Load(object? sender, EventArgs e)
        {
            // Modern gradient header panel
            var pnlHeader = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(500, 140),
                BackColor = Color.FromArgb(52, 152, 219)
            };
            this.Controls.Add(pnlHeader);

            // Modern Logo
            var lblIcon = new Label
            {
                Text = "☕",
                Font = new Font("Segoe UI Emoji", 48, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(0, 15),
                Size = new Size(500, 75),
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlHeader.Controls.Add(lblIcon);

            var lblBaslik = new Label
            {
                Text = "YENİ HESAP OLUŞTUR",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(0, 95),
                Size = new Size(500, 35),
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlHeader.Controls.Add(lblBaslik);

            // Ana içerik paneli
            var pnlContent = new Panel
            {
                Location = new Point(30, 170),
                Size = new Size(440, 550),
                BackColor = Color.FromArgb(35, 35, 42)
            };
            this.Controls.Add(pnlContent);

            var lblSubtitle = new Label
            {
                Text = "Lütfen bilgilerinizi eksiksiz doldurun",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.FromArgb(170, 170, 175),
                Location = new Point(0, 20),
                Size = new Size(440, 25),
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlContent.Controls.Add(lblSubtitle);

            // Form alanları container
            var pnlForm = new Panel
            {
                Location = new Point(40, 60),
                Size = new Size(360, 420),
                BackColor = Color.Transparent
            };
            pnlContent.Controls.Add(pnlForm);

            int yPos = 0;

            // Ad Soyad - Modern ikonlarla
            var lblAdSoyad = new Label
            {
                Text = "👤 Ad Soyad",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(210, 210, 215),
                Location = new Point(0, yPos),
                Size = new Size(360, 25)
            };
            pnlForm.Controls.Add(lblAdSoyad);

            txtAdSoyad = new TextBox
            {
                Location = new Point(0, yPos + 28),
                Size = new Size(360, 38),
                Font = new Font("Segoe UI", 12),
                BackColor = Color.FromArgb(50, 50, 58),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            pnlForm.Controls.Add(txtAdSoyad);

            yPos += 78;

            // Email
            var lblEmail = new Label
            {
                Text = "📧 E-posta (Opsiyonel)",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(210, 210, 215),
                Location = new Point(0, yPos),
                Size = new Size(360, 25)
            };
            pnlForm.Controls.Add(lblEmail);

            txtEmail = new TextBox
            {
                Location = new Point(0, yPos + 28),
                Size = new Size(360, 38),
                Font = new Font("Segoe UI", 12),
                BackColor = Color.FromArgb(50, 50, 58),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            pnlForm.Controls.Add(txtEmail);

            yPos += 78;

            // Kullanıcı Adı
            var lblKullaniciAdi = new Label
            {
                Text = "🔑 Kullanıcı Adı",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(210, 210, 215),
                Location = new Point(0, yPos),
                Size = new Size(360, 25)
            };
            pnlForm.Controls.Add(lblKullaniciAdi);

            txtKullaniciAdi = new TextBox
            {
                Location = new Point(0, yPos + 28),
                Size = new Size(360, 38),
                Font = new Font("Segoe UI", 12),
                BackColor = Color.FromArgb(50, 50, 58),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            pnlForm.Controls.Add(txtKullaniciAdi);

            yPos += 78;

            // Şifre
            var lblSifre = new Label
            {
                Text = "🔒 Şifre",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(210, 210, 215),
                Location = new Point(0, yPos),
                Size = new Size(360, 25)
            };
            pnlForm.Controls.Add(lblSifre);

            txtSifre = new TextBox
            {
                Location = new Point(0, yPos + 28),
                Size = new Size(360, 38),
                Font = new Font("Segoe UI", 12),
                BackColor = Color.FromArgb(50, 50, 58),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                PasswordChar = '●',
                UseSystemPasswordChar = false
            };
            pnlForm.Controls.Add(txtSifre);

            yPos += 78;

            // Şifre Tekrar
            var lblSifreTekrar = new Label
            {
                Text = "🔒 Şifre Tekrar",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(210, 210, 215),
                Location = new Point(0, yPos),
                Size = new Size(360, 25)
            };
            pnlForm.Controls.Add(lblSifreTekrar);

            txtSifreTekrar = new TextBox
            {
                Location = new Point(0, yPos + 28),
                Size = new Size(360, 38),
                Font = new Font("Segoe UI", 12),
                BackColor = Color.FromArgb(50, 50, 58),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                PasswordChar = '●',
                UseSystemPasswordChar = false
            };
            pnlForm.Controls.Add(txtSifreTekrar);

            // Modern Kayıt Butonu
            btnKayit = new Button
            {
                Text = "✅ KAYIT OL",
                Location = new Point(40, 490),
                Size = new Size(360, 48),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnKayit.FlatAppearance.BorderSize = 0;
            btnKayit.Click += BtnKayit_Click;
            pnlContent.Controls.Add(btnKayit);

            // Geri Dön Link - Modern
            var linkGeri = new LinkLabel
            {
                Text = "← Giriş sayfasına dön",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                LinkColor = Color.FromArgb(100, 200, 255),
                ActiveLinkColor = Color.FromArgb(52, 152, 219),
                VisitedLinkColor = Color.FromArgb(100, 200, 255),
                Location = new Point(0, 545),
                Size = new Size(440, 25),
                TextAlign = ContentAlignment.MiddleCenter,
                Cursor = Cursors.Hand
            };
            linkGeri.Click += (s, e) => this.Close();
            pnlContent.Controls.Add(linkGeri);

            // Modern hover efektleri
            btnKayit.MouseEnter += (s, e) => btnKayit.BackColor = Color.FromArgb(56, 214, 123);
            btnKayit.MouseLeave += (s, e) => btnKayit.BackColor = Color.FromArgb(46, 204, 113);

            // Input focus efektleri
            txtAdSoyad.Enter += (s, e) => txtAdSoyad.BackColor = Color.FromArgb(60, 60, 70);
            txtAdSoyad.Leave += (s, e) => txtAdSoyad.BackColor = Color.FromArgb(50, 50, 58);
            txtEmail.Enter += (s, e) => txtEmail.BackColor = Color.FromArgb(60, 60, 70);
            txtEmail.Leave += (s, e) => txtEmail.BackColor = Color.FromArgb(50, 50, 58);
            txtKullaniciAdi.Enter += (s, e) => txtKullaniciAdi.BackColor = Color.FromArgb(60, 60, 70);
            txtKullaniciAdi.Leave += (s, e) => txtKullaniciAdi.BackColor = Color.FromArgb(50, 50, 58);
            txtSifre.Enter += (s, e) => txtSifre.BackColor = Color.FromArgb(60, 60, 70);
            txtSifre.Leave += (s, e) => txtSifre.BackColor = Color.FromArgb(50, 50, 58);
            txtSifreTekrar.Enter += (s, e) => txtSifreTekrar.BackColor = Color.FromArgb(60, 60, 70);
            txtSifreTekrar.Leave += (s, e) => txtSifreTekrar.BackColor = Color.FromArgb(50, 50, 58);
        }

        private void BtnKayit_Click(object? sender, EventArgs e)
        {
            // Validasyon
            if (string.IsNullOrWhiteSpace(txtAdSoyad.Text))
            {
                MessageBox.Show("Lütfen ad soyad giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAdSoyad.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtKullaniciAdi.Text))
            {
                MessageBox.Show("Lütfen kullanıcı adı giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtKullaniciAdi.Focus();
                return;
            }

            if (txtKullaniciAdi.Text.Length < 3)
            {
                MessageBox.Show("Kullanıcı adı en az 3 karakter olmalıdır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtKullaniciAdi.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSifre.Text))
            {
                MessageBox.Show("Lütfen şifre giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSifre.Focus();
                return;
            }

            if (txtSifre.Text.Length < 4)
            {
                MessageBox.Show("Şifre en az 4 karakter olmalıdır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSifre.Focus();
                return;
            }

            if (txtSifre.Text != txtSifreTekrar.Text)
            {
                MessageBox.Show("Şifreler eşleşmiyor!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSifreTekrar.Clear();
                txtSifreTekrar.Focus();
                return;
            }

            try
            {
                // Kullanıcı adı kontrolü
                if (kullaniciRepo.KullaniciAdiVarMi(txtKullaniciAdi.Text.Trim()))
                {
                    MessageBox.Show("Bu kullanıcı adı zaten kullanılıyor!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtKullaniciAdi.Focus();
                    return;
                }

                // Yeni kullanıcı oluştur
                var yeniKullanici = new Kullanici
                {
                    AdSoyad = txtAdSoyad.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    KullaniciAdi = txtKullaniciAdi.Text.Trim(),
                    Sifre = txtSifre.Text,
                    Rol = "Personel",
                    KayitTarihi = DateTime.Now,
                    Aktif = true
                };

                kullaniciRepo.Add(yeniKullanici);

                MessageBox.Show("Kayıt başarılı! Şimdi giriş yapabilirsiniz.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

