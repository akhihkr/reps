using Common;
using Global;
using REPS.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Script.Serialization;

namespace REPS.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AddressService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AddressService.svc or AddressService.svc.cs at the Solution Explorer and start debugging.
    public class AddressService : IAddressService
    {
        /// <summary>
        /// Get Addresses Types
        /// </summary>
        /// <param name="addressTypeId">AddresstypeId</param>
        /// <param name="startRow">start Row</param>
        /// <param name="endRow">end Row</param>
        /// <returns>CValidator class + output results + success messages</returns>
        public CValidator GetAddressesTypes(int? addressTypeId = null, int? startRow = null, int? endRow = null)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Address.GetAddressTypes(addressTypeId, startRow, endRow)), "FetchedSuccessfully", true);

            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        ///  get address details 
        /// </summary>
        /// <param name="participantId"></param>
        /// <param name="addressId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public CValidator GetAddress(int? participantId = null, int? addressId = null, int? startRow = null, int? endRow = null)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Address.GetAddress(participantId, addressId, startRow, endRow)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Add address to person
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public CValidator AddAddress(DATA.Entity.Address obj, int dealID)
        {
            try
            {
                //variables
                int? result;
                var serializer = new JavaScriptSerializer();
                int userId = Convert.ToInt32(OperationContext.Current.IncomingMessageProperties["uuid"].ToString());
                //end of variables

                result = Address.AddAddress(obj);
                obj.AddressID = result.Value;

                ///add transaction log
                Business.LogTransaction.InsertTransactionLog((new DATA.Entity.Transaction() { DealID = dealID, TransactionTypeID = (int)Global.Enums.TransactionType.New, TransactionStatusID = 4 }), (int)Enums.WokflowTask.Participant, userId);
                ///end of add transaction log
                ///
                //audit Add
                //AuditBusiness.AddAudit(new DATA.Entity.Audit() { ForeignKey = result.ToString(), TableName = "Address", Description = "ActionAdd", UserID = userId }, "AddressID", Utilities.CString.ConvertToXMLParametersAdd(obj));
                //end of add audit
                return CValidator.initValidator("", serializer.Serialize(result), "AddedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        public CValidator UpdateAddress(DATA.Entity.Address obj, int dealID)
        {
            //variables
            string thisGuid = Guid.NewGuid().ToString();
            try
            {
                //variables
                int? result;
                int userId = Convert.ToInt32(OperationContext.Current.IncomingMessageProperties["uuid"].ToString());
                //end of variables
                ///add transaction log
                Business.LogTransaction.InsertTransactionLog((new DATA.Entity.Transaction() { DealID = dealID, TransactionTypeID = (int)Enums.TransactionType.Edit, TransactionStatusID = 7 }), (int)Enums.WokflowTask.Participant, userId);
                ///end of add transaction log

                //audit update
                // AuditBusiness.AddAudit(new DATA.Entity.Audit() { ForeignKey = obj.AddressID.ToString(), TableName = "Address", Description = "ActionUpdate", UserID = userId }, "AddressID", Utilities.CString.ConvertToXMLParametersAdd(obj, true));
                if ((result = Address.UpdateAddress(obj)) > 0)
                {
                    var serializer = new JavaScriptSerializer();
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
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Delete Address
        /// </summary>
        /// <param name="obj">Address object</param>
        /// <returns>CValidator class + output results + success messages</returns>
        public CValidator DeleteAddress(DATA.Entity.Address obj, int dealID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                int? result;
                int userId = Convert.ToInt32(OperationContext.Current.IncomingMessageProperties["uuid"].ToString());

                if ((result = Address.DeleteAddress(obj)) > 0)
                {
                    //audit delete
                    //AuditBusiness.AddAudit(new DATA.Entity.Audit() { ForeignKey = obj.AddressID.ToString(), TableName = "Address", Description = "ActionDelete", UserID = userId, Deleted = true }, "AddressID", "<Parameters></Parameters>");
                    return CValidator.initValidator("", serializer.Serialize(result), "DeleteSuccess", true);
                }
                else
                {
                    string thisGuid = Guid.NewGuid().ToString();
                    Common.CLog.WriteLogInfo(thisGuid, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                    return CValidator.initValidator(thisGuid, result.ToString(), "Deletefail", false);
                }
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Get Address ID
        /// </summary>
        /// <param name="dealGUID"></param>
        /// <returns></returns>
        public CValidator GetAddressIDByAddressGUID(string addressGUID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Address.GetAddressIDByAddressGUID(addressGUID)), "FetchedSuccessfully", true);
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
