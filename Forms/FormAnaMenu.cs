using System;
using System.Drawing;
using System.Windows.Forms;
using CafeOtomasyon.Helpers;

namespace CafeOtomasyon
{
    public partial class FormAnaMenu : Form
    {
        public FormAnaMenu()
        {
            InitializeComponent();
        }

        private void FormAnaMenu_Load(object? sender, EventArgs e)
        {
            // Modern header panel
            var pnlHeader = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(1200, 130),
                BackColor = Color.FromArgb(44, 62, 80)
            };
            this.Controls.Add(pnlHeader);

            // Logo  başlık
            var lblIcon = new Label
            {
                Text = "☕",
                Font = new Font("Segoe UI Emoji", 46, FontStyle.Bold),
                ForeColor = Color.FromArgb(241, 196, 15),
                Location = new Point(40, 30),
                Size = new Size(75, 75),
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlHeader.Controls.Add(lblIcon);

            var lblBaslik = new Label
            {
                Text = "CAFE OTOMASYON SİSTEMİ",
                Font = new Font("Segoe UI", 26, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(125, 35),
                Size = new Size(700, 45),
                TextAlign = ContentAlignment.MiddleLeft
            };
            pnlHeader.Controls.Add(lblBaslik);

            var lblAltBaslik = new Label
            {
                Text = "Modern Yönetim Paneli",
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                ForeColor = Color.FromArgb(189, 195, 199),
                Location = new Point(125, 82),
                Size = new Size(700, 25),
                TextAlign = ContentAlignment.MiddleLeft
            };
            pnlHeader.Controls.Add(lblAltBaslik);

            // Saat
            var lblDateTime = new Label
            {
                Text = DateTime.Now.ToString("dd MMMM yyyy HH:mm"),
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                ForeColor = Color.FromArgb(189, 195, 199),
                Location = new Point(900, 50),
                Size = new Size(260, 30),
                TextAlign = ContentAlignment.MiddleRight
            };
            pnlHeader.Controls.Add(lblDateTime);

            // Hoşgeldin paneli
            var pnlWelcome = new Panel
            {
                Location = new Point(30, 155),
                Size = new Size(1140, 55),
                BackColor = Color.FromArgb(52, 73, 94)
            };
            this.Controls.Add(pnlWelcome);

            var lblWelcome = new Label
            {
                Text = "🎯 Hızlı Erişim - İşleminizi Seçin",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(0, 0),
                Size = new Size(1140, 55),
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlWelcome.Controls.Add(lblWelcome);

            // Modern buton grid - Daha büyük ve şık
            int buttonWidth = 360;
            int buttonHeight = 135;
            int spacing = 30;
            int startX = 30;
            int startY = 235;

            // Modern renk paleti ile butonlar
            var buttons = new[]
            {
                ("🪑", "MASA İŞLEMLERİ", Color.FromArgb(46, 204, 113), new Action(() => OpenMasalar())),
                ("📦", "ÜRÜN YÖNETİMİ", Color.FromArgb(52, 152, 219), new Action(() => OpenUrunler())),
                ("📋", "KATEGORİLER", Color.FromArgb(155, 89, 182), new Action(() => OpenKategoriler())),
                ("📊", "SATIŞ RAPORLARI", Color.FromArgb(230, 126, 34), new Action(() => OpenRaporlar())),
                ("⚙️", "AYARLAR", Color.FromArgb(127, 140, 141), new Action(() => OpenAyarlar())),
                ("🚪", "ÇIKIŞ", Color.FromArgb(231, 76, 60), new Action(() => {
                    if (MessageBox.Show("Uygulamadan çıkmak istediğinize emin misiniz?", "Çıkış Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        CurrentUser.Logout();
                        Application.Exit();
                    }
                }))
            };

            for (int i = 0; i < buttons.Length; i++)
            {
                int row = i / 3;
                int col = i % 3;
                
                var buttonInfo = buttons[i];
                
                var btn = CreateMenuButton(
                    buttonInfo.Item1, 
                    buttonInfo.Item2, 
                    buttonInfo.Item3,
                    new Point(startX + (col * (buttonWidth + spacing)), startY + (row * (buttonHeight + spacing))),
                    new Size(buttonWidth, buttonHeight)
                );
                
                btn.Click += (s, e) => buttonInfo.Item4();
                this.Controls.Add(btn);
            }

            // Modern footer
            var pnlFooter = new Panel
            {
                Location = new Point(0, 750),
                Size = new Size(1200, 50),
                BackColor = Color.FromArgb(44, 62, 80)
            };
            this.Controls.Add(pnlFooter);

            var lblFooter = new Label
            {
                Text = "© 2024 Cafe Otomasyon Sistemi | v2.0 | Tüm Hakları Saklıdır",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.FromArgb(189, 195, 199),
                Location = new Point(0, 0),
                Size = new Size(1200, 50),
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlFooter.Controls.Add(lblFooter);
        }

        private Button CreateMenuButton(string icon, string text, Color bgColor, Point location, Size size)
        {
            var btn = new Button
            {
                Text = $"{icon}\n\n{text}",
                Location = location,
                Size = size,
                BackColor = bgColor,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleCenter
            };

            btn.FlatAppearance.BorderSize = 0;

            // Modern hover efekti - Daha parlak ve belirgin
            Color hoverColor = ControlPaint.Light(bgColor, 0.15f);
            Color clickColor = ControlPaint.Dark(bgColor, 0.05f);
            
            btn.MouseEnter += (s, e) => 
            {
                btn.BackColor = hoverColor;
                btn.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            };
            
            btn.MouseLeave += (s, e) => 
            {
                btn.BackColor = bgColor;
                btn.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            };
            
            btn.MouseDown += (s, e) => btn.BackColor = clickColor;
            btn.MouseUp += (s, e) => btn.BackColor = hoverColor;

            return btn;
        }

        private void OpenMasalar()
        {
            var form = new FormMasalar();
            form.ShowDialog();
        }

        private void OpenUrunler()
        {
            var form = new FormUrunler();
            form.ShowDialog();
        }

        private void OpenKategoriler()
        {
            var form = new FormKategoriler();
            form.ShowDialog();
        }

        private void OpenRaporlar()
        {
            var form = new FormRaporlar();
            form.ShowDialog();
        }

        private void OpenAyarlar()
        {
            var form = new FormAyarlar();
            form.ShowDialog();
        }
    }
}

