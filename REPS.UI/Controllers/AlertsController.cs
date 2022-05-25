using Global;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace REPS.UI.Controllers
{
    [TokenAuthorizeAttribute]
    public class AlertsController : Controller
    {
        // GET: Alerts
        public ActionResult Index(string filter = "all")
        {
            try
            {
                // Set selected tab to all alerts
                if (filter == "all")
                    ViewData["SelectedTab"] = Enums.PageNames.AllAlerts.ToString();

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

                ///call wcf to get alert info
                object getAlertInfo = Models.AlertsModel.GetAlertsInfo(HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value, filter, dealID);
                ViewData["AllAlerts"] = getAlertInfo;

                ViewBag.FilterSelection = filter;
                //int prav = Convert.ToInt32("test");
                return View();
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                //var controller = DependencyResolver.Current.GetService<ErrorHandlingController>(); //calling antother controller 
                //controller.ControllerContext = new ControllerContext(Request.RequestContext, controller);

                //object result = controller.ErrorHandlingMessage(ex.Message, Request.IsAjaxRequest());

                //if (((System.Web.Mvc.ContentResult)result).Content == "true") //if result is null, the call method is not an ajax call, thus shall implement the layout and return it to the same view
                //{
                //    ViewBag.PageLayoutPath = Constants.MainLayoutPath;

                //}

                //if (((System.Web.Mvc.ContentResult)result).Content == "tokenerror") //if result is token error or someone else is login, redirect the user to login page
                //{
                //    return RedirectToAction("Index", "Login", new { tokenerror = "true" }); // Redirect to login page using controller/action
                //}
                var errorResult = new ErrorHandlingController().ErrorHandlingMessage(ex.Message, Request.IsAjaxRequest());
                if (((System.Web.Mvc.ContentResult)errorResult).Content == "true") //if result is null, the call method is not an ajax call, thus shall implement the layout and return it to the same view
                    ViewBag.PageLayoutPath = Constants.MainLayoutPath;
                if (((System.Web.Mvc.ContentResult)errorResult).Content == "tokenerror") //if result is tokenerror (another user logged in on the same account), redirect the user to login page
                    return RedirectToAction("Index", "Login", new { tokenerror = "true" }); // Redirect to login page using controller/action

                TempData["ErrorMessage"] = ex;
                return PartialView(ConfigurationManager.AppSettings["ErrorMsg"].ToString());
            }
        }

        public ActionResult FilterPartialView(string filter = "all", bool showErrorDiv = false)
        {
            try
            {
                // Set selected tab to all alerts
                if (filter == "all")
                    ViewData["SelectedTab"] = Enums.PageNames.AllAlerts.ToString();

                #region Get DealID from URL Parameter (Unique Reference)

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

                #endregion Get DealID from URL Parameter (Unique Reference)


                //get userid
                string aspNetUsersId = HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value;
                object userIDObj = Models.UserModel.GetUserIDByAspNetID(aspNetUsersId);

                ///call wcf to get alert info
                object getAlertInfo = Models.AlertsModel.GetAlertsInfo(HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value, filter, dealID);
                ViewData["AllAlerts"] = getAlertInfo;

                ViewBag.FilterSelection = filter;

                if (showErrorDiv)
                {
                    Response.Write("<div id='my-div' data-info=" + (int)Enums.ValidationCheck.reusltOutput + "></div>");
                }
                return PartialView("AlertsList");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        public ActionResult PopupCreateEvent()
        {
            try
            {
                return View("PopupCreateEvent");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }


        public ActionResult PopupEditEvent()
        {
            try
            {
                int alertID = (int)Models.AlertsModel.GetAlertsIDByAlertsGUID(Request["alertsGUID"]);
                object getAlertEventDetails = Models.AlertsModel.GetEventByID(alertID);
                ViewData["AlertEventDetails"] = getAlertEventDetails;
                ViewBag.Filter = Request["filter"];
                return View("PopupEditEvent");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Call WCF to insert the Event Details 
        /// </summary>
        /// <param name="objModelAlerts"></param>
        /// <returns></returns>
        public ActionResult CreateEvent(Models.AlertsModel objModelAlerts)
        {
            try
            {
                #region Get DealID from URL Parameter (Unique Reference)

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

                #endregion Get DealID from URL Parameter (Unique Reference)

                #region variables 
                ///get the values from model
                DateTime EventStartDateTime = new DateTime(DateTime.ParseExact(objModelAlerts.StartingDate + " 00:00:00", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture).Year, DateTime.ParseExact(objModelAlerts.StartingDate + " 00:00:00", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture).Month, DateTime.ParseExact(objModelAlerts.StartingDate + " 00:00:00", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture).Day, DateTime.ParseExact(objModelAlerts.StartingDate + " " + objModelAlerts.StartTime + ":00", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture).Hour, DateTime.ParseExact(objModelAlerts.StartingDate + " " + objModelAlerts.StartTime + ":00", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture).Minute, DateTime.ParseExact(objModelAlerts.StartingDate + " " + objModelAlerts.StartTime + ":00", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture).Second);
                DateTime EventEndDateTime = new DateTime(DateTime.ParseExact(objModelAlerts.EndDate + " 00:00:00", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture).Year, DateTime.ParseExact(objModelAlerts.EndDate + " 00:00:00", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture).Month, DateTime.ParseExact(objModelAlerts.EndDate + " 00:00:00", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture).Day, DateTime.ParseExact(objModelAlerts.EndDate + " " + objModelAlerts.EndTime + ":00", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture).Hour, DateTime.ParseExact(objModelAlerts.EndDate + " " + objModelAlerts.EndTime + ":00", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture).Minute, DateTime.ParseExact(objModelAlerts.EndDate + " " + objModelAlerts.EndTime + ":00", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture).Second);
                //DateTime EventStartDateTime = new DateTime(objModelAlerts.StartDate.Year, objModelAlerts.StartDate.Month, objModelAlerts.StartDate.Day, int.Parse(Request["StartTime"].Split(':')[0].ToString()), int.Parse(Request["StartTime"].Split(':')[1].ToString()), 00);
                //DateTime EventEndDateTime = new DateTime(objModelAlerts.StartDate.Year, objModelAlerts.StartDate.Month, objModelAlerts.StartDate.Day, int.Parse(Request["EndTime"].Split(':')[0].ToString()), int.Parse(Request["EndTime"].Split(':')[1].ToString()), 00);
                string eventName = objModelAlerts.EventName;
                string location = objModelAlerts.Location;
                string description = objModelAlerts.Description;
                var result = "";
                #endregion end variables
                ///Call WCF to insert the Event Details
                #region Call WCF to insert the Event Details 
                object insertAlertInfo = Models.AlertsModel.InsertAlertInfo(eventName, location, EventStartDateTime, EventEndDateTime, description, HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value, 1, dealID);
                result = insertAlertInfo.ToString();
                #endregion end Call WCF to insert the Event Details 
                TempData["ToasterEventCreatedMsg"] = Notifications.GetToastrMessage(Enums.MessageType.success.ToString(), "Events has been successfully added");
                return RedirectToAction("FilterPartialView");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Call WCF to edit the Event Details
        /// </summary>
        /// <param name="objModelAlerts"></param>
        /// <returns></returns>
        public ActionResult EditEvent(Models.AlertsModel objModelAlerts, string filter)
        {
            try
            {
                #region Get DealID from URL Parameter (Unique Reference)

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

                #endregion Get DealID from URL Parameter (Unique Reference)

                #region variables 
                ///get the values from model  
                ///get the values from model  
                string eventName = objModelAlerts.EditEventName;
                string location = objModelAlerts.EditLocation;
                string description = objModelAlerts.EditDescription;
                string concatStartDateTimeValue = objModelAlerts.StartingDate + " " + objModelAlerts.StartTime;
                string concatEndDateTimeValue = objModelAlerts.EndDate + " " + objModelAlerts.EndTime;
                DateTime EventStartDateTime = DateTime.ParseExact(concatStartDateTimeValue.Trim(), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                DateTime EventEndDateTime = DateTime.ParseExact(concatEndDateTimeValue.Trim(), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                //DateTime EventStartDateTime = new DateTime(DateTime.ParseExact(objModelAlerts.StartingDate + " 00:00:00", "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture).Year, DateTime.ParseExact(objModelAlerts.StartingDate + " 00:00:00", "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture).Month, DateTime.ParseExact(objModelAlerts.StartingDate + " 00:00:00", "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture).Day, DateTime.ParseExact(objModelAlerts.StartingDate + " " + objModelAlerts.StartTime.PadLeft(5, '0') + ":00", "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture).Hour, DateTime.ParseExact(objModelAlerts.StartingDate + " " + objModelAlerts.StartTime.PadLeft(5, '0') + ":00", "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture).Minute, 00);
                //DateTime EventEndDateTime = new DateTime(DateTime.ParseExact(objModelAlerts.StartingDate + " 00:00:00", "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture).Year, DateTime.ParseExact(objModelAlerts.StartingDate + " 00:00:00", "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture).Month, DateTime.ParseExact(objModelAlerts.StartingDate + " 00:00:00", "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture).Day, DateTime.ParseExact(objModelAlerts.StartingDate + " " + objModelAlerts.EndTime.PadLeft(5, '0') + ":00", "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture).Hour, DateTime.ParseExact(objModelAlerts.StartingDate + " " + objModelAlerts.EndTime.PadLeft(5, '0') + ":00", "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture).Minute, 00);
                string alertGUID = Request["EventGUID"];
                int alertID = (int)Models.AlertsModel.GetAlertsIDByAlertsGUID(alertGUID);
                #endregion end variables

                
                object resultUpdatedAlert = Models.AlertsModel.EditAlertsModel(eventName, location, EventStartDateTime, EventEndDateTime, description, alertID, dealID);
                Guid guidOutput;
                bool showErrorDiv = false;
                bool isGuid = Guid.TryParse(resultUpdatedAlert.ToString(), out guidOutput);

                if (isGuid)
                {
                    showErrorDiv = true;
                }

                return RedirectToAction("FilterPartialView", new { filter = filter, showErrorDiv = showErrorDiv });
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// We change the event status: active, complete & archive by using AJAX
        /// </summary>
        /// <param name="alertsGUID"></param>
        /// <param name="statusID"></param>
        /// <returns></returns>
        [HttpPost]
        public object ChangeEventStatus(string alertsGUID, int statusID)
        {
            try
            {
                int alertsID = (int)Models.AlertsModel.GetAlertsIDByAlertsGUID(alertsGUID);
                int dealID = (int)Models.AlertsModel.GetDealIDByAlertsGUID(alertsGUID);
                Common.CValidator resultValidator = null;
                using (AlertsServiceReference.AlertsServiceClient alertsServiceClient = new AlertsServiceReference.AlertsServiceClient())
                {
                    resultValidator = alertsServiceClient.UpdateAlertsStatus(alertsID, statusID, dealID);

                    var output = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);

                    /// Call WCF to get all alerts for User
                    object allDiaryItems = Models.AlertsModel.GetAlertsForDiaryItems(HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value);
                    ViewData["DiaryItems"] = allDiaryItems;

                    return PartialView("PartialAlertsDiaryItems");
                }
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        [HttpPost]
        public object RenderPartialAlertsDiaryItems()
        {
            try
            {
                /// Call WCF to get all alerts for User
                object allDiaryItems = Models.AlertsModel.GetAlertsForDiaryItems(HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value);
                ViewData["DiaryItems"] = allDiaryItems;

                return PartialView("PartialAlertsDiaryItems");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// We change the event status: active, complete & archive by using javascript
        /// </summary>
        /// <param name="alertsGUID"></param>
        /// <param name="StatusID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ChangeEventStatusJavascript(string alertsGUID, int statusID, string filter)
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
                        ViewBag.PageLayoutPath = null;
                        UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
                    }
                    else
                    {
                        UniqueReference = Common.CUniqueReference.GetUniqueReferenceNonAjaxRequest(Request["URef"]);
                        ViewBag.PageLayoutPath = Global.Constants.MainLayoutPath;
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


                int alertsID = (int)Models.AlertsModel.GetAlertsIDByAlertsGUID(alertsGUID);
                object updateAlert = Models.AlertsModel.UpdateAlertStatus(alertsID, statusID, dealID);

                ///call wcf to get alert info
                object getAlertInfo = Models.AlertsModel.GetAlertsInfo(HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value, filter, dealID);
                ViewData["AllAlerts"] = getAlertInfo;

                ViewBag.FilterSelection = filter;

                return PartialView("AlertsList");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// We change the event status: active, complete & archive by using javascript
        /// </summary>
        /// <param name="alertsGUID"></param>
        /// <param name="StatusID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RefreshAlertList()
        {
            try
            {
                int dealID = 0;
                string UniqueReference = string.Empty;

                if ((Request.UrlReferrer.Query != null) || (Request["URef"] != null))
                {
                    if (Request["URef"] == null)
                    {
                        ViewBag.PageLayoutPath = null;
                        UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
                    }
                    else
                    {
                        UniqueReference = Common.CUniqueReference.GetUniqueReferenceNonAjaxRequest(Request["URef"]);
                        ViewBag.PageLayoutPath = Global.Constants.MainLayoutPath;
                    }
                }
                else
                {
                    ViewBag.PageLayoutPath = null;
                    UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
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
                
                ///call wcf to get alert info
                object getAlertInfo = Models.AlertsModel.GetAlertsInfo(HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value, "all", dealID);
                ViewData["AllAlerts"] = getAlertInfo;

                ViewBag.FilterSelection = "all";

                return PartialView("AlertsList");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// We update the number of alert items by using AJAX
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public object UpdateNotificationCount()
        {
            try
            {
                object allDiaryItems = Models.AlertsModel.GetAlertsForDiaryItems(HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value);
                ViewData["DiaryItems"] = allDiaryItems;

                return PartialView("PartialAlertsNotificationsCount");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// We build the outlook calendar file to be downloaded by the user
        /// </summary>
        /// <param name="alertsGUID"></param>
        /// <returns></returns>
        public string BuildOutlookFile(string alertsGUID)
        {
            try
            {
                int alertsID = (int)Models.AlertsModel.GetAlertsIDByAlertsGUID(alertsGUID);
                Common.CValidator resultValidator = null;
                using (AlertsServiceReference.AlertsServiceClient alertsServiceClient = new AlertsServiceReference.AlertsServiceClient())
                {
                    resultValidator = alertsServiceClient.GetEventByID(alertsID);

                    var output = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);

                    System.Text.StringBuilder sbICSFile = new System.Text.StringBuilder();
                    DateTime dtNow = DateTime.Now;


                    DateTime myStartDate = TimeZone.CurrentTimeZone.ToLocalTime(output[0]["StartDate"]);
                    DateTime myEndDate = TimeZone.CurrentTimeZone.ToLocalTime(output[0]["EndDate"]);

                    sbICSFile.AppendLine("BEGIN:VCALENDAR");
                    sbICSFile.AppendLine("VERSION:2.0");
                    sbICSFile.AppendLine("METHOD:PUBLISH");
                    sbICSFile.AppendLine("CALSCALE:GREGORIAN");

                    sbICSFile.AppendLine("BEGIN:VEVENT");
                    sbICSFile.AppendLine("SUMMARY:" + output[0]["EventName"]);

                    // Adding a time stamp
                    sbICSFile.Append("DTSTAMP:");
                    sbICSFile.AppendLine(output[0]["StartDate"].ToString("yyyyMMddTHHmmssZ"));

                    // Adding created date
                    sbICSFile.Append("CREATED:");
                    sbICSFile.AppendLine(output[0]["StartDate"].ToString("yyyyMMddTHHmmssZ"));

                    sbICSFile.Append("DTSTART:");
                    sbICSFile.AppendLine(myStartDate.ToString("yyyyMMddTHHmmss"));

                    sbICSFile.Append("DTEND:");
                    sbICSFile.AppendLine(myEndDate.ToString("yyyyMMddTHHmmss"));

                    sbICSFile.AppendLine("LOCATION: " + output[0]["Location"]);
                    sbICSFile.AppendLine("CATEGORIES:Yellow Category");

                    sbICSFile.AppendLine("X-ALT-DESC;FMTTYPE=text/html: " + output[0]["Comment"]);

                    sbICSFile.AppendLine("BEGIN:VALARM");
                    sbICSFile.AppendLine("TRIGGER:-PT30M");
                    sbICSFile.AppendLine("ACTION:DISPLAY");
                    sbICSFile.AppendLine("DESCRIPTION:Reminder");
                    sbICSFile.AppendLine("END:VALARM");

                    sbICSFile.AppendLine("END:VEVENT");
                    sbICSFile.AppendLine("END:VCALENDAR");

                    return sbICSFile.ToString();
                }
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return null;
            }
        }

        /// <summary>
        /// We return the file to the user
        /// </summary>
        /// <param name="alertsGUID"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OutlookCalendar(string alertsGUID)
        {
            try
            {
                var bytes = Encoding.UTF8.GetBytes(this.BuildOutlookFile(alertsGUID));
                int alertsID = (int)Models.AlertsModel.GetAlertsIDByAlertsGUID(alertsGUID);
                string eventName = string.Empty;
                Common.CValidator resultValidator = null;
                using (AlertsServiceReference.AlertsServiceClient alertsServiceClient = new AlertsServiceReference.AlertsServiceClient())
                {
                    resultValidator = alertsServiceClient.GetEventByID(alertsID);

                    var output = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                    eventName = output[0]["EventName"];
                }
                return this.File(bytes, "text/calendar", eventName+".ics");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return null;
            }
        }

        /// <summary>
        /// Display Create Event Popup
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCreateEventPopup()
        {
            try
            {
                return View("CreateEventPopup");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }
    }
}