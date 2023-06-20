namespace CargoApi.Models.AccountModels
{
    public class LoginRequestModel
    {
        public string Name { get; set; }
        public string PasswordHash { get; set; }
    }
}
