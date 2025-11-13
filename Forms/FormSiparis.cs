using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using CafeOtomasyon.Database;
using CafeOtomasyon.Models;

namespace CafeOtomasyon
{
    public partial class FormSiparis : Form
    {
        private Masa masa;
        private int? siparisId;
        private SiparisRepository siparisRepo;
        private UrunRepository urunRepo;
        private KategoriRepository kategoriRepo;
        private MasaRepository masaRepo;

        private FlowLayoutPanel pnlKategoriler;
        private FlowLayoutPanel pnlUrunler;
        private DataGridView dgvSiparis;
        private Label lblToplamTutar;
        private Button btnSiparisKaydet;

        public FormSiparis(Masa _masa, int? _siparisId = null)
        {
            masa = _masa;
            siparisId = _siparisId;
            siparisRepo = new SiparisRepository();
            urunRepo = new UrunRepository();
            kategoriRepo = new KategoriRepository();
            masaRepo = new MasaRepository();

            InitializeComponent();
        }

        private void FormSiparis_Load(object? sender, EventArgs e)
        {
            this.Text = $"Sipariş - {masa.MasaNo}";
            SetupUI();
            LoadKategoriler();
            
            if (siparisId.HasValue)
            {
                LoadSiparis();
            }
            else
            {
                // Yeni sipariş oluştur
                siparisId = siparisRepo.CreateSiparis(masa.Id);
                masaRepo.UpdateDurum(masa.Id, "Dolu", siparisId);
            }
        }

        private void SetupUI()
        {
            // Modern header panel
            var pnlHeader = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(1400, 100),
                BackColor = Color.FromArgb(241, 196, 15)
            };
            this.Controls.Add(pnlHeader);

            var lblIcon = new Label
            {
                Text = "🍽️",
                Font = new Font("Segoe UI Emoji", 36, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(30, 25),
                Size = new Size(60, 60),
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlHeader.Controls.Add(lblIcon);

            var lblBaslik = new Label
            {
                Text = $"SİPARİŞ YÖNETİMİ - {masa.MasaNo.ToUpper()}",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(100, 30),
                AutoSize = true
            };
            pnlHeader.Controls.Add(lblBaslik);

            // Sol Panel - Kategoriler ve Ürünler (Daha modern ve büyük)
            var pnlSol = new Panel
            {
                Location = new Point(20, 120),
                Size = new Size(820, 670),
                BackColor = Color.FromArgb(52, 73, 94)
            };
            this.Controls.Add(pnlSol);

            // Kategoriler Başlık
            var lblKategoriBaslik = new Label
            {
                Text = "📂 KATEGORİLER",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(15, 15),
                Size = new Size(790, 40),
                BackColor = Color.FromArgb(46, 204, 113),
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlSol.Controls.Add(lblKategoriBaslik);

            // Kategoriler Panel
            pnlKategoriler = new FlowLayoutPanel
            {
                Location = new Point(15, 65),
                Size = new Size(790, 110),
                BackColor = Color.FromArgb(44, 62, 80),
                AutoScroll = true,
                WrapContents = true
            };
            pnlSol.Controls.Add(pnlKategoriler);

            // Ürünler Başlık
            var lblUrunBaslik = new Label
            {
                Text = "🍕 ÜRÜNLER",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(15, 185),
                Size = new Size(790, 40),
                BackColor = Color.FromArgb(52, 152, 219),
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlSol.Controls.Add(lblUrunBaslik);

            // Ürünler Panel (Daha büyük butonlar için)
            pnlUrunler = new FlowLayoutPanel
            {
                Location = new Point(15, 235),
                Size = new Size(790, 420),
                BackColor = Color.FromArgb(44, 62, 80),
                AutoScroll = true,
                WrapContents = true
            };
            pnlSol.Controls.Add(pnlUrunler);

            // Sağ Panel - Sipariş Özeti (Daha modern ve büyük)
            var pnlSag = new Panel
            {
                Location = new Point(860, 120),
                Size = new Size(510, 670),
                BackColor = Color.FromArgb(52, 73, 94)
            };
            this.Controls.Add(pnlSag);

            // Sipariş Başlık
            var lblSiparisBaslik = new Label
            {
                Text = "🧾 SİPARİŞ ÖZETİ",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(15, 15),
                Size = new Size(480, 40),
                BackColor = Color.FromArgb(230, 126, 34),
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlSag.Controls.Add(lblSiparisBaslik);

            // Modern DataGridView
            dgvSiparis = new DataGridView
            {
                Location = new Point(15, 65),
                Size = new Size(480, 480),
                BackgroundColor = Color.FromArgb(44, 62, 80),
                ForeColor = Color.White,
                GridColor = Color.FromArgb(127, 140, 141),
                BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ColumnHeadersHeight = 40,
                RowTemplate = { Height = 35 },
                Font = new Font("Segoe UI", 10)
            };

            dgvSiparis.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(230, 126, 34);
            dgvSiparis.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvSiparis.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);

            dgvSiparis.DefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80);
            dgvSiparis.DefaultCellStyle.ForeColor = Color.White;
            dgvSiparis.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 126, 34);

            dgvSiparis.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);

            // Sil butonu için context menu
            dgvSiparis.MouseClick += (s, e) =>
            {
                if (e.Button == MouseButtons.Right && dgvSiparis.SelectedRows.Count > 0)
                {
                    var contextMenu = new ContextMenuStrip();
                    contextMenu.BackColor = Color.FromArgb(44, 62, 80);
                    contextMenu.ForeColor = Color.White;
                    contextMenu.Font = new Font("Segoe UI", 10);
                    
                    var menuSil = new ToolStripMenuItem("🗑️ Bu ürünü sil");
                    menuSil.Click += (ss, ee) => SiparisDetaySil();
                    contextMenu.Items.Add(menuSil);
                    
                    contextMenu.Show(dgvSiparis, e.Location);
                }
            };

            pnlSag.Controls.Add(dgvSiparis);

            // Modern Toplam Panel
            var pnlToplam = new Panel
            {
                Location = new Point(15, 555),
                Size = new Size(480, 65),
                BackColor = Color.FromArgb(46, 204, 113)
            };
            pnlSag.Controls.Add(pnlToplam);

            var lblToplamText = new Label
            {
                Text = "💵 TOPLAM:",
                Font = new Font("Segoe UI", 15, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 17),
                AutoSize = true
            };
            pnlToplam.Controls.Add(lblToplamText);

            lblToplamTutar = new Label
            {
                Text = "0.00 ₺",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(200, 12),
                Size = new Size(260, 40),
                TextAlign = ContentAlignment.MiddleRight
            };
            pnlToplam.Controls.Add(lblToplamTutar);

            // Modern Kaydet Butonu
            btnSiparisKaydet = new Button
            {
                Text = "✅ KAYDET VE KAPAT",
                Location = new Point(15, 630),
                Size = new Size(480, 50),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnSiparisKaydet.FlatAppearance.BorderSize = 0;
            
            // Modern hover efekti
            Color hoverColor = Color.FromArgb(72, 172, 239);
            btnSiparisKaydet.MouseEnter += (s, e) => btnSiparisKaydet.BackColor = hoverColor;
            btnSiparisKaydet.MouseLeave += (s, e) => btnSiparisKaydet.BackColor = Color.FromArgb(52, 152, 219);
            
            btnSiparisKaydet.Click += (s, e) => this.Close();
            pnlSag.Controls.Add(btnSiparisKaydet);
        }

        private void LoadKategoriler()
        {
            pnlKategoriler.Controls.Clear();
            var kategoriler = kategoriRepo.GetAll();

            foreach (var kategori in kategoriler)
            {
                var btn = new Button
                {
                    Text = kategori.Ad.ToUpper(),
                    Size = new Size(180, 50),
                    BackColor = Color.FromArgb(46, 204, 113),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 11, FontStyle.Bold),
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand,
                    Tag = kategori.Id,
                    Margin = new Padding(8)
                };
                btn.FlatAppearance.BorderSize = 0;
                btn.Click += (s, e) => LoadUrunler(kategori.Id);
                pnlKategoriler.Controls.Add(btn);
            }
        }

        private void LoadUrunler(int kategoriId)
        {
            pnlUrunler.Controls.Clear();
            var urunler = urunRepo.GetByKategori(kategoriId);

            foreach (var urun in urunler)
            {
                var pnl = new Panel
                {
                    Size = new Size(240, 120),
                    BackColor = Color.FromArgb(52, 152, 219),
                    Margin = new Padding(10),
                    Cursor = Cursors.Hand,
                    Tag = urun
                };

                var lblIcon = new Label
                {
                    Text = "🍴",
                    Font = new Font("Segoe UI Emoji", 20, FontStyle.Bold),
                    ForeColor = Color.White,
                    Location = new Point(10, 10),
                    Size = new Size(40, 40),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                pnl.Controls.Add(lblIcon);

                var lblAd = new Label
                {
                    Text = urun.Ad,
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    ForeColor = Color.White,
                    Location = new Point(60, 15),
                    Size = new Size(170, 50),
                    TextAlign = ContentAlignment.TopLeft
                };
                pnl.Controls.Add(lblAd);

                var lblFiyat = new Label
                {
                    Text = $"{urun.Fiyat:F2} ₺",
                    Font = new Font("Segoe UI", 16, FontStyle.Bold),
                    ForeColor = Color.FromArgb(241, 196, 15),
                    Location = new Point(10, 75),
                    Size = new Size(220, 35),
                    TextAlign = ContentAlignment.MiddleRight
                };
                pnl.Controls.Add(lblFiyat);

                // Hover efekti
                Color normalColor = Color.FromArgb(52, 152, 219);
                Color hoverColor = Color.FromArgb(72, 172, 239);
                pnl.MouseEnter += (s, e) => pnl.BackColor = hoverColor;
                pnl.MouseLeave += (s, e) => pnl.BackColor = normalColor;

                pnl.Click += (s, e) => UrunEkle(urun);
                lblIcon.Click += (s, e) => UrunEkle(urun);
                lblAd.Click += (s, e) => UrunEkle(urun);
                lblFiyat.Click += (s, e) => UrunEkle(urun);

                pnlUrunler.Controls.Add(pnl);
            }
        }

        private void UrunEkle(Urun urun)
        {
            // Modern Adet Girişi Dialogu
            var inputForm = new Form
            {
                Text = "Adet Seçin",
                Size = new Size(450, 320),
                StartPosition = FormStartPosition.CenterParent,
                BackColor = Color.FromArgb(44, 62, 80),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            // Modern Header
            var pnlHeader = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(450, 80),
                BackColor = Color.FromArgb(52, 152, 219)
            };
            inputForm.Controls.Add(pnlHeader);

            var lblIcon = new Label
            {
                Text = "🛒",
                Font = new Font("Segoe UI Emoji", 28, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 20),
                Size = new Size(50, 50),
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlHeader.Controls.Add(lblIcon);

            var lblUrunAd = new Label
            {
                Text = urun.Ad.ToUpper(),
                Location = new Point(80, 15),
                Size = new Size(350, 50),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };
            pnlHeader.Controls.Add(lblUrunAd);

            // Adet Label
            var lblAdet = new Label
            {
                Text = "📊 ADET SEÇİN:",
                Location = new Point(30, 100),
                Size = new Size(380, 30),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 13, FontStyle.Bold)
            };
            inputForm.Controls.Add(lblAdet);

            // Modern NumericUpDown
            var numAdet = new NumericUpDown
            {
                Location = new Point(30, 140),
                Size = new Size(380, 45),
                Minimum = 1,
                Maximum = 100,
                Value = 1,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                BackColor = Color.FromArgb(52, 73, 94),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                TextAlign = HorizontalAlignment.Center
            };
            inputForm.Controls.Add(numAdet);

            // Modern Butonlar
            var btnTamam = new Button
            {
                Text = "✅ EKLE",
                Location = new Point(30, 210),
                Size = new Size(180, 55),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                DialogResult = DialogResult.OK
            };
            btnTamam.FlatAppearance.BorderSize = 0;
            
            // Hover efekti
            Color ekleHover = Color.FromArgb(56, 214, 123);
            btnTamam.MouseEnter += (s, e) => btnTamam.BackColor = ekleHover;
            btnTamam.MouseLeave += (s, e) => btnTamam.BackColor = Color.FromArgb(46, 204, 113);
            
            inputForm.Controls.Add(btnTamam);

            var btnIptal = new Button
            {
                Text = "❌ İPTAL",
                Location = new Point(230, 210),
                Size = new Size(180, 55),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                DialogResult = DialogResult.Cancel
            };
            btnIptal.FlatAppearance.BorderSize = 0;
            
            // Hover efekti
            Color iptalHover = Color.FromArgb(251, 96, 80);
            btnIptal.MouseEnter += (s, e) => btnIptal.BackColor = iptalHover;
            btnIptal.MouseLeave += (s, e) => btnIptal.BackColor = Color.FromArgb(231, 76, 60);
            
            inputForm.Controls.Add(btnIptal);

            inputForm.AcceptButton = btnTamam;
            inputForm.CancelButton = btnIptal;

            if (inputForm.ShowDialog() == DialogResult.OK && siparisId.HasValue)
            {
                int adet = (int)numAdet.Value;
                var detay = new SiparisDetay
                {
                    SiparisId = siparisId.Value,
                    UrunId = urun.Id,
                    Adet = adet,
                    BirimFiyat = urun.Fiyat,
                    ToplamFiyat = urun.Fiyat * adet
                };

                try
                {
                    siparisRepo.AddSiparisDetay(detay);
                    LoadSiparis();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadSiparis()
        {
            if (!siparisId.HasValue) return;

            var detaylar = siparisRepo.GetSiparisDetaylar(siparisId.Value);
            dgvSiparis.DataSource = detaylar;

            if (dgvSiparis.Columns.Count > 0)
            {
                dgvSiparis.Columns["Id"].Visible = false;
                dgvSiparis.Columns["SiparisId"].Visible = false;
                dgvSiparis.Columns["UrunId"].Visible = false;
                dgvSiparis.Columns["UrunAd"].HeaderText = "Ürün";
                dgvSiparis.Columns["Adet"].HeaderText = "Adet";
                dgvSiparis.Columns["Adet"].Width = 60;
                dgvSiparis.Columns["BirimFiyat"].HeaderText = "Birim Fiyat";
                dgvSiparis.Columns["BirimFiyat"].DefaultCellStyle.Format = "N2";
                dgvSiparis.Columns["ToplamFiyat"].HeaderText = "Toplam";
                dgvSiparis.Columns["ToplamFiyat"].DefaultCellStyle.Format = "N2";
            }

            var siparis = siparisRepo.GetById(siparisId.Value);
            if (siparis != null)
            {
                lblToplamTutar.Text = $"{siparis.ToplamTutar:F2} ₺";
            }
        }

        private void SiparisDetaySil()
        {
            if (dgvSiparis.SelectedRows.Count > 0 && siparisId.HasValue)
            {
                var row = dgvSiparis.SelectedRows[0];
                int detayId = Convert.ToInt32(row.Cells["Id"].Value);

                var result = MessageBox.Show("Bu ürünü siparişten çıkarmak istediğinizden emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        siparisRepo.DeleteSiparisDetay(detayId, siparisId.Value);
                        LoadSiparis();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}

