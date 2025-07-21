Harika\! İstediğin README dosyasını LaTeX formatına dönüştürmeden, doğrudan markdown olarak aşağıda bulabilirsin. Bunu kopyalayıp GitHub projenin ana dizinine `README.md` adıyla kaydedebilirsin.

-----

# 🧠 BLUESENSE Backend Engineer Assignment - Gerçek Zamanlı Sohbet Sistemi

Bu proje, Bluesense için hazırlanan **gerçek zamanlı bir sohbet sistemi backend** uygulamasıdır. Üretim kalitesine yakın bir kod altyapısı sunmayı hedefler. API katmanı tamamlanmış, temel işlevler geliştirilmiş ve **Docker** ile kolayca çalıştırılabilir hale getirilmiştir.

-----

## ✅ Özellikler

Sistem, modern bir sohbet uygulamasından beklenen temel özellikleri ve daha fazlasını sunar:

### 🔐 Kimlik Doğrulama (Auth)

  * **JWT Tabanlı Erişim ve Yenileme Tokenları:** Güvenli ve süresi yönetilebilir kimlik doğrulama mekanizması.
  * **Giriş/Kayıt Uç Noktaları:** Kullanıcıların sisteme güvenli bir şekilde erişmesini sağlar.
  * **Token Yenileme:** Kullanıcı oturumlarının kesintisiz devamlılığı için token yenileme özelliği.

### 👥 Gruplar

  * **Genel ve Özel Grup Oluşturma:** Kullanıcıların ihtiyaçlarına göre farklı türde sohbet ortamları oluşturabilir.
  * **Davet ve Onay Mekanizması:** Özel gruplara katılımı kontrol altında tutar.
  * **Grup Üyelerini Yönetme:** Yöneticilerin (admin) ve üyelerin (member) rollerini belirleme ve yönetme yeteneği.

### 💬 Mesajlar

  * **Dosya Yükleme Desteği:** Kullanıcıların mesajlarına dosya eklemesine olanak tanır (dosyalar yerel diskte saklanır).
  * **Mesaj Düzenleme ve Silme:** Gönderilen mesajlar düzenlenebilir veya (soft delete ile) silinebilir.
  * **Sayfalanabilir Mesaj Listesi:** Büyük gruplardaki mesaj geçmişini verimli bir şekilde görüntüleme.
  * **Tam Metin Arama:** Mesajlar arasında hızlı ve etkili arama yapabilme.

### ⚡ Gerçek Zamanlı İletişim (SignalR + Redis)

  * **Anlık Mesaj Gönderimi:** SignalR ile gerçek zamanlı mesajlaşma deneyimi.
  * **Redis Entegrasyonu:** SignalR'ın ölçeklenebilirliğini artırarak yüksek performans sağlar.

-----

## 🧱 Teknolojiler ve Mimari

Proje, modern ve sağlam bir mimari üzerine kurulmuştur:

  * **ASP.NET Core Web API (.NET 8):** Yüksek performanslı ve güvenilir bir backend altyapısı.
  * **PostgreSQL:** Güçlü ve esnek bir ilişkisel veritabanı.
  * **Redis:** Hızlı önbellekleme ve SignalR ölçeklenebilirliği için kullanılır.
  * **SignalR:** Gerçek zamanlı iki yönlü iletişim için kullanılan kütüphane.
  * **MediatR + CQRS + Onion Architecture:** Temiz kod, sürdürülebilirlik ve test edilebilirliği artıran mimari desenler.
  * **Docker + Docker Compose:** Uygulamanın kolayca dağıtılabilir ve izole edilmiş ortamda çalışmasını sağlar.
  * **GitHub Actions ile CI Pipeline:** Otomatik build ve test süreçleri.
  * **xUnit ile Birim ve Entegrasyon Testleri:** Uygulamanın güvenilirliğini ve doğruluğunu sağlayan kapsamlı testler.

-----

## 🚀 Kurulum ve Çalıştırma

Uygulamayı yerel ortamınızda çalıştırmak oldukça kolaydır.

### 1\. Gereksinimler

Başlamadan önce aşağıdaki araçlara sahip olduğunuzdan emin olun:

  * [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
  * [Docker Desktop](https://www.docker.com/products/docker-desktop) (Docker ve Docker Compose içerir)

### 2\. Çalıştırma Adımları

1.  Projeyi klonlayın:
    ```bash
    git clone https://github.com/sedatcnn/Real_Chat.git
    cd Real_Chat
    ```
2.  Docker Compose ile uygulamayı ayağa kaldırın:
    ```bash
    docker-compose up --build
    ```
    Bu komut, uygulamanın API, WebUI, PostgreSQL, Redis ve pgAdmin servislerini otomatik olarak başlatacaktır. SignalR, Redis ile entegre bir şekilde çalışacaktır.

### 3\. Uygulama Adresleri

Uygulama başarıyla başlatıldığında aşağıdaki adreslerden erişilebilir:

  * **API:** `http://localhost:5000/`
  * **WebUI (Beta):** `http://localhost:5291/`

-----

## 🛠 API Kullanımı

Tüm API uç noktaları `/api` öneki altında bulunur. İşte bazı temel uç noktalar:

### Kimlik Doğrulama (Auth)

  * `POST /api/Login/login`
  * `POST /api/Register/register`
  * `POST /api/Auth/refresh`

### Gruplar

  * `GET /api/Group`
  * `POST /api/Group`
  * `POST /api/Group/join-request`
  * `PUT /api/Group/approve-request`

### Mesajlar

  * `GET /api/Message/group/{groupId}`
  * `POST /api/Message`
  * `PUT /api/Message/{id}`
  * `DELETE /api/Message/{id}`

-----

## 🔄 CI/CD

Proje, **GitHub Actions** ile entegre bir Sürekli Entegrasyon (CI) hattına sahiptir.

### CI Pipeline

  * Bir `pull request` açıldığında veya `main` branch'ine `push` yapıldığında otomatik olarak tetiklenir.
  * Aşamalar:
      * **Build (.NET):** Proje başarıyla derlenir.
      * **Lint:** Kod kalitesi kontrolü yapılır.
      * **Test:** **xUnit** ile yazılan birim ve entegrasyon testleri çalıştırılır ve başarılı olduğu doğrulanır.

*Not: Sürekli Dağıtım (CD) aşaması (otomatik deployment) henüz eklenmemiştir.*

-----

## 📦 Deployment

Uygulama, Docker ile kapsayıcılı (containerized) hale getirilmiştir. `docker-compose.yml` dosyası, tüm servislerin (API, Web UI, PostgreSQL, Redis, pgAdmin) geliştirme ortamında birlikte çalışmasını sağlar.

Bu yapı, geliştirme ortamı için yeterli olsa da, gerçek bir sunucuya taşımak için örnek `.env` dosyası ve Nginx gibi bir reverse proxy konfigürasyonları eklenmesi faydalı olacaktır.

-----

## 📸 Ekran Görüntüleri
<img width="1915" height="961" alt="image" src="https://github.com/user-attachments/assets/0a3b5625-4ed2-444b-ae06-30efe9091f46" />
<img width="1912" height="957" alt="image" src="https://github.com/user-attachments/assets/dc615c74-a03a-4f65-89fc-1749eac9f236" />
Planlanan UI tasarımı 

### API Dokümantasyonu Görünümü
<img width="1906" height="970" alt="image" src="https://github.com/user-attachments/assets/bffb5f4c-cc8c-45a9-b752-110ea8bf022f" />
<img width="1886" height="679" alt="image" src="https://github.com/user-attachments/assets/54e0c400-4ab5-4a27-905d-c13dbc6442d2" />

### Docker Çalışma Anı
<img width="1592" height="661" alt="image" src="https://github.com/user-attachments/assets/d607c681-ed63-4c3a-b3f3-e93ca8fc68e8" />
<img width="1588" height="904" alt="image" src="https://github.com/user-attachments/assets/3f31ea2b-cffa-49f6-9c6e-6659ac6d0b54" />
<img width="1911" height="963" alt="image" src="https://github.com/user-attachments/assets/566da40d-c13d-46d0-bc14-cefa6a41ec73" />
### Test Çalışma Anı
<img width="1110" height="177" alt="image" src="https://github.com/user-attachments/assets/a2a794f3-320d-4b71-85c9-179fb0aef792" />

-----

## ⚠️ Notlar

  * **Web UI (Beta):** Projedeki basit UI yapısı geliştirme aşamasındadır ve eksiklikleri bulunmaktadır.
  * **Gerçek Sunucu Dağıtımı:** Uygulamanın bir VPS veya bulut sağlayıcıya taşınması için kapsamlı bir dokümantasyon henüz sağlanmamıştır.

-----
