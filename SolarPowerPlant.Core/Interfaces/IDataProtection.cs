namespace SolarPowerPlant.Core.Interfaces
{
    public interface IDataProtection
    {
        public string GenerateSalt();
        public string Hash(string password, string salt);
        public string Encrypt(string plainText);
        public string Decrypt(string cipherText);

    }
}
