﻿namespace CargoApi.Models.AccountModels
{
    public class RegisterRequestModel
    {        
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
    }
}
