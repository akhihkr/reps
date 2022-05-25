using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REPS.DATA.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using System.Security.Cryptography.X509Certificates;
using System.Configuration;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Global;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Xml;
using Newtonsoft.Json;

namespace REPS.Business
{
    [DataContract]
    public class Authentication
    {

        #region properties 
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string role { get; set; }
        [DataMember]
        public DateTime expiry { get; set; }
        [DataMember]
        public string product { get; set; }
        [DataMember]
        public string ip { get; set; }
        #endregion

        /// <summary>
        /// login function check by email and password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string DoLogin(string email, string password, out string cryptJsonToken)
        {
            try
            {
                #region variables
                AuthenticateIdentityContext _dbAuth;

                _dbAuth = new AuthenticateIdentityContext();

                //use identity to look for user
                UserStore<MyIdentityUser> userStore = new UserStore<MyIdentityUser>(_dbAuth);
                var userManager = new UserManager<MyIdentityUser>(userStore);

                cryptJsonToken = null;
                #endregion variables

                MyIdentityUser user = userManager.FindByEmail(email);


                if (user != null && userManager.CheckPassword(user, password))
                {

                    #region CreateToken and add to DB User

                    X509Certificate2 signatureCert = Common.CToken.GetSecurityCertificate(ConfigurationManager.AppSettings["certificateName"].ToString());
                    if (signatureCert == null)
                    {
                        throw new Exception("Certificate missing.");
                    }


                    string jsonToken = JsonConvert.SerializeObject(
                                        new Authentication
                                        {
                                            name = user.Id,
                                            expiry = DateTime.Now.Add(TimeSpan.FromMinutes(Convert.ToDouble(ConfigurationManager.AppSettings["UserTimeout"].ToString()))),
                                                //ip = "127.0.0.1"
                                            }
                    );

                    cryptJsonToken = Common.CCrypt.Encrypt_Message(jsonToken, signatureCert);
                    #endregion

                    // add session id to reps user db
                    #region SaveTokenToDBUserTable

                    DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                    ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                    int? userID = (int?)Business.Deal.GetUserID(user.Id);

                    //to do : add validation if user is not found

                    #region check if user account is deleted
                    object results = null;
                    results = REPSDB.REPS_GetUserDeletedStatus(user.Id).FirstOrDefault();
                    bool accountDeactivated = (bool)results;
                    if (accountDeactivated)
                    {
                        return Enums.UserAccountDeactivated.deactivated.ToString();
                    }
                    #endregion check if user account is deleted


                    REPSDB.REPS_UpdateUserToken(cryptJsonToken, user.Id, rowCount);
                    ///save to transaction log file
                    //return (identity.Value == null ? null : (int?)identity.Value);
                    //    #endregion end logic save data to db   
                    //_dbFunc.reps_UpdateUserToken(xml, user.Id);

                    #endregion


                    return user.Id;

                }
                else
                {
                    return null;
                }

            }
            catch (Exception Ex)
            {

                throw Ex;
            }

        }
    }
}
