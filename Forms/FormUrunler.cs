using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using CafeOtomasyon.Database;
using CafeOtomasyon.Models;

namespace CafeOtomasyon
{
    public partial class FormUrunler : Form
    {
        private UrunRepository urunRepo;
        private KategoriRepository kategoriRepo;
        private DataGridView dgvUrunler;
        private TextBox txtAd, txtFiyat, txtAciklama;
        private ComboBox cmbKategori;
        private CheckBox chkAktif;
        private Button btnEkle, btnGuncelle, btnSil, btnTemizle;
        private int? seciliUrunId = null;

        public FormUrunler()
        {
            urunRepo = new UrunRepository();
            kategoriRepo = new KategoriRepository();
            InitializeComponent();
        }

        private void FormUrunler_Load(object? sender, EventArgs e)
        {
            SetupUI();
            LoadKategoriler();
            LoadUrunler();
        }

        private void SetupUI()
        {
            // Modern header panel
            var pnlHeader = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(1200, 100),
                BackColor = Color.FromArgb(52, 152, 219)
            };
            this.Controls.Add(pnlHeader);

            var lblIcon = new Label
            {
                Text = "📦",
                Font = new Font("Segoe UI Emoji", 36, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(30, 25),
                Size = new Size(60, 60),
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlHeader.Controls.Add(lblIcon);

            var lblBaslik = new Label
            {
                Text = "ÜRÜN YÖNETİMİ",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(100, 30),
                AutoSize = true
            };
            pnlHeader.Controls.Add(lblBaslik);

            // Modern Form Panel
            var pnlForm = new Panel
            {
                Location = new Point(30, 120),
                Size = new Size(1140, 240),
                BackColor = Color.FromArgb(52, 73, 94)
            };
            this.Controls.Add(pnlForm);

            // Ürün Adı
            var lblAd = new Label
            {
                Text = "📝 Ürün Adı:",
                Location = new Point(30, 25),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                AutoSize = true
            };
            pnlForm.Controls.Add(lblAd);

            txtAd = new TextBox
            {
                Location = new Point(180, 23),
                Size = new Size(400, 35),
                Font = new Font("Segoe UI", 12),
                BackColor = Color.FromArgb(44, 62, 80),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            pnlForm.Controls.Add(txtAd);

            // Kategori
            var lblKategori = new Label
            {
                Text = "📂 Kategori:",
                Location = new Point(620, 25),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                AutoSize = true
            };
            pnlForm.Controls.Add(lblKategori);

            cmbKategori = new ComboBox
            {
                Location = new Point(760, 23),
                Size = new Size(350, 35),
                Font = new Font("Segoe UI", 12),
                BackColor = Color.FromArgb(44, 62, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            pnlForm.Controls.Add(cmbKategori);

            // Fiyat
            var lblFiyat = new Label
            {
                Text = "💰 Fiyat (₺):",
                Location = new Point(30, 85),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                AutoSize = true
            };
            pnlForm.Controls.Add(lblFiyat);

            txtFiyat = new TextBox
            {
                Location = new Point(180, 83),
                Size = new Size(200, 35),
                Font = new Font("Segoe UI", 12),
                BackColor = Color.FromArgb(44, 62, 80),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            pnlForm.Controls.Add(txtFiyat);

            // Aktif Checkbox
            chkAktif = new CheckBox
            {
                Text = "✓ Aktif",
                Location = new Point(420, 85),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Checked = true,
                AutoSize = true
            };
            pnlForm.Controls.Add(chkAktif);

            // Açıklama
            var lblAciklama = new Label
            {
                Text = "📄 Açıklama:",
                Location = new Point(30, 145),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                AutoSize = true
            };
            pnlForm.Controls.Add(lblAciklama);

            txtAciklama = new TextBox
            {
                Location = new Point(180, 143),
                Size = new Size(930, 75),
                Font = new Font("Segoe UI", 11),
                BackColor = Color.FromArgb(44, 62, 80),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Multiline = true
            };
            pnlForm.Controls.Add(txtAciklama);

            // Butonlar Panel
            var pnlButtons = new Panel
            {
                Location = new Point(30, 380),
                Size = new Size(1140, 65),
                BackColor = Color.FromArgb(52, 73, 94)
            };
            this.Controls.Add(pnlButtons);

            btnEkle = CreateModernButton("➕ EKLE", Color.FromArgb(46, 204, 113), new Point(20, 10));
            btnEkle.Click += BtnEkle_Click;
            pnlButtons.Controls.Add(btnEkle);

            btnGuncelle = CreateModernButton("✏️ GÜNCELLE", Color.FromArgb(52, 152, 219), new Point(305, 10));
            btnGuncelle.Click += BtnGuncelle_Click;
            pnlButtons.Controls.Add(btnGuncelle);

            btnSil = CreateModernButton("🗑️ SİL", Color.FromArgb(231, 76, 60), new Point(590, 10));
            btnSil.Click += BtnSil_Click;
            pnlButtons.Controls.Add(btnSil);

            btnTemizle = CreateModernButton("🔄 TEMİZLE", Color.FromArgb(127, 140, 141), new Point(875, 10));
            btnTemizle.Click += BtnTemizle_Click;
            pnlButtons.Controls.Add(btnTemizle);

            // Modern DataGridView
            dgvUrunler = new DataGridView
            {
                Location = new Point(30, 465),
                Size = new Size(1140, 285),
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

            dgvUrunler.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 152, 219);
            dgvUrunler.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvUrunler.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvUrunler.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvUrunler.DefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80);
            dgvUrunler.DefaultCellStyle.ForeColor = Color.White;
            dgvUrunler.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dgvUrunler.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvUrunler.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);

            dgvUrunler.SelectionChanged += DgvUrunler_SelectionChanged;

            this.Controls.Add(dgvUrunler);
        }

        private Button CreateModernButton(string text, Color bgColor, Point location)
        {
            var btn = new Button
            {
                Text = text,
                Location = location,
                Size = new Size(270, 45),
                BackColor = bgColor,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            
            Color hoverColor = ControlPaint.Light(bgColor, 0.15f);
            btn.MouseEnter += (s, e) => btn.BackColor = hoverColor;
            btn.MouseLeave += (s, e) => btn.BackColor = bgColor;
            
            return btn;
        }

        private void LoadKategoriler()
        {
            var kategoriler = kategoriRepo.GetAll();
            cmbKategori.DataSource = kategoriler;
            cmbKategori.DisplayMember = "Ad";
            cmbKategori.ValueMember = "Id";
        }

        private void LoadUrunler()
        {
            var urunler = urunRepo.GetAll();
            dgvUrunler.DataSource = urunler;

            if (dgvUrunler.Columns.Count > 0)
            {
                dgvUrunler.Columns["Id"].HeaderText = "ID";
                dgvUrunler.Columns["Id"].Width = 60;
                dgvUrunler.Columns["Ad"].HeaderText = "Ürün Adı";
                dgvUrunler.Columns["KategoriId"].Visible = false;
                dgvUrunler.Columns["KategoriAd"].HeaderText = "Kategori";
                dgvUrunler.Columns["Fiyat"].HeaderText = "Fiyat (₺)";
                dgvUrunler.Columns["Fiyat"].DefaultCellStyle.Format = "N2";
                dgvUrunler.Columns["Aciklama"].HeaderText = "Açıklama";
                dgvUrunler.Columns["Aktif"].HeaderText = "Aktif";
                dgvUrunler.Columns["Aktif"].Width = 80;
            }
        }

        private void DgvUrunler_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvUrunler.SelectedRows.Count > 0)
            {
                var row = dgvUrunler.SelectedRows[0];
                seciliUrunId = Convert.ToInt32(row.Cells["Id"].Value);
                txtAd.Text = row.Cells["Ad"].Value.ToString();
                cmbKategori.SelectedValue = Convert.ToInt32(row.Cells["KategoriId"].Value);
                txtFiyat.Text = Convert.ToDecimal(row.Cells["Fiyat"].Value).ToString("F2");
                txtAciklama.Text = row.Cells["Aciklama"].Value.ToString();
                chkAktif.Checked = Convert.ToBoolean(row.Cells["Aktif"].Value);
            }
        }

        private void BtnEkle_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAd.Text))
            {
                MessageBox.Show("Lütfen ürün adını giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtFiyat.Text, out decimal fiyat) || fiyat <= 0)
            {
                MessageBox.Show("Lütfen geçerli bir fiyat giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var urun = new Urun
            {
                Ad = txtAd.Text.Trim(),
                KategoriId = Convert.ToInt32(cmbKategori.SelectedValue),
                Fiyat = fiyat,
                Aciklama = txtAciklama.Text.Trim(),
                Aktif = chkAktif.Checked
            };

            try
            {
                urunRepo.Add(urun);
                MessageBox.Show("Ürün başarıyla eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadUrunler();
                BtnTemizle_Click(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGuncelle_Click(object? sender, EventArgs e)
        {
            if (!seciliUrunId.HasValue)
            {
                MessageBox.Show("Lütfen güncellenecek ürünü seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAd.Text))
            {
                MessageBox.Show("Lütfen ürün adını giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtFiyat.Text, out decimal fiyat) || fiyat <= 0)
            {
                MessageBox.Show("Lütfen geçerli bir fiyat giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var urun = new Urun
            {
                Id = seciliUrunId.Value,
                Ad = txtAd.Text.Trim(),
                KategoriId = Convert.ToInt32(cmbKategori.SelectedValue),
                Fiyat = fiyat,
                Aciklama = txtAciklama.Text.Trim(),
                Aktif = chkAktif.Checked
            };

            try
            {
                urunRepo.Update(urun);
                MessageBox.Show("Ürün başarıyla güncellendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadUrunler();
                BtnTemizle_Click(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSil_Click(object? sender, EventArgs e)
        {
            if (!seciliUrunId.HasValue)
            {
                MessageBox.Show("Lütfen silinecek ürünü seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Bu ürünü silmek istediğinizden emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    urunRepo.Delete(seciliUrunId.Value);
                    MessageBox.Show("Ürün başarıyla silindi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadUrunler();
                    BtnTemizle_Click(null, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnTemizle_Click(object? sender, EventArgs e)
        {
            txtAd.Clear();
            txtFiyat.Clear();
            txtAciklama.Clear();
            chkAktif.Checked = true;
            if (cmbKategori.Items.Count > 0)
                cmbKategori.SelectedIndex = 0;
            seciliUrunId = null;
            dgvUrunler.ClearSelection();
            txtAd.Focus();
        }
    }
}

