using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPS.Business
{
    public class Role
    {
        /// <summary>
        /// Get User Role Actions
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static object GetUserRoleActions(int? roleId, int? userId, int? startRow, int? endRow)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables

                #region logic
                {
                    results = REPSDB.REPS_GetUserRoleActions(roleId, userId, startRow, endRow);
                    return results;
                }
                #endregion
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Get roles
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static object GetRoles(int? roleId, int? startRow, int? endRow)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables

                #region logic : Get roles
                {
                    results = REPSDB.REPS_GetRoles(roleId, startRow, endRow);
                    return results;
                }
                #endregion end of logic : Get roles
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Assign Role Action To Role
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="UserActionID"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public static object AssignRoleActionToRole(int? roleId, int? UserActionID, bool isActive)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion end of variables

                #region logic
                {
                    results = REPSDB.REPS_ADM_AssignRoleActionToRole(roleId, UserActionID, isActive, rowCount);
                    return (rowCount.Value == DBNull.Value ? null : (int?)rowCount.Value);
                }
                #endregion
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

        }

        /// <summary>
        /// Add Role
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public static object AddRole(string roleName)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter identity = new ObjectParameter("identity", typeof(int));
                object results = null;
                #endregion end of variables

                #region logic
                {
                    results = REPSDB.REPS_ADM_AddRole(roleName, identity);
                    return (identity.Value == null ? null : identity.Value);
                }
                #endregion
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

        }

        /// <summary>
        /// Update Role
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public static int? UpdateRole(int roleID, string roleName)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion end of variables

                #region logic
                {
                    results = REPSDB.REPS_ADM_UpdateRole(roleID, roleName, rowCount);
                    return (rowCount.Value == DBNull.Value ? null : (int?)rowCount.Value);
                }
                #endregion
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

        }

        /// <summary>
        /// Logout user on role change
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public static int? LogoutUserOnRoleChange(int roleID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion end of variables

                #region logic
                {
                    results = REPSDB.REPS_ADM_UpdateLogoutUserOnRoleChange(roleID, rowCount);
                    return (rowCount.Value == DBNull.Value ? null : (int?)rowCount.Value);
                }
                #endregion
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

        }

        /// <summary>
        /// Get All Role Actions
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public static object GetAllRoleActions(int? roleID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables

                #region logic
                {
                    results = REPSDB.REPS_ADM_GetAllRoleActions(roleID);
                    return results;
                }
                #endregion
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
    }
}
