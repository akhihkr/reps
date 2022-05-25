using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using REPS.UI.Models;
using Global;

namespace REPS.UI.Controllers
{
    [TokenAuthorizeAttribute]
    public class AdminRolesController : Controller
    {
        // GET: AdminRoles
        public ActionResult Index(bool partial = false, bool showErrorDiv = false)
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

                ViewData["SelectedLayout"] = "Admin";
                
                // Get all Roles
                object RolesList = RoleModel.GetRoles(null, null, null);
                ViewData["RolesList"] = RolesList;

                if (showErrorDiv)
                {
                    Response.Write("<div id='my-div' data-info=" + (int)Enums.ValidationCheck.reusltOutput + "></div>");
                }
                if (partial)
                {
                    return PartialView("PartialAdminRolesList");
                }
                return View();
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Get Add Role Popup
        /// </summary>
        /// <returns></returns>
        public ActionResult PopupAddRole()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Get Edit Role Popup
        /// </summary>
        /// <returns></returns>
        public ActionResult PopupEditRole(int roleID, string roleName)
        {
            try
            {
                ViewData["RoleName"] = roleName;
                ViewData["RoleID"] = roleID;

                // Get all Role Actions
                object RoleActionsList = RoleModel.GetAllRoleActions(roleID);
                ViewData["RoleActionsList"] = RoleActionsList;

                return View();
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Add Role
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddRole()
        {
            try
            {
                // Call WCF to add new Role
                object AddRoleResult = Models.RoleModel.AddRole(Request.Form["RoleName"]);
                
                return RedirectToAction("Index", new { partial = true });
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        public ActionResult EditRole(int roleID)
        {
            try
            {
                // Call WCF to add new Role
                object editRoleResult = Models.RoleModel.UpdateRole(roleID, Request.Form["RoleName"]);
                Guid guidOutput;
                bool showErrorDiv = false;
                bool isGuid = Guid.TryParse(editRoleResult.ToString(), out guidOutput);

                if (isGuid)
                {
                    showErrorDiv = true;
                }
                return RedirectToAction("Index", new { partial = true, showErrorDiv = showErrorDiv });
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        public ActionResult LogoutUserOnRoleChange(int roleID)
        {
            try
            {
                // Call WCF to logout all users
                object EditRoleResult = Models.RoleModel.LogoutUserOnRoleChange(roleID);

                return Content("ok");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Assign/Unassign UserAction to Role
        /// </summary>
        /// <param name="actionID"></param>
        /// <param name="roleID"></param>
        /// <param name="isActive"></param>
        [HttpPost]
        public void AssignRoleActionToRole(int actionID, int roleID, bool isActive)
        {
            try
            {
                object RoleActionObject = Models.RoleModel.AssignRoleActionToRole(actionID, roleID, isActive);
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            }
        }

        /// <summary>
        /// Check if role name already exists
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CheckIfRoleExists(string name, int? parameter)
        {
            // Get all Roles
            object RolesList = RoleModel.GetRoles(null, null, null);
            int roleFoundInList = 0;

            // Check if the role we are looking for already exists
            foreach (var role in RolesList as IEnumerable<dynamic>)
            {
                if (role["Description"].Trim().ToString().ToLower() == name.ToLower())
                {
                    roleFoundInList++;
                }
            }

            if (roleFoundInList > 0)
                return Content("ko"); // if role exists we return 'ko'
            else
                return Content("ok"); // else 'ok' to proceed with the form submission
        }
    }
}