﻿namespace PhoneBook.Web.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }  
        public string UserName { get; set; }
        public string EMail { get; set; }
        public string Title { get; set; }

        public IEnumerable<string> GetUserProps()
        {
            yield return UserName;
            yield return EMail;
            yield return Title;
        }
    }
}
