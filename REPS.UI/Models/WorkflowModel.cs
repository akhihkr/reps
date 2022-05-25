using REPS.DATA.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Script.Serialization;

namespace REPS.UI.Models
{
    public class WorkflowModel
    {
        /// <summary>
        /// get workflow id
        /// </summary>
        /// <param name="dealID"></param>
        /// <returns></returns>
        public static object GetWorkflow(int dealID)
        {
            try
            {
                #region variables
                /// Initiate Validator
                Common.CValidator resultValidator = null;
                Common.CValidator Workflowresult = null;
                #endregion end variables
                /// Call WCF to get workflow tasks depending on workflow ID
                #region logic
                using (WorkflowServiceReference.WorkflowServiceClient workflowClient = new WorkflowServiceReference.WorkflowServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(workflowClient.InnerChannel))
                    {
                        // Get Workflow ID for Deal
                        Workflowresult = workflowClient.GetTaskWorkflowID(dealID);
                        // end of Get Workflow ID for Deal

                        ///get Workflow tasks list depending on workflow ID
                        int workflowid = new JavaScriptSerializer().Deserialize<dynamic>(Workflowresult.output)[0];
                        resultValidator = workflowClient.GetWorkFlowTasks(workflowid);
                        var outputServalCall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);

                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            return outputServalCall;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                        ///end of get Workflow tasks actions list depending on workflow ID
                        ///
                   }
                }
                #endregion logic
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get work flow by work flow id
        /// </summary>
        /// <param name="taskWorkflowId">column in Task to show parent Workflow being selected</param>
        /// <returns></returns>
        public static object GetWorkflowTaskByWorkflowID(int taskWorkflowId)
        {
            try
            {
                #region variables
                /// Initiate Validator
                Common.CValidator resultValidator = null;
                object workflowTasksList = null;
                #endregion end variables

                /// Call WCF to get workflow tasks depending on workflow ID
                #region logic : Get work flow by work flow id
                using (WorkflowServiceReference.WorkflowServiceClient workflowClient = new WorkflowServiceReference.WorkflowServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(workflowClient.InnerChannel))
                    {
                        ///get Workflow tasks list depending on workflow ID
                        resultValidator = workflowClient.GetWorkFlowTasks(taskWorkflowId);
                        workflowTasksList = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            return workflowTasksList;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                        ///get Workflow tasks actions list depending on workflow ID
                    }
                }
                #endregion logic end of logic : Get work flow by work flow id
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Call WCF to get workflow tasks actions depending on task ID
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="DealID"></param>
        /// <returns></returns>
        public static object GetWorkFlowTasksActions(int taskID, int dealID)
        {
            try
            {
                #region variables
                /// Initiate Validator
                Common.CValidator resultValidator = null;
                #endregion end variables
                /// Call WCF to get workflow tasks actions depending on task ID
                #region Call WCF to get workflow tasks actions depending on task ID
                using (WorkflowServiceReference.WorkflowServiceClient workflowClient = new WorkflowServiceReference.WorkflowServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(workflowClient.InnerChannel))
                    {
                        ///get Workflow tasks actions list depending on task ID
                        resultValidator = workflowClient.GetWorkFlowTasksActions(taskID, dealID);
                        var result = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            return result;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end Call WCF to get workflow tasks actions depending on task ID
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Call WCF to Get all Workflow Tasks Actions fields for Add View
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="workflowActionID"></param>
        /// <returns></returns>
        public static object GetWorkFlowActionsAddFields(int taskID, int workflowActionID)
        {
            try
            {
                #region variables
                /// Initiate Validator
                Common.CValidator resultValidator = null;
                #endregion end variables
                ///Call WCF to Get all Workflow Tasks Actions fields for Add View
                #region Call WCF to Get all Workflow Tasks Actions fields for Add View
                using (WorkflowServiceReference.WorkflowServiceClient workflowClient = new WorkflowServiceReference.WorkflowServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(workflowClient.InnerChannel))
                    {
                        resultValidator = workflowClient.GetWorkFlowActionsAddFields(taskID, workflowActionID);
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
                #endregion end Call WCF to Get all Workflow Tasks Actions fields for Add View
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get Task name by DealID
        /// </summary>
        /// <param name="DealID"></param>
        /// <returns></returns>
        public static object GetWorkFlowTaskNameByDealID(int DealID)
        {
            try
            {
                #region variables
                /// Initiate Validator
                Common.CValidator resultValidator = null;
                #endregion end variables
                /// Call WCF to get workflow task name
                #region Call WCF to get workflow task name
                using (WorkflowServiceReference.WorkflowServiceClient workflowClient = new WorkflowServiceReference.WorkflowServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(workflowClient.InnerChannel))
                    {
                        ///get Workflow task name
                        resultValidator = workflowClient.GetTaskWorkflowName(DealID);
                        var result = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            return result;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end Call WCF to get workflow task name
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// get worflow id and task id by taskGUID
        /// </summary>
        /// <param name="taskGUID"></param>
        /// <returns></returns>
        public static object GetWorkflowIDTaskID(string taskGUID)
        {
            try
            {
                #region Variables
                Common.CValidator resultValidator = null;
                #endregion end vaiables

                /// Call WCF to get user information
                using (WorkflowServiceReference.WorkflowServiceClient workflowClient = new WorkflowServiceReference.WorkflowServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(workflowClient.InnerChannel))
                    {
                        resultValidator = workflowClient.GetWorkflowIDTaskID(taskGUID);
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
        /// Get Workflows list
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public static object GetWorkflowsList(int workflowId)
        {
            try
            {
                #region variables
                /// Initiate Validator
                Common.CValidator resultValidator = null;
                #endregion end variables
                /// Call WCF to get workflow list
                #region logic : Get Workflows list
                using (WorkflowServiceReference.WorkflowServiceClient workflowClient = new WorkflowServiceReference.WorkflowServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(workflowClient.InnerChannel))
                    {
                        ///get Workflow list depending on workflow ID
                        resultValidator = workflowClient.GetWorkflowsList(workflowId);
                        var outputServalCall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);

                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {

                            return outputServalCall;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                        ///get Workflow tasks actions list depending on workflow ID
                    }
                }
                #endregion End of logic : Get Workflows list
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Call WCF to get workflow tasks actions depending on task ID
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="DealID"></param>
        /// <returns></returns>
        public static object GetWorkFlowTasksActionsList(int taskID)
        {
            try
            {
                #region variables
                /// Initiate Validator
                Common.CValidator resultValidator = null;
                #endregion end variables
                /// Call WCF to get workflow tasks actions depending on task ID
                #region Call WCF to get workflow tasks actions depending on task ID
                using (WorkflowServiceReference.WorkflowServiceClient workflowClient = new WorkflowServiceReference.WorkflowServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(workflowClient.InnerChannel))
                    {
                        ///get Workflow tasks actions list depending on task ID
                        resultValidator = workflowClient.GetWorkFlowTasksActionsList(taskID);
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
                #endregion end Call WCF to get workflow tasks actions depending on task ID
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// add new data - saved
        /// </summary>
        /// <param name="FormObjects"></param>
        /// <param name="DealID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static object InsertWorkflowActionValues(Dictionary<string, dynamic> FormObjects, Dictionary<string, dynamic> FileObjects, int DealID, int UserID)
        {
            try
            {
                #region variables
                /// Initiate Validator
                Common.CValidator resultValidator = null;
                #endregion end variables
                /// Call WCF to Get all Workflow Tasks Actions fields for saving View
                #region Call WCF to Get all Workflow Tasks Actions fields for saving View
                using (WorkflowServiceReference.WorkflowServiceClient workflowClient = new WorkflowServiceReference.WorkflowServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(workflowClient.InnerChannel))
                    {
                        resultValidator = workflowClient.InsertWorkflowActionValues(FormObjects, FileObjects, DealID, UserID);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null && !Common.CString.CheckNullOrEmpty(resultValidator.output))
                        {
                            return resultValidator.output;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end Call WCF to Get all Workflow Tasks Actions fields for saving View
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Call WCF to Get all Workflow Tasks Actions fields for edit View
        /// </summary>
        /// <param name="DealID"></param>
        /// <param name="TaskID"></param>
        /// <param name="workflowActionID"></param>
        /// <returns></returns>
        public static object GetWorkFlowActionsEditFields(int DealID, int TaskID, int workflowActionID)
        {
            try
            {
                #region variables
                /// Initiate Validator
                Common.CValidator resultValidator = null;
                #endregion end variables
                /// Call WCF to Get all Workflow Tasks Actions fields for edit View
                #region Call WCF to Get all Workflow Tasks Actions fields for edit View
                using (WorkflowServiceReference.WorkflowServiceClient workflowClient = new WorkflowServiceReference.WorkflowServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(workflowClient.InnerChannel))
                    {
                        resultValidator = workflowClient.GetWorkFlowActionsEditFields(DealID, TaskID, workflowActionID);
                        var resultValidatorOutput = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            return resultValidatorOutput;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end Call WCF to Get all Workflow Tasks Actions fields for edit View
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// saving edit workflow
        /// </summary>
        /// <param name="FormObjects"></param>
        /// <param name="DealID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static object SaveWorkflowActionValues(Dictionary<string, object> FormObjects, Dictionary<string, dynamic> FileObjects, int DealID, int UserID)
        {
            try
            {
                #region variables
                /// Initiate Validator
                Common.CValidator resultValidator = null;
                #endregion end variables
                /// Call WCF to Get all Workflow Tasks Actions fields for saving View
                #region Call WCF to Get all Workflow Tasks Actions fields for saving View
                using (WorkflowServiceReference.WorkflowServiceClient workflowClient = new WorkflowServiceReference.WorkflowServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(workflowClient.InnerChannel))
                    {
                        resultValidator = workflowClient.UpdateWorkflowActionValues(FormObjects, FileObjects, DealID, UserID);
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
                #endregion end Call WCF to Get all Workflow Tasks Actions fields for saving View
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Save Workflow changes
        /// </summary>
        /// <param name="formObjects"></param>
        /// <returns></returns>
        public static object SaveWorkflowChanges(Dictionary<string, object> formObjects)
        {
            try
            {
                #region variables
                /// Initiate Validator
                Common.CValidator resultValidator = null;
                #endregion end variables

                /// Call WCF to save workflow
                #region Call WCF to save workflow
                using (WorkflowServiceReference.WorkflowServiceClient workflowClient = new WorkflowServiceReference.WorkflowServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(workflowClient.InnerChannel))
                    {
                        /// save workflow name
                        resultValidator = workflowClient.SaveWorkflowChanges(formObjects);
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
        /// Add New Workflow
        /// </summary>
        /// <param name="formObjects"></param>
        /// <param name="workflowID"></param>
        /// <returns></returns>
        public static object AddNewWorkflow(Dictionary<string, object> formObjects, int workflowID)
        {
            try
            {
                #region variables
                /// Initiate Validator
                Common.CValidator resultValidator = null;
                #endregion end variables

                /// Call WCF to save workflow
                #region Call WCF to save workflow
                using (WorkflowServiceReference.WorkflowServiceClient workflowClient = new WorkflowServiceReference.WorkflowServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(workflowClient.InnerChannel))
                    {
                        /// save workflow name
                        resultValidator = workflowClient.AddWorkflow(formObjects, workflowID);
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
        /// Get Uploaded File
        /// </summary>
        /// <param name="transactionID"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static byte[] GetUploadedFile(int transactionID, string filename)
        {
            return Global.File.GetFile(transactionID, filename, (int)Global.Enums.FolderPath.Actions);
        }

        /// <summary>
        /// get work flow by work flow id
        /// </summary>
        /// <param name="WorkflowTasksList"></param>
        /// <param name="taskWorkflowId">column in Task to show parent Workflow being selected</param>
        /// <returns></returns>
        public static object GetWorkflowTaskByWorkflowID(object WorkflowTasksList, int taskWorkflowId)
        {
            try
            {
                #region variables
                /// Initiate Validator
                Common.CValidator resultValidator = null;
                #endregion end variables
                /// Call WCF to get workflow tasks depending on workflow ID
                #region logic : get work flow by work flow id
                using (WorkflowServiceReference.WorkflowServiceClient workflowClient = new WorkflowServiceReference.WorkflowServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(workflowClient.InnerChannel))
                    {
                        ///get Workflow tasks list depending on workflow ID
                        resultValidator = workflowClient.GetWorkFlowTasks(taskWorkflowId);
                        WorkflowTasksList = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {

                            return WorkflowTasksList;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                        ///get Workflow tasks actions list depending on workflow ID
                    }
                }
                #endregion end of logic : get work flow by work flow id
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get all Tasks
        /// </summary>
        /// <param name="processID"></param>
        /// <param name="workflowID"></param>
        /// <returns></returns>
        public static object GetAllTasks(int processID, int workflowID)
        {
            try
            {
                #region variables
                /// Initiate Validator
                Common.CValidator resultValidator = null;
                #endregion end variables

                /// Call WCF to get workflow tasks 
                #region Call WCF to get workflow tasks
                using (WorkflowServiceReference.WorkflowServiceClient workflowClient = new WorkflowServiceReference.WorkflowServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(workflowClient.InnerChannel))
                    {
                        ///get Workflow tasks
                        resultValidator = workflowClient.GetAllTasks(processID, workflowID);
                        var result = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {

                            return result;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end Call WCF to get workflow tasks
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Get Workflow Variable List
        /// </summary>
        /// <param name="actionID"></param>
        /// <returns></returns>
        public static object GetWorkflowVariableList(int actionID, int workflowTaskID)
        {
            try
            {
                #region variables
                /// Initiate Validator
                Common.CValidator resultValidator = null;
                #endregion end variables

                /// Call WCF to Get Workflow Variable List
                #region Call WCF to Get Workflow Variable List
                using (WorkflowServiceReference.WorkflowServiceClient workflowClient = new WorkflowServiceReference.WorkflowServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(workflowClient.InnerChannel))
                    {
                        /// Get Workflow Variable List
                        resultValidator = workflowClient.GetWorkflowVariableList(actionID, workflowTaskID);
                        var result = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {

                            return result;
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
        /// <summary>
        /// Get Workflow Action IsMandatory value
        /// </summary>
        /// <param name="actionID"></param>
        /// <param name="workflowTaskID"></param>
        /// <returns></returns>
        public static object GetWorkflowActionIsMandatory(int actionID, int workflowTaskID)
        {
            try
            {
                #region variables
                /// Initiate Validator
                Common.CValidator resultValidator = null;
                #endregion end variables

                /// Call WCF to Get Workflow Variable List
                #region Call WCF to Get Workflow Variable List
                using (WorkflowServiceReference.WorkflowServiceClient workflowClient = new WorkflowServiceReference.WorkflowServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(workflowClient.InnerChannel))
                    {
                        /// Get Workflow Variable List
                        resultValidator = workflowClient.GetWorkflowActionIsMandatory(actionID, workflowTaskID);
                        var result = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {

                            return result;
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
        /// Assign/Unassign Workflow Variable to Action
        /// </summary>
        /// <param name="actionID"></param>
        /// <param name="workflowVariableID"></param>
        /// <param name="paramToggle"></param>
        /// <returns></returns>
        public static object ToggleAction(int actionID, int workflowVariableID, bool paramToggle)
        {
            try
            {
                #region variables
                /// Initiate Validator
                Common.CValidator resultValidator = null;
                #endregion end variables

                /// Call WCF to  Assign/Unassign Workflow Variable to Action
                #region Call WCF to  Assign/Unassign Workflow Variable to Action
                using (WorkflowServiceReference.WorkflowServiceClient workflowClient = new WorkflowServiceReference.WorkflowServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(workflowClient.InnerChannel))
                    {
                        ///  Assign/Unassign Workflow Variable to Action
                        resultValidator = workflowClient.ToggleWorkflowVariable(actionID, workflowVariableID, paramToggle);
                        var result = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {

                            return result;
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
        /// Update Workflow Variable mapping to set/unset IsRequired
        /// </summary>
        /// <param name="actionID"></param>
        /// <param name="workflowVariableID"></param>
        /// <param name="paramToggle"></param>
        /// <returns></returns>
        public static object ToggleActionRequired(int actionID, int workflowVariableID, bool paramToggle)
        {
            try
            {
                #region variables
                /// Initiate Validator
                Common.CValidator resultValidator = null;
                #endregion end variables

                /// Call WCF to  Assign/Unassign Workflow Variable to Action
                #region Call WCF to  Assign/Unassign Workflow Variable to Action
                using (WorkflowServiceReference.WorkflowServiceClient workflowClient = new WorkflowServiceReference.WorkflowServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(workflowClient.InnerChannel))
                    {
                        ///  Assign/Unassign Workflow Variable to Action
                        resultValidator = workflowClient.ToggleWorkflowVariableRequired(actionID, workflowVariableID, paramToggle);
                        var result = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {

                            return result;
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
        /// Toggle Action Mandatory
        /// </summary>
        /// <param name="actionID"></param>
        /// <param name="paramToggle"></param>
        /// <returns></returns>
        public static object ToggleActionMandatory(int actionID, bool paramToggle)
        {
            try
            {
                #region variables
                /// Initiate Validator
                Common.CValidator resultValidator = null;
                #endregion end variables

                /// Call WCF to  Assign/Unassign Workflow Variable to Action
                #region Call WCF to  Assign/Unassign Workflow Variable to Action
                using (WorkflowServiceReference.WorkflowServiceClient workflowClient = new WorkflowServiceReference.WorkflowServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(workflowClient.InnerChannel))
                    {
                        ///  Assign/Unassign Workflow Variable to Action
                        resultValidator = workflowClient.ToggleActionMandatory(actionID, paramToggle);
                        var result = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {

                            return result;
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
        /// Save Action changes
        /// </summary>
        /// <param name="formObjects"></param>
        /// <returns></returns>
        public static object SaveActionChanges(Dictionary<string, object> formObjects)
        {
            try
            {
                #region variables
                /// Initiate Validator
                Common.CValidator resultValidator = null;
                #endregion end variables

                /// Call WCF to save action
                #region Call WCF to save action
                using (WorkflowServiceReference.WorkflowServiceClient workflowClient = new WorkflowServiceReference.WorkflowServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(workflowClient.InnerChannel))
                    {
                        /// save action name
                        resultValidator = workflowClient.SaveActionChanges(formObjects);
                        var result = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {

                            return result;
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
        /// Save New Action
        /// </summary>
        /// <param name="formObjects"></param>
        /// <param name="workflowTaskID"></param>
        /// <returns></returns>
        public static object AddNewAction(Dictionary<string, object> formObjects, int workflowTaskID)
        {
            try
            {
                #region variables
                /// Initiate Validator
                Common.CValidator resultValidator = null;
                #endregion end variables

                /// Call WCF to save action
                #region Call WCF to save action
                using (WorkflowServiceReference.WorkflowServiceClient workflowClient = new WorkflowServiceReference.WorkflowServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(workflowClient.InnerChannel))
                    {
                        /// save action
                        resultValidator = workflowClient.AddNewAction(formObjects, workflowTaskID);
                        var result = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null && !Common.CString.CheckNullOrEmpty(resultValidator.output))
                        {

                            return result;
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
        /// Delete Workflow
        /// </summary>
        /// <param name="workflowID"></param>
        /// <returns></returns>
        public static object DeleteWorkflow(int workflowID)
        {
            try
            {
                #region variables
                /// Initiate Validator
                Common.CValidator resultValidator = null;
                #endregion end variables

                /// Call WCF to save workflow
                #region Call WCF to save workflow
                using (WorkflowServiceReference.WorkflowServiceClient workflowClient = new WorkflowServiceReference.WorkflowServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(workflowClient.InnerChannel))
                    {
                        /// save workflow name
                        resultValidator = workflowClient.DeleteWorkflow(workflowID);
                        var result = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {

                            return result;
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
        /// Switch user Workflow after WF has been deleted
        /// </summary>
        /// <param name="workflowID"></param>
        /// <returns></returns>
        public static object SwitchUserWorkflowAfterDelete(int workflowToBeDeleted, int workflowIDToBeReplacedWith)
        {
            try
            {
                #region variables
                /// Initiate Validator
                Common.CValidator resultValidator = null;
                #endregion end variables

                /// Call WCF to save workflow
                #region Call WCF to save workflow
                using (WorkflowServiceReference.WorkflowServiceClient workflowClient = new WorkflowServiceReference.WorkflowServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(workflowClient.InnerChannel))
                    {
                        /// save workflow name
                        resultValidator = workflowClient.SwitchUserWorkflowAfterDelete(workflowToBeDeleted, workflowIDToBeReplacedWith);
                        var result = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {

                            return result;
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
        /// Remove Task
        /// </summary>
        /// <param name="taskWorkflowID"></param>
        /// <returns></returns>
        public static object DeleteTask(int taskWorkflowID)
        {
            try
            {
                #region variables
                /// Initiate Validator
                Common.CValidator resultValidator = null;
                #endregion end variables

                /// Call WCF to remove task
                #region Call WCF to save workflow
                using (WorkflowServiceReference.WorkflowServiceClient workflowClient = new WorkflowServiceReference.WorkflowServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(workflowClient.InnerChannel))
                    {
                        /// remove task
                        resultValidator = workflowClient.DeleteTask(taskWorkflowID);
                        var result = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {

                            return result;
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
        /// Assign Task To Workflow
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="workflowID"></param>
        /// <returns></returns>
        public static object AssignTaskToWorkflow(int taskID, int workflowID)
        {
            try
            {
                #region variables
                /// Initiate Validator
                Common.CValidator resultValidator = null;
                #endregion end variables

                /// Call WCF to Assign Task To Workflow 
                #region Call WCF to Assign Task To Workflow
                using (WorkflowServiceReference.WorkflowServiceClient workflowClient = new WorkflowServiceReference.WorkflowServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(workflowClient.InnerChannel))
                    {
                        ///Assign Task To Workflow
                        resultValidator = workflowClient.AssignTaskToWorkflow(taskID, workflowID);
                        var result = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {

                            return result;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end Call WCF to Assign Task To Workflow
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// UnAssign Task From Workflow
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="workflowID"></param>
        /// <returns></returns>
        public static object UnAssignTaskFromWorkflow(int taskID, int workflowID)
        {
            try
            {
                #region variables
                /// Initiate Validator
                Common.CValidator resultValidator = null;
                #endregion end variables

                /// Call WCF to UnAssign Task From Workflow 
                #region Call WCF to UnAssign Task From Workflow
                using (WorkflowServiceReference.WorkflowServiceClient workflowClient = new WorkflowServiceReference.WorkflowServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(workflowClient.InnerChannel))
                    {
                        ///UnAssign Task From Workflow
                        resultValidator = workflowClient.UnAssignTaskFromWorkflow(taskID, workflowID);
                        var result = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {

                            return result;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end Call WCF to UnAssign Task From Workflow
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Assign Action To Task
        /// </summary>
        /// <param name="workflowActionID"></param>
        /// <param name="workflowTaskID"></param>
        /// <returns></returns>
        public static object AssignActionToTask(int workflowActionID, int workflowTaskID)
        {
            try
            {
                #region variables
                /// Initiate Validator
                Common.CValidator resultValidator = null;
                #endregion end variables

                /// Call WCF to Assign Action To Task
                #region Call WCF to Assign Action To Task
                using (WorkflowServiceReference.WorkflowServiceClient workflowClient = new WorkflowServiceReference.WorkflowServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(workflowClient.InnerChannel))
                    {
                        ///Assign Action To Task
                        resultValidator = workflowClient.AssignActionToTask(workflowActionID, workflowTaskID);
                        var result = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {

                            return result;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end Call WCF to Assign Action To Task
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// UnAssign Action From Task
        /// </summary>
        /// <param name="workflowActionID"></param>
        /// <param name="workflowTaskID"></param>
        /// <returns></returns>
        public static object UnAssignActionFromTask(int workflowActionID, int workflowTaskID)
        {
            try
            {
                #region variables
                /// Initiate Validator
                Common.CValidator resultValidator = null;
                #endregion end variables

                /// Call WCF to UnAssign Action From Task
                #region Call WCF to UnAssign Action From Task
                using (WorkflowServiceReference.WorkflowServiceClient workflowClient = new WorkflowServiceReference.WorkflowServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(workflowClient.InnerChannel))
                    {
                        ///UnAssign Action From Task
                        resultValidator = workflowClient.UnAssignActionFromTask(workflowActionID, workflowTaskID);
                        var result = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {

                            return result;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end Call WCF to UnAssign Action From Task
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Update Task
        /// </summary>
        /// <param name="formObjects"></param>
        /// <returns></returns>
        public static object UpdateTask(Dictionary<string, object> formObjects)
        {
            try
            {
                #region variables
                /// Initiate Validator
                Common.CValidator resultValidator = null;
                #endregion end variables

                /// Call WCF to save task
                #region Call WCF to save task
                using (WorkflowServiceReference.WorkflowServiceClient workflowClient = new WorkflowServiceReference.WorkflowServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(workflowClient.InnerChannel))
                    {
                        /// save task
                        resultValidator = workflowClient.UpdateTask(formObjects);
                        var result = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {

                            return result;
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
        /// Add New Task
        /// </summary>
        /// <param name="formObjects"></param>
        /// <returns></returns>
        public static object AddNewTask(Dictionary<string, object> formObjects)
        {
            try
            {
                #region variables
                /// Initiate Validator
                Common.CValidator resultValidator = null;
                #endregion end variables

                /// Call WCF to add task
                #region Call WCF to add task
                using (WorkflowServiceReference.WorkflowServiceClient workflowClient = new WorkflowServiceReference.WorkflowServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(workflowClient.InnerChannel))
                    {
                        /// Add task
                        resultValidator = workflowClient.AddNewTask(formObjects);
                        var result = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {

                            return result;
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
        /// get deal sum by task
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public static object[] GetWorkflowDealCount(int? userID = null, int? workflowID = null, int? dealProcessTaskID = null, int? entityID = null)
        {
            try
            {
                #region Variables
                Common.CValidator resultValidator = null;
                #endregion end vaiables
                /// Call WCF to get sum of deal 
                #region get deal sum by task
                using (WorkflowServiceReference.WorkflowServiceClient workflowClient = new WorkflowServiceReference.WorkflowServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(workflowClient.InnerChannel))
                    {
                        resultValidator = workflowClient.GetWorkflowDealCount(userID, workflowID, dealProcessTaskID, entityID);
                        var output = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        return output;
                    }
                }
                #endregion end get deal sum by task
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Get TransactionDetailID ID by transactionDetailGUID
        /// </summary>
        /// <param name="aspNetId"></param>
        /// <returns></returns>
        public static int GetTransactionDetailIDByTransactionDetailGUID(System.Guid transactionDetailGUID)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables 

                #region logic Get TransactionDetailID ID by transactionDetailGUID
                using (WorkflowServiceReference.WorkflowServiceClient workflowClient = new WorkflowServiceReference.WorkflowServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(workflowClient.InnerChannel))
                    {
                        resultValidator = workflowClient.GetTransactionDetailIDByTransactionDetailGUID(transactionDetailGUID);
                        var transactionDetailID = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            return Convert.ToInt32(transactionDetailID[0]);
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end logic Get TransactionDetailID ID by transactionDetailGUID
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// remove TransactionDetailID set delete to 1
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="participantInput"></param>
        /// <returns></returns>
        public static string RemoveTransactionImg(TransactionDetail obj)
        {
            try
            {
                #region Variables
                Common.CValidator resultValidator = null;
                #endregion end vaiables
                ///remove participant set delete to 1
                #region remove TransactionDetailID set delete to 1
                using (WorkflowServiceReference.WorkflowServiceClient workflowClient = new WorkflowServiceReference.WorkflowServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(workflowClient.InnerChannel))
                    {
                        resultValidator = workflowClient.RemoveTransactionImg(obj);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null && !Common.CString.CheckNullOrEmpty(resultValidator.output))
                        {
                            return resultValidator.ToString();
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion remove participTransactionDetailIDant set delete to 1
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// get current workflow details
        /// </summary>
        /// <param name="workflowID"></param>
        /// <param name="workflowTaskID"></param>
        /// <returns></returns>
        public static object GetCurrentWorkflowsDetails(int taskID, int taskWorkflowID)
        {
            try
            {
                #region variables
                /// Initiate Validator
                Common.CValidator resultValidator = null;
                #endregion end variables
                /// Call WCF to get workflow list
                #region logic : Get Workflows list
                using (WorkflowServiceReference.WorkflowServiceClient workflowClient = new WorkflowServiceReference.WorkflowServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(workflowClient.InnerChannel))
                    {
                        ///get Workflow list depending on workflow ID
                        resultValidator = workflowClient.GetCurrentWorkflowsDetails(taskID, taskWorkflowID);
                        var outputServalCall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);

                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            return outputServalCall;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                        ///get Workflow tasks actions list depending on workflow ID
                    }
                }
                #endregion End of logic : Get Workflows list
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get Document Template Workflowe list list
        /// </summary>
        /// <returns></returns>
        public static object GetTemplatePerWorkflowStep(int? documentVersionID = null, int? workflowStepID = null)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion End of variables

                #region Logic : Get Document Template list          
                using (WorkflowServiceReference.WorkflowServiceClient workflowClient = new WorkflowServiceReference.WorkflowServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(workflowClient.InnerChannel))
                    {
                        //get Document Category list 
                        resultValidator = workflowClient.GetTemplatePerWorkflowStep(documentVersionID == null ? null : documentVersionID, workflowStepID == null ? null : workflowStepID);
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
                #endregion end of Logic : Get Document Template list
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }
    }
}