using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Script.Serialization;

namespace REPS.UI.Models
{
    public class RoleModel
    {
        /// <summary>
        /// Get Roles
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static object GetRoles(int? roleId, int? startRow, int? endRow)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables

                #region logic : Get Roles
                using (RoleServiceReference.RoleServiceClient roleServiceClient = new RoleServiceReference.RoleServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(roleServiceClient.InnerChannel))
                    {
                        resultValidator = roleServiceClient.GetRoles(roleId == null ? null : roleId, startRow == null ? null : startRow, endRow == null ? null : endRow);
                        var output = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        return output;
                    }
                }
                #endregion end of logic : Get Roles
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }



        /// <summary>
        /// Get User Info
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static object[] GetUserRoleActions(int userID)
        {
            try
            {
                #region Variables
                Common.CValidator resultValidator = null;

                #endregion end vaiables

                /// Call WCF to get user information
                using (RoleServiceReference.RoleServiceClient roleServiceClient = new RoleServiceReference.RoleServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(roleServiceClient.InnerChannel))
                    {
                        resultValidator = roleServiceClient.GetUserRoleActions(null, userID, null, null);
                        var outputServalCall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            return outputServalCall;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Add Role
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public static object AddRole(string roleName)
        {
            #region variables
            Common.CValidator resultValidator = null;
            #endregion end variables

            #region logic
            using (RoleServiceReference.RoleServiceClient roleServiceClient = new RoleServiceReference.RoleServiceClient())
            {
                using (new System.ServiceModel.OperationContextScope((System.ServiceModel.IClientChannel)roleServiceClient.InnerChannel))
                {
                    resultValidator = roleServiceClient.AddRole(roleName);
                    var output = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                    return output;
                }
            }
            #endregion 
        }

        /// <summary>
        /// Update Role
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public static object UpdateRole(int roleID, string roleName)
        {
            #region variables
            Common.CValidator resultValidator = null;
            #endregion end variables

            #region logic
            using (RoleServiceReference.RoleServiceClient roleServiceClient = new RoleServiceReference.RoleServiceClient())
            {
                using (new System.ServiceModel.OperationContextScope((System.ServiceModel.IClientChannel)roleServiceClient.InnerChannel))
                {
                    resultValidator = roleServiceClient.UpdateRole(roleID, roleName);
                    if (resultValidator != null && resultValidator.success == true && resultValidator.output.ToString() != null && !Common.CString.CheckNullOrEmpty(resultValidator.output))
                    {
                        var output = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        return output;
                    }
                    else
                    {
                        return resultValidator.guid;
                    }
                }
            }
            #endregion 
        }

        /// <summary>
        /// Update Role
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public static object LogoutUserOnRoleChange(int roleID)
        {
            #region variables
            Common.CValidator resultValidator = null;
            #endregion end variables

            #region logic
            using (RoleServiceReference.RoleServiceClient roleServiceClient = new RoleServiceReference.RoleServiceClient())
            {
                using (new System.ServiceModel.OperationContextScope((System.ServiceModel.IClientChannel)roleServiceClient.InnerChannel))
                {
                    resultValidator = roleServiceClient.LogoutUserOnRoleChange(roleID);
                    var output = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                    return output;
                }
            }
            #endregion 
        }

        /// <summary>
        /// Get all Role Actions 
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public static object GetAllRoleActions(int roleID)
        {
            #region variables
            Common.CValidator resultValidator = null;
            #endregion end variables

            #region logic
            using (RoleServiceReference.RoleServiceClient roleServiceClient = new RoleServiceReference.RoleServiceClient())
            {
                using (new System.ServiceModel.OperationContextScope((System.ServiceModel.IClientChannel)roleServiceClient.InnerChannel))
                {
                    resultValidator = roleServiceClient.GetAllRoleActions(roleID);
                    var output = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                    return output;
                }
            }
            #endregion 
        }

        /// <summary>
        /// Assign/Unassign UserAction to Role
        /// </summary>
        /// <param name="actionID"></param>
        /// <param name="roleID"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public static object AssignRoleActionToRole(int actionID, int roleID, bool isActive)
        {
            #region variables
            Common.CValidator resultValidator = null;
            #endregion end variables

            #region logic
            using (RoleServiceReference.RoleServiceClient roleServiceClient = new RoleServiceReference.RoleServiceClient())
            {
                using (new System.ServiceModel.OperationContextScope((System.ServiceModel.IClientChannel)roleServiceClient.InnerChannel))
                {
                    resultValidator = roleServiceClient.AssignRoleActionToRole(roleID, actionID, isActive);
                    var output = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                    return output;
                }
            }
            #endregion
        }
    }
}