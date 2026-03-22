# Anlık Sıcaklık Takip ve Alarm Sistemi

Bu proje örneği: her 5 saniyede bir rastgele sıcaklık üreten ve sıcaklık 80°C'yi geçerse veritabanına bir "Alarm Logu" kaydeden bir backend servis içerir.

Özellikler
- .NET 8 WebAPI
- EF Core veri erişimi (kısmi model konfigürasyonu Infrastructure içinde)
- Raw SQL ile alarm kaydı (Database.ExecuteSqlInterpolatedAsync kullanılarak)
- Arka plan servis: `TemperatureMonitorService` (her 5 saniyede bir sıcaklık üretir)
- Docker & docker-compose örneği (SQL Server + WebAPI)

Çalıştırma (Docker ile)
1. docker ve docker-compose kurulu olduğundan emin olun.
2. docker-compose.yml dosyasında `SA_PASSWORD` (Your_password123) değerini güvenli bir parola ile değiştirin.
3. Aşağıdaki komut ile başlatın:

   docker-compose up --build

4. Servis 5000 numaralı portta HTTP üzerinde açılacaktır (örnek: http://localhost:5000)

Notlar
- Uygulama başlatıldığında veritabanı oluşturulmaya çalışılır (EnsureCreated).
- `TemperatureMonitorService` logları uygulama loglarında görebilirsiniz; 80°C üzerindeki değerler veritabanına raw SQL kullanılarak eklenir.

Geliştirme
- Projeyi Visual Studio ile açıp build edin.
- Mevcut migration veya DB şeması varsa `EnsureCreated` mevcut veritabanını değiştirmeyebilir; tam kontrol için EF Core migration akışını kullanın.

