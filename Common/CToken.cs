using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// Certificate to be able to encrypt token
    /// </summary>
    public class CToken
    {


        /// <summary>
        /// Look for certificate in local machine
        /// </summary>
        /// <param name="subjectName">Certificate name as parameter</param>
        /// <returns>Certificate or null if not found</returns>
        public static X509Certificate2 GetSecurityCertificate(string subjectName)
        {
            X509Certificate2 securityCert = null;
            X509Store store = new X509Store(StoreName.My,
              StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);
            try
            {
                securityCert = null;
                X509Certificate2Collection certs =
                    store.Certificates.Find(X509FindType.FindBySubjectName,
                    subjectName, true);

                foreach (X509Certificate2 x509 in store.Certificates)
                {

                    if(x509.FriendlyName == subjectName)
                    {
                        
                        securityCert = x509;
                    }
                }

  
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally 
            {
                if (store != null)
                    store.Close();
            }
            return securityCert;
        }
        
        
        
    }
}
