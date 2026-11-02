# BSM352 / BSM322 Görsel Programlama - Ödev 1: Windows Hesap Makinesi Klonu (.NET MAUI)

Bu proje, Bartın Üniversitesi Bilgisayar Mühendisliği Bölümü **BSM352 / BSM322 Görsel Programlama** dersi 1. Ödevi kapsamında, **.NET MAUI** teknolojisi ve **C#** kullanılarak geliştirilmiş modern bir Windows Hesap Makinesi klonudur.

---

## 🎥 Video Anlatımı & Sunum
- **Video Linki:** [YouTube / Drive Video Linkinizi Buraya Ekleyin] *(Maksimum 5 dakika)*
- **Öğrenci Adı Soyadı:** [Adınız Soyadınız]
- **Öğrenci Numarası:** [Öğrenci Numaranız]

---

## 🌟 Proje Özellikleri ve Değerlendirme Kriterleri Karşılığı

### 1. Standart Mod (15 Puan)
- Toplama ($+$), çıkarma ($-$), çarpma ($\times$) ve bölme ($\div$) işlemleri.
- Ekstra işlemler: Yüzde ($\%$, $1/x$, $x^2$, $\sqrt{x}$).
- Düzenleme ve temizleme tuşları: $C$, $CE$, $\pm$, geri silme ($\backspace$), ondalık virgül ($,$).
- Hafıza tuşları ($MC, MR, M+, M-, MS$).
- Sonuçların ve işlem geçmişinin Windows Hesap Makinesi standartlarında gösterimi.

### 2. Bilimsel Mod (15 Puan)
- Trigonometrik fonksiyonlar: $\sin, \cos, \tan$ (Derece / Radyan modu desteği).
- Üstel ve logaritmik fonksiyonlar: $x^y, x^2, 10^x, \log_{10}, \ln$.
- Özel fonksiyonlar ve sabitler: $n!$ (Faktöriyel), $|x|$ (Mutlak değer), $\text{exp}$, $\text{mod}$, $\pi$, $e$.
- Genişletilmiş 7x5 duyarlı tuş takımı.

### 3. Sayfalar Arası Geçiş - Shell (5 Puan)
- .NET MAUI Shell (`AppShell.xaml`) mimarisi kullanılmıştır.
- Sol üstteki hamburger menü aracılığıyla **Standart**, **Bilimsel** ve **Ayarlar** sayfaları arasında akıcı, kararlı ve durum korumalı geçiş sağlanır.

### 4. Arayüz Tasarımı - XAML (10 Puan)
- Tüm arayüz saf XAML ile oluşturulmuştur.
- Grid yapısı, esnek satır ve sütun tanımlamaları (`*` ve `Auto`), Button ve Label bileşenleri en iyi pratiklere uygun olarak yerleştirilmiştir.

### 5. Stiller ve Kaynak Sözlükleri (10 Puan - Kritik Kriter)
- **Sayfalarda asla sabit renk veya font tanımlanmamıştır.**
- `Resources/Styles/Colors.xaml`: Koyu ve Açık temaya özel renk paletleri ve tema fırçaları.
- `Resources/Styles/Styles.xaml`: 
  - Butonlar için ortak **örtük (implicit)** stil (`<Style TargetType="Button">`).
  - `BasedOn` ile türetilmiş **açık (explicit)** stiller: `NumberButtonStyle`, `OperatorButtonStyle`, `EqualButtonStyle`, `ScientificButtonStyle`, `ClearButtonStyle`, `MemoryButtonStyle`.
  - Dinamik font ve kenar boşluğu kaynakları.

### 6. Ayarlar Sayfası ve Tema Yönetimi (15 Puan)
- Shell menüsünden erişilebilen **Ayarlar (Settings)** sayfası.
- Seçenekler:
  - **Açık (Light)**
  - **Koyu (Dark)**
  - **Sistem ayarını kullan (Use system setting)**
- Tema değişikliği `Application.Current.UserAppTheme` üzerinden anında tüm sayfalara yansır.
- `Preferences` API'si ile seçilen tema kalıcı olarak kaydedilir; uygulama yeniden başlatıldığında son tema otomatik olarak yüklenir.

### 7. Hata Yönetimi (10 Puan)
- **Sıfıra Bölme:** Kullanıcı $0$'a bölme yaptığında uygulama çökmez; ekranda belirgin `"Sıfıra bölünemez"` uyarısı gösterilir.
- **Geçersiz Girişler:** Negatif sayıların karekökü veya logaritması, tanımsız açılar ve faktöriyel taşmalarında `"Geçersiz giriş"` uyarısı verilir.

### 8. Kod Kalitesi ve Mimarisi (5 Puan)
- `CalculatorEngine.cs` altında hesaplama ve iş mantığı UI katmanından izole edilmiştir.
- Okunabilir, anlaşılır isimlendirmeler ve ayrıntılı Türkçe XML / kod içi açıklamalar yapılmıştır.

---

## 🚀 Projeyi Çalıştırma

1. Projeyi Visual Studio 2022 / 2026 ile açın (`CalculatorApp.sln`).
2. Üst menüden hedef platform olarak **Windows Machine** seçin.
3. **F5** tuşuna basarak veya yeşil **Oynat** butonuna tıklayarak uygulamayı derleyin ve çalıştırın.
