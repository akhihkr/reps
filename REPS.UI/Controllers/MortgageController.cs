using Global;
using REPS.DATA.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace REPS.UI.Controllers
{
    public class MortgageController : Controller
    {
        // GET: Mortgage
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PopupAddMortgage()
        {
            try
            {
                #region Get DealID from URL Parameter (Unique Reference)

                if (Request.UrlReferrer.Query == null)
                {
                    return Content(Enums.UniqueReference.Invalidreference.ToString());
                }
                int dealID = 0;
                string UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
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


                #region logic call WCF 
                /// Call WCF to get all mortgage lenders
                object mortgageLender = Models.MortgageModel.MortgageLender();
                ViewData["MortgageLender"] = mortgageLender;

                /// Call WCF to get all mortgage types
                object mortgageType = Models.MortgageModel.MortgageType();
                ViewData["MortgageType"] = mortgageType;

                /// Call WCF to get all organisation types
                object interestType = Models.MortgageModel.InterestType();
                ViewData["InterestType"] = interestType;
                #endregion end logic call WCF 

                #region get bank & address count per participant
                object participantBankAddressCount = Models.ParticipantModel.GetParticipantBankAccountCount(dealID, null);
                ViewData["participantBankAddressCount"] = participantBankAddressCount;
                #endregion end of get bank & address count per participant

                return View("PopupAddMortgage");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        public ActionResult PopupEditMortgage(string mortgageGUID)
        {
            try
            {
                #region Get DealID from URL Parameter (Unique Reference)

                if (Request.UrlReferrer.Query == null)
                {
                    return Content(Enums.UniqueReference.Invalidreference.ToString());
                }
                int dealID = 0;
                string UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
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


                #region wcf call to get details
                /// Call WCF to get all mortgage lenders
                object mortgageLender = Models.MortgageModel.MortgageLender();
                ViewData["MortgageLender"] = mortgageLender;

                /// Call WCF to get all mortgage types
                object mortgageType = Models.MortgageModel.MortgageType();
                ViewData["MortgageType"] = mortgageType;

                /// Call WCF to get all organisation types
                object interestType = Models.MortgageModel.InterestType();
                ViewData["InterestType"] = interestType;

                /// Get Mortgage
                int mortgageEditID = (int)Models.MortgageModel.GetMortgageIDByMortgageGUID(mortgageGUID);
                object getMortgage = Models.MortgageModel.GetMortgage(dealID, mortgageEditID);
                ViewData["Mortgages"] = getMortgage;
                #endregion end of wcf call to get details

                #region get bank & address count per participant
                object participantBankAddressCount = Models.ParticipantModel.GetParticipantBankAccountCount(dealID, null);
                ViewData["participantBankAddressCount"] = participantBankAddressCount;
                #endregion end of get bank & address count per participant

                return View("PopupEditMortgage");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }

        /// <summary>
        /// save mortgage/transaction ID / financial instrument
        /// </summary>
        /// <returns></returns
        #region save mortgage/transaction ID / financial instrument
        [HttpPost]
        public ActionResult SaveNewMortgage()
        {
            try
            {
                if (HttpContext.Request.Cookies["REPS_AspNetUsersId"] != null)
                {
                    #region Get DealID from URL Parameter (Unique Reference)

                    if (Request.UrlReferrer.Query == null)
                    {
                        return Content(Enums.UniqueReference.Invalidreference.ToString());
                    }
                    int dealID = 0;
                    string UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
                    if (UniqueReference != Enums.UniqueReference.Invalid.ToString())
                    {
                        object dealIDObject = Models.DealModel.GetDealIDByDealUniqueRef(UniqueReference); // We get the DealID from the URL
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

                    #region form Values financial Instrument
                    FinancialInstrument financialInstrument = new FinancialInstrument();
                    financialInstrument.ParticipantID = Convert.ToInt32(Request["participantID"]);
                    financialInstrument.Value = Convert.ToDecimal(Request["loanAmount"]);
                    financialInstrument.Deposit = Convert.ToDecimal(Request["depositAmount"]);
                    financialInstrument.Term = Request["termOfLoan"];
                    financialInstrument.InterestRate = Convert.ToDecimal(Request["interestRate"]);
                    int LenderID = int.Parse(Request["mortgageLender"]);
                    int InstrumentTypeID = int.Parse(Request["mortgageType"]);
                    int InterestTypeID = int.Parse(Request["interestType"]);
                    int TransactionID = 0;
                    string aspNetID = HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value.ToString();
                    ///variables result
                    var transactionResult = "";
                    #endregion End form values financial Instrument

                    #region Servercall financialtransaction add             
                    /// Call WCF for financialtransaction add  
                    object financialtransactionInsert = Models.MortgageModel.InsertFinancialInstrument(financialInstrument, LenderID, InstrumentTypeID, InterestTypeID, dealID, aspNetID);
                    #endregion End Servercall financialtransaction add 

                    Guid guidOutput;
                    bool isGuid = Guid.TryParse(financialtransactionInsert.ToString(), out guidOutput);

                    if(!isGuid)
                    {
                        TransactionID = Convert.ToInt32(Regex.Replace(financialtransactionInsert.ToString(), "[^0-9]", ""));
                        ///save property if TransactionID > 0
                        if (TransactionID > 0)
                        {
                            #region form Values financial Instrument
                            FinancialTransaction transaction = new FinancialTransaction();
                            transaction.DealID = dealID;
                            transaction.InstrumentID = TransactionID; //TransactionID is the last inserted id of financial instrument table
                            #endregion End form values financial Instrument
                            /// Call WCF to save transaction 
                            #region Call WCF to save transaction 
                            object saveTransactionID = Models.MortgageModel.SaveTansaction(transaction, dealID, TransactionID);
                            transactionResult = saveTransactionID.ToString();
                            #endregion Call WCF to save transaction 
                        }
                        /// Call WCF to get all mortgage details
                    }

                    object getMortgage = Models.MortgageModel.GetMortgage(dealID, null);
                    ViewData["Mortgages"] = getMortgage;

                    var lastActionByResult = new DealController().LastActionBy(dealID);
                    if (((JsonResult)lastActionByResult).Data != null)
                    {
                        ViewData["LastActionBy"] = ((JsonResult)lastActionByResult).Data;
                    }


                    if (isGuid)
                    {
                        Response.Write("<div id='my-div' data-info=" + (int)Enums.ValidationCheck.reusltOutput + "></div>");
                    }
                    return View("PartialMortgage");
                }
                return View("error");
            }
            catch (Exception ex)
            {
                Response.Write("<div id='my-div' data-info=" + (int)Enums.ValidationCheck.systemCrash + "></div>");
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                if (((System.Web.Mvc.ContentResult)new ErrorHandlingController().ErrorHandlingMessage(ex.Message, Request.IsAjaxRequest())).Content == "tokenerror") //if result is tokenerror (another user logged in on the same account), redirect the user to login page
                    return RedirectToAction("Index", "Login", new { tokenerror = "true" }); // Redirect to login page using controller/action
                else if (((System.Web.Mvc.ContentResult)new ErrorHandlingController().ErrorHandlingMessage(ex.Message, Request.IsAjaxRequest())).Content == "true")
                    ViewBag.PageLayoutPath = Constants.MainLayoutPath;

                TempData["ErrorMessage"] = ex;

                return PartialView(ConfigurationManager.AppSettings["ErrorMsg"].ToString());
            }
        }
        #endregion end of save mortgage/transaction ID / financial instrument


        /// <summary>
        /// Update Mortgage Ajax
        /// </summary>
        /// <returns></returns>
        public ActionResult EditMortgage()
        {
            try
            {
                if (Request.UrlReferrer.Query == null)
                {
                    return Content(Enums.UniqueReference.Invalidreference.ToString());
                }
                Guid participantGUIDConvert = Guid.Empty;
                #region Get DealID from URL Parameter (Unique Reference)
                int dealID = 0;
                string UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
                if (UniqueReference != Enums.UniqueReference.Invalid.ToString())
                {
                    object dealIDObject = Models.DealModel.GetDealIDByDealUniqueRef(UniqueReference); // We get the DealID from the URL
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

                #region set variables
                int instrumentID = (int)Models.MortgageModel.GetMortgageIDByMortgageGUID(Request["instrumentGUID"].ToString());
                #endregion end of set variables 
                #region form Values financial Instrument
                FinancialInstrument financialInstrument = new FinancialInstrument();
                participantGUIDConvert = new Guid(Request["participantGUID"]);
                object participantID = Models.ParticipantModel.GetParticipantIDByParticipantGUID(participantGUIDConvert);
                financialInstrument.ParticipantID = Convert.ToInt32(participantID);
                financialInstrument.InstrumentID = instrumentID;
                financialInstrument.Value = Convert.ToDecimal(Request["loanAmount"]);
                financialInstrument.Deposit = Convert.ToDecimal(Request["depositAmount"]);
                financialInstrument.Term = Request["termOfLoan"];
                financialInstrument.InterestRate = Convert.ToDecimal(Request["interestRate"]);
                int LenderID = int.Parse(Request["mortgageLender"]);
                int InstrumentTypeID = int.Parse(Request["mortgageType"]);
                int InterestTypeID = int.Parse(Request["interestType"]);
                #endregion End form values financial Instrument

                var lastActionByResult = new DealController().LastActionBy(dealID);
                if (((JsonResult)lastActionByResult).Data != null)
                {
                    ViewData["LastActionBy"] = ((JsonResult)lastActionByResult).Data;
                }

                // Get Mortgage
                object getMortgage = Models.MortgageModel.GetMortgage(dealID, null);
                ViewData["Mortgages"] = getMortgage;

                // Call WCF for financialtransaction add
                object result = Models.MortgageModel.UpdateFinancialInstrument(financialInstrument, LenderID, InstrumentTypeID, InterestTypeID, dealID);
                Guid guidOutput;
                bool isGuid = Guid.TryParse(result.ToString(), out guidOutput);
               
                if (isGuid)
                {
                    Response.Write("<div id='my-div' data-info=" + (int)Enums.ValidationCheck.reusltOutput + "></div>");
                }

                return View("PartialMortgage");
            }
            catch (Exception ex)
            {
                Response.Write("<div id='my-div' data-info=" + (int)Enums.ValidationCheck.systemCrash + "></div>");
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                if (((System.Web.Mvc.ContentResult)new ErrorHandlingController().ErrorHandlingMessage(ex.Message, Request.IsAjaxRequest())).Content == "tokenerror") //if result is tokenerror (another user logged in on the same account), redirect the user to login page
                    return RedirectToAction("Index", "Login", new { tokenerror = "true" }); // Redirect to login page using controller/action
                else if (((System.Web.Mvc.ContentResult)new ErrorHandlingController().ErrorHandlingMessage(ex.Message, Request.IsAjaxRequest())).Content == "true")
                    ViewBag.PageLayoutPath = Constants.MainLayoutPath;

                TempData["ErrorMessage"] = ex;

                return PartialView(ConfigurationManager.AppSettings["ErrorMsg"].ToString());
            }
        }

        /// <summary>
        /// remove mortgage and set del to 1
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RemoveMortgage(string mortgageGUID)
        {
            try
            {
                #region Get DealID from URL Parameter (Unique Reference)

                if (Request.UrlReferrer.Query == null)
                {
                    return Content(Enums.UniqueReference.Invalidreference.ToString());
                }
                int dealID = 0;
                string UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
                if (UniqueReference != Enums.UniqueReference.Invalid.ToString())
                {
                    object dealIDObject = Models.DealModel.GetDealIDByDealUniqueRef(UniqueReference); // We get the DealID from the URL
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

                #region Variables
                int mortgageID = (int)Models.MortgageModel.GetMortgageIDByMortgageGUID(Request["mortgageGUID"].ToString());
                ViewData["MortgageLender"] = "";
                #endregion end vaiables
                /// Call WCF to remove selected mortgage
                #region logic Call WCF to remove selected mortgage
                object removeMortgage = Models.MortgageModel.RemoveMortgage(mortgageID, dealID);
                ViewData["MortgageLender"] = removeMortgage.ToString();
                #endregion end logic  Call WCF to remove selected mortgage

                /// Call WCF to get all mortgage details
                object getMortgage = Models.MortgageModel.GetMortgage(dealID, null);
                ViewData["Mortgages"] = getMortgage;

                var lastActionByResult = new DealController().LastActionBy(dealID);
                if (((JsonResult)lastActionByResult).Data != null)
                {
                    ViewData["LastActionBy"] = ((JsonResult)lastActionByResult).Data;
                }

                return View("PartialMortgage");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }


        /// <summary>
        /// We update the value of the mortgage in the view deal
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public object UpdateMortgageSum()
        {
            try
            {
                #region Get DealID from URL Parameter (Unique Reference)
                int dealID = 0;
                string UniqueReference = Common.CUniqueReference.GetUniqueReferenceAjaxRequest(Request.UrlReferrer.Query);
                if (UniqueReference != Enums.UniqueReference.Invalid.ToString())
                {
                    object dealIDObject = Models.DealModel.GetDealIDByDealUniqueRef(UniqueReference); // We get the DealID from the URL
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

                return PartialView("~/Views/Deal/PartialMortgagePriceSum.cshtml");
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                return RedirectToAction("ErrorHandling", "ErrorHandling", new { ex = ex.Message, ajax = Request.IsAjaxRequest() });
            }
        }
    }
}