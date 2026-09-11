# Borsa Platformu (Trading App)

Bu proje, ASP.NET Core Web API ve .NET MAUI teknolojileri kullanılarak geliştirilmiş, uçtan uca bir kripto/hisse senedi alım satım platformudur. İçerisinde Kullanıcı Yönetimi, Cüzdan (Wallet), Canlı Piyasa Verileri, Sipariş Motoru (Order Engine), Portföy ve İşlem Geçmişi özellikleri bulunur.

## Mimari
Proje, **Clean Architecture (Onion Architecture)** prensiplerine uygun olarak 4 temel katmana ayrılmıştır:
1. `TradingApp.Domain`: Çekirdek varlıklar ve arayüzler (Interfaces).
2. `TradingApp.Application`: İş kuralları, DTO'lar, servis arayüzleri ve CQRS/Servis katmanı.
3. `TradingApp.Infrastructure`: Veritabanı işlemleri (EF Core, PostgreSQL), Background Service'ler ve Repository implementasyonları.
4. `TradingApp.Api`: API Controller'ları, Dependency Injection ve Middleware ayarları.
5. `TradingApp.Desktop` (.NET MAUI): API'yi tüketen, cross-platform masaüstü (veya mobil) kullanıcı arayüzü.

## Geliştirme Ortamını Kurma

Projeyi bilgisayarınıza klonladıktan sonra aşağıdaki adımları sırasıyla uygulayarak projeyi ayağa kaldırabilirsiniz.

### 1. Gereksinimler
- .NET 9.0 (veya 8.0) SDK
- **Docker Desktop** (Veritabanını saniyeler içinde ayağa kaldırmak için en kolay yöntem)
- Visual Studio 2022 (MAUI ve .NET Web workload'ları) veya Visual Studio Code

### 2. Veritabanını Ayağa Kaldırma (Docker ile En Kolay Yöntem)
Projenizde karmaşık PostgreSQL kurulumlarıyla uğraşmamak için Docker kullanabilirsiniz. Proje dizininde yer alan `docker-compose.yml` dosyası, veritabanınızı tek komutla hazır hale getirir.

Bilgisayarınızda Docker Desktop açıkken, terminalde projenin kök dizininde şu komutu çalıştırın:
```bash
docker-compose up -d
```
Bu komut arka planda PostgreSQL indirecek ve `TradingAppDb` adında, şifresi `postgres` olan bir veritabanını `5432` portundan hizmete sunacaktır.

*(API projesinin `appsettings.json` dosyasındaki bağlantı dizesi halihazırda bu Docker ayarlarına göre yapılandırılmıştır, ek bir ayar yapmanıza gerek yoktur.)*

### 3. Migration ve Veritabanı Tablolarını Oluşturma
Veritabanı sunucumuz (Docker) çalıştığına göre, tabloları oluşturmamız gerekiyor. Terminalde API dizinine girin ve şu komutu çalıştırın:
*(Eğer `dotnet-ef` aracı bilgisayarınızda yoksa hata alırsınız. Varsa doğrudan ikinci komuta geçebilirsiniz)*

```bash
# Sadece ilk kurulumda (Tool yoksa)
dotnet tool install --global dotnet-ef

# Tabloları oluşturma
cd src/TradingApp.Api
dotnet ef database update
```

### 4. API'yi Çalıştırma (Backend)
Backend API'yi ayağa kaldırmak için API dizini içindeyken şu komutu çalıştırın:

```bash
dotnet run
```
Bu komuttan sonra API `http://localhost:5000` adresinde çalışmaya başlayacaktır.

### 5. Masaüstü Uygulamasını Çalıştırma (Frontend)
MAUI Desktop uygulamasını çalıştırmak için yeni bir terminal penceresi açın.
*(API'nin diğer pencerede arka planda çalıştığından emin olun, çünkü masaüstü uygulaması `localhost:5000` üzerinden bağlanacaktır.)*

Visual Studio kullanıyorsanız: `TradingApp.Desktop` projesine sağ tıklayıp "Set as Startup Project" yapın ve yeşil Oynat tuşuna basın.

Komut satırı kullanıyorsanız:
```bash
cd src/TradingApp.Desktop
dotnet build -t:Run -f net9.0-windows10.0.19041.0
```
*(Not: Sürüm uyuşmazlığı alırsanız `net9.0` kısmını `net8.0` yapabilirsiniz).*

## Uygulama Test Akışı
Masaüstü uygulaması yerine Postman/Swagger ile test etmek isterseniz temel akış şu şekildedir:

1. **Kayıt ve Giriş:** `/api/Auth/register` ve `/api/Auth/login` uçları ile kullanıcı oluşturup JWT Token alın.
2. **Cüzdan Bakiye Yükleme:** `/api/Wallet/deposit` ile hesabınıza sanal para yükleyin.
3. **Piyasayı Başlatma (Seed):** Piyasada satılacak ürünleri oluşturmak için **sadece bir kez** `/api/Asset/seed` metoduna POST isteği atın. (Background servis fiyatları 10 saniyede bir otonom güncelleyecektir).
4. **Alım-Satım:** `/api/Order/market` ucuyla `AssetSymbol` (Örn: BTC), `Side` (1 = Buy, 2 = Sell) ve `Quantity` vererek alım satım yapın.
5. **Portföy Görüntüleme:** `/api/Portfolio` ucuyla sahip olduğunuz varlıkları ve kâr/zararınızı (Profit/Loss) görüntüleyin.

