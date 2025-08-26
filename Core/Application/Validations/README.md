# Validations

Bu klasör, uygulama genelindeki tüm validation (doğrulama) kurallarını içerir.

## Klasör Yapısı

```
Validations/
├── Behaviors/                    # Validation davranışları
│   └── ValidationBehavior.cs     # Genel validation behavior
├── Commands/                     # Command validasyonları
│   ├── AppUsers/                 # Kullanıcı işlemleri
│   ├── Assets/                   # Varlık işlemleri
│   ├── AssetNetworkInfo/         # Ağ bilgisi işlemleri
│   ├── Cabinets/                 # Kabin işlemleri
│   ├── DomainNames/              # Domain adı işlemleri
│   ├── InternetLines/            # Internet hattı işlemleri
│   ├── Locations/                # Lokasyon işlemleri
│   ├── MaintenanceRecords/       # Bakım kayıt işlemleri
│   ├── MaintenanceTypes/         # Bakım tipi işlemleri
│   └── SslCertificates/          # SSL sertifika işlemleri
└── README.md                     # Bu dosya
```

## Validation Kuralları

Her entity için aşağıdaki validation kuralları uygulanır:

### Genel Kurallar
- **ID Kontrolü**: Tüm update işlemlerinde ID'nin boş olmaması
- **Zorunlu Alanlar**: Her entity için gerekli alanların kontrolü
- **Karakter Sınırları**: Metin alanları için maksimum uzunluk kontrolleri

### Özel Kurallar
- **E-posta**: Geçerli e-posta formatı kontrolü
- **IP Adresi**: Geçerli IP adresi formatı kontrolü
- **MAC Adresi**: Geçerli MAC adresi formatı kontrolü
- **Domain Adı**: Geçerli domain adı formatı kontrolü
- **Tarih Kontrolleri**: Gelecek tarihlerin ve tarih sıralamasının kontrolü
- **Sayısal Değerler**: Pozitif değer ve aralık kontrolleri

## Kullanım

Validation kuralları otomatik olarak `ValidationBehavior` tarafından uygulanır. Her command gönderildiğinde ilgili validator çalıştırılır ve hata durumunda uygun mesajlar döndürülür.
