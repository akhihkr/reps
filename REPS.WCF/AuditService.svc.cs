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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AuditService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AuditService.svc or AuditService.svc.cs at the Solution Explorer and start debugging.
    public class AuditService : IAuditService
    {
        /// <summary>
        /// get audit detals
        /// </summary>
        /// <param name="DealID"></param>
        /// <returns></returns>
        public CValidator GetAudit(int DealID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Audit.GetAudits(DealID)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "ouldNotGetResults", false);
            }
        }

        /// <summary>
        /// get audit details to timeline js
        /// </summary>
        /// <param name="DealID"></param>
        /// <returns></returns>
        public CValidator GetAuditsDetailsTimeline(int DealID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Audit.GetAuditsDetailsTimeline(DealID)), "FetchedSuccessfully", true);
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
