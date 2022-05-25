using Global;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Configuration;
using REPS.DATA.Entity;

namespace REPS.UI.Controllers
{
    public class WorkflowController : Controller
    {
        int SelectedTaskID = 0;

        // GET: Workflow
        public ActionResult Index()
        {
            try
            {
                int dealID = 0;
                string UniqueReference = string.Empty;
                // Check if Ajax Request and determine layout to be used
                if (Request.IsAjaxRequest())
                {
                    #region Get DealID from URL Parameter (Unique Reference)

                    if (Request.UrlReferrer.Query == null)
                    {
                        return Content(Enums.UniqueReference.Invalidreference.ToString());
                    }
                    UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
                    if (UniqueReference != Enums.UniqueReference.Invalid.ToString())
                    {
                        object dealIDObject = Models.DealModel.GetDealIDByDealUniqueRef(UniqueReference); // We get the DealID from the UR
                        if (dealIDObject == null)
                        {
                            return Content(Enums.UniqueReference.Invalidreference.ToString());
                        }
                        dealID = Convert.ToInt32(dealIDObject);
                    }
                    else
                    {
                        return Content(Enums.UniqueReference.Invalidreference.ToString());
                    }

                    #endregion Get DealID from URL Parameter (Unique Reference)

                    ViewBag.PageLayoutPath = null;
                }
                else
                {
                    #region Get DealID from URL Parameter (Unique Reference)

                    UniqueReference = Common.CUniqueReference.GetUniqueReferenceNonAjaxRequest(Request["URef"]);
                    if (UniqueReference != Enums.UniqueReference.Invalid.ToString())
                    {
                        object dealIDObject = Models.DealModel.GetDealIDByDealUniqueRef(UniqueReference); // We get the DealID from the UR
                        if (dealIDObject == null)
                        {
                            return RedirectToAction("Index", "Dashboard");
                        }
                        dealID = Convert.ToInt32(dealIDObject);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }

                    #endregion Get DealID from URL Parameter (Unique Reference)

                    ViewBag.PageLayoutPath = Global.Constants.MainLayoutPath;
                }

                #region variables
                //int SelectedTaskID = 0;
                object WorkflowTasksList = null;
                #endregion end variables

                /// Call WCF to get workflow tasks depending on workflow ID
                #region Call WCF to get workflow tasks depending on workflow ID
                WorkflowTasksList = Models.WorkflowModel.GetWorkflow(dealID);
                #endregion Call WCF to get workflow tasks depending on workflow ID

                if (WorkflowTasksList is object[] && ((object[])WorkflowTasksList).Any())
                {
                    ViewData["WorkflowTasksList"] = WorkflowTasksList;
                    /// set default Task ID
                    SelectedTaskID = (WorkflowTasksList as IEnumerable<dynamic>).FirstOrDefault()["WorkflowTaskID"];

                    ViewData["SelectedTaskID"] = SelectedTaskID;

                    GetTaskActions(SelectedTaskID, dealID);
                }
                /// Redirect to first tab         
                return View();
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Re-routes to view based on Task ID
        /// </summary>
        /// <param name="TaskID"></param>
        /// <returns></returns>
        public ActionResult WorkflowTaskTab(int TaskID, int? dealID)
        {
            try
            {
                //#region Variables
                object WorkflowTasksList = null;
                //#endregion end vaiables

                if (dealID == null)
                {
                    string UniqueReference = string.Empty;
                    // Check if Ajax Request and determine layout to be used
                    if (Request.IsAjaxRequest())
                    {
                        #region Get DealID from URL Parameter (Unique Reference)

                        if (Request.UrlReferrer.Query == null)
                        {
                            return Content(Enums.UniqueReference.Invalidreference.ToString());
                        }
                        UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
                        if (UniqueReference != Enums.UniqueReference.Invalid.ToString())
                        {
                            object dealIDObject = Models.DealModel.GetDealIDByDealUniqueRef(UniqueReference); // We get the DealID from the UR
                            if (dealIDObject == null)
                            {
                                return Content(Enums.UniqueReference.Invalidreference.ToString());
                            }
                            dealID = Convert.ToInt32(dealIDObject);
                        }
                        else
                        {
                            return Content(Enums.UniqueReference.Invalidreference.ToString());
                        }

                        #endregion Get DealID from URL Parameter (Unique Reference)

                        ViewBag.PageLayoutPath = null;
                    }
                    else
                    {
                        #region Get DealID from URL Parameter (Unique Reference)

                        UniqueReference = Common.CUniqueReference.GetUniqueReferenceNonAjaxRequest(Request["URef"]);
                        if (UniqueReference != Enums.UniqueReference.Invalid.ToString())
                        {
                            object dealIDObject = Models.DealModel.GetDealIDByDealUniqueRef(UniqueReference); // We get the DealID from the UR
                            if (dealIDObject == null)
                            {
                                return RedirectToAction("Index", "Dashboard");
                            }
                            dealID = Convert.ToInt32(dealIDObject);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Dashboard");
                        }

                        #endregion Get DealID from URL Parameter (Unique Reference)

                        ViewBag.PageLayoutPath = Global.Constants.MainLayoutPath;
                    }
                }
                int DealID = (int)dealID;
                /// Call WCF to get workflow tasks depending on workflow ID
                #region Call WCF to get workflow tasks depending on workflow ID
                WorkflowTasksList = Models.WorkflowModel.GetWorkflow(DealID);
                ViewData["WorkflowTasksList"] = WorkflowTasksList;
                #endregion Call WCF to get workflow tasks depending on workflow ID

                ViewData["SelectedTaskID"] = TaskID;
                ViewData["TaskID"] = TaskID;

                /// Get Workflow Task Actions and set to ViewData
                var Actionslist = GetTaskActions(TaskID, DealID);
                return View("Index");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Get Workflow Tasks Actions based on TaskID
        /// </summary>
        /// <param name="TaskID"></param>
        /// <returns></returns>
        public object GetTaskActions(int taskID, int dealID)
        {
            try
            {
                #region variables
                /// Log Start info
                Common.CLog.WriteLogInfo("Workflow", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                /// Initiate Validator
                ViewData["WorkflowTasksActionsList"] = null;
                #endregion variables


                /// Call WCF to get workflow tasks actions depending on task ID
                #region Call WCF to get workflow tasks actions depending on task ID
                object WorkflowTasksActionsListModel = Models.WorkflowModel.GetWorkFlowTasksActions(taskID, dealID);
                ViewData["WorkflowTasksActionsList"] = WorkflowTasksActionsListModel;
                #endregion end Call WCF to get workflow tasks actions depending on task ID

                return ViewData["WorkflowTasksActionsList"];
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Get all fields based on WorkflowActionID (ADD)
        /// </summary>
        /// <param name="workflowActionID"></param>
        /// <returns></returns>
        public ActionResult PopupAddAction(int taskID, int workflowActionID)
        {
            try
            {
                #region variables
                /// Initiate Validator
                TempData["WorkflowTasksActionsResult"] = null;
                #endregion variables

                /// Call WCF to Get all Workflow Tasks Actions fields for Add View
                #region Call WCF to Get all Workflow Tasks Actions fields for Add View
                object getWorkFlowActionsAddFields = Models.WorkflowModel.GetWorkFlowActionsAddFields(taskID, workflowActionID);
                ViewData["WorkflowTasksActionsResult"] = getWorkFlowActionsAddFields;
                #endregion Call WCF to Get all Workflow Tasks Actions fields for Add View

                ViewData["taskID"] = taskID;

                return View("PopupAddAction");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Save changes to DB (Add Action)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult InsertWorkflowActionValues()
        {

            try
            {
                #region Get DealID from URL Parameter (Unique Reference)

                if (Request.UrlReferrer.Query == null)
                {
                    return Content(Enums.UniqueReference.Invalidreference.ToString());
                }
                int dealID = 0;
                string UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
                if (UniqueReference != Enums.UniqueReference.Invalid.ToString())
                {
                    object dealIDObject = Models.DealModel.GetDealIDByDealUniqueRef(UniqueReference); // We get the DealID from the UR
                    if (dealIDObject == null)
                    {
                        return Content(Enums.UniqueReference.Invalidreference.ToString());
                    }
                    dealID = Convert.ToInt32(dealIDObject);
                }
                else
                {
                    return Content(Enums.UniqueReference.Invalidreference.ToString());
                }

                #endregion Get DealID from URL Parameter (Unique Reference)

                //get userid by AspNetUsersId"
                string aspNetUsersId = HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value;
                object userID = Models.UserModel.GetUserIDByAspNetID(aspNetUsersId);
                // end of get userid by AspNetUsersId"

                #region variables
                /// Log Start info
                Common.CLog.WriteLogInfo("Workflow", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                var FormObjects = new Dictionary<string, dynamic>();
                var FileObjects = new Dictionary<string, dynamic>();
                // object FormObjects;
                var TaskID = "";

                var dataSerialized = Request.Form["formDataValues"].ToString();
                FormObjects = Common.CDynamicSqlRow.DecodeSerialize(dataSerialized);

                //get taskID from FormObjects
                foreach (var taskIDCheck in FormObjects)
                {
                    if (taskIDCheck.Key.Contains("taskID"))
                    {
                        TaskID = taskIDCheck.Value;
                    }
                }
                //end of get task id from FormObjects
                #endregion end variables

                #region FilesLogic
                byte[] UploadedDoc = null;

                if (Request.Files["attachedFile"] != null)
                {
                    HttpPostedFileBase postedfile = Request.Files["attachedFile"];
                    var postedfileUIName = Request.Form["attachedFileUIName"]; /// Get UI element name

                    /// Add files to collection to insert in Transaction details table
                    FormObjects.Add(postedfileUIName, postedfile.FileName);

                    //var fileContent = postedfile.;
                    if (postedfile != null && postedfile.ContentLength > 0)
                    {
                        UploadedDoc = new byte[postedfile.ContentLength];
                        string uploadedFileName = postedfile.FileName.Substring(postedfile.FileName.LastIndexOf('\\') + 1);
                        string[] Extention = uploadedFileName.Split('.');
                        postedfile.InputStream.Position = 0;
                        postedfile.InputStream.Read(UploadedDoc, 0, postedfile.ContentLength);
                        FileObjects.Add(uploadedFileName, UploadedDoc);
                        //FileObjects.Add(postedfile.FileName, Common.CFile.ConvertByteArrayToBase64String(UploadedDoc));
                        //FileObjects.Add(postedfile.FileName,UploadedDoc.ToString());
                    }
                }
                #endregion end FilesLogic

                /// Call WCF to Get all Workflow Tasks Actions fields for Add View
                #region Call WCF to Get all Workflow Tasks Actions fields for saving View
                object insertWorkflowActionValues = Models.WorkflowModel.InsertWorkflowActionValues(FormObjects, FileObjects, dealID, Convert.ToInt32(userID));
                if (Convert.ToInt32(insertWorkflowActionValues) > 0)
                {
                    TempData["ToasterWorflowUser"] = Notifications.GetToastrMessage(Enums.MessageType.success.ToString(), "Workflow has been successfully added");
                }

                #endregion end call WCF to Get all Workflow Tasks Actions fields for saving View

                return Content(TaskID.ToString());
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Uploads file
        /// </summary>
        /// <returns></returns>
        //[HttpPost]
        //public bool FileUploader(HttpPostedFileBase attachedFile)
        //{

        //    try
        //    {

        //        #region FilesLogic
        //        byte[] UploadedDoc = null;
        //        var FileObjects = new Dictionary<string, dynamic>();


        //        if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
        //        {
        //            /// Get UI element name
        //            string name = Request.Files.AllKeys.FirstOrDefault().ToString();

        //            for (int i = 0; i < Request.Files.Count; i++)
        //            {
        //                HttpPostedFileBase postedfile = Request.Files[i];
        //                UploadedDoc = new byte[postedfile.ContentLength];
        //                string uploadedFileName = postedfile.FileName.Substring(postedfile.FileName.LastIndexOf('\\') + 1);
        //                string[] Extention = uploadedFileName.Split('.');
        //                //FormObjects.Add("FileExtension", Extention[1]);

        //                postedfile.InputStream.Position = 0;
        //                postedfile.InputStream.Read(UploadedDoc, 0, postedfile.ContentLength);

        //                FileObjects.Add(name + ":" + i, Common.CFile.ConvertByteArrayToBase64String(UploadedDoc));
        //                FileObjects.Add(postedfile.FileName, Common.CFile.ConvertByteArrayToBase64String(UploadedDoc));
        //            }


        //        }
        //        #endregion end FilesLogic



        //        return true;
        //    }
        //    catch (Exception ex)
        //    {

        //        Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //        return false;
        //    }

        //}

        /// <summary>
        /// get edit fields 
        /// </summary>
        /// <returns></returns>
        public ActionResult PopupEditAction(int taskID, int workflowActionID,bool partial)
        {
            try
            {
                #region variables
                /// Log Start info
                Common.CLog.WriteLogInfo("Workflow", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                /// Initiate Validator
                ViewData["WorkflowTasksActionsEditResult"] = null;
                #region Get DealID from URL Parameter (Unique Reference)

                if (Request.UrlReferrer.Query == null)
                {
                    return Content(Enums.UniqueReference.Invalidreference.ToString());
                }
                int dealID = 0;
                string UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
                if (UniqueReference != Enums.UniqueReference.Invalid.ToString())
                {
                    object dealIDObject = Models.DealModel.GetDealIDByDealUniqueRef(UniqueReference); // We get the DealID from the UR
                    if (dealIDObject == null)
                    {
                        return Content(Enums.UniqueReference.Invalidreference.ToString());
                    }
                    dealID = Convert.ToInt32(dealIDObject);
                }
                else
                {
                    return Content(Enums.UniqueReference.Invalidreference.ToString());
                }

                #endregion Get DealID from URL Parameter (Unique Reference)
                #endregion end variables

                /// Call WCF to Get all Workflow Tasks Actions fields for edit View
                #region Call WCF to Get all Workflow Tasks Actions fields for edit View
                object GetWorkFlowActionsEditFields = Models.WorkflowModel.GetWorkFlowActionsEditFields(dealID, taskID, workflowActionID);
                ViewData["WorkflowTasksActionsEditResult"] = GetWorkFlowActionsEditFields;
                #endregion end Call WCF to Get all Workflow Tasks Actions fields for edit View

                ViewData["taskID"] = taskID;
                ViewData["workflowActionID"] = workflowActionID;

                if(partial)
                {
                    return View("PartialFileEdit");
                }

                return View("PopupEditAction");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Save changes to DB (Edit Action)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveWorkflowActionValues()
        {

            try
            {
                #region variables

                #region Get DealID from URL Parameter (Unique Reference)

                if (Request.UrlReferrer.Query == null)
                {
                    return Content(Enums.UniqueReference.Invalidreference.ToString());
                }
                int dealID = 0;
                string UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
                if (UniqueReference != Enums.UniqueReference.Invalid.ToString())
                {
                    object dealIDObject = Models.DealModel.GetDealIDByDealUniqueRef(UniqueReference); // We get the DealID from the UR
                    if (dealIDObject == null)
                    {
                        return Content(Enums.UniqueReference.Invalidreference.ToString());
                    }
                    dealID = Convert.ToInt32(dealIDObject);
                }
                else
                {
                    return Content(Enums.UniqueReference.Invalidreference.ToString());
                }
                #endregion Get DealID from URL Parameter (Unique Reference)

                /// Log Start info
                Common.CLog.WriteLogInfo("Dashboard", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                var FormObjects = new Dictionary<string, dynamic>();
                var FileObjects = new Dictionary<string, dynamic>();
                var TaskID = "";

                var dataSerialized = Request.Form["formDataValues"].ToString();
                FormObjects = Common.CDynamicSqlRow.DecodeSerialize(dataSerialized); //split the string and add it to dictionary
                //get taskID from FormObjects
                foreach (var taskIDCheck in FormObjects)
                {
                    if (taskIDCheck.Key.Contains("taskID"))
                    {
                        TaskID = taskIDCheck.Value; ;
                    }
                }
                //end of get task id from FormObjects
                /// Initiate Validator            
                //get userid by AspNetUsersId"
                string aspNetUsersId = HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value;
                object userID = Models.UserModel.GetUserIDByAspNetID(aspNetUsersId);
                // end of get userid by AspNetUsersId"
                #endregion variables


                #region FilesLogic
                byte[] UploadedDoc = null;
                if (Request.Files["attachedFile"] != null)
                {
                    /// Get UI element name
                    HttpPostedFileBase postedfile = Request.Files["attachedFile"];
                    var postedfileUIName = Request.Form["attachedFileUIName"];

                    /// Add files to collection to insert in Transaction details table
                    FormObjects.Add(postedfileUIName, postedfile.FileName);

                    if (postedfile != null && postedfile.ContentLength > 0)
                    {
                        UploadedDoc = new byte[postedfile.ContentLength];
                        string uploadedFileName = postedfile.FileName.Substring(postedfile.FileName.LastIndexOf('\\') + 1);
                        string[] Extention = uploadedFileName.Split('.');

                        postedfile.InputStream.Position = 0;
                        postedfile.InputStream.Read(UploadedDoc, 0, postedfile.ContentLength);

                        FileObjects.Add(uploadedFileName, UploadedDoc);
                    }
                }
                #endregion end FilesLogic

                /// Call WCF to Get all Workflow Tasks Actions fields for saving View
                #region Call WCF to Get all Workflow Tasks Actions fields for saving View
                object saveWorkflowActionValues = Models.WorkflowModel.SaveWorkflowActionValues(FormObjects, FileObjects, dealID, Convert.ToInt32(userID));
                #endregion end call WCF to Get all Workflow Tasks Actions fields for saving View

                if (Convert.ToInt32(saveWorkflowActionValues) > 0)
                {
                    TempData["ToasterWorflowUser"] = Notifications.GetToastrMessage(Enums.MessageType.success.ToString(), "Workflow has been successfully updated");
                }

                return Content(TaskID.ToString());
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Get uploaded files from filesystem
        /// </summary>
        /// <param name="transactionID"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public ActionResult FileDownload(int transactionID, string filename)
        {
            try
            {
                // Get File from Filesystem
                byte[] filecontent = Models.WorkflowModel.GetUploadedFile(transactionID, filename);

                return File(filecontent, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// return page partial sub mneu
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Submenu()
        {
            try
            {
                return View("PartialSubMenuSideBar");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// call function to display inside div
        /// </summary>
        /// <param name="TaskID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DivDataWorkflow(string TaskID)
        {
            try
            {
                #region Get DealID from URL Parameter (Unique Reference)

                if (Request.UrlReferrer.Query == null)
                {
                    return Content(Enums.UniqueReference.Invalidreference.ToString());
                }
                int dealID = 0;
                string UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
                if (UniqueReference != Enums.UniqueReference.Invalid.ToString())
                {
                    object dealIDObject = Models.DealModel.GetDealIDByDealUniqueRef(UniqueReference); // We get the DealID from the UR
                    if (dealIDObject == null)
                    {
                        return Content(Enums.UniqueReference.Invalidreference.ToString());
                    }
                    dealID = Convert.ToInt32(dealIDObject);
                }
                else
                {
                    return Content(Enums.UniqueReference.Invalidreference.ToString());
                }

                #endregion Get DealID from URL Parameter (Unique Reference)

                #region Call WCF to get workflow tasks depending on workflow ID
                object WorkflowTasksList = Models.WorkflowModel.GetWorkflow(dealID);
                ViewData["WorkflowTasksList"] = WorkflowTasksList;
                #endregion Call WCF to get workflow tasks depending on workflow ID

                object WorkflowTasksActionsListModel = Models.WorkflowModel.GetWorkFlowTasksActions(Convert.ToInt32(TaskID), dealID);
                ViewData["WorkflowTasksActionsList"] = WorkflowTasksActionsListModel;

                ////set default Task ID
                ViewData["SelectedTaskID"] = TaskID;

                return View("PartialWorkflowActions");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// remove images 
        /// </summary>
        /// <param name="transactionDetailID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RemoveTransactionImg(string transactionDetailIDGUID, string taskID, string workflowActionID)
        {
            Guid transactionDetailID = Guid.Empty;
            try
            {
                #region Variables
                ///result varianle if saved
                var participantResult = "";
                // declare variable to check if transaction id existed, redirect to page
                string resultWorkflowList = null;
                #endregion end vaiables

                transactionDetailID = new Guid(transactionDetailIDGUID.Trim());
                int transactionDetailImgID = Models.WorkflowModel.GetTransactionDetailIDByTransactionDetailGUID(transactionDetailID);

                //transaction details values 
                TransactionDetail transactionDetail = new TransactionDetail();
                transactionDetail.TransactionDetailID =transactionDetailImgID;
                transactionDetail.Deleted = true;

                #region remove transactionDetailID set delete to 1
                string trasactionDetailDel = Models.WorkflowModel.RemoveTransactionImg(transactionDetail);
                participantResult = trasactionDetailDel.ToString();
                #endregion remove transactionDetailID set delete to 1

                #region Call WCF to Get all Workflow Tasks Actions fields for Add View and check if ever exist transaction id 

                #region Get DealID from URL Parameter (Unique Reference)

                if (Request.UrlReferrer.Query == null)
                {
                    return Content(Enums.UniqueReference.Invalidreference.ToString());
                }
                int dealID = 0;
                string UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
                if (UniqueReference != Enums.UniqueReference.Invalid.ToString())
                {
                    object dealIDObject = Models.DealModel.GetDealIDByDealUniqueRef(UniqueReference); // We get the DealID from the UR
                    if (dealIDObject == null)
                    {
                        return Content(Enums.UniqueReference.Invalidreference.ToString());
                    }
                    dealID = Convert.ToInt32(dealIDObject);
                }
                else
                {
                    return Content(Enums.UniqueReference.Invalidreference.ToString());
                }

                #endregion Get DealID from URL Parameter (Unique Reference)

                #region Call WCF to Get all Workflow Tasks Actions fields for edit View
                object GetWorkFlowActionsEditFields = Models.WorkflowModel.GetWorkFlowActionsEditFields(dealID, Convert.ToInt32(taskID), Convert.ToInt32(workflowActionID));
                #endregion end Call WCF to Get all Workflow Tasks Actions fields for edit View
                //check if ever exist transaction id 
                foreach (var getDetails in GetWorkFlowActionsEditFields as IEnumerable<dynamic>)
                {
                    foreach(var getDetailsKeyValue in getDetails as Dictionary<string,dynamic>)
                    {
                        if(getDetailsKeyValue.Key.Contains("TransactionID"))
                        {
                            if(getDetailsKeyValue.Value != null)
                            {
                                resultWorkflowList = getDetailsKeyValue.Value.ToString();
                                break;
                            }
                        }
                    }
                    if (resultWorkflowList != null)
                    { break; }
                    else { continue; }
                }
                #endregion Call WCF to Get all Workflow Tasks Actions fields for Add View and check if ever exist transaction id 

                if (resultWorkflowList == null)
                {
                    return RedirectToAction("PopupAddAction", "Workflow", new { taskID = Convert.ToInt32(taskID), workflowActionID = Convert.ToInt32(workflowActionID) }); //to be done
                }
                return RedirectToAction("PopupEditAction", "Workflow", new { taskID = Convert.ToInt32(taskID), workflowActionID =Convert.ToInt32(workflowActionID), partial = true  }); //to be done
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }
    }
}