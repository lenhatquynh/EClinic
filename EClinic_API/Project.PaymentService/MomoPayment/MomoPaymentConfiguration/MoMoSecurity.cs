using System.Security.Cryptography;
using System.Text;

namespace Project.PaymentService.MomoPayment.MomoPaymentConfiguration
{
    public class MoMoSecurity
    {
       public string RSAHash(string json, string publicKey)
        {
            byte[] data = Encoding.UTF8.GetBytes(json);
            string result = null;
            using (var rsa = new RSACryptoServiceProvider(4096))
            {
                try
                {
                    rsa.FromXmlString("<RSAKeyValue><Modulus>" + publicKey+ "</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>");
                    var encryptedData = rsa.Encrypt(data, false);
                    var base64Encrypted = Convert.ToBase64String(encryptedData);
                    result = base64Encrypted;
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }

            }
            return result;

        }
        public string signSHA256(string message, string key)
        {
            byte[] keyByte = Encoding.UTF8.GetBytes(key);
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                string hex = BitConverter.ToString(hashmessage);
                hex = hex.Replace("-", "").ToLower();
                return hex;
            }
        }
    }
}
