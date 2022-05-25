using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Global;
using REPS.DATA.Entity;

namespace REPS.UI.Controllers
{
    public class AdminDocumentsController : Controller
    {
        /// <summary>
        /// get details for index
        /// </summary>
        /// <param name="partial"></param>
        /// <returns></returns>
        public ActionResult Index(bool partial = false , bool responseResult = false)
        {
            if (Request.IsAjaxRequest())
            {
                ViewBag.PageLayoutPath = null;
            }
            else
            {
                ViewBag.PageLayoutPath = Global.Constants.MainLayoutPath;
            }

            ViewData["SelectedLayout"] = "Admin";


            //variables to get workflow 
            string taskGUID = HttpContext.Request.Cookies["REPS_TaskGUID"].Value;
            int taskID = 0;
            int taskWorkflowID = 0;
            object taskIDWorkflowIDResult = Models.WorkflowModel.GetWorkflowIDTaskID(taskGUID);
            foreach (var resultID in taskIDWorkflowIDResult as Dictionary<string, dynamic>)
            {
                if (resultID.Key == "TaskID")
                {
                    taskID = resultID.Value;
                }
                else if (resultID.Key == "TaskWorkflowID")
                {
                    taskWorkflowID = resultID.Value;
                }
            }
            //end of variables to get workflow 

            #region Set current workflow
            /// Call WCF to get Workflow Task Name
            object workflowName = Models.WorkflowModel.GetCurrentWorkflowsDetails(taskID, taskWorkflowID);
            ViewData["CurrentDealWorkflow"] = workflowName;
            #endregion end of Set current workflow

            #region get workflows
            /// Call WCF to get all workflows
            object WorkflowsList = Models.WorkflowModel.GetWorkflowsList((int)Global.Enums.Wokflow.Process);
            ViewData["WorkflowsList"] = WorkflowsList;
            #endregion end of get workflows

            /// Log Start info
            Common.CLog.WriteLogInfo("Document Generations", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            // Get Document list depending on workflow
            ViewData["DocumentCategoryList"] = Models.DocumentModel.GetAdminDocumentCategoryList();
            // End OF Get Document list depending on workflow

            //Get Document Template list depending on workflow
            ViewData["DocumentTemplateList"] = Models.DocumentModel.GetAdminDocumentTemplateList();
            //End of Get Document Template list depending on workflow


            if (partial)
            {
                if(responseResult)
                {
                    Response.Write("<div id='my-div' data-info=" + (int)Enums.ValidationCheck.reusltOutput + "></div>");
                }
                return PartialView("PartialAdminDocumentsList");
            }
            return View();
        }

        /// <summary>
        /// prompt add category popup
        /// </summary>
        /// <returns></returns>
        public ActionResult PopupAddCategory()
        {
            try
            {
                return View("PopupAddCategory");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// add document category
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddDocumentCategory()
        {
            try
            {
                #region variables
                /// Log Start info
                Common.CLog.WriteLogInfo("Document Admin : Add Document Category", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                var FormObjects = new Dictionary<string, dynamic>();

                /// Initiate Validator
                int UserID = Convert.ToInt32(ControllerContext.HttpContext.Session["UserID"]);
                #endregion end variables

                ///Copy posted values
                Request.Form.CopyTo(FormObjects);

                object resultsave = Models.DocumentModel.AddDocumentCategory(FormObjects);
                if (Convert.ToInt32(resultsave) > 0)
                {
                    TempData["ToasterMsg"] = Notifications.GetToastrMessage(Enums.MessageType.success.ToString(), "Document Category has been successfully added");
                }
                return RedirectToAction("Index", new { partial = true });
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// general popup - New Document Template Fields
        /// </summary>
        /// <param name="documentTypeID"></param>
        /// <returns></returns>
        public ActionResult NewDocumentTemplate(int objectTypeID)
        {
            try
            {
                ViewData["DocumentTypeID"] = objectTypeID;
                return View("PopupAddTemplate");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// add template to db
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddDocumentTemplate()
        {
            try
            {
                #region variables
                /// Log Start info
                Common.CLog.WriteLogInfo("Admin Document : Saving Template", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                var FormObjects = new Dictionary<string, dynamic>();

                /// Initiate Validator
                //get user by asp net id
                var UserID = Models.UserModel.GetUserIDByAspNetID(HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value.ToString());
                // end of get user by asp net id
                int DocumentTypeID = 0;
                #endregion end variables

                ///Copy posted values
                var dataSerialized = Request.Form["formDataValues"].ToString();
                FormObjects = Common.CDynamicSqlRow.DecodeSerialize(dataSerialized);
                FormObjects.Add("UserID", Convert.ToInt32(UserID));

                //get DocumentTypeID from FormObjects
                foreach (var documentTypeIDs in FormObjects)
                {
                    if (documentTypeIDs.Key.Contains("documentTypeID"))
                    {
                        DocumentTypeID = Convert.ToInt32(documentTypeIDs.Value);
                    }
                }
                //end of get DocumentTypeID from FormObjects


                /// Get File
                byte[] UploadedDoc = null;
                if (Request.Files["attachedFile"] != null)
                {
                    HttpPostedFileBase postedfile = Request.Files["attachedFile"];
                    //var filename = Request.Form["attachedFileUIName"]; /// Get UI element name

                    //var fileContent = postedfile.;
                    if (postedfile != null && postedfile.ContentLength > 0)
                    {
                        UploadedDoc = new byte[postedfile.ContentLength];
                        string uploadedFileName = postedfile.FileName.Substring(postedfile.FileName.LastIndexOf('\\') + 1);
                        string[] Extention = uploadedFileName.Split('.');


                        postedfile.InputStream.Position = 0;
                        postedfile.InputStream.Read(UploadedDoc, 0, postedfile.ContentLength);

                        /// Add files to collection to insert in Transaction details table
                        FormObjects.Add("filename", postedfile.FileName);
                        FormObjects.Add("MimeType", '.' + Extention[1]);
                    }
                }
 
                /// Add Document Template
                object resultsave = Models.DocumentModel.AddDocumentTemplate(FormObjects, UploadedDoc, DocumentTypeID);

                return RedirectToAction("Index", new { partial = true });
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// edit template - general pop up
        /// </summary>
        /// <param name="documentTemplateID"></param>
        /// <returns></returns>
        public ActionResult PopupEditTemplate(int objectTypeID)
        {
            try
            {
                #region Log Start info
                Common.CLog.WriteLogInfo("Admin Document:Get Edit Template Details", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                #endregion end Log Start info
                ViewData["DocumentTemplateDetails"] = Models.DocumentModel.GetEditDocumentTemplateFields(objectTypeID);
                ViewData["DocumentTemplateID"] = objectTypeID;

                return View("PopupEditTemplate");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// reset template delete to 1(true) from db
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RemoveTemplate(int id)
        {
            try
            {
                #region Log Start info
                Common.CLog.WriteLogInfo("Admin Document: Remove Template", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                #endregion end Log Start info

                object resultsave = Models.DocumentModel.RemoveDocumentTemplate(id);

                Guid guidOutput;
                bool isGuid = Guid.TryParse(resultsave.ToString(), out guidOutput);
                bool responseResult = false;
                if (isGuid)
                {
                    responseResult = true;
                }
                return RedirectToAction("Index", new { partial = true , responseResult = responseResult });
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// edit template
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditDocumentTemplate()
        {
            try
            {
                #region variables
                /// Log Start info
                Common.CLog.WriteLogInfo("Admin Document : Edit Template", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                var FormObjects = new Dictionary<string, dynamic>();

                //get user by asp net id
                var UserID = Models.UserModel.GetUserIDByAspNetID(HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value.ToString());
                // end of get user by asp net id
                int DocumentTemplateID = 0;
                ///Copy posted values
                var dataSerialized = Request.Form["formDataValues"].ToString();
                FormObjects = Common.CDynamicSqlRow.DecodeSerialize(dataSerialized);
                FormObjects.Add("UserID", Convert.ToInt32(UserID));

                //get DocumentTypeID from FormObjects
                foreach (var documentIDs in FormObjects)
                {
                    if (documentIDs.Key.Contains("DocumentTemplateID"))
                    {
                        DocumentTemplateID = Convert.ToInt32(documentIDs.Value);
                    }
                }
                //end of get DocumentTypeID from FormObjects
                #endregion end variables

                /// Get File
                byte[] UploadedDoc = null;
                if (Request.Files["attachedFile"] != null)
                {
                    HttpPostedFileBase postedfile = Request.Files["attachedFile"];
                    //var filename = Request.Form["attachedFileUIName"]; /// Get UI element name

                    //var fileContent = postedfile.;
                    if (postedfile != null && postedfile.ContentLength > 0)
                    {
                        UploadedDoc = new byte[postedfile.ContentLength];
                        string uploadedFileName = postedfile.FileName.Substring(postedfile.FileName.LastIndexOf('\\') + 1);
                        string[] Extention = uploadedFileName.Split('.');

                        postedfile.InputStream.Position = 0;
                        postedfile.InputStream.Read(UploadedDoc, 0, postedfile.ContentLength);

                        /// Add files to collection to insert in Transaction details table
                        FormObjects.Add("filename", postedfile.FileName);
                        FormObjects.Add("MimeType", '.' + Extention[1]);
                    }
                }
                //if (Request.Files[0] != null && Request.Files[0].ContentLength > 0)
                //{
                //    UploadedDoc = new byte[Request.Files[0].ContentLength];
                //    string uploadedFileName = Request.Files[0].FileName.Substring(Request.Files[0].FileName.LastIndexOf('\\') + 1);
                //    string[] Extension = uploadedFileName.Split('.');

                //    Request.Files[0].InputStream.Position = 0;
                //    Request.Files[0].InputStream.Read(UploadedDoc, 0, Request.Files[0].ContentLength);
                //    FormObjects.Add("filename", uploadedFileName);
                //    FormObjects.Add("MimeType", '.' + Extension[1]);
                //}

                /// Add Document Template
                object resultsave = Models.DocumentModel.SaveDocumentTemplate(FormObjects, UploadedDoc, DocumentTemplateID);

                TempData["ToasterMsg"] = Notifications.GetToastrMessage(Enums.MessageType.success.ToString(), "Document Template has been successfully updated");

                ViewData["DocumentTemplateDetails"] = Models.DocumentModel.GetEditDocumentTemplateFields(DocumentTemplateID);
                //return RedirectToAction("Index", "AdminDocuments");
                return RedirectToAction("Index", new { partial = true });
                //return View("PopupEditTemplate");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// general popup - Worflow Document Template Fields
        /// </summary>
        /// <param name="selectedValueID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetSelectWorkflowTask( string selectedValueID)
        {
            try
            {
                #region Call WCF to get all Tasks list per workflowID
                ViewData["workflowTaskList"] = Models.WorkflowModel.GetWorkflowsList(Convert.ToInt32(selectedValueID)); // Call WCF to get all Tasks list per workflowID
                #endregion end of Call WCF to get all Tasks list per workflowID

                return PartialView("PartialWorkflowTasksActions");
                //return RedirectToAction("PopupAddTemplate", "AdminDocuments");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// get workflow task details if workflow task is selected
        /// </summary>
        /// <param name="selectedValue"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetWorkflowTaskList(string selectedValueID)
        {
            try
            {
                /// Call WCF to get workflow tasks actions depending on task ID
                #region Call WCF to get workflow tasks actions depending on task ID
                object WorkflowTasksActionsListModel = Models.WorkflowModel.GetWorkFlowTasksActionsList(Convert.ToInt32(selectedValueID));
                #endregion end of Call WCF to get workflow tasks actions depending on task ID
                ViewData["WorkflowTasksActionsList"] = WorkflowTasksActionsListModel;
                //return RedirectToAction("PopupAddTemplate", "AdminDocuments");
                return PartialView("PartialWorkflowTasksActions");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// display workflow task in another form
        /// </summary>
        /// <param name="objectTypeID"></param>
        /// <returns></returns>
        public ActionResult PopupAddWorkflowTask(int objectTypeID)
        {
            try
            {
                //variables to get workflow 
                string taskGUID = HttpContext.Request.Cookies["REPS_TaskGUID"].Value;
                int taskID = 0;
                int taskWorkflowID = 0;
                object taskIDWorkflowIDResult = Models.WorkflowModel.GetWorkflowIDTaskID(taskGUID);
                foreach (var resultID in taskIDWorkflowIDResult as Dictionary<string, dynamic>)
                {
                    if (resultID.Key == "TaskID")
                    {
                        taskID = resultID.Value;
                    }
                    else if (resultID.Key == "TaskWorkflowID")
                    {
                        taskWorkflowID = resultID.Value;
                    }
                }
                /// Call WCF to get all Tasks list per workflowID
                ViewData["workflowList"] = Models.WorkflowModel.GetWorkflowsList(taskWorkflowID);
                //end of variables to get workflow 

                #region get workflows
                /// Call WCF to get all workflows
                object WorkflowsList = Models.WorkflowModel.GetWorkflowsList((int)Global.Enums.Wokflow.Process);
                ViewData["WorkflowsList"] = WorkflowsList;
                #endregion end of get workflows

                #region Set current workflow
                /// Call WCF to get Workflow Task Name
                object workflowName = Models.WorkflowModel.GetCurrentWorkflowsDetails(taskID, taskWorkflowID);
                ViewData["CurrentDealWorkflow"] = workflowName;
                #endregion end of Set current workflow

                TempData["DocumentTypeID"] = objectTypeID;
                return View("PopupAddWorkflowTask");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// add template to db
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddWorkflowTask()
        {
            try
            {
                #region variables
                /// Log Start info
                Common.CLog.WriteLogInfo("Admin Document : Saving Workflow Step", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                /// Initiate Validator
                //get user by asp net id
                var UserID = Models.UserModel.GetUserIDByAspNetID(HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value.ToString());
                // end of get user by asp net id
                #endregion end variables

                ///Copy posted values
                DocumentWorkflow documentWorkflowTaskStep = new DocumentWorkflow();
                documentWorkflowTaskStep.DocumentVersionID = Convert.ToInt32(Request.Form["TemplateDocumentID"]);
                documentWorkflowTaskStep.WorkflowStepID = Convert.ToInt32(Request.Form["workflowTaskListID"]);
                documentWorkflowTaskStep.CreatedByUserID = Convert.ToInt32(UserID);

                /// Add Document Template
                //object resultsave = Models.DocumentModel.AddWorkflowTask(documentWorkflowTaskStep);

                TempData["ToasterMsg"] = Notifications.GetToastrMessage(Enums.MessageType.success.ToString(), "Document Workflow Step has been successfully added");
                return RedirectToAction("Index", "AdminDocuments");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Remove Document workflow from document workflow table
        /// </summary>
        /// <param name="documentTemplateID"></param>
        /// <param name="documentWorkflowID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RemoveAdminDocumentWorkflow(int? documentTemplateID, int? documentWorkflowID, int? documentTypeID)
        {
            try
            {
                #region set delete true in document workflow table if workflow existed with the same template id
                object resultsave = Models.DocumentModel.RemoveAdminDocumentWorkflow(null, documentWorkflowID);
                if (Convert.ToInt32(resultsave) > 0)
                {
                    TempData["ToasterMsg"] = Notifications.GetToastrMessage(Enums.MessageType.success.ToString(), "Document Workflow has been successfully removed");
                }
                #endregion end of set delete true in document workflow table if workflow existed with the same template id

                //get template details
                ViewData["DocumentTemplateDetails"] = Models.DocumentModel.GetEditDocumentTemplateFields(Convert.ToInt32(documentTemplateID));

                //get document workflow action result
                ViewData["GetDocumentWorkflowTaskStep"] = Models.DocumentModel.GetDocumentWorkflowTaskStep(Convert.ToInt32(documentTemplateID));

                return View("PopupEditDocumentWorkflowTemplate");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        ///  Get Document Template
        /// </summary>
        /// <param name="documentTemplateID"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public ActionResult DownloadTemplate(int documentTemplateID, string filename)
        {
            try
            {
                /// Log Start info
                Common.CLog.WriteLogInfo("Download File", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                IEnumerable<dynamic> FileResultObj = Models.DocumentModel.DownloadDocumentTemplate(documentTemplateID);

                byte[] TemplateFile = null;
                TemplateFile = Global.File.GetFile(FileResultObj.FirstOrDefault()["ID"], filename, (int)Enums.FolderPath.Templates);

                return File(TemplateFile, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return View("error");
            }
        }
    }
}