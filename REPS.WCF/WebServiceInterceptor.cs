using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Web;
using Global;
using System.Configuration;
using Newtonsoft.Json;
using System.ServiceModel.Web;
using System.Net;

namespace REPS.WCF
{
    public class WebServiceInterceptor : IDispatchMessageInspector, IServiceBehavior
    {
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            try
            {
                var headers = OperationContext.Current.IncomingMessageProperties[HttpRequestMessageProperty.Name] as HttpRequestMessageProperty;

                DateTime expirydate = new DateTime();
                DateTime expirydate2 = new DateTime();
                DateTime currentTime = DateTime.Now;




                string bearer = "";
                if (headers.Headers["Bearer"] != null || headers.Headers["Bearer"] != "null")
                {
                    bearer = Uri.UnescapeDataString((headers.Headers["Bearer"]).Trim());
                    //check decrypted userid
                    X509Certificate2 signatureCert = Common.CToken.GetSecurityCertificate(ConfigurationManager.AppSettings["certificateName"].ToString());
                    if (signatureCert == null)
                    {
                        Common.CLog.WriteLogInfo("Certificate missing.", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                        throw new WebFaultException<string>("Certificate missing.", HttpStatusCode.Unauthorized);
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
                        Common.CLog.WriteLogInfo("Session Expired: " + aspNetId, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                        throw new WebFaultException<string>("Session Expired", HttpStatusCode.Unauthorized);
                    }

                    // check in db if same session id
                    DATA.Entity.REPS_GetUsersByAspId_Result userTokenResult = (DATA.Entity.REPS_GetUsersByAspId_Result)Business.User.GetUsersByAspId(aspNetId);
                    string uuid = userTokenResult.UserID.ToString();
                    OperationContext.Current.IncomingMessageProperties.Add("uuid", uuid);

                    if (String.Compare(userTokenResult.TokenId, bearer) == 0)
                    {
                        // Add an extension to the current operation context so
                        // that our custom context can be easily accessed anywhere.
                        System.Collections.Generic.List<object> obje = (System.Collections.Generic.List<object>)request.Properties.Values;
                        string absoluteUri = ((System.Uri)(obje.ToList().ElementAt(0))).AbsolutePath;
                        if (absoluteUri.Contains("DealService"))
                        {
                            if (1 == 1)//(REPSBus.CUser.CheckAccess(aspNetId, absoluteUri))
                            {
                                return null;
                            }
                            else
                            {
                                throw new WebFaultException<string>("No Access to this Feature.", HttpStatusCode.Unauthorized);
                            }
                        }
                        return null;
                    }
                    else
                    {
                        string xmlString2 = Common.CCrypt.Decrypt_Message(userTokenResult.TokenId.ToString(), signatureCert);
                        var obj2 = JsonConvert.DeserializeObject<Dictionary<string, string>>(xmlString2);

                        foreach (var st in obj2)
                        {
                            if (st.Key == "expiry")
                                expirydate2 = Convert.ToDateTime(st.Value);
                        }

                        if (DateTime.Compare(expirydate, expirydate2) < 0)
                        {
                            Common.CLog.WriteLogInfo("Logged in somewhere else: " + aspNetId, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                            throw new WebFaultException<string>("Logged in somewhere else.", HttpStatusCode.Unauthorized);
                        }
                        else
                        {
                            Common.CLog.WriteLogInfo("Token Expired: " + aspNetId, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                            throw new WebFaultException<string>("Token Expired.", HttpStatusCode.Unauthorized);
                        }
                    }
                }
                else
                {
                    Common.CLog.WriteLogInfo("Provide Bearer TOKEN", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                    throw new WebFaultException<string>("Provide Bearer TOKEN.", HttpStatusCode.Unauthorized);
                }
            }
            catch (WebFaultException e)
            {
                throw new WebFaultException<string>(e.Message, HttpStatusCode.Unauthorized);
            }
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {

        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher dispatcher in serviceHostBase.ChannelDispatchers)
            {
                foreach (var endpoint in dispatcher.Endpoints)
                {
                    endpoint.DispatchRuntime.MessageInspectors.Add(new WebServiceInterceptor());
                }
            }
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints,
            BindingParameterCollection bindingParameters)
        {
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }
    }

    public class WebServiceInterceptorExtension : BehaviorExtensionElement
    {
        protected override object CreateBehavior()
        {
            return new WebServiceInterceptor();
        }

        public override Type BehaviorType
        {
            get
            {
                return typeof(WebServiceInterceptor);
            }
        }
    }
}