using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPS.Business
{
    public class Address
    {

        /// <summary>
        /// get address types 
        /// </summary>
        /// <param name="addressTypeId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static object GetAddressTypes(int? addressTypeId, int? startRow, int? endRow)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion

                #region logic
                //using (_dbFunc)
                {
                    results = REPSDB.REPS_GetAddressTypes(addressTypeId, startRow, endRow);
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
        /// get person address
        /// </summary>
        /// <param name="participantId"></param>
        /// <param name="addressId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static object GetAddress(int? participantId, int? addressId, int? startRow, int? endRow)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables

                #region logic : get person address
                results = REPSDB.REPS_GetAddress(participantId, addressId, startRow, endRow);
                    return results;
                #endregion end of logic : get person address
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }


        /// <summary>
        /// add address person
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static int? AddAddress(DATA.Entity.Address address)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter identity = new ObjectParameter("identity", typeof(int));
                #endregion end of  variables
                #region logic : insert address
                //using (_dbFunc)
                {
                    REPSDB.REPS_AddAddress(
                                                address.ParticipantID
                                                , address.AddressTypeID
                                                , address.AddressLine1
                                                , address.AddressLine2
                                                , address.City
                                                , address.ProvinceID
                                                , address.CountryID
                                                , address.PostalCode
                                                , DateTime.Now
                                                , false
                                                , address.Verified
                                                , identity
                                         );
                    return (identity.Value == null ? null : (int?)identity.Value);
                }
                #endregion end of logic : insert address
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        /// <summary>
        /// update address
        /// </summary>
        /// <param name="ob"></param>
        /// <returns></returns>
        public static int? UpdateAddress(DATA.Entity.Address ob)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion end of variables

                #region logic : update address
                //update fields
                REPSDB.REPS_UpdateAddress(
                                            ob.ParticipantID,
                                            ob.AddressTypeID,
                                            ob.AddressLine1,
                                            ob.AddressLine2,
                                            ob.City,
                                            ob.ProvinceID,
                                            ob.CountryID,
                                            ob.PostalCode,
                                            ob.AddressID,
                                            ob.Verified,
                                            rowCount);
                return (rowCount.Value == null ? null : (int?)rowCount.Value);
                #endregion end of logic :update address
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// remove address set deleted to 1
        /// </summary>
        /// <param name="ob"></param>
        /// <returns></returns>
        public static int? DeleteAddress(DATA.Entity.Address ob)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                #endregion end of variables

                #region logic : remove address set deleted to 1
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                var res = REPSDB.REPS_DeleteAddress(ob.AddressID, ob.Deleted, rowCount);
                return (rowCount.Value == DBNull.Value ? null : (int?)rowCount.Value);
                #endregion end of logic : remove address set deleted to 1
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// store procedure call to get Address id
        /// </summary>
        /// <param name="dealGUID"></param>
        /// <returns></returns>
        public static object GetAddressIDByAddressGUID(string addressGUID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end variables

                #region logic
                results = REPSDB.REPS_GetAddressIDByAddressGUID(addressGUID).FirstOrDefault();
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
