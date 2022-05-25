using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Script.Serialization;
using Global;

namespace REPS.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PaymentService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select PaymentService.svc or PaymentService.svc.cs at the Solution Explorer and start debugging.
    public class PaymentService : IPaymentService
    {
        /// <summary>
        /// Get all payment fields for the add view
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="workflowActionID"></param>
        /// <returns></returns>
        public CValidator GetAddPaymentFields(int taskID, int workflowActionID)
        {

            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Payment.GetAddPaymentFields(taskID, workflowActionID)), "Resource.FetchedSuccessfully", true);

            }
            catch (Exception ex)
            {

                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);

            }

        }

        /// <summary>
        /// Get all payment fields for the edit view
        /// </summary>
        /// <param name="dealID"></param>
        /// <param name="transactionID"></param>
        /// <param name="workflowActionID"></param>
        /// <returns></returns>
        public CValidator GetPaymentEditFields(int dealID, int transactionID, int workflowActionID)
        {

            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Payment.GetPaymentEditFields(dealID, transactionID, workflowActionID)), "Resource.FetchedSuccessfully", true);

            }
            catch (Exception ex)
            {

                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);

            }

        }

        /// <summary>
        /// Get payment list
        /// </summary>
        /// <param name="dealID"></param>
        /// <returns></returns>
        public CValidator GetPaymentList(int dealID)
        {

            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Payment.GetPaymentList(dealID)), "Resource.FetchedSuccessfully", true);

            }
            catch (Exception ex)
            {

                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);

            }

        }

        /// <summary>
        /// Get payment type list
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public CValidator GetPaymentTypeList(DateTime? startDate, DateTime? endDate)
        {

            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Payment.GetPaymentTypeList(startDate, endDate)), "Resource.FetchedSuccessfully", true);

            }
            catch (Exception ex)
            {

                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);

            }

        }

        /// <summary>
        /// Update existing payment
        /// </summary>
        /// <param name="formObjects"></param>
        /// <param name="dealID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public CValidator UpdatePayment(Dictionary<string, object> formObjects, int dealID, int userID)
        {

            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Payment.UpdatePayment(formObjects, dealID, userID)), "Resource.FetchedSuccessfully", true);

            }
            catch (Exception ex)
            {

                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);

            }

        }

        /// <summary>
        /// Insert a new payment
        /// </summary>
        /// <param name="formObjects"></param>
        /// <param name="dealID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public CValidator InsertPayment(Dictionary<string, object> formObjects, int dealID, int userID)
        {

            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Payment.InsertPayment(formObjects, dealID, userID)), "Resource.FetchedSuccessfully", true);

            }
            catch (Exception ex)
            {

                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);

            }

        }


        /// <summary>
        /// Get Transaction Details ID
        /// </summary>
        /// <param name="transactionDetailsGUID"></param>
        /// <returns></returns>
        public CValidator GetTransactionDetailsIDByTransactionDetailsGUID(string transactionDetailsGUID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Payment.GetTransactionDetailsIDByTransactionDetailsGUID(transactionDetailsGUID)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }


        /// <summary>
        /// Remove payment from the database
        /// </summary>
        /// <param name="transactionID"></param>
        /// <param name="userID"></param>
        /// <param name="dealID"></param>
        /// <returns></returns>
        public CValidator RemovePayment(int transactionID, int userID, int dealID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                Business.LogTransaction.InsertTransactionLog((new DATA.Entity.Transaction() { DealID = dealID, TransactionTypeID = (int)Enums.TransactionType.Remove, TransactionStatusID = 6 }), (int)Enums.WokflowTask.Fees, userID);
                return CValidator.initValidator("", serializer.Serialize(Business.Payment.RemovePayment(transactionID, userID, dealID)), "FetchedSuccessfully", true);
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
