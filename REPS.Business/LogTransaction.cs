using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPS.Business
{
    public class LogTransaction
    {
        /// <summary>
        /// store procedure call to insert transaction log details
        /// </summary>
        /// <param name="tranasctionValues"></param>
        /// <param name="workflowTaskID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static object InsertTransactionLog(DATA.Entity.Transaction tranasctionValues, int workflowTaskID, int userID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables

                #region logic : store procedure call to insert transaction log details
                results = REPSDB.REPS_LogTransaction(tranasctionValues.DealID, tranasctionValues.TransactionTypeID, tranasctionValues.TransactionStatusID, workflowTaskID, userID);
                return results;
                #endregion end of logic : store procedure call to insert transaction log details
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
