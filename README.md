# ☕ Cafe Otomasyon Sistemi

<div align="center">

![.NET](https://img.shields.io/badge/.NET-7.0-512BD4?style=for-the-badge&logo=dotnet)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![SQLite](https://img.shields.io/badge/SQLite-003B57?style=for-the-badge&logo=sqlite&logoColor=white)
![Windows](https://img.shields.io/badge/Windows-0078D6?style=for-the-badge&logo=windows&logoColor=white)
![License](https://img.shields.io/badge/License-MIT-green?style=for-the-badge)

**Modern ve profesyonel bir cafe yönetim uygulaması**

*C# Windows Forms ve SQLite ile geliştirilmiştir*

</div>

---
<img width="499" height="844" alt="Ekran görüntüsü 2025-11-13 134045" src="https://github.com/user-attachments/assets/94677acf-4af2-49ca-844b-d55cac275956" />


## 🔐 Giriş Bilgileri

Uygulama ilk açıldığında giriş ekranı karşınıza gelecektir. Test için hazır kullanıcılar:

**Admin Hesabı:**
- Kullanıcı Adı: `admin`
- Şifre: `admin`

**Personel Hesabı:**
- Kullanıcı Adı: `personel`
- Şifre: `123456`

Yeni hesap oluşturmak için giriş ekranındaki "Kayıt Ol" bağlantısını kullanabilirsiniz.

## 🎯 Özellikler

### 🔐 Kullanıcı Yönetimi
- Modern login ve kayıt ekranları
- Güvenli şifre saklama (SHA256 hash)
- Rol tabanlı erişim (Admin, Personel)

### 📦 Ürün Yönetimi
- Ürün ekleme, silme ve güncelleme
- Kategorilere göre ürün organizasyonu
- Fiyat yönetimi
- Aktif/Pasif ürün durumu

### 📋 Kategori Yönetimi
- Kategori ekleme, silme ve güncelleme
- Kategori bazlı ürün filtreleme

### 🪑 Masa Yönetimi
- Masa durumu takibi (Boş, Dolu, Rezerve)
- Görsel masa haritası
- Hızlı masa durumu değiştirme

### 📝 Sipariş İşlemleri
- Kolay sipariş oluşturma
- Kategoriye göre ürün seçimi
- Sipariş detayları görüntüleme
- Sipariş düzenleme ve ürün ekleme/çıkarma
- Anlık toplam tutar hesaplama

### 💰 Ödeme İşlemleri
- Nakit ve kredi kartı ödeme seçenekleri
- Para üstü hesaplama
- Adisyon görüntüleme
- Hesap kapatma

### 📊 Satış Raporları
- Tarih aralığına göre satış raporları
- Toplam ciro görüntüleme
- Ortalama hesap tutarı
- Sipariş detaylarını görüntüleme

## 🛠️ Teknolojiler

- **.NET 7.0** - Modern .NET platformu
- **Windows Forms** - Masaüstü arayüzü
- **SQLite** - Hafif ve taşınabilir veritabanı
- **C#** - Programlama dili

## 📋 Gereksinimler

- Windows 10/11 (64-bit)
- .NET 7.0 Runtime (veya portable sürümü kullanın)
- Visual Studio 2022 (geliştirme için)

## 🚀 Kurulum

### Seçenek 1: Kullanıma Hazır (Portable) Sürüm 🎯

**En Kolay Yöntem!** Hiçbir kurulum gerektirmez:

1. [Releases](../../releases) sayfasından son sürümü indirin
2. ZIP dosyasını çıkarın
3. `CafeOtomasyon.exe` dosyasına çift tıklayın
4. Kullanmaya başlayın! 🎉

### Seçenek 2: Kaynak Koddan Derleme 👨‍💻

Geliştirme yapmak veya kaynak koddan derlemek için:

1. Projeyi klonlayın:
```bash
git clone https://github.com/KULLANICI_ADI/cafe-otomasyon.git
cd cafe-otomasyon
```

2. Visual Studio 2022 ile `CafeOtomasyon.sln` dosyasını açın
3. NuGet paketlerinin yüklenmesini bekleyin
4. F5 tuşuna basarak uygulamayı çalıştırın

**Veya Komut Satırından:**

```bash
# Projeyi build et
dotnet build

# Uygulamayı çalıştır
dotnet run

# Portable sürüm oluştur
dotnet publish -c Release -r win-x64 --self-contained true
```

## 📁 Proje Yapısı

```
CafeOtomasyon/
├── Models/              # Veri modelleri
│   ├── Kullanici.cs
│   ├── Kategori.cs
│   ├── Urun.cs
│   ├── Masa.cs
│   ├── Siparis.cs
│   └── SiparisDetay.cs
├── Database/            # Veritabanı katmanı
│   ├── DatabaseHelper.cs
│   ├── KullaniciRepository.cs
│   ├── KategoriRepository.cs
│   ├── UrunRepository.cs
│   ├── MasaRepository.cs
│   └── SiparisRepository.cs
├── Forms/               # Form ekranları
│   ├── FormLogin.cs
│   ├── FormKayit.cs
│   ├── FormAnaMenu.cs
│   ├── FormKategoriler.cs
│   ├── FormUrunler.cs
│   ├── FormMasalar.cs
│   ├── FormSiparis.cs
│   ├── FormOdeme.cs
│   └── FormRaporlar.cs
├── Helpers/             # Yardımcı sınıflar
│   ├── PasswordHelper.cs
│   └── CurrentUser.cs
├── Program.cs           # Uygulama giriş noktası
├── CafeOtomasyon.csproj # Proje dosyası
├── cafe_icon.ico        # Uygulama ikonu
├── LICENSE              # MIT Lisansı
└── README.md            # Dökümantasyon
```

## 💾 Veritabanı

Uygulama ilk çalıştırıldığında otomatik olarak:
- `CafeOtomasyon.db` dosyası oluşturulur
- Gerekli tablolar oluşturulur (Kullanıcılar, Kategoriler, Ürünler, Masalar, Siparişler)
- Örnek veriler yüklenir:
  - 2 Kullanıcı (admin, personel)
  - 5 Kategori
  - 19 Ürün
  - 10 Masa

Veritabanı dosyası uygulama klasöründe bulunur ve kolayca yedeklenebilir.

## 🎨 Kullanıcı Arayüzü

- **Modern Dark Tema** - Göz yormayan koyu renk paleti
- **Renkli Butonlar** - Her işlem için farklı renk kodları
- **İkonlar** - Görsel zenginlik için emoji ikonlar
- **Responsive Tasarım** - Farklı ekran boyutlarına uyumlu
- **Kolay Kullanım** - Sezgisel ve kullanıcı dostu arayüz

## 📖 Kullanım

### Giriş
1. Uygulamayı başlatın
2. Kullanıcı adı ve şifre ile giriş yapın
3. Veya "Kayıt Ol" ile yeni hesap oluşturun

### Ana Menü
Giriş yaptıktan sonra 6 ana işlem seçeneği sunulur:
- 🪑 Masa İşlemleri
- 📦 Ürün Yönetimi
- 📋 Kategoriler
- 📊 Satış Raporları
- ⚙️ Ayarlar
- ❌ Çıkış

### Sipariş Alma İşlemi
1. Masa İşlemleri'ne tıklayın
2. Boş bir masaya tıklayın
3. "Sipariş Aç" seçeneğini seçin
4. Kategori seçin
5. Ürünleri seçerek sipariş oluşturun
6. Kaydet ve Kapat

### Hesap Kapatma
1. Dolu bir masaya tıklayın
2. "Hesap Kapat" seçeneğini seçin
3. Ödeme yöntemi seçin (Nakit/Kredi Kartı)
4. Nakit için alınan tutarı girin
5. Ödeme Al butonuna tıklayın

## 🔒 Güvenlik

- SQL Injection koruması (Parametreli sorgular)
- Veri doğrulama kontrolleri
- Hata yönetimi ve kullanıcı bildirimleri

## 🐛 Bilinen Sorunlar

Şu anda bilinen bir sorun bulunmamaktadır.

## 📸 Ekran Görüntüleri

<div align="center">

### 🔐 Giriş Ekranı
Modern ve şık giriş ekranı ile kullanıcı dostu deneyim

### 🏠 Ana Menü
Tüm işlemlere kolay erişim sağlayan renkli menü sistemi

### 🪑 Masa Yönetimi
Görsel masa haritası ile kolay masa takibi

### 📝 Sipariş Ekranı
Kategori bazlı kolay sipariş alma sistemi

### 💰 Ödeme Ekranı
Nakit ve kredi kartı ödeme seçenekleri

### 📊 Raporlar
Detaylı satış raporları ve istatistikler

</div>

## 🤝 Katkıda Bulunma

Katkılarınızı bekliyoruz! Projeye katkıda bulunmak için:

1. Bu repository'yi fork edin
2. Yeni bir branch oluşturun (`git checkout -b feature/ozellik-adi`)
3. Değişikliklerinizi commit edin (`git commit -am 'Yeni özellik: Açıklama'`)
4. Branch'inizi push edin (`git push origin feature/ozellik-adi`)
5. Pull Request oluşturun

## 📝 Lisans

Bu proje MIT lisansı altında lisanslanmıştır. Detaylar için [LICENSE](LICENSE) dosyasına bakın.

## 👨‍💻 Geliştirici Notları

### Veritabanı Sıfırlama
Veritabanını sıfırlamak için `CafeOtomasyon.db` dosyasını silin ve uygulamayı yeniden başlatın.

### Özelleştirme
Renk şemalarını değiştirmek için form dosyalarındaki `Color.FromArgb()` değerlerini düzenleyin.

### Yeni Özellikler
Gelecek versiyonlarda eklenebilecek özellikler:
- Personel yönetimi
- Stok takibi
- Müşteri yönetimi ve sadakat programı
- Detaylı raporlar (günlük, haftalık, aylık)
- Fatura yazdırma
- Yedekleme/Geri yükleme

## ⭐ Yıldız Vermeyi Unutmayın!

Bu projeyi beğendiyseniz, lütfen GitHub'da ⭐ verin!

## 📞 İletişim

Sorularınız, önerileriniz veya hata bildirimleri için:
- **Issues** sekmesinden yeni bir issue açabilirsiniz
- **Pull Request** gönderebilirsiniz
- Projeyi **Fork** edip kendi versiyonunuzu geliştirebilirsiniz

## 🙏 Teşekkürler

Bu projeyi kullanan ve katkıda bulunan herkese teşekkürler! ❤️

---

<div align="center">

**© 2024 Cafe Otomasyon Sistemi**

[![MIT License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

*Kahvenizi severek içmeniz dileğiyle* ☕

</div>

