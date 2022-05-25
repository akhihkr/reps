using REPS.DATA.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Script.Serialization;

namespace REPS.UI.Models
{
    public class MortgageModel
    {
        /// <summary>
        /// Call WCF to get all mortgage lenders
        /// </summary>
        /// <returns></returns>
        public static object MortgageLender()
        {
            try
            {
                #region Variables
                /// Initiate Validator
                Common.CValidator resultValidator = null;
                #endregion end vaiables
                /// Call WCF to get all mortgage lenders
                #region logic call WCF to get all mortgage lenders
                using (MortgageServiceReference.MortgageServiceClient mortgageServiceClient = new MortgageServiceReference.MortgageServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(mortgageServiceClient.InnerChannel))
                    {
                        resultValidator = mortgageServiceClient.GetMortgageLender();
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
                #endregion end logic call WCF to get all mortgage lenders
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        ///  Call WCF to get all mortgage types
        /// </summary>
        /// <returns></returns>
        public static object MortgageType()
        {
            try
            {
                #region Variables
                /// Initiate Validator
                Common.CValidator resultValidator = null;
                #endregion end vaiables
                /// Call WCF to get all mortgage type
                #region WCF to get all mortgage type
                using (MortgageServiceReference.MortgageServiceClient mortgageServiceClient = new MortgageServiceReference.MortgageServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(mortgageServiceClient.InnerChannel))
                    {
                        resultValidator = mortgageServiceClient.GetMortgageType();
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
                #endregion WCF to get all mortgage type 
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        ///  Call WCF to get all organisation types
        /// </summary>
        /// <returns></returns>
        public static object InterestType()
        {
            try
            {
                #region Variables
                /// Initiate Validator
                Common.CValidator resultValidator = null;
                #endregion end vaiables
                /// Call WCF to get all organisation types
                #region WCF to get all mortgage type
                using (MortgageServiceReference.MortgageServiceClient mortgageServiceClient = new MortgageServiceReference.MortgageServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(mortgageServiceClient.InnerChannel))
                    {
                        resultValidator = mortgageServiceClient.GetMortgageInterestType();
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
                #endregion WCF to get all mortgage type
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Add financial instrument to db
        /// </summary>
        /// <param name="financialInstrumentInputValues"></param>
        /// <param name="LenderID"></param>
        /// <param name="InstrumentTypeID"></param>
        /// <param name="InterestTypeID"></param>
        /// <param name="DealID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static object InsertFinancialInstrument(FinancialInstrument financialInstrumentInputValues, int LenderID, int InstrumentTypeID, int InterestTypeID, int DealID, string aspNetId)
        {
            try
            {
                #region Variables
                /// Initiate Validator
                Common.CValidator resultValidator = null;
                #endregion end vaiables
                /// Call WCF for financialtransaction add    
                #region Servercall financialtransaction add                          
                using (MortgageServiceReference.MortgageServiceClient mortgageServiceClient = new MortgageServiceReference.MortgageServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(mortgageServiceClient.InnerChannel))
                    {
                        resultValidator = mortgageServiceClient.InsertFinancialInstrument(financialInstrumentInputValues, LenderID, InstrumentTypeID, InterestTypeID, DealID, aspNetId);
                      
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
                }
                #endregion End Servercall financialtransaction add 
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// savetransation mortgage 
        /// </summary>
        /// <param name="DealID"></param>
        /// <param name="TransactionID"></param>
        /// <returns></returns>
        public static object SaveTansaction(FinancialTransaction transaction, int DealID, int TransactionID)
        {
            try
            {
                #region form Values financial Instrument
                Common.CValidator resultValidatorPart = null;
                #endregion End form values financial Instrument
                /// Call WCF to save transaction 
                #region Call WCF to save transaction 
                using (MortgageServiceReference.MortgageServiceClient mortgageServiceClient = new MortgageServiceReference.MortgageServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(mortgageServiceClient.InnerChannel))
                    {
                        resultValidatorPart = mortgageServiceClient.InsertFinancialTransaction(transaction, DealID, TransactionID);
                        var output = new JavaScriptSerializer().Deserialize<dynamic>(resultValidatorPart.output);
                        if (resultValidatorPart != null && resultValidatorPart.success && resultValidatorPart.output.ToString() != null && !Common.CString.CheckNullOrEmpty(resultValidatorPart.output))
                        {
                            return output;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion Call WCF to save transaction
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Update Financial Instrument
        /// </summary>
        /// <param name="financialInstrumentInputValues"></param>
        /// <param name="LenderID"></param>
        /// <param name="InstrumentTypeID"></param>
        /// <param name="InterestTypeID"></param>
        /// <param name="DealID"></param>
        /// <returns></returns>
        public static object UpdateFinancialInstrument(FinancialInstrument financialInstrumentInputValues, int LenderID, int InstrumentTypeID, int InterestTypeID, int DealID)
        {
            try
            {
                #region Variables
                /// Initiate Validator
                Common.CValidator resultValidator = null;
                #endregion end vaiables
                /// Call WCF for financialtransaction add    
                #region Servercall financialtransaction add                          
                using (MortgageServiceReference.MortgageServiceClient mortgageServiceClient = new MortgageServiceReference.MortgageServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(mortgageServiceClient.InnerChannel))
                    {
                        resultValidator = mortgageServiceClient.UpdateFinancialInstrument(financialInstrumentInputValues, LenderID, InstrumentTypeID, InterestTypeID, DealID);                   
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
                }
                #endregion End Servercall financialtransaction add 
            }

            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// get all mortgage details by its Deal-ID
        /// </summary>
        /// <param name="dealID"></param>
        /// <returns></returns>
        public static object GetMortgage(int dealID, int? mortgageID)
        {
            try
            {
                #region Variables  
                /// Initiate Validator
                Common.CValidator resultValidator = null;
                #endregion end vaiables  
                /// Call WCF to check if we have existing mortgages per deal
                #region logic to check if we have existing mortgages per deal
                using (MortgageServiceReference.MortgageServiceClient mortgageServiceClient = new MortgageServiceReference.MortgageServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(mortgageServiceClient.InnerChannel))
                    {
                        resultValidator = mortgageServiceClient.GetAllMortgages(dealID, mortgageID);
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
                #endregion end logic to check if we have existing mortgages per deal
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// remove mortgage and set delete to 1
        /// </summary>
        /// <param name="MortgageID"></param>
        /// <returns></returns>
        public static object RemoveMortgage(int MortgageID,int DealID)
        {
            try
            {
                #region Variables        
                /// Initiate Validator
                Common.CValidator resultValidator = null;
                #endregion end vaiables
                /// Call WCF to remove selected mortgage
                #region logic Call WCF to remove selected mortgage
                using (MortgageServiceReference.MortgageServiceClient mortgageServiceClient = new MortgageServiceReference.MortgageServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(mortgageServiceClient.InnerChannel))
                    {
                        resultValidator = mortgageServiceClient.RemoveMortgages(MortgageID, DealID);
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

        /// <summary>
        /// WCF call get mortgage id by mortgageGUID
        /// </summary>
        /// <param name="mortgageGUID"></param>
        /// <returns></returns>
        public static object GetMortgageIDByMortgageGUID(string mortgageGUID)
        {
            try
            {
                #region Variables
                Common.CValidator resultValidator = null;
                #endregion end vaiables
                using (MortgageServiceReference.MortgageServiceClient mortgageServiceClient = new MortgageServiceReference.MortgageServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(mortgageServiceClient.InnerChannel))
                    {
                        resultValidator = mortgageServiceClient.GetMortgageIDByMortgageGUID(mortgageGUID);
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
    }
}