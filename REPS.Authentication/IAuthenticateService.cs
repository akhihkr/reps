using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace REPS.Authentication
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAuthenticateService" in both code and config file together.
    [ServiceContract]
    public interface IAuthenticateService
    {
        [OperationContract]
        [WebInvoke(Method = "POST",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Wrapped)]
        string DoLogin(string userEmail, string userPassword);

        [OperationContract]
        [WebInvoke(Method = "POST",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Wrapped)]
        bool DoChangePassword(int userId, string currentPassword, string newPassword);
    }
}
