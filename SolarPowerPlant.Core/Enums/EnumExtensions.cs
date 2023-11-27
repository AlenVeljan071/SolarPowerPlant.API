namespace SolarPowerPlant.Core.Enums
{
    public static class EnumExtensions
    {
        public static List<EnumValue> GetValues<T>()
        {
            List<EnumValue> values = new();
            foreach (var itemType in Enum.GetValues(typeof(T)))
            {
                values.Add(new EnumValue()
                {
                    Name = Enum.GetName(typeof(T), itemType)!,
                    Id = (int)itemType
                });
            }
            return values;
        }
    }
}
