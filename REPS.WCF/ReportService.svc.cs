using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Script.Serialization;

namespace REPS.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ReportService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ReportService.svc or ReportService.svc.cs at the Solution Explorer and start debugging.
    public class ReportService : IReportService
    {
        /// <summary>
        /// call business to get report names
        /// </summary>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public CValidator GetReportNames(object location, object chartName, object chartType, object description, int? startRow, int? endRow)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Report.GetReportNames(location, chartName, chartType, description, startRow, endRow)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// get detail for chart report
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="storedProcedureName"></param>
        /// <param name="formObjects"></param>
        /// <returns></returns>
        public CValidator GetReports(string storedProcedureName, string parameters, Dictionary<string, object> formObjects)
        {
            try
            {
                return CValidator.initValidator("", CString.DataTableToJSONWithJavaScriptSerializer(Business.Report.GetReports(parameters, storedProcedureName, formObjects)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// call business for filter
        /// </summary>
        /// <param name="reportId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public CValidator GetReportFilters(int? reportId, int? startRow, int? endRow)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Report.GetReportFilters(reportId, startRow, endRow)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// get "dynamic" dropdown filters
        /// </summary>
        /// <param name="dealStatusId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public CValidator GetDropDownFilters(string parameters, string storedProcedureName, Dictionary<string, object> formObjects)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Report.GetDropDownFilters(parameters, storedProcedureName, formObjects)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }

        }
    }
}
