using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using CafeOtomasyon.Database;
using CafeOtomasyon.Models;

namespace CafeOtomasyon
{
    public partial class FormMasalar : Form
    {
        private MasaRepository masaRepo;
        private Panel pnlMasalar;

        public FormMasalar()
        {
            masaRepo = new MasaRepository();
            InitializeComponent();
        }

        private void FormMasalar_Load(object? sender, EventArgs e)
        {
            SetupUI();
            LoadMasalar();
        }

        private void SetupUI()
        {
            // Modern header panel
            var pnlHeader = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(1300, 100),
                BackColor = Color.FromArgb(46, 204, 113)
            };
            this.Controls.Add(pnlHeader);

            var lblIcon = new Label
            {
                Text = "🪑",
                Font = new Font("Segoe UI Emoji", 36, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(30, 25),
                Size = new Size(60, 60),
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlHeader.Controls.Add(lblIcon);

            var lblBaslik = new Label
            {
                Text = "MASA İŞLEMLERİ",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(100, 30),
                AutoSize = true
            };
            pnlHeader.Controls.Add(lblBaslik);

            // Yeni Masa ve Masa Sil butonları
            var btnYeniMasa = new Button
            {
                Text = "➕ YENİ MASA",
                Location = new Point(1000, 30),
                Size = new Size(130, 45),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnYeniMasa.FlatAppearance.BorderSize = 0;
            btnYeniMasa.Click += BtnYeniMasa_Click;
            btnYeniMasa.MouseEnter += (s, e) => btnYeniMasa.BackColor = Color.FromArgb(72, 172, 239);
            btnYeniMasa.MouseLeave += (s, e) => btnYeniMasa.BackColor = Color.FromArgb(52, 152, 219);
            pnlHeader.Controls.Add(btnYeniMasa);

            var btnMasaSil = new Button
            {
                Text = "🗑️ MASA SİL",
                Location = new Point(1145, 30),
                Size = new Size(130, 45),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnMasaSil.FlatAppearance.BorderSize = 0;
            btnMasaSil.Click += BtnMasaSil_Click;
            btnMasaSil.MouseEnter += (s, e) => btnMasaSil.BackColor = Color.FromArgb(251, 96, 80);
            btnMasaSil.MouseLeave += (s, e) => btnMasaSil.BackColor = Color.FromArgb(231, 76, 60);
            pnlHeader.Controls.Add(btnMasaSil);

            // Modern bilgi paneli
            var pnlBilgi = new Panel
            {
                Location = new Point(20, 120),
                Size = new Size(1260, 70),
                BackColor = Color.FromArgb(52, 73, 94)
            };
            this.Controls.Add(pnlBilgi);

            var lblInfo = new Label
            {
                Text = "📍 Durum Göstergeleri:",
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(25, 20),
                AutoSize = true
            };
            pnlBilgi.Controls.Add(lblInfo);

            var lblBosInfo = new Label
            {
                Text = "🟢 Boş",
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = Color.FromArgb(46, 204, 113),
                Location = new Point(280, 20),
                AutoSize = true
            };
            pnlBilgi.Controls.Add(lblBosInfo);

            var lblDoluInfo = new Label
            {
                Text = "🔴 Dolu",
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = Color.FromArgb(231, 76, 60),
                Location = new Point(400, 20),
                AutoSize = true
            };
            pnlBilgi.Controls.Add(lblDoluInfo);

            var lblRezerveInfo = new Label
            {
                Text = "🟡 Rezerve",
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = Color.FromArgb(241, 196, 15),
                Location = new Point(520, 20),
                AutoSize = true
            };
            pnlBilgi.Controls.Add(lblRezerveInfo);

            var lblYardim = new Label
            {
                Text = "💡 İpucu: Masa üzerine tıklayarak işlem yapabilirsiniz",
                Font = new Font("Segoe UI", 11, FontStyle.Italic),
                ForeColor = Color.FromArgb(189, 195, 199),
                Location = new Point(700, 23),
                AutoSize = true
            };
            pnlBilgi.Controls.Add(lblYardim);

            // Modern masalar paneli
            pnlMasalar = new Panel
            {
                Location = new Point(20, 210),
                Size = new Size(1260, 550),
                BackColor = Color.FromArgb(44, 62, 80),
                AutoScroll = true
            };
            this.Controls.Add(pnlMasalar);
        }

        private void LoadMasalar()
        {
            pnlMasalar.Controls.Clear();
            var masalar = masaRepo.GetAll();

            int col = 0;
            int row = 0;
            int buttonWidth = 200;
            int buttonHeight = 150;
            int spacing = 25;
            int maxCols = 5;

            foreach (var masa in masalar)
            {
                var btnMasa = CreateMasaButton(masa, 
                    new Point(20 + (col * (buttonWidth + spacing)), 20 + (row * (buttonHeight + spacing))),
                    new Size(buttonWidth, buttonHeight));
                
                pnlMasalar.Controls.Add(btnMasa);

                col++;
                if (col >= maxCols)
                {
                    col = 0;
                    row++;
                }
            }
        }

        private Button CreateMasaButton(Masa masa, Point location, Size size)
        {
            Color bgColor;
            string icon;
            
            switch (masa.Durum)
            {
                case "Boş":
                    bgColor = Color.FromArgb(46, 204, 113);
                    icon = "🟢";
                    break;
                case "Dolu":
                    bgColor = Color.FromArgb(231, 76, 60);
                    icon = "🔴";
                    break;
                case "Rezerve":
                    bgColor = Color.FromArgb(241, 196, 15);
                    icon = "🟡";
                    break;
                default:
                    bgColor = Color.FromArgb(127, 140, 141);
                    icon = "⚪";
                    break;
            }

            var btn = new Button
            {
                Text = $"{icon}\n\n{masa.MasaNo}\n\n{masa.Durum}",
                Location = location,
                Size = size,
                BackColor = bgColor,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleCenter,
                Tag = masa
            };

            btn.FlatAppearance.BorderSize = 0;

            // Modern hover efekti
            Color hoverColor = ControlPaint.Light(bgColor, 0.15f);
            btn.MouseEnter += (s, e) => 
            {
                btn.BackColor = hoverColor;
                btn.Font = new Font("Segoe UI", 15, FontStyle.Bold);
            };
            btn.MouseLeave += (s, e) => 
            {
                btn.BackColor = bgColor;
                btn.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            };

            btn.Click += (s, e) => MasaTiklandi(masa);

            return btn;
        }

        private void BtnYeniMasa_Click(object? sender, EventArgs e)
        {
            // Yeni masa numarası oluştur
            var masalar = masaRepo.GetAll();
            int yeniMasaNo = masalar.Count > 0 ? masalar.Max(m => int.Parse(m.MasaNo.Replace("Masa ", ""))) + 1 : 1;

            var dialogResult = MessageBox.Show(
                $"'{yeniMasaNo}' numaralı yeni masa eklensin mi?",
                "Yeni Masa Ekle",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    var yeniMasa = new Masa
                    {
                        MasaNo = $"Masa {yeniMasaNo}",
                        Kapasite = 4,
                        Durum = "Boş",
                        Aktif = true
                    };

                    masaRepo.Add(yeniMasa);
                    MessageBox.Show($"'{yeniMasa.MasaNo}' başarıyla eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadMasalar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnMasaSil_Click(object? sender, EventArgs e)
        {
            var masalar = masaRepo.GetAll();
            
            if (masalar.Count == 0)
            {
                MessageBox.Show("Silinecek masa bulunmuyor!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Boş masaları listele
            var bosMasalar = masalar.Where(m => m.Durum == "Boş").ToList();
            
            if (bosMasalar.Count == 0)
            {
                MessageBox.Show("Sadece boş masalar silinebilir!\nŞu anda boş masa yok.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Masa seçim dialogu
            var secimForm = new Form
            {
                Text = "Masa Sil",
                Size = new Size(400, 300),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = Color.FromArgb(44, 62, 80)
            };

            var lblBaslik = new Label
            {
                Text = "Silinecek masayı seçin:",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 20),
                Size = new Size(360, 30)
            };
            secimForm.Controls.Add(lblBaslik);

            var listBox = new ListBox
            {
                Location = new Point(20, 60),
                Size = new Size(360, 150),
                Font = new Font("Segoe UI", 11),
                BackColor = Color.FromArgb(52, 73, 94),
                ForeColor = Color.White
            };

            foreach (var masa in bosMasalar)
            {
                listBox.Items.Add($"{masa.MasaNo} - Kapasite: {masa.Kapasite}");
            }
            secimForm.Controls.Add(listBox);

            var btnSil = new Button
            {
                Text = "🗑️ SİL",
                Location = new Point(20, 220),
                Size = new Size(170, 40),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnSil.FlatAppearance.BorderSize = 0;
            btnSil.Click += (s, e) =>
            {
                if (listBox.SelectedIndex >= 0)
                {
                    var secilenMasa = bosMasalar[listBox.SelectedIndex];
                    var confirm = MessageBox.Show(
                        $"'{secilenMasa.MasaNo}' silinecek. Emin misiniz?",
                        "Onay",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (confirm == DialogResult.Yes)
                    {
                        try
                        {
                            masaRepo.Delete(secilenMasa.Id);
                            MessageBox.Show($"'{secilenMasa.MasaNo}' başarıyla silindi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            secimForm.DialogResult = DialogResult.OK;
                            secimForm.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen bir masa seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };
            secimForm.Controls.Add(btnSil);

            var btnIptal = new Button
            {
                Text = "❌ İPTAL",
                Location = new Point(210, 220),
                Size = new Size(170, 40),
                BackColor = Color.FromArgb(127, 140, 141),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnIptal.FlatAppearance.BorderSize = 0;
            btnIptal.Click += (s, e) => secimForm.Close();
            secimForm.Controls.Add(btnIptal);

            if (secimForm.ShowDialog() == DialogResult.OK)
            {
                LoadMasalar();
            }
        }

        private void MasaTiklandi(Masa masa)
        {
            var contextMenu = new ContextMenuStrip();
            contextMenu.BackColor = Color.FromArgb(60, 60, 65);
            contextMenu.ForeColor = Color.White;
            contextMenu.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            if (masa.Durum == "Boş")
            {
                var menuSiparisAc = new ToolStripMenuItem("📝 Sipariş Aç");
                menuSiparisAc.Click += (s, e) => SiparisAc(masa);
                contextMenu.Items.Add(menuSiparisAc);

                var menuRezerve = new ToolStripMenuItem("🟡 Rezerve Et");
                menuRezerve.Click += (s, e) => MasaDurumDegistir(masa, "Rezerve");
                contextMenu.Items.Add(menuRezerve);
            }
            else if (masa.Durum == "Dolu")
            {
                var menuSiparisGor = new ToolStripMenuItem("👁️ Siparişi Gör");
                menuSiparisGor.Click += (s, e) => SiparisGor(masa);
                contextMenu.Items.Add(menuSiparisGor);

                var menuHesapKapat = new ToolStripMenuItem("💰 Hesap Kapat");
                menuHesapKapat.Click += (s, e) => HesapKapat(masa);
                contextMenu.Items.Add(menuHesapKapat);
            }
            else if (masa.Durum == "Rezerve")
            {
                var menuSiparisAc = new ToolStripMenuItem("📝 Sipariş Aç");
                menuSiparisAc.Click += (s, e) => SiparisAc(masa);
                contextMenu.Items.Add(menuSiparisAc);

                var menuRezervasyonIptal = new ToolStripMenuItem("❌ Rezervasyon İptal");
                menuRezervasyonIptal.Click += (s, e) => MasaDurumDegistir(masa, "Boş");
                contextMenu.Items.Add(menuRezervasyonIptal);
            }

            contextMenu.Show(Cursor.Position);
        }

        private void SiparisAc(Masa masa)
        {
            var formSiparis = new FormSiparis(masa);
            formSiparis.ShowDialog();
            LoadMasalar();
        }

        private void SiparisGor(Masa masa)
        {
            if (masa.AktifSiparisId.HasValue)
            {
                var formSiparis = new FormSiparis(masa, masa.AktifSiparisId.Value);
                formSiparis.ShowDialog();
                LoadMasalar();
            }
        }

        private void HesapKapat(Masa masa)
        {
            if (masa.AktifSiparisId.HasValue)
            {
                var formOdeme = new FormOdeme(masa.AktifSiparisId.Value);
                formOdeme.ShowDialog();
                LoadMasalar();
            }
        }

        private void MasaDurumDegistir(Masa masa, string yeniDurum)
        {
            try
            {
                masaRepo.UpdateDurum(masa.Id, yeniDurum, null);
                MessageBox.Show($"Masa durumu '{yeniDurum}' olarak güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadMasalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

