using REPS.DATA.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPS.Business
{
    public class Entity
    {
        #region get entity details
        /// <summary>
        /// Get Entities
        /// </summary>
        /// <param name="name"></param>
        /// <param name="legalName"></param>
        /// <param name="registrationNumber"></param>
        /// <param name="entityID"></param>
        /// <returns></returns>
        public static object GetEntities(string name = null, string legalName = null, string registrationNumber = null, int? entityID = null, int? emptyEntityId = null)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables

                #region logic
                results = REPSDB.REPS_GetEntities(name, legalName, registrationNumber, entityID, emptyEntityId);
                return results;
                #endregion end of logic
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }
        #endregion end of get entity details

        #region add entity
        /// <summary>
        /// update entity tables
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int? AddEntity(DATA.Entity.Entity obj)
        {
            try
            {
                #region variables
                REPSEntities REPSDB = new REPSEntities();
                ObjectParameter identity = new ObjectParameter("identity", typeof(int));
                #endregion end of variables

                #region logic
                //using (_dbFunc)
                {
                    //use idOrganisation 
                    REPSDB.REPS_ADM_AddEntity(
                                obj.OrganizationTypeID,
                                obj.Name,
                                obj.RegistrationNumber,
                                obj.LegalName,
                                obj.AlternateName,
                                obj.VatID,
                                obj.Telephone,
                                obj.FaxNumber,
                                obj.Email,
                                obj.AddressTypeID,
                                obj.AddressLine1,
                                obj.AddressLine2,
                                obj.City,
                                obj.ProvinceID,
                                obj.CountryID,
                                obj.PostalCode,
                                obj.ParentEntityID,
                                identity
                            );
                    return (identity.Value == null) ? null : (int?)identity.Value;
                }
                #endregion end of logic
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion end of add entity

        #region updates entity
        /// <summary>
        /// update organisation details
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int? UpdateEntity(DATA.Entity.Entity obj)
        {
            try
            {
                #region variables
                REPSEntities REPSDB = new REPSEntities();
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion

                #region logic :update organisation details
                REPSDB.REPS_UpdateEntity(
                                obj.EntityID,
                                obj.OrganizationTypeID,
                                obj.Name,
                                obj.RegistrationNumber,
                                obj.LegalName,
                                obj.AlternateName,
                                obj.VatID,
                                obj.Telephone,
                                obj.FaxNumber,
                                obj.Email,
                                obj.AddressTypeID,
                                obj.AddressLine1,
                                obj.AddressLine2,
                                obj.City,
                                obj.ProvinceID,
                                obj.CountryID,
                                obj.PostalCode,
                                obj.ParentEntityID,
                                obj.Verified,
                                obj.Deleted,
                                rowCount
                            );
                return (rowCount.Value == null ? null : (int?)rowCount.Value);
                #endregion end of logic : update organisation details
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion end of update entities

        /// <summary>
        /// get entity id
        /// </summary>
        /// <param name="entityID"></param>
        /// <returns></returns>
        public static object GetEntityID(string entityID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end variables

                #region logic
                results = REPSDB.REPS_GetEntityID(entityID).FirstOrDefault();
                return results;
                #endregion end logic
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        #region remove entity per id
        /// <summary>
        /// remove entity per id
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int? DeleteEntity(DATA.Entity.Entity obj)
        {
            try
            {
                #region variables
                REPSEntities propertyDB = new REPSEntities();
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion end of variables

                #region logic : remove entity per id
                propertyDB.REPS_ADM_DeleteEntity(obj.EntityID, obj.Deleted, rowCount);
                return (rowCount.Value == null ? null : (int?)rowCount.Value);
                #endregion end of logic : remove entity per id
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion end of remove entity per id

        /// <summary>
        /// Remove User from deleted entity
        /// </summary>
        /// <param name="entityID"></param>
        /// <returns></returns>
        public static object RemoveUserFromDeletedEntity(int entityID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new REPSEntities();
                object results = null;
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion end of variables

                #region logic : Remove User from deleted entity
                results = REPSDB.REPS_DeleteUserFromDeletedEntity(entityID, -1, rowCount);
                return (rowCount.Value == DBNull.Value ? null : (int?)rowCount.Value);
                #endregion end of logic :Remove User from deleted entity
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
