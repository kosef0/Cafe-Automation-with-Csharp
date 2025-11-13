using System;
using System.Drawing;
using System.Windows.Forms;
using CafeOtomasyon.Database;
using CafeOtomasyon.Models;

namespace CafeOtomasyon
{
    public partial class FormLogin : Form
    {
        private KullaniciRepository kullaniciRepo;
        private TextBox txtKullaniciAdi = null!;
        private TextBox txtSifre = null!;
        private Button btnGiris = null!;
        private LinkLabel linkKayit = null!;

        public Kullanici? GirisYapanKullanici { get; private set; }

        public FormLogin()
        {
            kullaniciRepo = new KullaniciRepository();
            InitializeComponent();
        }

        private void FormLogin_Load(object? sender, EventArgs e)
        {
            // Modern gradient header panel
            var pnlHeader = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(500, 160),
                BackColor = Color.FromArgb(67, 97, 238)
            };
            this.Controls.Add(pnlHeader);

            // Modern Logo
            var lblIcon = new Label
            {
                Text = "☕",
                Font = new Font("Segoe UI Emoji", 56, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(0, 20),
                Size = new Size(500, 90),
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlHeader.Controls.Add(lblIcon);

            var lblBaslik = new Label
            {
                Text = "CAFE OTOMASYON",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(0, 115),
                Size = new Size(500, 35),
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlHeader.Controls.Add(lblBaslik);

            // Ana içerik paneli - Daha modern ve büyük
            var pnlContent = new Panel
            {
                Location = new Point(30, 190),
                Size = new Size(440, 430),
                BackColor = Color.FromArgb(35, 35, 42)
            };
            this.Controls.Add(pnlContent);

            // Hoş geldiniz başlığı
            var lblWelcome = new Label
            {
                Text = "Hoş Geldiniz! 👋",
                Font = new Font("Segoe UI", 17, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(0, 30),
                Size = new Size(440, 40),
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlContent.Controls.Add(lblWelcome);

            var lblSubtitle = new Label
            {
                Text = "Devam etmek için hesabınıza giriş yapın",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.FromArgb(170, 170, 175),
                Location = new Point(0, 70),
                Size = new Size(440, 25),
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlContent.Controls.Add(lblSubtitle);

            // Input container
            var pnlInputs = new Panel
            {
                Location = new Point(40, 120),
                Size = new Size(360, 210),
                BackColor = Color.Transparent
            };
            pnlContent.Controls.Add(pnlInputs);

            // Kullanıcı Adı bölümü - Modern ikonlarla
            var lblKullaniciAdi = new Label
            {
                Text = "👤 Kullanıcı Adı",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(210, 210, 215),
                Location = new Point(0, 0),
                Size = new Size(360, 28)
            };
            pnlInputs.Controls.Add(lblKullaniciAdi);

            txtKullaniciAdi = new TextBox
            {
                Location = new Point(0, 33),
                Size = new Size(360, 40),
                Font = new Font("Segoe UI", 13),
                BackColor = Color.FromArgb(50, 50, 58),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            pnlInputs.Controls.Add(txtKullaniciAdi);

            // Şifre bölümü
            var lblSifre = new Label
            {
                Text = "🔒 Şifre",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(210, 210, 215),
                Location = new Point(0, 95),
                Size = new Size(360, 28)
            };
            pnlInputs.Controls.Add(lblSifre);

            txtSifre = new TextBox
            {
                Location = new Point(0, 128),
                Size = new Size(360, 40),
                Font = new Font("Segoe UI", 13),
                BackColor = Color.FromArgb(50, 50, 58),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                PasswordChar = '●',
                UseSystemPasswordChar = false
            };
            pnlInputs.Controls.Add(txtSifre);

            // Modern Giriş Butonu - Daha büyük ve belirgin
            btnGiris = new Button
            {
                Text = "🚀 GİRİŞ YAP",
                Location = new Point(40, 345),
                Size = new Size(360, 52),
                BackColor = Color.FromArgb(67, 97, 238),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnGiris.FlatAppearance.BorderSize = 0;
            btnGiris.Click += BtnGiris_Click;
            pnlContent.Controls.Add(btnGiris);

            // Kayıt ol bölümü
            var pnlKayit = new Panel
            {
                Location = new Point(0, 400),
                Size = new Size(440, 30),
                BackColor = Color.Transparent
            };
            pnlContent.Controls.Add(pnlKayit);

            var lblKayitSoru = new Label
            {
                Text = "Hesabınız yok mu?",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.FromArgb(170, 170, 175),
                Location = new Point(105, 5),
                Size = new Size(140, 25),
                TextAlign = ContentAlignment.MiddleRight
            };
            pnlKayit.Controls.Add(lblKayitSoru);

            linkKayit = new LinkLabel
            {
                Text = "Kayıt Ol",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                LinkColor = Color.FromArgb(100, 200, 255),
                ActiveLinkColor = Color.FromArgb(67, 97, 238),
                VisitedLinkColor = Color.FromArgb(100, 200, 255),
                Location = new Point(245, 5),
                Size = new Size(90, 25),
                TextAlign = ContentAlignment.MiddleLeft,
                Cursor = Cursors.Hand
            };
            linkKayit.Click += LinkKayit_Click;
            pnlKayit.Controls.Add(linkKayit);

            // Enter tuşu ile giriş
            txtSifre.KeyPress += (s, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    BtnGiris_Click(null, EventArgs.Empty);
                    e.Handled = true;
                }
            };

            txtKullaniciAdi.KeyPress += (s, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    txtSifre.Focus();
                    e.Handled = true;
                }
            };

            // Modern hover efekti
            btnGiris.MouseEnter += (s, e) => btnGiris.BackColor = Color.FromArgb(87, 117, 255);
            btnGiris.MouseLeave += (s, e) => btnGiris.BackColor = Color.FromArgb(67, 97, 238);

            // Input focus efektleri
            txtKullaniciAdi.Enter += (s, e) => txtKullaniciAdi.BackColor = Color.FromArgb(60, 60, 70);
            txtKullaniciAdi.Leave += (s, e) => txtKullaniciAdi.BackColor = Color.FromArgb(50, 50, 58);
            txtSifre.Enter += (s, e) => txtSifre.BackColor = Color.FromArgb(60, 60, 70);
            txtSifre.Leave += (s, e) => txtSifre.BackColor = Color.FromArgb(50, 50, 58);
        }

        private void BtnGiris_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtKullaniciAdi.Text))
            {
                MessageBox.Show("Lütfen kullanıcı adınızı giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtKullaniciAdi.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSifre.Text))
            {
                MessageBox.Show("Lütfen şifrenizi giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSifre.Focus();
                return;
            }

            try
            {
                var kullanici = kullaniciRepo.Login(txtKullaniciAdi.Text.Trim(), txtSifre.Text);

                if (kullanici != null)
                {
                    GirisYapanKullanici = kullanici;
                    MessageBox.Show($"Hoş geldiniz, {kullanici.AdSoyad}!", "Giriş Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya şifre hatalı!", "Giriş Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSifre.Clear();
                    txtSifre.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LinkKayit_Click(object? sender, EventArgs e)
        {
            var formKayit = new FormKayit();
            if (formKayit.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Kayıt başarılı! Şimdi giriş yapabilirsiniz.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

