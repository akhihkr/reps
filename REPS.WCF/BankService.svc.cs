using Common;
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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BankService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select BankService.svc or BankService.svc.cs at the Solution Explorer and start debugging.
    public class BankService : IBankService
    {
        /// <summary>
        /// Get Account Types
        /// </summary>
        /// <param name="accountTypeId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public CValidator GetAccountType(int? accountTypeId = null, int? startRow = null, int? endRow = null)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Bank.GetAccountType(accountTypeId, startRow, endRow)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Get Banks
        /// </summary>
        /// <param name="bankName"></param>
        /// <param name="swiftCode"></param>
        /// <param name="entityId"></param>
        /// <param name="bankId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public CValidator GetBanks(string bankName = null, string swiftCode = null, int? entityId = null, int? bankId = null, int? startRow = null, int? endRow = null)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Bank.GetBanks(bankName, swiftCode, entityId, bankId, startRow, endRow)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// get bank detail per participant to edit
        /// </summary>
        /// <param name="participantId"></param>
        /// <param name="participantBankDetailID"></param>
        /// <returns></returns>
        public CValidator GetBankDetailsPerParticipant(int? participantId = null, int? participantBankDetailID = null)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Bank.GetBankDetailsPerParticipant(participantId, participantBankDetailID)), "Resource.FetchedSuccessfully", true);
            }
            catch (WebFaultException ex)
            {

                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "Resource.CouldNotGetResults", false);
            }
        }
    }
}
