namespace FSU.SPORTIDY.Repository.Utils
{
    public class NumberTypeHelper
    {
        public static bool IsValidInteger(int value)
        {
            return value >= int.MinValue && value <= int.MaxValue;
        }
        public static bool IsValidDecimal(decimal value)
        {
            return value >= decimal.MinValue && value <= decimal.MaxValue;
        }

    }
}
