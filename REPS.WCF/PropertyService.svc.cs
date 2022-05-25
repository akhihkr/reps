using Common;
using Global;
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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PropertyService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select PropertyService.svc or PropertyService.svc.cs at the Solution Explorer and start debugging.
    public class PropertyService : IPropertyService
    {
        public CValidator GetProperties(int dealId, int? propertyId = null, int? startRow = null, int? endRow = null)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Property.GetProperties(dealId, propertyId, startRow, endRow)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        public CValidator GetPropertyTypes(int? propertyTypeId = null, int? startRow = null, int? endRow = null)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Property.GetPropertyTypes(propertyTypeId, startRow, endRow)), "FetchedSuccessfully", true);

            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Get Size Types
        /// </summary>
        /// <param name="sizeTypeId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public CValidator GetSizeTypes(int? sizeTypeId = null, int? startRow = null, int? endRow = null)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Property.GetSizeTypes(sizeTypeId, startRow, endRow)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// add detail to property tables
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public CValidator AddProperty(DATA.Entity.Property obj, string aspNetId)
        {
            //variables
            string thisGuid = Guid.NewGuid().ToString();
            int userId = Convert.ToInt32(OperationContext.Current.IncomingMessageProperties["uuid"].ToString());
            //end of variables

            try
            {
                //variables
                var serializer = new JavaScriptSerializer();
                int? result;
                //end of variables

                ///add transaction log
                Business.LogTransaction.InsertTransactionLog((new DATA.Entity.Transaction() { DealID = obj.DealID, TransactionTypeID = (int)Global.Enums.TransactionType.New, TransactionStatusID = 4 }), (int)Enums.WokflowTask.Property, userId);
                ///end of add transaction log

                result = Business.Property.AddProperty(obj, aspNetId); //add detail to property tables
                obj.PropertyID = result.Value;

                //audit Add                
                //AuditBusiness.AddAudit(new Audit() { ForeignKey = result.ToString(), TableName = "Property", Description = "ActionAdd", UserID = userId }, "PropertyID", Utilities.CString.ConvertToXMLParametersAdd(obj));
                //end of audit add
                if (result > 0)
                {
                    return CValidator.initValidator("", serializer.Serialize(result), "AddedSuccessfully", true);
                }
                else
                {
                    return CValidator.initValidator(thisGuid, serializer.Serialize(result), "CouldNotAdd", false);
                }
            }
            catch (Exception ex)
            {
                CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotAdd", false);
            }
        }

        /// <summary>
        /// Add Property Detail
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="dealID"></param>
        /// <returns></returns>
        public CValidator AddPropertyDetail(DATA.Entity.PropertyDetail obj, int dealID)
        {
            //variables
            string thisGuid = Guid.NewGuid().ToString();
            int userId = Convert.ToInt32(OperationContext.Current.IncomingMessageProperties["uuid"].ToString());
            //end of variables
            try
            {
                //variables
                var serializer = new JavaScriptSerializer();
                int? result;
                //end of vairables

                result = Business.Property.AddPropertyDetail(obj);//Add Property Detail
                obj.PropertyDetailID = result.Value;

                //audit Add                
                //AuditBusiness.AddAudit(new Audit() { ForeignKey = result.ToString(), TableName = "PropertyDetail", Description = "ActionAdd", UserID = userId }, "PropertyDetailID", Utilities.CString.ConvertToXMLParametersAdd(obj));
                //end of audit Add  

                if (result > 0)
                {
                    return CValidator.initValidator("", serializer.Serialize(result), "AddedSuccessfully", true);
                }
                else
                {
                    return CValidator.initValidator(thisGuid, serializer.Serialize(result), "CouldNotAdd", false);
                }
            }
            catch (Exception ex)
            {
                CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotAdd", false);
            }
        }


        public CValidator GetPropertyDetails(int dealID, int? propertyId, int? startRow, int? endRow)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Property.GetPropertiesWithDetails(dealID, propertyId, startRow, endRow)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// update property 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public CValidator UpdateProperty(DATA.Entity.Property obj)
        {
            var serializer = new JavaScriptSerializer();
            int? result;
            string thisGuid = Guid.NewGuid().ToString();
            int userId = Convert.ToInt32(OperationContext.Current.IncomingMessageProperties["uuid"].ToString());
            try
            {
                //audit update
                //AuditBusiness.AddAudit(new Audit() { ForeignKey = obj.PropertyID.ToString(), TableName = "Property", Description = "ActionUpdate", UserID = userId }, "PropertyID", Utilities.CString.ConvertToXMLParametersAdd(obj, true));
                if ((result = Business.Property.UpdateProperty(obj)) > 0)
                {
                    ///add transaction log
                    Business.LogTransaction.InsertTransactionLog((new DATA.Entity.Transaction() { DealID = obj.DealID, TransactionTypeID = (int)Global.Enums.TransactionType.Edit, TransactionStatusID = 7 }), (int)Enums.WokflowTask.Property, userId);
                    ///end of add transaction log
                    return CValidator.initValidator("", serializer.Serialize(result), "UpdatedSuccessfully", true);
                }
                else
                {
                    return CValidator.initValidator(thisGuid, result.ToString(), "CouldNotUpdate", false);
                }
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotAdd", false);
            }
        }

        /// <summary>
        /// Update Property Detail
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public CValidator UpdatePropertyDetail(DATA.Entity.PropertyDetail obj)
        {
            var serializer = new JavaScriptSerializer();
            int? result;
            string thisGuid = Guid.NewGuid().ToString();

            try
            {
                //audit update
                // AuditBusiness.AddAudit(new Audit() { ForeignKey = obj.PropertyDetailID.ToString(), TableName = "PropertyDetail", Description = "ActionUpdate", UserID = userId }, "PropertyDetailID", Utilities.CString.ConvertToXMLParametersAdd(obj, true));
                if ((result = Business.Property.UpdatePropertyDetail(obj)) > 0)
                {
                    return CValidator.initValidator("", serializer.Serialize(result), "UpdatedSuccessfully", true);
                }
                else
                {
                    return CValidator.initValidator(thisGuid, result.ToString(), "CouldNotUpdate", false);
                }
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotAdd", false);
            }
        }

        /// <summary>
        /// remove property
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public CValidator DeleteProperty(DATA.Entity.Property obj, int dealID)
        {
            var serializer = new JavaScriptSerializer();
            int? result;
            string thisGuid = Guid.NewGuid().ToString();
            int userId = Convert.ToInt32(OperationContext.Current.IncomingMessageProperties["uuid"].ToString());

            try
            {
                ///add transaction log
                Business.LogTransaction.InsertTransactionLog((new DATA.Entity.Transaction() { DealID = dealID, TransactionTypeID = (int)Global.Enums.TransactionType.Remove, TransactionStatusID = 6 }), (int)Enums.WokflowTask.Property, userId);
                ///end of add transaction log
                //audit delete
                //AuditBusiness.AddAudit(new Audit() { ForeignKey = obj.PropertyID.ToString(), TableName = "Property", Description = "ActionDelete", UserID = userId, Deleted = true }, "PropertyID", "<Parameters></Parameters>");
                if ((result = Business.Property.DeleteProperty(obj)) > 0)
                {
                    return CValidator.initValidator("", serializer.Serialize(result), "DeleteSuccess", true);
                }
                else
                {
                    return CValidator.initValidator(thisGuid, serializer.Serialize(result), "Deletefail", false);
                }
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotAdd", false);
            }
        }

        /// <summary>
        /// Get Property ID
        /// </summary>
        /// <param name="dealGUID"></param>
        /// <returns></returns>
        public CValidator GetPropertyIDByPropertyGUID(string propertyGUID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Property.GetPropertyIDByPropertyGUID(propertyGUID)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Get Property Detail  ID
        /// </summary>
        /// <param name="dealGUID"></param>
        /// <returns></returns>
        public CValidator GetPropertyDetailIDByPropertyDetailGUID(string propertyDetailGUID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Property.GetPropertyDetailIDByPropertyDetailGUID(propertyDetailGUID)), "FetchedSuccessfully", true);
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
