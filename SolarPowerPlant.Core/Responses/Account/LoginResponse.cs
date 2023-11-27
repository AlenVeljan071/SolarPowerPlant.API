namespace SolarPowerPlant.Core.Responses.Account
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? RefreshToken { get; set; }

    }
}
