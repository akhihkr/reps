using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPS.Business
{
    public class Organization
    {
        /// <summary>
        /// call organisation types
        /// </summary>
        /// <param name="organisationTypeId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static object GetOrganisationTypes(int? organisationTypeId, int? startRow, int? endRow)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables

                #region logic : call organisation types
                results = REPSDB.REPS_GetOrganisationTypes(organisationTypeId, startRow, endRow);
                return results;
                #endregion end of logic : call organisation types
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// get organisation details
        /// </summary>
        /// <param name="organisationTypeId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static object GetOrganisation(int? organisationTypeId, int? startRow, int? endRow)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables

                #region logic : get organisation details
                {
                    results = REPSDB.REPS_GetOrganisation(organisationTypeId, startRow, endRow);
                    return results;
                }
                #endregion end of logic : get organisation details
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// add organisation details
        /// </summary>
        /// <param name="ob"></param>
        /// <returns></returns>
        public static int? AddOrganisation(DATA.Entity.Organization ob, DATA.Entity.Participant objParticipant)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter identity = new ObjectParameter("identity", typeof(int));
                #endregion end of  variables

                #region logic : add organisation
                //use idOrganisation 
                REPSDB.REPS_AddOrganisation(
                            ob.OrganizationTypeID,
                            ob.Name,
                            ob.RegistrationNumber,
                            ob.LegalName,
                            ob.VatID,
                            ob.Telephone,
                            ob.FaxNumber,
                            ob.Email,
                            ob.Verified,
                            DateTime.Now,
                            ob.Deleted,
                            ob.EntityID,
                            objParticipant.DealID,
                            objParticipant.ParticipantRoleID,
                            identity
                        );
                return (identity.Value == null ? null : (int?)identity.Value);
                #endregion end of logic : add organisation
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// update organisation details
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int? UpdateOrganisation(DATA.Entity.Participant participantObj, DATA.Entity.Organization obj)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion end of variables

                #region logic : update organisation details
                REPSDB.REPS_UpdateOrganisation(
                                obj.OrganizationID,
                                obj.OrganizationTypeID,
                                obj.Name,
                                obj.RegistrationNumber,
                                obj.LegalName,
                                obj.VatID,
                                obj.Telephone,
                                obj.FaxNumber,
                                obj.Email,
                                obj.Verified,
                                obj.Deleted,
                                obj.EntityID,
                                participantObj.DealID,
                                participantObj.ParticipantRoleID,
                                rowCount
                            );
                return (rowCount.Value == DBNull.Value ? null : (int?)rowCount.Value);
                #endregion end of logic : update organisation details
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
