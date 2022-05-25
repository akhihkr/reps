using Common;
using REPS.DATA.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Script.Serialization;

namespace REPS.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UserService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select UserService.svc or UserService.svc.cs at the Solution Explorer and start debugging.
    public class UserService : IUserService
    {
        /// <summary>
        /// Get User profile information
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="aspNetID"></param>
        /// <returns></returns>
        public CValidator GetUserInfo(int? userID, string aspNetID)
        {

            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.User.GetUserInfo(userID, aspNetID)), "Resource.FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }

        }

        /// <summary>
        /// Get User ID by AspNet ID
        /// </summary>
        /// <param name="aspNetID"></param>
        /// <returns></returns>
        public CValidator GetUserIDByAspNetID(string aspNetID)
        {

            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.User.GetUserIDByAspNetID(aspNetID)), "Resource.FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }

        }

        /// <summary>
        /// get user details
        /// </summary>
        /// <param name="givenName"></param>
        /// <param name="familyName"></param>
        /// <param name="identityNumber"></param>
        /// <param name="userId"></param>
        /// <param name="entityId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public CValidator GetUsers(string givenName = null, string familyName = null, string identityNumber = null, int? userId = null, int? entityId = null, int? emptyEntityId = null, int? startRow = null, int? endRow = null)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.User.GetUsers(givenName, familyName, identityNumber, userId, entityId, emptyEntityId, startRow, endRow)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }
        }

        public bool ForgotPassword(string email, string reset = "")
        {
            try
            {
                return Business.User.ForgotPassword(email, reset);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return false;
            }
        }

        /// <summary>
        /// Update User Information
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="userRoleId"></param>
        /// <returns></returns>
        public CValidator UpdateUserProfile(Dictionary<string, dynamic> formObjects)
        {
            try
            {
                User UserEntity = new User();
                UserEntity.EntityID = formObjects.ContainsKey("EntityID") ? Convert.ToInt32(formObjects["EntityID"]) : null;
                UserEntity.UserID = formObjects.ContainsKey("UserID") ? Convert.ToInt32(formObjects["UserID"]) : 0;
                UserEntity.TitleID = formObjects.ContainsKey("TitleID") ? Convert.ToInt32(formObjects["TitleID"]) : 0;
                UserEntity.GivenName = formObjects.ContainsKey("FirstName") ? formObjects["FirstName"] : null;
                UserEntity.FamilyName = formObjects.ContainsKey("LastName") ? formObjects["LastName"] : null;
                UserEntity.IdentityNumber = formObjects.ContainsKey("IdentityNumber") ? formObjects["IdentityNumber"] : null;
                UserEntity.PassportNumber = formObjects.ContainsKey("PassportNumber") ? formObjects["PassportNumber"] : null;
                UserEntity.TaxID = formObjects.ContainsKey("TaxID") ? formObjects["TaxID"] : null;
                UserEntity.PassportCountryID = formObjects.ContainsKey("PassportCountryID") ? Convert.ToInt32(formObjects["PassportCountryID"]) : null;
                UserEntity.BirthDate = DateTime.ParseExact(formObjects["BirthDate"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                UserEntity.JobTitleID = formObjects.ContainsKey("JobTitleID") ? Convert.ToInt32(formObjects["JobTitleID"]) : null;
                UserEntity.Telephone = string.IsNullOrWhiteSpace(formObjects["Telephone"]) ? null : Convert.ToDecimal(formObjects["Telephone"]);
                UserEntity.MobileNumber = string.IsNullOrWhiteSpace(formObjects["MobileNumber"]) ? null : Convert.ToDecimal(formObjects["MobileNumber"]);
                UserEntity.Email = formObjects.ContainsKey("Email") ? formObjects["Email"] : null;
                UserEntity.WorkflowID = formObjects.ContainsKey("DealProcessTaskID") ? Convert.ToInt32(formObjects["DealProcessTaskID"]) : null;

                int userId = UserEntity.UserID;

                var serializer = new JavaScriptSerializer();
                var result = Business.User.UpdateUserProfile(UserEntity, formObjects.ContainsKey("RoleID") ? Convert.ToInt32(formObjects["RoleID"]) : null);
                return CValidator.initValidator("", serializer.Serialize(result), "Resource.AddedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotAdd", false);
            }
        }

        /// <summary>
        /// Change Password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="newPassword"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool ChangePassword(string email, string newPassword, string token)
        {
            try
            {
                return Business.User.ChangePassword(email, newPassword, token);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return false;
            }
        }

        /// <summary>
        /// Verify if token is valid to change password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool VerifyUserToken(string email, string token)
        {
            try
            {
                return Business.User.VerifyUserToken(email, token);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return false;
            }
        }

        /// <summary>
        /// Get Titles
        /// </summary>
        /// <param name="titleID"></param>
        /// <returns></returns>
        public CValidator GetTitles(int? titleID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.User.GetTitles(titleID)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Create New User
        /// </summary>
        /// <param name="formObjects"></param>
        /// <returns></returns>
        public CValidator AddUser(Dictionary<string, dynamic> formObjects)
        {
            User UserEntity = new User();
            UserEntity.EntityID = formObjects.ContainsKey("EntityID") ? Convert.ToInt32(formObjects["EntityID"]) : null;
            UserEntity.WorkflowID = formObjects.ContainsKey("DealProcessTaskID") ? Convert.ToInt32(formObjects["DealProcessTaskID"]) : null;
            UserEntity.GivenName = formObjects.ContainsKey("FirstName") ? formObjects["FirstName"] : null;
            UserEntity.FamilyName = formObjects.ContainsKey("LastName") ? formObjects["LastName"] : null;
            UserEntity.IdentityNumber = formObjects.ContainsKey("IdentityNumber") ? formObjects["IdentityNumber"] : null;
            UserEntity.PassportNumber = formObjects.ContainsKey("PassportNumber") ? formObjects["PassportNumber"] : null;
            UserEntity.TaxID = formObjects.ContainsKey("TaxID") ? formObjects["TaxID"] : null;
            UserEntity.PassportCountryID = formObjects.ContainsKey("PassportCountryID") ? Convert.ToInt32(formObjects["PassportCountryID"]) : null;
            UserEntity.BirthPlace = formObjects.ContainsKey("BirthPlace") ? formObjects["BirthPlace"] : null;
            UserEntity.BirthDate = DateTime.ParseExact(formObjects["birthDateUser"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
            UserEntity.JobTitleID = formObjects.ContainsKey("JobTitleID") ? Convert.ToInt32(formObjects["JobTitleID"]) : null;
            UserEntity.Telephone = string.IsNullOrWhiteSpace(formObjects["TelephoneNumber"]) ? null : Convert.ToDecimal(formObjects["TelephoneNumber"]);
            UserEntity.FaxNumber = string.IsNullOrWhiteSpace(formObjects["FaxNumber"]) ? null : Convert.ToDecimal(formObjects["FaxNumber"]);
            //UserEntity.MobileNumber = formObjects.ContainsKey("MobileNumber") ? Convert.ToDecimal(formObjects["MobileNumber"]) : null;
            UserEntity.Email = formObjects.ContainsKey("Email") ? formObjects["Email"] : null;
            UserEntity.TitleID = formObjects.ContainsKey("TitleID") ? Convert.ToInt32(formObjects["TitleID"]) : 0;

            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.User.AddUser(UserEntity, formObjects.ContainsKey("RoleID") ? Convert.ToInt32(formObjects["RoleID"]) : null)), "Resource.FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Update User Information
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="userRoleId"></param>
        /// <returns></returns>
        public CValidator UpdateUserInfo(Dictionary<string, dynamic> formObjects)
        {

            REPS.DATA.Entity.User UserEntity = new User();
            UserEntity.EntityID = formObjects.ContainsKey("EntityID") ? Convert.ToInt32(formObjects["EntityID"]) : null;
            UserEntity.UserID = formObjects.ContainsKey("UserID") ? Convert.ToInt32(formObjects["UserID"]) : 0;
            UserEntity.TitleID = formObjects.ContainsKey("TitleID") ? Convert.ToInt32(formObjects["TitleID"]) : 0;
            UserEntity.GivenName = formObjects.ContainsKey("FirstName") ? formObjects["FirstName"] : null;
            UserEntity.FamilyName = formObjects.ContainsKey("LastName") ? formObjects["LastName"] : null;
            UserEntity.IdentityNumber = formObjects.ContainsKey("IdentityNumber") ? formObjects["IdentityNumber"] : null;
            UserEntity.PassportNumber = formObjects.ContainsKey("PassportNumber") ? formObjects["PassportNumber"] : null;
            UserEntity.TaxID = formObjects.ContainsKey("TaxID") ? formObjects["TaxID"] : null;
            UserEntity.PassportCountryID = formObjects.ContainsKey("PassportCountryID") ? Convert.ToInt32(formObjects["PassportCountryID"]) : null;
            UserEntity.BirthPlace = formObjects.ContainsKey("BirthPlace") ? formObjects["BirthPlace"] : null;
            UserEntity.BirthDate = DateTime.ParseExact(formObjects["BirthDate"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
            UserEntity.JobTitleID = formObjects.ContainsKey("JobTitleID") ? Convert.ToInt32(formObjects["JobTitleID"]) : null;
            UserEntity.Telephone = string.IsNullOrWhiteSpace(formObjects["TelephoneNumber"]) ? null : Convert.ToDecimal(formObjects["TelephoneNumber"]);
            //UserEntity.MobileNumber = formObjects.ContainsKey("MobileNumber") ? Convert.ToDecimal(formObjects["MobileNumber"]) : null;
            UserEntity.FaxNumber = string.IsNullOrWhiteSpace(formObjects["FaxNumber"]) ? null : Convert.ToDecimal(formObjects["FaxNumber"]);
            UserEntity.Email = formObjects.ContainsKey("Email") ? formObjects["Email"] : null;
            UserEntity.WorkflowID = formObjects.ContainsKey("DealProcessTaskID") ? Convert.ToInt32(formObjects["DealProcessTaskID"]) : null;
            UserEntity.TokenId = formObjects.ContainsKey("TokenId") ? formObjects["TokenId"] : null;

            string thisGuid = Guid.NewGuid().ToString();
            int userId = UserEntity.UserID;
            try
            {
                var serializer = new JavaScriptSerializer();
                //audit update
                //Business.AuditBusiness.AddAudit(new Audit() { ForeignKey = UserEntity.UserID.ToString(), TableName = "User", Description = "ActionUpdate", UserID = userId }, "UserID", Com.CString.ConvertToXMLParametersAdd(UserEntity, true));
                var result = Business.User.UpdateUserInfo(UserEntity, formObjects.ContainsKey("RoleID") ? Convert.ToInt32(formObjects["RoleID"]) : null);
                return CValidator.initValidator("", serializer.Serialize(result), "AddedSuccessfully", true);
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotAdd", false);
            }
        }

        /// <summary>
        /// Remove User
        /// </summary>
        /// <param name="titleID"></param>
        /// <returns></returns>
        public CValidator RemoveUser(int userID, bool Deleted)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.User.RemoveUser(userID, Deleted)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Create New User
        /// </summary>
        /// <param name="formObjects"></param>
        /// <returns></returns>
        /// Author: Hemraj B
        public CValidator AddExistingUser(Dictionary<string, dynamic> formObjects)
        {
            User UserEntity = new User();
            UserEntity.EntityID = formObjects.ContainsKey("EntityID") ? Convert.ToInt32(formObjects["EntityID"]) : null;
            UserEntity.WorkflowID = formObjects.ContainsKey("DealProcessTaskID") ? Convert.ToInt32(formObjects["DealProcessTaskID"]) : null;
            UserEntity.GivenName = formObjects.ContainsKey("FirstName") ? formObjects["FirstName"] : null;
            UserEntity.FamilyName = formObjects.ContainsKey("LastName") ? formObjects["LastName"] : null;
            UserEntity.IdentityNumber = formObjects.ContainsKey("IdentityNumber") ? formObjects["IdentityNumber"] : null;
            UserEntity.PassportNumber = formObjects.ContainsKey("PassportNumber") ? formObjects["PassportNumber"] : null;
            UserEntity.TaxID = formObjects.ContainsKey("TaxID") ? formObjects["TaxID"] : null;
            UserEntity.PassportCountryID = formObjects.ContainsKey("PassportCountryID") ? Convert.ToInt32(formObjects["PassportCountryID"]) : null;
            UserEntity.BirthPlace = formObjects.ContainsKey("BirthPlace") ? formObjects["BirthPlace"] : null;
            UserEntity.BirthDate = DateTime.ParseExact(formObjects["BirthDate"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
            UserEntity.JobTitleID = formObjects.ContainsKey("JobTitleID") ? Convert.ToInt32(formObjects["JobTitleID"]) : null;
            UserEntity.Telephone = string.IsNullOrWhiteSpace(formObjects["TelephoneNumber"]) ? null : Convert.ToDecimal(formObjects["TelephoneNumber"]);
            UserEntity.FaxNumber = string.IsNullOrWhiteSpace(formObjects["FaxNumber"]) ? null : Convert.ToDecimal(formObjects["FaxNumber"]);
            UserEntity.Email = formObjects.ContainsKey("Email") ? formObjects["Email"] : null;
            UserEntity.TokenId = formObjects.ContainsKey("TokenId") ? formObjects["TokenId"] : null;
            UserEntity.TitleID = formObjects.ContainsKey("TitleID") ? Convert.ToInt32(formObjects["TitleID"]) : 0;

            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.User.AddExistingUser(UserEntity, formObjects.ContainsKey("RoleID") ? Convert.ToInt32(formObjects["RoleID"]) : null)), "Resource.FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }
        }
        /// <summary>
        /// Get User profile information
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="aspNetID"></param>
        /// <returns></returns>
        public CValidator GetExistingUserId(string userID, int? EntityId)
        {

            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.User.GetExistingUserId(userID, EntityId)), "Resource.FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }

        }

        /// <summary>
        /// Check whether email exists
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public CValidator ValidateExistingEmail(string email)
        {

            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.User.ValidateExistingEmail(email)), "Resource.FetchedSuccessfully", true);
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
