using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Global;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Data;
using REPS.DATA.Entity;

namespace REPS.UI.Controllers
{
    [TokenAuthorizeAttribute]
    public class DashboardController : Controller
    {
        // GET: Dashboard
        //[OutputCache(NoStore = true, Duration = 1)]
        public ActionResult Index(string error = null)
        {
            try
            {
                // Set Tab title dashboard
                ViewData["SelectedTab"] = Enums.PageNames.Dashboard.ToString();
                string userFullName = null;

                if (Request.Cookies["REPS_AspNetUsersId"] != null) // We check if the cookie has been set
                {
                    object[] userInfo = Models.UserModel.GetUserInfo(null, HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value.ToString());
                    bool headerTabStatus = false;

                    foreach (var userprofile in userInfo as IEnumerable<dynamic>)
                    {
                        if (userprofile["GivenName"] != null || userprofile["FamilyName"] != null)
                        {
                            userFullName = userprofile["GivenName"] + " " + userprofile["FamilyName"];
                        }
                        if (userprofile["HeaderTabToggle"] != null) // We check if the user has enabled the Tabbing Feature in his/her profile
                            headerTabStatus = userprofile["HeaderTabToggle"];
                        else
                            headerTabStatus = false;
                    }
                    HttpContext.Response.Cookies.Add(new HttpCookie("REPS_HeaderTab", headerTabStatus.ToString()));
                }

                string entityGUID = HttpContext.Request.Cookies["REPS_EntityGUID"].Value;
                string taskGUID = HttpContext.Request.Cookies["REPS_TaskGUID"].Value;


                //ViewData["username"] = HttpContext.Request.Cookies["REPS_FullName"].Value;
                ViewData["username"] = userFullName;


                // set variable to indicate whether to display company data
                ViewData["company"] = "companyAccount";
                //call wcf to get id by GUID
                int entityID = Convert.ToInt32(Models.EntityModel.GetEntityIDByEntityGUID(entityGUID));
                int dealProcessTaskID = 0;
                int dealWorkflowID = 0;
                object taskIDWorkflowIDResult = Models.WorkflowModel.GetWorkflowIDTaskID(taskGUID);

                //try to pass table names and get the columns header values


                //REPSEntities REPSDB  = new REPSEntities();
                //string tableName  = typeof(Task).ToString();

                // Task tablenameTask = new Task();
                //tablenameTask.TaskName.ToString();

                //end of try to pass table names and get the columns header values

                foreach (var resultID in taskIDWorkflowIDResult as Dictionary<string, dynamic>)
                {
                    if (resultID.Key == "TaskID")
                    {
                        dealProcessTaskID = resultID.Value;
                    }
                    else if (resultID.Key == "TaskWorkflowID")
                    {
                        dealWorkflowID = resultID.Value;
                    }
                }
                //end of call wcf to get id by GUID

                // Call WCF to get all Deal
                Deal deal = new Deal();
                deal.DealProcessTaskID = dealProcessTaskID;
                deal.WorkflowTaskID = null;

                //object[] resultDeals = Models.DealModel.GetDeals(deal, null, null, entityGUID, taskGUID, null, null);
                object[] resultDeals = Models.DealModel.GetDeal(deal, null, entityID, null, null);
                var FiveLatestDeals = (resultDeals as IEnumerable<dynamic>).Take(5);        // Select TOP 5 latest deals for dashboard
                ViewData["dealsResult"] = FiveLatestDeals;


                //table & chart report parameters
                var chartParameter = "";
                var chartProcedure = "";
                var chartType = "";
                var location = "";
                //end of table & chart report parameters



                #region Roles - Set user roles
                if (Request.Cookies["UserRolesActions"] == null)
                {
                    var userID = Models.UserModel.GetUserIDByAspNetID(HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value.ToString());
                    object userRoleActions = Models.RoleModel.GetUserRoleActions(Convert.ToInt32(userID));

                    HashSet<string> hashRoles = new HashSet<string>(); // We declare the hashset which will contain the userroles
                    foreach (var role in userRoleActions as IEnumerable<dynamic>)
                    {
                        hashRoles.Add(role["Code"]); // We add the roles to the hashset
                    }

                    var serializer = new JavaScriptSerializer();
                    string hashRolesString = serializer.Serialize(hashRoles);
                    HttpContext.Response.Cookies.Add(new HttpCookie("UserRolesActions", hashRolesString));
                }
                #endregion Roles - Set user roles

                #region try catch
                try
                {
                    #region call wcf to get all report names
                    object getReportDetails = Models.ReportModel.GetReportNames("Dashboard", null, null, null, null, null);
                    ViewData["getReportDetails"] = getReportDetails;
                    #endregion end call wcf to get all report names

                    // form object variables
                    var formObjects = new Dictionary<string, dynamic>();
                    formObjects.Add("workflowTaskID", (int)Enums.WokflowTask.Fees);
                    formObjects.Add("workflowID", dealWorkflowID);
                    formObjects.Add("dealProcessTaskID", dealProcessTaskID);
                    formObjects.Add("entityId", entityID);
                    //formObjects.Add("taskID", 111);
                    formObjects.Add("feeTypeID", (int)Enums.FieldType.Type);
                    formObjects.Add("valueTypeID", (int)Enums.FieldType.Value);
                    formObjects.Add("variableTypeID", (int)Enums.FieldType.Value);
                    formObjects.Add("year", DateTime.Now.Year.ToString());
                    // end of form object variables

                    //dashboard chart data
                    Dictionary<string, object> dasbboardCharts = new Dictionary<string, object>();

                    //call wcf to get report procedure name 
                    foreach (var item in getReportDetails as IEnumerable<dynamic>)
                    {
                        chartType = item["ChartType"];
                        location = item["location"];
                        chartParameter = item["ChartParameter"];
                        chartProcedure = item["ChartProcedure"];
                        ///call wcf for display charts 
                        #region call wcf for display charts 
                        if (chartType != "table" && location == "Dashboard")
                        {
                            object getDealChartProcedure = Models.ReportModel.GetChartReport(chartParameter, chartProcedure, formObjects);
                            if (getDealChartProcedure != null)
                            {
                                dasbboardCharts.Add("chart_" + item["Order"] + "_" + item["ChartType"], getDealChartProcedure);
                            }
                        }
                        #endregion end call wcf for display charts                    
                        continue;
                    }
                    ViewData["DashboardChartsDetails"] = dasbboardCharts; ///assign all chart
                    //end of call wcf to get report procedure name 
                    //end of dashboard chart data
                    ViewData["links"] = "Company";
                    if (!string.IsNullOrEmpty(error))
                    {
                        return View("Index", new { error = error });
                    }

                    return View();
                }
                catch (Exception ex)
                {
                    Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                    ViewData["DashboardChartsDetails"] = null;
                    ViewData["links"] = "Company";
                    return View();
                }
                #endregion end of try catch
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// get info for user only
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public new ActionResult User(string error = null)
        {
            try
            {
                // Set Tab title dashboard
                ViewData["SelectedTab"] = Enums.PageNames.Dashboard.ToString();
                string userFullName = null;
                if (Request.Cookies["REPS_AspNetUsersId"] != null) // We check if the cookie has been set
                {
                    object[] userInfo = Models.UserModel.GetUserInfo(null, HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value.ToString());
                    bool headerTabStatus = false;

                    foreach (var userprofile in userInfo as IEnumerable<dynamic>)
                    {
                        if (userprofile["GivenName"] != null || userprofile["FamilyName"] != null)
                        {
                            userFullName = userprofile["GivenName"] + " " + userprofile["FamilyName"];
                        }
                        if (userprofile["HeaderTabToggle"] != null) // We check if the user has enabled the Tabbing Feature in his/her profile
                            headerTabStatus = userprofile["HeaderTabToggle"];
                        else
                            headerTabStatus = false;
                    }
                    HttpContext.Response.Cookies.Add(new HttpCookie("REPS_HeaderTab", headerTabStatus.ToString()));
                }

                string entityGUID = HttpContext.Request.Cookies["REPS_EntityGUID"].Value;
                string taskGUID = HttpContext.Request.Cookies["REPS_TaskGUID"].Value;
                //ViewData["username"] = HttpContext.Request.Cookies["REPS_FullName"].Value;
                ViewData["username"] = userFullName;


                //call wcf to get id by GUID
                var userID = Models.UserModel.GetUserIDByAspNetID(HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value.ToString());
                int entityID = Convert.ToInt32(Models.EntityModel.GetEntityIDByEntityGUID(entityGUID));
                int dealProcessTaskID = 0;
                int dealWorkflowID = 0;
                object taskIDWorkflowIDResult = Models.WorkflowModel.GetWorkflowIDTaskID(taskGUID);
                foreach (var resultID in taskIDWorkflowIDResult as Dictionary<string, dynamic>)
                {
                    if (resultID.Key == "TaskID")
                    {
                        dealProcessTaskID = resultID.Value;
                    }
                    else if (resultID.Key == "TaskWorkflowID")
                    {
                        dealWorkflowID = resultID.Value;
                    }
                }
                //end of call wcf to get id by GUID

                // Call WCF to get all Deal
                Deal deal = new Deal();
                deal.DealProcessTaskID = dealProcessTaskID;
                deal.WorkflowTaskID = null;
                deal.UserID = Convert.ToInt32(userID);
                object[] resultDeals = Models.DealModel.GetDeal(deal, null, entityID, null, null);

                var FiveLatestDeals = (resultDeals as IEnumerable<dynamic>).Take(5);        // Select TOP 5 latest deals for dashboard
                ViewData["dealsResult"] = FiveLatestDeals;

                //table & chart report parameters
                var chartParameter = "";
                var chartProcedure = "";
                var chartType = "";
                var location = "";
                //end of table & chart report parameters

                #region try catch
                try
                {
                    #region call wcf to get all report names
                    object getReportDetails = Models.ReportModel.GetReportNames("Dashboard", null, null, null, null, null);
                    ViewData["getReportDetails"] = getReportDetails;
                    #endregion end call wcf to get all report names

                    // form object variables
                    var formObjects = new Dictionary<string, dynamic>();
                    formObjects.Add("workflowTaskID", (int)Enums.WokflowTask.Fees);
                    formObjects.Add("workflowID", dealWorkflowID);
                    formObjects.Add("dealProcessTaskID", dealProcessTaskID);
                    formObjects.Add("entityId", entityID);
                    //formObjects.Add("taskID", 111);
                    formObjects.Add("feeTypeID", (int)Enums.FieldType.Type);
                    formObjects.Add("valueTypeID", (int)Enums.FieldType.Value);
                    formObjects.Add("userID", Convert.ToInt32(userID));
                    formObjects.Add("variableTypeID", (int)Enums.FieldType.Value);
                    formObjects.Add("year", DateTime.Now.Year.ToString());
                    // end of form object variables

                    //dashboard chart data
                    Dictionary<string, object> dasbboardCharts = new Dictionary<string, object>();

                    //call wcf to get report procedure name 
                    foreach (var item in getReportDetails as IEnumerable<dynamic>)
                    {
                        chartType = item["ChartType"];
                        location = item["location"];
                        chartParameter = item["ChartParameter"];
                        chartProcedure = item["ChartProcedure"];
                        ///call wcf for display charts 
                        #region call wcf for display charts 
                        if (chartType != "table" && location == "Dashboard")
                        {
                            object getDealChartProcedure = Models.ReportModel.GetChartReport(chartParameter, chartProcedure, formObjects);
                            if (getDealChartProcedure != null)
                            {
                                dasbboardCharts.Add("chart_" + item["Order"] + "_" + item["ChartType"], getDealChartProcedure);
                            }
                        }
                        #endregion end call wcf for display charts                    
                        continue;
                    }
                    ViewData["DashboardChartsDetails"] = dasbboardCharts; ///assign all chart
                    //end of call wcf to get report procedure name 
                    //end of dashboard chart data
                    ViewData["links"] = "Account";
                    if (!string.IsNullOrEmpty(error))
                    {
                        return View("Index", new { error = error });
                    }

                    return View("Index");
                }
                catch (Exception ex)
                {
                    Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                    ViewData["DashboardChartsDetails"] = null;
                    ViewData["links"] = "Account";
                    return View("Index");
                }
                #endregion end of try catch
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// get  details from objid
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDashboardDetails(string objID)
        {
            try
            {
                //get cookies variables
                string entityGUID = HttpContext.Request.Cookies["REPS_EntityGUID"].Value;
                string taskGUID = HttpContext.Request.Cookies["REPS_TaskGUID"].Value;
                //end of get cookies variables
                object dealIDObject = Models.DealModel.GetDealIDByDealUniqueRef(objID); // We get the DealID from the URL
                int dealID = Convert.ToInt32(dealIDObject);
                //call wcf to get id by GUID
                int entityID = Convert.ToInt32(Models.EntityModel.GetEntityIDByEntityGUID(entityGUID));
                int dealProcessTaskID = 0;
                int dealWorkflowID = 0;
                object taskIDWorkflowIDResult = Models.WorkflowModel.GetWorkflowIDTaskID(taskGUID);
                foreach (var resultID in taskIDWorkflowIDResult as Dictionary<string, dynamic>)
                {
                    if (resultID.Key == "TaskID")
                    {
                        dealProcessTaskID = resultID.Value;
                    }
                    else if (resultID.Key == "TaskWorkflowID")
                    {
                        dealWorkflowID = resultID.Value;
                    }
                }
                //end of call wcf to get id by GUID

                // Call WCF to get Deal info per deal id
                Deal deal = new Deal();
                deal.DealID = dealID;
                deal.DealProcessTaskID = dealProcessTaskID;
                deal.WorkflowTaskID = null;

                object[] resultDeals = Models.DealModel.GetDeal(deal, null, entityID, null, null);
                // end of Call WCF to get Deal info per deal id
                ViewData["DashboardDetails"] = resultDeals;
                return View("PopupDashboard");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        public ActionResult RefreshSidebar()
        {
            try
            {
                return PartialView("PartialSidebar");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }
    }
}