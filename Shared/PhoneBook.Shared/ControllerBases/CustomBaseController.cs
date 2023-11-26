using Microsoft.AspNetCore.Mvc;
using PhoneBook.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.Shared.ControllerBases
{
    public class CustomBaseController
    {
        public IActionResult CreateActionResultInstance<T>(Response<T> response)
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode,
            };
        }
    }
}
