using Global;
using REPS.UI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace REPS.UI.Controllers
{
    [TokenAuthorizeAttribute]
    public class AdminUserController : Controller
    {
        // GET: AdminUser
        public ActionResult Index(bool partial = false)
        {
            try
            {
                // Check if Ajax Request and determine layout to be used
                if (Request.IsAjaxRequest() )
                {
                    ViewBag.PageLayoutPath = null;
                }
                else
                {
                    ViewBag.PageLayoutPath = Constants.MainLayoutPath;
                }

                ViewData["SelectedLayout"] = "Admin";
                // Get Users list
                object Userslist = UserModel.GetUsers(null, null, null, null, null,null, null, null);
                ViewData["Userslist"] = Userslist;

                if (partial)
                {
                    return PartialView("PartialAdminUserList");
                }
                return View("Index");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// return partial parts only after confirmation
        /// </summary>
        /// <returns></returns>
        public ActionResult ReturnPartialUserDetails()
        {
            try
            {
                // Get Users list
                object Userslist = UserModel.GetUsers(null, null, null, null, null,null, null, null);
                ViewData["Userslist"] = Userslist;

                return View("PartialUserDetails");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// pop up details of user admin
        /// </summary>
        /// <returns></returns>
        public ActionResult PopupAddUser()
        {
            try
            {
                /// Call WCF to get all countries
                object CountryList = Models.PropertyModel.GetCountry(null, null, null, null);
                ViewData["Countries"] = CountryList;

                /// Call WC to get all entities
                object EntitiesList = Models.EntityModel.GetEntities(null, null, null, null,-1);
                ViewData["EntitiesList"] = EntitiesList;

                /// Call WCF to get all titles
                object TitlesList = Models.UserModel.GetTitles(null);
                ViewData["TitlesList"] = TitlesList;

                // Get all Roles
                object RolesList = RoleModel.GetRoles(null, null, null);
                ViewData["RolesList"] = RolesList;

                /// Call WCF to get all Deal Processes
                //object dealProcessesWorkflow = WorkflowModel.GetWorkflowTaskByWorkflowID(null, (int)Global.Enums.Wokflow.Process);
                object dealProcessesWorkflow = WorkflowModel.GetWorkflowsList((int)Global.Enums.Wokflow.Process);
                ViewData["DealProcesses"] = dealProcessesWorkflow;

                return View("PopupAddUser");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// add user admin
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult InsertNewUser()
        {
            try
            {
                var FormObjects = new Dictionary<string, dynamic>();

                //Copy posted values
                Request.Form.CopyTo(FormObjects);
                string emailExists = "no";//cancel confirm

                // Call wcf to create new user
                object result = UserModel.AddNewUser(FormObjects);

                if (Convert.ToInt32(result) == -2)// User with email already exists
                {
                    emailExists = FormObjects.ContainsKey("Email") ? FormObjects["Email"] : null; //assign email 
                }
                else
                {
                    TempData["ToasterMsgUpdated"] = Notifications.GetToastrMessage(Enums.MessageType.success.ToString(), "User has been successfully added");
                }
                return Json(emailExists, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Reset new account password
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ResetUserPassword(string email, string reset = "")
        {
            try
            {
                /// Call WCF to generate new token
                bool result = UserModel.ForgotPassword(email, reset);
                object Userslist = UserModel.GetUsers(null, null, null, null, null,null, null, null);
                ViewData["Userslist"] = Userslist;
                TempData["ToasterMsgUpdated"] = Notifications.GetToastrMessage(Enums.MessageType.success.ToString(), "User has been successfully updated");
                return View("PartialUserDetails");
                //return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Edit user
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public ActionResult PopupEditUser(string userID)
        {
            try
            {
                object userIDByAspNetUserID = Models.UserModel.GetUserIDByAspNetID(userID);

                /// Call WCF to get all countries
                object CountryList = PropertyModel.GetCountry(null, null, null, null);
                ViewData["Countries"] = CountryList;

                /// Call WCF to get all titles
                object TitlesList = UserModel.GetTitles(null);
                ViewData["TitlesList"] = TitlesList;

                /// Call WC to get all entities
                object EntitiesList = EntityModel.GetEntities(null, null, null, null,-1);
                ViewData["EntitiesList"] = EntitiesList;

                // Get User Info
                object UserInfo = UserModel.GetUserInfo(Convert.ToInt32(userIDByAspNetUserID));
                ViewData["UserInfo"] = UserInfo;

                // Get all Roles
                object RolesList = RoleModel.GetRoles(null, null, null);
                ViewData["RolesList"] = RolesList;

                /// Call WCF to get all Deal Processes
                //object dealProcessesWorkflow = WorkflowModel.GetWorkflowTaskByWorkflowID(null, (int)Global.Enums.Wokflow.Process);
                object dealProcessesWorkflow = WorkflowModel.GetWorkflowsList((int)Global.Enums.Wokflow.Process);
                ViewData["DealProcesses"] = dealProcessesWorkflow;

                ViewData["UserID"] = userID;
                ViewData["SelectedTab"] = Enums.PageNames.Users.ToString();

                return View("PopupEditUser");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }

        }


        /// <summary>
        /// remove user and set delete
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
       /* [HttpPost]
        public ActionResult UpdateUser(string userID)
        {
            try
            {
                var FormObjects = new Dictionary<string, dynamic>();
                object userIDByAspNetUserID = Models.UserModel.GetUserIDByAspNetID(userID);
                //Copy posted values
                Request.Form.CopyTo(FormObjects);
                FormObjects.Add("UserID", Convert.ToInt32(userIDByAspNetUserID));
                int roleId = Convert.ToInt32(Request.Form["RoleID"]);
                // Call wcf to update user info
                object resultUpdatedUser = UserModel.UpdateUser(FormObjects);

                if (Convert.ToInt32(resultUpdatedUser) > 0)
                {
                    TempData["ToasterMsgUpdated"] = Notifications.GetToastrMessage(Enums.MessageType.success.ToString(), "User has been successfully updated','Successful");
                }

                // Get all Roles
                object RolesList = RoleModel.GetRoles(null, null, null);
                ViewData["RolesList"] = RolesList;

                // Get User Info
                object UserInfo = UserModel.GetUserInfo(Convert.ToInt32(userIDByAspNetUserID));
                ViewData["UserInfo"] = UserInfo;
                ViewData["SelectedTab"] = "Users";
                // Get Users list
                object Userslist = UserModel.GetUsers(null, null, null, null, null,-1, null, null);
                ViewData["Userslist"] = Userslist;

                return View("PartialUserDetails");
                //return Index();
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }*/



        /// <summary>
        /// remove user and set delete
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateUser(string userID)
        {
            try
            {
                object Userslist = null;
                var FormObjects = new Dictionary<string, dynamic>();
                //Copy posted values
                Request.Form.CopyTo(FormObjects);
                int roleId = Convert.ToInt32(Request.Form["RoleID"]);

                #region get selected user id from form, for  editing
                object userIDByAspNetUserID = UserModel.GetUserIDByAspNetID(userID);
                #endregion end of get selected user id from form, for  editing
                if (userIDByAspNetUserID == null)
                {                    
                    TempData["ToasterMsgUpdated"] = Notifications.GetToastrMessage(Enums.MessageType.error.ToString(), "User is not available to be edited");                    
                    Userslist = UserModel.GetUsers(null, null, null, null, null, null, null, null);
                    ViewData["Userslist"] = Userslist;
                    return View("PartialUserDetails");
                }

                #region get login user id info
                var currentUserIDInfo = UserModel.GetUserIDByAspNetID(HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value.ToString());
                #endregion end of get current user id info

                #region current workflow
                //declare varaibles
                int dealProcessTaskID = 0;
                int dealWorkflowID = 0;
                //get the actual workflow id to check if user has change workflow status
                string taskGUID = HttpContext.Request.Cookies["REPS_TaskGUID"].Value;
                object taskIDWorkflowIDResult = WorkflowModel.GetWorkflowIDTaskID(taskGUID);
                foreach (var resultID in taskIDWorkflowIDResult as Dictionary<string, dynamic>)
                {
                    if (resultID.Key == "TaskID")
                    {
                        dealProcessTaskID = resultID.Value; //TaskID
                    }
                    else if (resultID.Key == "TaskWorkflowID")
                    {
                        dealWorkflowID = resultID.Value;//WorkFLowID
                    }
                }
                //end of get the actual workflow id to check if user has change workflow status
                #endregion current workflow

                #region Gets Entity value from Formobjects and compare with existing in DB to know if entity has changed
                //Hemraj B Begin: Task 1476 - Checks if Entity has changed. by comparing FormObjects.entityID to existing entityId in table User
                #region variables and info from database | form object
                int FormEntityId = 0;
                int formRoleId = 0;
                int UserEntityId = 0;
                int userRoleId = 0;
                object resultUpdatedUser = "";
                string Bearer = "";
                foreach (var resultID in FormObjects as Dictionary<string, dynamic>)
                {
                    if (resultID.Key == "EntityID")
                    {
                        //FormEntityId = resultID.Value;
                        FormEntityId = Convert.ToInt32(resultID.Value);
                    }
                    if (resultID.Key == "RoleID")
                    {
                        formRoleId = Convert.ToInt32(resultID.Value);
                    }
                }
                IEnumerable<dynamic> GetUserInfo = UserModel.GetUserInfo(Convert.ToInt32(userIDByAspNetUserID), null);
                UserEntityId = Convert.ToInt32(GetUserInfo.FirstOrDefault()["EntityID"].ToString());
                userRoleId = Convert.ToInt32(GetUserInfo.FirstOrDefault()["RoleID"].ToString());
                var worflowkIDForEditedUser = Convert.ToInt32(GetUserInfo.FirstOrDefault()["WorkflowID"].ToString());
                if (GetUserInfo.FirstOrDefault()["TokenId"] != null)
                {
                    Bearer = GetUserInfo.FirstOrDefault()["TokenId"].ToString();
                }
                #endregion end of variables and info from database | form object

                #region check if user entity has been changed and user with same entity already exist, update row else add another record
                if (FormEntityId != UserEntityId)
                {
                    //check if user already exist, and edit the existing user
                    var CheckUserExist = (UserModel.GetExistingUserId(userID, FormEntityId) as IEnumerable<dynamic>).FirstOrDefault();
                    if (CheckUserExist != null)
                    {
                        RemoveUser(Convert.ToString(userID), true);
                        //Update UserID to take UserID to set active
                        ActivateUser(Convert.ToString(CheckUserExist), false);
                        FormObjects.Add("UserID", CheckUserExist);
                        FormObjects.Add("TokenId", "");
                        resultUpdatedUser = UserModel.UpdateUser(FormObjects);
                    }
                   //if no user, the add new one to db
                    else
                    {
                        FormObjects.Add("UserID", Convert.ToInt32(userIDByAspNetUserID));
                        RemoveUser(Convert.ToString(userID), true);
                        FormObjects.Add("TokenId", "");
                        resultUpdatedUser = UserModel.AddExistingUser(FormObjects);
                    }
                }
                #endregion end of check if user entity has been changed and user with same entity already exist, update row else add another record

                #region check if user role has been changed, clear token id
                else if (formRoleId != userRoleId)
                {
                    FormObjects.Add("UserID", Convert.ToInt32(userIDByAspNetUserID));
                    FormObjects.Add("TokenId", "");
                    resultUpdatedUser = UserModel.UpdateUser(FormObjects);
                    if (userIDByAspNetUserID.ToString() == currentUserIDInfo.ToString())
                    {
                        return RedirectToAction("Logout", "Login", new { tokenerror = "true" }); // Redirect to login page using controller/action
                    }
                }
                #endregion end of check if user role has been changed, clear token id

                #region Check whether user has changed workflow, if yes, clear tokenid
                else if ((worflowkIDForEditedUser.ToString() != FormObjects["DealProcessTaskID"]) && (userIDByAspNetUserID.ToString() != currentUserIDInfo.ToString()))
                {
                    FormObjects.Add("UserID", Convert.ToInt32(userIDByAspNetUserID));
                    FormObjects.Add("TokenId", "");
                    resultUpdatedUser = UserModel.UpdateUser(FormObjects);
                }
                #endregion Check whether user has changed workflow, if yes, clear tokenid

                else
                {
                    FormObjects.Add("UserID", Convert.ToInt32(userIDByAspNetUserID));
                    FormObjects.Add("TokenId", Bearer);
                    resultUpdatedUser = UserModel.UpdateUser(FormObjects);
             
                }
                // Hemraj B: End - Checks if Entity has changed. by comparing FormObjects.entityID to existing entityId in table User
                #endregion end of Gets Entity value from Formobjects and compare with existing in DB to know if entity has changed

                //if success, display message
                if (Convert.ToInt32(resultUpdatedUser) > 0)
                {
                    TempData["ToasterMsgUpdated"] = Notifications.GetToastrMessage(Enums.MessageType.success.ToString(), "User has been successfully updated");
                }
                //end of if success, display message

                #region get login user info
                string aspNetUsersId = HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value;
                IEnumerable<dynamic> ProfileUser = UserModel.GetUserInfo(Convert.ToInt32(userIDByAspNetUserID), null);
                #endregion end of get login user info

                #region Check if login user has update his entity, then redirect to login page
                if ((FormEntityId != UserEntityId) && (userIDByAspNetUserID.ToString() == currentUserIDInfo.ToString()))
                {
                    return Json(new { url = Url.Action("Index", "Login") });
                }
                #endregion end of Check if login user has update his entity, then redirect to login page

                #region chk if workflow has updated , then reset taskguid to cookies
                if (dealProcessTaskID.ToString() != FormObjects["DealProcessTaskID"])
                {
                    //check if login user has update his own info, then redirect to dashboard otherwise redirect to Admin User Page
                    if (userIDByAspNetUserID.ToString() == currentUserIDInfo.ToString())
                    {
                        HttpContext.Response.Cookies.Add(new HttpCookie("REPS_TaskGUID", ProfileUser.FirstOrDefault()["TaskGUID"].ToString()));
                        return Json(new { url = Url.Action("Index", "Dashboard") }, JsonRequestBehavior.AllowGet);
                    }
                    //end of check if login user has update his own info, then redirect to dashboard otherwise redirect to Admin User Page
                }
                #endregion end of chk if workflow has updated , then reset taskguid to cookies   

                #region get PartialUserDetails to display in form, before redirect to PartialUserDetails
                // Get all Roles
                object RolesList = RoleModel.GetRoles(null, null, null);
                ViewData["RolesList"] = RolesList;

                // Get User Info
                object UserInfo = UserModel.GetUserInfo(Convert.ToInt32(userIDByAspNetUserID));
                ViewData["UserInfo"] = UserInfo;
                ViewData["SelectedTab"] = "Users";
                // Get Users list
                Userslist = UserModel.GetUsers(null, null, null, null, null,null, null, null);
                ViewData["Userslist"] = Userslist;
                #endregion end of get PartialUserDetails to display in form, before redirect to PartialUserDetails

                return View("PartialUserDetails");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// wcf call to remove user and set deleted true
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RemoveUser(string userID, Boolean Deleted)
        {
            try
            {
                #region variables
                /// Log Start info
                Common.CLog.WriteLogInfo("Dashboard", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                object userIDByAspNetUserID = Models.UserModel.GetUserIDByAspNetID(userID);
                /// Initiate Validator
                #endregion end variables

                object resultsave = Models.UserModel.RemoveUser(Convert.ToInt32(userIDByAspNetUserID), Deleted);
                object Userslist = UserModel.GetUsers(null, null, null, null, null,null, null, null);
                ViewData["Userslist"] = Userslist;
                return View("PartialUserDetails");
                //return Index();
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// wcf call to activate user and set deleted False.
        /// Will call same function as RemoveUser
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        /// <Author>Hemraj B</Author>
        [HttpPost]
        public ActionResult ActivateUser(string userID, bool Deleted)
        {
            try
            {
                #region variables
                /// Log Start info
                Common.CLog.WriteLogInfo("Dashboard", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                /// Initiate Validator
                #endregion end variables

                object resultsave = Models.UserModel.RemoveUser(Convert.ToInt32(userID), Deleted);
                object Userslist = UserModel.GetUsers(null, null, null, null, null,null, null, null);
                ViewData["Userslist"] = Userslist;
                return View("PartialUserDetails");
                //return Index();
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Get email address if exist
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetEmailIfExist(string email)
        {
           try
                {
                //string testemail = "oahmed@e4international.com";
                    object emailExist = Models.UserModel.ValidateExistingEmail(email);
                    return Json(emailExist, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                    return null;
                }
        }
    }
}