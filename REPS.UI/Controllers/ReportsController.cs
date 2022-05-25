using Global;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data;
using ExcelFuncs;
using System.IO;

namespace REPS.UI.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        [OutputCache(NoStore = true, Duration = 1)]
        public ActionResult Index()
        {
            try
            {
                ViewData["SelectedTab"] = Enums.PageNames.Reports.ToString();
                ///call wcf to get all report names
                #region call wcf to get all report names
                object getReportNames = Models.ReportModel.GetReportNames("Report", null, null, null, null, null);
                ViewData["reportNames"] = getReportNames;
                #endregion end call wcf to get all report names
                return View();
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// get the value of selected dropdwon menu
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetFilters()
        {
            try
            {
                #region Variables
                ///variables 
                string taskGUID = HttpContext.Request.Cookies["REPS_TaskGUID"].Value;
                string entityGUID = HttpContext.Request.Cookies["REPS_EntityGUID"].Value;
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
                int reportId = Convert.ToInt32(Request.Form["report_select"].Trim());
                ViewData["ReportId"] = reportId;
                int entityID = Convert.ToInt32(Models.EntityModel.GetEntityIDByEntityGUID(entityGUID));
                //form values and filters parameters
                var formObjectFilter = new Dictionary<string, dynamic>();
                //end of form values and filters parameters
                #endregion end o f variables


                ///wcf to call filter by id to check if match to database                  
                #region wcf to call filter by id 
                object reportFilter = Models.ReportModel.GetFilters(reportId, null, null);
                ViewData["reportFilter"] = reportFilter;
                #endregion end wcf to call filter by id

                #region get dropdowm values from per selected filter
                if (reportFilter != null)
                {
                    ///fill form to object///wcf to call dropdown menu after form is generated (refreshed)
                    formObjectFilter.Add("WorkflowID", dealWorkflowID);
                    formObjectFilter.Add("entityId", entityID);
                    Dictionary<string, object> dropDownFilters = Models.ReportModel.GetDropDown(reportFilter, formObjectFilter);
                    ViewData["dropDownFilters"] = dropDownFilters;

                }
                #endregion end ofget dropdowm values from per selected filter

                return View("PartialFiltersDropdown");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// get report result for chart and table
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Search(string input)
        {
            try
            {
                #region variables
                Dictionary<string, object> formObjects = new Dictionary<string, object>();

                var parameterForm = "";
                var tableValues = "";
                var tableVariables = "";
                var parameterStoreProcedure = "";
                string[] parameterString;
                string[] tableParameterString;
                var paramString = input.Split('&');
                int reportId = 0;
                #endregion end of variables
                ///check parameters
                foreach (var paramCheck in paramString)
                {
                    parameterString = paramCheck.Split('=');
                    parameterForm = parameterString[1];
                    parameterStoreProcedure = parameterString[0];
                    if (parameterStoreProcedure.Contains("report_select"))
                    {
                        reportId = Convert.ToInt32(parameterForm);
                    }
                    ///convert to date time else value is null
                    if (parameterStoreProcedure.Contains("startDate"))
                    {
                        if (parameterForm.ToString() != "")
                        {
                            parameterForm = "'" + DateTime.ParseExact(parameterForm, "dd/M/yyyy", CultureInfo.CurrentCulture).ToString("yyyy-M-dd 00:0:00") + "'";

                        }
                        else
                        {
                            parameterForm = "-1";
                        }

                    }
                    if (parameterStoreProcedure.Contains("endDate"))
                    {
                        if (parameterForm.ToString() != "")
                        {
                            parameterForm = "'" + DateTime.ParseExact(parameterForm, "dd/M/yyyy", CultureInfo.CurrentCulture).ToString("yyyy-M-dd 00:0:00") + "'";
                        }
                        else
                        {
                            parameterForm = "-1";
                        }
                    }
                    ///end of convert to date time else value is null
                    formObjects.Add(parameterStoreProcedure, parameterForm);
                }
                ///end check parameters

                #region Variables
                ///variables 
                ViewData["ReportId"] = reportId;
                string taskGUID = HttpContext.Request.Cookies["REPS_TaskGUID"].Value;
                string entityGUID = HttpContext.Request.Cookies["REPS_EntityGUID"].Value;
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
                int entityID = Convert.ToInt32(Models.EntityModel.GetEntityIDByEntityGUID(entityGUID));
                //form values and filters parameters

                var formObjectFilter = new Dictionary<string, dynamic>();
                //end of form values and filters parameters

                ///table & chart report parameters
                var tableProcedure = "";
                var tableParameter = "";
                var chartParameter = "";
                var chartProcedure = "";
                ///end of variables
                #endregion end o f variables

                #region Copy posted values
                Request.Form.CopyTo(formObjects);
                formObjects.Add("workflowID", dealWorkflowID);
                formObjects.Add("dealProcessTaskID", dealProcessTaskID);
                formObjects.Add("entityId", entityID);
                //formObjects.Add("taskID", 111);
                formObjects.Add("year", DateTime.Now.Year.ToString());
                formObjects.Add("workflowTaskID", (int)Enums.WokflowTask.Fees);
                formObjects.Add("feeTypeID", (int)Enums.FieldType.Type);
                formObjects.Add("valueTypeID", (int)Enums.FieldType.Value);
                formObjects.Add("variableTypeID", (int)Enums.FieldType.Value);
                ///end of add object & session to form
                #endregion end of copy posted values


                ///call wcf to get report details for each charts
                #region call wcf to get report details for each charts
                object getReportDetails = Models.ReportModel.GetReportNames("Report", null, null, null, null, null);
                ViewData["getReportDetail"] = getReportDetails;

                /// call wcf to get report procedure name 
                foreach (var item in getReportDetails as IEnumerable<dynamic>)
                {
                    if (item["ReportsId"] == reportId)
                    {
                        tableProcedure = item["TableProcedure"];
                        tableParameter = item["TableParameter"];
                        chartParameter = item["ChartParameter"];
                        chartProcedure = item["ChartProcedure"];
                        continue;
                    }
                }
                #endregion end call wcf to get report details for each charts

                ///call wcf for displaying table procedure
                #region call wcf for displaying table procedure
                if (tableProcedure != null)
                {
                    var tableProcedureSplit = tableParameter.Split(',');
                    foreach (var tableProcedureChk in tableProcedureSplit)
                    {
                        tableParameterString = tableProcedureChk.Split('=');
                        tableValues = tableParameterString[1];
                        tableVariables = tableParameterString[0];
                        foreach (var parameterIDCheck in formObjects as Dictionary<string, object>)
                        {
                            if (tableVariables == parameterIDCheck.Key)
                            {
                                formObjects.Add(tableVariables, tableValues);
                            }
                        }
                    }
                    ///end check parameters

                    ///call wcf to get tables reports
                    object getDealTableProcedure = Models.ReportModel.GetChartReport(tableParameter, tableProcedure, formObjects);
                    ViewData["getDealTableReport"] = getDealTableProcedure;
                }

                #endregion end call wcf for displaying table procedure

                ///call wcf for display charts 
                #region call wcf for display charts 
                if (chartProcedure != null)
                {
                    object getDealChartProcedure = Models.ReportModel.GetChartReport(chartParameter, chartProcedure, formObjects);
                    ViewData["getDealReports"] = getDealChartProcedure;
                }
                #endregion end call wcf for display charts       
                return PartialView("PartialSearchReportResult");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// generate reports
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GenerateReport(string input)
        //public ActionResult GenerateReport(FormCollection formCollections,string input)
        {
            try
            {

                //return RedirectToAction("GenerateReportResult","Reports",new { input= input });
                //string input = "startDate=" + startDate + "&endDate=" + endDate + "&report_select=" + report_select + "&userID=" + userID;
                var FormObjects = new Dictionary<string, dynamic>();
                //Copy posted values
                Request.Form.CopyTo(FormObjects);

                #region variables
                Dictionary<string, object> formObjects = new Dictionary<string, object>();
                var parameterForm = "";
                var tableValues = "";
                var tableVariables = "";
                var parameterStoreProcedure = "";
                string[] parameterString;
                string[] tableParameterString;
                var paramString = input.Split('&');
                int reportId = 0;
                #endregion end of variables

                #region  check parameters for date
                foreach (var paramCheck in paramString)
                {
                    parameterString = paramCheck.Split('=');
                    parameterForm = parameterString[1];
                    parameterStoreProcedure = parameterString[0];
                    if (parameterStoreProcedure.Contains("report_select"))
                    {
                        reportId = Convert.ToInt32(parameterForm);
                    }
                    ///convert to date time else value is null
                    if (parameterStoreProcedure.Contains("startDate"))
                    {
                        if (parameterForm.ToString() != "")
                        {
                            parameterForm = "'" + DateTime.ParseExact(parameterForm, "dd/M/yyyy", CultureInfo.CurrentCulture).ToString("yyyy-M-dd 00:0:00") + "'";

                        }
                        else
                        {
                            parameterForm = "-1";
                        }

                    }
                    if (parameterStoreProcedure.Contains("endDate"))
                    {
                        if (parameterForm.ToString() != "")
                        {
                            parameterForm = "'" + DateTime.ParseExact(parameterForm, "dd/M/yyyy", CultureInfo.CurrentCulture).ToString("yyyy-M-dd 00:0:00") + "'";
                        }
                        else
                        {
                            parameterForm = "-1";
                        }
                    }
                    ///end of convert to date time else value is null
                    formObjects.Add(parameterStoreProcedure, parameterForm);
                }
                #endregion end of check parameters for date

                #region Variables
                ///variables 
                ViewData["ReportId"] = reportId;
                string taskGUID = HttpContext.Request.Cookies["REPS_TaskGUID"].Value;
                string entityGUID = HttpContext.Request.Cookies["REPS_EntityGUID"].Value;
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
                int entityID = Convert.ToInt32(Models.EntityModel.GetEntityIDByEntityGUID(entityGUID));
                //form values and filters parameters

                var formObjectFilter = new Dictionary<string, dynamic>();
                //end of form values and filters parameters

                ///table & chart report parameters
                var tableProcedure = "";
                var tableParameter = "";
                var chartParameter = "";
                var chartProcedure = "";
                string ReportName = string.Empty;
                ///end of variables
                #endregion end o f variables

                #region Copy posted values
                Request.Form.CopyTo(formObjects);
                formObjects.Add("workflowID", dealWorkflowID);
                formObjects.Add("dealProcessTaskID", dealProcessTaskID);
                formObjects.Add("entityId", entityID);
                formObjects.Add("year", DateTime.Now.Year.ToString());
                formObjects.Add("workflowTaskID", (int)Enums.WokflowTask.Fees);
                formObjects.Add("feeTypeID", (int)Enums.FieldType.Type);
                formObjects.Add("valueTypeID", (int)Enums.FieldType.Value);
                formObjects.Add("variableTypeID", (int)Enums.FieldType.Value);
                ///end of add object & session to form
                #endregion end of copy posted values

                ///call wcf to get report details for each charts
                #region call wcf to get report details for each charts
                object getReportDetails = Models.ReportModel.GetReportNames("Report", null, null, null, null, null);
                ViewData["getReportDetail"] = getReportDetails;

                /// call wcf to get report procedure name 
                foreach (var item in getReportDetails as IEnumerable<dynamic>)
                {
                    if (item["ReportsId"] == reportId)
                    {
                        tableProcedure = item["TableProcedure"];
                        tableParameter = item["TableParameter"];
                        chartParameter = item["ChartParameter"];
                        chartProcedure = item["ChartProcedure"];
                        ReportName = item["Description"];
                        continue;
                    }
                }
                #endregion end call wcf to get report details for each charts

                ///call wcf for displaying table procedure
                #region call wcf for displaying table procedure
                if (tableProcedure != null)
                {
                    var tableProcedureSplit = tableParameter.Split(',');
                    foreach (var tableProcedureChk in tableProcedureSplit)
                    {
                        tableParameterString = tableProcedureChk.Split('=');
                        tableValues = tableParameterString[1];
                        tableVariables = tableParameterString[0];
                        foreach (var parameterIDCheck in formObjects as Dictionary<string, object>)
                        {
                            if (tableVariables == parameterIDCheck.Key)
                            {
                                formObjects.Add(tableVariables, tableValues);
                            }
                        }
                    }
                    ///end check parameters
                    ///call wcf to get tables reports
                    object getDealTableProcedure = Models.ReportModel.GetChartReport(tableParameter, tableProcedure, formObjects);
                    ViewData["getDealTableReport"] = getDealTableProcedure;
                }
                #endregion end call wcf for displaying table procedure

                ///call wcf for display charts 
                #region call wcf for display charts 
                if (chartProcedure != null)
                {
                    object getDealChartProcedure = Models.ReportModel.GetChartReport(chartParameter, chartProcedure, formObjects);
                    ViewData["getDealReports"] = getDealChartProcedure;
                }
                #endregion end call wcf for display charts       

                #region download Reports

                // we check if there is data for the report
                if (ViewData["getDealTableReport"] != null)
                {
                    // we create a datatable for the export
                    DataTable dealDataTable = new DataTable("dealTable");
                    var firstDeal = (ViewData["getDealTableReport"] as IEnumerable<dynamic>).FirstOrDefault();
                    foreach (var deal in firstDeal)
                    {
                        dealDataTable.Columns.Add(deal.Key); // we add the columns to the datatable
                    }

                    foreach (var deal in ViewData["getDealTableReport"] as IEnumerable<dynamic>)
                    {
                        DataRow dr = dealDataTable.NewRow();
                        int columnCounter = 0;
                        foreach (var dealDetail in deal as Dictionary<string, dynamic>) // we add the rows
                        {
                            if (dealDetail.Key == "Date Created")
                            {
                                dr[columnCounter] = DateTime.Parse(dealDetail.Value).ToString("dd/MM/yyyy");
                            }
                            else
                            {
                                dr[columnCounter] = dealDetail.Value;
                            }
                            columnCounter++;
                        }
                        // we add all the rows to the datatable
                        dealDataTable.Rows.Add(dr);
                    }
                    DataSet set = new DataSet("dealsReport");
                    set.Tables.Add(dealDataTable); // we add the datatable to the dataset
                    string fileType = "xls";
                    string pathXMLfile = ConfigurationManager.AppSettings["ReportTemplate"] + ReportName.Replace(" ", string.Empty) + "Export.xml";
                    var bytes = CTemplateRenderer.templateRenderGeneic(pathXMLfile, set, out fileType);
                    //ViewData["fileTodownload"] = GetDownload(File(bytes, "application/ms-excel", ReportName.Replace(" ", string.Empty) + "Export.xls"));
                    //return PartialView("PartialSearchReportResult");



                    //Response.AddHeader("content-disposition", "attachment" + ReportName.Replace(" ", string.Empty) + "Export.xls;");
                    //Response.ContentType = "application/ms-excel";

                    return this.File(bytes, "application/ms-excel", ReportName.Replace(" ", string.Empty) + "Export.xls");
                }
                else
                {
                    TempData["ToasterMsg"] = Notifications.GetToastrMessage(Enums.MessageType.warning.ToString(), "No information for this Deal Report");
                    return RedirectToAction("Index", "Report");
                }
                #endregion download reports
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        [HttpGet]
        public FileContentResult GetDownload(FileContentResult file)
        {
            return file;
        }
        [HttpGet]
        public FileContentResult GetDownloads(string file)
        {
            return null; 
        }
    }
}