Projenin Çalıştırılması
=>Proje dizininde "docker-compose build" ve ardından  "docker-compose up" komutu ile projenin docker üzerinden ayağa kalması sağlanır. 

  -reportApi servisinde postgresql database otomatik olarak docker-compose yapılırken tablolar create edilmektedir. PhoneBook/sql/create_tables.sql
  
  -docker-compose up işlemi tamamlandıktan sonra hiç bir ek konfigürasyon yapmadan http://localhost:6010/ web projesine erişilebilir.

Services

  -personApi => http://localhost:6011/swagger/index.html
  
  -reportApi => http://localhost:6012/swagger/index.html
  
Gateway => http://localhost:6001/swagger/index.html
  Gateway üzerine swagger konfigürasyonu eklenmiştir.
  PhoneBook.Gateway/configuration.production.json Routes ve SwaggerEndPoints.
  
Web => http://localhost:6010/


Projenin genel yapısı
Mikroservisler

=>PhoneBook.Services.Person
  Person mikroservisi rehberdeki kişiler ve kişilerin iletişim bilgilerinden sorumludur.
  DB olarak MongoDB ve içerisinde Persons ve ContactInfo koleksiyonları yer almaktadır.

=>PhoneBook.Services.Report
  Report mikroservisi ise rapor ve raporların asenkron olarak oluşturulmasından sorumludur.
  DB olarak PostgreSQL ve içerisinde report ve reportlocations tabloları yer almaktadır.
  Message Queue sistemi için RabbitMQ tercih edilmiştir.

=>Mikroservisler Arası iletişim

  PhoneBook.Services.Report/Controllers/ReportsController içerisinde Create Methodunda MassTransit ile "queue:create-report-service" kuyruğuna mesaj bırakılarak, 
  PhoneBook.Services.Report/Consumers/CreateReportMessageCommandConsumer.cs içerisinde bırakılan mesajların sırayla dinlenmesi sağlanmıştır.
  CreateReportMessageCommandConsumer sınıfı içerisinde PhoneBook.Services.Person servisi ile Gateway üzerinden iletişim kurularak raporun detay bilgileri alınır.


=>PhoneBook.Gateway

=>PhoneBook.Web

.NET Core MVC projesi

     /Controllers

        PersonsController.cs

        ContactInfosController.cs

        ReportsController.cs
        
    /Services
    
        ContactInfoService.cs
        
        PersonService.cs

        ReportService.cs
        
Servisleri ile gateway üzerinden mikroservislerle iletişim kurulmuştur.


Unit Test Projeleri

=>PhoneBook.Presentation.Web.Test

=>PhoneBook.Services.Person.Test

=>PhoneBook.Services.Report.Test
