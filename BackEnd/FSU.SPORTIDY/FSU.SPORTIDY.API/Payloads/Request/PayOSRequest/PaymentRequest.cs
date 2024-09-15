namespace FSU.SPORTIDY.API.Payloads.Request.PayOSRequest
{
    public class PaymentRequest
    {

        public int? orderCode { get; set; }
        //public int? amount { get; set; }
        //public string? description { get; set; }
        //public string? buyerName { get; set; }
        //public string? buyerEmail { get; set; }
        //public string? buyerPhone { get; set; }
        //public string? buyerAddress { get; set; }
        //public List<object>? items { get; set; }
        //public string? cancelUrl { get; set; }
        //public string? returnUrl { get; set; }
        //public int? expiredAt { get; set; }
        public string? signature { get; set; }

    }
}
