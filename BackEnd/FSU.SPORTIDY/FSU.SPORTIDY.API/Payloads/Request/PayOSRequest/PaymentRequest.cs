﻿namespace FSU.SPORTIDY.API.Payloads.Request.PayOSRequest
{
    public class PaymentRequest
    {
        public decimal amount { get; set; }
        public string description { get; set; }
        public string buyerName { get; set; }
        public string buyerPhone { get; set; }
        public string UserId {  get; set; }
        public string playfieldName { get; set; }
        public int playfieldId { get; set; }
        public int hour {  get; set; }
    }
}