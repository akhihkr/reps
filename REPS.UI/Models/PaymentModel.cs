using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using REPS.UI.EntityServiceReference;
using REPS.DATA.Entity;
using Global;
using System.ServiceModel;

namespace REPS.UI.Models
{
    public class PaymentModel
    {
        public string transactionDetailGUID { get; set; }
        public string paymentDate { get; set; }
        public string paymentTime { get; set; }
        public string paymentDescription { get; set; }
        public string paymentType { get; set; }
        public decimal paymentCredit { get; set; }
        /// <summary>
        /// Get payment list
        /// </summary>
        /// <param name="dealID"></param>
        /// <returns></returns>
        public static List<REPS.UI.Models.PaymentModel> GetPaymentList(int dealID)
        {
            try
            {
                object allPaymentTypeList = Models.PaymentModel.GetPaymentTypeList();
                var paymentTypeList = allPaymentTypeList as IEnumerable<dynamic>;

                #region variables
                List<REPS.UI.Models.PaymentModel> PaymentList = new List<REPS.UI.Models.PaymentModel>();
                Common.CValidator resultValidator = null;
                #endregion

                /// Call WCF to get payment list
                using (PaymentServiceReference.PaymentServiceClient paymentClient = new PaymentServiceReference.PaymentServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(paymentClient.InnerChannel))
                    {
                        resultValidator = paymentClient.GetPaymentList(dealID);
                        object allPayments = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        var Feeslist = allPayments as IEnumerable<dynamic>;
                        #region Create the payment list

                        int paymentCount = 0;

                        string date = string.Empty, time = string.Empty, description = string.Empty, paymentType = string.Empty;
                        decimal credit = 0;
                        foreach (var Fee in Feeslist)
                        {
                            if ((int)Fee["VariableTypeID"] == (int)Enums.FieldType.Date)
                            {
                                var StoredDatetime = Convert.ToString(Fee["Value"]).Split(' ');
                                date = StoredDatetime[0];
                                time = StoredDatetime[1];
                                paymentCount++;
                            }
                            if ((int)Fee["VariableTypeID"] == (int)Enums.FieldType.Comment)
                            {
                                description = Fee["Value"];
                                paymentCount++;
                            }
                            if ((int)Fee["VariableTypeID"] == (int)Enums.FieldType.Type)
                            {
                                int TypeID = Convert.ToInt32(Fee["Value"]);
                                foreach (var FeeType in paymentTypeList)
                                {
                                    if (TypeID == Convert.ToInt32(FeeType["FeeTypeID"]))
                                    {
                                        paymentType = FeeType["Description"];
                                        paymentCount++;
                                    }
                                }
                            }
                            if ((int)Fee["VariableTypeID"] == (int)Enums.FieldType.Value)
                            {
                                credit = Convert.ToDecimal(Fee["Value"]);
                                if (!credit.ToString().Contains("."))
                                {
                                    credit = Convert.ToDecimal(credit.ToString() + ".00");
                                }
                                paymentCount++;
                            }
                            if (paymentCount % 4 == 0)
                            {
                                PaymentList.Add(new REPS.UI.Models.PaymentModel { paymentDate = date, paymentTime = time, paymentType = paymentType, paymentCredit = credit, paymentDescription = description, transactionDetailGUID = Fee["TransactionDetailGUID"] });
                            }
                        }
                        return PaymentList;
                        #endregion Create the payment list
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
        /// Get payment sum
        /// </summary>
        /// <param name="dealID"></param>
        /// <returns></returns>
        public static double GetSumPayments(int dealID)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                double totalPayments = 0;
                #endregion

                /// Call WCF to get payment list
                using (PaymentServiceReference.PaymentServiceClient paymentClient = new PaymentServiceReference.PaymentServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(paymentClient.InnerChannel))
                    {
                        resultValidator = paymentClient.GetPaymentList(dealID);
                        object allPayments = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        var paymentList = allPayments as IEnumerable<dynamic>;
                        foreach (var Fee in paymentList)
                        {
                            if ((int)Fee["VariableTypeID"] == (int)Enums.FieldType.Value)
                            {
                                totalPayments = totalPayments + Convert.ToDouble(Fee["Value"]);
                            }
                        }
                    }
                }
                return totalPayments;
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get payment type list
        /// </summary>
        /// <returns></returns>
        public static object GetPaymentTypeList()
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion

                /// Call WCF to get payment type list
                using (PaymentServiceReference.PaymentServiceClient paymentClient = new PaymentServiceReference.PaymentServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(paymentClient.InnerChannel))
                    {
                        resultValidator = paymentClient.GetPaymentTypeList(null, null);
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
        /// Get add payment fields
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="workflowActionID"></param>
        /// <returns></returns>
        public static object GetAddPaymentFields(int taskID, int workflowActionID)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion

                /// Call WCF to get all payment fields for add form
                using (PaymentServiceReference.PaymentServiceClient paymentClient = new PaymentServiceReference.PaymentServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(paymentClient.InnerChannel))
                    {
                        resultValidator = paymentClient.GetAddPaymentFields(taskID, workflowActionID);
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
        /// Get edit payment fields
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
                Common.CValidator resultValidator = null;
                #endregion

                /// Call WCF to get all payment fields for edit form
                using (PaymentServiceReference.PaymentServiceClient paymentClient = new PaymentServiceReference.PaymentServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(paymentClient.InnerChannel))
                    {
                        resultValidator = paymentClient.GetPaymentEditFields(dealID, transactionID, workflowActionID);
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
        /// Insert payment into DB
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
                Common.CValidator resultValidator = null;
                #endregion

                /// Call WCF to insert a new payment
                using (PaymentServiceReference.PaymentServiceClient paymentClient = new PaymentServiceReference.PaymentServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(paymentClient.InnerChannel))
                    {
                        resultValidator = paymentClient.InsertPayment(formObjects, dealID, userID);
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
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Update payment in DB
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
                Common.CValidator resultValidator = null;
                #endregion

                /// Call WCF to update payment
                using (PaymentServiceReference.PaymentServiceClient paymentClient = new PaymentServiceReference.PaymentServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(paymentClient.InnerChannel))
                    {
                        resultValidator = paymentClient.UpdatePayment(formObjects, dealID, userID);
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
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// WCF call get transaction details id by transactiondetailsGUID
        /// </summary>
        /// <param name="transactionDetailsGUID"></param>
        /// <returns></returns>
        public static object GetTransactionDetailsIDByTransactionDetailsGUID(string transactionDetailsGUID)
        {
            try
            {
                #region Variables
                Common.CValidator resultValidator = null;
                #endregion end vaiables
                using (PaymentServiceReference.PaymentServiceClient paymentClient = new PaymentServiceReference.PaymentServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(paymentClient.InnerChannel))
                    {
                        resultValidator = paymentClient.GetTransactionDetailsIDByTransactionDetailsGUID(transactionDetailsGUID);
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
        /// remove payment and set delete to 1
        /// </summary>
        /// <param name="transactionID"></param>
        /// <param name="userID"></param>
        /// <param name="dealID"></param>
        /// <returns></returns>
        public static object RemovePayment(int transactionID, int userID, int dealID)
        {
            try
            {
                #region Variables        
                /// Initiate Validator
                Common.CValidator resultValidator = null;
                #endregion end vaiables
                /// Call WCF to remove selected payment
                #region logic Call WCF to remove selected payment
                using (PaymentServiceReference.PaymentServiceClient paymentClient = new PaymentServiceReference.PaymentServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(paymentClient.InnerChannel))
                    {
                        resultValidator = paymentClient.RemovePayment(transactionID, userID, dealID);
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
                #endregion end logic  Call WCF to remove selected mortgage
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }
    }
}