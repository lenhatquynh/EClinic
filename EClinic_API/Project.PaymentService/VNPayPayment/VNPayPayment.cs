using Newtonsoft.Json;
using Project.Core.Logger;
using Project.PaymentService.Model;
using Project.PaymentService.MomoPayment.MomoPaymentConfiguration;
using Project.PaymentService.MomoPayment.MomoPaymentModel;
using Project.PaymentService.VNPayPayment.VNPayPaymentConfiguration;
using Project.PaymentService.VNPayPayment.VNPayPaymentModel;

namespace Project.PaymentService.VNPayPayment
{
    public class VNPayPayment : IVNPayPayment
    {
        private readonly VNPayInformation VNPayInformation;
        private readonly ILogger<VNPayPayment> logger;
        public VNPayPayment(IConfiguration configuration, ILogger<VNPayPayment> logger)
        {
            VNPayInformation = configuration.GetSection("VNPayConnectionInformation").Get<VNPayInformation>();
            this.logger = logger;
        }

        public PaymentResult PaymentConfirm(IQueryCollection query)
        {

            try
            {
                string hashSecret = VNPayInformation.VNPaySecretKey;
                var vnpayData = query;
                VnPayLibrary pay = new VnPayLibrary();
                foreach (var item in query)
                {
                    if (!string.IsNullOrEmpty(item.Key) && item.Key.StartsWith("vnp_"))
                    {
                        pay.AddResponseData(item.Key, item.Value[0]);
                    }
                }
                string orderId = Convert.ToString(pay.GetResponseData("vnp_TxnRef"));
                string vnpayTranId = Convert.ToString(pay.GetResponseData("vnp_TransactionNo"));
                string vnp_ResponseCode = pay.GetResponseData("vnp_ResponseCode");
                string vnp_OrderInfo = pay.GetResponseData("vnp_OrderInfo");
                string vnp_SecureHash = query["vnp_SecureHash"];
                double amount = double.Parse(query["vnp_Amount"]) / 100;
                string payDate = query["vnp_PayDate"];
                string dateFormat = "yyyyMMddHHmmss";

                DateTime dateTime;
                DateTime.TryParseExact(payDate, dateFormat, null, System.Globalization.DateTimeStyles.None, out dateTime);
                string[] parts = vnp_OrderInfo.Split('|');
                string message = "";
                string bookingId = "";
                string userId = "";
                if (parts.Length >= 3)
                {
                    message = parts[0];
                    bookingId = parts[1];
                    userId = parts[2];
                }
                bool checkSignature = pay.ValidateSignature(vnp_SecureHash, hashSecret); //check chữ ký đúng hay không?
                PaymentResult result = new PaymentResult();

                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00")
                    {
                        result.IsSuccess = true;
                        result.TransactionID = vnpayTranId.ToString();
                        result.OrderID = orderId.ToString();
                        result.UserID = Guid.Parse(userId);
                        result.BookingID = Guid.Parse(bookingId);
                        result.Amount = amount;
                        result.PaymentTime = dateTime;
                        result.PaymentType = message;
                        return result;

                    }
                }
                result.IsSuccess = false;
                result.TransactionID = vnpayTranId.ToString();
                result.OrderID = orderId.ToString();
                result.UserID = Guid.Parse(userId);
                result.BookingID = Guid.Parse(bookingId);
                result.Amount = amount;
                result.PaymentTime = dateTime;
                result.PaymentType = message;
                return result;
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                PaymentResult result = new PaymentResult();
                result.IsSuccess = false;
                return result;
            }
        }
        public async Task<RefundResult> PaymentRefund(VNPayRefundModel RefundModel, string ipAddress)
        {
            try
            {
                var vnp_Api = VNPayInformation.VNPayAPI;
                var vnp_HashSecret = VNPayInformation.VNPaySecretKey;
                var vnp_TmnCode = VNPayInformation.VNPayTerminalID;

                var vnp_RequestId = DateTime.Now.Ticks.ToString();
                var vnp_Version = VnPayLibrary.VERSION;
                var vnp_Command = "refund";
                var vnp_TransactionType = "02";
                var vnp_Amount = RefundModel.Amount.ToString() + "00";
                var vnp_TxnRef = RefundModel.OrderID;
                var vnp_OrderInfo = "Hoan tien giao dich:" + RefundModel.Message;
                var vnp_TransactionNo = RefundModel.TransactionID;
                var vnp_TransactionDate = RefundModel.TransactionDate.ToString("yyyyMMddHHmmss");
                var vnp_CreateDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                var vnp_CreateBy = RefundModel.UserID.ToString();
                var vnp_IpAddr = ipAddress;
                var signData = vnp_RequestId + "|" + vnp_Version + "|" + vnp_Command + "|" + vnp_TmnCode + "|" + vnp_TransactionType + "|" + vnp_TxnRef + "|" + vnp_Amount + "|" + vnp_TransactionNo + "|" + vnp_TransactionDate + "|" + vnp_CreateBy + "|" + vnp_CreateDate + "|" + vnp_IpAddr + "|" + vnp_OrderInfo;
                var vnp_SecureHash = Utils.HmacSHA512(vnp_HashSecret, signData);

                var rfData = new
                {
                    vnp_RequestId = vnp_RequestId,
                    vnp_Version = vnp_Version,
                    vnp_Command = vnp_Command,
                    vnp_TmnCode = vnp_TmnCode,
                    vnp_TransactionType = vnp_TransactionType,
                    vnp_TxnRef = vnp_TxnRef,
                    vnp_Amount = vnp_Amount,
                    vnp_OrderInfo = vnp_OrderInfo,
                    vnp_TransactionNo = vnp_TransactionNo,
                    vnp_TransactionDate = vnp_TransactionDate,
                    vnp_CreateBy = vnp_CreateBy,
                    vnp_CreateDate = vnp_CreateDate,
                    vnp_IpAddr = ipAddress,
                    vnp_SecureHash = vnp_SecureHash

                };
                var httpResponse = await VNPayPaymentMethod.SendPaymentRequest(vnp_Api, rfData);
                var data = JsonConvert.DeserializeObject<VNPayRefundResponse>(httpResponse);
                RefundResult result = new RefundResult();
                if (data.ResponseCode == "00")
                {
                    result.IsSuccess = true;
                }
                else
                {
                    result.IsSuccess = false;
                }

                return result;
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return null;
            }
        }

        public string PaymentRequest(PaymentModel PaymentModel, string ipAddress)
        {
            try
            {
                string url = VNPayInformation.VNPayURL;
                string returnUrl = VNPayInformation.ReturnUrl;
                string tmnCode = VNPayInformation.VNPayTerminalID;
                string hashSecret = VNPayInformation.VNPaySecretKey;
                string orderID = DateTime.Now.Ticks.ToString();
                string orderInfo = PaymentModel.Message + "|" + PaymentModel.BookingID + "|" + PaymentModel.UserID;
                string Amount = PaymentModel.Amount.ToString() + "00";
                VnPayLibrary pay = new VnPayLibrary();
                pay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
                pay.AddRequestData("vnp_Command", "pay");
                pay.AddRequestData("vnp_TmnCode", tmnCode);
                pay.AddRequestData("vnp_Amount", Amount);
                pay.AddRequestData("vnp_BankCode", "VNBANK");
                pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
                pay.AddRequestData("vnp_CurrCode", "VND");
                pay.AddRequestData("vnp_IpAddr", ipAddress);
                pay.AddRequestData("vnp_Locale", "vn");
                pay.AddRequestData("vnp_OrderInfo", orderInfo);
                pay.AddRequestData("vnp_OrderType", "other");
                pay.AddRequestData("vnp_ReturnUrl", returnUrl);
                pay.AddRequestData("vnp_TxnRef", orderID);
                string paymentUrl = pay.CreateRequestUrl(url, hashSecret);
                return paymentUrl;
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return null;
            }
        }
    }
}
