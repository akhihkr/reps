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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MortgageService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MortgageService.svc or MortgageService.svc.cs at the Solution Explorer and start debugging.
    public class MortgageService : IMortgageService
    {
        /// <summary>
        /// get mortgage lender
        /// </summary>
        /// <returns></returns>
        public CValidator GetMortgageLender()
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Mortgage.GetMortgageLender()), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// get mortgage type
        /// </summary>
        /// <returns></returns>
        public CValidator GetMortgageType()
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Mortgage.GetMortgageType()), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// get mortage interest type
        /// </summary>
        /// <returns></returns>
        public CValidator GetMortgageInterestType()
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Mortgage.GetMortgageInterestType()), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// insert finnacial instrument
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="LenderID"></param>
        /// <param name="InstrumentTypeID"></param>
        /// <param name="InterestTypeID"></param>
        /// <param name="DealID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public CValidator InsertFinancialInstrument(DATA.Entity.FinancialInstrument obj, int LenderID, int InstrumentTypeID, int InterestTypeID, int DealID, string aspNetId)
        {
            try
            {
                //object result;
                //result = Business.Mortgage.InsertFinancialInstrument(obj, LenderID, InstrumentTypeID, InterestTypeID, DealID, aspNetId); //add detail to property tables
                //obj.InstrumentID = Convert.ToInt32(result);

                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Mortgage.InsertFinancialInstrument(obj, LenderID, InstrumentTypeID, InterestTypeID, DealID, aspNetId)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// insert financial transaction
        /// </summary>
        /// <param name="DealID"></param>
        /// <param name="InstrumentID"></param>
        /// <returns></returns>
        public CValidator InsertFinancialTransaction(DATA.Entity.FinancialTransaction obj, int DealID, int InstrumentID)
        {
            var serializer = new JavaScriptSerializer();
            string thisGuid = Guid.NewGuid().ToString();
            int userId = Convert.ToInt32(OperationContext.Current.IncomingMessageProperties["uuid"].ToString());
            int? result;
            try
            {
                result = Business.Mortgage.InsertFinancialTransaction(DealID, InstrumentID); //add detail to property tables
                obj.FinancialTransactionID = Convert.ToInt32(result);

                if (result > 0)
                {
                    ///add transaction log
                    Business.LogTransaction.InsertTransactionLog((new DATA.Entity.Transaction() { DealID = DealID, TransactionTypeID = (int)Global.Enums.TransactionType.New, TransactionStatusID = 4 }), (int)Enums.WokflowTask.Mortgage, userId);
                    ///end of add transaction log
                    return CValidator.initValidator("", serializer.Serialize(result), "AddedSuccessfully", true);
                }
                else
                {
                    return CValidator.initValidator(thisGuid, serializer.Serialize(result), "CouldNotAdd", false);
                }
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// Update Financial Instrument
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="LenderID"></param>
        /// <param name="InstrumentTypeID"></param>
        /// <param name="InterestTypeID"></param>
        /// <param name="DealID"></param>
        /// <returns></returns>
        public CValidator UpdateFinancialInstrument(DATA.Entity.FinancialInstrument obj, int LenderID, int InstrumentTypeID, int InterestTypeID, int DealID)
        {
            var serializer = new JavaScriptSerializer();
            string thisGuid = Guid.NewGuid().ToString();
            int userId = Convert.ToInt32(OperationContext.Current.IncomingMessageProperties["uuid"].ToString());
            int? result;
            try
            {
                if ((result = Business.Mortgage.UpdateFinancialInstrument(obj, LenderID, InstrumentTypeID, InterestTypeID, DealID)) > 0)
                {
                    ///add transaction log
                    Business.LogTransaction.InsertTransactionLog((new DATA.Entity.Transaction() { DealID = DealID, TransactionTypeID = (int)Enums.TransactionType.Edit, TransactionStatusID = 7 }), (int)Enums.WokflowTask.Mortgage, userId);
                    ///end of add transaction log
                    return CValidator.initValidator("", result.ToString(), "FetchedSuccessfully", true);
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
        /// get all mortgages 
        /// </summary>
        /// <param name="DealID"></param>
        /// <param name="MortgageID"></param>
        /// <returns></returns>
        public CValidator GetAllMortgages(int DealID, int? MortgageID)
        {
            try
            {

                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Mortgage.GetAllMortgages(DealID, MortgageID)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        public CValidator RemoveMortgages(int InstrumentID, int DealID)
        {
            //variables
            int userId = Convert.ToInt32(OperationContext.Current.IncomingMessageProperties["uuid"].ToString());
            //end of variables
            try
            {
                ///add transaction log
                Business.LogTransaction.InsertTransactionLog((new DATA.Entity.Transaction() { DealID = DealID, TransactionTypeID = (int)Global.Enums.TransactionType.Remove, TransactionStatusID = 6 }), (int)Enums.WokflowTask.Mortgage, userId);
                ///end of add transaction log
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Mortgage.RemoveMortgages(InstrumentID)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }

        /// <summary>
        /// call wcf to get total sum of mortgage purchase price 
        /// </summary>
        /// <param name="DealID"></param>
        /// <returns></returns>
        #region get total sum of mortgage purchase price 
        public CValidator GetSumValueMortgages(int DealID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Mortgage.GetSumValueMortgages(DealID)), "FetchedSuccessfully", true);
            }
            catch (Exception ex)
            {
                string thisGuid = Guid.NewGuid().ToString();
                Common.CLog.WriteLogInfo(thisGuid + ex.ToString(), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return CValidator.initValidator(thisGuid, ex.Message, "CouldNotGetResults", false);
            }
        }
        #endregion end get total sum of mortgage purchase price 

        
        /// <summary>
        /// Get Mortgage ID
        /// </summary>
        /// <param name="dealGUID"></param>
        /// <returns></returns>
        public CValidator GetMortgageIDByMortgageGUID(string mortgageGUID)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return CValidator.initValidator("", serializer.Serialize(Business.Mortgage.GetMortgageIDByMortgageGUID(mortgageGUID)), "FetchedSuccessfully", true);
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
