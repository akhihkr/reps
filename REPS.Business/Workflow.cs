using Global;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPS.Business
{
    public class Workflow
    {
        /// <summary>
        /// Get workflow tasks related to workflowID
        /// </summary>
        /// <param name="workflowID"></param>
        /// <returns></returns>
        public static object GetWorkflowTasks(int workflowID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end pf variables

                #region logic : Get workflow tasks related to workflowID
                results = REPSDB.REPS_GetWorkFlowTasks(workflowID);
                return results;
                #endregion end of logic : Get workflow tasks related to workflowID
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Get workflow tasks actions related to task ID
        /// </summary>
        /// <param name="workflowName">WorkFlow Name</param>
        /// <returns></returns>
        public static object GetWorkflowTasksActions(int taskID, int dealID)
        {
            try
            {
                #region variables

                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;

                #endregion

                #region logic

                results = REPSDB.REPS_GetWorkFlowTasksActions(taskID, dealID);
                return results;

                #endregion


            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// Get Task Workflow ID
        /// </summary>
        /// <param name="dealID"></param>
        /// <returns></returns>
        public static object GetTaskWorkflowID(int dealID)
        {
            try
            {
                #region variables

                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;

                #endregion

                #region logic

                results = REPSDB.REPS_GetTaskWorkflowIDByDealID(dealID);
                return results;

                #endregion


            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// Get all Workflow Tasks Actions fields for Add View
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="workflowActionID"></param>
        /// <returns></returns>
        public static object GetWorkFlowActionsAddFields(int taskID, int workflowActionID)
        {
            try
            {
                #region variables

                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;

                #endregion

                #region logic

                results = REPSDB.REPS_GetWorkFlowActionsAddFields(taskID, workflowActionID);
                return results;

                #endregion


            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// Get Task Workflow Name
        /// </summary>
        /// <param name="dealID"></param>
        /// <returns></returns>
        public static object GetTaskWorkflowName(int dealID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables

                #region logic : Get Task Workflow Name
                results = REPSDB.REPS_GetTaskWorkflowNameByDealID(dealID).FirstOrDefault();
                return results;
                #endregion end of logic : Get Task Workflow Name 
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// store procedure call to get workflow id and task id value
        /// </summary>
        /// <param name="taskGUID"></param>
        /// <returns></returns>
        public static object GetWorkflowIDTaskID(string taskGUID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end variables

                #region logic
                results = REPSDB.REPS_GetWorkflowGUIDTaskGUID(taskGUID).FirstOrDefault();
                return results;
                #endregion end logic
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Get Workflow list
        /// </summary>
        /// <param name="workflowID"></param>
        /// <returns></returns>
        public static object GetWorkflowsList(int workflowID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables

                #region logic
                results = REPSDB.REPS_ADM_GetWorkflowsList(workflowID);
                return results;
                #endregion end of logic
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Get Workflow Task Actions
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public static object GetWorkflowTasksActionsList(int taskID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion End of variables

                #region logic : Get Workflow Task Actions
                results = REPSDB.REPS_ADM_GetWorkFlowTasksActionsByTaskID(taskID);
                return results;
                #endregion end of logic : Get Workflow Task Actions
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Save Actions fields values for Add View
        /// </summary>
        /// <param name="workflowName">WorkFlow Name</param>
        /// <returns></returns>
        public static object InsertWorkflowActionValues(Dictionary<string, dynamic> formObjects, Dictionary<string, dynamic> FileObjects, int dealID, int userID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                ObjectParameter WFActionRowCount = new ObjectParameter("rowCount", typeof(int));
                Dictionary<string, object> tempnormalfields = new Dictionary<string, object>();
                int WorkflowTaskID = 0;
                int WorkflowActionVarID = 0;
                int VariableTypeID = 0;
                #endregion end of variables

                #region logic : Save Actions fields values for Add View

                // filter and remove any unwanted fields
                foreach (var item in formObjects )
                {
                    if (item.Key.Contains("FieldType"))
                    {
                        tempnormalfields.Add(item.Key, item.Value);
                        var IDArray = item.Key.Split(':');
                        WorkflowTaskID = Convert.ToInt32(IDArray[1]);
                    }
                }

                var TID = REPSDB.REPS_AddWorkflowActionNewTransaction(dealID, (int)Enums.TransactionType.New, 4, WorkflowTaskID, userID, rowCount);
                int transactionID = Convert.ToInt32(rowCount.Value);

                // Insert into DB
                foreach (var item in tempnormalfields)
                {
                    var IDArray = item.Key.Split(':');
                    WorkflowTaskID = Convert.ToInt32(IDArray[1]);
                    WorkflowActionVarID = Convert.ToInt32(IDArray[2]);
                    VariableTypeID = Convert.ToInt32(IDArray[3]);

                    // results = Property.PS_InsertWorkflowActionValues(dealID,userID,WorkflowTaskID, WorkflowActionVarID, VariableTypeID, item.Value.ToString());
                    results = REPSDB.REPS_AddWorkflowActionValues(dealID, userID, transactionID, WorkflowTaskID, WorkflowActionVarID, VariableTypeID, item.Value.ToString(), WFActionRowCount);
                }

                // Save uploaded files
                foreach (var fileobj in FileObjects)
                {
                    string filename = fileobj.Key.ToString();
                    // Get Byte Array
                    //byte[] filecontent = Common.CFile.FromBase64StringToByteArray(fileobj.Value);
                    byte[] filecontent =fileobj.Value;

                    // Build File Path
                    string filepath = File.GetFilePath(transactionID, filename, (int)Enums.FolderPath.Actions);

                    // Create Directory
                    File.CreateNewDirectoryActions(transactionID, (int)Enums.FolderPath.Actions);

                    // Write file to path
                    File.WriteFiletoPath(filepath, filename, filecontent);
                }
                return (WFActionRowCount.Value == DBNull.Value ? null : (int?)WFActionRowCount.Value);

                #endregion end of logic : Save Actions fields values for Add View
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Get all Workflow Tasks Actions fields values for Edit View
        /// </summary>
        /// <param name="workflowName">WorkFlow Name</param>
        /// <returns></returns>
        public static object GetWorkFlowActionsEditFields(int dealID, int taskID, int workflowActionID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables

                #region logic :  Get all Workflow Tasks Actions fields values for Edit View
                results = REPSDB.REPS_GetWorkFlowActionsEditFieldsValues(dealID, taskID, workflowActionID);
                return results;
                #endregion end of logic :  Get all Workflow Tasks Actions fields values for Edit View
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Save Actions fields values for Edit View
        /// </summary>
        /// <param name="workflowName">WorkFlow Name</param>
        /// <returns></returns>
        public static object SaveWorkflowActionValues(Dictionary<string, dynamic> formObjects, Dictionary<string, dynamic> FileObjects, int dealID, int userID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                Dictionary<string, object> tempnormalfields = new Dictionary<string, object>();
                Dictionary<string, object> tempdatefields = new Dictionary<string, object>();
                int OldTransactionID = 0;
                int WorkflowActionVarID = 0;
                int VariableTypeID = 0;
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                ObjectParameter WFrowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion end of variables

                #region logic : Save Actions fields values for Edit View

                // filter and remove any unwanted fields
                foreach (var item in formObjects)
                {
                    if (item.Key.Contains("FieldType"))
                    {
                        var IDArray = item.Key.Split(':');
                        if (Convert.ToInt32(IDArray[3]) == 5)           // If File upload
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(item.Value)))    // if empty, do not insert in db
                            {
                                tempnormalfields.Add(item.Key, item.Value);
                            }
                        }
                        else
                        {
                            tempnormalfields.Add(item.Key, item.Value);
                        }

                        if (OldTransactionID == 0)                      // first time only
                        {
                            //var IDArray = item.Key.Split(':');
                            OldTransactionID = Convert.ToInt32(IDArray[1]);
                        }

                    }
                }

                // Archive old values
                //var TID = REPSDB.REPS_ArchiveWorkflowActionsByTransactionID(OldTransactionID, dealID, (int)Enums.TransactionType.Edit, 4, userID, rowCount);

                int NewTransactionID = 0;

                foreach (var item in tempnormalfields)
                {

                    var IDArray = item.Key.Split(':');
                    //TransactionID = Convert.ToInt32(IDArray[1]);
                    WorkflowActionVarID = Convert.ToInt32(IDArray[2]);
                    VariableTypeID = Convert.ToInt32(IDArray[3]);
                    var TID = REPSDB.REPS_ArchiveWorkflowActionsByTransactionID(OldTransactionID, WorkflowActionVarID, dealID, (int)Enums.TransactionType.Edit, 4, userID, rowCount);

                    if (NewTransactionID == 0)                      // first time only
                    {
                        NewTransactionID = Convert.ToInt32(rowCount.Value);
                    }
                    //NewTransactionID = Convert.ToInt32(rowCount.Value);

                    results = REPSDB.REPS_UpdateWorkflowActionValues(NewTransactionID, WorkflowActionVarID, VariableTypeID, item.Value.ToString(), WFrowCount);
                }

                #region FilesLogic
                // Archive old files and copy old files to new Transaction folder
                File.ArchiveTransactionFiles(OldTransactionID, NewTransactionID, (int)Enums.FolderPath.Actions);

                // Save uploaded files
                foreach (var fileobj in FileObjects)
                {
                    string filename = fileobj.Key.ToString();

                    // Get Byte Array
                    byte[] filecontent = fileobj.Value;

                    // Build New File Path
                    string filepath = File.GetFilePath(NewTransactionID, filename, (int)Enums.FolderPath.Actions);

                    // Write New files to path
                    File.WriteFiletoPath(filepath, filename, filecontent);
                }
                #endregion end of FilesLogic

                return (WFrowCount.Value == DBNull.Value ? null : (int?)WFrowCount.Value);
                #endregion end of logic : Save Actions fields values for Edit View
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Save Workflow Name
        /// </summary>
        /// <param name="formObjects"></param>
        /// <returns></returns>
        public static object SaveWorkflowChanges(Dictionary<string, object> formObjects)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion

                #region logic

                foreach (var item in formObjects)
                {
                    results = REPSDB.REPS_ADM_UpdateWorkflowName(item.Value.ToString(), Convert.ToInt32(item.Key), rowCount);
                }

                return (rowCount.Value == DBNull.Value ? null : (int?)rowCount.Value);

                #endregion
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// Add Workflow
        /// </summary>
        /// <param name="workflowName"></param>
        /// <param name="workflowID"></param>
        /// <returns></returns>
        public static object AddWorkflow(Dictionary<string, object> formObjects, int workflowID)
        {
            try
            {

                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;

                #endregion

                #region logic

                foreach (var WorkflowName in formObjects)
                {
                    results = REPSDB.REPS_ADM_AddWorkflow(WorkflowName.Value.ToString(), workflowID);
                }


                return results;

                #endregion


            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// Get All Tasks
        /// </summary>
        /// <param name="processID"></param>
        /// <param name="workflowID"></param>
        /// <returns></returns>
        public static object GetAllTasks(int processID, int workflowID)
        {
            try
            {

                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;

                #endregion

                #region logic

                results = REPSDB.REPS_ADM_GetAllTasksPerProcess(processID, workflowID);
                return results;

                #endregion
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// Get workflow variable List
        /// </summary>
        /// <param name="actionID"></param>
        /// <returns></returns>
        public static object GetWorkflowVariableList(int actionID, int workflowTaskID)
        {
            try
            {

                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;

                #endregion

                #region logic

                results = REPSDB.REPS_ADM_GetWorkflowVariableList(actionID);
                return results;

                #endregion
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

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
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;

                #endregion

                #region logic

                results = REPSDB.REPS_ADM_GetWorkflowActionIsMandatory(actionID, workflowTaskID);
                return results;

                #endregion
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// Assign/Unassign Workflow Variable to Action
        /// </summary>
        /// <param name="actionID"></param>
        /// <param name="workflowVariableID"></param>
        /// <param name="toggle"></param>
        /// <returns></returns>
        public static object ToggleWorkflowVariable(int actionID, int workflowVariableID, bool toggle)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion end of varibles

                #region logic
                REPSDB.REPS_ADM_ToggleWorkflowVariable(workflowVariableID, actionID, toggle, rowCount);
                return (rowCount.Value == null ? null : (int?)rowCount.Value);
                #endregion end of logic
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Toggle Workflow Variable Required
        /// </summary>
        /// <param name="actionID"></param>
        /// <param name="workflowVariableID"></param>
        /// <param name="toggle"></param>
        /// <returns></returns>
        public static object ToggleWorkflowVariableRequired(int actionID, int workflowVariableID, bool toggle)
        {
            try
            {

                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion

                #region logic

                results = REPSDB.REPS_ADM_ToggleWorkflowVariableRequired(workflowVariableID, actionID, toggle, rowCount);
                return (rowCount.Value == DBNull.Value ? null : (int?)rowCount.Value);

                #endregion
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// Toggle Action Mandatory
        /// </summary>
        /// <param name="actionID"></param>
        /// <param name="toggle"></param>
        /// <returns></returns>
        public static object ToggleActionMandatory(int actionID, bool toggle)
        {
            try
            {

                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion

                #region logic

                results = REPSDB.REPS_ADM_ToggleActionMandatory(actionID, toggle, rowCount);
                return (rowCount.Value == DBNull.Value ? null : (int?)rowCount.Value);

                #endregion
            }
            catch (Exception Ex)
            {

                throw Ex;
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
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion

                #region logic

                foreach (var item in formObjects)
                {
                    results = REPSDB.REPS_ADM_UpdateActionName(item.Value.ToString(), Convert.ToInt32(item.Key), rowCount);
                }

                return (rowCount.Value == DBNull.Value ? null : (int?)rowCount.Value);

                #endregion
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

        }

        /// <summary>
        /// Add New Action
        /// </summary>
        /// <param name="formObjects"></param>
        /// <param name="workflowTaskID"></param>
        /// <returns></returns>
        public static object AddNewAction(Dictionary<string, object> formObjects, int workflowTaskID)
        {
            try
            {

                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));


                #endregion

                #region logic

                foreach (var WorkflowName in formObjects)
                {
                    results = REPSDB.REPS_ADM_AddNewAction(WorkflowName.Value.ToString(), workflowTaskID, rowCount);
                }

                return (rowCount.Value == DBNull.Value ? null : (int?)rowCount.Value);

                #endregion


            }
            catch (Exception Ex)
            {

                throw Ex;
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
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion

                #region logic

                results = REPSDB.REPS_ADM_DeleteWorkflow(workflowID, rowCount);
                return (rowCount.Value == DBNull.Value ? null : (int?)rowCount.Value);

                #endregion
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// Delete Task
        /// </summary>
        /// <param name="taskWorkflowID"></param>
        /// <returns></returns>
        public static object DeleteTask(int taskWorkflowID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion end of variables

                #region logic
                REPSDB.REPS_ADM_DeleteTask(taskWorkflowID, rowCount);
                return (rowCount.Value == null ? null : (int?)rowCount.Value);
                #endregion end of logic
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Replace user workflow ID with new workflow ID
        /// </summary>
        /// <param name="taskWorkflowID"></param>
        /// <returns></returns>
        public static object SwitchUserWorkflowAfterDelete(int workflowToBeDeleted, int workflowIDToBeReplacedWith)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion end of variables

                #region logic
                REPSDB.REPS_ADM_SwitchUserWorkflowID(workflowToBeDeleted, workflowIDToBeReplacedWith, rowCount);
                return (rowCount.Value == null ? null : (int?)rowCount.Value);
                #endregion end of logic
            }
            catch (Exception Ex)
            {
                throw Ex;
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
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));

                #endregion

                #region logic

                results = REPSDB.REPS_ADM_AssignTaskToWorkflow(taskID, workflowID, rowCount);
                return (rowCount.Value == DBNull.Value ? null : (int?)rowCount.Value);

                #endregion
            }
            catch (Exception Ex)
            {

                throw Ex;
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
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));

                #endregion wnd of variables

                #region logic
                results = REPSDB.REPS_ADM_UnAssignTaskFromWorkflow(taskID, workflowID, rowCount);
                return (rowCount.Value == DBNull.Value ? null : (int?)rowCount.Value);
                #endregion end of logic
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Assign Action to Task
        /// </summary>
        /// <param name="workflowActionID"></param>
        /// <param name="workflowTaskID"></param>
        /// <returns></returns>
        public static object AssignActionToTask(int workflowActionID, int workflowTaskID)
        {
            try
            {

                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));

                #endregion

                #region logic

                results = REPSDB.REPS_ADM_AssignActionToTask(workflowActionID, workflowTaskID, rowCount);
                return (rowCount.Value == DBNull.Value ? null : (int?)rowCount.Value);

                #endregion
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// Assign Action to Task
        /// </summary>
        /// <param name="workflowActionID"></param>
        /// <param name="workflowTaskID"></param>
        /// <returns></returns>
        public static object UnAssignActionFromTask(int workflowActionID, int workflowTaskID)
        {
            try
            {

                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion end of variables

                #region logic
                REPSDB.REPS_ADM_UnAssignActionFromTask(workflowActionID, workflowTaskID, rowCount);
                return (rowCount.Value == null ? null : (int?)rowCount.Value);
                #endregion end of logic
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// Add New Task
        /// </summary>
        /// <param name="formObjects"></param>
        /// <returns></returns>
        public static int? AddNewTask(Dictionary<string, object> formObjects)
        {
            try
            {

                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                ObjectParameter identity = new ObjectParameter("identity", typeof(int));

                #endregion

                #region logic

                for (int i = 0; i < formObjects.Count; i = i + 3)               // Loop at each 3 rows
                {
                    string Taskname = null;
                    int Taskorder = 0;
                    string Icon = null;
                    int WorkflowID = 0;
                    bool Exitloop = false;

                    int fieldcount = 0;
                    for (int j = i; j < i + 3; j++)                             // Process 3 subsequent rows ( form fields) 
                    {
                        fieldcount++;
                        switch (fieldcount)
                        {
                            case 1:
                                Taskname = formObjects[formObjects.Keys.ElementAt(j).ToString()].ToString();

                                var TextboxName = formObjects.Keys.ElementAt(j).ToString().Split(':');
                                WorkflowID = Convert.ToInt32(TextboxName[2]);

                                if (String.IsNullOrWhiteSpace(Taskname))      // Exit - Do not process other fields if Task Name is empty
                                {
                                    Exitloop = true;
                                }
                                break;
                            case 2:
                                Taskorder = Convert.ToInt32(formObjects[formObjects.Keys.ElementAt(j).ToString()]);
                                break;
                            case 3:
                                Icon = formObjects[formObjects.Keys.ElementAt(j).ToString()].ToString();
                                if (String.IsNullOrWhiteSpace(Icon))
                                {
                                    Icon = null;
                                }
                                break;
                            default:
                                break;
                        }

                        if (Exitloop)
                        {
                            break;         // endloop 
                        }
                    }

                    if (!String.IsNullOrEmpty(Taskname))
                    {
                        results = REPSDB.REPS_ADM_AddNewTask(WorkflowID, Taskname, Taskorder, Icon, rowCount, identity);   // Update values
                    }
                }

                //return (rowCount.Value == DBNull.Value ? null : (int?)rowCount.Value);
                return (identity.Value == null ? null : (int?)identity.Value);

                #endregion
            }
            catch (Exception Ex)
            {

                throw Ex;
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
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));

                #endregion

                #region logic

                for (int i = 0; i < formObjects.Count; i = i + 3)               // Loop at each 3 rows
                {
                    string Taskname = null;
                    int Taskorder = 0;
                    string Icon = null;
                    int WorkflowTaskID = 0;
                    int TaskID = 0;
                    bool Exitloop = false;

                    int fieldcount = 0;
                    for (int j = i; j < i + 3; j++)                             // Process 3 subsequent rows ( form fields) 
                    {
                        fieldcount++;
                        switch (fieldcount)
                        {
                            case 1:
                                Taskname = formObjects[formObjects.Keys.ElementAt(j).ToString()].ToString();

                                var TextboxName = formObjects.Keys.ElementAt(j).ToString().Split(':');
                                WorkflowTaskID = Convert.ToInt32(TextboxName[2]);
                                TaskID = Convert.ToInt32(TextboxName[3]);

                                if (String.IsNullOrWhiteSpace(Taskname))      // Exit - Do not process other fields if Task Name is empty
                                {
                                    Exitloop = true;
                                }
                                break;
                            case 2:
                                Taskorder = Convert.ToInt32(formObjects[formObjects.Keys.ElementAt(j).ToString()]);
                                break;
                            case 3:
                                Icon = formObjects[formObjects.Keys.ElementAt(j).ToString()].ToString();
                                if (String.IsNullOrWhiteSpace(Icon))
                                {
                                    Icon = null;
                                }
                                break;
                            default:
                                break;
                        }

                        if (Exitloop)
                        {
                            break;         // endloop 
                        }
                    }

                    results = REPSDB.REPS_ADM_UpdateTask(WorkflowTaskID, TaskID, Taskname, Taskorder, Icon, rowCount);   // Update values
                }

                return (rowCount.Value == DBNull.Value ? null : (int?)rowCount.Value);

                #endregion
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

        }

        /// <summary>
        /// get workflow deal count for company or per user
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="workflowID"></param>
        /// <param name="entityID"></param>
        /// <returns></returns>
        public static object GetWorkflowDealCount(int? UserID = null, int? workflowID = null, int? dealProcessTaskID = null, int? entityID = null)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables

                #region logic : get workflow deal count for company or per user
                results = REPSDB.REPS_GetWorkflowDealCount(UserID == null ? null : UserID, workflowID == null ? null : workflowID, dealProcessTaskID == null ? null : dealProcessTaskID, entityID == null ? null : entityID);
                return results;
                #endregion end of logic : get workflow deal count for company or per user
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Get TransactionDetailID ID by transactionDetailGUID
        /// </summary>
        /// <param name="aspNetId"></param>
        /// <returns></returns>
        public static object GetTransactionDetailIDByTransactionDetailGUID(System.Guid transactionDetailGUID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables

                #region logic : Get TransactionDetailID ID by transactionDetailGUID
                results = REPSDB.REPS_GetTransactionDetailByTransactionDetailGUID(transactionDetailGUID);
                return results;
                #endregion end of logic : Get TransactionDetailID ID by transactionDetailGUID
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// remove TransactionDetailID
        /// </summary>
        /// <param name="ob"></param>
        /// <returns></returns>
        public static int? RemoveTransactionImg(DATA.Entity.TransactionDetail obj)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion end of variables

                #region logic : remove participant
                REPSDB.REPS_DeleteTransactionImg((int)obj.TransactionDetailID, obj.Deleted , rowCount);
                return (rowCount.Value == null ? null : (int?)rowCount.Value);
                #endregion logic : remove participant
            }
            catch (Exception Ex)
            {
                throw Ex;
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
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables

                #region logic
                results = REPSDB.REPS_GetCurrentWorkflowDetails(taskID, taskWorkflowID);
                return results;
                #endregion end of logic
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Add workflowtaskid for each document to documentworkflow table
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object AddWorkflowStep(DATA.Entity.DocumentWorkflow obj)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion end of variables

                #region logic : Add workflowtaskid for each document to documentworkflow table
                REPSDB.REPS_ADM_AddWorkflowStepToDocumentTemplate(
                    (int)obj.DocumentVersionID, 
                    (int)obj.WorkflowStepID,
                    (int)obj.CreatedByUserID, 
                    rowCount
                    );
                return (rowCount.Value == null ? null : (int?)rowCount.Value);
                #endregion logic : Add workflowtaskid for each document to documentworkflow table
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Get Templates of Workflow Step 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object GetTemplatePerWorkflowStep(int? documentVersionID ,int? workflowStepID )
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                #endregion end of variables

                #region logic : Get Templates of Workflow Step 
                return REPSDB.REPS_GetTemplatePerWorkflowStep(documentVersionID, workflowStepID);
                #endregion logic : Get Templates of Workflow Step 
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

    }
}
