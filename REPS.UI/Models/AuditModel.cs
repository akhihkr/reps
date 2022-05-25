using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Script.Serialization;

namespace REPS.UI.Models
{
    public class AuditModel
    {
        /// <summary>
        /// get all participant  - audit
        /// </summary>
        /// <param name="DealID"></param>
        /// <returns></returns>
        public static object GetParticipantAudit(int DealID)
        {
            try
            {
                #region Variables
                /// Initiate Validator
                Common.CValidator resultValidator = null;
                #endregion end vaiables
                /// Call WCF to check if we have existing participants per deal
                #region Call WCF to check if we have existing participants per deal
                using (AuditServiceReference.AuditServiceClient auditServiceClient = new AuditServiceReference.AuditServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(auditServiceClient.InnerChannel))
                    {
                        resultValidator = auditServiceClient.GetAudit(DealID);
                        var outputServalCall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            return outputServalCall;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion Call WCF to check if we have existing participants per deal
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// get all participant  - audit
        /// </summary>
        /// <param name="DealID"></param>
        /// <returns></returns>
        public static object GetParticipantAuditDetailsTimeline(int DealID)
        {
            try
            {
                #region Variables
                /// Initiate Validator
                Common.CValidator resultValidator = null;
                #endregion end vaiables
                /// Call WCF to check if we have existing participants per deal
                #region Call WCF to check if we have existing participants per deal
                using (AuditServiceReference.AuditServiceClient auditServiceClient = new AuditServiceReference.AuditServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(auditServiceClient.InnerChannel))
                    {
                        resultValidator = auditServiceClient.GetAuditsDetailsTimeline(DealID);
                        var outputServalCall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            return outputServalCall;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion Call WCF to check if we have existing participants per deal
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }
    }
}