namespace Project.PaymentService.MomoPayment.MomoPaymentModel
{
    public class MomoInformation
    {
        public string MomoPartnerCode { get; set; }
        public string MomoAccessKey { get; set; }
        public string MomoSerectKey { get; set; }
        public string MomoPublicKey { get; set; }
        public string Endpoint { get; set; }
        public string ReturnUrl { get; set; }
        public string NotifyUrl { get; set; }
    }
}
