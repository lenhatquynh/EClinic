using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Project.Core.Logger;
using Project.PaymentService.Model;
using Project.PaymentService.MomoPayment.MomoPaymentConfiguration;
using Project.PaymentService.MomoPayment.MomoPaymentModel;

namespace Project.PaymentService.MomoPayment
{
    public class MomoPayment : IMomoPayment
    {
        private readonly MomoInformation MomoInformation;
        private readonly ILogger<MomoPayment> logger;
        public MomoPayment(IConfiguration configuration, ILogger<MomoPayment> logger)
        {
            MomoInformation = configuration.GetSection("MomoConnectionInformation").Get<MomoInformation>();
            this.logger = logger;
        }

        public PaymentResult PaymentConfirm(IQueryCollection query)
        {
            try
            {
                var momoResponse = new MomoResponseModel
                {
                    PartnerCode = query["partnerCode"],
                    AccessKey = query["accessKey"],
                    RequestId = query["requestId"],
                    Amount = int.Parse(query["amount"]),
                    OrderId = query["orderId"],
                    OrderInfo = query["orderInfo"],
                    OrderType = query["orderType"],
                    TransId = long.Parse(query["transId"]),
                    Message = query["message"],
                    LocalMessage = query["localMessage"],
                    ResponseTime = DateTime.Parse(query["responseTime"]),
                    ErrorCode = int.Parse(query["errorCode"]),
                    PayType = query["payType"],
                    ExtraData = query["extraData"],
                    Signature = query["signature"]
                };
                PaymentResult result = new PaymentResult();
                if (momoResponse.ErrorCode != 0)
                {
                    result.IsSuccess = false;
                    result.TransactionID = momoResponse.TransId.ToString();
                    result.OrderID = momoResponse.OrderId;
                    result.PaymentType = momoResponse.ExtraData;
                    result.BookingID = Guid.Parse(momoResponse.RequestId);
                    result.Amount = momoResponse.Amount;
                    result.PaymentTime = momoResponse.ResponseTime;
                    result.UserID = Guid.Parse(momoResponse.OrderInfo);
                    return result;
                }
                result.IsSuccess = true;
                result.TransactionID = momoResponse.TransId.ToString();
                result.OrderID = momoResponse.OrderId;
                result.PaymentType = momoResponse.ExtraData;
                result.BookingID = Guid.Parse(momoResponse.RequestId);
                result.Amount = momoResponse.Amount;
                result.PaymentTime = momoResponse.ResponseTime;
                result.UserID = Guid.Parse(momoResponse.OrderInfo);
                return result;
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                PaymentResult result = new PaymentResult();
                result.IsSuccess = true;
                return result;
            }
        }

        public async Task<RefundResult> PaymentRefund(MomoRefundModel RefundModel)
        {
            try
            {
                string router = "pay/refund";
                var OrderID = RefundModel.OrderID;
                var TransactionID = RefundModel.TransactionID;
                string version = "2.0";
                var Amount = RefundModel.Amount.ToString();
                var ExtraData = RefundModel.UserID.ToString();
                string orderid = DateTime.Now.Ticks.ToString();
                string requestId = RefundModel.BookingID.ToString();
                string transId = RefundModel.TransactionID;
                var rawHash = new
                {
                    partnerCode = MomoInformation.MomoPartnerCode,
                    partnerRefId = OrderID,
                    momoTransId = TransactionID,
                    amount = Amount,
                    description = RefundModel.Message
                };
                var json = JsonConvert.SerializeObject(rawHash);

                MoMoSecurity crypto = new MoMoSecurity();
                string hash = crypto.RSAHash(json, MomoInformation.MomoPublicKey);
                var message = new
                {
                    partnerCode = MomoInformation.MomoPartnerCode,
                    requestId = requestId,
                    hash = hash,
                    version = version
                };
                var responseFromMomo = await MomoPaymentMethod.SendPaymentRequest(MomoInformation.Endpoint, router, message);
                JObject jmessage = JObject.Parse(responseFromMomo);
                var status = Int32.Parse(jmessage.GetValue("status").ToString());
                RefundResult result = new RefundResult();
                if (status != 0)
                {
                    result.IsSuccess = false;
                    return result;
                }
                result.IsSuccess = true;
                return result;
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return null;
            }
        }

        public async Task<string> PaymentRequest(PaymentModel PaymentModel)
        {
            try
            {
                string router = "gw_payment/transactionProcessor";
                var OrderInfo = PaymentModel.UserID.ToString();
                var Amount = PaymentModel.Amount.ToString();
                var ExtraData = PaymentModel.Message;
                string orderid = DateTime.Now.Ticks.ToString();
                string requestId = PaymentModel.BookingID.ToString();
                string rawHash = "partnerCode=" +
                    MomoInformation.MomoPartnerCode + "&accessKey=" +
                    MomoInformation.MomoAccessKey + "&requestId=" +
                    requestId + "&amount=" +
                    Amount + "&orderId=" +
                    orderid + "&orderInfo=" +
                    OrderInfo + "&returnUrl=" +
                    MomoInformation.ReturnUrl + "&notifyUrl=" +
                    MomoInformation.NotifyUrl + "&extraData=" +
                    ExtraData;
                MoMoSecurity crypto = new MoMoSecurity();
                string signature = crypto.signSHA256(rawHash, MomoInformation.MomoSerectKey);
                var message = new
                {
                    accessKey = MomoInformation.MomoAccessKey,
                    partnerCode = MomoInformation.MomoPartnerCode,
                    requestType = "captureMoMoWallet",
                    notifyUrl = MomoInformation.NotifyUrl,
                    returnUrl = MomoInformation.ReturnUrl,
                    orderId = orderid,
                    amount = Amount,
                    orderInfo = OrderInfo,
                    requestId = requestId,
                    extraData = ExtraData,
                    signature = signature
                };
                var responseFromMomo = await MomoPaymentMethod.SendPaymentRequest(MomoInformation.Endpoint, router, message);
                JObject jmessage = JObject.Parse(responseFromMomo);
                if (jmessage.GetValue("payUrl") != null)
                {
                    return jmessage.GetValue("payUrl").ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return null;
            }
        }


    }
}
