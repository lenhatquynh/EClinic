using Project.Core.Logger;
using Project.NotificationService.Dtos;
using System.Net;
using System.Net.Mail;

namespace Project.NotificationService.Service
{
    public class MailService : IMailService
    {
        private readonly ILogger<MailService> logger;
        private readonly IConfiguration configuration;
        private readonly MailInformation mailInformation = new MailInformation();
        public MailService(ILogger<MailService> logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
            configuration.GetSection("MailConnectionInformation").Bind(mailInformation);
        }

        public async Task<bool> ConfirmEmail(string email, string code)
        {
            try
            {
                var mailName = email.Substring(0, email.IndexOf("@"));
                var mailModel = new MailModel
                {
                    EmailTo = email,
                    Subject = "Confirm your email address",
                    Body = $"Welcome {mailName.ToLower()}!" +
                    $"<br/><br/>" +
                    $"Thanks for signing up with {mailInformation.MailTile}!" +
                    $"<br/><b>{code}</b> is your {mailInformation.MailTile} verification." +
                    $" <br/>" +
                    $"Thank you," +
                    $" <br/>" +
                    $"{mailInformation.MailTile} account group"
                };
                return await SendMail(mailModel);
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return false;
            }
        }
        public async Task<bool> VerifyEmail(string email, string code)
        {
            try
            {
                var mailName = email.Substring(0, email.IndexOf("@"));
                var mailModel = new MailModel
                {
                    EmailTo = email,
                    Subject = $"Reset {mailInformation.MailTile} account password",
                    Body = $"Hello {mailName.ToLower()}!" +
                    $"<br/><br/>" +
                    $"Please use this code to reset the password for your {mailInformation.MailTile} account {email}" +
                    $"<br/>Here is your code: <b>{code}</b>." +
                    $" <br/>" +
                    $"Thank you," +
                    $" <br/>" +
                    $"{mailInformation.MailTile} account group"

                };
                return await SendMail(mailModel);
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return false;
            }
        }

        private async Task<bool> SendMail(MailModel mailModel)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(mailInformation.Mail, mailInformation.MailTile);
                mailMessage.To.Add(new MailAddress(mailModel.EmailTo));
                mailMessage.Subject = mailModel.Subject;
                mailMessage.Body = mailModel.Body;

                if (mailModel.CC != null)
                {
                    foreach (var cc in mailModel.CC)
                    {
                        mailMessage.CC.Add(cc);
                    }
                }
                if (mailModel.Attachments != null)
                {
                    foreach (var attachment in mailModel.Attachments)
                    {
                        string fileName = Path.GetFileName(attachment.FileName);
                        mailMessage.Attachments.Add(new Attachment(attachment.OpenReadStream(), fileName));
                    }
                }
                mailMessage.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = mailInformation.Host;
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(mailInformation.Mail, mailInformation.MailAppPassword);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Port = Int32.Parse(mailInformation.Port);
                await smtp.SendMailAsync(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> SendBill(string email, PaymentModelData paymentModel)
        {
            try
            {
                var name = paymentModel.PaymentService;
                var mailName = email.Substring(0, email.IndexOf("@"));
                var mailModel = new MailModel
                {
                    EmailTo = email,
                    Subject = $"Your invoice from {mailInformation.MailTile}",
                    Body = @"<!DOCTYPE html>
                                <html lang=""en"">
                                <head>
                                    <meta charset=""UTF-8"">
                                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                    <title>Document</title>
                                </head>
                                <body>
                                    <div class=""""><div class=""aHl""></div><div id="":n6"" tabindex=""-1""></div><div id="":uf"" class=""ii gt"" jslog=""20277; u014N:xr6bB; 1:WyIjdGhyZWFkLWY6MTc3MTU2OTA4NTAwMjY5Nzc4MCIsbnVsbCxudWxsLG51bGwsbnVsbCxudWxsLG51bGwsbnVsbCxudWxsLG51bGwsbnVsbCxudWxsLG51bGwsW11d; 4:WyIjbXNnLWY6MTc3MTU2OTA4NTAwMjY5Nzc4MCIsbnVsbCxbXV0.""><div id="":n5"" class=""a3s aiL msg5038991568354012347""><u></u>
                                        <div style=""padding:0;margin:0;font-family:tahoma;font-size:14px;display:block;background:#ffffff"" bgcolor=""#ffffff"">
                                            <table align=""center"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
                                                <tbody><tr>
                                                    <td align=""center"" valign=""top"" bgcolor=""#ffffff"" width=""100%"">
                                                        <table cellspacing=""0"" cellpadding=""0"" width=""100%"">
                                                            <tbody><tr>
                                                                <td>
                                                                    <center style=""margin:20px 0px"">
                                                                        <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""max-width:640px"">
                                                                            <tbody style=""display:block;margin:20px 32px!important"">
                                                                                <tr id=""m_5038991568354012347"">
                                                                                    <td height=""40"" style=""background:#f8f8f8!important"">
                                                                                        <div align=""center"" style=""font-family:tahoma;font-size:12px;color:#384860;background:#f8f8f8!important"">
                                                                                            <b style=""font-weight:bold"">Note</b>: This is an automatic email from the system, please do not reply to this email
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id=""m_5038991568354012347trNameCustomer"">
                                                                                    <td height=""44"" style=""vertical-align:middle"">
                                                                                         <span style=""""text-align:left;font-family:tahoma;font-size:14px;color:#384860;line-height:24px"""" id=""""m_5038991568354012347titleCusTaxCode"""">Dear:</span> <b style=""""color:#384860"""">" + paymentModel.FullName + @"</b>
                                                                                        <span style=""text-align:left;font-family:tahoma;font-size:14px;color:#384860;line-height:24px"" id=""m_5038991568354012347titleCusTaxCode"">ID:</span> <b style=""color:#384860"">" + paymentModel.TransactionID + @"</b>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id=""m_5038991568354012347trCusCode"">
                                                                                    <td height=""50"" style=""vertical-align:middle;border-top:1px dashed #d3d3d3;padding-top:24px"">
                                                                                        <b style=""text-align:left;font-family:tahoma;font-size:14px;color:#384860;line-height:24px;padding-bottom:12px;text-transform:uppercase"">EClinic</b><br>
                                                                                        <span style=""text-align:left;font-family:tahoma;font-size:14px;color:#384860;line-height:24px;margin-bottom:8px"">Just issued an e-invoice to you with the following content::</span>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td height=""276"" style=""vertical-align:middle;background:#f1f5f9!important;border-radius:10px"">
                                                                                        <center style=""margin:0 16px"">
                                                                                            <table cellspacing=""0"" cellpadding=""0"" width=""100%"" style=""max-width:544px"">
                                                                                                <tbody>
                                                                                                    <tr>
                                                                                                        <td colspan=""2"" height=""8"" style=""vertical-align:middle;line-height:8px"">
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr id=""m_5038991568354012347trPattern"">
                                                                                                        <td height=""36"" style=""vertical-align:middle;border-top-left-radius:4px;border-bottom-left-radius:4px;background:#fff;padding-left:12px"">
                                                                                                            <b id=""m_5038991568354012347titlePattern"" style=""text-align:left;font-family:tahoma;font-size:14px"">Payment ID:</b>
                                                                                                        </td>
                                                                                                        <td height=""36"" style=""vertical-align:middle;border-top-right-radius:4px;border-bottom-right-radius:4px;background:#fff;text-align:right;padding-right:16px"">
                                                                                                            <span style=""font-family:tahoma;font-size:14px"">" + paymentModel.PaymentID + @"</span>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan=""2"" height=""16"" style=""vertical-align:middle;line-height:16px"">
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr id=""m_5038991568354012347trPattern"">
                                                                                                        <td height=""36"" style=""vertical-align:middle;border-top-left-radius:4px;border-bottom-left-radius:4px;background:#fff;padding-left:12px"">
                                                                                                            <b id=""m_5038991568354012347titlePattern"" style=""text-align:left;font-family:tahoma;font-size:14px"">Booking Type:</b>
                                                                                                        </td>
                                                                                                        <td height=""36"" style=""vertical-align:middle;border-top-right-radius:4px;border-bottom-right-radius:4px;background:#fff;text-align:right;padding-right:16px"">
                                                                                                            <span style=""font-family:tahoma;font-size:14px"">" + paymentModel.BookingType + @"</span>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan=""2"" height=""16"" style=""vertical-align:middle;line-height:16px"">
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr id=""m_5038991568354012347trInvNumber"">
                                                                                                        <td height=""36"" style=""vertical-align:middle;border-top-left-radius:4px;border-bottom-left-radius:4px;background:#fff;padding-left:12px"">
                                                                                                            <b id=""m_5038991568354012347titlePattern"" style=""text-align:left;font-family:tahoma;font-size:14px"">Payment By:</b>
                                                                                                        </td>
                                                                                                        <td height=""36"" style=""vertical-align:middle;border-top-right-radius:4px;border-bottom-right-radius:4px;background:#fff;text-align:right;padding-right:16px"">
                                                                                                            <span style=""font-family:tahoma;font-size:14px"">" + name + @"</span>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan=""2"" height=""16"" style=""vertical-align:middle;line-height:16px"">
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr id=""m_5038991568354012347trArisingDate"">
                                                                                                        <td height=""36"" style=""vertical-align:middle;border-top-left-radius:4px;border-bottom-left-radius:4px;background:#fff;padding-left:12px"">
                                                                                                            <b id=""m_5038991568354012347titlePattern"" style=""text-align:left;font-family:tahoma;font-size:14px"">Invoice date:</b>
                                                                                                        </td>
                                                                                                        <td height=""36"" style=""vertical-align:middle;border-top-right-radius:4px;border-bottom-right-radius:4px;background:#fff;text-align:right;padding-right:16px"">
                                                                                                            <span style=""font-family:tahoma;font-size:14px"">" + paymentModel.PaymentTime.ToString("yyyy/MM/dd HH:mm:ss") + @"</span>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan=""2"" height=""16"" style=""vertical-align:middle;line-height:16px"">
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr id=""m_5038991568354012347trMCQT"">
                                                                                                        <td height=""36"" style=""vertical-align:middle;border-top-left-radius:4px;border-bottom-left-radius:4px;background:#fff;padding-left:12px"">
                                                                                                            <b id=""m_5038991568354012347titlePattern"" style=""text-align:left;font-family:tahoma;font-size:14px"">Amount:</b>
                                                                                                        </td>
                                                                                                        <td height=""36"" style=""vertical-align:middle;border-top-right-radius:4px;border-bottom-right-radius:4px;background:#fff;text-align:right;padding-right:16px"">
                                                                                                            <span style=""font-family:tahoma;font-size:14px"">" + paymentModel.PaymentAmount + @"</span>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan=""2"" height=""8"" style=""vertical-align:middle;line-height:8px"">
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </tbody>
        
                                                                                            </table>
                                                                                        </center>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td height=""74"" style=""vertical-align:left;background:#f8f8f8!important"">
                                                                                        <div align=""left"" style=""font-family:tahoma;font-size:14px;color:#384860;margin:12px 32px!important;line-height:24px"">
                                                                                            Best regards<br>
                                                                                            <b style=""color:#384860;text-transform:uppercase"">Five Man Dev</b>
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>

                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </center>
                                                                </td>
                                                            </tr>
                                                        </tbody></table>
                                                    </td>
                                                </tr>
                                            </tbody></table><div class=""yj6qo""></div><div class=""adL"">
                                        </div></div><div class=""adL"">
                                        </div></div></div><div id="":mv"" class=""ii gt"" style=""display:none""><div id="":mw"" class=""a3s aiL ""></div></div><div class=""hi""></div></div>
                                </body>
                                </html>"
                };
                return await SendMail(mailModel);
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return false;
            }
        }
        public async Task<bool> SendAccount(string email, AccountDtos account)
        {
            try
            {
                var mailModel = new MailModel
                {
                    EmailTo = email,
                    Subject = $"Your account in {mailInformation.MailTile} ",
                    Body = $"<tr bgcolor=\"#efefef\">\r\n    <td style=\"color:#282828\">\r\n        <center>\r\n            <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width:600px;font-size:14px\">\r\n                <tbody>\r\n                    <tr>\r\n                        <td align=\"left\" style=\"padding:20px 10px;padding-bottom:0\">\r\n                            <div>\r\n                                <b>Welcome</b>\r\n                            </div>\r\n                        </td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td align=\"left\" style=\"padding:10px;color:#000\">\r\n                            <table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">\r\n                                <tbody>\r\n                                    <tr>\r\n                                        <td>\r\n                                            Account information:<br><br>&nbsp; &nbsp; &nbsp; &nbsp; - UserName:\r\n                                            {account.UserName}<br>\r\n                                            &nbsp; &nbsp; &nbsp; &nbsp; - Password: {account.Password}<br>&nbsp; &nbsp; &nbsp;\r\n                                            &nbsp;<br><br>\r\n                                        </td>\r\n                                    </tr>\r\n                                    <tr>\r\n                                        <td>\r\n                                            <p\r\n                                                style=\"font-family:tahoma;font-size:12px;color:#363636;line-height:20px;margin:5px 0;padding-bottom:10px\">\r\n                                                <b>Note:</b>\r\n                                                After receiving the email, please change your password. Do not give your\r\n                                                account and password to 3rd parties\r\n                                            </p>\r\n                                        </td>\r\n                                    </tr>\r\n                                    <tr>\r\n                                        <td style=\"font-family:tahoma;font-size:12px;color:#363636;line-height:20px\">\r\n                                            Five Man Dev\r\n                                        </td>\r\n                                    </tr>\r\n                                </tbody>\r\n                            </table>\r\n                        </td>\r\n                    </tr>\r\n                </tbody>\r\n            </table>\r\n        </center>\r\n    </td>\r\n</tr>\r\n</table>"

                };
                return await SendMail(mailModel);
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return false;
            }
        }
    }
}
