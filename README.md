# OV Launcher

OV Launcher, kullanıcıların favori oyunlarını hızlı ve kolay bir şekilde başlatmalarını sağlayan bir Windows uygulamasıdır. Bu uygulama, oyunların yürütülebilir dosyalarını seçmek, oyun isimlerini özelleştirmek ve oyunların başlatılmasını kolaylaştırmak için tasarlanmıştır. Ayrıca, karanlık ve aydınlık temalar arasında geçiş yapabilme, otomatik başlatma ayarları yapabilme ve oyunların resimlerini değiştirebilme gibi ekstra özellikler de sunar.

## Özellikler

### Oyun Yönetimi
- Oyunların yürütülebilir dosyalarını seçin ve kaydedin.

### Oyun İsimleri Özelleştirme
- Oyunların isimlerini özelleştirin ve kaydedin.

### Tema Seçenekleri
- Karanlık ve aydınlık temalar arasında geçiş yapın.

### Otomatik Başlatma
- Uygulamayı Windows başlangıcında otomatik olarak başlatın.

### Resim Değiştirme
- Oyunların resimlerini özelleştirin.

### Sıfırlama Seçenekleri
- Tüm ayarları varsayılanlara sıfırlayın.

## Kurulum

### Projeyi Klonlayın
```bash
git clone https://github.com/rmco3/OV-Launcher.git
```

### Proje Dizinine Gidin
```bash
cd OV-Launcher
```

### Gerekli Paketleri Yükleyin
```bash
dotnet restore
```

### Uygulamayı Derleyin ve Çalıştırın
```bash
dotnet build
dotnet run
```

## Kullanım

### Oyun Ekleme
- "Oyun Seç" butonuna tıklayarak oyunun yürütülebilir dosyasını seçin. Seçilen dosya yolu otomatik olarak kaydedilecektir.
- İlgili textbox'a yeni bir isim girin ve "İsim Ekle" butonuna tıklayın. Yeni isim otomatik olarak kaydedilecektir.
- "Resim Seç" linkine tıklayarak yeni bir resim dosyası seçin. Seçilen resim otomatik olarak kaydedilecektir.

### Tema Seçimi
- "Karanlık Tema" radyo butonunu seçin.
- "Aydınlık Tema" radyo butonunu seçin.

### Otomatik Başlatma
- "Otomatik Başlat" checkbox'ını işaretleyin.
- "Otomatik Başlat" checkbox'ının işaretini kaldırın.

### Ayarları Sıfırlama
- "Varsayılanlara Dön" butonuna tıklayarak tüm ayarları varsayılanlara sıfırlayın.

## Katkıda Bulunma
Projeye katkıda bulunmak isterseniz, lütfen aşağıdaki adımları takip edin:

1. Projeyi forklayın.
2. Yeni bir branch oluşturun: `git checkout -b feature/yeni-ozellik`.
3. İstediğiniz değişiklikleri yapın ve bunları commitleyin: `git commit -m "Yeni özellik eklendi"`.
4. Değişikliklerinizi forkladığınız repoya pushlayın: `git push origin feature/yeni-ozellik`.
5. Orijinal repoda bir pull request açın.

## Lisans
Bu proje MIT lisansı altında lisanslanmıştır. Daha fazla bilgi için LICENSE.md dosyasına bakın.

## İletişim
Herhangi bir soru veya öneriniz varsa, lütfen bana rmco3hub@gmail.com adresinden ulaşın veya GitHub üzerinden bir issue açın.
