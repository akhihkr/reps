using REPS.DATA.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web;
using System.Web.Script.Serialization;

namespace REPS.UI.Models
{
    public class BankModel
    {
        /// <summary>
        /// Get Account types
        /// </summary>
        /// <param name="accountTypeId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static object GetAccountType(int? accountTypeId, int? startRow, int? endRow)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables
                /// Call WCF to get all account types
                /// 
                #region WCF for account type
                using (BankServiceReference.BankServiceClient bankServiceClient = new BankServiceReference.BankServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(bankServiceClient.InnerChannel))
                    {
                        resultValidator = bankServiceClient.GetAccountType(accountTypeId == null ? null : accountTypeId, startRow == null ? null : startRow, endRow == null ? null : endRow);
                        var outputServalCall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            ///Created Deal id save into Sessin for reuse
                            return outputServalCall;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end for WCF account type
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get Bank Types
        /// </summary>
        /// <param name="accountTypeId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static object GetBanks(string bankName, string swiftCode, int? entityId, int? bankId, int? startRow, int? endRow)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables
                /// Call WCF to get all account types
                /// 
                #region WCF for account type
                using (BankServiceReference.BankServiceClient bankServiceClient = new BankServiceReference.BankServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(bankServiceClient.InnerChannel))
                    {
                        resultValidator = bankServiceClient.GetBanks(bankName, swiftCode, entityId, bankId, startRow, endRow);
                        var outputServalCall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            ///Created Deal id save into Sessin for reuse
                            return outputServalCall;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end for WCF account type
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Update Participant Bank Details
        /// </summary>
        /// <param name="formInputParticipantBank"></param>
        /// <returns></returns>
        public static object UpdateParticipantBankDetails(ParticipantBankDetail formInputParticipantBank,int dealID)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables

                #region logic
                /// Call WCF to update Participant Bank Details
                using (ParticipantServiceReference.ParticipantServiceClient participantServiceClient = new ParticipantServiceReference.ParticipantServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(participantServiceClient.InnerChannel))
                    {
                        resultValidator = participantServiceClient.UpdateParticipantBankDetails(formInputParticipantBank, dealID);
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
                #endregion End logic
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// wcf call to get bank details per participant
        /// </summary>
        /// <param name="participantId"></param>
        /// <param name="participantBankDetailID"></param>
        /// <returns></returns>
        public static object GetBankDetailsPerParticipant(int? participantId, int? participantBankDetailID)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables
                /// Call WCF to get all participant bank details
                #region logic : Call WCF to get all participant bank details
                using (BankServiceReference.BankServiceClient bankServiceClient = new BankServiceReference.BankServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(bankServiceClient.InnerChannel))
                    {
                        resultValidator = bankServiceClient.GetBankDetailsPerParticipant(participantId, participantBankDetailID == null ? null : participantBankDetailID);
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
                #endregion logic : Call WCF to get all participant bank details
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }
    }
}