using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Script.Serialization;

namespace REPS.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RoleService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select RoleService.svc or RoleService.svc.cs at the Solution Explorer and start debugging.
    public class RoleService : IRoleService
    {
        /// <summary>
        /// Get User Role Actions
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public CValidator GetUserRoleActions(int? roleId, int? userId, int? startRow, int? endRow)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Role.GetUserRoleActions(roleId, userId, startRow, endRow)), "Resource.FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Get roles
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public CValidator GetRoles(int? roleId, int? startRow, int? endRow)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Role.GetRoles(roleId, startRow, endRow)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        
        /// <summary>
        /// Assign Role Action To Role
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="UserActionID"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public CValidator AssignRoleActionToRole(int? roleId, int? UserActionID, bool isActive)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Role.AssignRoleActionToRole(roleId, UserActionID, isActive)), "Resource.FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Add Role
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public CValidator AddRole(string roleName)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Role.AddRole(roleName)), "Resource.FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Update Role
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public CValidator UpdateRole(int roleID, string roleName)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                string thisGuid = Guid.NewGuid().ToString();;
                int? result;
                
                //return CValidator.initValidator("", serializer.Serialize(Business.Role.UpdateRole(roleID, roleName)), "Resource.FetchedSuccessfully", true);
                if ((result = Business.Role.UpdateRole(roleID, roleName)) > 0)
                {
                    return CValidator.initValidator("", result.ToString(), "FetchedSuccessfully", true);
                }
                else
                {
                    return CValidator.initValidator(thisGuid, result.ToString(), "CouldNotUpdate", false);
                }
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Logout user on role change
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public CValidator LogoutUserOnRoleChange(int roleID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Role.LogoutUserOnRoleChange(roleID)), "Resource.FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Get all role Actions
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public CValidator GetAllRoleActions(int roleID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Role.GetAllRoleActions(roleID)), "Resource.FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();

                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }
        }
    }
}
