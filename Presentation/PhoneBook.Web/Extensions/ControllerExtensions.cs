using Microsoft.AspNetCore.Mvc;
using PhoneBook.Web.Models;

namespace PhoneBook.Web.Extensions
{
    internal static class ControllerExtensions
    {
        public static JsonResult CustomJsonResponse(this ControllerBase controller,
        string title = "",
        string info = "",
        bool? popup = null,
        int? id = null,
        string error = "",
        string url = "",
        bool urlTime = false,
        string functionName = "")
        {
            MyCustomResponse response = new MyCustomResponse()
            {
                Title = title,
                Info = info,
                Popup = popup,
                Id = id,
                Error = error,
                UrlTime = urlTime,
                Url = url,
                FunctionName = functionName
            };
            return new JsonResult(response);

        }
    }
}
