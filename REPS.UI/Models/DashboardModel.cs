using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Script.Serialization;

namespace REPS.UI.Models
{
    public class DashboardModel
    {
        /// <summary>
        /// Get Client Reference
        /// </summary>
        /// <param name="dealID"></param>
        /// <returns></returns>
        public static object GetClientReference(int dealID)
        {
            try
            {
                #region variable
                Common.CValidator resultValidator = null;
                #endregion end Variables 

                #region wcf to call 
                using (DashboardServiceReference.DashboardServiceClient client = new DashboardServiceReference.DashboardServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
                    {
                        resultValidator = client.GetClientReference(dealID);
                        var formdata = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            ///Created Deal id save into Sessin for reuse
                            return formdata;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion wcf to call
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Server call to get dashboard deals
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static object[] GetDashboardDeals(int? UserId = null, int? dealProcessTaskID = null, int? entityID = null, int? startRow = null, int? endRow = null)
        {
            try
            {
                #region Variables
                Common.CValidator resultValidator = null;
                #endregion end variables
                using (DashboardServiceReference.DashboardServiceClient client = new DashboardServiceReference.DashboardServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
                    {
                        resultValidator = client.GetDashboardDeals(UserId, dealProcessTaskID, entityID, startRow, endRow);
                        var outputServerCall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            ///Created Deal id save into Sessin for reuse
                            return outputServerCall;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// get correspondence info
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="dealID"></param>
        /// <returns></returns>
        public static object GetDashboardDetailsByID(int objID)
        {
            #region variables
            /// variable
            Common.CValidator resultValidator = null;
            #endregion end variables
            /// Call WCF to get all the Correspondence emails
            #region Call WCF to get all the Correspondence emails
            using (DashboardServiceReference.DashboardServiceClient client = new DashboardServiceReference.DashboardServiceClient())
            {
                //operation context to read headers
                using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
                {
                    resultValidator = client.GetDashboardDetailsByID(objID);
                    var outputServerCall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                    if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                    {
                        ///Created Deal id save into Sessin for reuse
                        return outputServerCall;
                    }
                    else
                    {
                        throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                    }
                }
            }
            #endregion end Call WCF to get all the Correspondence emails
        }
    }
}