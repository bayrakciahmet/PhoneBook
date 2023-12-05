Projenin �al��t�r�lmas�
=>Proje dizininde "docker-compose build" ve ard�ndan  "docker-compose up" methodu ile projenin docker �zerinden aya�a kalmas� sa�lan�r. 
  -reportApi servisinde postgresql database ye otomatik olarak docker-compose yap�l�rken tablolar create edilmektedir. PhoneBook/sql/create_tables.sql
  -docker-compose up i�lemi tamamland�ktan sonra hi� bir ek konfig�rasyon yapmadan http://localhost:6010/ web projesine eri�ilebilir.

Services
  -personApi => http://localhost:6011/swagger/index.html
  -reportApi => http://localhost:6012/swagger/index.html
Gateway => http://localhost:6000/
  Gateway �zerinde bir swagger bulunmamaktad�r.
  PhoneBook.Gateway/configuration.production.json route yap�lanmas�
Web => http://localhost:6010/


Projenin genel yap�s�
Mikroservisler

=>PhoneBook.Services.Person
  Person mikroservisi rehberdeki ki�iler ve ki�ilerin ileti�im bilgilerinden sorumludur.
  DB olarak MongoDB ve i�erisinde Persons ve ContactInfo koleksiyonlar� yer almaktad�r.

=>PhoneBook.Services.Report
  Report mikroservisi ise rapor ve raporlar�n asenkron olarak olu�turulmas�ndan sorumludur.
  DB olarak PostgreSQL ve i�erisinde report ve reportlocations tablolar� yer almaktad�r.
  Raporlar�n asenkron olarak olu�turulmas� i�in RabbitMQ ve MassTransit kullan�lm��t�r.

=>Mikroservisler Aras� ileti�im
  PhoneBook.Services.Report/Controllers/ReportsController i�erisinde Create Methodunda MassTransit ile "queue:create-report-service" kuyru�una mesaj b�rak�larak, 
  PhoneBook.Services.Report/Consumers/CreateReportMessageCommandConsumer.cs i�erisinde b�rak�lan mesajlar�n s�rayla dinlenmesi sa�lanm��t�r.
  CreateReportMessageCommandConsumer s�n�f� i�erisinde Report servisi PhoneBook.Services.Person servisi ile REST protokol� ile ileti�im kurarak 
  rapor i�in gerekli bilgileri almaktad�r.


=>PhoneBook.Gateway
   Ocelot ile mikroservisler bir araya getirilmi�tir.

=>PhoneBook.Web
ASP.NET Core MVC projesi
    /Services
        ContactInfo
        Person
        Report
Servisleri ile gateway �zerinden mikroservislerle ileti�im kurulmu�tur.


Unit Test Projeleri
=>PhoneBook.Presentation.Web.Test
=>PhoneBook.Services.Person.Test
=>PhoneBook.Services.Report.Test