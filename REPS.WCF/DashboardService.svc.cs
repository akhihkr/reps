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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DashboardService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select DashboardService.svc or DashboardService.svc.cs at the Solution Explorer and start debugging.
    public class DashboardService : IDashboardService
    {
        /// <summary>
        /// Get Deals Dashboard
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dealProcessTaskID"></param>
        /// <param name="entityID"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public CValidator GetDashboardDeals(int? userId = null, int? dealProcessTaskID = null, int? entityID = null, int? startRow = null, int? endRow = null)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Dashboard.GetDashboardDeals(userId, dealProcessTaskID, entityID, startRow, endRow)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Get Client reference
        /// </summary>
        /// <param name="dealID"></param>
        /// <returns></returns>
        public CValidator GetClientReference(int dealID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Dashboard.GetClientReference(dealID)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        public CValidator GetDashboardDetailsByID(int objID)
        {

            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Dashboard.GetDashboardDetailsByID(objID)), "Resource.FetchedSuccessfully", true);
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
