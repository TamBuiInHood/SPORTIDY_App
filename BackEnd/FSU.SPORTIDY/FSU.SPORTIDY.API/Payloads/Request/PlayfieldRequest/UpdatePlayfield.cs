namespace FSU.SPORTIDY.API.Payloads.Request.PlayfieldRequest
{
    public class UpdatePlayfield
    {
        public string? playfieldName { get; set; }

        public int? price { get; set; }

        public string? address { get; set; }

        public TimeOnly? openTime { get; set; }

        public TimeOnly? closeTime { get; set; }

        public int status { get; set; }
    }
}
