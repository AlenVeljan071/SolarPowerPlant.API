namespace SolarPowerPlant.API.Errors
{
    public class NotFoundException : GlobalException
    {
        public NotFoundException(string code, string message) : base(code, message)
        {
        }
    }
}
