namespace FSU.SPORTIDY.API.Payloads.Request.PlayfieldRequest
{
    public class UpdatePlayfield
    {
        public string? PlayFieldName { get; set; }

        public int? Price { get; set; }

        public string? Address { get; set; }

        public TimeOnly? OpenTime { get; set; }

        public TimeOnly? CloseTime { get; set; }
    }
}
