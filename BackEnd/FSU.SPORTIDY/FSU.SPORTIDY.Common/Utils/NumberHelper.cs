namespace FSU.SPORTIDY.Common.Utils
{
    public class NumberHelper
    {
        public static bool IsValidInteger(int value)
        {
            return value >= int.MinValue && value <= int.MaxValue;
        }
        public static bool IsValidDecimal(decimal value)
        {
            return value >= decimal.MinValue && value <= decimal.MaxValue;
        }
        public static int GenerateSixDigitNumber()
        {
            Random random = new Random();
            int number = random.Next(100000, 1000000);
            return number;
        }

        public static long GenerateRandomLong()
        {
            Guid guid = Guid.NewGuid();
            byte[] bytes = guid.ToByteArray();
            return BitConverter.ToInt64(bytes, 0);
        }
    }
}
