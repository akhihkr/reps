using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.Objects;

namespace REPS.Business
{
    public class HeaderTab
    {
        /// <summary>
        /// Add 
        /// </summary>
        /// <param name="aspNetUserId"></param>
        /// <param name="DealID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public static int? InsertDeleteUserDeals(string aspNetUserId, int DealID, int Status)
        {
            try
            {
                #region variables

                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));

                #endregion

                #region logic

                int? userID = (int?)Business.Deal.GetUserID(aspNetUserId);
                REPSDB.REPS_AddUserDeal(DealID, userID, Status, 4);
                return (rowCount.Value == null ? null : (int?)rowCount.Value);

                #endregion

            }
            catch (Exception Ex)
            {

                throw Ex;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aspNetUserId"></param>
        /// <returns></returns>
        public static int? DeleteAllUserDeals(string aspNetUserId)
        {
            try
            {
                #region variables

                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));

                #endregion

                #region logic

                int? userID = (int?)Business.Deal.GetUserID(aspNetUserId);
                var result = REPSDB.REPS_DeleteAllTabsForUser_ByUserGUID(userID, rowCount).ToList();
                return (rowCount.Value == null ? 1 : (int?)rowCount.Value);

                #endregion

            }
            catch (Exception Ex)
            {

                throw Ex;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aspNetUserId"></param>
        /// <param name="DealID"></param>
        /// <param name="ViewName"></param>
        /// <returns></returns>
        public static int? InsertLastViewName(string aspNetUserId, int DealID, string ViewName)
        {
            try
            {
                #region variables

                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));

                #endregion

                #region logic

                int? userID = (int?)Business.Deal.GetUserID(aspNetUserId);
                REPSDB.REPS_UpdateLastActiveView_ByDealGUIDUserGUID(userID, DealID, ViewName, rowCount);
                return (rowCount.Value == null ? null : (int?)rowCount.Value);

                #endregion

            }
            catch (Exception Ex)
            {

                throw Ex;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aspNetId"></param>
        /// <returns></returns>
        public static object GetUserActiveDeals(string aspNetId)
        {
            try
            {
                #region variables

                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;

                #endregion

                #region logic

                int? userID = (int?)Business.Deal.GetUserID(aspNetId);
                results = REPSDB.REPS_GetHeaderTabUserActiveDeals_ByUserGUID(userID, 4);
                return results;

                #endregion

            }
            catch (Exception Ex)
            {

                throw Ex;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aspNetUserId"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public static int? ToggleHeaderTabsActivation(string aspNetUserId, bool Status)
        {
            try
            {
                #region variables

                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));

                #endregion

                #region logic

                int? userID = (int?)Business.Deal.GetUserID(aspNetUserId);
                REPSDB.REPS_UpdateUser_ToggleHeaderTab_ByUserID(Status, userID, rowCount);
                return (rowCount.Value == null ? null : (int?)rowCount.Value);

                #endregion

            }
            catch (Exception Ex)
            {

                throw Ex;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aspNetId"></param>
        /// <returns></returns>
        public static object GetUserNotificationBarActiveDeals(string aspNetId)
        {
            try
            {
                #region variables

                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;

                #endregion

                #region logic

                int? userID = (int?)Business.Deal.GetUserID(aspNetId);
                results = REPSDB.REPS_GetHeaderTabNotificationBarActiveDeals_ByUserGUID(userID, 4);
                return results;

                #endregion

            }
            catch (Exception Ex)
            {

                throw Ex;
            }

        }
    }
}
