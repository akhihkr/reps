using Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using REPS.UI.EntityServiceReference;
using System.Configuration;
using System.Web.Script.Serialization;
using System.IO;
using System.Dynamic;

namespace REPS.UI.Controllers
{
    [TokenAuthorizeAttribute]
    public class CorrespondenceController : Controller
    {
        // GET: Correspondence
        public ActionResult Index(bool partial = false)
        {
            try
            {
                ViewData["SelectedTab"] = Enums.PageNames.CorrespondenceList.ToString();
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
                    if (Request["URef"] == null)
                    {
                        UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
                    }
                    else
                    {
                        UniqueReference = Common.CUniqueReference.GetUniqueReferenceNonAjaxRequest(Request["URef"]);
                    }
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

                //get userid
                string aspNetUsersId = HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value;
                object userID = Models.UserModel.GetUserIDByAspNetID(aspNetUsersId);
                //end of get user id

                object getCorrespondenceInfo = Models.CorrespondenceModel.GetCorrespondenceInfo(Convert.ToInt32(userID.ToString()), dealID);
                ViewData["CorrespondenceInfo"] = getCorrespondenceInfo; 

                if (partial)
                {
                    ViewBag.PageLayoutPath = null;
                    return PartialView("CorrespondenceList");
                }

                return View();
            }
            catch (Exception ex)
            {
                Response.Write("<div id='my-div' data-info=" + (int)Enums.ValidationCheck.systemCrash + "></div>");
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                if (((System.Web.Mvc.ContentResult)new ErrorHandlingController().ErrorHandlingMessage(ex.Message, Request.IsAjaxRequest())).Content == "tokenerror") //if result is tokenerror (another user logged in on the same account), redirect the user to login page
                    return RedirectToAction("Index", "Login", new { tokenerror = "true" }); // Redirect to login page using controller/action
                else if (((System.Web.Mvc.ContentResult)new ErrorHandlingController().ErrorHandlingMessage(ex.Message, Request.IsAjaxRequest())).Content == "true")
                    ViewBag.PageLayoutPath = Constants.MainLayoutPath;

                TempData["ErrorMessage"] = ex;

                return PartialView(ConfigurationManager.AppSettings["ErrorMsg"].ToString());
            }
        }

        public ActionResult SendEmail()
        {
            try
            {
                if (Request.IsAjaxRequest())
                {
                    ViewBag.PageLayoutPath = null;
                }
                else
                {
                    ViewBag.PageLayoutPath = Global.Constants.MainLayoutPath;
                }

                ViewData["SelectedTab"] = Enums.PageNames.SendEmail.ToString();
                return View();
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }


        /// <summary>
        /// Send email to the recipient with the optional attachment. It also saves a copy of the file on the server
        /// </summary>
        /// <param name="objModelMail"></param>
        /// <param name="fileUploader"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SendEmailAction()
        {
            try
            {

                /// Log Start info
                Common.CLog.WriteLogInfo("Correspondence", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                #region Get DealID
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
                }
                else
                {
                    #region Get DealID from URL Parameter (Unique Reference)
                    if (Request["URef"] == null)
                    {
                        UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
                    }
                    else
                    {
                        UniqueReference = Common.CUniqueReference.GetUniqueReferenceNonAjaxRequest(Request["URef"]);
                    }
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
                #endregion Get DealID

                //get userid
                string aspNetUsersId = HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value;
                object userID = Models.UserModel.GetUserIDByAspNetID(aspNetUsersId);
                //end of get user id

                var FormObjects = new Dictionary<string, dynamic>();
                var FileObjects = new Dictionary<string, dynamic>();
                var dataSerialized = Request.Form["formDataValues"].ToString();
                FormObjects = Common.CDynamicSqlRow.DecodeSerializes(dataSerialized);
                dynamic eoDynamicData = Common.CDynamicSqlRow.DictionaryValues(FormObjects);//get values from dictionary

                #region FilesLogic
                byte[] UploadedDoc = null;
                MemoryStream stream = new MemoryStream();

                //get files details 
                HttpPostedFileBase postedfile = Request.Files["attachedFile"];
                var postedfileUIName = Request.Form["attachedFileUIName"]; /// Get UI element name          

                if (Request.Files["attachedFile"] != null)
                {
                    //save copy of file
                    using (var binaryReader = new BinaryReader(postedfile.InputStream))
                    {
                        UploadedDoc = binaryReader.ReadBytes(postedfile.ContentLength);
                    }
                    stream = new MemoryStream(UploadedDoc); // we save a copy of the file to the memorystream
                                                            //end of save copy of file
                }

                bool saveAttachment = false;

                try
                {
                    if (eoDynamicData.saveAttachementToggle.ToString() != null && eoDynamicData.saveAttachementToggle.ToString() == "on")
                    {
                        #region FilesLogic


                        if (postedfile != null)
                        {
                            string uploadedFileName = postedfile.FileName.Substring(postedfile.FileName.LastIndexOf('\\') + 1);
                            FormObjects.Add("UploadedFile", Common.CFile.ConvertByteArrayToBase64String(UploadedDoc));
                            FormObjects.Add("Filename", uploadedFileName);
                            saveAttachment = true;
                        }
                        else
                        {
                            FormObjects.Add("Filename", "empty");
                        }
                        #endregion end FilesLogic
                    }
                }
                catch (Exception)
                {
                    FormObjects.Add("Filename", "empty");
                }
                #endregion end FilesLogic

                // Call the send email method
                string returnString = Common.CMail.sendEmailWithAttachment(eoDynamicData.To, eoDynamicData.Subject, eoDynamicData.Body.Replace("\r\n", "<br/>").Replace("\"", @"'"), eoDynamicData.CC, postedfile, stream);
                if (returnString == "success")
                {
                    Models.CorrespondenceModel.InsertCorrespondence(dealID, eoDynamicData.Subject, Convert.ToInt32(userID.ToString()), "TOSENDER:" + eoDynamicData.To.Trim() + "CCSENDER:" + eoDynamicData.CC, eoDynamicData.Body.Replace("\r\n", "<br/>").Replace("\"", @"'"), eoDynamicData.Subject, eoDynamicData.Body, 0, 0, "email", FormObjects, saveAttachment, UploadedDoc);
                    return null;
                }
                return RedirectToAction("Index", new { partial = true });
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("Index", new { partial = true });
            }
        }


        public ActionResult SendSMS()
        {
            try
            {
                if (Request.IsAjaxRequest())
                {
                    ViewBag.PageLayoutPath = null;
                }
                else
                {
                    ViewBag.PageLayoutPath = Global.Constants.MainLayoutPath;
                }

                ViewData["SelectedTab"] = Enums.PageNames.SendSMS.ToString();
                return View();
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// We send the sms to the recipient's number
        /// </summary>
        /// <param name="objModelSMS"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SendSMSAction(UI.Models.SMSModel objModelSMS)
        {
            //string ToastrMessage = "toastr.{0}('{1}')";
            try
            {

                #region Get DealID
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
                    if (Request["URef"] == null)
                    {
                        UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
                    }
                    else
                    {
                        UniqueReference = Common.CUniqueReference.GetUniqueReferenceNonAjaxRequest(Request["URef"]);
                    }
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
                    ViewBag.PageLayoutPath = null;
                }
                #endregion Get DealID

                //get userid
                string aspNetUsersId = HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value;
                object userID = Models.UserModel.GetUserIDByAspNetID(aspNetUsersId);
                //end of get user id


                Common.CSMS sms = new Common.CSMS();
                var FormObjects = new Dictionary<string, dynamic>();
                FormObjects.Add("Filename", "SMS");
                int status = 0;
                byte[] emptyFile = null;

                // if there is an error while sending the sms
                if (sms.Send(objModelSMS.Number.Replace("+", string.Empty), objModelSMS.Message.ToString()))
                {
                    status = 1;
                }

                Models.CorrespondenceModel.InsertCorrespondence(dealID, "SMS", Convert.ToInt32(userID.ToString()), "NUMBER:" + objModelSMS.Number.Replace("+", string.Empty), objModelSMS.Message, objModelSMS.Message, objModelSMS.Message, status, 0, "", FormObjects, false, emptyFile);


                object getCorrespondenceInfo = Models.CorrespondenceModel.GetCorrespondenceInfo(Convert.ToInt32(userID.ToString()), dealID);
                ViewData["CorrespondenceInfo"] = getCorrespondenceInfo;
                ViewBag.PageLayoutPath = null;
                return PartialView("CorrespondenceList");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// get correspondence list from db
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CorrespondenceList()
        {
            try
            {
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

                    ViewBag.PageLayoutPath = null;
                }
                else
                {
                    #region Get DealID from URL Parameter (Unique Reference)
                    if (Request["URef"] == null)
                    {
                        UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
                    }
                    else
                    {
                        UniqueReference = Common.CUniqueReference.GetUniqueReferenceNonAjaxRequest(Request["URef"]);
                    }
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

                ViewData["SelectedTab"] = Enums.PageNames.CorrespondenceList.ToString();

                //get userid
                string aspNetUsersId = HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value;
                object userID = Models.UserModel.GetUserIDByAspNetID(aspNetUsersId);
                //end of get user id

                object getCorrespondenceInfo = Models.CorrespondenceModel.GetCorrespondenceInfo(Convert.ToInt32(userID.ToString()), dealID);
                ViewData["CorrespondenceInfo"] = getCorrespondenceInfo;
                return View();
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// We generate a list of the emails of all the participants of the selected deal
        /// </summary>
        /// <param name="DealID"></param>
        /// <param name="term"></param>
        /// <returns></returns>
        public JsonResult AutocompleteEmails(int DealID, string term)
        {
            try
            {
                object suggestions = Models.CorrespondenceModel.GetEmailAutocomplete(DealID, term);
                return Json(suggestions, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return null;
            }
        }

        /// <summary>
        /// We generate a list of the telephone numbers of all the participants of the selected deal
        /// </summary>
        /// <param name="DealID"></param>
        /// <param name="term"></param>
        /// <returns></returns>
        public JsonResult AutocompleteTelephoneNumber(int DealID, string term)
        {
            try
            {
                object suggestions = Models.CorrespondenceModel.GetTelephoneAutocomplete(DealID, term);
                return Json(suggestions, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return null;
            }
        }

        /// <summary>
        /// get correspondence details from correspondence id
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetCorrespondenceDetails(string correspondenceGUID)
        {
            try
            {
                int correspondenceID;
                object correspondenceIDObject = Models.CorrespondenceModel.GetCorrespondenceIDByCorrespondenceGUID(correspondenceGUID); // We get the CorrespondenceID from the GUID
                correspondenceID = Convert.ToInt32(correspondenceIDObject);
                /// Call WCF to get all the Correspondence emails
                object getCorrespondenceInfo = Models.CorrespondenceModel.GetCorrespondenceDetails_ByID(correspondenceID);
                ViewData["CorrespondenceDetails"] = getCorrespondenceInfo;
                return View("PopupCorrespondenceDetails");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return View("error");
            }
        }

        /// <summary>
        /// Download attachment file
        /// </summary>
        /// <param name="documentID"></param>
        /// <returns></returns>

        public ActionResult DownloadAttachedtFile(int documentID)
        {
            try
            {
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

                    ViewBag.PageLayoutPath = null;
                }
                else
                {
                    #region Get DealID from URL Parameter (Unique Reference)
                    if (Request["URef"] == null)
                    {
                        UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
                    }
                    else
                    {
                        UniqueReference = Common.CUniqueReference.GetUniqueReferenceNonAjaxRequest(Request["URef"]);
                    }
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

                Common.CValidator resultValidator = null;
                /// Call WCF to get Document name
                using (CorrespondenceServiceReference.CorrespondenceServiceClient correspondenceServiceClient = new CorrespondenceServiceReference.CorrespondenceServiceClient())
                {
                    resultValidator = correspondenceServiceClient.GetBinaryDocumentAttachment(documentID, dealID);
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    serializer.MaxJsonLength = Int32.MaxValue;
                    var FileResultObj = serializer.Deserialize<dynamic>(resultValidator.output);

                    return File(Common.CFile.FromBase64StringToByteArray(FileResultObj["FileContent"]), System.Net.Mime.MediaTypeNames.Application.Octet, FileResultObj["TemplateDisplayName"]);
                }
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return View("error");
            }
        }
    }
}