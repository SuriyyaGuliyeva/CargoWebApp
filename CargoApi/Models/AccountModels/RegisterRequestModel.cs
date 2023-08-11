namespace CargoApi.Models.AccountModels
{
    public class RegisterRequestModel
    {        
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }
        public string PinCode { get; set; }
        public string Address { get; set; }
    }
}
