using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Web.Models;

namespace PhoneBook.Web.Helpers
{
    public class ControllerHelper
    {
        public static JsonResult GenerateModelErrorJsonResult(ModelStateDictionary modelState)
        {
            return new JsonResult(new GenerateModelError
            {
                Title = LocalizationHelper.ValidationFailedTitle,
                Error = LocalizationHelper.ValidationFailedError + string.Join(", ", modelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))
            });
        }
    }
}
