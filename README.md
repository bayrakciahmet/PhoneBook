Projenin Çalýþtýrýlmasý
=>Proje dizininde "docker-compose build" ve ardýndan  "docker-compose up" methodu ile projenin docker üzerinden ayaða kalmasý saðlanýr. 
  -reportApi servisinde postgresql database ye otomatik olarak docker-compose yapýlýrken tablolar create edilmektedir. PhoneBook/sql/create_tables.sql
  -docker-compose up iþlemi tamamlandýktan sonra hiç bir ek konfigürasyon yapmadan http://localhost:6010/ web projesine eriþilebilir.

Services
  -personApi => http://localhost:6011/swagger/index.html
  -reportApi => http://localhost:6012/swagger/index.html
Gateway => http://localhost:6000/
  Gateway üzerinde bir swagger bulunmamaktadýr.
  PhoneBook.Gateway/configuration.production.json route yapýlanmasý
Web => http://localhost:6010/


Projenin genel yapýsý
Mikroservisler

=>PhoneBook.Services.Person
  Person mikroservisi rehberdeki kiþiler ve kiþilerin iletiþim bilgilerinden sorumludur.
  DB olarak MongoDB ve içerisinde Persons ve ContactInfo koleksiyonlarý yer almaktadýr.

=>PhoneBook.Services.Report
  Report mikroservisi ise rapor ve raporlarýn asenkron olarak oluþturulmasýndan sorumludur.
  DB olarak PostgreSQL ve içerisinde report ve reportlocations tablolarý yer almaktadýr.
  Raporlarýn asenkron olarak oluþturulmasý için RabbitMQ ve MassTransit kullanýlmýþtýr.

=>Mikroservisler Arasý iletiþim
  PhoneBook.Services.Report/Controllers/ReportsController içerisinde Create Methodunda MassTransit ile "queue:create-report-service" kuyruðuna mesaj býrakýlarak, 
  PhoneBook.Services.Report/Consumers/CreateReportMessageCommandConsumer.cs içerisinde býrakýlan mesajlarýn sýrayla dinlenmesi saðlanmýþtýr.
  CreateReportMessageCommandConsumer sýnýfý içerisinde Report servisi PhoneBook.Services.Person servisi ile REST protokolü ile iletiþim kurarak 
  rapor için gerekli bilgileri almaktadýr.


=>PhoneBook.Gateway
   Ocelot ile mikroservisler bir araya getirilmiþtir.

=>PhoneBook.Web
ASP.NET Core MVC projesi
    /Services
        ContactInfo
        Person
        Report
Servisleri ile gateway üzerinden mikroservislerle iletiþim kurulmuþtur.


Unit Test Projeleri
=>PhoneBook.Presentation.Web.Test
=>PhoneBook.Services.Person.Test
=>PhoneBook.Services.Report.Test