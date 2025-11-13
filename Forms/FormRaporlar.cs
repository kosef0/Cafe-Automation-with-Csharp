using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using CafeOtomasyon.Database;
using CafeOtomasyon.Models;

namespace CafeOtomasyon
{
    public partial class FormRaporlar : Form
    {
        private SiparisRepository siparisRepo;
        private DateTimePicker dtpBaslangic, dtpBitis;
        private DataGridView dgvRapor;
        private Label lblToplamSatis, lblToplamTutar, lblOrtalamaTutar;
        private Button btnRaporGetir;

        public FormRaporlar()
        {
            siparisRepo = new SiparisRepository();
            InitializeComponent();
        }

        private void FormRaporlar_Load(object? sender, EventArgs e)
        {
            SetupUI();
            // İlk yükleme
            BtnRaporGetir_Click(null, EventArgs.Empty);
        }

        private void SetupUI()
        {
            // Modern header panel
            var pnlHeader = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(1200, 100),
                BackColor = Color.FromArgb(230, 126, 34)
            };
            this.Controls.Add(pnlHeader);

            var lblIcon = new Label
            {
                Text = "📊",
                Font = new Font("Segoe UI Emoji", 36, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(30, 25),
                Size = new Size(60, 60),
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlHeader.Controls.Add(lblIcon);

            var lblBaslik = new Label
            {
                Text = "SATIŞ RAPORLARI & ANALİTİK",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(100, 30),
                AutoSize = true
            };
            pnlHeader.Controls.Add(lblBaslik);

            // Modern Filtre Panel
            var pnlFiltre = new Panel
            {
                Location = new Point(30, 120),
                Size = new Size(1140, 90),
                BackColor = Color.FromArgb(52, 73, 94)
            };
            this.Controls.Add(pnlFiltre);

            var lblBaslangic = new Label
            {
                Text = "📅 Başlangıç:",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(30, 30),
                AutoSize = true
            };
            pnlFiltre.Controls.Add(lblBaslangic);

            dtpBaslangic = new DateTimePicker
            {
                Location = new Point(180, 28),
                Size = new Size(220, 35),
                Font = new Font("Segoe UI", 11),
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Now.AddMonths(-1)
            };
            pnlFiltre.Controls.Add(dtpBaslangic);

            var lblBitis = new Label
            {
                Text = "📅 Bitiş:",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(440, 30),
                AutoSize = true
            };
            pnlFiltre.Controls.Add(lblBitis);

            dtpBitis = new DateTimePicker
            {
                Location = new Point(540, 28),
                Size = new Size(220, 35),
                Font = new Font("Segoe UI", 11),
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Now
            };
            pnlFiltre.Controls.Add(dtpBitis);

            btnRaporGetir = new Button
            {
                Text = "🔍 RAPOR OLUŞTUR",
                Location = new Point(810, 23),
                Size = new Size(290, 45),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnRaporGetir.FlatAppearance.BorderSize = 0;
            btnRaporGetir.Click += BtnRaporGetir_Click;
            
            // Modern hover efekti
            Color hoverColor = Color.FromArgb(56, 214, 123);
            btnRaporGetir.MouseEnter += (s, e) => btnRaporGetir.BackColor = hoverColor;
            btnRaporGetir.MouseLeave += (s, e) => btnRaporGetir.BackColor = Color.FromArgb(46, 204, 113);
            
            pnlFiltre.Controls.Add(btnRaporGetir);

            // Modern İstatistik Kartları
            // Toplam Satış Kartı
            var pnlToplamSatis = CreateStatCard("📦", "TOPLAM SATIŞ", "0", 
                Color.FromArgb(52, 152, 219), new Point(30, 230));
            this.Controls.Add(pnlToplamSatis);
            lblToplamSatis = (Label)pnlToplamSatis.Controls[2];

            // Toplam Ciro Kartı
            var pnlToplamTutar = CreateStatCard("💰", "TOPLAM CİRO", "0.00 ₺", 
                Color.FromArgb(46, 204, 113), new Point(405, 230));
            this.Controls.Add(pnlToplamTutar);
            lblToplamTutar = (Label)pnlToplamTutar.Controls[2];

            // Ortalama Sepet Kartı
            var pnlOrtalamaTutar = CreateStatCard("📊", "ORTALAMA SEPET", "0.00 ₺", 
                Color.FromArgb(155, 89, 182), new Point(780, 230));
            this.Controls.Add(pnlOrtalamaTutar);
            lblOrtalamaTutar = (Label)pnlOrtalamaTutar.Controls[2];

            // Modern DataGridView
            dgvRapor = new DataGridView
            {
                Location = new Point(30, 370),
                Size = new Size(1140, 380),
                BackgroundColor = Color.FromArgb(44, 62, 80),
                ForeColor = Color.White,
                GridColor = Color.FromArgb(127, 140, 141),
                BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ColumnHeadersHeight = 45,
                RowTemplate = { Height = 40 },
                Font = new Font("Segoe UI", 10)
            };

            dgvRapor.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(230, 126, 34);
            dgvRapor.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvRapor.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvRapor.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvRapor.DefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80);
            dgvRapor.DefaultCellStyle.ForeColor = Color.White;
            dgvRapor.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 126, 34);
            dgvRapor.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvRapor.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);

            this.Controls.Add(dgvRapor);
        }

        private Panel CreateStatCard(string icon, string title, string value, Color bgColor, Point location)
        {
            var panel = new Panel
            {
                Location = location,
                Size = new Size(355, 120),
                BackColor = bgColor
            };

            var lblIcon = new Label
            {
                Text = icon,
                Font = new Font("Segoe UI Emoji", 32, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(15, 25),
                Size = new Size(70, 70),
                TextAlign = ContentAlignment.MiddleCenter
            };
            panel.Controls.Add(lblIcon);

            var lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(236, 240, 241),
                Location = new Point(100, 20),
                Size = new Size(240, 25)
            };
            panel.Controls.Add(lblTitle);

            var lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(100, 50),
                Size = new Size(240, 50),
                TextAlign = ContentAlignment.MiddleLeft
            };
            panel.Controls.Add(lblValue);

            return panel;
        }


        private void BtnRaporGetir_Click(object? sender, EventArgs e)
        {
            try
            {
                DateTime baslangic = dtpBaslangic.Value.Date;
                DateTime bitis = dtpBitis.Value.Date.AddDays(1).AddSeconds(-1);

                var siparisler = siparisRepo.GetSiparislerByDateRange(baslangic, bitis);
                dgvRapor.DataSource = siparisler;

                if (dgvRapor.Columns.Count > 0)
                {
                    dgvRapor.Columns["Id"].HeaderText = "Sipariş No";
                    dgvRapor.Columns["Id"].Width = 100;
                    dgvRapor.Columns["MasaId"].Visible = false;
                    dgvRapor.Columns["MasaNo"].HeaderText = "Masa";
                    dgvRapor.Columns["MasaNo"].Width = 120;
                    dgvRapor.Columns["Tarih"].HeaderText = "Açılış Tarihi";
                    dgvRapor.Columns["Tarih"].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";
                    dgvRapor.Columns["ToplamTutar"].HeaderText = "Tutar (₺)";
                    dgvRapor.Columns["ToplamTutar"].DefaultCellStyle.Format = "N2";
                    dgvRapor.Columns["ToplamTutar"].Width = 120;
                    dgvRapor.Columns["Durum"].HeaderText = "Durum";
                    dgvRapor.Columns["Durum"].Width = 100;
                    dgvRapor.Columns["KapanisTarihi"].HeaderText = "Kapanış Tarihi";
                    dgvRapor.Columns["KapanisTarihi"].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";
                }

                // Özet bilgileri hesapla
                lblToplamSatis.Text = siparisler.Count.ToString();
                decimal toplamTutar = siparisler.Sum(s => s.ToplamTutar);
                lblToplamTutar.Text = $"{toplamTutar:F2} ₺";
                decimal ortalamaTutar = siparisler.Count > 0 ? toplamTutar / siparisler.Count : 0;
                lblOrtalamaTutar.Text = $"{ortalamaTutar:F2} ₺";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvRapor_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvRapor.SelectedRows.Count > 0)
            {
                var row = dgvRapor.SelectedRows[0];
                int siparisId = Convert.ToInt32(row.Cells["Id"].Value);
                string masaNo = row.Cells["MasaNo"].Value.ToString() ?? "";
                
                // Sipariş detaylarını göster
                var detaylar = siparisRepo.GetSiparisDetaylar(siparisId);
                
                var detayForm = new Form
                {
                    Text = $"Sipariş Detayları - {masaNo}",
                    Size = new Size(700, 500),
                    StartPosition = FormStartPosition.CenterParent,
                    BackColor = Color.FromArgb(45, 45, 48),
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    MaximizeBox = false,
                    MinimizeBox = false
                };

                var lblBaslik = new Label
                {
                    Text = $"SİPARİŞ DETAYLARI - {masaNo}",
                    Font = new Font("Segoe UI", 14, FontStyle.Bold),
                    ForeColor = Color.FromArgb(255, 152, 0),
                    Location = new Point(20, 20),
                    Size = new Size(640, 30)
                };
                detayForm.Controls.Add(lblBaslik);

                var dgvDetay = new DataGridView
                {
                    Location = new Point(20, 60),
                    Size = new Size(640, 350),
                    BackgroundColor = Color.FromArgb(60, 60, 65),
                    ForeColor = Color.White,
                    GridColor = Color.FromArgb(80, 80, 85),
                    BorderStyle = BorderStyle.None,
                    AllowUserToAddRows = false,
                    AllowUserToDeleteRows = false,
                    ReadOnly = true,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                    DataSource = detaylar
                };

                dgvDetay.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 152, 0);
                dgvDetay.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvDetay.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

                dgvDetay.DefaultCellStyle.BackColor = Color.FromArgb(60, 60, 65);
                dgvDetay.DefaultCellStyle.ForeColor = Color.White;
                dgvDetay.DefaultCellStyle.Font = new Font("Segoe UI", 9);

                if (dgvDetay.Columns.Count > 0)
                {
                    dgvDetay.Columns["Id"].Visible = false;
                    dgvDetay.Columns["SiparisId"].Visible = false;
                    dgvDetay.Columns["UrunId"].Visible = false;
                    dgvDetay.Columns["UrunAd"].HeaderText = "Ürün";
                    dgvDetay.Columns["Adet"].HeaderText = "Adet";
                    dgvDetay.Columns["Adet"].Width = 80;
                    dgvDetay.Columns["BirimFiyat"].HeaderText = "Birim Fiyat";
                    dgvDetay.Columns["BirimFiyat"].DefaultCellStyle.Format = "N2";
                    dgvDetay.Columns["ToplamFiyat"].HeaderText = "Toplam";
                    dgvDetay.Columns["ToplamFiyat"].DefaultCellStyle.Format = "N2";
                }

                detayForm.Controls.Add(dgvDetay);

                var btnKapat = new Button
                {
                    Text = "Kapat",
                    Location = new Point(520, 420),
                    Size = new Size(140, 35),
                    BackColor = Color.FromArgb(244, 67, 54),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand,
                    DialogResult = DialogResult.OK
                };
                btnKapat.FlatAppearance.BorderSize = 0;
                detayForm.Controls.Add(btnKapat);

                detayForm.ShowDialog();
            }
        }
    }
}

