namespace FSU.SPORTIDY.API.Payloads.Request.PayOSRequest
{
    public class PaymentRequest
    {
        public string bookingCode { get; set; }
        public decimal amount { get; set; }
        public string description { get; set; }
        public string buyerName { get; set; }
        public string buyerPhone { get; set; }
        public string userId {  get; set; }
        public string playfieldName { get; set; }
        public int playfieldId { get; set; }
        public int hour {  get; set; }
    }
}
