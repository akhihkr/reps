using Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Configuration;

namespace REPS.UI.Controllers
{
    [TokenAuthorizeAttribute]
    public class DocumentsController : Controller
    {
        // GET: Documents
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

                /// Log Start info
                Common.CLog.WriteLogInfo("Document Generations", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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

                // Get Document list depending on workflow
                ViewData["DocumentTypeList"] = Models.DocumentModel.GetDocumentType(taskWorkflowID);
                // End OF Get Document list depending on workflow

                //Get Document Template list depending on workflow
                ViewData["DocumentTemplateList"] = Models.DocumentModel.GetDocumentTemplateDetails(taskWorkflowID, dealID);
                //End of Get Document Template list depending on workflow

                return View();
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
        [HttpPost]
        public ActionResult PopupUploadDocument(int objectTypeID, int workflowStepId)
        {
            try
            {
                ViewData["DocumentTemplateID"] = objectTypeID;
                ViewData["workflowStepId"] = workflowStepId;
                return View("PopupUploadDocument");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// return to partial
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ReturnToPartialDocument()
        {
            try
            {
                #region get deal ID by url
                int dealID = 0;
                string UniqueReference = string.Empty;
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
                }
                else
                {
                    #region Get DealID from URL Parameter (Unique Reference)

                    UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
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
                }
                #endregion end of  get deal ID by url

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

                // Get Document list depending on workflow
                ViewData["DocumentTypeList"] = Models.DocumentModel.GetDocumentType(taskWorkflowID);
                // End OF Get Document list depending on workflow

                //Get Document Template list depending on workflow
                ViewData["DocumentTemplateList"] = Models.DocumentModel.GetDocumentTemplateDetails(taskWorkflowID, dealID);
                //End of Get Document Template list depending on workflow

                return View("PartialDocument");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Upload Document ( Standard or Signed )
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadDocument()
        {
            /// Call WCF to upload documents
            try
            {
                /// Log Start info
                Common.CLog.WriteLogInfo("Document", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                /// Initiate Validator
                ViewData["UploadError"] = null;

                var FormObjects = new Dictionary<string, dynamic>();
                //Copy posted values
                var dataSerialized = Request.Form["formDataValues"].ToString();
                FormObjects = Common.CDynamicSqlRow.DecodeSerialize(dataSerialized);

                #region get deal id by url
                int dealID = 0;
                string UniqueReference = string.Empty;
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
                }
                else
                {
                    #region Get DealID from URL Parameter (Unique Reference)

                    UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
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
                }
                #endregion end of get deal id by url

                //get user by asp net id
                var userID = Models.UserModel.GetUserIDByAspNetID(HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value.ToString());
                // end of get user by asp net id

                // Add DealID and UserID
                FormObjects.Add("dealID", dealID);
                FormObjects.Add("userID", Convert.ToInt32(userID));

                // Get uploaded file
                byte[] UploadedDoc = null;
                object UploadDocumentToFolder = 0;

                if (Request.Files["attachedFile"] != null && Request.Files["attachedFile"].FileName.Length <= Convert.ToInt32(Enums.Document.Length))
                {
                    HttpPostedFileBase postedfile = Request.Files["attachedFile"];
                    var postedfileUIName = Request.Form["attachedFileUIName"]; /// Get UI element name

                    //var fileContent = postedfile.;
                    if (postedfile != null && postedfile.ContentLength > 0)
                    {
                        UploadedDoc = new byte[postedfile.ContentLength];
                        string uploadedFileName = postedfile.FileName.Substring(postedfile.FileName.LastIndexOf('\\') + 1);
                        string[] Extention = uploadedFileName.Split('.');
                        FormObjects.Add("FileExtension", Extention[1]);
                        postedfile.InputStream.Position = 0;
                        postedfile.InputStream.Read(UploadedDoc, 0, postedfile.ContentLength);

                        if ((FormObjects["UploadOption"]).ToString().Contains(Enums.UploadType.standard.ToString()))
                        {
                            uploadedFileName = Extention[0] + "_Standard." + Extention[1];  // Add Signed to prevent file overwriting
                        }
                        else if ((FormObjects["UploadOption"]).ToString().Contains(Enums.UploadType.signed.ToString()))
                        {
                            uploadedFileName = Extention[0] + "_Signed." + Extention[1];  // Add Signed to prevent file overwriting
                        }

                        FormObjects.Add("Filename", uploadedFileName);
                        FormObjects.Add("UploadedFile", UploadedDoc);

                        UploadDocumentToFolder = Models.DocumentModel.UploadDocumentToFolder(FormObjects);
                    }
                }

                
                // If documents were generated, reload view else show error message
                if (Convert.ToInt32(UploadDocumentToFolder) > 0)
                {
                    TempData["ToasterMsg"] = Notifications.GetToastrMessage(Enums.MessageType.success.ToString(), "Document successfully uploaded");
                }
                else
                {
                    TempData["ToasterMsg"] = Notifications.GetToastrMessage(Enums.MessageType.error.ToString(), "Document could not be uploaded");
                }
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

                // Get Document list depending on workflow
                ViewData["DocumentTypeList"] = Models.DocumentModel.GetDocumentType(taskWorkflowID);
                // End OF Get Document list depending on workflow

                //Get Document Template list depending on workflow
                ViewData["DocumentTemplateList"] = Models.DocumentModel.GetDocumentTemplateDetails(taskWorkflowID, dealID);
                //End of Get Document Template list depending on workflow

                return View("PartialDocument");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Download Signed file
        /// </summary>
        /// <returns></returns>
        public ActionResult DownloadSignedFile(int documentID, string filename)
        {
            try
            {
                /// Log Start info
                Common.CLog.WriteLogInfo("Document", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                /// Initiate Validator
                ViewData["DocumentTypeList"] = null;
                ViewData["DocumentTemplateList"] = null;

                #region Get DealID from URL Parameter (Unique Reference)

                if (Request.UrlReferrer.Query == null)
                {
                    return Content(Enums.UniqueReference.Invalidreference.ToString());
                }
                int dealID = 0;
                string UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
                if (UniqueReference != Enums.UniqueReference.Invalid.ToString())
                {
                    object dealIDObject = Models.DealModel.GetDealIDByDealUniqueRef(UniqueReference); // We get the DealID from the URL
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

                // Get File from Filesystem
                byte[] filecontent = Models.DocumentModel.GetSignedBinaryDocumentFilename(documentID, filename, dealID);

                return File(filecontent, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
            }
            catch (Exception ex)
            {

                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Download Signed file
        /// </summary>
        /// <returns></returns>
        public ActionResult DownloadSigned(int documentID, string filename)
        {
            try
            {
                /// Log Start info
                Common.CLog.WriteLogInfo("Document", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                /// Initiate Validator
                ViewData["DocumentTypeList"] = null;
                ViewData["DocumentTemplateList"] = null;
                var fileContent = "";
                var templateDisplayName = "";

                int dealID = 0;
                string UniqueReference = string.Empty;
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
                }


                /// Call WCF to get Document list depending on workflow
                object getSignedBinaryDocument = Models.DocumentModel.GetSignedBinaryDocument(documentID, dealID);
                if (getSignedBinaryDocument.ToString() != null)
                {
                    foreach (var fileName in getSignedBinaryDocument as dynamic)
                    {
                        if (fileName.Key == "FileContent")
                        {
                            fileContent = fileName.Value;
                        }
                        if (fileName.Key == "TemplateDisplayName")
                        {
                            templateDisplayName = fileName.Value;
                        }
                    }
                    return File(fileContent, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
                }
                else
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
                    //end of variables to get workflow 

                    // Get Document list depending on workflow
                    ViewData["DocumentTypeList"] = Models.DocumentModel.GetDocumentType(taskWorkflowID);
                    // End OF Get Document list depending on workflow

                    //Get Document Template list depending on workflow
                    ViewData["DocumentTemplateList"] = Models.DocumentModel.GetDocumentTemplateDetails(taskWorkflowID, dealID);
                    //End of Get Document Template list depending on workflow

                    return View("PartialDocument");
                }
            }
            catch (Exception ex)
            {

                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Download Standard/Generated file
        /// </summary>
        /// <returns></returns>
        public ActionResult DownloadFile(int documentID, string filename)
        {
            try
            {
                /// Log Start info
                Common.CLog.WriteLogInfo("Document", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                /// Initiate Validator
                ViewData["DocumentTypeList"] = null;
                ViewData["DocumentTemplateList"] = null;

                #region Get DealID from URL Parameter (Unique Reference)

                if (Request.UrlReferrer.Query == null)
                {
                    return Content(Enums.UniqueReference.Invalidreference.ToString());
                }
                int dealID = 0;
                string UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
                if (UniqueReference != Enums.UniqueReference.Invalid.ToString())
                {
                    object dealIDObject = Models.DealModel.GetDealIDByDealUniqueRef(UniqueReference); // We get the DealID from the URL
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

                // Get File from Filesystem
                byte[] filecontent = Models.DocumentModel.GetSignedBinaryDocumentFilename(documentID, filename, dealID);

                return File(filecontent, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Download Static Document Template
        /// </summary>
        /// <returns></returns>
        public ActionResult DownloadStaticFile(int documentTemplateID)
        {
            try
            {
                /// Log Start info
                Common.CLog.WriteLogInfo("Document", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                /// Initiate Validator
                ViewData["DocumentTypeList"] = null;
                ViewData["DocumentTemplateList"] = null;

                string fileContent = null;
                var templateDisplayName = "";
                #region Get DealID from URL Parameter (Unique Reference)

                if (Request.UrlReferrer.Query == null)
                {
                    return Content(Enums.UniqueReference.Invalidreference.ToString());
                }
                int dealID = 0;
                string UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
                if (UniqueReference != Enums.UniqueReference.Invalid.ToString())
                {
                    object dealIDObject = Models.DealModel.GetDealIDByDealUniqueRef(UniqueReference); // We get the DealID from the URL
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
                /// Call WCF to get Document list depending on workflow
                object getStaticDocumentTemplate = Models.DocumentModel.GetStaticDocumentTemplate(documentTemplateID);
                if (getStaticDocumentTemplate.ToString() != null)
                {
                    //return File(Common.CFile.FromBase64StringToByteArray(getStaticDocumentTemplate["FileContent"]), System.Net.Mime.MediaTypeNames.Application.Octet, getStaticDocumentTemplate["TemplateDisplayName"]);
                    foreach (var fileName in getStaticDocumentTemplate as dynamic)
                    {
                        if (fileName.Key == "FileContent")
                        {
                            fileContent = fileName.Value;
                        }
                        if (fileName.Key == "TemplateDisplayName")
                        {
                            templateDisplayName = fileName.Value;
                        }
                    }
                    return File(Common.CFile.FromBase64StringToByteArray(fileContent), System.Net.Mime.MediaTypeNames.Application.Octet, templateDisplayName);
                }
                else
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
                    //end of variables to get workflow 

                    // Get Document list depending on workflow
                    ViewData["DocumentTypeList"] = Models.DocumentModel.GetDocumentType(taskWorkflowID);
                    // End OF Get Document list depending on workflow

                    //Get Document Template list depending on workflow
                    ViewData["DocumentTemplateList"] = Models.DocumentModel.GetDocumentTemplateDetails(taskWorkflowID, dealID);
                    //End of Get Document Template list depending on workflow

                    return View("PartialDocument");
                }
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Generate Documents
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GenerateDocuments(string objectDocumentID, string format)
        {
            /// Call WCF to generate documents
            try
            {
                /// Log Start info
                Common.CLog.WriteLogInfo("Document Upload File : PDF/DOCX", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                /// Initiate Validator
                ViewData["GenerationError"] = null;
                List<object> DocumentListID = new List<object>();
                string[] parametersplited; 
                var FormObjects = new Dictionary<string, object>();

                if (objectDocumentID.ToString().Contains(","))
                {
                    parametersplited = objectDocumentID.Split(',');
                    foreach (var paramCheck in parametersplited)
                    {
                        DocumentListID.Add(paramCheck);
                    }
                }
                else
                {
                    DocumentListID.Add(objectDocumentID); 
                }


                #region get deal id by url
                int dealID = 0;
                string UniqueReference = string.Empty;
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
                }
                else
                {
                    #region Get DealID from URL Parameter (Unique Reference)

                    UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
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
                }
                #endregion end of get deal id by url

                #region get user by asp net id
                var userID = Models.UserModel.GetUserIDByAspNetID(HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value.ToString());
                #endregion end of get user by asp net id

                #region get workflowID by REPS_TaskGUID
                string taskGUID = HttpContext.Request.Cookies["REPS_TaskGUID"].Value;
                int dealProcessTaskID = 0;
                int dealWorkflowID = 0;
                object taskIDWorkflowIDResult = Models.WorkflowModel.GetWorkflowIDTaskID(taskGUID);
                foreach (var resultID in taskIDWorkflowIDResult as Dictionary<string, dynamic>)
                {
                    if (resultID.Key == "TaskID")
                    {
                        dealProcessTaskID = resultID.Value;
                    }
                    else if (resultID.Key == "TaskWorkflowID")
                    {
                        dealWorkflowID = resultID.Value;
                    }
                }
                #endregion end of get workflowID by REPS_TaskGUID

                #region call wcf to get Entityid by REPS_EntityGUID
                string entityGUID = HttpContext.Request.Cookies["REPS_EntityGUID"].Value;
                int entityID = Convert.ToInt32(Models.EntityModel.GetEntityIDByEntityGUID(entityGUID));
                #endregion end of call wcf to get Entityid by REPS_EntityGUID

                // Add DealID and UserID
                FormObjects.Add("dealID", dealID);
                FormObjects.Add("userID", Convert.ToInt32(userID));
                FormObjects.Add("entityID", entityID);
                FormObjects.Add("WorkflowID", dealWorkflowID);
                FormObjects.Add("docType", format == "null" ? null : format);
                /// Call WCF to get Document list depending on workflow
                object generateDocuments = Models.DocumentModel.GenerateDocuments(FormObjects, DocumentListID);

                #region Variables to display error message
                string MessageSuccess = null;
                string MessageFailure = null;
                #endregion Variables to display error message

                #region setting error messsage for each toast
                foreach (var resultID in generateDocuments as Dictionary<string, dynamic>)
                {
                    string GeneratedError = Convert.ToString(resultID.Value);
                    string doc = Convert.ToString(resultID.Value);
                       
                    if (GeneratedError.Equals("0"))
                        {
                            MessageSuccess = MessageSuccess + resultID.Key + ": " + " successfully generated" + ", ";
                        }
                    else if (GeneratedError.Equals("-1"))
                        {
                            MessageFailure = MessageFailure + " Document(s) could not be generated.Some information is missing" + ", ";
                        }
                    else if (GeneratedError.Equals("-3"))
                        {
                            MessageFailure = MessageFailure + " Document(s) could not be generated. Document Template is missing" + ", ";
                        }
                    else if (GeneratedError.Contains("Param"))
                        {
                            MessageFailure = MessageFailure + resultID.Key + ": " + "Add following to proceed: " + doc.Substring(6) + ", ";
                        }
                    else
                        {
                            MessageFailure = MessageFailure + resultID.Key + ": " + "Document(s) could not be generated. Document Template is missing','Error" + ", ";
                        }
                }
                #endregion setting error messsage for each toast 

                #region Display Success Message
                if (MessageSuccess != null)
                {
                    MessageSuccess = MessageSuccess.Replace("@", "@" + Environment.NewLine);
                    MessageSuccess = MessageSuccess = MessageSuccess.Remove(MessageSuccess.Length - 2);
                    ViewData["GenerationError"] = false;
                    TempData["ToasterMsg"] = Notifications.GetToastrMessage(Enums.MessageType.success.ToString(), MessageSuccess);
                }
                #endregion Display Success Message

                #region Display Failure Message
                if (MessageFailure != null)
                {
                    ViewData["GenerationError"] = true;
                    MessageFailure = MessageFailure.Replace("@", "@" + System.Environment.NewLine);
                    MessageFailure = MessageFailure = MessageFailure.Remove(MessageFailure.Length - 2);
                    TempData["ToasterMsg1"] = Notifications.GetToastrMessage(Enums.MessageType.error.ToString(), MessageFailure);
                }
                #endregion Display Failure Message

                //return RedirectToAction("Index", "Documents");
                // Get Document list depending on workflow
                ViewData["DocumentTypeList"] = Models.DocumentModel.GetDocumentType(dealWorkflowID);
                // End OF Get Document list depending on workflow

                //Get Document Template list depending on workflow
                ViewData["DocumentTemplateList"] = Models.DocumentModel.GetDocumentTemplateDetails(dealWorkflowID, dealID);
                //End of Get Document Template list depending on workflow

                return View("PartialDocument");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// call popup to display doc options
        /// </summary>
        /// <param name="objectDocumentID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PopupDocumentExtention(string objectDocumentID)
        {
            try
            {
                #region variables
                /// Log Start info
                Common.CLog.WriteLogInfo("Document Upload File : PDF/DOCX", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                #endregion end variables

                if (objectDocumentID != null)
                {
                    ViewData["objectDocumentID"] = objectDocumentID;
                }
                return View("PopupGenerateDocumentExtention");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }
    }
}