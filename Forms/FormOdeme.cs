using System;
using System.Drawing;
using System.Windows.Forms;
using CafeOtomasyon.Database;
using CafeOtomasyon.Models;

namespace CafeOtomasyon
{
    public partial class FormOdeme : Form
    {
        private int siparisId;
        private SiparisRepository siparisRepo;
        private MasaRepository masaRepo;
        private Siparis? siparis;

        private DataGridView dgvDetaylar;
        private Label lblToplamTutar, lblAlinanTutar, lblParaUstu;
        private TextBox txtAlinanTutar;
        private Button btnNakit, btnKrediKarti, btnHesapla, btnOdemeAl;

        public FormOdeme(int _siparisId)
        {
            siparisId = _siparisId;
            siparisRepo = new SiparisRepository();
            masaRepo = new MasaRepository();

            InitializeComponent();
        }

        private void FormOdeme_Load(object? sender, EventArgs e)
        {
            SetupUI();
            LoadSiparis();
        }

        private void SetupUI()
        {
            // Modern header panel
            var pnlHeader = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(900, 100),
                BackColor = Color.FromArgb(46, 204, 113)
            };
            this.Controls.Add(pnlHeader);

            var lblIcon = new Label
            {
                Text = "💳",
                Font = new Font("Segoe UI Emoji", 36, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(30, 25),
                Size = new Size(60, 60),
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlHeader.Controls.Add(lblIcon);

            var lblBaslik = new Label
            {
                Text = "ÖDEME İŞLEMİ",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(100, 30),
                AutoSize = true
            };
            pnlHeader.Controls.Add(lblBaslik);

            // Modern Sipariş Detayları Panel
            var pnlDetay = new Panel
            {
                Location = new Point(30, 120),
                Size = new Size(840, 340),
                BackColor = Color.FromArgb(52, 73, 94)
            };
            this.Controls.Add(pnlDetay);

            var lblDetayBaslik = new Label
            {
                Text = "📋 SİPARİŞ DETAYLARI",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(15, 15),
                Size = new Size(810, 40),
                BackColor = Color.FromArgb(52, 152, 219),
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlDetay.Controls.Add(lblDetayBaslik);

            // Modern DataGridView
            dgvDetaylar = new DataGridView
            {
                Location = new Point(15, 65),
                Size = new Size(810, 260),
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

            dgvDetaylar.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 152, 219);
            dgvDetaylar.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDetaylar.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);

            dgvDetaylar.DefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80);
            dgvDetaylar.DefaultCellStyle.ForeColor = Color.White;

            dgvDetaylar.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);

            pnlDetay.Controls.Add(dgvDetaylar);

            // Modern Toplam Tutar Panel
            var pnlToplam = new Panel
            {
                Location = new Point(30, 480),
                Size = new Size(840, 75),
                BackColor = Color.FromArgb(230, 126, 34)
            };
            this.Controls.Add(pnlToplam);

            var lblToplamText = new Label
            {
                Text = "💰 TOPLAM TUTAR:",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(30, 20),
                AutoSize = true
            };
            pnlToplam.Controls.Add(lblToplamText);

            lblToplamTutar = new Label
            {
                Text = "0.00 ₺",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(350, 18),
                Size = new Size(460, 40),
                TextAlign = ContentAlignment.MiddleRight
            };
            pnlToplam.Controls.Add(lblToplamTutar);

            // Modern Ödeme Yöntemi Butonları
            var lblOdemeBaslik = new Label
            {
                Text = "🔸 ÖDEME YÖNTEMİ SEÇİN",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                Location = new Point(30, 575),
                AutoSize = true
            };
            this.Controls.Add(lblOdemeBaslik);

            btnNakit = new Button
            {
                Text = "💵 NAKİT",
                Location = new Point(30, 615),
                Size = new Size(400, 60),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnNakit.FlatAppearance.BorderSize = 0;
            
            Color nakitHover = Color.FromArgb(56, 214, 123);
            btnNakit.MouseEnter += (s, e) => btnNakit.BackColor = nakitHover;
            btnNakit.MouseLeave += (s, e) => btnNakit.BackColor = Color.FromArgb(46, 204, 113);
            
            btnNakit.Click += (s, e) => OdemeYontemiSec("Nakit");
            this.Controls.Add(btnNakit);

            btnKrediKarti = new Button
            {
                Text = "💳 KREDİ KARTI",
                Location = new Point(470, 615),
                Size = new Size(400, 60),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnKrediKarti.FlatAppearance.BorderSize = 0;
            
            Color kartHover = Color.FromArgb(72, 172, 239);
            btnKrediKarti.MouseEnter += (s, e) => btnKrediKarti.BackColor = kartHover;
            btnKrediKarti.MouseLeave += (s, e) => btnKrediKarti.BackColor = Color.FromArgb(52, 152, 219);
            
            btnKrediKarti.Click += (s, e) => OdemeYontemiSec("Kredi Kartı");
            this.Controls.Add(btnKrediKarti);

            // Modern Nakit Ödeme Paneli (başlangıçta gizli)
            var pnlNakit = new Panel
            {
                Location = new Point(30, 695),
                Size = new Size(840, 120),
                BackColor = Color.FromArgb(52, 73, 94),
                Visible = false,
                Name = "pnlNakit"
            };
            this.Controls.Add(pnlNakit);

            var lblAlinan = new Label
            {
                Text = "💵 Alınan Tutar:",
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(25, 25),
                AutoSize = true
            };
            pnlNakit.Controls.Add(lblAlinan);

            txtAlinanTutar = new TextBox
            {
                Location = new Point(200, 22),
                Size = new Size(220, 40),
                Font = new Font("Segoe UI", 14),
                BackColor = Color.FromArgb(44, 62, 80),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            txtAlinanTutar.KeyPress += (s, e) =>
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.' && e.KeyChar != '\b')
                    e.Handled = true;
            };
            pnlNakit.Controls.Add(txtAlinanTutar);

            btnHesapla = new Button
            {
                Text = "🧮 HESAPLA",
                Location = new Point(440, 17),
                Size = new Size(180, 45),
                BackColor = Color.FromArgb(230, 126, 34),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnHesapla.FlatAppearance.BorderSize = 0;
            
            Color hesaplaHover = Color.FromArgb(250, 146, 54);
            btnHesapla.MouseEnter += (s, e) => btnHesapla.BackColor = hesaplaHover;
            btnHesapla.MouseLeave += (s, e) => btnHesapla.BackColor = Color.FromArgb(230, 126, 34);
            
            btnHesapla.Click += BtnHesapla_Click;
            pnlNakit.Controls.Add(btnHesapla);

            var lblParaUstuText = new Label
            {
                Text = "💸 Para Üstü:",
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(25, 75),
                AutoSize = true
            };
            pnlNakit.Controls.Add(lblParaUstuText);

            lblParaUstu = new Label
            {
                Text = "0.00 ₺",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(46, 204, 113),
                Location = new Point(200, 72),
                AutoSize = true
            };
            pnlNakit.Controls.Add(lblParaUstu);

            lblAlinanTutar = new Label(); // Dummy label for reference

            // Modern Ödeme Al Butonu
            btnOdemeAl = new Button
            {
                Text = "✅ ÖDEME AL VE KAPAT",
                Location = new Point(30, 835),
                Size = new Size(840, 65),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Enabled = false
            };
            btnOdemeAl.FlatAppearance.BorderSize = 0;
            
            Color odemeHover = Color.FromArgb(56, 214, 123);
            btnOdemeAl.MouseEnter += (s, e) => { if (btnOdemeAl.Enabled) btnOdemeAl.BackColor = odemeHover; };
            btnOdemeAl.MouseLeave += (s, e) => { if (btnOdemeAl.Enabled) btnOdemeAl.BackColor = Color.FromArgb(46, 204, 113); };
            
            btnOdemeAl.Click += BtnOdemeAl_Click;
            this.Controls.Add(btnOdemeAl);
        }

        private void LoadSiparis()
        {
            siparis = siparisRepo.GetById(siparisId);
            if (siparis == null) return;

            var detaylar = siparisRepo.GetSiparisDetaylar(siparisId);
            dgvDetaylar.DataSource = detaylar;

            if (dgvDetaylar.Columns.Count > 0)
            {
                dgvDetaylar.Columns["Id"].Visible = false;
                dgvDetaylar.Columns["SiparisId"].Visible = false;
                dgvDetaylar.Columns["UrunId"].Visible = false;
                dgvDetaylar.Columns["UrunAd"].HeaderText = "Ürün";
                dgvDetaylar.Columns["Adet"].HeaderText = "Adet";
                dgvDetaylar.Columns["Adet"].Width = 80;
                dgvDetaylar.Columns["BirimFiyat"].HeaderText = "Birim Fiyat";
                dgvDetaylar.Columns["BirimFiyat"].DefaultCellStyle.Format = "N2";
                dgvDetaylar.Columns["ToplamFiyat"].HeaderText = "Toplam";
                dgvDetaylar.Columns["ToplamFiyat"].DefaultCellStyle.Format = "N2";
            }

            lblToplamTutar.Text = $"{siparis.ToplamTutar:F2} ₺";
        }

        private void OdemeYontemiSec(string yontem)
        {
            var pnlNakit = this.Controls.Find("pnlNakit", false)[0] as Panel;
            
            if (yontem == "Nakit")
            {
                if (pnlNakit != null)
                    pnlNakit.Visible = true;
                btnOdemeAl.Enabled = false;
                btnNakit.BackColor = Color.FromArgb(56, 155, 60);
                btnKrediKarti.BackColor = Color.FromArgb(33, 150, 243);
            }
            else
            {
                if (pnlNakit != null)
                    pnlNakit.Visible = false;
                btnOdemeAl.Enabled = true;
                btnKrediKarti.BackColor = Color.FromArgb(13, 130, 223);
                btnNakit.BackColor = Color.FromArgb(76, 175, 80);
            }
        }

        private void BtnHesapla_Click(object? sender, EventArgs e)
        {
            if (siparis == null) return;

            if (decimal.TryParse(txtAlinanTutar.Text.Replace('.', ','), out decimal alinan))
            {
                decimal paraUstu = alinan - siparis.ToplamTutar;
                lblParaUstu.Text = $"{paraUstu:F2} ₺";
                lblParaUstu.ForeColor = paraUstu >= 0 ? Color.FromArgb(76, 175, 80) : Color.FromArgb(244, 67, 54);
                
                btnOdemeAl.Enabled = paraUstu >= 0;
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir tutar giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnOdemeAl_Click(object? sender, EventArgs e)
        {
            if (siparis == null) return;

            try
            {
                // Siparişi kapat
                siparisRepo.CloseSiparis(siparisId);
                
                // Masa durumunu güncelle
                masaRepo.UpdateDurum(siparis.MasaId, "Boş", null);

                MessageBox.Show($"Ödeme başarıyla alındı!\n\nToplam Tutar: {siparis.ToplamTutar:F2} ₺", 
                    "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
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

