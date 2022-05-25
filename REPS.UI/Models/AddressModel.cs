using REPS.DATA.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Script.Serialization;

namespace REPS.UI.Models
{
    public class AddressModel
    {

        /// <summary>
        /// Get Address Types
        /// </summary>
        /// <param name="addressTypeId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static object GetAddressType(int? addressTypeId, int? startRow, int? endRow)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables
                /// Call WCF to get all address types
                #region WCF for address type
                using (AddressServiceReference.AddressServiceClient addressServiceClient = new AddressServiceReference.AddressServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(addressServiceClient.InnerChannel))
                    {
                        resultValidator = addressServiceClient.GetAddressesTypes(addressTypeId == null ? null : addressTypeId, startRow == null ? null : startRow, endRow == null ? null : endRow);
                        var outputServalCall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            ///Created Deal id save into Sessin for reuse
                            return outputServalCall;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end for WCF properties type
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get Addresses
        /// </summary>
        /// <param name="participantID"></param>
        /// <param name="addressID"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static object GetAddress(int? participantID, int? addressID, int? startRow, int? endRow)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;

                #endregion end variables
                /// Call WCF to get all address
                #region WCF for address 
                using (AddressServiceReference.AddressServiceClient addressServiceClient = new AddressServiceReference.AddressServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(addressServiceClient.InnerChannel))
                    {
                        resultValidator = addressServiceClient.GetAddress(participantID, addressID == null ? null : addressID, startRow == null ? null : startRow, endRow == null ? null : endRow);
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
                #endregion end for WCF address
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Add Address
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="addressinput"></param>
        /// <returns></returns>
        public static string SaveAddress(Address addressinput,int dealID)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables
                /// Call WCF to save an address
                #region WCF to save an address
                using (AddressServiceReference.AddressServiceClient addressServiceClient = new AddressServiceReference.AddressServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(addressServiceClient.InnerChannel))
                    {
                        resultValidator = addressServiceClient.AddAddress(addressinput, dealID);
                        var output = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null && !Common.CString.CheckNullOrEmpty(resultValidator.output))
                        {
                            return resultValidator.output;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end WCF to save an address
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Udpdate Address
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="addressinput"></param>
        /// <returns></returns>
        public static string UpdateAddress(Address addressinput, int dealID)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables
                /// Call WCF to save an address
                #region WCF to save an address
                using (AddressServiceReference.AddressServiceClient addressServiceClient = new AddressServiceReference.AddressServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(addressServiceClient.InnerChannel))
                    {
                        resultValidator = addressServiceClient.UpdateAddress(addressinput, dealID);
                        var output = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null && !Common.CString.CheckNullOrEmpty(resultValidator.output))
                        {
                            return resultValidator.output;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end WCF to save an address
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// get province address
        /// </summary>
        /// <param name="countryId"></param>
        /// <param name="provinceId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static object GetProvince(int? countryId = null, int? provinceId = null, int? startRow = null, int? endRow = null)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables
                /// Call WCF to get all prvince
                #region WCF for address 
                using (CountryServiceReference.CountryServiceClient countryServiceClient = new CountryServiceReference.CountryServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(countryServiceClient.InnerChannel))
                    {
                        resultValidator = countryServiceClient.GetProvince(countryId == null ? null : countryId, provinceId == null ? null : provinceId, startRow == null ? null : startRow, endRow == null ? null : endRow);
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
                #endregion end for WCF address
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// wcf call get address id by addressGUID
        /// </summary>
        /// <param name="dealUniqueRef"></param>
        /// <returns></returns>
        public static object GetAddressIDByAddressGUID(string addressGUID)
        {
            try
            {
                #region Variables
                Common.CValidator resultValidator = null;
                #endregion end vaiables
                using (AddressServiceReference.AddressServiceClient addressServiceClient = new AddressServiceReference.AddressServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(addressServiceClient.InnerChannel))
                    {
                        resultValidator = addressServiceClient.GetAddressIDByAddressGUID(addressGUID);
                        var outputServalCall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            ///Created Deal id save into Sessin for reuse
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
    }

}