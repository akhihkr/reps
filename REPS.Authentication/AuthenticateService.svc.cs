using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;
using REPS.Authentication;
using REPS.DATA.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using REPS.Business;
using System.Security.Cryptography.X509Certificates;
using System.Configuration;
using System.Xml.Serialization;

namespace REPS.Authentication
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AuthenticateService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AuthenticateService.svc or AuthenticateService.svc.cs at the Solution Explorer and start debugging.
    public class AuthenticateService : IAuthenticateService
    {
        public string DoLogin(string userEmail, string userPassword)
        {
            try
            {
                //var
                bool successUser = true;
                ///Get Message properties
                OperationContext context = OperationContext.Current;
                MessageProperties messageProperties = context.IncomingMessageProperties;
                MessageProperties messagePropertiesOut = context.OutgoingMessageProperties;
                RemoteEndpointMessageProperty endpointProperty = (RemoteEndpointMessageProperty)messageProperties[RemoteEndpointMessageProperty.Name];

                IncomingWebRequestContext requests = WebOperationContext.Current.IncomingRequest;

                //check against database roles
                //1. chk username password match
                string cryptJsonToken = null;
                string loginResultAspNetID = Business.Authentication.DoLogin(userEmail, userPassword, out cryptJsonToken);

                var response = WebOperationContext.Current.OutgoingResponse;
                //+response.Headers.Add("Bearer", Uri.EscapeDataString(outres));
                response.Headers.Add("Bearer", cryptJsonToken);

                return loginResultAspNetID;

            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return ex.Message;

                //to do : add log to logfile to know database cannot connect
            }
        }



        /// <summary>
        /// Change Password from Profile menu
        /// </summary>
        /// <param name="email"></param>
        /// <param name="currentPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public bool DoChangePassword(int userID, string currentPassword, string newPassword)
        {
            try
            {
                return Business.User.ChangeUserPasswordProfile(userID, currentPassword, newPassword);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw ex;
            }

        }
    }
}
