# İlaç Takip Sistemi

Hastaların/kullanıcıların ilaçlarını ve ilaç hatırlatmalarını yönetebildiği, masaüstü (Windows Forms) tabanlı bir ilaç takip uygulaması.

## Özellikler

- **İlaç Yönetimi**: İlaç ekleme, silme, güncelleme ve listeleme
- **Hatırlatma Yönetimi**:
  - İlaç + kullanıcı + tarih/saat bazlı hatırlatma oluşturma
  - Aynı ilaç için aynı dakikaya çakışan hatırlatmaların engellenmesi
  - **Tekrarlayan hatırlatma**: "her gün" veya "her X günde bir" şeklinde otomatik tekrar
  - **Direkt düzenleme**: Var olan hatırlatmayı sil + yeniden ekle yerine tek tıkla güncelleme
  - Hatırlatma silme
- **Otomatik Alarm**: Uygulama açıkken arka planda 30 saniyede bir kontrol eden bir zamanlayıcı, vakti gelen hatırlatmalar için pop-up bildirim gösterir
  - Tek seferlik hatırlatmalar alarm sonrası otomatik olarak veritabanından temizlenir (geçmiş hatırlatma birikmez)
  - Tekrarlı hatırlatmalar alarm sonrası bir sonraki tetiklenme zamanına otomatik taşınır
- **Kullanıcı Yönetimi**: Kullanıcı (hasta) ekleme ve silme
  - Hatırlatması bulunan bir kullanıcı, ilişkili hatırlatmalar silinmeden silinemez (veri bütünlüğü koruması)

## Kullanılan Teknolojiler

| Katman | Teknoloji |
|---|---|
| Arayüz | Windows Forms (.NET 9, `net9.0-windows`) |
| Veri Erişimi | ADO.NET (`Microsoft.Data.SqlClient`) |
| Veritabanı | Microsoft SQL Server |
| Dil | C# |

## Proje Mimarisi

Proje, sorumlulukların ayrıldığı klasik **3 katmanlı mimari** ile yazılmıştır:

```
İlacTakipSistemi/
├── EntityLayer/          → Veri modelleri (Ilac, Kullanici, Hatirlatma)
├── DataAccess/            → Veritabanı işlemleri (Repository sınıfları)
├── Business/               → İş kuralları (HatirlatmaService)
├── Presentation/         → Kullanıcı arayüzü (FormMain, FormHatirlatma, FormKullanici)
├── App.config              → Veritabanı bağlantı bilgisi
├── DbGuncelleme.sql      → Tekrarlayan hatırlatma desteği için DB güncelleme scripti
└── Program.cs              → Uygulama giriş noktası
```

- **EntityLayer**: `Ilac`, `Kullanici`, `Hatirlatma` sınıflarını içerir. Sadece veri taşır, mantık barındırmaz.
- **DataAccess**: `IlacRepository`, `KullaniciRepository`, `HatirlatmaRepository` — tüm SQL sorguları (parametreli komutlarla) burada çalışır.
- **Business**: `HatirlatmaService` — hatırlatma ekleme/güncelleme öncesi çakışma kontrolü, alarm sonrası temizleme/tekrarlama mantığı gibi iş kurallarını barındırır.
- **Presentation**: Üç form —
  - `FormMain`: İlaç listesi, ekleme/silme/güncelleme, alarm zamanlayıcısı
  - `FormHatirlatma`: Hatırlatma ekleme/düzenleme/silme, tekrar ayarları
  - `FormKullanici`: Kullanıcı ekleme/silme

## Kurulum

### 1. Veritabanını Hazırlama

SQL Server üzerinde `İlacTakipDb` adında bir veritabanı oluşturun ve aşağıdaki tabloları kurun:

```sql
CREATE DATABASE İlacTakipDb;
GO
USE İlacTakipDb;
GO

CREATE TABLE Ilac (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    Ad              NVARCHAR(200)   NOT NULL,
    KullanimSikligi INT             NOT NULL,
    Aciklama        NVARCHAR(500)   NOT NULL
);

CREATE TABLE Kullanici (
    Id      INT IDENTITY(1,1) PRIMARY KEY,
    AdSoyad NVARCHAR(200) NOT NULL,
    Telefon NVARCHAR(50)  NOT NULL
);

CREATE TABLE Hatirlatma (
    Id               INT IDENTITY(1,1) PRIMARY KEY,
    IlacId           INT      NOT NULL FOREIGN KEY REFERENCES Ilac(Id),
    KullaniciId      INT      NOT NULL FOREIGN KEY REFERENCES Kullanici(Id),
    HatirlatmaZamani DATETIME NOT NULL,
    TekrarliMi       BIT      NOT NULL DEFAULT 0,
    TekrarAraligiGun INT      NOT NULL DEFAULT 0
);
```

> Eğer `Ilac`, `Kullanici`, `Hatirlatma` tabloları zaten varsa (tekrar özelliği eklenmeden önceki sürüm), sadece `DbGuncelleme.sql` dosyasını çalıştırmanız yeterlidir. Bu script `Hatirlatma` tablosuna `TekrarliMi` ve `TekrarAraligiGun` kolonlarını ekler.

### 2. Bağlantı Dizesini Ayarlama

`App.config` dosyasındaki bağlantı dizesini kendi SQL Server adınıza göre düzenleyin:

```xml
<connectionStrings>
  <add name="İlacTakipDb"
       connectionString="Server=SUNUCU_ADINIZ;Database=İlacTakipDb;Trusted_Connection=True;TrustServerCertificate=True;"
       providerName="Microsoft.Data.SqlClient" />
</connectionStrings>
```

### 3. Projeyi Çalıştırma

1. `İlacTakipSistemi.sln` dosyasını Visual Studio 2022 (veya üzeri) ile açın.
2. NuGet paketlerinin otomatik geri yüklenmesini bekleyin (`Microsoft.Data.SqlClient`).
3. `F5` ile derleyip çalıştırın.

## Kullanım

1. **İlaç Ekle** sekmesinden (ana ekran) ilaçlarınızı kaydedin.
2. **Kullanıcılar** butonundan hatırlatma alacak kişileri (hastaları) tanımlayın.
3. **Hatırlatıcı** butonundan, bir ilaç + kullanıcı + tarih-saat seçerek hatırlatma oluşturun.
   - Düzenli kullanılan bir ilaç için **"Tekrarlayan hatırlatma"** kutusunu işaretleyip kaç günde bir tekrar edeceğini girin.
   - Var olan bir hatırlatmayı değiştirmek için listeden satıra tıklayın, alanlar otomatik dolacaktır; ardından **Düzenle**'ye basın.
4. Uygulama açık kaldığı sürece, hatırlatma zamanı geldiğinde otomatik bir bildirim penceresi açılır.

## Bilinen Kısıtlar

- Alarm kontrolü yalnızca uygulama açıkken çalışır (arka plan servisi/sistem bildirimi değildir).
- Hata yönetimi (try-catch) veri erişim katmanında sınırlıdır; veritabanı geçici olarak erişilemez olursa alarm döngüsü sessizce atlanır.
- Asenkron (`async/await`) veritabanı çağrıları kullanılmamıştır; yoğun kullanımda arayüz kısa süreli yanıt vermeyebilir.
## Ekran Görüntüleri
<img width="798" height="490" alt="Image" src="https://github.com/user-attachments/assets/e957436f-8f9b-4cec-a0ee-9dc5caa24788" />

<img width="586" height="486" alt="Image" src="https://github.com/user-attachments/assets/879a3dff-5f7b-4af3-bc05-fc7f20f87ed8" />

<img width="796" height="544" alt="Image" src="https://github.com/user-attachments/assets/fd84f154-f1e0-44ee-9414-3be91bb815ba" />

<img width="797" height="546" alt="Image" src="https://github.com/user-attachments/assets/186b7113-cadd-4ecb-b56e-6b60d070e3c5" />

Bu proje eğitim/portföy amaçlıdır.
