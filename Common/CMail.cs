using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Configuration;
using System.Web;
using System.Net;
using System.IO;

namespace Common
{
    public class CMail
    {

        public static string SendMail(string to, string subject, string body)
        {
            // Get the unique identifier for this asynchronous operation.
            // String token = (string) e.UserState;

            SmtpClient client = new SmtpClient();
            client.Port = Convert.ToInt32(ConfigurationManager.AppSettings["mailport"]);
            client.Host = ConfigurationManager.AppSettings["mailhost"].ToString();
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["mailuser"].ToString(), ConfigurationManager.AppSettings["mailpass"].ToString());

            MailMessage mm = new MailMessage(ConfigurationManager.AppSettings["mailuser"].ToString(), to, subject, body);
            mm.IsBodyHtml = true;
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            try
            {
                //client.Send(mm);
                System.Threading.Tasks.Task.Factory.StartNew(() => client.SendAsync(mm, null));
            }
            catch (Exception ex)
            {
                return (ex.ToString() + "Something went wrong");
            }

            return ("Email has been sent");

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="CC"></param>
        /// <param name="fileUploader"></param>
        /// <param name="newFileUpload"></param>
        /// <returns></returns>
        public static string sendEmailWithAttachment(string to, string subject, string body, string CC, HttpPostedFileBase fileUploader, MemoryStream newFileUpload)
        {
            try
            {
                //get email id from config file
                using (MailMessage mail = new MailMessage())
                {
                    mail.Subject = subject;
                    mail.Body = body;

                    //add the From email
                    mail.From = new MailAddress(ConfigurationManager.AppSettings["mailuser"].ToString());


                    if (to != null)
                    {
                        if (to.EndsWith("; "))
                        {
                            to = to.Substring(0, to.Length - 2);
                        }
                        if (to.EndsWith(";"))
                        {
                            to = to.Substring(0, to.Length - 1);
                        }

                        to = to.Trim().Replace(" ", "");

                        string[] emails = to.ToString().Split(';');

                        foreach (string email in emails)
                        {
                            mail.To.Add(email);
                        }
                    }

                    // Check if there are any attachments
                    if (fileUploader != null)
                    {
                        string fileName = Path.GetFileName(fileUploader.FileName);
                        mail.Attachments.Add(new Attachment(newFileUpload, fileName));
                    }

                    //Add CC emails
                    if (!string.IsNullOrEmpty(CC))
                    {
                        if (CC.EndsWith("; "))
                        {
                            CC = CC.Substring(0, CC.Length - 2);
                        }
                        if (CC.EndsWith(";"))
                        {
                            CC = CC.Substring(0, CC.Length - 1);
                        }

                        CC = CC.Trim().Replace(" ", "");
                        string[] emails = CC.ToString().Split(';');
                        foreach (string email in emails)
                        {
                            mail.CC.Add(email);
                        }
                    }
                    mail.IsBodyHtml = true;
                    mail.BodyEncoding = UTF8Encoding.UTF8;
                    mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = ConfigurationManager.AppSettings["mailhost"].ToString();
                    smtp.EnableSsl = true;
                    NetworkCredential networkCredential = new NetworkCredential(ConfigurationManager.AppSettings["mailuser"].ToString(), ConfigurationManager.AppSettings["mailpass"].ToString());
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = networkCredential;
                    smtp.Port = 587;
                    smtp.Send(mail);
                }
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return (ex.ToString() + "Something went wrong");
            }

            return ("success");
        }

        public static string sendErrorMail(string body, string FullName)
        {
            try
            {
                //get email id from config file
                using (MailMessage mail = new MailMessage(ConfigurationManager.AppSettings["mailuser"].ToString(), "e4testuser@gmail.com"))
                {
                    mail.Subject = "Error Contact Form - " + FullName;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    mail.BodyEncoding = UTF8Encoding.UTF8;
                    mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = ConfigurationManager.AppSettings["mailhost"].ToString();
                    smtp.EnableSsl = true;
                    NetworkCredential networkCredential = new NetworkCredential(ConfigurationManager.AppSettings["mailuser"].ToString(), ConfigurationManager.AppSettings["mailpass"].ToString());
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = networkCredential;
                    smtp.Port = 587;
                    smtp.Send(mail);
                }
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return (ex.ToString() + "Something went wrong");
            }

            return ("success");
        }

    }
}
