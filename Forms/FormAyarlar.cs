using System;
using System.Drawing;
using System.Windows.Forms;
using CafeOtomasyon.Database;
using CafeOtomasyon.Models;
using CafeOtomasyon.Helpers;

namespace CafeOtomasyon
{
    public partial class FormAyarlar : Form
    {
        private KullaniciRepository kullaniciRepo;

        public FormAyarlar()
        {
            kullaniciRepo = new KullaniciRepository();
            InitializeComponent();
        }

        private void FormAyarlar_Load(object? sender, EventArgs e)
        {
            // Modern header panel
            var pnlHeader = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(900, 100),
                BackColor = Color.FromArgb(127, 140, 141)
            };
            this.Controls.Add(pnlHeader);

            var lblIcon = new Label
            {
                Text = "⚙️",
                Font = new Font("Segoe UI Emoji", 36, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(30, 25),
                Size = new Size(60, 60),
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlHeader.Controls.Add(lblIcon);

            var lblBaslik = new Label
            {
                Text = "SİSTEM AYARLARI",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(100, 30),
                AutoSize = true
            };
            pnlHeader.Controls.Add(lblBaslik);

            // Ana içerik paneli
            var pnlContent = new Panel
            {
                Location = new Point(30, 130),
                Size = new Size(840, 520),
                BackColor = Color.FromArgb(52, 73, 94)
            };
            this.Controls.Add(pnlContent);

            // Kullanıcı Yönetimi Bölümü
            var pnlKullanici = new Panel
            {
                Location = new Point(20, 20),
                Size = new Size(800, 220),
                BackColor = Color.FromArgb(44, 62, 80)
            };
            pnlContent.Controls.Add(pnlKullanici);

            var lblKullaniciBaslik = new Label
            {
                Text = "👤 KULLANICI YÖNETİMİ",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 20),
                AutoSize = true
            };
            pnlKullanici.Controls.Add(lblKullaniciBaslik);

            var lblKullaniciAciklama = new Label
            {
                Text = "Kullanıcı bilgilerinizi görüntüleyin ve güncelleyin",
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                ForeColor = Color.FromArgb(189, 195, 199),
                Location = new Point(20, 55),
                AutoSize = true
            };
            pnlKullanici.Controls.Add(lblKullaniciAciklama);

            var btnKullanicilar = new Button
            {
                Text = "👥 Kullanıcıları Görüntüle",
                Location = new Point(20, 100),
                Size = new Size(360, 50),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnKullanicilar.FlatAppearance.BorderSize = 0;
            btnKullanicilar.Click += (s, e) => KullanicilarıGoster();
            btnKullanicilar.MouseEnter += (s, e) => btnKullanicilar.BackColor = Color.FromArgb(72, 172, 239);
            btnKullanicilar.MouseLeave += (s, e) => btnKullanicilar.BackColor = Color.FromArgb(52, 152, 219);
            pnlKullanici.Controls.Add(btnKullanicilar);

            var btnSifreDegistir = new Button
            {
                Text = "🔒 Şifre Değiştir",
                Location = new Point(400, 100),
                Size = new Size(360, 50),
                BackColor = Color.FromArgb(155, 89, 182),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnSifreDegistir.FlatAppearance.BorderSize = 0;
            btnSifreDegistir.Click += (s, e) => SifreDegistir();
            btnSifreDegistir.MouseEnter += (s, e) => btnSifreDegistir.BackColor = Color.FromArgb(175, 109, 202);
            btnSifreDegistir.MouseLeave += (s, e) => btnSifreDegistir.BackColor = Color.FromArgb(155, 89, 182);
            pnlKullanici.Controls.Add(btnSifreDegistir);

            // Veritabanı Bölümü
            var pnlVeritabani = new Panel
            {
                Location = new Point(20, 260),
                Size = new Size(800, 220),
                BackColor = Color.FromArgb(44, 62, 80)
            };
            pnlContent.Controls.Add(pnlVeritabani);

            var lblVtBaslik = new Label
            {
                Text = "💾 VERİTABANI YÖNETİMİ",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 20),
                AutoSize = true
            };
            pnlVeritabani.Controls.Add(lblVtBaslik);

            var lblVtAciklama = new Label
            {
                Text = "Veritabanı işlemlerini yönetin",
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                ForeColor = Color.FromArgb(189, 195, 199),
                Location = new Point(20, 55),
                AutoSize = true
            };
            pnlVeritabani.Controls.Add(lblVtAciklama);

            var btnVeriYedekle = new Button
            {
                Text = "💾 Veritabanı Yedekle",
                Location = new Point(20, 100),
                Size = new Size(360, 50),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnVeriYedekle.FlatAppearance.BorderSize = 0;
            btnVeriYedekle.Click += (s, e) => MessageBox.Show("Veritabanı yedekleme özelliği yakında eklenecek!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnVeriYedekle.MouseEnter += (s, e) => btnVeriYedekle.BackColor = Color.FromArgb(66, 224, 133);
            btnVeriYedekle.MouseLeave += (s, e) => btnVeriYedekle.BackColor = Color.FromArgb(46, 204, 113);
            pnlVeritabani.Controls.Add(btnVeriYedekle);

            var btnHakkinda = new Button
            {
                Text = "ℹ️ Hakkında",
                Location = new Point(400, 100),
                Size = new Size(360, 50),
                BackColor = Color.FromArgb(230, 126, 34),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnHakkinda.FlatAppearance.BorderSize = 0;
            btnHakkinda.Click += (s, e) => MessageBox.Show("Cafe Otomasyon Sistemi v2.0\n\n© 2024 Tüm Hakları Saklıdır\n\nModern ve kullanıcı dostu cafe yönetim sistemi.", "Hakkında", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnHakkinda.MouseEnter += (s, e) => btnHakkinda.BackColor = Color.FromArgb(250, 146, 54);
            btnHakkinda.MouseLeave += (s, e) => btnHakkinda.BackColor = Color.FromArgb(230, 126, 34);
            pnlVeritabani.Controls.Add(btnHakkinda);
        }

        private void KullanicilarıGoster()
        {
            var kullanicilar = kullaniciRepo.GetAll();
            
            var listForm = new Form
            {
                Text = "Kullanıcı Listesi",
                Size = new Size(800, 500),
                StartPosition = FormStartPosition.CenterParent,
                BackColor = Color.FromArgb(44, 62, 80)
            };

            var dgv = new DataGridView
            {
                Location = new Point(20, 20),
                Size = new Size(760, 420),
                BackgroundColor = Color.FromArgb(52, 73, 94),
                ForeColor = Color.White,
                GridColor = Color.FromArgb(127, 140, 141),
                Font = new Font("Segoe UI", 10),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                BorderStyle = BorderStyle.None
            };

            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;

            dgv.Columns.Add("KullaniciAdi", "Kullanıcı Adı");
            dgv.Columns.Add("AdSoyad", "Ad Soyad");
            dgv.Columns.Add("Email", "E-posta");
            dgv.Columns.Add("Rol", "Rol");
            dgv.Columns.Add("Aktif", "Durum");

            foreach (var kullanici in kullanicilar)
            {
                dgv.Rows.Add(
                    kullanici.KullaniciAdi,
                    kullanici.AdSoyad,
                    kullanici.Email ?? "-",
                    kullanici.Rol,
                    kullanici.Aktif ? "Aktif" : "Pasif"
                );
            }

            listForm.Controls.Add(dgv);
            listForm.ShowDialog();
        }

        private void SifreDegistir()
        {
            var sifreForm = new Form
            {
                Text = "Şifre Değiştir",
                Size = new Size(450, 350),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = Color.FromArgb(44, 62, 80)
            };

            var lblMevcut = new Label
            {
                Text = "Mevcut Şifre:",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(30, 30),
                AutoSize = true
            };
            sifreForm.Controls.Add(lblMevcut);

            var txtMevcut = new TextBox
            {
                Location = new Point(30, 60),
                Size = new Size(380, 35),
                Font = new Font("Segoe UI", 12),
                BackColor = Color.FromArgb(52, 73, 94),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                UseSystemPasswordChar = true
            };
            sifreForm.Controls.Add(txtMevcut);

            var lblYeni = new Label
            {
                Text = "Yeni Şifre:",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(30, 110),
                AutoSize = true
            };
            sifreForm.Controls.Add(lblYeni);

            var txtYeni = new TextBox
            {
                Location = new Point(30, 140),
                Size = new Size(380, 35),
                Font = new Font("Segoe UI", 12),
                BackColor = Color.FromArgb(52, 73, 94),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                UseSystemPasswordChar = true
            };
            sifreForm.Controls.Add(txtYeni);

            var lblTekrar = new Label
            {
                Text = "Yeni Şifre Tekrar:",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(30, 190),
                AutoSize = true
            };
            sifreForm.Controls.Add(lblTekrar);

            var txtTekrar = new TextBox
            {
                Location = new Point(30, 220),
                Size = new Size(380, 35),
                Font = new Font("Segoe UI", 12),
                BackColor = Color.FromArgb(52, 73, 94),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                UseSystemPasswordChar = true
            };
            sifreForm.Controls.Add(txtTekrar);

            var btnDegistir = new Button
            {
                Text = "✔️ DEĞİŞTİR",
                Location = new Point(30, 270),
                Size = new Size(180, 45),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnDegistir.FlatAppearance.BorderSize = 0;
            btnDegistir.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtMevcut.Text) || string.IsNullOrWhiteSpace(txtYeni.Text) || string.IsNullOrWhiteSpace(txtTekrar.Text))
                {
                    MessageBox.Show("Lütfen tüm alanları doldurun!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtYeni.Text != txtTekrar.Text)
                {
                    MessageBox.Show("Yeni şifreler eşleşmiyor!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTekrar.Clear();
                    txtTekrar.Focus();
                    return;
                }

                if (txtYeni.Text.Length < 4)
                {
                    MessageBox.Show("Yeni şifre en az 4 karakter olmalıdır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    if (CurrentUser.User == null)
                    {
                        MessageBox.Show("Kullanıcı bilgisi bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Mevcut şifreyi kontrol et
                    var mevcutSifreHash = PasswordHelper.HashPassword(txtMevcut.Text);
                    if (CurrentUser.User.Sifre != mevcutSifreHash)
                    {
                        MessageBox.Show("Mevcut şifre hatalı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtMevcut.Clear();
                        txtMevcut.Focus();
                        return;
                    }

                    // Yeni şifreyi kaydet
                    var kullanici = kullaniciRepo.GetById(CurrentUser.User.Id);
                    if (kullanici != null)
                    {
                        kullanici.Sifre = PasswordHelper.HashPassword(txtYeni.Text);
                        kullaniciRepo.Update(kullanici);
                        
                        // CurrentUser'ı güncelle
                        CurrentUser.User.Sifre = kullanici.Sifre;
                        
                        MessageBox.Show("Şifreniz başarıyla değiştirildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        sifreForm.Close();
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            sifreForm.Controls.Add(btnDegistir);

            var btnIptal = new Button
            {
                Text = "❌ İPTAL",
                Location = new Point(230, 270),
                Size = new Size(180, 45),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnIptal.FlatAppearance.BorderSize = 0;
            btnIptal.Click += (s, e) => sifreForm.Close();
            sifreForm.Controls.Add(btnIptal);

            sifreForm.ShowDialog();
        }
    }
}

