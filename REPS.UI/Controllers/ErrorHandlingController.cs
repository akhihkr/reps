using Global;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace REPS.UI.Controllers
{
    public class ErrorHandlingController : Controller
    {
        [HttpGet]
        public ActionResult ErrorHandling(string ex, bool ajax)
        {
            #region Error Handling
            switch (Models.ErrorHandlingModel.ErrorHandling(ex, ajax))
            {
                case (int)Global.Enums.WCFTokenError.ErrorToastrAjax:
                    TempData["ErrorMessage"] = ex;
                    return PartialView(ConfigurationManager.AppSettings["ErrorMsg"].ToString()); // Display toastr error message

                case (int)Global.Enums.WCFTokenError.ErrorToastrNonAjax:
                    return RedirectToAction("Index", "Dashboard", new { error = "true" }); // Redirect to Dashboard with toastr using controller/action

                case (int)Global.Enums.WCFTokenError.WrongTokenAjax:
                    return Content(ConfigurationManager.AppSettings["WrongTokenAjax"].ToString()); // Redirect to login page via JavaScript

                case (int)Global.Enums.WCFTokenError.WrongTokenNonAjax:
                    return RedirectToAction("Logout", "Login", new { tokenerror = "true" }); // Redirect to login page using controller/action

                default:
                    TempData["ErrorMessage"] = ex;
                    return PartialView(ConfigurationManager.AppSettings["ErrorMsg"].ToString()); // Display toastr error message
            }
            #endregion Error Handling
        }




        [HttpGet]
        public ActionResult ErrorHandlingMessage(string ex, bool ajax)
        {
            #region Error Handling
            string error = null;
            switch (Models.ErrorHandlingModel.ErrorHandling(ex, ajax))
            {
                case (int)Global.Enums.WCFTokenError.ErrorToastrAjax:
                    error = "false";
                    break;

                case (int)Global.Enums.WCFTokenError.ErrorToastrNonAjax:
                    error = "true";
                    break;

                case (int)Global.Enums.WCFTokenError.WrongTokenAjax:
                case (int)Global.Enums.WCFTokenError.WrongTokenNonAjax:
                    error = "tokenerror";
                    break;

                default:
                    error = "false";
                    break;
            }
            return Content(error);
            #endregion Error Handling
        }


        [HttpGet]
        public ActionResult NewErrorHandling(string ex, bool ajax)
        {
            string error = null;
            switch (Models.ErrorHandlingModel.ErrorHandling(ex, ajax))
            {
                case (int)Global.Enums.WCFTokenError.ErrorToastrAjax:
                    error = "false";
                    break;

                case (int)Global.Enums.WCFTokenError.ErrorToastrNonAjax:
                    error = "true";
                    ViewBag.PageLayoutPath = Constants.MainLayoutPath;
                    break;

                case (int)Global.Enums.WCFTokenError.WrongTokenAjax:
                case (int)Global.Enums.WCFTokenError.WrongTokenNonAjax:
                    error = "tokenerror";
                    break;
                //return RedirectToAction("Index", "Login", new { tokenerror = "true" });

                default:
                    error = "false";
                    break;
            }

            return Content(error);
        }



        /// <summary>
        /// Render error form page and pre-fill the fields
        /// </summary>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ErrorForm(string errorMsg, string nullValue, string errorValue)
        {
            //get userid
            string aspNetUsersId = HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value;
            object userID = Models.UserModel.GetUserIDByAspNetID(aspNetUsersId);
            //end of get user id

            //set variables
            if (nullValue != null && errorValue != null)
            {
                ViewData["NoEntity"] = nullValue;
                ViewData["errorMsg"] = errorValue;
            }
            //end of variables

            /// Get user information
            object[] UserInfo = Models.UserModel.GetUserInfo((int)userID, null);
            ViewData["UserInfo"] = UserInfo;
            foreach (var User in ViewData["UserInfo"] as IEnumerable<dynamic>)
            {
                TempData["FullName"] = User["GivenName"] + " " + User["FamilyName"];
                TempData["TelephoneNumber"] = User["Telephone"];
                TempData["errorMsg"] = errorMsg;
            }
            return View("ErrorPage");
        }

        /// <summary>
        /// Submit Error form by email
        /// </summary>
        /// <returns></returns>
        public ActionResult SubmitError(string credential)
        {
            try
            {
                string message = Request["message"] + "<br /><br />Error Message:<br />" + Request["errormsg"];
                message += "<br /><br /><br />Regards,<br />" + Request["fullname"] + "<br /><br /><br />Contact Number: " + Request["number"];
                Common.CMail.sendErrorMail(message, Request["fullname"]);

                if(credential != null)
                {
                    TempData["message"] = Notifications.GetToastrMessage(Enums.MessageType.success.ToString(), "Message has been sent to Administrator");
                    //return View("ErrorRemoveEntityMessage");
                    ViewData["NoEntity"] = Common.CString.GetEnumDescription(Enums.NoEntity.Noentity);
                    ViewData["errorMsg"] = Common.CString.GetEnumDescription(Enums.NoEntity.ContactAdmin);
                    return RedirectToAction("ErrorForm", "ErrorHandling", new { nullValue = ViewData["NoEntity"].ToString(), @errorValue = ViewData["errorMsg"].ToString() });
                }
                return RedirectToAction("Index", "Dashboard");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("Index", "Dashboard");
            }
        }

        public ActionResult DisplayPopupError()
        {
            try
            {
                string aspNetUsersId = HttpContext.Request.Cookies["nothing"].Value;
                return View("~/Views/ErrorHandling/ErrorPage.cshtml");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }
    }
}