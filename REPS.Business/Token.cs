using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace REPS.Business
{
    public class Token
    {
        public static bool ValidateToken(string token)
        {
            string bearer = "";
            DateTime expirydate = new DateTime();
            DateTime currentTime = DateTime.Now;

            if (!String.IsNullOrEmpty(token))
            {
                ///verify token 
                bearer = Uri.UnescapeDataString(token.Trim());
                //check decrypted userid
                X509Certificate2 signatureCert = Common.CToken.GetSecurityCertificate(ConfigurationManager.AppSettings["certificateName"].ToString());
                if (signatureCert == null)
                {
                    throw new Exception("Certificate missing.");
                }

                string xmlString = Common.CCrypt.Decrypt_Message(bearer, signatureCert);
                var obj = JsonConvert.DeserializeObject<Dictionary<string, string>>(xmlString);
                string aspNetId = "";

                foreach (var st in obj)
                {
                    if (st.Key == "name")
                        aspNetId = st.Value;
                    if (st.Key == "expiry")
                        expirydate = Convert.ToDateTime(st.Value);
                }
                //check token expired
                if (DateTime.Compare(expirydate, currentTime) < 0)
                {
                    throw new WebFaultException<string>("Token Expired", HttpStatusCode.Unauthorized);
                }

                return true;
            }

            throw new WebFaultException<string>("Token Wrong", HttpStatusCode.Unauthorized);
        }
    }
}
