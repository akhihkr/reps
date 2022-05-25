using Global;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPS.Business
{
    public class Participant
    {
        /// <summary>
        /// store procedure call to get participant types from db
        /// </summary>
        /// <param name="participantTypeId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static object GetParticipantTypes(int? participantTypeId, int? startRow, int? endRow)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables

                #region logic : store procedure call to get participant types from db
                results = REPSDB.REPS_GetParticipantTypes(participantTypeId, startRow, endRow);
                return results;
                #endregion end of logic : store procedure call to get participant types from db
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// insert participant to data base 
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        public static int? AddParticipant(DATA.Entity.Participant part, int userID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter identity = new ObjectParameter("identity", typeof(int));
                #endregion end of variables 

                #region logic : insert participant
                REPSDB.REPS_AddParticipant(
                             part.DealID
                           , part.ParticipantTypeID
                           , part.ParticipantRoleID
                           , part.PersonID
                           , part.OrganizationID
                           , part.BankID
                           , DateTime.Now
                           , false
                           , part.EntityID
                           , identity
                          
                        );

                return (identity.Value == null ? null : (int?)identity.Value);
                #endregion end of logic : insert participant
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// wcf call get bank & address count per participant
        /// </summary>
        /// <param name="dealID"></param>
        /// <param name="participantId"></param>
        /// <returns></returns>
        public static object GetParticipantInfo(int? dealID = null, int? participantId = null)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables

                #region logic : wcf call get bank & address count per participant
                results = REPSDB.REPS_GetBankAddressCountByParticpantID(dealID, participantId);
                return results;
                #endregion end of logic : wcf call get bank & address count per participant
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// store procedure call to get participant roles
        /// </summary>
        /// <param name="participantRoleId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static object GetParticipantRoles(int? participantRoleId, int? startRow, int? endRow)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables

                #region logic : store procedure call to get participant roles
                results = REPSDB.REPS_GetParticipantRoles(participantRoleId, startRow, endRow);
                return results;
                #endregion end of logic : store procedure call to get participant roles
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// store procedure call to get participant details
        /// </summary>
        /// <param name="dealId"></param>
        /// <param name="participantId"></param>
        /// <param name="givenName"></param>
        /// <param name="familyName"></param>
        /// <param name="identityNumber"></param>
        /// <param name="participantTypeId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static object GetParticipants(int dealId, int? participantId, object givenName, object familyName, object identityNumber, int? participantTypeId, int? startRow, int? endRow)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables 

                #region logic : store procedure call to get participant details
                results = REPSDB.REPS_GetParticipants(dealId, participantId, givenName == null ? "" : givenName.ToString(), familyName == null ? "" : familyName.ToString(), identityNumber == null ? "" : identityNumber.ToString(), participantTypeId, startRow, endRow);
                return results;
                #endregion end of logic : store procedure call to get participant details
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Update Participant
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        public static int? UpdateParticipant(DATA.Entity.Participant part)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion end of variables

                #region logic : Update Participant
                REPSDB.REPS_UpdateParticipant(
                                part.ParticipantTypeID,
                                part.ParticipantRoleID,
                                part.PersonID,
                                part.OrganizationID,
                                part.BankID,
                                part.ParticipantID,
                                rowCount
                            );
                return (rowCount.Value == DBNull.Value ? null : (int?)rowCount.Value);
                #endregion end of logic : Update Participant
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// remove participant
        /// </summary>
        /// <param name="ob"></param>
        /// <returns></returns>
        public static int? DeleteParticipant(DATA.Entity.Participant ob)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion end of variables

                #region logic : remove participant
                REPSDB.REPS_DeleteParticipant(ob.ParticipantID, ob.Deleted, rowCount);
                return (rowCount.Value == null ? null : (int?)rowCount.Value);
                #endregion logic : remove participant
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Update Participant Bank Details
        /// </summary>
        /// <param name="partBank"></param>
        /// <returns></returns>
        public static int? UpdateParticipantBankDetails(DATA.Entity.ParticipantBankDetail partBank)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion end of variables

                #region logic : Update Participant Bank Details
                REPSDB.REPS_UpdateParticipantBankDetail(
                                                        partBank.ParticipantID,
                                                        partBank.BankID,
                                                        partBank.AccountTypeID,
                                                        partBank.AccountName,
                                                        partBank.AccountNumber,
                                                        partBank.SortCode,
                                                        partBank.Verified,
                                                        partBank.ID,
                                                        rowCount);
                return (rowCount.Value == DBNull.Value ? null : (int?)rowCount.Value);
                #endregion logic : Update Participant Bank Details
            }
            catch (Exception Ex)
            {
                throw Ex;
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
        public static object GetParticipantAutosearch(int userID, object searchResult, int entityID, int? startRow, int? endRow)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion

                #region logic : get participant details auto search 
                results = REPSDB.REPS_AutoSearchParticipant(userID, searchResult.ToString(), entityID, startRow, endRow);
                return results;
                #endregion end of logic :  logic : get participant details auto search 
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Get Participant ID by ParticipantGUID
        /// </summary>
        /// <param name="aspNetId"></param>
        /// <returns></returns>
        public static object GetParticipantIDByParticipantGUID(System.Guid participantGUID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables

                #region logic : Get Participant ID by ParticipantGUID
                results = REPSDB.REPS_GetParticipantIDByParticipantGUID(participantGUID);
                return results;
                #endregion end of logic : Get Participant ID by ParticipantGUID
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Add Participant Bank Details
        /// </summary>
        /// <param name="partBank"></param>
        /// <returns></returns>
        public static int? AddParticipantBankDetails(DATA.Entity.ParticipantBankDetail partBank)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter identity = new ObjectParameter("identity", typeof(int));
                #endregion end of variables

                #region logic :Add Participant Bank Details
                REPSDB.REPS_AddParticipantBankDetail(
                            partBank.ParticipantID,
                            partBank.BankID,
                            partBank.AccountTypeID,
                            partBank.AccountName,
                            partBank.AccountNumber,
                            partBank.SortCode,
                            partBank.Verified,
                            identity
                        );

                return (identity.Value == null ? null : (int?)identity.Value);
                #endregion end of logic : Add Participant Bank Details
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Delete Participant Bank
        /// </summary>
        /// <param name="ob"></param>
        /// <returns></returns>
        public static int? DeleteParticipantBank(DATA.Entity.ParticipantBankDetail ob)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion end of variables

                #region logic : Delete Participant Bank
                REPSDB.REPS_DeleteParticipantBank(ob.ID, ob.Deleted, rowCount);
                return (rowCount.Value == null ? null : (int?)rowCount.Value);
                #endregion end of logic : Delete Participant Bank

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// wcf call get bank & address count per participant
        /// </summary>
        /// <param name="dealID"></param>
        /// <param name="participantId"></param>
        /// <returns></returns>
        public static object GetParticipantBankAccountCount(int? dealID = null, int? participantId = null)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion

                #region logic
                {
                    results = REPSDB.REPS_GetBankAddressCountByParticpantID(dealID, participantId);
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
        /// call store procedure to verify if person exist with same role
        /// </summary>
        /// <param name="objParticipant"></param>
        /// <param name="objPerson"></param>
        /// <returns></returns>
        public static int? VerifyIndividualExist(DATA.Entity.Participant objParticipant, DATA.Entity.Person objPerson)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion end of variables

                #region logic : remove participant

                REPSDB.REPS_VerifyIndividualExist(
                    objParticipant.DealID, 
                    objParticipant.ParticipantRoleID, 
                    objPerson.FamilyName,
                    objPerson.GivenName,
                    objPerson.Email
                    );

                return (rowCount.Value == null ? null : (int?)rowCount.Value);
                #endregion logic : remove participant
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
