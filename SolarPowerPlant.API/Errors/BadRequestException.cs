namespace SolarPowerPlant.API.Errors
{
    public class BadRequestException : GlobalException
    {
        public BadRequestException(string code, string message) : base(code, message)
        {
        }
    }
}
