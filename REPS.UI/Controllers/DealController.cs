using Global;
using REPS.DATA.Entity;
using REPS.UI.DealServiceReference;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace REPS.UI.Controllers
{
    [TokenAuthorizeAttribute]
    public class DealController : Controller
    {
        // GET: Deal
        [OutputCache(NoStore = true, Duration = 1)]
        public ActionResult Index(string taskStatus)
        {
            try
            {
                // Set Tab title deals
                ViewData["SelectedTab"] = Enums.PageNames.Deal.ToString();
                ViewBag.PageLayoutPath = Constants.MainLayoutPath;
                // End Set Tab title deals

                //declare variables 
                int? taskID = null;
                int dealProcessTaskID = 0;
                int dealWorkflowID = 0;
                var taskName = "";
                //end of declare variables

                #region  call wcf to get id by GUID
                //get values from cookies when user login
                string entityGUID = HttpContext.Request.Cookies["REPS_EntityGUID"].Value;
                string taskGUID = HttpContext.Request.Cookies["REPS_TaskGUID"].Value;
                //end of get values from cookies when user login

                int entityID = Convert.ToInt32(Models.EntityModel.GetEntityIDByEntityGUID(entityGUID));//wcf call to get entity value per user login

                //wcf call to get taskID and taskWorkFLowID from task-table per user login (EG. if ABSA Bonds / Seller Workflow)
                object taskIDWorkflowIDResult = Models.WorkflowModel.GetWorkflowIDTaskID(taskGUID);
                foreach (var resultID in taskIDWorkflowIDResult as Dictionary<string, dynamic>)
                {
                    if (resultID.Key == "TaskID")
                    {
                        dealProcessTaskID = resultID.Value; //TaskID
                    }
                    else if (resultID.Key == "TaskWorkflowID")
                    {
                        dealWorkflowID = resultID.Value;//WorkFLowID
                    }
                }
                //End of wcf call to get taskID and taskWorkFLowID from task-table per user login (EG.if ABSA Bonds / Seller Workflow)
                #endregion end of call wcf to get id by GUID

                #region wcf call to get task id
                //get specific task id from task-table when taskStatus is required 
                if (taskStatus != Global.Constants.all)
                {
                    object taskIDServalcall = Models.WorkflowModel.GetWorkflowIDTaskID(taskStatus);
                    foreach (var taskIDResult in taskIDServalcall as Dictionary<string, dynamic>)
                    {
                        if (taskIDResult.Key == "TaskID")
                        {
                            taskID = taskIDResult.Value; //(New Deal, Archive...)
                        }
                        if(taskIDResult.Key == "TaskName")
                        {
                            taskName = taskIDResult.Value;
                        }
                    }
                    ViewData["TaskName"] = taskName;
                }
                //end of get specific task id from task-table when taskStatus is required 
                #endregion end of wcf call to get task id

                //deal object values
                Deal deal = new Deal();
                deal.DealProcessTaskID = dealProcessTaskID; //TaskID per user login (EG. 120 or 121 or other task-id)

                #region servercall to display all deals data
                // Call WCF to get all Deal
                if (taskStatus == Global.Constants.all) //if url: param is "taskStatus=all", get all deal 
                {
                    deal.WorkflowTaskID = null; // set workflow null to get all deal info
                    ViewData["Deals"] = Models.DealModel.GetDeal(deal, null, entityID, null, null);
                }
                else //otherwise get all deal per *taskID
                {
                    deal.WorkflowTaskID = dealWorkflowID;  // set workflow the current workflowID to get deal info per user login
                    ViewData["Deals"] = Models.DealModel.GetDeal(deal, taskID, entityID, null, null);               
                }
                // End of Call WCF to get all Deal
                #endregion End servercall to display all deals data

                return View();

            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        //[OutputCache(NoStore = true, Duration = 1)]
        public ActionResult ViewDeal(bool partial = false)
        {
            try
            {

                string taskGUID = HttpContext.Request.Cookies["REPS_TaskGUID"].Value;
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

                int dealID = 0;
                string UniqueReference = string.Empty;
                // Check if Ajax Request and determine layout to be used
                if (Request.IsAjaxRequest())
                {
                    #region Get DealID from URL Parameter (Unique Reference)

                    if (Request.UrlReferrer.Query == null)
                    {
                        return Content(Enums.UniqueReference.Invalidreference.ToString());
                    }
                    UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
                    if (UniqueReference != Enums.UniqueReference.Invalid.ToString())
                    {
                        object dealIDObject = Models.DealModel.GetDealIDByDealUniqueRef(UniqueReference); // We get the DealID from the UR
                        if (dealIDObject == null)
                        {
                            return Content(Enums.UniqueReference.Invalidreference.ToString());
                        }
                        dealID = Convert.ToInt32(dealIDObject);
                    }
                    else
                    {
                        return Content(Enums.UniqueReference.Invalidreference.ToString());
                    }

                    #endregion Get DealID from URL Parameter (Unique Reference)

                    ViewBag.PageLayoutPath = null;
                }
                else
                {
                    #region Get DealID from URL Parameter (Unique Reference)

                    if (Request["URef"] == null)
                    {
                        UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
                    }
                    else
                    {
                        UniqueReference = Common.CUniqueReference.GetUniqueReferenceNonAjaxRequest(Request["URef"]);
                    }

                    if (UniqueReference != Enums.UniqueReference.Invalid.ToString())
                    {
                        object dealIDObject = Models.DealModel.GetDealIDByDealUniqueRef(UniqueReference); // We get the DealID from the UR
                        if (dealIDObject == null)
                        {
                            return RedirectToAction("Index", "Dashboard");
                        }
                        dealID = Convert.ToInt32(dealIDObject);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }

                    #endregion Get DealID from URL Parameter (Unique Reference)

                    ViewBag.PageLayoutPath = Global.Constants.MainLayoutPath;
                }
                ViewBag.DealID = dealID;

                #region deal form Values and entity cookies
                Deal deal = new Deal();
                deal.DealID = dealID;

                //entity
                string entityGUID = HttpContext.Request.Cookies["REPS_EntityGUID"].Value;
                int entityID = Convert.ToInt32(Models.EntityModel.GetEntityIDByEntityGUID(entityGUID));
                #endregion End of deal form values and entity cookies

                #region Set current workflow
                /// Call WCF to get Workflow Task Name
                object workflowName = Models.WorkflowModel.GetWorkFlowTaskNameByDealID(dealID);
                HttpContext.Response.Cookies.Add(new HttpCookie("CurrentDealWorkflow", workflowName.ToString()));
                ViewData["CurrentDealWorkflow"] = workflowName.ToString();
                #endregion End Set current workflow

                #region deal edit: get deal values from form
                //get selected values from form for edit process
                object dealEdit = Models.DealModel.GetDeal(deal, null, entityID, null, null);
                ViewData["dealEdit"] = dealEdit;
                //end of get selected values from form for edit process
                #endregion end of deal edit : get deal values from form

                #region Header Tabs
                if (Request.Cookies["REPS_HeaderTab"] != null && Request.Cookies["REPS_HeaderTab"].Value.ToString() == "True")
                {
                    /// Call WCF to insert User Deals
                    Models.HeaderTabModel.InsertDeleteUserDeals(dealID, HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value.ToString(), 0);
                    #region HeaderTab Configurations - save current open action

                    string ViewLink = Global.ViewDetails.GenerateControllerViewLink(ControllerContext.RouteData.GetRequiredString("action"), ControllerContext.RouteData.GetRequiredString("controller"));
                    Models.HeaderTabModel.InsertLastViewName(HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value.ToString(), dealID, ViewLink);

                    #endregion HeaderTab Configurations - save current open action
                }
                #endregion end of Header Tabs

                #region get bank & address count per participant
                object participantBankAddressCount = Models.ParticipantModel.GetParticipantInfo(dealID, null);
                ViewData["participantBankAddressCount"] = participantBankAddressCount;
                #endregion end of get bank & address count per participant

                #region prepopulate 1 property
                /// Call WCF to check if we have existing properties per deal
                object propertyPerDeal = Models.PropertyModel.GetProperty(dealID, null, null, null);
                ViewData["Properties"] = propertyPerDeal;
                #endregion prepopulate 1 property

                #region Mortgage
                /// Call WCF to get all mortgage details
                object getMortgage = Models.MortgageModel.GetMortgage(dealID, null);
                ViewData["Mortgages"] = getMortgage;
                #endregion

                #region Total Payments
                ViewBag.TotalPayments = Models.PaymentModel.GetSumPayments(dealID);
                #endregion Total Payments

                #region Completion Date
                /// call wcf to get get completion date
                try
                {
                    object[] completionDate = REPS.UI.Models.DealModel.GetCompletedDate(dealID);
                    Dictionary<string, object> registrationDate = (Dictionary<string, object>)completionDate.FirstOrDefault();

                    if (completionDate != null)
                    {
                        ViewBag.CompletionDate = registrationDate["ExportCompletionDate"];
                    }
                    else
                    {
                        ViewBag.CompletionDate = "-";
                    }
                }
                catch
                {
                    ViewBag.CompletionDate = "-";
                }
                #endregion

                #region pre populate total sum of mortgage value 
                /// call wcf to purchase price morgage            
                object[] getMortgageSumValue = Models.DealModel.GetMortgageSumValuePerDeal(dealID);
                if (getMortgageSumValue != null)
                {
                    Dictionary<string, object> res = (Dictionary<string, object>)getMortgageSumValue.FirstOrDefault();
                    ViewBag.PriceMortgageSumValue = res["Price"];
                }
                else
                {
                    ViewBag.PriceMortgageSumValue = "0";
                }
                #endregion pre populate total sum of mortgage value 

                #region Get Workflow mandatory Actions list
                object MandatoryActionsList = Models.DealModel.GetMandatoryActionsList(Convert.ToInt32(dealWorkflowID));
                ViewData["MandatoryActionsCount"] = (MandatoryActionsList as IEnumerable<dynamic>).Count();
                #endregion end of Get Workflow mandatory Actions list

                #region Get Workflow completed Actions
                object CompletedActionsList = Models.DealModel.GetCompletedActionsByWorkflowID(dealID, Convert.ToInt32(dealWorkflowID));
                ViewData["CompletedActionsCount"] = (CompletedActionsList as IEnumerable<dynamic>).Count();
                #endregion end of  Get Workflow completed Actions

                #region last action performed by whom
                object[] ActionBy = Models.DealModel.GetDealLastActionBy(dealID);
                Dictionary<string, object> resAction = (Dictionary<string, object>)ActionBy.FirstOrDefault();
                ViewData["LastActionBy"] = resAction["GivenName"];
                #endregion end of last action performed by whom


                #region Call WCF to get workflow tasks depending on workflow ID
                object[] dealTypes = Models.DealModel.GetDealWorkflowStep(dealID, dealWorkflowID, null, entityID, null, null);
                if (dealTypes != null)
                {
                    Dictionary<string, object> res = (Dictionary<string, object>)dealTypes.FirstOrDefault();
                    ViewData["LastWorkflowTaskName"] = res["TaskName"];
                }
                else
                {
                    ViewData["LastWorkflowTaskName"] = "";
                }

                #endregion end of Call WCF to get workflow tasks depending on workflow ID

                if (partial)
                {
                    return View("PartialDealSummary");
                }

                return View("ViewDeal");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// display detal of deals to pop up box
        /// </summary>
        /// <returns></returns>
        public ActionResult PopupCreateDeal()
        {
            try
            {

                #region get workflow tasks for dealProcess dropdown
                ///Deal Autogenerated GUID
                ViewData["UniqueReference"] = Common.CUuid.GeneratedealId(); ;

                /// Call WCF to get all Deal Processes
                //object dealProcesses = Models.WorkflowModel.GetWorkflowTaskByWorkflowID((int)Global.Enums.Wokflow.Process);
                object dealProcesses = Models.WorkflowModel.GetWorkflowsList((int)Global.Enums.Wokflow.Process);
                ViewData["DealProcesses"] = dealProcesses;
                #endregion end of get workflow tasks for dealProcess dropdown

                #region Get deal types
                /// Call WCF to get all Deal types
                object deal = Models.DealModel.GetDealTypes(null, null, null);
                ViewData["DealTypes"] = deal;
                #endregion end Get deal types

                return View("PopupCreateDeal");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        public ActionResult PopupCompletionDate()
        {
            try
            {
                int dealID = 0;
                string UniqueReference = string.Empty;
                // Check if Ajax Request and determine layout to be used
                if (Request.IsAjaxRequest())
                {
                    #region Get DealID from URL Parameter (Unique Reference)

                    if (Request.UrlReferrer.Query == null)
                    {
                        return Content(Enums.UniqueReference.Invalidreference.ToString());
                    }
                    UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
                    if (UniqueReference != Enums.UniqueReference.Invalid.ToString())
                    {
                        object dealIDObject = Models.DealModel.GetDealIDByDealUniqueRef(UniqueReference); // We get the DealID from the UR
                        if (dealIDObject == null)
                        {
                            return Content(Enums.UniqueReference.Invalidreference.ToString());
                        }
                        dealID = Convert.ToInt32(dealIDObject);
                    }
                    else
                    {
                        return Content(Enums.UniqueReference.Invalidreference.ToString());
                    }

                    #endregion Get DealID from URL Parameter (Unique Reference)
                }
                else
                {
                    #region Get DealID from URL Parameter (Unique Reference)

                    if (Request["URef"] == null)
                    {
                        UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
                    }
                    else
                    {
                        UniqueReference = Common.CUniqueReference.GetUniqueReferenceNonAjaxRequest(Request["URef"]);
                    }

                    if (UniqueReference != Enums.UniqueReference.Invalid.ToString())
                    {
                        object dealIDObject = Models.DealModel.GetDealIDByDealUniqueRef(UniqueReference); // We get the DealID from the UR
                        if (dealIDObject == null)
                        {
                            return RedirectToAction("Index", "Dashboard");
                        }
                        dealID = Convert.ToInt32(dealIDObject);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }

                    #endregion Get DealID from URL Parameter (Unique Reference)
                }
                ViewBag.DealID = dealID;

                #region Completion Date
                /// call wcf to get get completion date
                try
                {
                    object[] completionDate = REPS.UI.Models.DealModel.GetCompletedDate(dealID);
                    Dictionary<string, object> registrationDate = (Dictionary<string, object>)completionDate.FirstOrDefault();

                    if (completionDate != null)
                    {
                        ViewBag.CompletionDate = registrationDate["RegistrationDate"];
                    }
                    else
                    {
                        ViewBag.CompletionDate = "-";
                    }
                }
                catch
                {
                    ViewBag.CompletionDate = "-";
                }
                #endregion

                return View("PopupCompletionDate");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// Edit Completion Date
        /// </summary>
        /// <returns></returns>
        public ActionResult EditCompletionDate()
        {
            try
            {
                int dealID = 0;
                string UniqueReference = string.Empty;
                // Check if Ajax Request
                if (Request.IsAjaxRequest())
                {
                    #region Get DealID from URL Parameter (Unique Reference)

                    if (Request.UrlReferrer.Query == null)
                    {
                        return Content(Enums.UniqueReference.Invalidreference.ToString());
                    }
                    UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
                    if (UniqueReference != Enums.UniqueReference.Invalid.ToString())
                    {
                        object dealIDObject = Models.DealModel.GetDealIDByDealUniqueRef(UniqueReference); // We get the DealID from the UR
                        if (dealIDObject == null)
                        {
                            return Content(Enums.UniqueReference.Invalidreference.ToString());
                        }
                        dealID = Convert.ToInt32(dealIDObject);
                    }
                    else
                    {
                        return Content(Enums.UniqueReference.Invalidreference.ToString());
                    }

                    #endregion Get DealID from URL Parameter (Unique Reference)
                }
                else
                {
                    #region Get DealID from URL Parameter (Unique Reference)

                    if (Request["URef"] == null)
                    {
                        UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
                    }
                    else
                    {
                        UniqueReference = Common.CUniqueReference.GetUniqueReferenceNonAjaxRequest(Request["URef"]);
                    }

                    if (UniqueReference != Enums.UniqueReference.Invalid.ToString())
                    {
                        object dealIDObject = Models.DealModel.GetDealIDByDealUniqueRef(UniqueReference); // We get the DealID from the UR
                        if (dealIDObject == null)
                        {
                            return RedirectToAction("Index", "Dashboard");
                        }
                        dealID = Convert.ToInt32(dealIDObject);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }

                    #endregion Get DealID from URL Parameter (Unique Reference)
                }

                if (Request["completionDate"] != "")
                {
                    DateTime completionDate = new DateTime(DateTime.ParseExact(Request["completionDate"] + " 00:00:00", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture).Year, DateTime.ParseExact(Request["completionDate"] + " 00:00:00", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture).Month, DateTime.ParseExact(Request["completionDate"] + " 00:00:00", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture).Day, 00, 00, 00);
                    #region logic
                    /// Call WCF to get all Deal types
                    string completionDealType = Models.DealModel.EditCompletionDealDate(dealID, completionDate);
                    #endregion end logic
                }

                try
                {
                    object[] completionDate = REPS.UI.Models.DealModel.GetCompletedDate(dealID);
                    Dictionary<string, object> registrationDate = (Dictionary<string, object>)completionDate.FirstOrDefault();

                    if (completionDate != null)
                    {
                        ViewBag.CompletionDate = registrationDate["ExportCompletionDate"];
                    }
                    else
                    {
                        ViewBag.CompletionDate = "-";
                    }
                }
                catch
                {
                    ViewBag.CompletionDate = "-";
                }

                return PartialView("PartialCompletionDateList");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// save deal to db
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveDeal()
        {
            try
            {
                ///insert only if REPS_AspNetUsersId is present 
                if (HttpContext.Request.Cookies["REPS_AspNetUsersId"] != null)
                {
                    #region form Values
                    Deal deal = new Deal();
                    deal.UniqueReference = Request["UniqueReference"];
                    deal.DealTypeID = Convert.ToInt32(Request["DealTypeID"]);
                    deal.WorkflowTaskID = (int)Enums.WokflowTask.Participant;
                    deal.ClientReference = Request["clientRef"];
                    deal.DealProcessTaskID = Convert.ToInt32(Request["DealProcessTaskID"]);
                    string aspNetUsersId = HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value;
                    #endregion End form values

                    #region ServerCAll to save deal
                    object savedResult = Models.DealModel.SaveDeal(deal, aspNetUsersId);
                    //if (Convert.ToInt32(savedResult) > 0)
                    //{
                    //    TempData["ToasterMsgDeal"] = Notifications.GetToastrMessage(Enums.MessageType.success.ToString(), "Deal has been successfully created','Successful");
                    //}

                    return Json(new { parameter = "{'controller':'Deal', 'methodName':'ViewDeal',  'UREF':'" + deal.UniqueReference + "', 'msg': 'Deal has been successfully created', 'url': '" + Url.Action("ViewDeal", new { UREF = deal.UniqueReference }) + "' }" }, JsonRequestBehavior.AllowGet);
                    //return Json(new { url = Url.Action("ViewDeal", new { UREF = deal.UniqueReference }) });
                    //return RedirectToAction("ViewDeal", new { UREF = deal.UniqueReference });
                    #endregion End ServerCAll to dave deal
                }

                return View("error");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        #region display deal details to Separate View form on ajax EDIT call
        /// <summary>
        /// get edit details | ajax
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditDeal(string URef)
        {
            try
            {
                object dealIDObject = Models.DealModel.GetDealIDByDealUniqueRef(URef); // We get the DealID from the UR
                int dealID = Convert.ToInt32(dealIDObject);
                ViewBag.DealID = dealID;

                #region Variables
                /// Initiate Validator
                ViewData["AspNetUsersId"] = HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value;
                string entityGUID = HttpContext.Request.Cookies["REPS_EntityGUID"].Value;
                //call wcf to get id by GUID
                int entityID = Convert.ToInt32(Models.EntityModel.GetEntityIDByEntityGUID(entityGUID));
                //end of call wcf to get id by GUID

                //deal class object
                Deal deal = new Deal();
                deal.DealID = dealID;
                //end of deal class object
                #endregion end vaiables

                #region deal edit: get deal values from form
                //get selected values from form for edit process 
                //object dealEdit = Models.DealModel.GetDeals(deal, null, null, entityGUID, null, null, null);
                object dealEdit = Models.DealModel.GetDeal(deal, null, entityID, null, null);
                ViewData["dealEdit"] = dealEdit;
                //end of get selected values from form for edit process
                #endregion end of deal edit : get deal values from form

                #region get workflow tasks for dealProcess dropdown
                /// Call WCF to get all Deal Processes
                //object dealProcesses = Models.WorkflowModel.GetWorkflowTaskByWorkflowID((int)Global.Enums.Wokflow.Process);
                object dealProcesses = Models.WorkflowModel.GetWorkflowsList((int)Global.Enums.Wokflow.Process);
                ViewData["DealProcesses"] = dealProcesses;
                #endregion end of get workflow tasks for dealProcess dropdown

                #region Get deal types
                /// Call WCF to get all Deal types
                object dealType = Models.DealModel.GetDealTypes(null, null, null);
                ViewData["DealType"] = dealType;
                #endregion end Get deal types


                return View("PopupEditDeal");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }
        #endregion end of display deal details to Separate View form on ajax EDIT call

        #region call wcf to udpate deal without refresh
        /// <summary>
        /// update a deal with its form values
        /// </summary>
        /// <param name="dealID"></param>
        /// <param name="formEditDealValues"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateDeal()
        {
            try
            {
                #region form Values
                string entityGUID = HttpContext.Request.Cookies["REPS_EntityGUID"].Value;
                string taskGUID = HttpContext.Request.Cookies["REPS_TaskGUID"].Value;

                object dealIDObject = Models.DealModel.GetDealIDByDealUniqueRef(Request["UniqueReference"]); // We get the DealID from the UR
                int dealID = Convert.ToInt32(dealIDObject);

                //get userid
                string aspNetUsersId = HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value;
                object userID = Models.UserModel.GetUserIDByAspNetID(aspNetUsersId);
                //end of get user id

                //call wcf to get id by GUID
                int entityID = Convert.ToInt32(Models.EntityModel.GetEntityIDByEntityGUID(entityGUID));
                //end of call wcf to get id by GUID

                //deal class object
                Deal deal = new Deal();
                deal.DealID = dealID;
                deal.UniqueReference = Request["UniqueReference"];
                deal.DealTypeID = Convert.ToInt32(Request["DealTypeID"]);
                deal.ClientReference = Request["clientRef"];
               // deal.UserID = Convert.ToInt32(userID.ToString());
                deal.DateModified = DateTime.Now;
                deal.DealProcessTaskID = Convert.ToInt32(Request["DealProcessTaskID"]);
                //end of deal class object
                #endregion End form values

                #region ServerCAll to update deal
                string savedResult = Models.DealModel.UpdateDeal(deal);

                if (Convert.ToInt32(savedResult) > 0)
                {                  
                    TempData["ToasterMsgDeal"] = Notifications.GetToastrMessage(Enums.MessageType.success.ToString(), "Deal has been successfully updated");
                    return RedirectToAction("ViewDeal", "Deal", new { partial = true });
                }
                else
                {
                    return RedirectToAction("ViewDeal", "Deal", new { partial = true });
                }
                #endregion End ServerCAll to update deal
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }
        #endregion end of wcf call to udpate without refresh

        ///// <summary>
        ///// update tabs summarry
        ///// </summary>
        ///// <returns></returns>
        public ActionResult GeneralTabsSession()
        {
            try
            {
                int dealID = 0;
                string UniqueReference = string.Empty;
                // Check if Ajax Request and determine layout to be used
                if (Request.IsAjaxRequest())
                {
                    if (Request.UrlReferrer.Query == null)
                    {
                        return Content(Enums.UniqueReference.Invalidreference.ToString());
                    }
                    #region Get DealID from URL Parameter (Unique Reference)
                    UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
                    if (UniqueReference != Enums.UniqueReference.Invalid.ToString())
                    {
                        object dealIDObject = Models.DealModel.GetDealIDByDealUniqueRef(UniqueReference); // We get the DealID from the UR
                        if (dealIDObject == null)
                        {
                            return Content(Enums.UniqueReference.Invalidreference.ToString());
                        }
                        dealID = Convert.ToInt32(dealIDObject);
                    }
                    else
                    {
                        return Content(Enums.UniqueReference.Invalidreference.ToString());
                    }

                    #endregion Get DealID from URL Parameter (Unique Reference)

                    ViewBag.PageLayoutPath = null;
                }
                else
                {
                    #region Get DealID from URL Parameter (Unique Reference)

                    if (Request["URef"] == null)
                    {
                        UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
                    }
                    else
                    {
                        UniqueReference = Common.CUniqueReference.GetUniqueReferenceNonAjaxRequest(Request["URef"]);
                    }

                    if (UniqueReference != Enums.UniqueReference.Invalid.ToString())
                    {
                        object dealIDObject = Models.DealModel.GetDealIDByDealUniqueRef(UniqueReference); // We get the DealID from the UR
                        if (dealIDObject == null)
                        {
                            return RedirectToAction("Index", "Dashboard");
                        }
                        dealID = Convert.ToInt32(dealIDObject);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }

                    #endregion Get DealID from URL Parameter (Unique Reference)

                    ViewBag.PageLayoutPath = Global.Constants.MainLayoutPath;
                }
                ViewBag.DealID = dealID;

                #region Set current workflow
                /// Call WCF to get Workflow Task Name
                object workflowName = Models.WorkflowModel.GetWorkFlowTaskNameByDealID(dealID);
                HttpContext.Response.Cookies.Add(new HttpCookie("CurrentDealWorkflow", workflowName.ToString()));
                ViewData["CurrentDealWorkflow"] = workflowName.ToString();

                ViewData["SelectedTab"] = Enums.PageNames.Deal.ToString();
                return PartialView("PartialSubSection");
                #endregion End Set current workflow
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// get the last action result
        /// </summary>
        /// <returns></returns>
        public ActionResult LastActionBy(int dealID)
        {
            try
            {
                #region last action performed by whom
                object[] ActionBy = Models.DealModel.GetDealLastActionBy(dealID);
                Dictionary<string, object> resAction = (Dictionary<string, object>)ActionBy.FirstOrDefault();
                ViewData["LastActionBy"] = resAction["GivenName"];
                #endregion end of last action performed by whom
                return Json(ViewData["LastActionBy"].ToString(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }
    }
}