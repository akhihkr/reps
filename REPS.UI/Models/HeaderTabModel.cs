using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Script.Serialization;

namespace REPS.UI.Models
{
    public class HeaderTabModel
    {
        /// <summary>
        /// Insert or Delete the User Deal type from the database
        /// </summary>
        /// <param name="DealID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static object ToggleHeaderTabsActivation(string aspNetUserId, bool Status)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion
                #region get all deal type if role is attorney
                using (HeaderTabsServiceReference.HeaderTabServiceClient headerTabServiceClient = new HeaderTabsServiceReference.HeaderTabServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(headerTabServiceClient.InnerChannel))
                    {
                        resultValidator = headerTabServiceClient.ToggleHeaderTabsActivation(aspNetUserId, Status);
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
                #endregion
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Insert or Delete the User Deal type from the database
        /// </summary>
        /// <param name="DealID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static object InsertDeleteUserDeals(int DealID, string aspNetUserId, int Status)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion
                #region get all deal type if role is attorney
                using (HeaderTabsServiceReference.HeaderTabServiceClient headerTabServiceClient = new HeaderTabsServiceReference.HeaderTabServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(headerTabServiceClient.InnerChannel))
                    {
                        resultValidator = headerTabServiceClient.InsertDeleteUserDeals(aspNetUserId, DealID, Status);
                        var outputServalCall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null && !Common.CString.CheckNullOrEmpty(resultValidator.output))
                        {
                            return outputServalCall;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Insert the last view name (state) for the deal
        /// </summary>
        /// <param name="DealID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static object InsertLastViewName(string aspNetUserId, int DealID, string ViewName)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion
                #region get all deal type if role is attorney
                using (HeaderTabsServiceReference.HeaderTabServiceClient headerTabServiceClient = new HeaderTabsServiceReference.HeaderTabServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(headerTabServiceClient.InnerChannel))
                    {
                        resultValidator = headerTabServiceClient.InsertLastViewName(aspNetUserId, DealID, ViewName);
                        var outputServalCall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null && !Common.CString.CheckNullOrEmpty(resultValidator.output))
                        {
                            return outputServalCall;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete all the active deals for the user
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static object DeleteAllUserDeals(string aspNetId)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion
                #region get all deal type if role is attorney
                using (HeaderTabsServiceReference.HeaderTabServiceClient headerTabServiceClient = new HeaderTabsServiceReference.HeaderTabServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(headerTabServiceClient.InnerChannel))
                    {
                        resultValidator = headerTabServiceClient.DeleteAllUserDeals(aspNetId);
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
                #endregion
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get all active deals by UserID
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static object GetUserActiveDeals(string aspNetId)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion
                #region get the user active deals
                using (HeaderTabsServiceReference.HeaderTabServiceClient headerTabServiceClient = new HeaderTabsServiceReference.HeaderTabServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(headerTabServiceClient.InnerChannel))
                    {
                        resultValidator = headerTabServiceClient.GetUserActiveDeals(aspNetId);
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
                #endregion
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get all active deals by UserID
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static object GetUserActiveDealsNotificationBar(string aspNetId)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion
                #region get all deal type if role is attorney
                using (HeaderTabsServiceReference.HeaderTabServiceClient headerTabServiceClient = new HeaderTabsServiceReference.HeaderTabServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(headerTabServiceClient.InnerChannel))
                    {
                        resultValidator = headerTabServiceClient.GetUserNotificationBarActiveDeals(aspNetId);
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
                #endregion
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// get current deal reference
        /// </summary>
        /// <param name="aspNetId"></param>
        /// <returns></returns>
        public static object GetCurrentDealReference(string aspNetId)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion
                #region get all deal type if role is attorney
                using (HeaderTabsServiceReference.HeaderTabServiceClient headerTabServiceClient = new HeaderTabsServiceReference.HeaderTabServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(headerTabServiceClient.InnerChannel))
                    {
                        resultValidator = headerTabServiceClient.GetUserActiveDeals(aspNetId);
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
                #endregion
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }
    }
}