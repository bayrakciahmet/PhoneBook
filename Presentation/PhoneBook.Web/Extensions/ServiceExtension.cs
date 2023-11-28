using PhoneBook.Web.Models;
using PhoneBook.Web.Services.ContactInfo;
using PhoneBook.Web.Services.Person;
using PhoneBook.Web.Services.Report;

namespace PhoneBook.Web.Extensions
{
    public static class ServiceExtension
    {
        public static void AddHttpClientServices(this IServiceCollection services, IConfiguration Configuration)
        {
            var serviceApiSettings = Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();

            services.AddHttpClient<IPersonService, PersonService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Person.Path}");
            });
            services.AddHttpClient<IContactInfoService, ContactInfoService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Person.Path}");
            });
            services.AddHttpClient<IReportService, ReportService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Report.Path}");
            });

        }
    }
}
