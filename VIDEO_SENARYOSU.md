# 🎬 5 Dakikalık Ödev Video Sunumu Senaryosu (Tam Puan Rehberi)

> [!IMPORTANT]
> **Önemli Kural:** Video süresi **en fazla 5 dakika** olmalıdır. Aşağıdaki dakikaları ve cümleleri takip ederseniz videoyu 3.5 - 4 dakikada eksiksiz tamamlayıp 10 tam puan alabilirsiniz!

---

### ⏱️ 0:00 - 0:30 | 1. Adım: Kendini Tanıtma ve Genel Bakış
- **Ekranda Açık Olacak:** Visual Studio ve proje açık olsun.
- **Ne Söyleyeceksin:**
  > *"Merhaba hocam, ben [Adınız Soyadınız], öğrenci numaram [Öğrenci Numaranız]. BSM352/BSM322 Görsel Programlama dersi 1. ödevi olan .NET MAUI Windows Hesap Makinesi projemi sunuyorum. Projem Standart mod, Bilimsel mod ve tema seçimi yapabildiğimiz Ayarlar sayfasından oluşmaktadır."*

---

### ⏱️ 0:30 - 1:15 | 2. Adım: Shell Yapısı ve XAML Bileşenleri
- **Ekranda Göster:** `AppShell.xaml` ve `StandardPage.xaml` dosyalarını açıp göster.
- **Ne Söyleyeceksin:**
  > *"Sayfalar arası geçişi sağlamak için `AppShell.xaml` dosyamızda .NET MAUI Shell mimarisini kullandık. Sol taraftaki Flyout menümüz Standart, Bilimsel ve Ayarlar sayfalarına bağlanıyor.*
  > 
  > *Arayüzlerimizi tasarlarken saf XAML kullandık. Düzenli ve esnek bir yerleşim için satır ve sütunlarını yıldız oranlarıyla böldüğümüz `Grid` yapısından yararlandık. Rakam ve işlemler için `Button`, sonuç ve geçmiş ifadelerini göstermek için `Label` bileşenlerini kullandık."*

---

### ⏱️ 1:15 - 2:00 | 3. Adım: Stiller, Örtük/Açık Stiller ve BasedOn
- **Ekranda Göster:** `Resources/Styles/Styles.xaml` ve `Colors.xaml` dosyalarını aç.
- **Ne Söyleyeceksin:**
  > *"Ödev kriterlerinde belirtildiği gibi sayfalarda hiçbir sabit renk veya yazı boyutu değeri doğrudan yazılmamıştır. Tüm renkleri `Colors.xaml`, stilleri ise `Styles.xaml` içinde topladık.*
  > 
  > *Burada butonlarımız için `x:Key` değeri olmayan ortak bir **Örtük (implicit)** stil tanımladık. Rakamlar, operatörler ve eşittir butonları için ise bu ortak stilden `BasedOn` ile türettiğimiz `NumberButtonStyle`, `OperatorButtonStyle` ve `EqualButtonStyle` gibi **Açık (explicit)** stilleri kullandık."*

---

### ⏱️ 2:00 - 2:45 | 4. Adım: Kod Yapısı ve Hesaplama Motoru (CalculatorEngine)
- **Ekranda Göster:** `Services/CalculatorEngine.cs` dosyasını aç.
- **Ne Söyleyeceksin:**
  > *"Kod kalitesi ve modülerlik açısından hesaplama mantığını arayüzden ayırarak `CalculatorEngine.cs` servisinde topladık. Standart dört işlem, üs alma, kök alma, trigonometrik `sin`, `cos`, `tan` fonksiyonları ve logaritma işlemlerini burada yönettik."*

---

### ⏱️ 2:45 - 4:00 | 5. Adım: Uygulama Çalıştırma, Örnek İşlemler ve Hata Yönetimi
- **Ekranda Göster:** Visual Studio'dan yeşil **F5 (Windows Machine)** tuşuna bas, uygulama açılsın!
- **Ne Yapacaksın ve Söyleyeceksin:**
  1. **Standart Modda İşlem:**
     > *"Şimdi uygulamamızı çalıştıralım. Standart modda `25 + 75 =` yapıyorum, sonuç `100` olarak ekranda. `√100` yapıyorum, `10` çıkıyor."*
  2. **Hata Yönetimi (Sıfıra Bölme):**
     > *"Hata yönetimini test edelim: `50 ÷ 0 =` tuşladığımızda uygulama çökmüyor ve ekranda kırmızı renkle `Sıfıra bölünemez` uyarısını gösteriyor."*
  3. **Bilimsel Mod Geçişi:**
     > *"Sol üstteki menüyü açıp Bilimsel moda geçiyorum. Burada `DEG` modunda `90 sin` yapıyorum, `1` sonucunu alıyoruz. `log(100)` yapıyorum, `2` sonucunu alıyoruz. Negatif kök denediğimizde ise `Geçersiz giriş` uyarısı alıyoruz."*

---

### ⏱️ 4:00 - 4:45 | 6. Adım: Ayarlar Sayfası, Tema Değişimi ve Kalıcılık Testi
- **Ekranda Göster:** Menüden **Ayarlar (Settings)** sayfasına tıkla.
- **Ne Yapacaksın ve Söyleyeceksin:**
  1. **Koyu Temaya Geçiş:**
     > *"Menüden Ayarlar sayfasına geliyoruz. Burada Açık, Koyu ve Sistem teması seçeneklerimiz var. 'Koyu' temayı seçiyorum; gördüğünüz gibi anında tüm sayfa ve buton renkleri koyu temaya büründü."*
  2. **Standart Moda Dön:**
     > *"Standart moda dönüyorum, koyu temanın tüm butonlara ve metinlere kusursuz yansıdığını görüyoruz."*
  3. **Kapat-Aç Testi (Preferences):**
     > *"Şimdi uygulamayı tamamen kapatıyorum. Tekrar F5 ile başlatıyorum... Ve gördüğünüz gibi `Preferences` sayesinde seçtiğimiz son tema (Koyu tema) korunarak başladı."*

---

### ⏱️ 4:45 - 5:00 | 7. Adım: Kapanış
- **Ne Söyleyeceksin:**
  > *"Projemiz tüm işlevleri, hata yönetimi ve tema desteğiyle eksiksiz çalışmaktadır. Kodları GitHub üzerinden inceleyebilirsiniz. Dinlediğiniz için teşekkür ederim."*
