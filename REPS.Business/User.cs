using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using REPS.DATA.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Runtime.Serialization;

namespace REPS.Business
{
    public class User
    {
        #region properties
        [DataMember]
        public string aspNetID { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string role { get; set; }
        [DataMember]
        public DateTime expiry { get; set; }
        [DataMember]
        public string product { get; set; }
        [DataMember]
        public string ip { get; set; }
        #endregion end of properties

        /// <summary>
        /// Get User profile Information
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="aspNetId"></param>
        /// <returns></returns>
        public static object GetUserInfo(int? userID = null, string aspNetId = null)
        {
            try
            {
                #region variables

                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;

                #endregion

                #region logic

                results = REPSDB.REPS_GetUserInfo(userID, aspNetId);
                return results;

                #endregion
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// Get User ID by AspNet ID
        /// </summary>
        /// <param name="aspNetId"></param>
        /// <returns></returns>
        public static object GetUserIDByAspNetID(string aspNetId = null)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables

                #region logic :Get User ID by AspNet ID
                results = REPSDB.REPS_GetUserID(aspNetId);
                return results;
                #endregion end of logic :Get User ID by AspNet ID
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// Get users info
        /// </summary>
        /// <param name="givenName"></param>
        /// <param name="familyName"></param>
        /// <param name="identityNumber"></param>
        /// <param name="userId"></param>
        /// <param name="entityId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static object GetUsers(string givenName, string familyName, string identityNumber, int? userId, int? entityId,int? emptyEntityId, int? startRow, int? endRow)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion

                #region logic
                {
                    results = REPSDB.REPS_GetUsers(givenName, familyName, identityNumber, userId, entityId, emptyEntityId, startRow, endRow);
                    return results;
                }
                #endregion
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        public static bool ForgotPassword(string email, string reset = "")
        {
            try
            {
                #region variables
                AuthenticateIdentityContext _dbAuth = new AuthenticateIdentityContext();
                #endregion

                #region logic

                UserStore<MyIdentityUser> userStore = new UserStore<MyIdentityUser>(_dbAuth);
                var userManager = new UserManager<MyIdentityUser>(userStore);
                var provider = new DpapiDataProtectionProvider("RepsToken");
                //userManager.UserTokenProvider = new DataProtectorTokenProvider<MyIdentityUser>(provider.Create("EmailConfirmation"));
                userManager.UserTokenProvider = new DataProtectorTokenProvider<MyIdentityUser, string>(provider.Create("EmailConfirmation")) as IUserTokenProvider<MyIdentityUser, string>;

                MyIdentityUser user = userManager.FindByEmail(email);

                //if user name match, user is returned
                if (user != null)
                {

                    // Send User email
                    //String body = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["NewUserEmailTemplate"].ToString()));
                    String body = File.ReadAllText(ConfigurationManager.AppSettings["NewUserEmailTemplate"].ToString() + "ResetPassword.html");


                    var tokendict = GenerateToken(user.Email);
                    string link = ConfigurationManager.AppSettings["MailService"].ToString() + "/Login/ResetPassword?";

                    var uriBuilder = new UriBuilder(link);
                    var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);
                    query["email"] = user.Email;
                    query["token"] = HttpUtility.UrlEncode(tokendict.ToString());
                    uriBuilder.Query = query.ToString();
                    link = uriBuilder.ToString();
                    string url = "To set your password, please follow this link:<a href=" + link + "> set password </a>";

                    var UserDetails = (GetUserInfo(null, user.Id) as IEnumerable<dynamic>).FirstOrDefault();
                    String GivenName = UserDetails.GivenName;

                    if (user.Email != null)
                    {
                        System.Threading.Tasks.Task.Factory.StartNew(() => Common.CMail.SendMail(user.Email, reset == "" ? Common.Resource.EmailSubjectForgotPassword : "Reset Password", String.Format(body, GivenName, user.Email, url)));
                        return true;
                    }

                    return true;
                }
                else
                {
                    return false;
                }
                #endregion


            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        public static object GenerateToken(string email)
        {
            #region variables
            AuthenticateIdentityContext _dbAuth = new AuthenticateIdentityContext();
            #endregion

            #region logic

            UserStore<MyIdentityUser> userStore = new UserStore<MyIdentityUser>(_dbAuth);
            var userManager = new UserManager<MyIdentityUser>(userStore);
            var provider = new DpapiDataProtectionProvider("RepsToken");
            //userManager.UserTokenProvider = new DataProtectorTokenProvider<MyIdentityUser>(provider.Create("EmailConfirmation"));
            userManager.UserTokenProvider = new DataProtectorTokenProvider<MyIdentityUser, string>(provider.Create("EmailConfirmation")) as IUserTokenProvider<MyIdentityUser, string>;

            MyIdentityUser user = userManager.FindByEmail(email);

            //if user name match, user is returned
            if (user != null)
            {
                var res = userManager.GenerateUserToken("EmailConfirmation", user.Id);
                return res;
            }
            else
            {
                return null;
            }


            #endregion
        }

        /// <summary>
        /// Update User Information
        /// </summary>
        /// <param name="workflowName">WorkFlow Name</param>
        /// <returns></returns>
        public static object UpdateUserProfile(REPS.DATA.Entity.User us, int? userRoleId = null)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion

                #region logic

                results = REPSDB.REPS_UpdateUser(us.EntityID,
                                            us.TitleID,
                                            us.GivenName,
                                            us.FamilyName,
                                            us.IdentityNumber,
                                            us.PassportNumber,
                                            us.PassportCountryID,
                                            us.TaxID,
                                            us.BirthDate,
                                            us.BirthPlace,
                                            us.Telephone,
                                            us.MobileNumber,
                                            us.Email,
                                            us.FaxNumber,
                                            us.JobTitleID,
                                            us.Verified,
                                            us.UserID,
                                            userRoleId,
                                            us.WorkflowID,
                                            us.TokenId,
                                            rowCount);

                return (rowCount.Value == DBNull.Value ? null : (int?)rowCount.Value);

                #endregion
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Change Password from Profile menu
        /// </summary>
        /// <param name="email"></param>
        /// <param name="newPassword"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool ChangeUserPasswordProfile(int userID, string currentPassword, string newPassword)
        {
            try
            {
                #region variables
                REPS.DATA.Entity.AuthenticateIdentityContext _dbAuth = new REPS.DATA.Entity.AuthenticateIdentityContext();
                #endregion

                #region logic

                UserStore<MyIdentityUser> userStore = new UserStore<MyIdentityUser>(_dbAuth);
                var userManager = new UserManager<MyIdentityUser>(userStore);
                var provider = new DpapiDataProtectionProvider("RepsToken");
                //userManager.UserTokenProvider = new DataProtectorTokenProvider<MyIdentityUser>(provider.Create("EmailConfirmation"));
                userManager.UserTokenProvider = new DataProtectorTokenProvider<MyIdentityUser, string>(provider.Create("EmailConfirmation")) as IUserTokenProvider<MyIdentityUser, string>;

                var UserDetails = (GetUserInfo(userID, null) as IEnumerable<dynamic>).FirstOrDefault();
                String UserEmail = UserDetails.Email;

                MyIdentityUser user = userManager.FindByEmail(UserEmail);

                //if user name match, user is returned
                if (user != null)
                {
                    var CheckPassword = userManager.CheckPassword(user, currentPassword);

                    if (!CheckPassword)         // Incorrect password
                    {
                        return false;
                    }

                    var succ = userManager.ChangePassword(user.Id, currentPassword, newPassword);

                    if (succ.Succeeded)
                    {
                        userManager.UpdateSecurityStamp(user.Id);
                        return true;
                    }

                    return false;

                }
                else
                {
                    return false;
                }
                #endregion


            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        public static bool ChangePassword(string email, string newPassword, string token)
        {
            try
            {
                #region variables
                AuthenticateIdentityContext _dbAuth = new AuthenticateIdentityContext();
                #endregion

                #region logic

                UserStore<MyIdentityUser> userStore = new UserStore<MyIdentityUser>(_dbAuth);
                var userManager = new UserManager<MyIdentityUser>(userStore);
                var provider = new DpapiDataProtectionProvider("RepsToken");
                //userManager.UserTokenProvider = new DataProtectorTokenProvider<MyIdentityUser>(provider.Create("EmailConfirmation"));
                userManager.UserTokenProvider = new DataProtectorTokenProvider<MyIdentityUser, string>(provider.Create("EmailConfirmation")) as IUserTokenProvider<MyIdentityUser, string>;

                MyIdentityUser user = userManager.FindByEmail(email);

                //if user name match, user is returned
                if (user != null)
                {
                    var tokenCorrect = userManager.VerifyUserToken(user.Id, "EmailConfirmation", (HttpUtility.UrlDecode(token)).ToString().Replace(" ", "+"));

                    if (tokenCorrect)
                    {
                        // do stuff if token is correct
                        userManager.RemovePassword(user.Id);
                        var rest = userManager.AddPassword(user.Id, newPassword);
                        if (rest.Succeeded)
                        {
                            userManager.UpdateSecurityStamp(user.Id);
                            return true;

                        }
                    }

                    return false;
                }
                else
                {
                    return false;
                }
                #endregion


            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// Get Title
        /// </summary>
        /// <param name="titleID"></param>
        /// <returns></returns>
        public static object GetTitles(int? titleID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables

                #region logic : Get Title
                {
                    results = REPSDB.REPS_GetTitles(titleID);
                    return results;
                }
                #endregion end of logic : Get Title
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        /// <summary>
        /// Create ASPNET User
        /// </summary>
        /// <param name="user"></param>
        /// <param name="PSUser"></param>
        /// <param name="PSUsername"></param>
        /// <returns></returns>
        public static object CreateASPUser(REPS.DATA.Entity.AspNetUsers user, REPS.DATA.Entity.User PSUser)
        {
            try
            {
                #region variables
                DATA.Entity.E4IntMgtAccEntities E4UserEntities = new DATA.Entity.E4IntMgtAccEntities();
                object results = null;

                REPS.DATA.Entity.AuthenticateIdentityContext _dbAuth = new REPS.DATA.Entity.AuthenticateIdentityContext();
                #endregion end of variables

                #region logic :  Create ASPNET User
                {

                    UserStore<MyIdentityUser> userStore = new UserStore<MyIdentityUser>(_dbAuth);
                    var userManager = new UserManager<MyIdentityUser>(userStore);
                    string SecurityStamp = Guid.NewGuid().ToString();

                    // Check if user with email already exists
                    var GetUser = userManager.FindByEmail(PSUser.Email);
                    if (GetUser != null)
                    {
                        #region variables
                        DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                        object resultsUpdateUser = null;
                        #endregion end of variables 

                        resultsUpdateUser = REPSDB.REPS_ADM_UpdateReactivateUser(
                         PSUser.EntityID,
                         PSUser.TitleID,
                         PSUser.GivenName,
                         PSUser.FamilyName,
                         PSUser.IdentityNumber,
                         PSUser.PassportNumber,
                         PSUser.PassportCountryID,
                         PSUser.TaxID,
                         PSUser.BirthDate,
                         PSUser.BirthPlace,
                         PSUser.Telephone,
                         PSUser.MobileNumber,
                         PSUser.FaxNumber,
                         PSUser.JobTitleID,
                         PSUser.Verified,
                         DateTime.Today.ToLocalTime(),
                         GetUser.Id,
                         PSUser.WorkflowID,
                         PSUser.Email,
                         DateTime.Today.ToLocalTime()
                     );

                        return -2;
                    }

                    // Copy from submitted values 
                    user.BirthDate = PSUser.BirthDate;
                    user.Email = PSUser.Email;
                    user.EmailConfirmed = false;
                    user.PasswordHash = Common.CPassword.CreateRandomPassword(8);
                    user.SecurityStamp = null;
                    user.PhoneNumber = Convert.ToString(PSUser.Telephone);
                    user.PhoneNumberConfirmed = false;
                    user.TwoFactorEnabled = false;
                    user.LockoutEnabled = false;
                    user.AccessFailedCount = 0;
                    user.UserName = PSUser.Email;
                    user.Firstname = PSUser.GivenName;
                    user.Lastname = PSUser.FamilyName;
                    user.sex = null;
                    user.Question = null;
                    user.Answer = null;
                    user.Discriminator = null;

                    results = E4UserEntities.reps_CreateAspNetUsers(
                                            user.Id,
                                            user.BirthDate,
                                            user.Email,
                                            user.EmailConfirmed,
                                            userManager.PasswordHasher.HashPassword(user.PasswordHash),
                                            SecurityStamp,//user.SecurityStamp,
                                            user.PhoneNumber,
                                            user.PhoneNumberConfirmed,
                                            user.TwoFactorEnabled,
                                            user.LockoutEndDateUtc,
                                            user.LockoutEnabled,
                                            user.AccessFailedCount,
                                            user.UserName,
                                            user.Firstname,
                                            user.Lastname,
                                            user.sex,
                                            user.Question,
                                            user.Answer,
                                            user.Discriminator,
                                            DateTime.Today.ToLocalTime(),
                                            null,
                                            false
                        );
                    return null;
                }
                #endregion end of logic :  Create ASPNET User
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        /// <summary>
        /// Add User Role
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public static int AddUserRole(int userId, int roleId)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter identity = new ObjectParameter("identity", typeof(int));
                #endregion end of variables

                #region logic
                REPSDB.REPS_AddUserRole(userId, roleId, identity);
                return (int)identity.Value; 
                #endregion end of logic : Add User Role
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Add User
        /// </summary>
        /// <param name="us"></param>
        /// <param name="userRoleId"></param>
        /// <returns></returns>
        public static object AddUser(REPS.DATA.Entity.User user, int? userRoleId = null)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                ObjectParameter identity = new ObjectParameter("identity", typeof(int));
                REPS.DATA.Entity.AspNetUsers ASPUser = new REPS.DATA.Entity.AspNetUsers();
                #endregion end of variables

                #region logic : Add User
                string userGuid = Guid.NewGuid().ToString();

                user.AspNetUsersId = userGuid;
                ASPUser.Id = userGuid;

                // Create ASP NET User
                var AspUser = CreateASPUser(ASPUser, user);

                // Check if User with email already exists
                if (Convert.ToInt32(AspUser) == -2)
                {
                    return -2;
                }

                // Create REPS  User
                results = REPSDB.REPS_ADM_AddUser(
                         user.EntityID,
                         user.TitleID,
                         user.GivenName,
                         user.FamilyName,
                         user.IdentityNumber,
                         user.PassportNumber,
                         user.PassportCountryID,
                         user.TaxID,
                         user.BirthDate,
                         user.BirthPlace,
                         user.Telephone,
                         user.MobileNumber,
                         user.Email,
                         user.FaxNumber,
                         user.JobTitleID,
                         user.Verified,
                         DateTime.Today.ToLocalTime(),
                         null,
                         false,
                         user.AspNetUsersId,
                         user.WorkflowID,
                         identity,
                         ""
                     );

                // Add User Role
                if ((int)identity.Value > 0)
                {
                    AddUserRole((int)identity.Value, (int)userRoleId);
                }

                // Send User email
                //String body = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["NewUserEmailTemplate"].ToString()));
                String body = System.IO.File.ReadAllText(ConfigurationManager.AppSettings["NewUserEmailTemplate"].ToString() + "WelcomeToReps.html");

                var tokendict = GenerateToken(user.Email);
                string link = ConfigurationManager.AppSettings["MailService"].ToString() + "/Login/ResetPassword?";

                var uriBuilder = new UriBuilder(link);
                var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);
                query["email"] = user.Email;
                query["token"] = tokendict.ToString();
                uriBuilder.Query = query.ToString();
                link = uriBuilder.ToString();
                string url = "To set your password, please follow this link:<a href=" + link + "> set password </a>";

                if (user.Email != null)
                {
                    System.Threading.Tasks.Task.Factory.StartNew(() => Common.CMail.SendMail(user.Email, Common.Resource.EmailSubjectNewUser, String.Format(body, user.GivenName, user.Email, url)));
                    return results;
                }

                return results;

                #endregion end of logic : Add User
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Update User Information
        /// </summary>
        /// <param name="workflowName">WorkFlow Name</param>
        /// <returns></returns>
        public static object UpdateUserInfo(REPS.DATA.Entity.User us, int? userRoleId = null)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion end of variables

                #region logic : Update User Information
                results = REPSDB.REPS_UpdateUser(us.EntityID,
                                            us.TitleID,
                                            us.GivenName,
                                            us.FamilyName,
                                            us.IdentityNumber,
                                            us.PassportNumber,
                                            us.PassportCountryID,
                                            us.TaxID,
                                            us.BirthDate,
                                            us.BirthPlace,
                                            us.Telephone,
                                            us.MobileNumber,
                                            us.Email,
                                            us.FaxNumber,
                                            us.JobTitleID,
                                            us.Verified,
                                            us.UserID,
                                            userRoleId,
                                            us.WorkflowID,
                                            us.TokenId,
                                            rowCount);

                return (rowCount.Value == DBNull.Value ? null : (int?)rowCount.Value);
                #endregion end of logic : Update User Information
            }
            catch (Exception Ex)
            {
                throw Ex;
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
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion end of variables

                #region logic : Remove User
                results = REPSDB.REPS_DeleteUser(userID, Deleted, rowCount);
                return (rowCount.Value == DBNull.Value ? null : (int?)rowCount.Value);
                #endregion end of logic : Remove User
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aspNetUsersId"></param>
        /// <returns></returns>
        public static object GetUsersByAspId(string aspNetUsersId)
        {
            try
            {
                #region variables

                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();

                object users = null;


                #endregion

                #region logic
                
                {
                    users = REPSDB.REPS_GetUsersByAspId(aspNetUsersId).FirstOrDefault();
                    return users;
                }
                #endregion



            }
            catch (Exception Ex)
            {

                throw Ex;
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
                AuthenticateIdentityContext _dbAuth = new AuthenticateIdentityContext();
                #endregion

                #region logic

                UserStore<MyIdentityUser> userStore = new UserStore<MyIdentityUser>(_dbAuth);
                var userManager = new UserManager<MyIdentityUser>(userStore);
                var provider = new DpapiDataProtectionProvider("RepsToken");
                userManager.UserTokenProvider = new DataProtectorTokenProvider<MyIdentityUser, string>(provider.Create("EmailConfirmation")) as IUserTokenProvider<MyIdentityUser, string>;

                MyIdentityUser user = userManager.FindByEmail(email);

                //if user name match, user is returned
                if (user != null)
                {
                    if (userManager.VerifyUserToken(user.Id, "EmailConfirmation", (HttpUtility.UrlDecode(token)).ToString().Replace(" ", "+")))
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
                #endregion
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// Add User
        /// </summary>
        /// <param name="us"></param>
        /// <param name="userRoleId"></param>
        /// <returns></returns>
        /// Author: Hemraj B
        public static object AddExistingUser(REPS.DATA.Entity.User user, int? userRoleId = null)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                ObjectParameter identity = new ObjectParameter("identity", typeof(int));
                REPS.DATA.Entity.AspNetUsers ASPUser = new REPS.DATA.Entity.AspNetUsers();

                //Hemraj B Begin:Task 1476 - Connection to E4IntMgtAccEntities db to retrieve existing UserID            
                DATA.Entity.E4IntMgtAccEntities E4UserEntities = new DATA.Entity.E4IntMgtAccEntities();
                REPS.DATA.Entity.AuthenticateIdentityContext _dbAuth = new REPS.DATA.Entity.AuthenticateIdentityContext();
                UserStore<MyIdentityUser> userStore = new UserStore<MyIdentityUser>(_dbAuth);
                var userManager = new UserManager<MyIdentityUser>(userStore);
                #endregion end of variables

                #region logic : Add New User using existing ASP ID
                string userGuid = Guid.NewGuid().ToString();

                // Check if user with email already exists
                var GetUser = userManager.FindByEmail(user.Email);

                user.AspNetUsersId = userGuid;
                ASPUser.Id = userGuid;

                //var UserDetails = (GetUserInfo(null, Convert.ToString(GetUser.Id)) as IEnumerable<dynamic>).FirstOrDefault();
                //String existingUserId = UserDetails.AspNetUsersId;

                results = REPSDB.REPS_ADM_AddUser(
                     user.EntityID,
                     user.TitleID,
                     user.GivenName,
                     user.FamilyName,
                     user.IdentityNumber,
                     user.PassportNumber,
                     user.PassportCountryID,
                     user.TaxID,
                     user.BirthDate,
                     user.BirthPlace,
                     user.Telephone,
                     user.MobileNumber,
                     user.Email,
                     user.FaxNumber,
                     user.JobTitleID,
                     user.Verified,
                     DateTime.Today.ToLocalTime(),
                     null,
                     false,
                      //existingUserId,
                      GetUser.Id,
                     user.WorkflowID,
                     identity,
                     user.TokenId
                 );

                // Add User Role
                if ((int)identity.Value > 0)
                {
                    //Hemraj B - Return User Role Id
                    var ReturnUserRole = AddUserRole((int)identity.Value, (int)userRoleId);
                    if (Convert.ToUInt32(ReturnUserRole) > 0)
                    {
                        return Convert.ToInt32(ReturnUserRole);
                    }
                    //Hemraj B - Return User Role Id
                }

                return results;
            }

            #endregion end of logic : Add New User using existing ASP ID

            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Get User profile Information
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="aspNetId"></param>
        /// <returns></returns>
        public static object GetExistingUserId(string UserId = null, int? EntityId = null)
        {
            try
            {
                #region variables

                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;

                #endregion

                #region logic

                //results = REPSDB.REPS_GetUserInfo(UserId, EntityId);
                results = REPSDB.REPS_GetExistingUser(UserId, EntityId);
                return results;

                #endregion
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }


        /// <summary>
        /// Check whether email exists
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static object ValidateExistingEmail(string email)
        {
            try
            {
                #region variables
                AuthenticateIdentityContext _dbAuth = new AuthenticateIdentityContext();
                #endregion

                #region logic

                UserStore<MyIdentityUser> userStore = new UserStore<MyIdentityUser>(_dbAuth);
                var userManager = new UserManager<MyIdentityUser>(userStore);
                var provider = new DpapiDataProtectionProvider("RepsToken");
                //userManager.UserTokenProvider = new DataProtectorTokenProvider<MyIdentityUser>(provider.Create("EmailConfirmation"));
                userManager.UserTokenProvider = new DataProtectorTokenProvider<MyIdentityUser, string>(provider.Create("EmailConfirmation")) as IUserTokenProvider<MyIdentityUser, string>;

                MyIdentityUser user = userManager.FindByEmail(email);
                //if user name match, user is returned
                if (user != null)
                {
                    return 9;
                }
                else
                {
                    return 10;
                }
                #endregion logic
            }

            catch (Exception Ex)
            {

                throw Ex;
            }
        }

    }
}
