using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Global;
using System.ServiceModel;
using System.Web.Security;
using System.ServiceModel.Channels;
using System.Configuration;

namespace REPS.UI.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index(string tokenerror = null)
        {
            if (!string.IsNullOrEmpty(tokenerror))
            {
                return View("Index", new { tokenerror = tokenerror });
            }
            return View();
        }

        /// <summary>
        /// login to e4international DB
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult login()
        {
            try
            {
                #region variables
                string userEmail = Request["repsUserEmail"];
                string userPassword = Request["repsUserPassword"];
                //set view data to login for title layout
                ViewData["login"] = "login";
                #endregion end variables

                ///Connect to Auth Service and Get Token

                /// Call WCF authenticate
                using (AuthenticateServiceReference.AuthenticateServiceClient authenticateClient = new AuthenticateServiceReference.AuthenticateServiceClient())
                {

                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(authenticateClient.InnerChannel))
                    {
                        var res = authenticateClient.DoLogin(userEmail, userPassword);
                        if (res == Enums.UserAccountDeactivated.deactivated.ToString())
                        {
                            TempData["LoginErrorMsg"] = "The email you have entered does not match any account.";
                            return View("Index");
                        }
                        /// get bearer header with token from server
                        var httpProperties = (HttpResponseMessageProperty)OperationContext.Current
                                 .IncomingMessageProperties[HttpResponseMessageProperty.Name];

                        Common.CLog.WriteLogErr(new System.Exception(httpProperties.Headers["Bearer"]), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                        /// add token to cookie
                        if (httpProperties.Headers["Bearer"] != null)
                        {
                            HttpContext.Response.Cookies.Add(new HttpCookie("Bearer", httpProperties.Headers["Bearer"]));
                        }
                        else
                        {
                            throw new Exception("Could not save bearer to user table");
                        }

                        if (res != null)///IF successful with returned Bearer 
                        {
                            ///save bearer in Temp data for cross controller reference to call succesive functions
                            TempData["Bearer"] = res;// 

                            //Let us now set the authentication cookie so that we can use that later.
                            FormsAuthentication.SetAuthCookie(res, false);

                            IEnumerable<dynamic> UserInfo = Models.UserModel.GetUserInfo(null, res.ToString());
                            HttpContext.Response.Cookies.Add(new HttpCookie("REPS_AspNetUsersId", UserInfo.FirstOrDefault()["AspNetUsersId"].ToString()));
                            HttpContext.Response.Cookies.Add(new HttpCookie("REPS_EntityGUID", UserInfo.FirstOrDefault()["EntityGUID"].ToString()));
                            HttpContext.Response.Cookies.Add(new HttpCookie("REPS_TaskGUID", UserInfo.FirstOrDefault()["TaskGUID"].ToString()));
                            HttpContext.Response.Cookies.Add(new HttpCookie("REPS_FullName", UserInfo.FirstOrDefault()["GivenName"].ToString() + " " + UserInfo.FirstOrDefault()["FamilyName"].ToString()));

                            if (UserInfo.FirstOrDefault()["EntityID"].ToString() == "-1")
                            {
                                ViewData["NoEntity"] = Common.CString.GetEnumDescription(Enums.NoEntity.Noentity);
                                ViewData["errorMsg"] = Common.CString.GetEnumDescription(Enums.NoEntity.ContactAdmin);
                                return RedirectToAction("ErrorForm", "ErrorHandling", new { nullValue = ViewData["NoEntity"].ToString(), @errorValue= ViewData["errorMsg"].ToString() });
                            }
                            return RedirectToAction("Index", "Dashboard");
                        }
                        else
                        {
                            //ModelState.AddModelError("", "Invalid Password or Email Address");
                            TempData["LoginErrorMsg"] = "The email or password you've entered is invalid. Please try again.";
                            return View("Index");
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                TempData["LoginErrorMsg"] = "An error has occured, please retry the action!\nError Message: " + ex.Message;
                return View("Index");
            }


        }

        /// <summary>
        /// Forgot Password View
        /// </summary>
        /// <returns></returns>
        public ActionResult ForgotPassword()
        {
            try
            {
                return View("ForgotPassword");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                TempData["LoginErrorMsg"] = "An error has occured, please retry the action!";
                return View("Index");
            }
        }


        /// <summary>
        /// Forgot Password
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public ActionResult ForgotPasswordAction(string repsUserEmail)
        {
            try
            {
                /// Call WCF to generate new token
                bool result = Models.UserModel.ForgotPassword(repsUserEmail.Trim());

                return View("ResetPasswordEmailSentPage");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                TempData["LoginErrorMsg"] = "An error has occured, please retry the action!";
                return View("Index");
            }
        }

        public ActionResult ResetPassword(string email, string token)
        {
            try
            {
                Session.RemoveAll();
                Session.Clear();

                if (!Models.UserModel.VerifyUserToken(email, token))
                    return View("LinkExpiredPage");

                ViewData["Email"] = email;
                ViewData["Token"] = token;

                return View("ResetPassword");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                TempData["LoginErrorMsg"] = "An error has occured, please retry the action!";
                return View("Index");
            }
        }

        public ActionResult ResetPasswordAction(string email, string token)
        {
            try
            {
                // Check if entered password is same/matched
                if (Request.Form["NewPassword"] != Request.Form["ConfirmPassword"])
                {
                    TempData["PasswordChangeResult"] = "NotMatched";
                    return RedirectToAction("ResetPassword", "login", new { email = email, token = token });
                }

                /// Call WCF to reset password
                bool result = Models.UserModel.ResetPassword(email, Request.Form["NewPassword"], token);

                if (result)
                {
                    return RedirectToAction("Index", "login");
                }
                else
                {
                    TempData["PasswordChangeResult"] = result;
                    return RedirectToAction("ResetPassword", "login", new { email = email, token = token });
                }
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                TempData["LoginErrorMsg"] = "An error has occured, please retry the action!";
                return View("Index");
            }
        }

        public ActionResult Logout(string tokenerror = null)
        {
            try
            {
                string[] myCookies = Request.Cookies.AllKeys;
                foreach (string cookie in myCookies)
                {
                    Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
                }
                if (tokenerror != null)
                {
                    return RedirectToAction("Index", "Login", new { tokenerror = tokenerror });
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                TempData["LoginErrorMsg"] = "An error has occured, please retry the action!";
                return View("Index");
            }
        }
    }
}