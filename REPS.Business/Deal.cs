using REPS.DATA.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPS.Business
{
    public class Deal
    {
        /// <summary>
        /// store procedure call to insert deal info 
        /// </summary>
        /// <param name="deal"></param>
        /// <returns></returns>
        public static int? AddDeal(DATA.Entity.Deal obj, string AspNetUsersId)
        {
            #region variables
            DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
            ObjectParameter identity = new ObjectParameter("identity", typeof(int));
            int? userID = (int?)Business.Deal.GetUserID(AspNetUsersId);
            #endregion end of variables 

            #region logic save data to db           
            REPSDB.REPS_AddDeal(
                                  obj.UniqueReference
                                , obj.DealTypeID
                                , obj.WorkflowTaskID
                                , DateTime.Now
                                , obj.ClientReference
                                , userID
                                , false
                                , identity
                                , obj.DealProcessTaskID
                );
            ///save to transaction log file
            return (identity.Value == null ? null : (int?)identity.Value);
            #endregion end logic save data to db   
        }

        /// <summary>
        /// get deal for company
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="taskID"></param>
        /// <param name="entityID"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        #region  get deal for company
        public static object GetDeal(DATA.Entity.Deal obj, int? taskID, int entityID, int? startRow = null, int? endRow = null)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables

                #region logic
                results = REPSDB.REPS_GetDeals
                    (
                        (int?)Common.CString.ReturnNullOrObjectForZero<int>(obj.UserID),
                        taskID,
                        obj.ClientReference,
                        obj.WorkflowTaskID,
                        obj.DealProcessTaskID,
                        (int?)Common.CString.ReturnNullOrObjectForZero<int>(obj.DealID),
                        obj.UniqueReference,
                        (int?)Common.CString.ReturnNullOrObjectForZero<int>(obj.DealTypeID),
                        entityID,
                        startRow,
                        endRow
                    );
                return results;
                #endregion end of logic
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion end of  get deal for company

        /// <summary>
        /// update deal 
        /// </summary>
        /// <param name="uniqueReference"></param>
        /// <param name="dealId"></param>
        /// <param name="dealTypeId"></param>
        /// <param name="clientReference"></param>
        /// <returns></returns>
        public static int? UpdateDeal(DATA.Entity.Deal obj)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion end of variables 

                #region logic : update deal 
                var updateDealResult = REPSDB.REPS_UpdateDeal
                    (
                        obj.UniqueReference,
                        obj.DealID,
                        obj.DealTypeID,
                        obj.ClientReference,
                        obj.DealProcessTaskID,
                        obj.DateModified,
                        rowCount
                    );
                return (rowCount.Value == DBNull.Value ? null : (int?)rowCount.Value);
                #endregion end of logic : update deal 
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// store procedure call to get deal types details
        /// </summary>
        /// <param name="dealTypeId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static object GetDealTypes(int? dealTypeId, int? startRow, int? endRow)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end variables

                #region logic
                results = REPSDB.REPS_GetDealTypes(dealTypeId, startRow, endRow);
                return results;
                #endregion end logic
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// get userid
        /// </summary>
        /// <param name="aspNetUsaerId"></param>
        /// <returns></returns>
        public static object GetUserID(string aspNetUserId)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end variables

                #region logic
                results = REPSDB.REPS_GetUserID(aspNetUserId).FirstOrDefault();
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
        /// <param name="dealGUID"></param>
        /// <returns></returns>
        public static object GetDealIDByDealUniqueRef(string dealUniqueRef)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end variables

                #region logic
                results = REPSDB.REPS_GETDealIDByDealGUID(dealUniqueRef).FirstOrDefault();
                return results;
                #endregion end logic
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Edit completion date
        /// </summary>
        /// <param name="DealID"></param>
        /// <param name="NewDate"></param>
        /// <returns></returns>
        public static int? EditCompletionDate(int dealID, DateTime newDate)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                ObjectParameter rowCount = new ObjectParameter("rowCount", typeof(int));
                #endregion end of variables

                #region logic
                REPSDB.REPS_UpdateDealCompletionDate_ByDealID(newDate, dealID, rowCount);
                return (rowCount.Value == null ? null : (int?)rowCount.Value);
                #endregion end of logic
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Get date completion
        /// </summary>
        /// <param name="DealID"></param>
        /// <returns></returns>
        public static object GetCompletionDate(int DealID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables

                #region logic
                results = REPSDB.REPS_GetDealCompletionDate_ByDealID(DealID);
                return results;
                #endregion end of logic
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        /// <summary>
        /// Get mandatory Actions list
        /// </summary>
        /// <param name="workflowID"></param>
        /// <returns></returns>
        public static object GetMandatoryActionsList(int workflowID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables

                #region logic : Get mandatory Actions list
                results = REPSDB.REPS_GetMandatoryActionsList(workflowID);
                return results;
                #endregion end of logic : Get mandatory Actions list
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Get completed Actions
        /// </summary>
        /// <param name="dealID"></param>
        /// <param name="workflowID"></param>
        /// <returns></returns>
        public static object GetCompletedActions(int dealID, int workflowID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables

                #region logic
                results = REPSDB.REPS_GetCompletedActionsByWorkflowID(dealID, workflowID);
                return results;
                #endregion end of logic
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        
        /// <summary>
        /// Get last Action done by whom
        /// </summary>
        /// <param name="DealID"></param>
        /// <returns></returns>
        public static object GetDealLastActionBy(int dealID)
        {
            try
            {
                #region variables
                DATA.Entity.REPSEntities REPSDB = new DATA.Entity.REPSEntities();
                object results = null;
                #endregion end of variables

                #region logic
                results = REPSDB.REPS_GetDealLastActionBy(dealID);
                return results;
                #endregion end of logic
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// get current workflow step 
        /// </summary>
        /// <param name="dealID"></param>
        /// <param name="workflowID"></param>
        /// <param name="userID"></param>
        /// <param name="entityID"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static DataTable GetCurrentWorkflowStep(int dealID, int workflowID, int? userID = null, int? entityID = null, int? startDate = null, int? endDate = null)
        {
            try
            {
                #region variables
                object context = new DATA.Entity.REPSEntities();
                object userIDResult = null;
                object startDateResult = null;
                object endDateResult = null;
                #endregion end of variables

                #region logic : get workflow per id
                {
                    if (userIDResult != null)
                    {
                        userIDResult = ",@userID=" + userID;
                    }
                    else
                    {
                        userIDResult = null;
                    }
                    if (startDateResult != null)
                    {
                        startDateResult = ",@StartDate=" + startDate;
                    }
                    else
                    {
                        startDateResult = null;
                    }
                    if (endDateResult != null)
                    {
                        endDateResult = ",@EndDate=" + endDate;
                    }
                    else
                    {
                        endDateResult = null;
                    }
                    string myQuery = string.Format("EXEC REPS_GetCurrentWorkflowStep_ByDealIDTransactionTypeID " + " @DealID=" + dealID + ",@WorkflowID=" + workflowID + userIDResult + ",@EntityID=" + entityID + startDateResult + endDateResult);
                    DataTable resultTable = new DataTable();

                    ///use of expando object to get anonymous resultset from query
                    foreach (IDictionary<string, object> keyValues in Common.CDynamicSqlRow.GetDynamicSql(myQuery, context).ToList())
                    {
                        /// Declare DataColumn and DataRow variables.
                        DataColumn column;
                        ///add columns
                        foreach (string str in keyValues.Keys)
                        {
                            /// Create  columns if not exists
                            if (!resultTable.Columns.Contains(str))
                            {
                                column = new DataColumn();
                                column.DataType = Type.GetType("System.String");
                                column.ColumnName = str;
                                resultTable.Columns.Add(column);
                            }
                        }
                        ///add rows                    
                        resultTable.Rows.Add(keyValues.Values.ToArray());
                    }
                    return resultTable;
                }
                #endregion end of logic : get workflow per id

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
