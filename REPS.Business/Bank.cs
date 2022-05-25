using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPS.Business
{
    public class Bank
    {
        /// <summary>
        /// Get Account Types
        /// </summary>
        /// <param name="accountTypeId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static object GetAccountType(int? accountTypeId, int? startRow, int? endRow)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables

                #region logic
                results = REPSDB.REPS_GetAccountTypes(accountTypeId, startRow, endRow);
                return results;
                #endregion end of logic
            }
            catch (Exception Ex)
            {
                throw Ex;
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
        public static object GetBanks(string bankName, string swiftCode, int? entityId, int? bankId, int? startRow, int? endRow)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables

                #region logic : Get Banks
                {
                    results = REPSDB.REPS_GetBanks(bankName, swiftCode, entityId, bankId, startRow, endRow);
                    return results;
                }
                #endregion end of logic : Get Banks
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        ///  get bank detail per participant to edit
        /// </summary>
        /// <param name="participantId"></param>
        /// <param name="participantBankDetailID"></param>
        /// <returns></returns>
        public static object GetBankDetailsPerParticipant(int? participantId, int? participantBankDetailID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion

                #region logic : get bank detail per participant to edit
                results = REPSDB.REPS_GetBankDetailsPerParticipant(participantId, participantBankDetailID);
                return results;
                #endregion end of logic : get bank detail per participant to edit
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

    }
}
