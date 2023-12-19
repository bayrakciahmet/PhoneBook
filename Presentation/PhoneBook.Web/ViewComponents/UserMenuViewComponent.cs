using Microsoft.AspNetCore.Mvc;
using PhoneBook.Web.Services.Interfaces;

namespace PhoneBook.Web.ViewComponents
{
    [ViewComponent]
    public class UserMenuViewComponent : ViewComponent
    {
        private readonly IUserService _userService;

        public UserMenuViewComponent(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _userService.GetUser());
        }
    }
}
