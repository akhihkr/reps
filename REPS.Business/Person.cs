using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPS.Business
{
    public class Person
    {
        /// <summary>
        /// save person to database
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public static int? AddPerson(DATA.Entity.Participant objParticipant, DATA.Entity.Person person)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter identity = new ObjectParameter("identity", typeof(int));
                #endregion end of variables

                #region logic : save person to db
                REPSDB.REPS_AddPerson(
                            person.OrganizationID,
                            person.TitleID,
                            person.GivenName,
                            person.FamilyName,
                            person.IdentityTypeID,
                            person.IdentityNumber,
                            person.PassportNumber,
                            person.PassportCountryID,
                            person.TaxID,
                            person.BirthDate,
                            person.BirthPlace,
                            person.Telephone,
                            person.FaxNumber,
                            person.MobileNumber,
                            person.Email,
                            person.JobTitleID,
                            person.Verified,
                            DateTime.Now,
                            false,
                            objParticipant.DealID,
                            objParticipant.ParticipantRoleID,
                            identity
                        );
                return (identity.Value == null ? null : (int?)identity.Value);
                #endregion end of logic : save person to db
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// update person to db 
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public static int? UpdatePerson(DATA.Entity.Participant participant, DATA.Entity.Person person)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion end of variables

                #region logic : update person to db 
                REPSDB.REPS_UpdatePerson(
                                       person.OrganizationID,
                                       person.TitleID,
                                       person.GivenName,
                                       person.FamilyName,
                                       person.IdentityTypeID,
                                       person.IdentityNumber,
                                       person.PassportNumber,
                                       person.PassportCountryID,
                                       person.TaxID,
                                       person.BirthDate,
                                       person.BirthPlace,
                                       person.Telephone,
                                       person.FaxNumber,
                                       person.MobileNumber,
                                       person.Email,
                                       person.JobTitleID,
                                       person.Verified,
                                       person.PersonID,
                                       participant.ParticipantRoleID,
                                       participant.DealID,
                                       rowCount
                           );
                return (rowCount.Value == DBNull.Value ? null : (int?)rowCount.Value);
                #endregion end of logic : update person to db 
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
