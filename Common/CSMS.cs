using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace Common
{
    public class CSMS : System.Web.UI.Page
    {
        #region " Private Fields "

        private string _Numbers;
        private string _Message;
        private string _Username;
        private string _Password;
        private string _Result;
        private string _ErrorCode;
        private bool _Error;

        #endregion

        #region " Public Properties "

        public string Numbers
        {
            get { return _Numbers; }
            set { _Numbers = value; }
        }

        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        public string Username
        {
            set { _Username = value; }
        }

        public string Password
        {
            set { _Password = value; }
        }

        public string Credits
        {
            get { return GetCredits(); }
        }

        public string Result
        {
            get { return _Result; }
        }

        public bool Errors
        {
            get { return _Error; }
        }

        public string ErrorCode
        {
            get { return _ErrorCode; }
        }

        #endregion

        #region " Public Methods "

        public void SMS()
        {
            PopulateDefault();
        }

        public void Send()
        {
            //Check if number string is correct
            //If _Numbers.Length > 0 And _Message.Length > 0 And _Username.Length > 0 And _Password.Length > 0 Then
            string sAddress = "http://bulksms.2way.co.za:5567/eapi/submission/send_sms/2/2.0";
            string sData = "username=" + HttpUtility.UrlEncode("ForexPeople") + "&password=" + HttpUtility.UrlEncode("d3b0n41rs") + "&message=" + HttpUtility.UrlEncode(_Message) + "&msisdn=" + HttpUtility.UrlEncode(_Numbers);
            System.Net.WebClient wUpload = new System.Net.WebClient();
            byte[] bPostArray;
            byte[] bResponse;
            string sWebPage;
            string[] fbArr;

            wUpload.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            bPostArray = Encoding.ASCII.GetBytes(sData);

            try
            {
                bResponse = wUpload.UploadData(sAddress, "POST", bPostArray);
                sWebPage = new string(Encoding.ASCII.GetChars(bResponse));

                // Output the Response Body 
                fbArr = sWebPage.Split('|');
                if (fbArr[0] == "0")
                {
                    _Result = "Message sent successfully.";
                    _Error = false;
                }
                else
                {
                    _Result = "An error occurred. (Error code: " + fbArr[0] + ")";
                    _Error = true;
                    _ErrorCode = fbArr[0];
                }
            }
            catch (Exception ex)
            {
                _Result = "An error occurred. (Error message: " + ex.Message + ")";
                _Error = true;
            }
            //Else
            // _Result = "Message, Number, Username or Password not supplied."
            // _Error = True
            //End If
        }

        public bool Send(string cellNumbers, string smsMessage)
        {
            //Check if number string is correct
            //If cellNumbers.Length > 0 And smsMessage.Length > 0 And smsUsername.Length > 0 And smsPassword.Length > 0 Then
            string sAddress = "http://bulksms.2way.co.za:5567/eapi/submission/send_sms/2/2.0";
            string sData = "username=" + HttpUtility.UrlEncode("ForexPeople") + "&password=" + HttpUtility.UrlEncode("d3b0n41rs") + "&message=" + HttpUtility.UrlEncode(smsMessage) + "&msisdn=" + HttpUtility.UrlEncode(cellNumbers);
            System.Net.WebClient wUpload = new System.Net.WebClient();
            byte[] bPostArray;
            byte[] bResponse;
            string sWebPage;
            string[] fbArr;

            wUpload.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            bPostArray = Encoding.ASCII.GetBytes(sData);

            try
            {
                bResponse = wUpload.UploadData(sAddress, "POST", bPostArray);
                sWebPage = new string(Encoding.ASCII.GetChars(bResponse));

                // Output the Response Body 
                fbArr = sWebPage.Split('|');
                if (fbArr[0] == "0")
                {
                    _Result = "Message sent successfully.";
                    _Error = false;
                }
                else
                {
                    _Result = "An error occurred. (Error code: " + fbArr[0] + ")";
                    _Error = true;
                    _ErrorCode = fbArr[0];
                }
                return _Error;
            }
            catch (Exception ex)
            {
                _Result = "An error occurred. (Error message: " + ex.Message + ")";
                _Error = true;
                return _Error;
            }
            //Else
            //_Result = "Message, Number, Username or Password not supplied."
            //_Error = True
            //End If
        }

        public int GetCredits(string username, string password)
        {
            UriBuilder urlBuilder = new UriBuilder();
            urlBuilder.Scheme = "http";
            urlBuilder.Host = "bulksms.2way.co.za";
            urlBuilder.Port = 5567;
            urlBuilder.Path = "/eapi/user/get_credits/1/1.1";

            string data = String.Empty;
            data += "username=" + HttpUtility.UrlEncode(username, System.Text.Encoding.GetEncoding("ISO-8859-1"));
            data += "&password=" + HttpUtility.UrlEncode(password, System.Text.Encoding.GetEncoding("ISO-8859-1"));

            urlBuilder.Query = string.Format(data);

            try
            {
                HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(new Uri(urlBuilder.ToString()));
                HttpWebResponse httpResponse = (HttpWebResponse)(httpReq.GetResponse());

                StreamReader input = new StreamReader(httpResponse.GetResponseStream());
                string sWebPage = input.ReadToEnd();
                input.Dispose();
                urlBuilder = null;

                if (sWebPage.StartsWith("0"))
                {
                    sWebPage = sWebPage.Substring(sWebPage.IndexOf("|") + 1, sWebPage.LastIndexOf(".") - 2);
                    return Convert.ToInt32(sWebPage);
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }

        }

        public string ResolveErrorCode(string Code)
        {
            switch ((string)Code)
            {
                case "0":
                    return "The sms was sent successfully.";
                case "22":
                    return "An error occurred during the sms sending procedure. (INTERNAL FATAL ERROR)";
                case "23":
                    return "An error occurred during the sms sending procedure. (AUTHENTICATION FAILURE)";
                case "24":
                    return "An error occurred during the sms sending procedure. (DATA VALIDATION FAILED)";
                case "25":
                    return "An error occurred during the sms sending procedure. (INSUFFICIENT CREDITS)";
                case "26":
                    return "An error occurred during the sms sending procedure. (UPSTREAM INSUFFICIENT CREDITS)";
                case "27":
                    return "An error occurred during the sms sending procedure. (DAILY QUOTA EXCEEDED)";
                case "28":
                    return "An error occurred during the sms sending procedure. (UPSTREAM QUATA EXCEEDED)";
                case "40":
                    return "An error occurred during the sms sending procedure. (TEMPORARILY UNAVAILABLE)";
                default:
                    return "An error occurred during the sms sending procedure. Code: " + Code;
            }
        }

        #endregion

        #region " Private Methods "

        private void PopulateDefault()
        {
            _Numbers = "";
            _Message = "";
            _Username = "";
            _Password = "";
            _Result = "";
            _ErrorCode = "";
            _Error = false;
        }

        private string GetCredits()
        {
            string sAddress = "http://bulksms.2way.co.za:7512/eapi/1.0/get_credits.mc";
            string sData = "username=" + Server.UrlEncode("ForexPeople") + "&password=" + Server.UrlEncode("d3b0n41rs");
            System.Net.WebClient wUpload = new System.Net.WebClient();
            byte[] bPostArray;
            byte[] bResponse;
            string sWebPage;

            wUpload.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            bPostArray = Encoding.ASCII.GetBytes(sData);

            try
            {
                bResponse = wUpload.UploadData(sAddress, "POST", bPostArray);
                sWebPage = new string(Encoding.ASCII.GetChars(bResponse));

                return sWebPage;
            }
            catch
            {
                return "SMS credit server unavailable.";
            }
        }

        #endregion
    }
}
