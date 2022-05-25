using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPS.Business
{
    public class Property
    {

        /// <summary>
        /// get property details
        /// </summary>
        /// <param name="dealId"></param>
        /// <param name="PropertyId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static object GetProperties(int dealId, int? PropertyId, int? startRow, int? endRow)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables

                #region logic :get property details
                results = REPSDB.REPS_GetProperties(dealId, PropertyId, startRow, endRow);
                return results;
                #endregion end of logic :get property details
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// logic : get property types
        /// </summary>
        /// <param name="propertyTypeId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static object GetPropertyTypes(int? propertyTypeId, int? startRow, int? endRow)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables

                #region logic : get property types
                results = REPSDB.REPS_GetPropertyTypes(propertyTypeId, startRow, endRow);
                return results;
                #endregion end of logic : get property types
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Get Size Types
        /// </summary>
        /// <param name="sizeTypeId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static object GetSizeTypes(int? sizeTypeId, int? startRow, int? endRow)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities(); object results = null;
                #endregion end of variables

                #region logic : get size type
                results = REPSDB.REPS_GetSizes(sizeTypeId, startRow, endRow);
                return results;
                #endregion end of logic : get size type
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// logic : add property to db
        /// </summary>
        /// <param name="property"></param>
        /// <param name="aspNetId"></param>
        /// <returns></returns>
        public static int? AddProperty(DATA.Entity.Property property, string aspNetId)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter identity = new ObjectParameter("identity", typeof(int));
                int userID = Convert.ToInt32(Business.Deal.GetUserID(aspNetId));
                #endregion end of variables

                #region logic : add property to db
                REPSDB.REPS_AddProperty(
                           property.DealID
                           , property.PropertyDescription
                           , property.LegalDescription
                           , property.AddressID
                           , property.PropertyTypeID
                           , property.Verified
                           , DateTime.Now
                           , false
                           , identity
                           , (int)Global.Enums.WokflowTask.Property
                           , userID
                        );
                return (identity.Value == null ? null : (int?)identity.Value);
                #endregion end of logic : add property to db
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Add Property Detail
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static int? AddPropertyDetail(DATA.Entity.PropertyDetail property)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter identity = new ObjectParameter("identity", typeof(int));
                #endregion end of variables

                #region logic : add property details
                REPSDB.REPS_AddPropertyDetail(
                                                    property.PropertyID,
                                                    property.RightTypeID,
                                                    property.PropertyNumber,
                                                    property.PortionNumber,
                                                    property.Township,
                                                    property.PropertyName,
                                                    property.SectionNumber,
                                                    property.RegistrationDivision,
                                                    property.PlanNumber,
                                                    property.UnitNumber,
                                                    property.SizeTypeID,
                                                    property.Size,
                                                    property.Geo,
                                                    DateTime.Now,
                                                    false,
                                                    identity
                        );
                return (identity.Value == null ? null : (int?)identity.Value);
                #endregion end of logic : add property details
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// logic :get property details
        /// </summary>
        /// <param name="dealId"></param>
        /// <param name="propertyId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static object GetPropertiesWithDetails(int dealId, int? propertyId, int? startRow, int? endRow)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables

                #region logic :get property details
                results = REPSDB.REPS_GetPropertiesWithDetails(dealId, propertyId, startRow, endRow);
                return results;
                #endregion end of logic :get property details
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// logic : update property
        /// </summary>
        /// <param name="ob"></param>
        /// <returns></returns>
        public static int? UpdateProperty(DATA.Entity.Property ob)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion end of variables 

                #region logic : update property
                REPSDB.REPS_UpdateProperty(
                            ob.DealID,
                            ob.PropertyDescription,
                            ob.LegalDescription,
                            ob.AddressID,
                            ob.PropertyTypeID,
                            ob.Verified,
                            ob.PropertyID,
                            rowCount
                        );
                return (rowCount.Value == null ? null : (int?)rowCount.Value);
                #endregion end of  logic : update property
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// logic :remove property
        /// </summary>
        /// <param name="ob"></param>
        /// <returns></returns>
        public static int? DeleteProperty(DATA.Entity.Property ob)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion end of vairables

                #region logic :remove property
                REPSDB.REPS_DeleteProperty(ob.PropertyID, ob.Deleted, rowCount);
                return (rowCount.Value == null ? null : (int?)rowCount.Value);
                #endregion end of logic :remove property
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Update Property Detail
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static int? UpdatePropertyDetail(DATA.Entity.PropertyDetail property)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion end of variables

                #region logic : Update Property Detail
                REPSDB.REPS_UpdatePropertyDetail(
                                        property.PropertyID,
                                        property.RightTypeID,
                                        property.PropertyNumber,
                                        property.PortionNumber,
                                        property.Township,
                                        property.PropertyName,
                                        property.SectionNumber,
                                        property.RegistrationDivision,
                                        property.PlanNumber,
                                        property.UnitNumber,
                                        property.SizeTypeID,
                                        property.Size,
                                        property.Geo,
                                        property.PropertyDetailID,
                                        rowCount
                        );
                return (rowCount.Value == null ? null : (int?)rowCount.Value);
                #endregion end of logic : Update Property Detail
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// store procedure call to get Property id
        /// </summary>
        /// <param name="dealGUID"></param>
        /// <returns></returns>
        public static object GetPropertyIDByPropertyGUID(string propertyGUID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end variables

                #region logic
                results = REPSDB.REPS_GetPropertyIDByPropertyGUID(propertyGUID).FirstOrDefault();
                return results;
                #endregion end logic
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// store procedure call to get PropertyDetail id
        /// </summary>
        /// <param name="dealGUID"></param>
        /// <returns></returns>
        public static object GetPropertyDetailIDByPropertyDetailGUID(string propertyDetailGUID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end variables

                #region logic
                results = REPSDB.REPS_GetPropertyDetailIDByPropertyDetailGUID(propertyDetailGUID).FirstOrDefault();
                return results;
                #endregion end logic
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
