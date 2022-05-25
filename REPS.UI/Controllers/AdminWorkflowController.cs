using Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using REPS.UI.Models;
using REPS.DATA.Entity;

namespace REPS.UI.Controllers
{
    [TokenAuthorizeAttribute]
    public class AdminWorkflowController : Controller
    {
        public int SelectedWorkflowIDTab = 0;
        public int SelectedTaskIDTab = 0;
        // GET: AdminWorkflow
        public ActionResult Index(int? WorkflowID, int? TaskID, bool partial = false, bool noLayout = false)
        {
            try
            {
                // Check if Ajax Request and determine layout to be used
                if (Request.IsAjaxRequest())
                {
                    ViewBag.PageLayoutPath = null;
                }
                else
                {
                    ViewBag.PageLayoutPath = Global.Constants.MainLayoutPath;
                }
                #region Variables
                int SelectedWorkflowID = 0;
                int SelectedTaskID = 0;
                int SelectedTaskWorkflowID = 0;
                object TasksList = null;
                #endregion end of variables
                /// Call WCF to get all workflows
                object WorkflowsList = Models.WorkflowModel.GetWorkflowsList((int)Global.Enums.Wokflow.Process);
                ViewData["WorkflowsList"] = WorkflowsList;

                // First time load
                if (TaskID == null)
                {
                    if (WorkflowID == null)
                    {
                        SelectedWorkflowID = (WorkflowsList as IEnumerable<dynamic>).FirstOrDefault()["TaskID"];
                    }
                    else
                    {
                        SelectedWorkflowID = (int)WorkflowID;
                    }

                    foreach (var workflow in WorkflowsList as IEnumerable<dynamic>)
                    {
                        if (workflow["TaskID"] == SelectedWorkflowID)
                        {
                            SelectedTaskWorkflowID = workflow["TaskWorkflowID"];
                            break;
                        }
                    }

                    /// Call WCF to get all Tasks list per workflowID
                    TasksList = Models.WorkflowModel.GetWorkflowsList(SelectedTaskWorkflowID);

                    ///Check if tasklist has a list
                    if ((TasksList as IEnumerable<dynamic>).Count() > 0)
                    {
                        SelectedTaskID = (TasksList as IEnumerable<dynamic>).FirstOrDefault()["TaskID"];
                    }
                }
                else
                {   // Set previously selected ID's
                    SelectedWorkflowID = (int)WorkflowID;
                    SelectedTaskID = (int)TaskID;

                    foreach (var workflow in WorkflowsList as IEnumerable<dynamic>)
                    {
                        if (workflow["TaskID"] == SelectedWorkflowID)
                        {
                            SelectedTaskWorkflowID = workflow["TaskWorkflowID"];
                            break;
                        }
                    }

                    /// Call WCF to get all Tasks list per workflowID
                    TasksList = Models.WorkflowModel.GetWorkflowsList(SelectedTaskWorkflowID);
                }
                
                TempData["SelectedWorkflowID"] = SelectedWorkflowID;
                TempData["SelectedTaskID"] = SelectedTaskID;

                // Save value for use when Adding/Updating Tasks,Actions
                SelectedWorkflowIDTab = SelectedWorkflowID;
                SelectedTaskIDTab = SelectedTaskID;
                
                string WorkflowTaskID = SelectedTaskWorkflowID.ToString() + SelectedTaskID.ToString();

                /// Call WCF to get workflow tasks actions depending on task ID
                #region Call WCF to get workflow tasks actions depending on task ID
                object WorkflowTasksActionsListModel = Models.WorkflowModel.GetWorkFlowTasksActionsList(Convert.ToInt32(WorkflowTaskID));
                #endregion end of Call WCF to get workflow tasks actions depending on task ID

                ViewData["WorkflowsList"] = WorkflowsList;
                ViewData["TasksList"] = TasksList;
                ViewData["TaskActionsList"] = WorkflowTasksActionsListModel;

                ViewData["SelectedLayout"] = "Admin";

                if (partial)
                {
                    return View("PartialAdminWorkflowList");
                }
                else
                {
                    if (noLayout)
                    {
                        ViewBag.PageLayoutPath = null;
                    }
                    return View();
                }
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Load popup to assign tasks
        /// </summary>
        /// <param name="workflowID"></param>
        /// <param name="taskWorkflowID"></param>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public ActionResult PopupAssignTasks(int workflowID, int taskWorkflowID, int taskID)
        {
            try
            {
                #region get tasks
                /// Call WCF to get all tasks
                object TasksList = Models.WorkflowModel.GetAllTasks((int)Global.Enums.Wokflow.Process, taskWorkflowID);
                ViewData["TasksList"] = TasksList;
                ViewData["TaskID"] = taskID;
                ViewData["TaskWorkflowID"] = taskWorkflowID;
                ViewData["WorkflowID"] = workflowID;
                ViewData["title"] = "Manage Tasks";
                #endregion

                return View("PopupAssignTasks");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Get Add Action fields
        /// </summary>
        /// <returns></returns>
        public ActionResult PopupAddAction(int workflowTaskID, int indexTaskID, int workflowID)
        {
            try
            {
                /// Call WCF to get workflow tasks actions depending on task ID
                #region Call WCF to get workflow tasks actions depending on task ID
                object ActionsList = Models.WorkflowModel.GetWorkFlowTasksActionsList(workflowTaskID);
                ViewData["ActionsList"] = ActionsList;
                ViewData["WorkflowTaskID"] = workflowTaskID;
                #endregion

                ViewData["SelectedWorkflowIDTab"] = SelectedWorkflowIDTab;
                ViewData["SelectedTaskIDTab"] = SelectedTaskIDTab;
                
                ViewData["WorkflowID"] = workflowID;
                ViewData["TaskID"] = indexTaskID;

                return View();
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        public ActionResult PopupEditAction(int taskID, int actionID)
        {
            try
            {
                object VariableList = Models.WorkflowModel.GetWorkflowVariableList(actionID, taskID);
                ViewData["VariableList"] = VariableList;
                ViewData["ActionID"] = actionID;
                ViewData["WorkflowTaskID"] = taskID;

                object WorkflowActionMandatory = Models.WorkflowModel.GetWorkflowActionIsMandatory(actionID, taskID);
                ViewData["WorkflowActionMandatory"] = WorkflowActionMandatory;

                foreach (var item in (ViewData["WorkflowActionMandatory"] as IEnumerable<dynamic>))
                {

                    ViewData["WorkflowActionMapID"] = Convert.ToInt32(item["ID"]);
                    ViewData["IsMandatory"] = Convert.ToBoolean(item["IsMandatory"]);

                }

                return View();
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        public ActionResult PopupManageTasks(int workflowID, int taskWorkflowID, int taskID)
        {
            try
            {
                /// Call WCF to get all Tasks list per workflowID
                object TasksList = Models.WorkflowModel.GetWorkflowsList(taskWorkflowID);
                ViewData["TasksList"] = TasksList;

                object TasksListAssigned = Models.WorkflowModel.GetAllTasks((int)Global.Enums.Wokflow.Process, taskWorkflowID);
                ViewData["TasksListAssigned"] = TasksListAssigned;

                ViewData["TaskWorkflowID"] = taskWorkflowID;


                ViewData["WorkflowID"] = workflowID;
                ViewData["TaskID"] = taskID;

                return View();
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        public ActionResult PopupManageWorkflows(int workflowID, int taskID)
        {
            try
            {
                #region get workflows
                /// Call WCF to get all workflows
                object WorkflowsList = Models.WorkflowModel.GetWorkflowsList((int)Global.Enums.Wokflow.Process);
                ViewData["WorkflowsList"] = WorkflowsList;
                #endregion

                ViewData["WorkflowID"] = workflowID;
                ViewData["TaskID"] = taskID;

                return View();
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Save workflow changes( workflow name )
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveWorkflowChanges(int WorkflowID, int TaskID)
        {
            try
            {
                var WorkflowItemsUpdate = new Dictionary<string, object>();
                var WorkflowItemsAdd = new Dictionary<string, object>();

                // Separate fields Edit and Add workflow for processing
                foreach (var item in Request.Form)
                {
                    if (Convert.ToString(item).Contains("Edit"))
                    {
                        var TextboxName = item.ToString().Split(':');
                        WorkflowItemsUpdate.Add(TextboxName[1], Request.Form[item.ToString()]);
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(Request.Form[item.ToString()]))           // Remove fields with no data entered
                        {
                            if (item.ToString() != "X-Requested-With")
                                WorkflowItemsAdd.Add(item.ToString(), Request.Form[item.ToString()]);
                        }

                    }
                }

                // Call WCF to save workflow names
                object SaveWorkflow = Models.WorkflowModel.SaveWorkflowChanges(WorkflowItemsUpdate);

                //if (Convert.ToInt32(SaveWorkflow) > 0)
                //{
                //    TempData["ToasterMsg"] = Notifications.GetToastrMessage(Enums.MessageType.success.ToString(), "Change(s) successfully saved','Successful");
                //}

                // Call WCF to Add new workflow
                if (WorkflowItemsAdd.Count > 0)
                {
                    object AddNewWorkflow = Models.WorkflowModel.AddNewWorkflow(WorkflowItemsAdd, (int)Global.Enums.Wokflow.Process);
                    //if (Convert.ToInt32(AddNewWorkflow) > 0)
                    //{
                    //    TempData["ToasterMsg"] = Notifications.GetToastrMessage(Enums.MessageType.success.ToString(), "Change(s) successfully saved','Successful");
                    //}
                }


                return RedirectToAction("Index", new { WorkflowID = WorkflowID, TaskID = TaskID });
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Go to selected Workflow
        /// </summary>
        /// <param name="WorkflowId"></param>
        /// <returns></returns>
        public ActionResult GetWorkflowTab(int? WorkflowId, bool partial = false)
        {
            try
            {
                //return Index(WorkflowId, null);
                return RedirectToAction("Index", new { WorkflowID = WorkflowId, partial = partial });
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Go to selected Task
        /// </summary>
        /// <param name="WorkflowId"></param>
        /// <param name="TaskId"></param>
        /// <returns></returns>
        public ActionResult GetTaskTab(int? workflowId, int? taskId)
        {
            try
            {
                return RedirectToAction("Index", new { WorkflowID = workflowId, TaskID = taskId });
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Assign/Unassign Workflow Variable to Action
        /// </summary>
        /// <param name="actionID"></param>
        /// <param name="workflowVariableID"></param>
        /// <param name="paramToggle"></param>
        [HttpPost]
        public void ToggleAction(int actionID, int workflowVariableID, bool toggle)
        {
            try
            {
                // Call WCF to Assign/Unassign Workflow Variable to Action
                object SaveAction = Models.WorkflowModel.ToggleAction(actionID, workflowVariableID, toggle);
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            }
        }

        /// <summary>
        /// Update Workflow Variable mapping to set/unset IsRequired
        /// </summary>
        /// <param name="actionID"></param>
        /// <param name="workflowVariableID"></param>
        /// <param name="paramToggle"></param>
        [HttpPost]
        public void ToggleActionRequired(int actionID, int workflowVariableID, bool toggle)
        {
            try
            {
                // Call WCF to Update Workflow Variable mapping to set/unset IsRequired
                object SaveAction = Models.WorkflowModel.ToggleActionRequired(actionID, workflowVariableID, toggle);
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            }
        }

        /// <summary>
        /// Toggle Action Mandatory
        /// </summary>
        /// <param name="actionID"></param>
        /// <param name="toggle"></param>
        [HttpPost]
        public void ToggleActionMandatory(int actionID, bool toggle)
        {
            try
            {
                // Call WCF to Update Workflow Variable mapping to set/unset IsMandatory
                object SaveAction = Models.WorkflowModel.ToggleActionMandatory(actionID, toggle);
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            }
        }

        /// <summary>
        /// Save Action changes
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveActionChanges(int WorkflowID, int TaskID)
        {
            try
            {
                var ActionItemsUpdate = new Dictionary<string, object>();
                var ActionItemsAdd = new Dictionary<string, object>();
                int WorkflowTaskID = 0;

                // Separate fields Edit and Add action for processing
                foreach (var item in Request.Form)
                {
                    if (Convert.ToString(item).Contains("Edit"))
                    {
                        var TextboxName = item.ToString().Split(':');
                        ActionItemsUpdate.Add(TextboxName[1], Request.Form[item.ToString()]);
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(Request.Form[item.ToString()]))           // Remove fields with no data entered
                        {
                            if (item.ToString() != "X-Requested-With")
                            {
                                var TextboxName = item.ToString().Split(':');
                                WorkflowTaskID = Convert.ToInt32(TextboxName[1]);
                                ActionItemsAdd.Add(item.ToString(), Request.Form[item.ToString()]);
                            }
                        }

                    }
                }

                // Call WCF to save action names
                object SaveAction = Models.WorkflowModel.SaveActionChanges(ActionItemsUpdate);

                //if (Convert.ToInt32(SaveAction) > 0)
                //{
                //    TempData["ToasterMsg"] = Notifications.GetToastrMessage(Enums.MessageType.success.ToString(), "Change(s) successfully saved','Successful");
                //}

                // Call WCF to Add new action
                if (ActionItemsAdd.Count > 0)
                {
                    object AddNewWorkflow = Models.WorkflowModel.AddNewAction(ActionItemsAdd, WorkflowTaskID);
                    //if (Convert.ToInt32(AddNewWorkflow) > 0)
                    //{
                    //    TempData["ToasterMsg"] = Notifications.GetToastrMessage(Enums.MessageType.success.ToString(), "Change(s) successfully saved','Successful");
                    //}
                }

                return RedirectToAction("Index", new { WorkflowID = WorkflowID, TaskID = TaskID });
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Save Tasks ( Add or Update )
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveTaskChanges(int WorkflowID, int TaskID)
        {
            try
            {
                int newtaskID = 0;
                var TaskItemsUpdate = new Dictionary<string, object>();
                var TaskItemsAdd = new Dictionary<string, object>();

                // Separate fields Edit and Add workflow for processing
                foreach (var item in Request.Form)
                {
                    if (Convert.ToString(item).Contains("Edit"))
                    {
                        //var TextboxName = item.ToString().Split(':');
                        TaskItemsUpdate.Add(item.ToString(), Request.Form[item.ToString()]);
                    }
                    else
                    {    // Add fields
                        if (item.ToString() != "X-Requested-With")
                            TaskItemsAdd.Add(item.ToString(), Request.Form[item.ToString()]);
                    }
                }

                // Call WCF to save Tasks details
                object SaveTask = Models.WorkflowModel.UpdateTask(TaskItemsUpdate);

                //if (Convert.ToInt32(SaveTask) > 0)
                //{
                //    TempData["ToasterMsg"] = Notifications.GetToastrMessage(Enums.MessageType.success.ToString(), "Change(s) successfully saved','Successful");
                //}


                // Call WCF to Add new task
                if (TaskItemsAdd.Count > 0)
                {
                    newtaskID = (int)Models.WorkflowModel.AddNewTask(TaskItemsAdd);
                    //if (Convert.ToInt32(AddNewTask) > 0)
                    //{
                    //    TempData["ToasterMsg"] = Notifications.GetToastrMessage(Enums.MessageType.success.ToString(), "Change(s) successfully saved','Successful");
                    //}

                }

                return RedirectToAction("Index", new { WorkflowID = WorkflowID, TaskID = newtaskID });
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Remove Workflow 
        /// </summary>
        /// <param name="workflowID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteWorkflow(int workflowID, int? indexWorkflowID, int? taskID)
        {
            try
            {
                if (indexWorkflowID.ToString() == workflowID.ToString().Substring(workflowID.ToString().Length - indexWorkflowID.ToString().Length))
                {
                    indexWorkflowID = null;
                    taskID = null;
                }

                #region get workflows
                /// Call WCF to get all workflows
                object WorkflowsListBeforeDelete = Models.WorkflowModel.GetWorkflowsList((int)Global.Enums.Wokflow.Process);
                ViewData["WorkflowsListBeforeDelete"] = WorkflowsListBeforeDelete;
                #endregion

                #region set new workflowid for user
                bool foundNewWF = false;
                int newWorkflowID = -1;
                int oldWorkflowID = 0;
                foreach (var Workflow in ViewData["WorkflowsListBeforeDelete"] as IEnumerable<dynamic>)
                {
                    if (workflowID == Workflow["TaskWorkflowID"])
                    {
                        oldWorkflowID = Workflow["TaskID"];
                    }
                    if ((!foundNewWF) && (Workflow["TaskWorkflowID"] != workflowID))
                    {
                        newWorkflowID = Workflow["TaskID"];
                    }
                }
                #endregion end set new workflowid for user

                object WorkflowObject = Models.WorkflowModel.DeleteWorkflow(workflowID);

                object setWorkflowID = Models.WorkflowModel.SwitchUserWorkflowAfterDelete(oldWorkflowID, newWorkflowID);

                ViewData["WorkflowID"] = indexWorkflowID;
                ViewData["TaskID"] = taskID;
                
                #region get workflows
                /// Call WCF to get all workflows
                object WorkflowsList = Models.WorkflowModel.GetWorkflowsList((int)Global.Enums.Wokflow.Process);
                ViewData["WorkflowsList"] = WorkflowsList;
                #endregion

                return PartialView("PopupManageWorkflows");

                //return RedirectToAction("Index", new { WorkflowID = indexWorkflowID, TaskID = taskID, noLayout = true });

            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Remove Task
        /// </summary>
        /// <param name="taskWorkflowID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteTask(int taskWorkflowID, int workflowID, int? taskID)
        {
            try
            {
                if (taskID.ToString() == taskWorkflowID.ToString().Substring(taskWorkflowID.ToString().Length - taskID.ToString().Length))
                {
                    taskID = null;
                }

                object WorkflowObject = Models.WorkflowModel.DeleteTask(taskWorkflowID);

                /// Call WCF to get all Tasks list per workflowID
                object TasksList = Models.WorkflowModel.GetWorkflowsList(int.Parse(taskWorkflowID.ToString().Substring(0, 7)));
                ViewData["TasksList"] = TasksList;

                object TasksListAssigned = Models.WorkflowModel.GetAllTasks((int)Global.Enums.Wokflow.Process, taskWorkflowID);
                ViewData["TasksListAssigned"] = TasksListAssigned;

                ViewData["TaskWorkflowID"] = taskWorkflowID;


                ViewData["WorkflowID"] = workflowID;
                ViewData["TaskID"] = taskID;

                return PartialView("PopupManageTasks");
                //return RedirectToAction("Index", new { WorkflowID = workflowID, TaskID = taskID, noLayout = true });
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Assign Selected Task to Workflow
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="workflowID"></param>
        /// <returns></returns>
        [HttpPost]
        public void AssignTaskToWorkflow(int taskID, int workflowID)
        {
            try
            {
                // Call WCF to assign Task to worflow
                object WorkflowObject = Models.WorkflowModel.AssignTaskToWorkflow(taskID, workflowID);
                //if (Convert.ToInt32(WorkflowObject) > 0)
                //{
                //    TempData["ToasterMsg"] = Notifications.GetToastrMessage(Enums.MessageType.success.ToString(), "Change(s) successfully saved','Successful");
                //}


            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                //return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }

        }

        /// <summary>
        /// Unassign Selected Task from Workflow
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="workflowID"></param>
        /// <returns></returns>
        [HttpPost]
        public void UnassignTaskFromWorkflow(int taskID, int workflowID)
        {
            try
            {
                // Call WCF to unassign Task from worflow
                object WorkflowObject = Models.WorkflowModel.UnAssignTaskFromWorkflow(taskID, workflowID);
                //if (Convert.ToInt32(WorkflowObject) > 0)
                //{
                //    TempData["ToasterMsg"] = Notifications.GetToastrMessage(Enums.MessageType.success.ToString(), "Change(s) successfully saved','Successful");
                //}
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                //return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }

        }

        /// <summary>
        /// Remove Action
        /// </summary>
        /// <param name="actionID"></param>
        /// <param name="workflowTaskID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteAction(int actionID, int workflowTaskID, int workflowID, int taskID)
        {
            try
            {
                object WorkflowObject = Models.WorkflowModel.UnAssignActionFromTask(actionID, workflowTaskID);
                return RedirectToAction("Index", new { WorkflowID = workflowID, TaskID = taskID, noLayout = true });
                //return Json(WorkflowObject, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Unassign Action From Task
        /// </summary>
        /// <param name="actionID"></param>
        /// <param name="workflowTaskID"></param>
        [HttpPost]
        public void UnassignActionFromTask(int actionID, int workflowTaskID)
        {
            try
            {
                // Call WCF to unassign Task from worflow
                object WorkflowObject = Models.WorkflowModel.UnAssignActionFromTask(actionID, workflowTaskID);
                //TempData["ToasterMsg"] = Notifications.GetToastrMessage(Enums.MessageType.success.ToString(), "Change(s) successfully saved','Successful");

            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                //return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Assign Action To Task
        /// </summary>
        /// <param name="actionID"></param>
        /// <param name="workflowTaskID"></param>
        [HttpPost]
        public void AssignActionToTask(int actionID, int workflowTaskID)
        {
            try
            {
                // Call WCF to assign Task to worflow
                object TaskObject = Models.WorkflowModel.AssignActionToTask(actionID, workflowTaskID);
                if (Convert.ToInt32(TaskObject) > 0)
                {
                    //TempData["ToasterMsg"] = Notifications.GetToastrMessage(Enums.MessageType.success.ToString(), "Change(s) successfully saved','Successful");
                }
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                //return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Check if workflow name exists
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CheckIfWorkflowNameExists(string name, int? parameter)
        {
            // Get all workflows
            /// Call WCF to get all workflows
            object WorkflowsList = Models.WorkflowModel.GetWorkflowsList((int)Global.Enums.Wokflow.Process);
            int workflowFoundInList = 0;

            // Check if the workflow we are looking for already exists
            foreach (var workflow in WorkflowsList as IEnumerable<dynamic>)
            {
                if (workflow["TaskName"].Trim().ToString().ToLower() == name.ToLower())
                {
                    workflowFoundInList++;
                }
            }

            if (workflowFoundInList > 0)
                return Content("ko"); // if workflow exists we return 'ko'
            else
                return Content("ok"); // else 'ok' to proceed with the form submission
        }

        /// <summary>
        /// Check if task name exists
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CheckIfWorkflowTaskNameExists(string name, int parameter)
        {
            // Get all tasks
            object TasksList = Models.WorkflowModel.GetAllTasks((int)Global.Enums.Wokflow.Process, parameter);
            int taskFoundInList = 0;
            
            // Check if the task we are looking for already exists
            foreach (var task in TasksList as IEnumerable<dynamic>)
            {
                if (task["TaskName"].Trim().ToString().ToLower() == name.ToLower())
                {
                    taskFoundInList++;
                }
            }

            if (taskFoundInList > 0)
                return Content("ko"); // if task exists we return 'ko'
            else
                return Content("ok"); // else 'ok' to proceed with the form submission
        }

        /// <summary>
        /// get document details 
        /// </summary>
        /// <param name="workflowStepID"></param>
        /// <returns></returns>
        public ActionResult PopupDocumentWorkflow(int workflowStepID)
        {
            try
            {
                //Get Document Template list depending on workflow
                ViewData["DocumentTemplateList"] = DocumentModel.GetAdminDocumentTemplateList();
                ViewData["DocumentTemplateWorkflowStepList"] = WorkflowModel.GetTemplatePerWorkflowStep(null, workflowStepID);
                //End of Get Document Template list depending on workflow
                ViewData["workflowStepID"] = workflowStepID.ToString();
                return View();
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// assign document a workflow step
        /// </summary>
        /// <param name="documentTemplateID"></param>
        /// <param name="workflowStepID"></param>
        /// <returns></returns>
        public ActionResult SaveDocumentWorkflow(string documentTemplateID, string workflowStepID)
        {
            try
            {
                #region variables
                /// Log Start info
                Common.CLog.WriteLogInfo("Admin Workflow : Assign Document to Workflow Step", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                /// Initiate Validator
                //get user by asp net id
                var UserID = Models.UserModel.GetUserIDByAspNetID(HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value.ToString());
                // end of get user by asp net id
                #endregion end variables

                ///Copy posted values
                DocumentWorkflow documentWorkflowTaskStep = new DocumentWorkflow();
                documentWorkflowTaskStep.DocumentVersionID = Convert.ToInt32(documentTemplateID);
                documentWorkflowTaskStep.WorkflowStepID = Convert.ToInt32(workflowStepID);
                documentWorkflowTaskStep.CreatedByUserID = Convert.ToInt32(UserID);

                /// Add Document Template
                object resultsave = DocumentModel.AddWorkflowTask(documentWorkflowTaskStep);

                TempData["ToasterMsg"] = Notifications.GetToastrMessage(Enums.MessageType.success.ToString(), "Document Workflow Step has been successfully added");

                return View();
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// unassign selected document a workflow step
        /// </summary>
        /// <param name="documentTemplateID"></param>
        /// <param name="workflowStepID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UnassignDocumentWorkflow(string documentTemplateID, string workflowStepID)
        {
            try
            {
                #region set delete true in document workflow table if workflow existed with the same template id
                object resultsave = DocumentModel.RemoveAdminDocumentWorkflow(Convert.ToInt32( documentTemplateID), Convert.ToInt32(workflowStepID));
                if (Convert.ToInt32(resultsave) > 0)
                {
                    TempData["ToasterMsg"] = Notifications.GetToastrMessage(Enums.MessageType.success.ToString(), "Document Workflow has been successfully removed");
                }
                #endregion end of set delete true in document workflow table if workflow existed with the same template id
                return View();
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }

        }
    }
}