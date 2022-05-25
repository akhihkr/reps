using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Script.Serialization;

namespace REPS.UI.Models
{
    public class AlertsModel
    {
        public string EventName { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string StartingDate { get; set; }
        public string EndDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Filter { get; set; }
        public int EventID { get; set; }
        public string EditEventName { get; set; }
        public string EditLocation { get; set; }
        public string EditDescription { get; set; }

        /// <summary>
        /// get alert info
        /// </summary>
        /// <param name="aspNetId"></param>
        /// <param name="filter"></param>
        /// <param name="dealID"></param>
        /// <returns></returns>
        public static object GetAlertsInfo(string aspNetId, string filter, int dealID)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables
                /// get alert info
                #region get alert info
                using (AlertsServiceReference.AlertsServiceClient alertsServiceClient = new AlertsServiceReference.AlertsServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(alertsServiceClient.InnerChannel))
                    {
                        resultValidator = alertsServiceClient.GetAlertsInfo(aspNetId, filter, dealID);

                        var output = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);

                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            return output;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end get alert info
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// insert alert notification
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="location"></param>
        /// <param name="EventStartDateTime"></param>
        /// <param name="EventEndDateTime"></param>
        /// <param name="description"></param>
        /// <param name="aspNetId"></param>
        /// <param name="AlertTypeID"></param>
        /// <param name="DealID"></param>
        /// <returns></returns>
        public static object InsertAlertInfo(string eventName, string location, System.DateTime EventStartDateTime, System.DateTime EventEndDateTime, string description, string aspNetId, int AlertTypeID, int DealID)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables
                /// insert alert info
                #region get alert info
                using (AlertsServiceReference.AlertsServiceClient alertsServiceClient = new AlertsServiceReference.AlertsServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(alertsServiceClient.InnerChannel))
                    {
                        resultValidator = alertsServiceClient.InsertAlerts(eventName, location, EventStartDateTime, EventEndDateTime, description, aspNetId, 1, DealID);
                        var output = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null && !Common.CString.CheckNullOrEmpty(resultValidator.output))
                        {
                            return output;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end get alert info
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Call WCF to get Diary Items
        /// </summary>
        /// <param name="aspNetId"></param>
        /// <returns></returns>
        public static object GetAlertsForDiaryItems(string aspNetId)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables
                /// Call WCF to get the Diary Items
                #region Call WCF to edit the Event Details
                using (AlertsServiceReference.AlertsServiceClient alertsServiceClient = new AlertsServiceReference.AlertsServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(alertsServiceClient.InnerChannel))
                    {
                        resultValidator = alertsServiceClient.GetAlertsForDiaryItems(aspNetId);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            return new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }

                #endregion end call WCF to edit the Event Details
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        ///  wcf call to update alert
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="location"></param>
        /// <param name="EventStartDateTime"></param>
        /// <param name="EventEndDateTime"></param>
        /// <param name="description"></param>
        /// <param name="eventID"></param>
        /// <returns></returns>
        public static object EditAlertsModel(string eventName, string location, DateTime EventStartDateTime, DateTime EventEndDateTime, string description, int eventID, int dealID)
        {
            try
            {
                #region Variables
                /// Initiate Validator
                Common.CValidator resultValidator = null;
                #endregion end vaiables

                #region logic
                using (AlertsServiceReference.AlertsServiceClient alertsServiceClient = new AlertsServiceReference.AlertsServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(alertsServiceClient.InnerChannel))
                    {
                        resultValidator = alertsServiceClient.UpdateAlerts(eventName, location, EventStartDateTime, EventEndDateTime, description, eventID, dealID);
                        if (resultValidator != null && resultValidator.success == true && resultValidator.output.ToString() != null && !Common.CString.CheckNullOrEmpty(resultValidator.output))
                        {
                            var output = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                            return output;
                        }
                        else
                        {
                            return resultValidator.guid;
                        }
                    }
                    #endregion end logic
                }
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// wcf call to get edit alerts info 
        /// </summary>
        /// <param name="EventID"></param>
        /// <returns></returns>
        public static object GetEditAlertInfoModel(int editEvent)
        {
            try
            {
                #region variables
                //variables
                Common.CValidator resultValidator = null;
                //end of variables
                #endregion end of variables

                #region wcf call to get edit alerts info 
                using (AlertsServiceReference.AlertsServiceClient alertsServiceClient = new AlertsServiceReference.AlertsServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(alertsServiceClient.InnerChannel))
                    {
                        resultValidator = alertsServiceClient.GetEventByID(editEvent);

                        var output = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            return output;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end of wcf call to get edit alerts info
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// WCF call get alerts id from alertsGUID
        /// </summary>
        /// <param name="alertsGUID"></param>
        /// <returns></returns>
        public static object GetAlertsIDByAlertsGUID(string alertsGUID)
        {
            try
            {
                #region Variables
                Common.CValidator resultValidator = null;
                #endregion end vaiables
                using (AlertsServiceReference.AlertsServiceClient alertsServiceClient = new AlertsServiceReference.AlertsServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(alertsServiceClient.InnerChannel))
                    {
                        resultValidator = alertsServiceClient.GetAlertsIDByAlertsGUID(alertsGUID);
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
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// WCF call get deal id from alertsGUID
        /// </summary>
        /// <param name="alertsGUID"></param>
        /// <returns></returns>
        public static object GetDealIDByAlertsGUID(string alertsGUID)
        {
            try
            {
                #region Variables
                Common.CValidator resultValidator = null;
                #endregion end vaiables
                using (AlertsServiceReference.AlertsServiceClient alertsServiceClient = new AlertsServiceReference.AlertsServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(alertsServiceClient.InnerChannel))
                    {
                        resultValidator = alertsServiceClient.GetDealIDByAlertsGUID(alertsGUID);
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
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }



        /// <summary>
        /// WCF call get all details for selected alert
        /// </summary>
        /// <param name="alertsID"></param>
        /// <returns></returns>
        public static object GetEventByID(int alertsID)
        {
            try
            {
                #region Variables
                Common.CValidator resultValidator = null;
                #endregion end vaiables
                using (AlertsServiceReference.AlertsServiceClient alertsServiceClient = new AlertsServiceReference.AlertsServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(alertsServiceClient.InnerChannel))
                    {
                        resultValidator = alertsServiceClient.GetEventByID(alertsID);

                        var output = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);

                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            return output;
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

        public static object UpdateAlertStatus(int alertID, int statusID, int dealID)
        {
            try
            {
                Common.CValidator resultValidator = null;
                #region logic
                using (AlertsServiceReference.AlertsServiceClient alertsServiceClient = new AlertsServiceReference.AlertsServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(alertsServiceClient.InnerChannel))
                    {
                        resultValidator = alertsServiceClient.UpdateAlertsStatus(alertID, statusID, dealID);
                        var output = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            return output;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end logic
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }
    }
}