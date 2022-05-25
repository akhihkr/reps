using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Script.Serialization;

namespace REPS.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WorkflowService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WorkflowService.svc or WorkflowService.svc.cs at the Solution Explorer and start debugging.
    public class WorkflowService : IWorkflowService
    {
        /// <summary>
        /// Get workflow tasks
        /// </summary>
        /// <param name="workflowID"></param>
        /// <returns></returns>
        public CValidator GetWorkFlowTasks(int workflowID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Workflow.GetWorkflowTasks(workflowID)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Get workflow tasks actions
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="dealID"></param>
        /// <returns></returns>
        public CValidator GetWorkFlowTasksActions(int taskID, int dealID)
        {

            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Workflow.GetWorkflowTasksActions(taskID, dealID)), "Resource.FetchedSuccessfully", true);

            }
            catch (Exception ex)
            {

                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);

            }

        }

        /// <summary>
        /// Get Task Workflow ID
        /// </summary>
        /// <param name="dealID"></param>
        /// <returns></returns>
        public CValidator GetTaskWorkflowID(int dealID)
        {

            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Workflow.GetTaskWorkflowID(dealID)), "Resource.FetchedSuccessfully", true);

            }
            catch (Exception ex)
            {

                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);

            }

        }

        /// <summary>
        /// Get all Workflow Tasks Actions fields for Add View
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="workflowActionID"></param>
        /// <returns></returns>
        public CValidator GetWorkFlowActionsAddFields(int taskID, int workflowActionID)
        {

            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Workflow.GetWorkFlowActionsAddFields(taskID, workflowActionID)), "Resource.FetchedSuccessfully", true);

            }
            catch (Exception ex)
            {

                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);

            }

        }

        /// <summary>
        /// Get Task Workflow Name
        /// </summary>
        /// <param name="dealID"></param>
        /// <returns></returns>
        public CValidator GetTaskWorkflowName(int dealID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Workflow.GetTaskWorkflowName(dealID)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Get User ID by AspNet ID
        /// </summary>
        /// <param name="aspNetID"></param>
        /// <returns></returns>
        public CValidator GetWorkflowIDTaskID(string taskGUID)
        {

            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Workflow.GetWorkflowIDTaskID(taskGUID)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }

        }

        /// <summary>
        /// Get Workflows List
        /// </summary>
        /// <param name="workflowID"></param>
        /// <returns></returns>
        public CValidator GetWorkflowsList(int workflowID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Workflow.GetWorkflowsList(workflowID)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Save Actions fields values for Add View
        /// </summary>
        /// <param name="workflowID"></param>
        /// <returns></returns>
        public CValidator InsertWorkflowActionValues(Dictionary<string, dynamic> formObjects, Dictionary<string, dynamic> FileObjects, int dealID, int userID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Workflow.InsertWorkflowActionValues(formObjects, FileObjects, dealID, userID)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }

        }

        /// <summary>
        /// Get all Workflow Tasks Actions fields values for Edit View
        /// </summary>
        /// <param name="workflowID"></param>
        /// <returns></returns>
        public CValidator GetWorkFlowActionsEditFields(int dealID, int taskID, int workflowActionID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Workflow.GetWorkFlowActionsEditFields(dealID, taskID, workflowActionID)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Save Actions fields values for Edit View
        /// </summary>
        /// <param name="workflowID"></param>
        /// <returns></returns>
        public CValidator UpdateWorkflowActionValues(Dictionary<string, dynamic> formObjects, Dictionary<string, dynamic> FileObjects, int dealID, int userID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Workflow.SaveWorkflowActionValues(formObjects, FileObjects, dealID, userID)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        #region Admin Workflow WCF 

        /// <summary>
        /// Get workflow tasks actions
        /// </summary>
        /// <param name="workflowID"></param>
        /// <returns></returns>
        public CValidator GetWorkFlowTasksActionsList(int taskID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Workflow.GetWorkflowTasksActionsList(taskID)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }
        #endregion end of admin workflow WCF

        /// <summary>
        /// Save Workflow name
        /// </summary>
        /// <param name="formObjects"></param>
        /// <returns></returns>
        public CValidator SaveWorkflowChanges(Dictionary<string, object> formObjects)
        {

            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Workflow.SaveWorkflowChanges(formObjects)), "Resource.FetchedSuccessfully", true);

            }
            catch (Exception ex)
            {

                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);

            }

        }

        /// <summary>
        /// Add Workflow
        /// </summary>
        /// <param name="formObjects"></param>
        /// <param name="workflowID"></param>
        /// <returns></returns>
        public CValidator AddWorkflow(Dictionary<string, object> formObjects, int workflowID)
        {

            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Workflow.AddWorkflow(formObjects, workflowID)), "Resource.FetchedSuccessfully", true);

            }
            catch (Exception ex)
            {

                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);

            }

        }

        /// <summary>
        /// Get all Tasks
        /// </summary>
        /// <param name="processID"></param>
        /// <param name="workflowID"></param>
        /// <returns></returns>
        public CValidator GetAllTasks(int processID, int workflowID)
        {

            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Workflow.GetAllTasks(processID, workflowID)), "Resource.FetchedSuccessfully", true);

            }
            catch (Exception ex)
            {

                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }
        }


        /// <summary>
        /// Get Workflow Variable List
        /// </summary>
        /// <param name="actionID"></param>
        /// <returns></returns>
        public CValidator GetWorkflowVariableList(int actionID, int workflowTaskID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Workflow.GetWorkflowVariableList(actionID, workflowTaskID)), "Resource.FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Get Workflow Action IsMandatory value
        /// </summary>
        /// <param name="actionID"></param>
        /// <param name="workflowTaskID"></param>
        /// <returns></returns>
        public CValidator GetWorkflowActionIsMandatory(int actionID, int workflowTaskID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Workflow.GetWorkflowActionIsMandatory(actionID, workflowTaskID)), "Resource.FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Assign/Unassign Workflow Variable to Action
        /// </summary>
        /// <param name="actionID"></param>
        /// <param name="workflowVariableID"></param>
        /// <param name="toggle"></param>
        /// <returns></returns>
        public CValidator ToggleWorkflowVariable(int actionID, int workflowVariableID, bool toggle)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Workflow.ToggleWorkflowVariable(actionID, workflowVariableID, toggle)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Toggle Workflow Variable Required
        /// </summary>
        /// <param name="actionID"></param>
        /// <param name="workflowVariableID"></param>
        /// <param name="toggle"></param>
        /// <returns></returns>
        public CValidator ToggleWorkflowVariableRequired(int actionID, int workflowVariableID, bool toggle)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Workflow.ToggleWorkflowVariableRequired(actionID, workflowVariableID, toggle)), "Resource.FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Toggle Action Mandatory
        /// </summary>
        /// <param name="actionID"></param>
        /// <param name="toggle"></param>
        /// <returns></returns>
        public CValidator ToggleActionMandatory(int actionID, bool toggle)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Workflow.ToggleActionMandatory(actionID, toggle)), "Resource.FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Save Action changes
        /// </summary>
        /// <param name="formObjects"></param>
        /// <returns></returns>
        public CValidator SaveActionChanges(Dictionary<string, object> formObjects)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Workflow.SaveActionChanges(formObjects)), "Resource.FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Add New Action
        /// </summary>
        /// <param name="formObjects"></param>
        /// <param name="workflowTaskID"></param>
        /// <returns></returns>
        public CValidator AddNewAction(Dictionary<string, object> formObjects, int workflowTaskID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Workflow.AddNewAction(formObjects, workflowTaskID)), "Resource.FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Delete Workflow
        /// </summary>
        /// <param name="workflowID"></param>
        /// <returns></returns>
        public CValidator DeleteWorkflow(int workflowID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Workflow.DeleteWorkflow(workflowID)), "Resource.FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Delete Workflow
        /// </summary>
        /// <param name="workflowID"></param>
        /// <returns></returns>
        public CValidator SwitchUserWorkflowAfterDelete(int workflowToBeDeleted, int workflowIDToBeReplacedWith)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Workflow.SwitchUserWorkflowAfterDelete(workflowToBeDeleted, workflowIDToBeReplacedWith)), "Resource.FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Delete Task
        /// </summary>
        /// <param name="taskWorkflowID"></param>
        /// <returns></returns>
        public CValidator DeleteTask(int taskWorkflowID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Workflow.DeleteTask(taskWorkflowID)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Assign Task To Workflow
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="workflowID"></param>
        /// <returns></returns>
        public CValidator AssignTaskToWorkflow(int taskID, int workflowID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Workflow.AssignTaskToWorkflow(taskID, workflowID)), "Resource.FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// UnAssign Task From Workflow
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="workflowID"></param>
        /// <returns></returns>
        public CValidator UnAssignTaskFromWorkflow(int taskID, int workflowID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Workflow.UnAssignTaskFromWorkflow(taskID, workflowID)), "Resource.FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Assign Action To Task
        /// </summary>
        /// <param name="workflowActionID"></param>
        /// <param name="workflowTaskID"></param>
        /// <returns></returns>
        public CValidator AssignActionToTask(int workflowActionID, int workflowTaskID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Workflow.AssignActionToTask(workflowActionID, workflowTaskID)), "Resource.FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);

            }
        }

        /// <summary>
        /// UnAssign Action From Task
        /// </summary>
        /// <param name="workflowActionID"></param>
        /// <param name="workflowTaskID"></param>
        /// <returns></returns>
        public CValidator UnAssignActionFromTask(int workflowActionID, int workflowTaskID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Workflow.UnAssignActionFromTask(workflowActionID, workflowTaskID)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Add New Task
        /// </summary>
        /// <param name="formObjects"></param>
        /// <returns></returns>
        public CValidator AddNewTask(Dictionary<string, object> formObjects)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Workflow.AddNewTask(formObjects)), "Resource.FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Update Task
        /// </summary>
        /// <param name="formObjects"></param>
        /// <returns></returns>
        public CValidator UpdateTask(Dictionary<string, object> formObjects)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Workflow.UpdateTask(formObjects)), "Resource.FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }
        }

        /// <summary>
        ///  call business to get sum of lastest deal workflow
        /// </summary>
        /// <param name="DealID"></param>
        /// <param name="VariableTypeID"></param>
        /// <param name="WorkflowTaskID"></param>
        /// <param name="userID"></param>
        /// <param name="EntityID"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public CValidator GetWorkflowDealCount(int? UserID = null, int? workflowID = null, int? dealProcessTaskID = null, int? entityID = null)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Workflow.GetWorkflowDealCount(UserID, workflowID, dealProcessTaskID, entityID)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Get TransactionDetailID ID by transactionDetailGUID
        /// </summary>
        /// <param name="participantGUID"></param>
        /// <returns></returns>
        public CValidator GetTransactionDetailIDByTransactionDetailGUID(System.Guid transactionDetailGUID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Workflow.GetTransactionDetailIDByTransactionDetailGUID(transactionDetailGUID)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Delete transaction img n set delete to 1
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public CValidator RemoveTransactionImg(DATA.Entity.TransactionDetail obj)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Workflow.RemoveTransactionImg(obj)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// get current workflow details
        /// </summary>
        /// <param name="workflowID"></param>
        /// <param name="workflowTaskID"></param>
        /// <returns></returns>
        public CValidator GetCurrentWorkflowsDetails(int taskID, int taskWorkflowID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Workflow.GetCurrentWorkflowsDetails(taskID, taskWorkflowID)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Add workflowtaskid for each document to documentworkflow table
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public CValidator AddWorkflowStep(DATA.Entity.DocumentWorkflow obj)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Workflow.AddWorkflowStep(obj)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Get Templates of Workflow Step 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public CValidator GetTemplatePerWorkflowStep(int? documentVersionID = null, int? workflowStepID = null)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Workflow.GetTemplatePerWorkflowStep(documentVersionID, workflowStepID)), "FetchedSuccessfully", true);
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