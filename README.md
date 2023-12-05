Projenin Çalıştırılması
=>Proje dizininde "docker-compose build" ve ardından  "docker-compose up" komutu ile projenin docker üzerinden ayağa kalması sağlanır. 
  -reportApi servisinde postgresql database ye otomatik olarak docker-compose yapılırken tablolar create edilmektedir. PhoneBook/sql/create_tables.sql
  -docker-compose up işlemi tamamlandıktan sonra hiç bir ek konfigürasyon yapmadan http://localhost:6010/ web projesine erişilebilir.

Services
  -personApi => http://localhost:6011/swagger/index.html
  -reportApi => http://localhost:6012/swagger/index.html
Gateway => http://localhost:6000/
  Gateway üzerinde bir swagger bulunmamaktadır.
  PhoneBook.Gateway/configuration.production.json route yapılanması
Web => http://localhost:6010/


Projenin genel yapısı
Mikroservisler

=>PhoneBook.Services.Person
  Person mikroservisi rehberdeki kişiler ve kişilerin iletişim bilgilerinden sorumludur.
  DB olarak MongoDB ve içerisinde Persons ve ContactInfo koleksiyonları yer almaktadır.

=>PhoneBook.Services.Report
  Report mikroservisi ise rapor ve raporların asenkron olarak oluşturulmasından sorumludur.
  DB olarak PostgreSQL ve içerisinde report ve reportlocations tabloları yer almaktadır.
  Raporların asenkron olarak oluşturulması için RabbitMQ ve MassTransit kullanılmıştır.

=>Mikroservisler Arası iletişim
  PhoneBook.Services.Report/Controllers/ReportsController içerisinde Create Methodunda MassTransit ile "queue:create-report-service" kuyruğuna mesaj bırakılarak, 
  PhoneBook.Services.Report/Consumers/CreateReportMessageCommandConsumer.cs içerisinde bırakılan mesajların sırayla dinlenmesi sağlanmıştır.
  CreateReportMessageCommandConsumer sınıfı içerisinde Report servisi PhoneBook.Services.Person servisi ile REST protokolü ile iletişim kurarak 
  rapor için gerekli bilgileri almaktadır.


=>PhoneBook.Gateway
   Ocelot ile mikroservisler bir araya getirilmiştir.

=>PhoneBook.Web
ASP.NET Core MVC projesi
    /Services
        ContactInfo
        Person
        Report
Servisleri ile gateway üzerinden mikroservislerle iletişim kurulmuştur.


Unit Test Projeleri
=>PhoneBook.Presentation.Web.Test
=>PhoneBook.Services.Person.Test
=>PhoneBook.Services.Report.Test
