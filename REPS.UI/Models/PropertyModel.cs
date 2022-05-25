using REPS.DATA.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Script.Serialization;

namespace REPS.UI.Models
{
    public class PropertyModel
    {
        /// <summary>
        ///  to check if we have existing properties per deal ***** Property Model *****
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="DealID"></param>
        /// <returns></returns>
        public static object GetProperty(int DealID, int? propertyId, int? startRow, int? endRow)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end of variables
                #region call participant
                using (PropertyServiceReference.PropertyServiceClient propertyServiceClient = new PropertyServiceReference.PropertyServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(propertyServiceClient.InnerChannel))
                    {
                        resultValidator = propertyServiceClient.GetProperties(DealID == 0 ? 0 : DealID, propertyId == null ? null : propertyId, startRow == null ? null : startRow, endRow == null ? null : endRow);
                        var output = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            ///Created Deal id save into Sessin for reuse
                            return output;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                        #endregion end of participant call
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
        /// get property types detail
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="propertyTypeId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static object GetPropertyTypes(int? propertyTypeId, int? startRow, int? endRow)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables
                /// Call WCF to get all property types
                #region WCF for properties type
                using (PropertyServiceReference.PropertyServiceClient propertyServiceClient = new PropertyServiceReference.PropertyServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(propertyServiceClient.InnerChannel))
                    {
                        resultValidator = propertyServiceClient.GetPropertyTypes(propertyTypeId == null ? null : propertyTypeId, startRow == null ? null : startRow, endRow == null ? null : endRow);
                        var output = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            ///Created Deal id save into Sessin for reuse
                            return output;
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
        /// Get Size Types
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="sizeTypeId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static object GetSizeTypes(int? sizeTypeId, int? startRow, int? endRow)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables
                /// Call WCF to get all property types
                #region WCF for properties type
                using (PropertyServiceReference.PropertyServiceClient propertyServiceClient = new PropertyServiceReference.PropertyServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(propertyServiceClient.InnerChannel))
                    {
                        resultValidator = propertyServiceClient.GetSizeTypes(sizeTypeId == null ? null : sizeTypeId, startRow == null ? null : startRow, endRow == null ? null : endRow);
                        var output = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            ///Created Deal id save into Sessin for reuse
                            return output;
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
        /// Get countries
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="countryId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static object[] GetCountries(int? countryId, int? startRow, int? endRow)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables
                /// Call WCF to get all countries
                #region WCF for country
                using (CountryServiceReference.CountryServiceClient countryServiceClient = new CountryServiceReference.CountryServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(countryServiceClient.InnerChannel))
                    {
                        resultValidator = countryServiceClient.GetCountries(countryId == null ? null : countryId, startRow == null ? null : startRow, endRow == null ? null : endRow);
                        var output = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            ///Created Deal id save into Sessin for reuse
                            return output;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end WCF for country
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// get token using password and username to get information
        /// </summary>
        /// <returns></returns>
        public static object GetTokenSearchWorks()
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                string userName = ConfigurationManager.AppSettings["SearchWorksUsername"];
                string password = ConfigurationManager.AppSettings["SearchWorksPassword"];
                #endregion end variables

                #region removing a property
                using (SearchWorksServiceReference.SearchWorksServiceClient SearchWorkClient = new SearchWorksServiceReference.SearchWorksServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(SearchWorkClient.InnerChannel))
                    {
                        resultValidator = SearchWorkClient.SearchWorksToken(userName, password);

                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            ///Created Deal id save into Sessin for reuse
                            var output = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                            return output;
                        }
                        else
                        {
                            //throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                            return "";        // If Searchworks service is facing downtime =>
                        }
                    }
                }
                #endregion end removing a property
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// get deeds office property
        /// </summary>
        /// <param name="token"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        public static object GetDeedsOfficeProperty(string token, string reference)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables

                #region removing a property
                using (SearchWorksServiceReference.SearchWorksServiceClient SearchWorkClient = new SearchWorksServiceReference.SearchWorksServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(SearchWorkClient.InnerChannel))
                    {
                        resultValidator = SearchWorkClient.SearchWorksGetDeed(token, reference);
                        var output = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            ///Created Deal id save into Sessin for reuse
                            return output;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end removing a property
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// save property 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="propertyInput"></param>
        /// <returns></returns>
        public static object SaveProperty(Property propertyInput, string aspNetId)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables
                /// Call WCF to save property and country 
                #region WCF to save an property
                using (PropertyServiceReference.PropertyServiceClient propertyServiceClient = new PropertyServiceReference.PropertyServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(propertyServiceClient.InnerChannel))
                    {
                        resultValidator = propertyServiceClient.AddProperty(propertyInput, aspNetId);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null && !Common.CString.CheckNullOrEmpty(resultValidator.output))/// checknullorempty use only on save not on get
                        {
                            ///Created Deal id save into Sessin for reuse
                            return resultValidator.output.ToString();
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion wcf to save an property
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Save Property Detail
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="dealID"></param>
        /// <param name="propertyInput"></param>
        /// <returns></returns>
        public static object SavePropertyDetail(int dealID, PropertyDetail propertyInput)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables
                /// Call WCF to save property and country 
                #region WCF to save an property
                using (PropertyServiceReference.PropertyServiceClient propertyServiceClient = new PropertyServiceReference.PropertyServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(propertyServiceClient.InnerChannel))
                    {
                        resultValidator = propertyServiceClient.AddPropertyDetail(propertyInput, dealID);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null && !Common.CString.CheckNullOrEmpty(resultValidator.output))/// checknullorempty use only on save not on get
                        {
                            return resultValidator.output.ToString();
                        }
                        else
                        {
                          return resultValidator.guid;
                        }
                    }
                }
                #endregion wcf to save an property
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        public static object SearchTownDetail(string token, string reference, string townname, string DeedsOfficeIDs)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables
                #region removing a property
                using (SearchWorksServiceReference.SearchWorksServiceClient SearchWorkClient = new SearchWorksServiceReference.SearchWorksServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(SearchWorkClient.InnerChannel))
                    {
                        resultValidator = SearchWorkClient.GetSearchWorksTown(token, reference, townname, DeedsOfficeIDs);
                        var output = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            ///Created Deal id save into Sessin for reuse
                            if (resultValidator.output.Contains("[]"))
                            {
                                output = null;
                            }
                            return output;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end removing a property
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        public static object GetPropertyDetails(int dealId, int? propertyId, int? startRow, int? endRow)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables
                /// Call WCF to get property details
                #region WCF to get property details
                using (PropertyServiceReference.PropertyServiceClient propertyServiceClient = new PropertyServiceReference.PropertyServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(propertyServiceClient.InnerChannel))
                    {
                        resultValidator = propertyServiceClient.GetPropertyDetails(dealId, propertyId == null ? null : propertyId, startRow == null ? null : startRow, endRow == null ? null : endRow);
                        var output = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null)
                        {
                            ///Created Deal id save into Sessin for reuse
                            return output;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end WCF to get property details
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Update Property
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="propertyInput"></param>
        /// <returns></returns>
        public static object UpdateProperty(Property propertyInput)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables
                /// Call WCF to save property and country 
                #region WCF to save an property
                using (PropertyServiceReference.PropertyServiceClient propertyServiceClient = new PropertyServiceReference.PropertyServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(propertyServiceClient.InnerChannel))
                    {
                        resultValidator = propertyServiceClient.UpdateProperty(propertyInput);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null && !Common.CString.CheckNullOrEmpty(resultValidator.output))/// checknullorempty use only on save not on get
                        {
                            ///Created Deal id save into Sessin for reuse
                            return resultValidator.output.ToString();
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion wcf to save an property
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Update Property Detail
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="dealID"></param>
        /// <param name="propertyInput"></param>
        /// <returns></returns>
        public static object UpdatePropertyDetail(PropertyDetail propertyInput)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
 
                #endregion end variables
                /// Call WCF to save property and country 
                #region WCF to save an property
                using (PropertyServiceReference.PropertyServiceClient propertyServiceClient = new PropertyServiceReference.PropertyServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(propertyServiceClient.InnerChannel))
                    {
                        resultValidator = propertyServiceClient.UpdatePropertyDetail(propertyInput);
                        if (resultValidator != null && resultValidator.success == true && resultValidator.output.ToString() != null && !Common.CString.CheckNullOrEmpty(resultValidator.output))
                        {
                            var output = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                            return output;
                            //return resultValidator.output.ToString();
                        }
                        else
                        {
                            return resultValidator.guid;
                        }
                    }
                }
                #endregion wcf to save an property
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// wcf call get property id by propertyGUID
        /// </summary>
        /// <param name="dealUniqueRef"></param>
        /// <returns></returns>
        public static object GetPropertyIDByPropertyGUID(string propertyGUID)
        {
            try
            {
                #region Variables
                Common.CValidator resultValidator = null;
                #endregion end vaiables
                using (PropertyServiceReference.PropertyServiceClient propertyServiceClient = new PropertyServiceReference.PropertyServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(propertyServiceClient.InnerChannel))
                    {
                        resultValidator = propertyServiceClient.GetPropertyIDByPropertyGUID(propertyGUID);
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

        /// <summary>
        /// wcf call get property detail id by propertyDetailGUID
        /// </summary>
        /// <param name="dealUniqueRef"></param>
        /// <returns></returns>
        public static object GetPropertyDetailIDByPropertyDetailGUID(string propertyDetailGUID)
        {
            try
            {
                #region Variables
                Common.CValidator resultValidator = null;
                #endregion end vaiables
                using (PropertyServiceReference.PropertyServiceClient propertyServiceClient = new PropertyServiceReference.PropertyServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(propertyServiceClient.InnerChannel))
                    {
                        resultValidator = propertyServiceClient.GetPropertyDetailIDByPropertyDetailGUID(propertyDetailGUID);
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

        /// <summary>
        /// remove property and set delete column to 1
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="propertyInput"></param>
        /// <returns></returns>
        public static string RemoveProperty(Property propertyInput, int dealID)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables

                #region removing a property
                using (PropertyServiceReference.PropertyServiceClient propertyServiceClient = new PropertyServiceReference.PropertyServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(propertyServiceClient.InnerChannel))
                    {
                        resultValidator = propertyServiceClient.DeleteProperty(propertyInput, dealID);
                        if (resultValidator != null && resultValidator.success && resultValidator.output.ToString() != null && !Common.CString.CheckNullOrEmpty(resultValidator.output))/// checknullorempty use only on save not on get
                        {
                            ///Created Deal id save into Sessin for reuse
                            return resultValidator.ToString();
                        }
                        else
                        {
                            return resultValidator.guid;
                        }
                    }
                }
                #endregion end removing a property
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// get countries
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="countryId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static object GetCountry(string userID, int? countryId, int? startRow, int? endRow)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables
                /// Call WCF to get all countries
                #region WCF for country
                using (CountryServiceReference.CountryServiceClient countryServiceClient = new CountryServiceReference.CountryServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(countryServiceClient.InnerChannel))
                    {
                        resultValidator = countryServiceClient.GetCountries(countryId == null ? null : countryId, startRow == null ? null : startRow, endRow == null ? null : endRow);
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
                #endregion end WCF for country
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }
    }
}