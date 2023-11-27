namespace SolarPowerPlant.API.Errors
{
    public class GlobalException : Exception
    {
        public string Code { get; set; }

        public GlobalException(string code, string message) : base(message)
        {
            Code = code;
        }

        public GlobalException(string code, string message, Exception innerException) : base(message, innerException)
        {
            Code = code;
        }

    }
}
