Projenin genel yap�s�

Mikroservisler

=>PhoneBook.Services.Person
   Person mikroservisi rehberdeki ki�iler ve ki�ilerin ileti�im bilgilerinden sorumludur.
   DB olarak MongoDB ve i�erisinde Persons ve ContactInfo koleksiyonlar� yer almaktad�r.



=>PhoneBook.Services.Person.Test
   xUnit kullan�larak �rnek bir birim test projesi


=>PhoneBook.Services.Report
  Report mikroservisi ise rapor ve raporlar�n asenkron olarak olu�turulmas�ndan sorumludur.
  DB olarak PostgreSQL ve i�erisinde report ve reportlocations tablolar� yer almaktad�r.
  ORM olarak Dapper'i tercih ettim.
  Raporlar�n asenkron olarak olu�turulmas� i�in RabbitMQ ve MassTransit kullan�lm��t�r.
  Reports.Controllers i�erisinde MassTransit ile "queue:create-report-service" kuyru�una mesaj b�rak�larak, Consumers/CreateReportMessageCommandConsumer.cs i�erisinde b�rak�lan mesajlar�n s�rayla dinlenmesi sa�lanm��t�r.
  CreateReportMessageCommandConsumer s�n�f� i�erisinde Report servisi Person servisi ile ileti�im kurarak rapor i�in gerekli bilgileri almaktad�r.


=>PhoneBook.Gateway
   Ocelot ile mikroservisler bir araya getirilmi�tir.


=>PhoneBook.Web
ASP.NET Core MVC projesi
    /Services
        ContactInfo
        Person
        Report
Servisleri ile gateway �zerinden mikroservislerle ileti�im kurulmu�tur.