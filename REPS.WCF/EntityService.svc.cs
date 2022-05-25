using Common;
using REPS.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Script.Serialization;

namespace REPS.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EntityService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select EntityService.svc or EntityService.svc.cs at the Solution Explorer and start debugging.
    public class EntityService : IEntityService
    {
        /// <summary>
        /// Get Entities
        /// </summary>
        /// <param name="name"></param>
        /// <param name="legalName"></param>
        /// <param name="registrationNumber"></param>
        /// <param name="entityID"></param>
        /// <returns></returns>
        public CValidator GetEntities(string name = null, string legalName = null, string registrationNumber = null, int? entityID = null, int? emptyEntityId = null)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Entity.GetEntities(name, legalName, registrationNumber, entityID, emptyEntityId)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        #region add entity
        /// <summary>
        /// add entity to db
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public CValidator AddEntity(DATA.Entity.Entity obj)
        {
            try
            {
                //variables
                //IncomingWebRequestContext request = WebOperationContext.Current.IncomingRequest;
                //System.Net.WebHeaderCollection headers = request.Headers;
                //int userId = Convert.ToInt32(headers["UserID"]);
                var serializer = new JavaScriptSerializer();
                int? result;
                //end of variables

                result = Entity.AddEntity(obj);
                return CValidator.initValidator("", serializer.Serialize(result), "AddedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotAdd", false);
            }

        }
        #endregion end of add entity

        #region update Entity details 
        /// <summary>
        /// update Entity details 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public CValidator UpdateEntity(int userId, DATA.Entity.Entity obj)
        {
            int? result;
            var serializer = new JavaScriptSerializer();
            try
            {
                result = Entity.UpdateEntity(obj);
                //audit Add                
                //AuditBusiness.AddAudit(new DATA.Entity.Audit() { ForeignKey = result.ToString(), TableName = "Entity", Description = "ActionUpdate", UserID = userId }, "EntityID", Utilities.CString.ConvertToXMLParametersAdd(obj, true));
                return CValidator.initValidator("", serializer.Serialize(result), "AddedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }
        #endregion end of update Entity details

        /// <summary>
        /// Get Participant ID by ParticipantGUID
        /// </summary>
        /// <param name="participantGUID"></param>
        /// <returns></returns>
        public CValidator GetEntityIDByEntityGUID(string entityGUID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Entity.GetEntityID(entityGUID)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        #region remove entity per id
        /// <summary>
        /// remove entity per id
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public CValidator DeleteEntity(DATA.Entity.Entity obj)
        {
            try
            {
                //variables
                var serializer = new JavaScriptSerializer();
                int? result;
                //IncomingWebRequestContext request = WebOperationContext.Current.IncomingRequest;
                //System.Net.WebHeaderCollection headers = request.Headers;
                //int userId = Convert.ToInt32(headers["UserID"]);
                //end of variables 

                if ((result = Entity.DeleteEntity(obj)) > 0)
                {

                    //audit delete
                    //AuditBusiness.AddAudit(new DATA.Entity.Audit() { ForeignKey = obj.EntityID.ToString(), TableName = "Entity", Description = "ActionDelete", UserID = userId, Deleted = true }, "EntityID", "<Parameters></Parameters>");
                    //end of audit delete
                    return CValidator.initValidator("", serializer.Serialize(result), "DeleteSuccess", true);

                }
                else
                {
                    string thisGuid = Guid.NewGuid().ToString();
                    CLog.WriteLogInfo(thisGuid, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                    return CValidator.initValidator(thisGuid, result.ToString(), "Deletefail", false);
                }
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }
        #endregion end of remove entity per id

        /// <summary>
        /// Remove User from deleted entity
        /// </summary>
        /// <param name="entityID"></param>
        /// <returns></returns>
        public CValidator RemoveUserFromDeletedEntity(int entityID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Entity.RemoveUserFromDeletedEntity(entityID)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }
    }
}
