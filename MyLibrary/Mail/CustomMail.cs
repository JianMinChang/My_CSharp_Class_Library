using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace MyLibrary.Mail
{
    /// <summary>
    /// mail server 列舉
    /// </summary>
    public enum CustomSmtpList
    {
        None = 0, Outlook = 1, Gmail = 2, Yahoo = 3, Hotmail = 4
    }

    /// <summary>
    /// 自建發Email
    /// </summary>
    public class CustomMail
    {
        public bool IsSuccess { private set; get; }

        public string ErrorMessage { private set; get; }


        private SmtpClient OringeSendEmail;
        private MailMessage msg;

        private string initEmailAccount { set; get; }

        public CustomMail(CustomSmtpList smtp, string EmailAccont, string Password)
        {
            OringeSendEmail = GetSmtpClient(smtp);
            OringeSendEmail.EnableSsl = true;
            OringeSendEmail.Credentials = new System.Net.NetworkCredential(EmailAccont, Password);
            this.initEmailAccount = EmailAccont;
        }

        /// <summary>
        /// 取得SmtpClient
        /// </summary>
        /// <param name="smtp"></param>
        /// <returns></returns>
        private SmtpClient GetSmtpClient(CustomSmtpList smtp)
        {
            SmtpClient tmp;
            switch (smtp)
            {
                case CustomSmtpList.None:
                    tmp = null;
                    break;
                case CustomSmtpList.Outlook:
                    tmp = new SmtpClient();
                    break;
                case CustomSmtpList.Gmail:
                    tmp = new SmtpClient("smtp.gmail.com", 587);
                    break;
                case CustomSmtpList.Yahoo:
                    tmp = new SmtpClient("smtp.mail.yahoo.com", 587);
                    break;
                case CustomSmtpList.Hotmail:
                    tmp = new SmtpClient("smtp.live.com", 587);
                    break;
                default:
                    tmp = null;
                    break;
            }
            return tmp;
        }

        /// <summary>
        /// 初始化預設值
        /// </summary>
        private void Reset()
        {
            this.IsSuccess = false;
            this.ErrorMessage = string.Empty;
        }

        public void SendEMail(IList<string> MailList, string Subject, string Body)
        {
            Reset();
            if (this.OringeSendEmail != null)
            {
                this.msg = new MailMessage();

                //收件者，以逗號分隔不同收件者 ex "test@gmail.com,test2@gmail.com"  
                msg.To.Add(string.Join(",", MailList.ToArray()));
                msg.From = new MailAddress(this.initEmailAccount, "測試郵件", System.Text.Encoding.UTF8);
                msg.Subject = Subject;  //郵件標題   
                msg.SubjectEncoding = System.Text.Encoding.UTF8;  //郵件標題編碼 

                msg.Body = Body;  //郵件內容  
                msg.IsBodyHtml = true;
                msg.BodyEncoding = System.Text.Encoding.UTF8;//郵件內容編碼   
                msg.Priority = MailPriority.Normal;//郵件優先級   

                try
                {
                    this.OringeSendEmail.Send(msg);
                    this.IsSuccess = true;
                }
                catch (Exception err)
                {
                    this.ErrorMessage = err.Message;
                    this.IsSuccess = false;
                }
            }
            else
            {
                this.IsSuccess = false;
                this.ErrorMessage = "初始化SmtpClient發生錯誤，請檢查帳號密碼或Mail Server是否正確";
            }
        }

    }
}
