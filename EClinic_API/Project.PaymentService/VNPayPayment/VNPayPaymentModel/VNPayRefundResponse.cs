using Newtonsoft.Json;

namespace Project.PaymentService.VNPayPayment.VNPayPaymentModel
{
    public class VNPayRefundResponse
    {
        [JsonProperty("vnp_ResponseId")]
        public string ResponseId { get; set; }

        [JsonProperty("vnp_Command")]
        public string Command { get; set; }

        [JsonProperty("vnp_ResponseCode")]
        public string ResponseCode { get; set; }

        [JsonProperty("vnp_Message")]
        public string Message { get; set; }

        [JsonProperty("vnp_TmnCode")]
        public string TmnCode { get; set; }

        [JsonProperty("vnp_TxnRef")]
        public string TxnRef { get; set; }

        [JsonProperty("vnp_Amount")]
        public string Amount { get; set; }

        [JsonProperty("vnp_OrderInfo")]
        public string OrderInfo { get; set; }

        [JsonProperty("vnp_BankCode")]
        public string BankCode { get; set; }

        [JsonProperty("vnp_PayDate")]
        public string PayDate { get; set; }

        [JsonProperty("vnp_TransactionNo")]
        public string TransactionNo { get; set; }

        [JsonProperty("vnp_TransactionType")]
        public string TransactionType { get; set; }

        [JsonProperty("vnp_TransactionStatus")]
        public string TransactionStatus { get; set; }

        [JsonProperty("vnp_SecureHash")]
        public string SecureHash { get; set; }
    }
}
