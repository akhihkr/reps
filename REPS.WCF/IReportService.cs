using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace REPS.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IReportService" in both code and config file together.
    [ServiceContract]
    public interface IReportService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        CValidator GetReportNames(object location, object chartName, object chartType, object description, int? startRow, int? endRow);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        CValidator GetReports(string parameters, string storedProcedureName, Dictionary<string, object> formObjects);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        CValidator GetReportFilters(int? reportId, int? startRow, int? endRow);

        #region Dropdown menu service call
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        CValidator GetDropDownFilters(string parameters, string storedProcedureName, Dictionary<string, object> formObjects);
        #endregion end Dropdown menu service call
    }
}
