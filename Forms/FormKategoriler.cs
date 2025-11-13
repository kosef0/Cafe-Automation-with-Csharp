using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using CafeOtomasyon.Database;
using CafeOtomasyon.Models;

namespace CafeOtomasyon
{
    public partial class FormKategoriler : Form
    {
        private KategoriRepository kategoriRepo;
        private DataGridView dgvKategoriler;
        private TextBox txtAd, txtAciklama;
        private Button btnEkle, btnGuncelle, btnSil, btnTemizle;
        private int? seciliKategoriId = null;

        public FormKategoriler()
        {
            kategoriRepo = new KategoriRepository();
            InitializeComponent();
        }

        private void FormKategoriler_Load(object? sender, EventArgs e)
        {
            SetupUI();
            LoadKategoriler();
        }

        private void SetupUI()
        {
            // Modern header panel
            var pnlHeader = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(1100, 100),
                BackColor = Color.FromArgb(155, 89, 182)
            };
            this.Controls.Add(pnlHeader);

            var lblIcon = new Label
            {
                Text = "📋",
                Font = new Font("Segoe UI Emoji", 36, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(30, 25),
                Size = new Size(60, 60),
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlHeader.Controls.Add(lblIcon);

            var lblBaslik = new Label
            {
                Text = "KATEGORİ YÖNETİMİ",
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
                Size = new Size(1040, 180),
                BackColor = Color.FromArgb(52, 73, 94)
            };
            this.Controls.Add(pnlForm);

            // Kategori Adı
            var lblAd = new Label
            {
                Text = "📝 Kategori Adı:",
                Location = new Point(30, 30),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                AutoSize = true
            };
            pnlForm.Controls.Add(lblAd);

            txtAd = new TextBox
            {
                Location = new Point(200, 27),
                Size = new Size(800, 35),
                Font = new Font("Segoe UI", 12),
                BackColor = Color.FromArgb(44, 62, 80),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            pnlForm.Controls.Add(txtAd);

            // Açıklama
            var lblAciklama = new Label
            {
                Text = "📄 Açıklama:",
                Location = new Point(30, 85),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                AutoSize = true
            };
            pnlForm.Controls.Add(lblAciklama);

            txtAciklama = new TextBox
            {
                Location = new Point(200, 82),
                Size = new Size(800, 70),
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
                Location = new Point(30, 320),
                Size = new Size(1040, 65),
                BackColor = Color.FromArgb(52, 73, 94)
            };
            this.Controls.Add(pnlButtons);

            btnEkle = CreateModernButton("➕ EKLE", Color.FromArgb(46, 204, 113), new Point(15, 10));
            btnEkle.Click += BtnEkle_Click;
            pnlButtons.Controls.Add(btnEkle);

            btnGuncelle = CreateModernButton("✏️ GÜNCELLE", Color.FromArgb(52, 152, 219), new Point(275, 10));
            btnGuncelle.Click += BtnGuncelle_Click;
            pnlButtons.Controls.Add(btnGuncelle);

            btnSil = CreateModernButton("🗑️ SİL", Color.FromArgb(231, 76, 60), new Point(535, 10));
            btnSil.Click += BtnSil_Click;
            pnlButtons.Controls.Add(btnSil);

            btnTemizle = CreateModernButton("🔄 TEMİZLE", Color.FromArgb(127, 140, 141), new Point(795, 10));
            btnTemizle.Click += BtnTemizle_Click;
            pnlButtons.Controls.Add(btnTemizle);

            // Modern DataGridView
            dgvKategoriler = new DataGridView
            {
                Location = new Point(30, 405),
                Size = new Size(1040, 285),
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

            dgvKategoriler.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(155, 89, 182);
            dgvKategoriler.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvKategoriler.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvKategoriler.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvKategoriler.DefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80);
            dgvKategoriler.DefaultCellStyle.ForeColor = Color.White;
            dgvKategoriler.DefaultCellStyle.SelectionBackColor = Color.FromArgb(155, 89, 182);
            dgvKategoriler.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvKategoriler.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);

            dgvKategoriler.SelectionChanged += DgvKategoriler_SelectionChanged;

            this.Controls.Add(dgvKategoriler);
        }

        private Button CreateModernButton(string text, Color bgColor, Point location)
        {
            var btn = new Button
            {
                Text = text,
                Location = location,
                Size = new Size(245, 45),
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
            dgvKategoriler.DataSource = kategoriler;

            if (dgvKategoriler.Columns.Count > 0)
            {
                dgvKategoriler.Columns["Id"].HeaderText = "ID";
                dgvKategoriler.Columns["Id"].Width = 80;
                dgvKategoriler.Columns["Ad"].HeaderText = "Kategori Adı";
                dgvKategoriler.Columns["Aciklama"].HeaderText = "Açıklama";
            }
        }

        private void DgvKategoriler_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvKategoriler.SelectedRows.Count > 0)
            {
                var row = dgvKategoriler.SelectedRows[0];
                seciliKategoriId = Convert.ToInt32(row.Cells["Id"].Value);
                txtAd.Text = row.Cells["Ad"].Value.ToString();
                txtAciklama.Text = row.Cells["Aciklama"].Value.ToString();
            }
        }

        private void BtnEkle_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAd.Text))
            {
                MessageBox.Show("Lütfen kategori adını giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var kategori = new Kategori
            {
                Ad = txtAd.Text.Trim(),
                Aciklama = txtAciklama.Text.Trim()
            };

            try
            {
                kategoriRepo.Add(kategori);
                MessageBox.Show("Kategori başarıyla eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadKategoriler();
                BtnTemizle_Click(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGuncelle_Click(object? sender, EventArgs e)
        {
            if (!seciliKategoriId.HasValue)
            {
                MessageBox.Show("Lütfen güncellenecek kategoriyi seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAd.Text))
            {
                MessageBox.Show("Lütfen kategori adını giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var kategori = new Kategori
            {
                Id = seciliKategoriId.Value,
                Ad = txtAd.Text.Trim(),
                Aciklama = txtAciklama.Text.Trim()
            };

            try
            {
                kategoriRepo.Update(kategori);
                MessageBox.Show("Kategori başarıyla güncellendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadKategoriler();
                BtnTemizle_Click(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSil_Click(object? sender, EventArgs e)
        {
            if (!seciliKategoriId.HasValue)
            {
                MessageBox.Show("Lütfen silinecek kategoriyi seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Bu kategoriyi silmek istediğinizden emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    kategoriRepo.Delete(seciliKategoriId.Value);
                    MessageBox.Show("Kategori başarıyla silindi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadKategoriler();
                    BtnTemizle_Click(null, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}\n\nBu kategoriye bağlı ürünler olabilir.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnTemizle_Click(object? sender, EventArgs e)
        {
            txtAd.Clear();
            txtAciklama.Clear();
            seciliKategoriId = null;
            dgvKategoriler.ClearSelection();
            txtAd.Focus();
        }
    }
}

