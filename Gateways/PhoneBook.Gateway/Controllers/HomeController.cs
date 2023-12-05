﻿using Microsoft.AspNetCore.Mvc;

namespace PhoneBook.Gateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
