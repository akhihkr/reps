using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.Objects;
using Global;

namespace REPS.Business
{
    public class Alerts
    {
        public static int? InsertAlerts(string EventName, string Location, DateTime StartDate, DateTime EndDate, string Description, string aspNetId, int AlertTypeID, int DealID)
        {
            try
            {
                #region variables
                int userID = Convert.ToInt32(Business.Deal.GetUserID(aspNetId));
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter identity = new ObjectParameter("identity", typeof(int));
                #endregion end of variables 

                #region logic

                REPSDB.REPS_AddAlerts(EventName, Location, StartDate, EndDate, Description, userID, AlertTypeID, DealID, (int)Enums.WokflowTask.Alerts, identity);
                return (identity.Value == null ? null : (int?)identity.Value);

                #endregion

            }
            catch (Exception Ex)
            {

                throw Ex;
            }

        }

        public static int? UpdateAlerts(string EventName, string Location, DateTime StartDate, DateTime EndDate, string Description, int AlertID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion end of variables

                #region logic

                REPSDB.REPS_UpdateAlerts_ByAlertID(EventName, Location, StartDate, EndDate, Description, AlertID, rowCount);
                return (rowCount.Value == null ? null : (int?)rowCount.Value);

                #endregion

            }
            catch (Exception Ex)
            {

                throw Ex;
            }

        }

        public static int? UpdateAlertsStatus(int AlertID, int StatusID)
        {
            try
            {
                #region variables

                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));

                #endregion

                #region logic

                REPSDB.REPS_UpdateAlertsStatus_ByAlertID(StatusID, AlertID, rowCount);
                return (rowCount.Value == null ? null : (int?)rowCount.Value);

                #endregion

            }
            catch (Exception Ex)
            {

                throw Ex;
            }

        }

        public static object GetAlertsInfo(string aspNetId, string Filter, int DealID)
        {
            try
            {
                #region variables

                int userID = Convert.ToInt32(Business.Deal.GetUserID(aspNetId));
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;

                #endregion

                #region logic

                results = REPSDB.REPS_GetAlerts_ByUserID(userID, Filter, DealID);
                return results;

                #endregion

            }
            catch (Exception Ex)
            {

                throw Ex;
            }

        }

        public static object GetEventByID(int EventID)
        {
            try
            {
                #region variables

                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;

                #endregion

                #region logic

                results = REPSDB.REPS_GetAlerts_ByAlertID(EventID);
                return results;

                #endregion

            }
            catch (Exception Ex)
            {

                throw Ex;
            }

        }

        public static object GetAlertsForDiaryItems(string aspNetId)
        {
            try
            {
                #region variables

                int userID = Convert.ToInt32(Business.Deal.GetUserID(aspNetId));
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;

                #endregion

                #region logic

                results = REPSDB.REPS_GetDiaryItems_ByUserID(userID);
                return results;

                #endregion

            }
            catch (Exception Ex)
            {

                throw Ex;
            }

        }


        /// <summary>
        /// store procedure call to get alerts id
        /// </summary>
        /// <param name="alertsGUID"></param>
        /// <returns></returns>
        public static object GetAlertsIDByAlertsGUID(string alertsGUID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end variables

                #region logic
                results = REPSDB.REPS_GetAlertsIDByAlertsGUID(alertsGUID).FirstOrDefault();
                return results;
                #endregion end logic
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// store procedure call to get deal id
        /// </summary>
        /// <param name="alertsGUID"></param>
        /// <returns></returns>
        public static object GetDealIDByAlertsGUID(string alertsGUID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end variables

                #region logic
                results = REPSDB.REPS_GetDealIDByAlertsGUID(alertsGUID).FirstOrDefault();
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
