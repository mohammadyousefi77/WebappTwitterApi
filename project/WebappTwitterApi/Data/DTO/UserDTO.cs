﻿namespace WebappTwitterApi.Data.DTO
{
    public class UserDTO
    {

        public string Id { get; set; }

        public bool IsConfirmed { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public  string UserName { get; set; }

        public  string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
