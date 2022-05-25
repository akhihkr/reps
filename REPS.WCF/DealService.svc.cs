using Common;
using Global;
using REPS.Business;
using REPS.DATA.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Web.Script.Serialization;

namespace REPS.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DealService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select DealService.svc or DealService.svc.cs at the Solution Explorer and start debugging.
    public class DealService : IDealService
    {
        /// <summary>
        /// add deal when created
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public CValidator AddDeal(DATA.Entity.Deal obj, string AspNetUsersId)
        {
            try
            {
                ///variables
                int? result;
                var serializer = new JavaScriptSerializer();
                int userID = Convert.ToInt32(Business.Deal.GetUserID(AspNetUsersId));
                ///end of variables

                #region verifytoken

                Token.ValidateToken(System.ServiceModel.Web.WebOperationContext.Current.IncomingRequest.Headers["Bearer"]);

                #endregion

                result = Business.Deal.AddDeal(obj, AspNetUsersId);

                if (result == -2)
                {
                    return CValidator.initValidator("", serializer.Serialize(result), "AlreadyExists", true);
                }
                else
                {
                    obj.DealID = result.Value;
                    ///add transaction log
                    Business.LogTransaction.InsertTransactionLog((new DATA.Entity.Transaction() { DealID = obj.DealID, TransactionTypeID = (int)Enums.TransactionType.New, TransactionStatusID = 4 }), (int)Enums.WokflowTask.Deal, userID);

                    ///audit add              
                    //AuditBusiness.AddAudit(new Audit() { ForeignKey = result.ToString(), TableName = "Deal", Description = "ActionAdd", UserID = userId }, "DealID", Utilities.CString.ConvertToXMLParametersAdd(obj));

                    return CValidator.initValidator("", serializer.Serialize(result), "AddedSuccessfully", true);
                }
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// call to get deal
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="entityID"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public CValidator GetDeal(DATA.Entity.Deal obj, int? taskID, int entityID, int? startRow = null, int? endRow = null)
        {
            try
            {
                var request = OperationContext.Current.IncomingMessageProperties[HttpRequestMessageProperty.Name] as HttpRequestMessageProperty;
                var version = request.Headers["Bearer"];

                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Deal.GetDeal(obj, taskID, entityID, startRow, endRow)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// get deal types
        /// </summary>
        /// <param name="dealTypeId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public CValidator GetDealTypes(int? dealTypeId = null, int? startRow = null, int? endRow = null)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Deal.GetDealTypes(dealTypeId, startRow, endRow)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }

        }

        /// <summary>
        /// get user id 
        /// </summary>
        /// <param name="dealTypeId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public CValidator GetUserID(string aspNetUserId)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Deal.GetUserID(aspNetUserId)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }

        }

        /// <summary>
        /// update deal 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public CValidator UpdateDeal(DATA.Entity.Deal obj)
        {
            try
            {
                #region add to transaction log
                int userId = Convert.ToInt32(OperationContext.Current.IncomingMessageProperties["uuid"].ToString());
                LogTransaction.InsertTransactionLog((new Transaction() { DealID = obj.DealID, TransactionTypeID = (int)Enums.TransactionType.Edit, TransactionStatusID = 7 }), (int)Enums.WokflowTask.Deal, userId);
                #endregion end add to transaction log

                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Deal.UpdateDeal(obj)), "UpdatedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// get deal id by dealGUID
        /// </summary>
        /// <param name="dealGUID"></param>
        /// <returns></returns>
        public CValidator GetDealIDByDealUniqueRef(string dealUniqueRef)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Deal.GetDealIDByDealUniqueRef(dealUniqueRef)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Set new completion date for deal
        /// </summary>
        /// <param name="DealID"></param>
        /// <param name="NewDate"></param>
        /// <returns></returns>
        public CValidator EditCompletionDate(int dealID, DateTime newDate)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                int? result;
                if ((result = Business.Deal.EditCompletionDate(dealID, newDate)) > 0)
                {
                    int userId = Convert.ToInt32(OperationContext.Current.IncomingMessageProperties["uuid"].ToString());
                    LogTransaction.InsertTransactionLog((new Transaction() { DealID = dealID, TransactionTypeID = (int)Enums.TransactionType.Edit, TransactionStatusID = 7 }), (int)Enums.WokflowTask.Deal, userId);

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

        /// <summary>
        /// Get completion date
        /// </summary>
        /// <param name="DealID"></param>
        /// <returns></returns>
        public CValidator GetCompletionDate(int DealID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Deal.GetCompletionDate(DealID)), "Resource.FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Get Completed Actions
        /// </summary>
        /// <param name="dealID"></param>
        /// <param name="workflowID"></param>
        /// <returns></returns>
        public CValidator GetCompletedActionsByWorkflowID(int dealID, int workflowID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Deal.GetCompletedActions(dealID, workflowID)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Get mandatory Actions list
        /// </summary>
        /// <param name="workflowID"></param>
        /// <returns></returns>
        public CValidator GetMandatoryActionsList(int workflowID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Deal.GetMandatoryActionsList(workflowID)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Get last action by user
        /// </summary>
        /// <param name="dealID"></param>
        /// <returns></returns>
        public CValidator GetDealLastActionBy(int dealID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Deal.GetDealLastActionBy(dealID)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// get current workflow step
        /// </summary>
        /// <param name="dealID"></param>
        /// <param name="workflowID"></param>
        /// <param name="userID"></param>
        /// <param name="entityID"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public CValidator GetCurrentWorkflowStep(int dealID, int workflowID, int? userID = null, int? entityID = null, int? startDate = null, int? endDate = null)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", Common.CString.DataTableToJSONWithJavaScriptSerializer(Business.Deal.GetCurrentWorkflowStep(dealID, workflowID, userID, entityID, startDate, endDate)), "FetchedSuccessfully", true);
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
