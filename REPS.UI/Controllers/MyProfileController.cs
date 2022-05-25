using Global;
using REPS.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace REPS.UI.Controllers
{
    public class MyProfileController : Controller
    {
        // GET: MyProfile
        [OutputCache(NoStore = true, Duration = 1)]
        public ActionResult Index(string error = null)
        {
            try
            {
                /* if (!string.IsNullOrEmpty(error))
                 {
                     ViewBag.PageLayoutPath = Global.Constants.MainLayoutPath;
                     ViewData["errorMsg"] = error;
                     return View("Index", new { error = error });
                 }
                 */


                // Check if Ajax Request and determine layout to be used
                if (Request.IsAjaxRequest())
                {
                    ViewBag.PageLayoutPath = null;
                }
                else
                {
                    ViewBag.PageLayoutPath = Global.Constants.MainLayoutPath;
                }

                ViewData["SelectedLayout"] = "Profile";

                /// Call WCF to get all countries
                object[] getCountry = Models.PropertyModel.GetCountries(null, null, null);
                ViewData["Countries"] = getCountry;

                //get userid
                string aspNetUsersId = HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value;
                object userID = Models.UserModel.GetUserIDByAspNetID(aspNetUsersId);
                //end of get user id

                /// Get user information
                object[] UserInfo = Models.UserModel.GetUserInfo((int)userID, null);
                ViewData["UserInfo"] = UserInfo;

                // Get all Roles
                object RolesList = RoleModel.GetRoles(null, null, null);
                ViewData["RolesList"] = RolesList;

                return View("Index");

            }
            catch (Exception ex)
            {
                 //var errorResult = new ErrorHandlingController().ErrorHandlingMessage(ex.Message, Request.IsAjaxRequest());
                //if (((System.Web.Mvc.ContentResult)errorResult).Content == "true") //if result is null, the call method is not an ajax call, thus shall implement the layout and return it to the same view
                //    ViewBag.PageLayoutPath = Constants.MainLayoutPath;
                //else if (((System.Web.Mvc.ContentResult)errorResult).Content == "tokenerror") //if result is tokenerror (another user logged in on the same account), redirect the user to login page
                //    return RedirectToAction("Index", "Login", new { tokenerror = "true" }); // Redirect to login page using controller/action
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

        /// <summary>
        /// Update User Profile
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateUserProfile()
        {
            try
            {
                var FormObjects = new Dictionary<string, dynamic>();
                Request.Form.CopyTo(FormObjects);

                //get userid
                string aspNetUsersId = HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value;
                object userID = Models.UserModel.GetUserIDByAspNetID(aspNetUsersId);
                //end of get user id

                /// Add UserID
                FormObjects.Add("UserID", (int)userID);

                var resultUpdatedUser = UserModel.UpdateUserProfile(FormObjects);

                if (Convert.ToInt32(resultUpdatedUser) > 0)
                {
                    TempData["ToasterProfileMsg"] = Notifications.GetToastrMessage(Enums.MessageType.success.ToString(), "Profile has been successfully updated");
                }

                return Index();
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

        public ActionResult Security()
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
                ViewData["SelectedLayout"] = "Profile";

                return View("Security");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Update User Profile Security
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateUserProfileSecurity()
        {
            try
            {
                var FormObjects = new Dictionary<string, dynamic>();

                Request.Form.CopyTo(FormObjects);

                //get userid
                string aspNetUsersId = HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value;
                object userID = Models.UserModel.GetUserIDByAspNetID(aspNetUsersId);
                //end of get user id

                // Add UserID
                FormObjects.Add("UserID", (int)userID);

                var result = UserModel.UpdateUserProfileSecurity(FormObjects);

                if (result == false)
                {
                    TempData["ToasterMsg"] = Notifications.GetToastrMessage(Enums.MessageType.error.ToString(), "Password could not be updated");
                    //return Json(new { success = false, responseText = "Password could not be updated!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    TempData["ToasterMsg"] = Notifications.GetToastrMessage(Enums.MessageType.success.ToString(), "Password has been successfully updated");

                }
                //if (result == true)
                //{
                //    TempData["ToasterMsg"] = Notifications.GetToastrMessage(Enums.MessageType.success.ToString(), "Password successfully changed");
                //}
                //else
                //{
                //    TempData["ToasterMsg"] = Notifications.GetToastrMessage(Enums.MessageType.error.ToString(), "Password change failed. Please try again.");
                //}
                return Security();
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        public ActionResult ChangeUserWorkflow(string taskGUID)
        {
            try
            {
                HttpContext.Response.Cookies.Add(new HttpCookie("REPS_TaskGUID", taskGUID));
                object deleteAllDeals = Models.HeaderTabModel.DeleteAllUserDeals(HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value.ToString()); // We clear the header tab
                return Content(Enums.UniqueReference.Invalidreference.ToString()); // We return invalidreference so that the user gets redirected to the dashboard
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }
    }
}