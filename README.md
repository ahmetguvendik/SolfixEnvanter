## Solfix Envanter Takip Sistemi

Kurumsal varlık/envanter takibi için geliştirilen, katmanlı mimariye sahip .NET 9 Web API projesi. JWT ile kimlik doğrulama, rol tabanlı yetkilendirme, MediatR ile CQRS, FluentValidation, Serilog ile MSSQL loglama ve hız sınırlama (rate limiting) içerir.

### Özellikler
- **Varlık Yönetimi**: Asset, AssetType, Department, Location, Cabinet vb.
- **Ağ ve Altyapı**: AssetNetworkInfo, InternetLine, DomainName, SslCertificate.
- **Bakım Süreçleri**: MaintenanceType, MaintenanceRecord, formlar ve prosedürler.
- **Kimlik Doğrulama**: JWT Bearer, rol tabanlı yetkilendirme (Identity).
- **Rate Limiting**: Genel, kimlik doğrulama ve varlık operasyonları için farklı kotalar.
- **Loglama**: Serilog ile konsol ve MSSQL `Logs` tablosuna yazım (ek alanlar: UserId, UserName, RequestPath, ClientIp).
- **API Dokümantasyonu**: Development ortamında Swagger UI.

### Mimari
- **Core**
  - `Domain`: Entity ve enum tanımları.
  - `Application`: MediatR tabanlı komut/sorgu, Validations (FluentValidation), DTO/Result katmanı.
- **Infrastructure**
  - `Persistance`: EF Core DbContext, Repository implementasyonları, servisler, migrasyonlar.
- **Presentation**
  - `WebApi`: Program/Startup, Controller'lar, appsettings ve Swagger.

Proje yapısı (özet):
```text
Core/
  Application/
    Features/{Commands,Queries,Handlers,Results}
    Validations/
    ServiceRegistration.cs
  Domain/
    Entites/*.cs
Infrastructure/
  Persistance/
    Contexts/SolfixEnvanterDbContext.cs
    Migrations/*
    Repositories/*
    Services/*
    ServiceRegistration.cs
Presentation/
  WebApi/
    Program.cs
    appsettings*.json
    Controller/*
    wwwroot/uploads/*
```

### Gereksinimler
- **.NET SDK 9.0**
- **SQL Server** (LocalDB, Docker veya uzak sunucu)
- (Önerilir) **EF Core Tools**: `dotnet tool install --global dotnet-ef`

### Hızlı Başlangıç
1) **Konfigürasyonu ayarla** (güvenli yöntem: User Secrets veya ortam değişkenleri):
```bash
# WebApi projesi dizininde secrets başlat
cd Presentation/WebApi
 dotnet user-secrets init

# Bağlantı cümlesi
 dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Database=SolfixEnvanterDb;User Id=sa;Password=<SIFRE>;Encrypt=True;TrustServerCertificate=True;"

# JWT ayarları
 dotnet user-secrets set "Jwt:Key" "<en az 32 baytlık gizli anahtar>"
 dotnet user-secrets set "Jwt:Issuer" "SolfixEnvanterTakipSistemi"
 dotnet user-secrets set "Jwt:Audience" "SolfixEnvanterTakipSistemiKullanicilari"
```
Alternatif: ortam değişkenleri
```bash
export ConnectionStrings__DefaultConnection="..."
export Jwt__Key="..."
export Jwt__Issuer="SolfixEnvanterTakipSistemi"
export Jwt__Audience="SolfixEnvanterTakipSistemiKullanicilari"
```

2) **Veritabanını hazırla** (migrasyonları uygula):
```bash
# Çalıştırma (startup) projesi WebApi, migrasyon projesi Persistance
 dotnet ef database update \
  --startup-project Presentation/WebApi \
  --project Infrastructure/Persistance
```

3) **Uygulamayı çalıştır**:
```bash
 dotnet run --project Presentation/WebApi/WebApi.csproj
```
- Geliştirme ortamında Swagger: `https://localhost:<port>/swagger`
- Statik dosyalar: `Presentation/WebApi/wwwroot/uploads/...`

### Konfigürasyon
- `ConnectionStrings:DefaultConnection`: SQL Server bağlantı cümlesi.
- `Jwt:Key/Issuer/Audience/ExpireMinutes`: JWT doğrulama parametreleri.
- `RateLimiting`:
  - `General`, `Authentication`, `AssetOperations` için `PermitLimit`, `Window`, `QueueLimit`.
- `Serilog`:
  - Konsol ve MSSQL sink; tablo adı: `Logs`, `autoCreateSqlTable: true`.

### Kimlik Doğrulama ve Yetkilendirme
- `Authorization: Bearer <token>` ile çağrı yapın.
- Token claim'lerinden `Role`, `Name`, `NameIdentifier` desteklenir.
- Login/register uç noktalarını ve örnek istekleri Swagger'da görebilirsiniz.

### Rate Limiting (Özet)
- Politikalar: `General`, `Authentication`, `AssetOperations`.
- Limit aşımlarında HTTP 429 döner: "Rate limit exceeded...".

### Loglama
- Serilog ile konsol + MSSQL `Logs` tablosu.
- Ek sütunlar: `UserId`, `UserName`, `RequestPath`, `ClientIp`.
- Performans için batch yazım ayarları yapılmıştır.

### Statik Dosyalar
- `wwwroot/uploads/assignments`, `forms`, `procedures`, `services` klasörleri servis edilir.

### Geliştirme İpuçları
- Yeni migrasyon ekleme:
```bash
 dotnet ef migrations add <MigrationAdi> \
  --startup-project Presentation/WebApi \
  --project Infrastructure/Persistance
```
- Veritabanını sıfırlama:
```bash
 dotnet ef database drop -f \
  --startup-project Presentation/WebApi \
  --project Infrastructure/Persistance
```

### Sorun Giderme
- SQL bağlantı hatası: Sunucu/port, kullanıcı/parola ve `TrustServerCertificate=True` (geliştirme) ayarlarını kontrol edin.
- JWT key uzunluğu: En az 32 bayt (HMAC-SHA256 için önerilir).
- Swagger görünmüyor: Sadece Development ortamında aktiftir.


