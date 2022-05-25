using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Script.Serialization;
using Common;
using Global;

namespace REPS.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AlertsService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AlertsService.svc or AlertsService.svc.cs at the Solution Explorer and start debugging.
    public class AlertsService : IAlertsService
    {
        public CValidator InsertAlerts(string EventName, string Location, DateTime StartDate, DateTime EndDate, string Description, string aspNetId, int AlertTypeID, int DealID)
        {

            try
            {
                var serializer = new JavaScriptSerializer();
                int? result;
                result = Business.Alerts.InsertAlerts(EventName, Location, StartDate, EndDate, Description, aspNetId, AlertTypeID, DealID);

                if (result == -2) // if alert cannot be added
                {
                    return CValidator.initValidator("", serializer.Serialize(result), "AlertsExists", true);
                }
                else
                {
                    return CValidator.initValidator("", serializer.Serialize(result), "AddedSuccessfully", true);
                }
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotAddAlerts", false);
            }
        }

        public CValidator UpdateAlerts(string EventName, string Location, DateTime StartDate, DateTime EndDate, string Description, int AlertID, int dealID)
        {
            string thisGuid = Guid.NewGuid().ToString();
            try
            {
                var serializer = new JavaScriptSerializer();
                int? result;

                if ((result = Business.Alerts.UpdateAlerts(EventName, Location, StartDate, EndDate, Description, AlertID)) > 0)
                {
                    int userId = Convert.ToInt32(OperationContext.Current.IncomingMessageProperties["uuid"].ToString());
                    Business.LogTransaction.InsertTransactionLog((new DATA.Entity.Transaction() { DealID = dealID, TransactionTypeID = (int)Enums.TransactionType.Edit, TransactionStatusID = 7 }), (int)Enums.WokflowTask.Alerts, userId);
                    return CValidator.initValidator("", serializer.Serialize(result), "UpdateSuccess", true);
                }
                else
                {
                    return CValidator.initValidator(thisGuid, serializer.Serialize(result), "UpdateFailed", false);
                }

            }
            catch (Exception ex)
            {

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }
        }

        public CValidator UpdateAlertsStatus(int AlertID, int StatusID, int dealID)
        {

            try
            {
                int transactionStatusID = 0;
                int transactionType = 0;
                if (StatusID == 2) // Completed
                {
                    transactionStatusID = 5;
                    transactionType = (int)Enums.TransactionType.Completed;
                }
                else if (StatusID == 4) // Archived
                {
                    transactionStatusID = 6;
                    transactionType = (int)Enums.TransactionType.Archived;
                }
                var serializer = new JavaScriptSerializer();
                int? result;

                if ((result = Business.Alerts.UpdateAlertsStatus(AlertID, StatusID)) > 0)
                {
                    int userId = Convert.ToInt32(OperationContext.Current.IncomingMessageProperties["uuid"].ToString());
                    Business.LogTransaction.InsertTransactionLog((new DATA.Entity.Transaction() { DealID = dealID, TransactionTypeID = transactionType, TransactionStatusID = transactionStatusID }), (int)Enums.WokflowTask.Alerts, userId);
                    return CValidator.initValidator("", serializer.Serialize(result), "UpdateSuccess", true);
                }
                else
                {
                    return CValidator.initValidator("", serializer.Serialize(result), "UpdateFailed", false);
                }
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }
        }

        public CValidator GetAlertsInfo(string aspNetId, string Filter, int DealID)
        {

            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Alerts.GetAlertsInfo(aspNetId, Filter, DealID)), "Resource.FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }

        }

        public CValidator GetEventByID(int EventID)
        {

            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Alerts.GetEventByID(EventID)), "Resource.FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }

        }

        public CValidator GetAlertsForDiaryItems(string aspNetId)
        {

            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Alerts.GetAlertsForDiaryItems(aspNetId)), "Resource.FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }

        }

        public CValidator GetAlertsIDByAlertsGUID(string alertsGUID)
        {

            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Alerts.GetAlertsIDByAlertsGUID(alertsGUID)), "Resource.FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }

        }

        public CValidator GetDealIDByAlertsGUID(string alertsGUID)
        {

            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Alerts.GetDealIDByAlertsGUID(alertsGUID)), "Resource.FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }

        }
    }
}
