using REPS.DATA.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace REPS.Business
{
    public class Mortgage
    {
        public static object GetMortgageLender()
        {
            try
            {
                #region variables

                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;

                #endregion

                #region logic
                {
                    results = REPSDB.REPS_GetMortgageLender();

                    return results;
                }
                #endregion

            }
            catch (Exception Ex)
            {

                throw Ex;
            }

        }

        public static object GetMortgageType()
        {
            try
            {
                #region variables

                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;

                #endregion

                #region logic
                {
                    results = REPSDB.REPS_GetMortgageType();

                    return results;
                }
                #endregion

            }
            catch (Exception Ex)
            {

                throw Ex;
            }

        }

        public static object GetMortgageInterestType()
        {
            try
            {
                #region variables

                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;

                #endregion

                #region logic
                {
                    results = REPSDB.REPS_GetInterestType();

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
        /// insert mortgage sum to database
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="LenderID"></param>
        /// <param name="InstrumentTypeID"></param>
        /// <param name="InterestTypeID"></param>
        /// <param name="DealID"></param>
        /// <param name="aspNetId"></param>
        /// <returns></returns>
        public static object InsertFinancialInstrument(FinancialInstrument obj, int LenderID, int InstrumentTypeID, int InterestTypeID, int dealID, string aspNetId)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter identity = new ObjectParameter("identity", typeof(int));
                int userID = Convert.ToInt32(Business.Deal.GetUserID(aspNetId));
                #endregion end of variables 

                #region logic : insert mortgage details
                ///add transaction log
                REPSDB.REPS_AddFinancialInstrument(obj.ParticipantID, obj.Value, obj.Deposit, obj.Term, obj.InterestRate, LenderID, InstrumentTypeID, InterestTypeID, identity, dealID, (int)Global.Enums.WokflowTask.Mortgage, userID);
                return (identity.Value == null ? null : (int?)identity.Value);
                #endregion end of logic : insert mortgage details

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// insert financial transaction
        /// </summary>
        /// <param name="DealID"></param>
        /// <param name="InstrumentID"></param>
        /// <returns></returns>
        public static int? InsertFinancialTransaction(int DealID, int InstrumentID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter identity = new ObjectParameter("identity", typeof(int));
                #endregion end of variables

                #region logic
                REPSDB.REPS_AddFinancialTransaction(InstrumentID, DealID, identity);
                return (identity.Value == null ? null : (int?)identity.Value);
                #endregion end of logic
            }
            catch (Exception Ex)
            {
                throw Ex;
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
        public static int? UpdateFinancialInstrument(DATA.Entity.FinancialInstrument obj, int LenderID, int InstrumentTypeID, int InterestTypeID, int DealID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion end of variables

                #region logic : Update Financial Instrument
                {
                    IncomingWebRequestContext request = WebOperationContext.Current.IncomingRequest;
                    System.Net.WebHeaderCollection headers = request.Headers;
                    int userId = Convert.ToInt32(headers["UserID"]);
                    REPSDB.REPS_UpdateFinancialInstrument(obj.InstrumentID, obj.ParticipantID, obj.Value, obj.Deposit, obj.Term, obj.InterestRate, LenderID, InstrumentTypeID, InterestTypeID, rowCount);
                    return (rowCount.Value == null ? null : (int?)rowCount.Value);
                }
                #endregion end of logic : Update Financial Instrument
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public static object GetAllMortgages(int DealID, int? MortgageID)
        {
            try
            {
                #region variables

                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;

                #endregion

                #region logic
                {
                    results = REPSDB.REPS_GetMortgages_ByDealID(DealID, MortgageID);

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
        /// wcf call to remove mortmage per id
        /// </summary>
        /// <param name="InstrumentID"></param>
        /// <returns></returns>
        public static object RemoveMortgages(int InstrumentID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion end of variables

                #region logic
                REPSDB.REPS_UpdateMortgageStatus(InstrumentID, rowCount);
                return (rowCount.Value == null ? null : (int?)rowCount.Value);
                #endregion

            }
            catch (Exception Ex)
            {

                throw Ex;
            }

        }

        /// <summary>
        /// get total sum of mortgage purchase price 
        /// </summary>
        /// <param name="DealID"></param>
        /// <returns></returns>
        public static object GetSumValueMortgages(int DealID)
        {
            try
            {
                #region variables

                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities(); ;
                object results = null;

                #endregion

                #region logic
                {
                    results = REPSDB.REPS_GetMortgagePurchasePrice(DealID);

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
        /// store procedure call to get Mortgage id
        /// </summary>
        /// <param name="mortgageGUID"></param>
        /// <returns></returns>
        public static object GetMortgageIDByMortgageGUID(string mortgageGUID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end variables

                #region logic
                results = REPSDB.REPS_GetMortgageIDByMortgageGUID(mortgageGUID).FirstOrDefault();
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
