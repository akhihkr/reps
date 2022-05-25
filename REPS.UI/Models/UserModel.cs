using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Script.Serialization;

namespace REPS.UI.Models
{
    public class UserModel
    {
        /// <summary>
        /// Get User Info
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static object[] GetUserInfo(int? userID, string aspNetID)
        {
            try
            {
                #region Variables
                Common.CValidator resultValidator = null;

                #endregion end vaiables

                /// Call WCF to get user information
                using (UserServiceReference.UserServiceClient userServiceClient = new UserServiceReference.UserServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(userServiceClient.InnerChannel))
                    {
                        resultValidator = userServiceClient.GetUserInfo(userID, aspNetID);
                        var outputServalCall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            ///Created Deal id save into Sessin for reuse
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
        /// Get User ID BY AspNetID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static object GetUserIDByAspNetID(string aspNetID)
        {
            try
            {
                #region Variables
                Common.CValidator resultValidator = null;
                #endregion end vaiables

                /// Call WCF to get user information
                using (UserServiceReference.UserServiceClient userServiceClient = new UserServiceReference.UserServiceClient())
                { //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(userServiceClient.InnerChannel))
                    {
                        resultValidator = userServiceClient.GetUserIDByAspNetID(aspNetID);
                        if (resultValidator != null && resultValidator.success == true && resultValidator.output.ToString() != null && !Common.CString.CheckNullOrEmpty(resultValidator.output) && !resultValidator.output.Equals("[]"))
                        {
                            ///Created Deal id save into Sessin for reuse
                            var outputServalCall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                            return outputServalCall[0];
                        }
                        else
                        {
                            return null;
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
        /// Get Users List
        /// </summary>
        /// <param name="givenName"></param>
        /// <param name="familyName"></param>
        /// <param name="identityNumber"></param>
        /// <param name="userId"></param>
        /// <param name="entityId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static object GetUsers(string givenName, string familyName, string identityNumber, int? userId = null, int? entityId = null,int? emptyEntityId=null, int? startRow = null, int? endRow = null)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables

                /// Call wcf to get users list
                #region wcf to get users list 
                using (UserServiceReference.UserServiceClient userServiceClient = new UserServiceReference.UserServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(userServiceClient.InnerChannel))
                    {
                        resultValidator = userServiceClient.GetUsers(givenName == null ? null : givenName, familyName == null ? null : familyName, identityNumber == null ? null : identityNumber, userId, entityId, emptyEntityId == null ? null : emptyEntityId, startRow == null ? null : startRow, endRow == null ? null : endRow);
                        var outputServalCall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            ///Created Deal id save into Sessin for reuse
                            return outputServalCall;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="reset"></param>
        /// <returns></returns>
        public static bool ForgotPassword(string email, string reset = "")
        {
            try
            {
                #region variables
                bool result;
                #endregion end variables

                /// Call wcf to change password
                #region wcf to create user
                using (UserServiceReference.UserServiceClient userServiceClient = new UserServiceReference.UserServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(userServiceClient.InnerChannel))
                    {
                        return result = userServiceClient.ForgotPassword(email, reset);
                    }
                }
                #endregion end of wcf to create user
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Update User Profile Information 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static int? UpdateUserProfile(Dictionary<string, dynamic> formObjects)
        {
            try
            {
                #region Variables
                Common.CValidator resultValidator = null;
                #endregion end vaiables

                /// Call WCF to update user information
                using (UserServiceReference.UserServiceClient userServiceClient = new UserServiceReference.UserServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(userServiceClient.InnerChannel))
                    {
                        resultValidator = userServiceClient.UpdateUserProfile(formObjects);
                       
                        if (resultValidator != null && resultValidator.success == true && resultValidator.output.ToString() != null)
                        {
                            var outputServalCall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                            ///Created Deal id save into Sessin for reuse
                            return outputServalCall;
                        }
                        else
                        {
                            throw new Common.CWCFException("Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name +"- "+System.Reflection.MethodBase.GetCurrentMethod().DeclaringType + "--");            
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
        /// Update User Profile Security Information 
        /// </summary>
        /// <param name="formObjects"></param>
        /// <returns></returns>
        public static bool UpdateUserProfileSecurity(Dictionary<string, dynamic> formObjects)
        {
            try
            {
                #region Variables
                bool PasswordChangeResult;
                #endregion end variables

                int UserID = formObjects["UserID"];
                string CurrentPassword = formObjects["CurrentPassword"];
                string NewPassword = formObjects["NewPassword"];

                /// Call WCF authenticate
                using (AuthenticateServiceReference.AuthenticateServiceClient authenticateClient = new AuthenticateServiceReference.AuthenticateServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(authenticateClient.InnerChannel))
                    {
                        return PasswordChangeResult = authenticateClient.DoChangePassword(UserID, CurrentPassword, NewPassword);
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
        /// Reset user password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="newPassword"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool ResetPassword(string email, string newPassword, string token)
        {
            try
            {
                #region variables
                bool result;
                #endregion end variables

                /// Call wcf to reset password
                #region wcf to reset password
                using (UserServiceReference.UserServiceClient userServiceClient = new UserServiceReference.UserServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(userServiceClient.InnerChannel))
                    {
                        return result = userServiceClient.ChangePassword(email, newPassword, token);
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }
        
        /// <summary>
        /// Verify if token is valid to change password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool VerifyUserToken(string email, string token)
        {
            try
            {
                #region variables
                bool result;
                #endregion end variables

                /// Call wcf to verify user token
                using (UserServiceReference.UserServiceClient userServiceClient = new UserServiceReference.UserServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(userServiceClient.InnerChannel))
                    {
                        return result = userServiceClient.VerifyUserToken(email, token);
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
        /// Get Titles
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static object GetTitles(int? titleID)
        {
            try
            {
                #region Variables
                Common.CValidator resultValidator = null;

                #endregion end vaiables

                /// Call WCF to get user information
                using (UserServiceReference.UserServiceClient userServiceClient = new UserServiceReference.UserServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(userServiceClient.InnerChannel))
                    {
                        resultValidator = userServiceClient.GetTitles(titleID);
                        var output = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        return output;
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
        /// Create New User
        /// </summary>
        /// <param name="formObjects"></param>
        /// <returns></returns>
        public static object AddNewUser(Dictionary<string, dynamic> formObjects)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables

                /// Call wcf to create user
                #region wcf to create user
                using (UserServiceReference.UserServiceClient userServiceClient = new UserServiceReference.UserServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(userServiceClient.InnerChannel))
                    {
                        resultValidator = userServiceClient.AddUser(formObjects);
                        
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null && !Common.CString.CheckNullOrEmpty(resultValidator.output))
                        {
                            var outputResult = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                            ///Created Deal id save into Sessin for reuse
                            return outputResult;
                        }
                        else
                        {
                            return resultValidator.guid;
                        }
                    }
                }
                #endregion end of wcf to create user
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
        public static object GetUserInfo(int userID)
        {
            try
            {
                #region Variables
                Common.CValidator resultValidator = null;
                #endregion end vaiables

                /// Call WCF to get user information
                using (UserServiceReference.UserServiceClient userServiceClient = new UserServiceReference.UserServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(userServiceClient.InnerChannel))
                    {
                        resultValidator = userServiceClient.GetUserInfo(userID, null);
                        
                        if (resultValidator != null && resultValidator.success == true && resultValidator.output.ToString() != null && !Common.CString.CheckNullOrEmpty(resultValidator.output) && !resultValidator.output.Equals("[]"))
                        {
                            var outputResult = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                            ///Created Deal id save into Sessin for reuse
                            return outputResult;
                        }
                        else
                        {
                            return null;
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
        /// Update User Info
        /// </summary>
        /// <param name="formObjects"></param>
        /// <returns></returns>
        public static object UpdateUser(Dictionary<string, dynamic> formObjects)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables

                /// Call wcf to create user
                #region wcf to create user
                using (UserServiceReference.UserServiceClient userServiceClient = new UserServiceReference.UserServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(userServiceClient.InnerChannel))
                    {
                        resultValidator = userServiceClient.UpdateUserInfo(formObjects);
                        var outputResult = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null && !Common.CString.CheckNullOrEmpty(resultValidator.output))
                        {
                            ///Created Deal id save into Sessin for reuse
                            return outputResult;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Remove User
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static object RemoveUser(int userID, bool Deleted)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end of variables

                #region Logic : Remove User
                /// Call WCF to remove User
                using (UserServiceReference.UserServiceClient userServiceClient = new UserServiceReference.UserServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(userServiceClient.InnerChannel))
                    {
                        resultValidator = userServiceClient.RemoveUser(userID, Deleted);
                        var outputResult = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null && !Common.CString.CheckNullOrEmpty(resultValidator.output))
                        {
                            return outputResult;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end of logic : Remove User
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Create New User
        /// </summary>
        /// <param name="formObjects"></param>
        /// <returns></returns>
        /// Author: Hemraj B
        public static object AddExistingUser(Dictionary<string, dynamic> formObjects)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables

                /// Call wcf to create user
                #region wcf to create user
                using (UserServiceReference.UserServiceClient userServiceClient = new UserServiceReference.UserServiceClient())
                {
                    resultValidator = userServiceClient.AddExistingUser(formObjects);
                    var outputResult = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                    if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null && !Common.CString.CheckNullOrEmpty(resultValidator.output))
                    {
                        ///Created Deal id save into Sessin for reuse
                        return outputResult;
                    }
                    else
                    {
                        throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                    }
                }
                #endregion end of wcf to create user
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
        public static object[] GetExistingUserId(string userID, int? EntityId)
        {
            try
            {
                #region Variables
                Common.CValidator resultValidator = null;

                #endregion end vaiables

                /// Call WCF to get user information
                using (UserServiceReference.UserServiceClient userServiceClient = new UserServiceReference.UserServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(userServiceClient.InnerChannel))
                    {
                        resultValidator = userServiceClient.GetExistingUserId(userID, EntityId);
                        var outputServalCall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            ///Created Deal id save into Sessin for reuse
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
        /// Get User Info
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static int ValidateExistingEmail(string email)
        {
            try
            {
                #region Variables
                Common.CValidator resultValidator = null;

                #endregion end vaiables

                /// Call WCF to get user information
                using (UserServiceReference.UserServiceClient userServiceClient = new UserServiceReference.UserServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(userServiceClient.InnerChannel))
                    {
                        resultValidator = userServiceClient.ValidateExistingEmail(email);
                        var outputServalCall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            ///Created Deal id save into Sessin for reuse
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

    }
}