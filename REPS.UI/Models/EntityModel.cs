using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using REPS.UI.EntityServiceReference;
using REPS.DATA.Entity;
using System.ServiceModel;

namespace REPS.UI.Models
{
    public class EntityModel
    {
        /// <summary>
        /// Get Entities
        /// </summary>
        /// <param name="name"></param>
        /// <param name="legalName"></param>
        /// <param name="registrationNumber"></param>
        /// <param name="entityID"></param>
        /// <returns></returns>
        public static object GetEntities(string name = null, string legalName = null, string registrationNumber = null, int? entityID = null,int? emptyEntityId = null)
        {
            try
            {
                #region Variables
                Common.CValidator resultValidator = null;

                #endregion end vaiables

                /// Call WCF to get user information
                using (EntityServiceReference.EntityServiceClient entityServiceClient = new EntityServiceReference.EntityServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(entityServiceClient.InnerChannel))
                    {
                        resultValidator = entityServiceClient.GetEntities(name, legalName, registrationNumber, entityID, emptyEntityId);
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
        /// wcf call to add entity
        /// </summary>
        /// <param name="formKeysOrg"></param>
        /// <returns></returns>
        #region wcf call to add entity
        public static object AddEntity(Entity formKeysOrg)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables

                #region ServerCAll Organisation Add       
                /// Call WCF 
                using (EntityServiceReference.EntityServiceClient entityServiceClient = new EntityServiceReference.EntityServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(entityServiceClient.InnerChannel))
                    {
                        resultValidator = entityServiceClient.AddEntity(formKeysOrg);
                       
                        if (resultValidator != null && resultValidator.success == true && resultValidator.output.ToString() != null && !Common.CString.CheckNullOrEmpty(resultValidator.output))
                        {
                            var outputServalCall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                            return outputServalCall;
                        }
                        else
                        {
                            return resultValidator.guid;
                        }
                    }
                }
                #endregion End ServerCALL Organisation Add
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }
        #endregion end of wcf call to add entity

        /// <summary>
        /// Get Participant ID by ParticipantGUID
        /// </summary>
        /// <param name="aspNetId"></param>
        /// <returns></returns>
        public static object GetEntityIDByEntityGUID(string entityGUID)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables 

                #region logic Get Entity ID by EntityGUID
                using (EntityServiceReference.EntityServiceClient entityServiceClient = new EntityServiceReference.EntityServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(entityServiceClient.InnerChannel))
                    {
                        resultValidator = entityServiceClient.GetEntityIDByEntityGUID(entityGUID);
                        var entityID = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            return entityID;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end of logic Get Entity ID by EntityGUID
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// update Entity details 
        /// </summary>
        /// <param name="formKeysOrg"></param>
        /// <returns></returns>
        public static object UpdateEntity(int userid, Entity formKeysOrg)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables
                #region ServerCAll Organisation Add       
                /// Call WCF 
                using (EntityServiceReference.EntityServiceClient entityServiceClient = new EntityServiceReference.EntityServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(entityServiceClient.InnerChannel))
                    {
                        resultValidator = entityServiceClient.UpdateEntity(userid, formKeysOrg);
                        var outputServalCall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null && !Common.CString.CheckNullOrEmpty(resultValidator.output))
                        {
                            return outputServalCall;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion End ServerCALL Organisation Add
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// wcf call to remove entity and set delete to 1
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="entityInput"></param>
        /// <returns></returns>
        public static string RemoveEntity(Entity entityInput)
        {
            try
            {
                #region Variables
                Common.CValidator resultValidator = null;
                #endregion end vaiables
                ///remove participant set delete to 1
                #region remove Entity set delete to 1
                using (EntityServiceReference.EntityServiceClient entityServiceClient = new EntityServiceReference.EntityServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(entityServiceClient.InnerChannel))
                    {
                        resultValidator = entityServiceClient.DeleteEntity(entityInput);                    
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null && !Common.CString.CheckNullOrEmpty(resultValidator.output))
                        {
                            //var outputServalCall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                            //return outputServalCall.ToString();
                            return resultValidator.reason;
                        }
                        else
                        {
                            return resultValidator.guid;
                        }
                    }
                }
                #endregion remove Entity set delete to 1
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
        /// <param name="entityID"></param>
        /// <returns></returns>
        public static object RemoveUserFromDeletedEntity(int entityID)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end of variables

                #region Logic : Remove User
                /// Call WCF to remove User
                using (EntityServiceReference.EntityServiceClient entityServiceClient = new EntityServiceReference.EntityServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(entityServiceClient.InnerChannel))
                    {
                        resultValidator = entityServiceClient.RemoveUserFromDeletedEntity(entityID);
                        
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null && !Common.CString.CheckNullOrEmpty(resultValidator.output))
                        {
                            //var outputResult = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                            //return outputResult;
                            return resultValidator.reason;
                        }
                        else
                        {
                            return resultValidator.guid;
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
    }
}