# ❄️Snowly - TR

Snowly Backend, gerçek zamanlı mesajlaşma uygulamasını kontrol eden bir backend servisidir
Kullanıcılar arası iletişimi sağlamak, mesaj yönetimini gerçekleştirmek ve güvenli veri akışı sunmak amacıyla tasarlanmıştır

## Özellikler

* Gerçek zamanlı mesajlaşma altyapısı (SignalR ile sağlıyorum)
* Kullanıcı yönetimi
* Kimlik doğrulama ve yetkilendirme (JWT kullandım)
* RESTful API (Uygulamanın dışarıya açılması için API yapısı kurdum)
* Veritabanı entegrasyonu (PostgreSQL kullandım ve veritabanımı Neon.com üzerinden oluşturdum)
* MediatR kütüphanesini kullandım (Her işlem için özel handlerlar yazmak geliştirilmeyi esnek hale getiriyor)

## Kullanılan Teknolojiler

* .NET Core (WebAPI geliştirme)
* MediatR (CQRS ve loose coupling için)
* PostgreSQL (İlişkisel veritabanı)
* Neon (Veritabanı barınması için)
* Render (Projenin canlıya alınması için)
* FluentValidation (API istekleri sırasında doğrulama yapmak için)
* SignalR (Gerçek zamanlı iletişim için)
* JWT (Kullanıcıların kimliklerinin doğrulanması ve uygulama içerisinde yetkilendirme)


## Proje Yapısı

Projeyi, Clean Architecture prensiplerine uygun olarak katmanlı bir yapı ile geliştirdim
Katmanlar arası bağımlılık minimum seviyede tuttum ve iş mantığı ayrıştırdım

* **Application** (Uygulamanın iş mantığı / CQRS yapısı ile Handlerları burada yazdım)
* **Domain** (Uygulamanın sınıflarının bulunduğu katman)
* **Infrastructure** (Uygulamanın veritabanı bağlantısının ve işlemlerinin yapıldığı katman)
* **WebAPI** (Uygulamanın dışarıya açılan kısmı / HTTP isteklerinin karşılandığı katman)
  
<img width="297" height="112" alt="Ekran görüntüsü 2026-04-13 202229" src="https://github.com/user-attachments/assets/54669aac-ca4a-424d-a7cf-ec74d741d903" />

--- 

**Katmanların içeriği detaylı olarak aşağıdadır**


<img width="300" height="787" alt="Ekran görüntüsü 2026-04-13 205511" src="https://github.com/user-attachments/assets/474be011-7c4e-41db-a2ba-de7143707463" />

## Modeller (Entities)

* **BaseEntity** 

Tüm entity’lerde ortak olarak bulunan temel özellikleri içerir
(Örneğin: Id, CreatedDate, UpdatedDate gibi alanlar)

<img width="347" height="268" alt="Ekran görüntüsü 2026-04-13 210143" src="https://github.com/user-attachments/assets/8c6fec14-ec47-4c28-8b57-b2bc0e9ebc2c" />

---

* **User**

Kullanıcı bilgilerini temsil eden temel entity yapısıdır
Sistemdeki kullanıcıların kimlik ve profil bilgilerini içerir

<img width="432" height="614" alt="Ekran görüntüsü 2026-04-13 210216" src="https://github.com/user-attachments/assets/0743a10d-c7e9-4840-96d0-d040cd639a9e" />

---

* **Message**

Kullanıcılar arasındaki mesajlar için entity yapısı

<img width="328" height="274" alt="Ekran görüntüsü 2026-04-13 210238" src="https://github.com/user-attachments/assets/a4e85969-7c04-4e48-a700-eda80342476e" />

--- 

* **UserConfirmCode**

Kullanıcıların kayıt esnasında E-posta adreslerine gönderilen kodların entity yapısı

<img width="343" height="257" alt="Ekran görüntüsü 2026-04-13 210224" src="https://github.com/user-attachments/assets/f72b73d1-9f3f-4963-a749-5bc22ce2ad11" />

--- 

* **Group**

Kullanıcılar arasında grup sistemi için entity yapısı

<img width="348" height="223" alt="Ekran görüntüsü 2026-04-13 210341" src="https://github.com/user-attachments/assets/ca82beb1-787b-4371-8f8c-b99169a89abf" />

---

* **RefreshToken**

Kimliklendirme ve yetkilendirmede kullanılan JWT için süre aşımında yenileme tokeni entity yapısı

<img width="424" height="206" alt="Ekran görüntüsü 2026-04-13 210353" src="https://github.com/user-attachments/assets/ed2f0345-06e8-4544-b2c0-579a0d3b2697" />

---

* **UserGroup**

Kullanıcının hangi gruplarda bulunduğunu tutan entity yapısı

<img width="288" height="236" alt="Ekran görüntüsü 2026-04-13 210228" src="https://github.com/user-attachments/assets/9a8ae8df-56c3-487d-982c-fdf11044a4a0" />




