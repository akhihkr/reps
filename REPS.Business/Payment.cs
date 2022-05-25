using Global;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPS.Business
{
    public class Payment
    {
        /// <summary>
        /// Get Payment fields for Add View
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="workflowActionID"></param>
        /// <returns></returns>
        public static object GetAddPaymentFields(int taskID, int workflowActionID)
        {
            try
            {
                #region variables

                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;

                #endregion

                #region logic

                results = REPSDB.REPS_GetAddPaymentFields(taskID, workflowActionID);
                return results;

                #endregion
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Get all Payment fields values for Edit View
        /// </summary>
        /// <param name="dealID"></param>
        /// <param name="transactionID"></param>
        /// <param name="workflowActionID"></param>
        /// <returns></returns>
        public static object GetPaymentEditFields(int dealID, int transactionID, int workflowActionID)
        {
            try
            {
                #region variables

                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;

                #endregion

                #region logic

                results = REPSDB.REPS_GetEditPaymentFields(dealID, transactionID, workflowActionID);
                return results;

                #endregion
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// Get all Payment for deal
        /// </summary>
        /// <param name="dealID"></param>
        /// <returns></returns>
        public static object GetPaymentList(int dealID)
        {
            try
            {
                #region variables

                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;

                #endregion

                #region logic

                results = REPSDB.REPS_GetPaymentList(dealID, (int)Enums.WokflowTask.Fees);
                return results;

                #endregion


            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// Get Payment Type List
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static object GetPaymentTypeList(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                #region variables

                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;

                #endregion

                #region logic

                results = REPSDB.REPS_GetPaymentTypeList(startDate, endDate);
                return results;

                #endregion


            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// Save Payment fields values for Edit View
        /// </summary>
        /// <param name="formObjects"></param>
        /// <param name="dealID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static object UpdatePayment(Dictionary<string, object> formObjects, int dealID, int userID)
        {
            try
            {
                #region variables

                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                Dictionary<string, object> tempnormalfields = new Dictionary<string, object>();
                Dictionary<string, object> tempdatefields = new Dictionary<string, object>();
                //int NewTransactionID = 0;
                int OldTransactionID = 0;
                int WorkflowActionVarID = 0;
                int VariableTypeID = 0;
                dynamic VariableTypeIDValue = null;
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                ObjectParameter FeesRowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion

                #region logic

                // filter and remove any unwanted fields
                foreach (var item in formObjects)
                {
                    if (item.Key.Contains("FieldType"))
                    {
                        tempnormalfields.Add(item.Key, item.Value);

                        var IDArray = item.Key.Split(':');
                        OldTransactionID = Convert.ToInt32(IDArray[1]);
                    }
                }


                // Archive old values
                var TID = REPSDB.REPS_ArchivePaymentByTransactionID(OldTransactionID, dealID, (int)Enums.TransactionType.Edit, 4, (int)Enums.WokflowTask.Fees, userID, rowCount);
                int NewTransactionID = Convert.ToInt32(rowCount.Value);

                foreach (var item in tempnormalfields)
                {
                    var IDArray = item.Key.Split(':');
                    //TransactionID = Convert.ToInt32(IDArray[1]);
                    WorkflowActionVarID = Convert.ToInt32(IDArray[2]);
                    VariableTypeID = Convert.ToInt32(IDArray[3]);
                    //check if type is value, then remove "," from string
                    if ((int?)VariableTypeID == (int)Enums.FieldType.Value)
                    {
                        VariableTypeIDValue = item.Value.ToString().Replace(",", string.Empty);
                    }
                    else
                    {
                        VariableTypeIDValue = item.Value.ToString();
                    }

                    results = REPSDB.REPS_UpdatePayment(NewTransactionID, WorkflowActionVarID, VariableTypeID, VariableTypeIDValue, FeesRowCount);
                }


                return (FeesRowCount.Value == DBNull.Value ? null : (int?)FeesRowCount.Value);

                #endregion


            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// Save Payment fields values for Add View
        /// </summary>
        /// <param name="formObjects"></param>
        /// <param name="dealID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static object InsertPayment(Dictionary<string, object> formObjects, int dealID, int userID)
        {
            try
            {
                #region variables

                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                Dictionary<string, object> tempnormalfields = new Dictionary<string, object>();
                int WorkflowTaskID = 0;
                int WorkflowActionVarID = 0;
                int VariableTypeID = 0;
                dynamic VariableTypeIDValue = null;
                ObjectParameter rowCount = new ObjectParameter("transactionID", typeof(int));
                ObjectParameter paymentRowCount = new ObjectParameter("identity", typeof(int));
                #endregion

                #region logic

                // filter and remove any unwanted fields
                foreach (var item in formObjects)
                {
                    if (item.Key.Contains("FieldType"))
                    {

                        tempnormalfields.Add(item.Key, item.Value);
                    }
                }

                // Insert into transaction table 
                var TID = REPSDB.REPS_AddPaymentTransaction(dealID, (int)Enums.TransactionType.New, 4, (int)Enums.WokflowTask.Fees, userID, rowCount);
                int transactionID = Convert.ToInt32(rowCount.Value);

                foreach (var item in tempnormalfields)
                {
                    var IDArray = item.Key.Split(':');
                    WorkflowTaskID = Convert.ToInt32(IDArray[1]);
                    WorkflowActionVarID = Convert.ToInt32(IDArray[2]);
                    VariableTypeID = Convert.ToInt32(IDArray[3]);
                    //check if type is value, then remove "," from string
                    if ((int?)VariableTypeID == (int)Enums.FieldType.Value)
                    {
                        VariableTypeIDValue = item.Value.ToString().Replace(",", string.Empty);
                    }
                    else
                    {
                        VariableTypeIDValue = item.Value.ToString();
                    }

                    results = REPSDB.REPS_AddPayment(dealID, transactionID, userID, WorkflowTaskID, WorkflowActionVarID, VariableTypeID, VariableTypeIDValue, paymentRowCount);
                }


                return (paymentRowCount.Value == null ? null : (int?)paymentRowCount.Value);

                #endregion


            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }


        /// <summary>
        /// store procedure call to get Transaction Details ID
        /// </summary>
        /// <param name="mortgageGUID"></param>
        /// <returns></returns>
        public static object GetTransactionDetailsIDByTransactionDetailsGUID(string transactionDetailsGUID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end variables

                #region logic
                results = REPSDB.REPS_GetTransactionDetailsIDByTransactionDetailsGUID(transactionDetailsGUID).FirstOrDefault();
                return results;
                #endregion end logic
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// wcf call to remove payment per id
        /// </summary>
        /// <param name="transactionID"></param>
        /// <param name="userID"></param>
        /// <param name="dealID"></param>
        /// <returns></returns>
        public static object RemovePayment(int transactionID, int userID, int dealID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion end of variables

                #region logic
                
                REPSDB.REPS_DeletePaymentByTransactionID(transactionID, rowCount);
                return (rowCount.Value == null ? null : (int?)rowCount.Value);
                #endregion
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
