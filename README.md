# Bing Duvar Kağıdı Değiştiricisi

**Bu depo arşivlenmiştir ve artık geliştirmesi yapılmamaktadır.**

<img src="https://img.shields.io/badge/Versiyon-3.0.0-blueviolet.svg?style=flat" alt="Versiyon" /> <img src="https://img.shields.io/badge/Durum-Çalışıyor-brightgreen.svg?style=flat" alt="Durum" /> <img src="https://img.shields.io/badge/Lisans-MIT-blue.svg?style=flat" alt="Lisans" />

Bing.com adlı web sitesinden günlük duvar kağıtlarını indiren ve mevcut (Windows) işletim sistemine duvar kağıdı olarak atayan program.

## Özellikler

- Bing’in dünya çapındaki farklı web sayfalarına <strong>(Almanya, Amerika, Avustralya, Brezilya, Britanya, Fransa, Hindistan, Japonya, Kanada, Türkiye, Yeni Zelanda)</strong> bağlanarak günün duvar kağıtlarını bilgisayara indirir.
- İndirilen günün fotoğrafını (mevcut ekran çözünürlüğüne göre) duvar kağıdı olarak ayarlar.
- Ekran çözünürlüğünü otomatik algılayıp o boyutta duvar kağıdı indirir.
- Görselleri yerel önbellekte saklar; aynı gün ve aynı ayarlar için tekrar indirmez.
- Yeni sürüm kontrolü yapar ve güncelleme adresini gösterir.

## Kurulum

Kaynaktan derlemek için [.NET 10 SDK](https://dotnet.microsoft.com/download) gerekir:

```bash
dotnet build BingDuvarKagidiDegistirici.sln
```

Tek dosya halinde çalıştırılabilir (`.exe`) sürüm üretmek için:

```bash
dotnet publish BingUI/BingUI.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

Oluşan `Bing Duvar Kagidi Degistirici.exe` dosyası şu klasördedir:

```
BingUI\bin\Release\net10.0-windows\win-x64\publish\
```

Bu dosyayı herhangi bir Windows 10/11 (x64) bilgisayara kopyalayıp doğrudan çalıştırabilirsiniz; ayrıca .NET kurulumu gerekmez.

## Kullanım

1. Programı açın; ülke ve çözünürlük ayarlarını seçin.
2. Günün duvar kağıdını indirin.
3. Duvar kağıdını uygulayın.

Ayarlar `settings.json` dosyasında saklanır. İndirilen görseller `%LOCALAPPDATA%\BingDuvarKagidiDegistirici\images` klasörüne kaydedilir.

## Gereksinimler

- Windows 10 (17763) veya üzeri
- .NET 10 SDK (yalnızca derleme için)
- İnternet bağlantısı

## Testler

```bash
dotnet test BingLibrary.Tests/BingLibrary.Tests.csproj --filter "FullyQualifiedName!~LiveSmoke"
```

`LiveSmoke` testleri gerçek Bing ve GitHub isteği yaptığı için bu komutta atlanır.

## 3.0.0 sürümündeki değişiklikler

- Sürüm bilgisi tek kaynaktan (`BingLibrary.csproj`) okunur.
- Kütüphane WinForms `MessageBox` ve `Screen` bağımlılığından arındırıldı; görsel ayarlarını UI katmanı verir.
- Önbellek dosya adı tarih ve görsel URL'sinin özetini içerir; farklı ülke ve çözünürlükler birbirinin dosyasını ezmez.
- Duvar kağıdı ayarı başarısız olursa önceki kayıt değerleri geri yüklenir.
- Senkron sarmalayıcılar kaldırıldı; ağ işlemleri tamamen `async` çalışır.
- Kullanılmayan `Settings` ve `app.config` dosyaları kaldırıldı.

## Lisans

MIT lisansı. Ayrıntılar için depodaki `LICENSE` dosyasına bakın.
