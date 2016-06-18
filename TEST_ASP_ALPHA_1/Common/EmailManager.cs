using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace TEST_ASP_ALPHA_1.Common
{
    public class EmailManager
    {
        public void SendActivationEmail(string activationCode, string username, string url)
        {
            var sptEmail = new AESManager().DecryptString(ConfigurationManager.AppSettings.Get("SptEmail"));
            var sptPassword = new AESManager().DecryptString(ConfigurationManager.AppSettings.Get("SptPassword"));
            var email = new AESManager().DecryptString(activationCode);

            using (MailMessage mm = new MailMessage(sptEmail, email))
            {
                mm.Subject = "Account Activation";
                string body = "Hey " + username + "!";
                body += "<br /><br />Please click the following link to activate your account";
                body += "<br /><a href = '" + url + "Activation.aspx?ActivationCode=" + activationCode + "'>Click here to activate your account.</a>";
                body += "<br /><br />Thanks";
                mm.Body = body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(sptEmail, sptPassword);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
            }
        }

        public void SendResetPasswordEmail(string activationCode, string username, string url)
        {
            var sptEmail = new AESManager().DecryptString(ConfigurationManager.AppSettings.Get("SptEmail"));
            var sptPassword = new AESManager().DecryptString(ConfigurationManager.AppSettings.Get("SptPassword"));
            var email = new AESManager().DecryptString(GetCustEmailFromEmailingCode(activationCode));

            using (MailMessage mm = new MailMessage(sptEmail, email))
            {
                mm.Subject = "Reset Password";
                string body = "Hey " + username + "!";
                body += "<br /><br />Please click the following link to reset your password";
                body += "<br /><a href = '" + url + "ResetPassword.aspx?RE=" + activationCode + "'>Reset password.</a>";
                body += "<br /><br />Thanks";
                mm.Body = body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(sptEmail, sptPassword);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
            }
        }

        public string GetEmailingCode(string custEncrEmail)
        {
            var aesMgr = new AESManager();
            var email = aesMgr.DecryptString(custEncrEmail);
            return aesMgr.EncryptToString(email + "|" + DateTime.Now);
        }

        public string GetCustEmailFromEmailingCode(string emailingCode)
        {
            var aesMgr = new AESManager();
            var code = aesMgr.DecryptString(emailingCode);
            return aesMgr.EncryptToString(code.Split('|').First());
        }

        public string GetCustEmailFromEmailingCode(string emailingCode, out DateTime requestedDate)
        {
            var aesMgr = new AESManager();
            var code = aesMgr.DecryptString(emailingCode);
            requestedDate = Convert.ToDateTime(code.Split('|').Last());
            return aesMgr.EncryptToString(code.Split('|').First());
        }
    }
}