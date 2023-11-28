Projenin genel yapýsý

Mikroservisler

=>PhoneBook.Services.Person
   Person mikroservisi rehberdeki kiþiler ve kiþilerin iletiþim bilgilerinden sorumludur.
   DB olarak MongoDB ve içerisinde Persons ve ContactInfo koleksiyonlarý yer almaktadýr.



=>PhoneBook.Services.Person.Test
   xUnit kullanýlarak örnek bir birim test projesi


=>PhoneBook.Services.Report
  Report mikroservisi ise rapor ve raporlarýn asenkron olarak oluþturulmasýndan sorumludur.
  DB olarak PostgreSQL ve içerisinde report ve reportlocations tablolarý yer almaktadýr.
  ORM olarak Dapper'i tercih ettim.
  Raporlarýn asenkron olarak oluþturulmasý için RabbitMQ ve MassTransit kullanýlmýþtýr.
  Reports.Controllers içerisinde MassTransit ile "queue:create-report-service" kuyruðuna mesaj býrakýlarak, Consumers/CreateReportMessageCommandConsumer.cs içerisinde býrakýlan mesajlarýn sýrayla dinlenmesi saðlanmýþtýr.
  CreateReportMessageCommandConsumer sýnýfý içerisinde Report servisi Person servisi ile iletiþim kurarak rapor için gerekli bilgileri almaktadýr.


=>PhoneBook.Gateway
   Ocelot ile mikroservisler bir araya getirilmiþtir.


=>PhoneBook.Web
ASP.NET Core MVC projesi
    /Services
        ContactInfo
        Person
        Report
Servisleri ile gateway üzerinden mikroservislerle iletiþim kurulmuþtur.