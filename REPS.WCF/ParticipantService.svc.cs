using Common;
using Global;
using REPS.DATA.Entity;
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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ParticipantService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ParticipantService.svc or ParticipantService.svc.cs at the Solution Explorer and start debugging.
    public class ParticipantService : IParticipantService
    {
        /// <summary>
        /// get all participants types 
        /// </summary>
        /// <param name="participantTypeId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public CValidator GetParticipantTypes(int? participantTypeId = null, int? startRow = null, int? endRow = null)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Participant.GetParticipantTypes(participantTypeId, startRow, endRow)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Add Participant Person or Company
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public CValidator AddParticipant(Participant obj, int userID)
        {
            try
            {
                //variables 
                var serializer = new JavaScriptSerializer();
                int? result;
                int userId = Convert.ToInt32(OperationContext.Current.IncomingMessageProperties["uuid"].ToString());
                //end of variables 

                ///add transaction log
                Business.LogTransaction.InsertTransactionLog((new Transaction() { DealID = obj.DealID, TransactionTypeID = (int)Global.Enums.TransactionType.New, TransactionStatusID = 4 }), (int)Enums.WokflowTask.Participant, userId);
                ///end of add transaction log

                result = Business.Participant.AddParticipant(obj, userID);
                obj.ParticipantID = Convert.ToInt32(result);
                //Add audit
                //AuditBusiness.AddAudit(new Audit() { ForeignKey = result.ToString(), TableName = "Participant", Description = "ActionAdd", UserID = userId }, "ParticipantID", Utilities.CString.ConvertToXMLParametersAdd(obj));
                //end of audit add
                if (result == -2) //if participant existed
                {
                    return CValidator.initValidator("", serializer.Serialize(result), "ParticipantExists", true);
                }
                else
                {
                    return CValidator.initValidator("", serializer.Serialize(result), "AddedSuccessfully", true);
                }
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotAdd", false);
            }
        }

        /// <summary>
        /// wcf call get bank & address count per participant
        /// </summary>
        /// <param name="dealID"></param>
        /// <param name="participantId"></param>
        /// <returns></returns>
        public CValidator GetParticipantInfo(int? dealID = null, int? participantId = null)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Participant.GetParticipantInfo(dealID, participantId)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// get participants roles
        /// </summary>
        /// <param name="participantRoleId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public CValidator GetParticipantRoles(int? participantRoleId = null, int? startRow = null, int? endRow = null)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Participant.GetParticipantRoles(participantRoleId, startRow, endRow)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Get existing participants involveds
        /// </summary>
        /// <param name="uniqueReference"></param>
        /// <param name="participantId"></param>
        /// <param name="givenName"></param>
        /// <param name="familyName"></param>
        /// <param name="identityNumber"></param>
        /// <param name="participantTypeId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public CValidator GetParticipants(int dealId, int? participantId, object givenName, object familyName, object identityNumber, int? participantTypeId = null, int? startRow = null, int? endRow = null)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Participant.GetParticipants(dealId, participantId, givenName, familyName, identityNumber, participantTypeId, startRow, endRow)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Update Participant
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public CValidator UpdateParticipant(DATA.Entity.Participant obj)
        {
            try
            {
                //variables
                int? result;
                var serializer = new JavaScriptSerializer();
                //end of variables
                result = Business.Participant.UpdateParticipant(obj);
                int userId = Convert.ToInt32(OperationContext.Current.IncomingMessageProperties["uuid"].ToString());

                ///add transaction log
                Business.LogTransaction.InsertTransactionLog((new Transaction() { DealID = obj.DealID, TransactionTypeID = (int)Enums.TransactionType.Edit, TransactionStatusID = 7 }), (int)Enums.WokflowTask.Participant, userId);
                ///end of add transaction log

                return CValidator.initValidator("", serializer.Serialize(result), "AddedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }

        }

        /// <summary>
        /// remove participant
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public CValidator DeleteParticipant(DATA.Entity.Participant obj, int dealID)
        {
            try
            {
                //variables
                var serializer = new JavaScriptSerializer();
                int? result;
                int userId = Convert.ToInt32(OperationContext.Current.IncomingMessageProperties["uuid"].ToString());
                //end variables

                ///add transaction log
                Business.LogTransaction.InsertTransactionLog((new DATA.Entity.Transaction() { DealID = dealID, TransactionTypeID = (int)Global.Enums.TransactionType.Remove, TransactionStatusID = 6 }), (int)Enums.WokflowTask.Participant, userId);
                ///end of add transaction log

                if ((result = Business.Participant.DeleteParticipant(obj)) > 0)
                {
                    return CValidator.initValidator("", serializer.Serialize(result), "DeleteSuccess", true);
                }
                else
                {
                    return CValidator.initValidator("", serializer.Serialize(result), "Deletefail", false);
                }
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                thisGuid = Guid.NewGuid().ToString();
                CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotAdd", false);
            }

        }

        /// <summary>
        /// Update Participant Bank Details
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public CValidator UpdateParticipantBankDetails(ParticipantBankDetail obj,int dealID)
        {
            try
            {
                //variables
                var serializer = new JavaScriptSerializer();
                int? result;
                int userId = Convert.ToInt32(OperationContext.Current.IncomingMessageProperties["uuid"].ToString());
                //end of variables
                ///add transaction log
                Business.LogTransaction.InsertTransactionLog((new Transaction() { DealID = dealID, TransactionTypeID = (int)Enums.TransactionType.Edit, TransactionStatusID = 7 }), (int)Enums.WokflowTask.Participant, userId);
                ///end of add transaction log
                result = Business.Participant.UpdateParticipantBankDetails(obj);

                return CValidator.initValidator("", serializer.Serialize(result), "AddedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotAdd", false);
            }
        }

        /// <summary>
        /// get participant details auto search
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="searchResult"></param>
        /// <param name="entityID"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public CValidator GetParticipantAutosearch(int userID, object searchResult, int entityID, int? startRow, int? endRow)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Participant.GetParticipantAutosearch(userID, searchResult, entityID, startRow, endRow)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Get Participant ID by ParticipantGUID
        /// </summary>
        /// <param name="participantGUID"></param>
        /// <returns></returns>
        public CValidator GetParticipantIDByParticipantGUID(System.Guid participantGUID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Participant.GetParticipantIDByParticipantGUID(participantGUID)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Add Participant Bank Details
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public CValidator AddParticipantBankDetails(DATA.Entity.ParticipantBankDetail obj,int dealID)
        {
            try
            {
                //variables
                var serializer = new JavaScriptSerializer();
                int? result;
                int userId = Convert.ToInt32(OperationContext.Current.IncomingMessageProperties["uuid"].ToString());
                //end of variables
                ///add transaction log
                Business.LogTransaction.InsertTransactionLog((new DATA.Entity.Transaction() { DealID = dealID, TransactionTypeID = (int)Global.Enums.TransactionType.New, TransactionStatusID = 4 }), (int)Enums.WokflowTask.Participant, userId);
                ///end of add transaction log
                result = Business.Participant.AddParticipantBankDetails(obj);
                return CValidator.initValidator("", serializer.Serialize(result), "AddedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotAdd", false);
            }
        }

        /// <summary>
        /// Delete Participant Bank
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public CValidator DeleteParticipantBank(ParticipantBankDetail obj, int dealID)
        {
            try
            {
                //variables
                var serializer = new JavaScriptSerializer();
                int? result;
                int userId = Convert.ToInt32(OperationContext.Current.IncomingMessageProperties["uuid"].ToString());
                //end of variables
                ///add transaction log
                Business.LogTransaction.InsertTransactionLog((new DATA.Entity.Transaction() { DealID = dealID, TransactionTypeID = (int)Global.Enums.TransactionType.Remove, TransactionStatusID = 6}), (int)Enums.WokflowTask.Participant, userId);
                ///end of add transaction log
                if ((result = Business.Participant.DeleteParticipantBank(obj)) > 0)
                {
                    return CValidator.initValidator("", serializer.Serialize(result), "DeleteSuccess", true);
                }
                else
                {
                    return CValidator.initValidator("", serializer.Serialize(result), "Deletefail", false);
                }
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                thisGuid = Guid.NewGuid().ToString();
                CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotAdd", false);
            }
        }

        /// <summary>
        /// wcf call get bank & address count per participant
        /// </summary>
        /// <param name="dealID"></param>
        /// <param name="participantId"></param>
        /// <returns></returns>
        public CValidator GetParticipantBankAccountCount(int? dealID = null, int? participantId = null)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Participant.GetParticipantBankAccountCount(dealID, participantId)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// call store procedure to verify if person exist with same role
        /// </summary>
        /// <param name="objParticipant"></param>
        /// <param name="objPerson"></param>
        /// <returns></returns>
        public CValidator VerifyIndividualExist(DATA.Entity.Participant objParticipant, DATA.Entity.Person objPerson)
        {
            try
            {
                //variables
                var serializer = new JavaScriptSerializer();
                int? result;
                //end of variables

                if ((result = Business.Participant.VerifyIndividualExist(objParticipant, objPerson)) > 0)
                {
                    return CValidator.initValidator("", serializer.Serialize(result), "DeleteSuccess", true);
                }
                else
                {
                    return CValidator.initValidator("", serializer.Serialize(result), "Deletefail", false);
                }
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                thisGuid = Guid.NewGuid().ToString();
                CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotAdd", false);
            }
        }
    }
}
