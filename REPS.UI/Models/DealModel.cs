using REPS.DATA.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Script.Serialization;

namespace REPS.UI.Models
{
    public class DealModel
    {
        /// <summary>
        /// Get all deal types
        /// </summary>
        /// <param name="dealTypeId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static object GetDealTypes(int? dealTypeId, int? startRow, int? endRow)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end of variables
                #region get all deal type if role is attorney
                using (DealServiceReference.DealServiceClient dealServiceClient = new DealServiceReference.DealServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(dealServiceClient.InnerChannel))
                    {
                        resultValidator = dealServiceClient.GetDealTypes(dealTypeId == null ? null : dealTypeId, startRow == null ? null : startRow, endRow == null ? null : endRow);
                        var res = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            ///Created Deal id save into Sessin for reuse
                            return res;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end of get all deal type if role is attorney
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        ///  ServerCAll to save deal
        /// </summary>
        /// <param name="formInput"></param>
        /// <returns></returns>
        public static string SaveDeal(Deal formInput, string aspnetUsersId)
        {
            try
            {
                using (DealServiceReference.DealServiceClient client = new DealServiceReference.DealServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
                    {
                        var res = client.AddDeal(formInput, aspnetUsersId);
                        if (res != null && res.success && res.output.ToString() != null && !Common.CString.CheckNullOrEmpty(res.output))/// checknullorempty use only on save not on get
                        {
                            ///Created Deal id save into Sessin for reuse
                            return res.output.ToString();
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
        /// wcf call to get deal 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="taskID"></param>
        /// <param name="entityID"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static object[] GetDeal(Deal obj, int? taskID, int entityID, int? startRow = null, int? endRow = null)
        {
            try
            {
                #region Variables
                Common.CValidator resultValidator = null;
                #endregion end vaiables
                using (DealServiceReference.DealServiceClient client = new DealServiceReference.DealServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
                    {
                        resultValidator = client.GetDeal(obj, taskID == null ? null : taskID, entityID, startRow, endRow);
                        var outputServalCall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            ///Created Deal id save into Sessin for reuse
                            return outputServalCall; //to uncomment latter
                                                     //throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");// to be remove later
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
        /// wcf call get deal id by dealGUID
        /// </summary>
        /// <param name="dealUniqueRef"></param>
        /// <returns></returns>
        public static object GetDealIDByDealUniqueRef(string dealUniqueRef)
        {
            try
            {
                #region Variables
                Common.CValidator resultValidator = null;
                #endregion end vaiables
                using (DealServiceReference.DealServiceClient client = new DealServiceReference.DealServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
                    {
                        resultValidator = client.GetDealIDByDealUniqueRef(dealUniqueRef);
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
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// call wcf to update a deal
        /// </summary>
        /// <param name="formInput"></param>
        /// <returns></returns>
        public static string UpdateDeal(Deal obj)
        {
            try
            {
                #region logic
                using (DealServiceReference.DealServiceClient clientUpdate = new DealServiceReference.DealServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(clientUpdate.InnerChannel))
                    {
                        var res = clientUpdate.UpdateDeal(obj);
                        if (res != null && res.success && res.output.ToString() != null && !Common.CString.CheckNullOrEmpty(res.output))
                        {
                            ///Created Deal id save into Sessin for reuse
                            return res.output.ToString();
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

        /// <summary>
        /// Call WCF to get completion date of the current deal
        /// </summary>
        /// <param name="DealID"></param>
        /// <param name="CompletionDate"></param>
        /// <returns></returns>
        public static string EditCompletionDealDate(int DealID, DateTime CompletionDate)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion
                #region completion date
                using (DealServiceReference.DealServiceClient dealServiceClient = new DealServiceReference.DealServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(dealServiceClient.InnerChannel))
                    {
                        resultValidator = dealServiceClient.EditCompletionDate(DealID, CompletionDate);
                        var res = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            return res.ToString();
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end completion dates
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// return completion date
        /// </summary>
        /// <param name="dealID"></param>
        /// <returns></returns>
        public static object[] GetCompletedDate(int dealID)
        {
            try
            {
                #region variables
                Common.CValidator resultValidatorDate = null;
                #endregion
                #region logic
                using (DealServiceReference.DealServiceClient dealServiceClientDate = new DealServiceReference.DealServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(dealServiceClientDate.InnerChannel))
                    {
                        resultValidatorDate = dealServiceClientDate.GetCompletionDate(dealID);
                        var outputDate = new JavaScriptSerializer().Deserialize<dynamic>(resultValidatorDate.output);
                        if (resultValidatorDate != null && resultValidatorDate.success && resultValidatorDate.output.ToString() != null)
                        {
                            return outputDate;
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
        /// check if we have existing mortgages per deal ***** mortgage Model *****
        /// </summary>
        /// <param name="dealID"></param>
        /// <returns></returns>
        public static object[] GetMortgageSumValuePerDeal(int dealID)
        {
            try
            {
                using (MortgageServiceReference.MortgageServiceClient mortgageServiceClient = new MortgageServiceReference.MortgageServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(mortgageServiceClient.InnerChannel))
                    {
                        #region variables
                        Common.CValidator resultValidator = null;
                        #endregion end of variables
                        #region mortgage sum values per deal call  
                        resultValidator = mortgageServiceClient.GetSumValueMortgages(dealID);
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
                    #endregion end of mortgage sum values per deal call  
                }
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get mandatory Actions list
        /// </summary>
        /// <param name="workflowID"></param>
        /// <returns></returns>
        public static object[] GetMandatoryActionsList(int workflowID)
        {
            try
            {
                #region variables
                Common.CValidator resultValidatorAction = null;
                #endregion end of variables

                #region logic : Get mandatory Actions list
                using (DealServiceReference.DealServiceClient dealServiceAction = new DealServiceReference.DealServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(dealServiceAction.InnerChannel))
                    {
                        resultValidatorAction = dealServiceAction.GetMandatoryActionsList(workflowID);
                        var outputServalCall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidatorAction.output);
                        if (resultValidatorAction != null && resultValidatorAction.success && resultValidatorAction.output.ToString() != null)
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
                #endregion end of logic : Get mandatory Actions list
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get Completed Actions
        /// </summary>
        /// <param name="dealID"></param>
        /// <param name="workflowID"></param>
        /// <returns></returns>
        public static object[] GetCompletedActionsByWorkflowID(int dealID, int workflowID)
        {
            try
            {
                #region variables
                Common.CValidator resultValidatorAction = null;
                #endregion end of variables

                #region logic : Get Completed Actions
                using (DealServiceReference.DealServiceClient dealServiceAction = new DealServiceReference.DealServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(dealServiceAction.InnerChannel))
                    {
                        resultValidatorAction = dealServiceAction.GetCompletedActionsByWorkflowID(dealID, workflowID);
                        var outputServalCall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidatorAction.output);
                        if (resultValidatorAction != null && resultValidatorAction.success && resultValidatorAction.output.ToString() != null)
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
                #endregion end of logic : Get Completed Actions
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// get all deal types by dealID workflow steps
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public static object[] GetDealWorkflowStep(int dealID, int workflowID, int? userID = null, int? entityID = null, int? startDate = null, int? endDate = null)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end of variables

                #region logic get all deal types by dealID workflow steps
                using (DealServiceReference.DealServiceClient dealServiceAction = new DealServiceReference.DealServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(dealServiceAction.InnerChannel))
                    {
                        resultValidator = dealServiceAction.GetCurrentWorkflowStep(dealID, workflowID, null, entityID, null, null);
                        if (resultValidator != null && resultValidator.success == true && resultValidator.output.ToString() != null)
                        {
                            var output = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                            return output;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                        
                    }
                }
                #endregion end logic get all deal types by dealID workflow steps
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get Deal LastAction By
        /// </summary>
        /// <param name="dealID"></param>
        /// <returns></returns>
        public static object[] GetDealLastActionBy(int dealID)
        {
            try
            {
                #region variables
                Common.CValidator resultValidatorAction = null;
                #endregion

                #region logic
                using (DealServiceReference.DealServiceClient dealServiceAction = new DealServiceReference.DealServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(dealServiceAction.InnerChannel))
                    {
                        resultValidatorAction = dealServiceAction.GetDealLastActionBy(dealID);
                        var outputActionby = new JavaScriptSerializer().Deserialize<dynamic>(resultValidatorAction.output);
                        if (resultValidatorAction != null && resultValidatorAction.success && resultValidatorAction.output.ToString() != null)
                        {
                            return outputActionby;
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