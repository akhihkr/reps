using Global;
using REPS.DATA.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using System.Web.Mvc;
using System.Configuration;


namespace REPS.UI.Controllers
{
    [TokenAuthorizeAttribute]
    public class ParticipantController : Controller
    {
        /// <summary>
        /// call wcf to get participant details
        /// </summary>
        /// <param name="dealID"></param>
        /// <returns></returns>
        public ActionResult PopupAddParticipant(string dealID)
        {
            try
            {
                /// Call WCF to get all participant types
                object getParticipantType = Models.ParticipantModel.GetParticipantType(null, null, null);
                ViewData["PartType"] = getParticipantType;

                /// call WCF to get all participantRole
                object getParticipantRole = Models.ParticipantModel.GetParticipantRole(null, null, null);
                ViewData["PartRole"] = getParticipantRole;

                /// Call WCF to get all organisation types 
                object getOrgTypes = Models.ParticipantModel.GetOrganisationType(null, null, null);
                ViewData["OrganizationTypes"] = getOrgTypes;

                return View("PopupAddParticipant");
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
        /// save participant individual pop up
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveParticipant()
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

                //get entity from cookies
                string entityGUID = HttpContext.Request.Cookies["REPS_EntityGUID"].Value;
                int entityID = Convert.ToInt32(Models.EntityModel.GetEntityIDByEntityGUID(entityGUID));
                //end of get entity from cookies

                #region form Values
                /// person form input
                Person person = new Person();
                person.GivenName = Request["participantName"];
                person.FamilyName = Request["participantFamilyName"];
                decimal telNumber = 0;
                Decimal.TryParse(Request["participantContactNum"], out telNumber);
                person.Telephone = telNumber;
                person.Email = Request["participantEmail"];
                person.Verified = false;
                person.Deleted = false;
                person.IdentityNumber = Request.Form.AllKeys.Contains("identityNum") ? Request.Form["identityNum"] : null;
                person.BirthDate = DateTime.ParseExact(Request.Form["birthDate"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                person.TaxID = Request.Form.AllKeys.Contains("taxNum") ? Request.Form["taxNum"] : null;

                if (Request.Form["faxNum"] == "")
                {
                    person.FaxNumber = null;
                }
                else
                {
                    person.FaxNumber = Request.Form.AllKeys.Contains("faxNum") ? Convert.ToDecimal(Request.Form["faxNum"]) : 0;
                }
                if (Request.Form["mobileNum"] == "")
                {
                    person.MobileNumber = null;
                }
                else
                {
                    person.MobileNumber = Request.Form.AllKeys.Contains("mobileNum") ? Convert.ToDecimal(Request.Form["mobileNum"]) : 0;
                }
                int personID = Request.Form.AllKeys.Contains("PersonID") ? Convert.ToInt32(Request["PersonID"]) : 0;

                ///result varianle if saved
                var participantResult = "";
                var personResult = "";
                #endregion End form values

                string aspNetUsersId = HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value;
                object userID = Models.UserModel.GetUserIDByAspNetID(aspNetUsersId);

                if (Request.Form.AllKeys.Contains("ParticipantGUID"))// Participant already exists - Update Participant
                {
                    Guid participantGUID = Guid.Empty;
                    participantGUID = new Guid(Request["ParticipantGUID"].Trim());
                    object participantID = Models.ParticipantModel.GetParticipantIDByParticipantGUID(participantGUID);

                    #region variables
                    ///participant formkeysinput
                    Participant participant = new Participant();
                    participant.ParticipantRoleID = Convert.ToInt16(Request["ptRoleexist"]);
                    participant.DealID = dealID;
                    participant.ParticipantTypeID = 10;
                    participant.PersonID = Convert.ToInt32(personID);
                    participant.ParticipantID = Convert.ToInt32(participantID);
                    participant.EntityID = entityID;
                    #endregion end variables

                    // Update Person
                    person.PersonID = Request.Form.AllKeys.Contains("PersonID") ? Convert.ToInt32(Request["PersonID"]) : 0;
                    object UpdatePerson = Models.ParticipantModel.UpdatePerson(participant, person);

                    // Update Participant
                    if (Convert.ToInt32(UpdatePerson) == 1)
                    {
                        object UpdateParticipant = Models.ParticipantModel.UpdateParticipant(userID.ToString(), participant);
                        TempData["ToasterParticipantMsg"] = Notifications.GetToastrMessage(Enums.MessageType.success.ToString(), "Participant has been successfully updated");
                    }
                    else
                    {
                        TempData["ToasterParticipantMsg"] = Notifications.GetToastrMessage(Enums.MessageType.warning.ToString(), "Participant already existed");
                    }

                    #region get bank & address count per participant
                    object participantBankAddressCount = Models.ParticipantModel.GetParticipantInfo(dealID, null);
                    ViewData["participantBankAddressCount"] = participantBankAddressCount;
                    #endregion end of get bank & address count per participant

                    var lastActionByResult = new DealController().LastActionBy(dealID);
                    if (((JsonResult)lastActionByResult).Data != null)
                    {
                        ViewData["LastActionBy"] = ((JsonResult)lastActionByResult).Data;
                    }

                    return PartialView("PartialParticipant");
                }
                else  // Create New Participant
                {
                    //verify if participant exist
                    #region variables
                    ///participant formkeysinput
                    Participant participant = new Participant();
                    participant.ParticipantRoleID = Convert.ToInt16(Request["ptRoleexist"]);
                    participant.DealID = dealID;
                    #endregion end variables                                   
                    //end of verify if participant exist

                    ///save  person
                    #region ServerCAll Person Add
                    object addPerson = Models.ParticipantModel.SavePerson(participant, person);

                    //check if rows does exist
                    if (Convert.ToInt32(addPerson) != 0)
                    {
                        personResult = addPerson.ToString();
                        int.TryParse(personResult, out personID);
                        ///save participant if personid > 0
                        if (personID > 0)
                        {
                            #region variables
                            ///participant formkeysinput
                            participant.ParticipantTypeID = 10;
                            participant.PersonID = personID;
                            participant.EntityID = entityID;
                            #endregion end variables
                            #region ServerCAll particiapnt Add
                            object addParticipant = Models.ParticipantModel.SaveParticipant(userID.ToString(), participant);
                            participantResult = addParticipant.ToString();
                            #endregion ServerCAll particiapnt Add

                            if (Convert.ToInt32(participantResult) > 0)
                            {
                                TempData["ToasterParticipantMsg"] = Notifications.GetToastrMessage(Enums.MessageType.success.ToString(), "Participant has been successfully added");
                            }
                        }
                    }
                    else
                    {
                        //dispay warning "Rows existed"
                        TempData["ToasterParticipantMsg"] = Notifications.GetToastrMessage(Enums.MessageType.warning.ToString(), "Participant already existed");
                    }
                    #endregion End ServerCALL Person Add

                    ViewData["DealID"] = dealID;
                    #region get bank & address count per participant
                    object participantBankAddressCount = Models.ParticipantModel.GetParticipantInfo(dealID, null);
                    ViewData["participantBankAddressCount"] = participantBankAddressCount;
                    #endregion end of get bank & address count per participant

                    var lastActionByResult = new DealController().LastActionBy(dealID);
                    if (((JsonResult)lastActionByResult).Data != null)
                    {
                        ViewData["LastActionBy"] = ((JsonResult)lastActionByResult).Data;
                    }

                    return View("PartialParticipant");
                }
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
        /// save company pop up form
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveOrganisation()
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

                //get entity from cookies
                string entityGUID = HttpContext.Request.Cookies["REPS_EntityGUID"].Value;
                int entityID = Convert.ToInt32(Models.EntityModel.GetEntityIDByEntityGUID(entityGUID));
                //end of get entity from cookies


                #region form Values
                string aspNetUsersId = HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value;
                object userID = Models.UserModel.GetUserIDByAspNetID(aspNetUsersId);
                /// organisation form values
                Organization organization = new Organization();
                organization.Name = Request["organizationName"];
                organization.OrganizationTypeID = Convert.ToInt32(Request["organisationType"]);
                organization.LegalName = Request["legalName"];
                organization.RegistrationNumber = Request["registrationNum"];
                decimal telNumber = 0;
                Decimal.TryParse(Request["tellNum"], out telNumber);
                organization.Telephone = telNumber;
                organization.Email = Request["email"];
                organization.Verified = false;
                organization.Deleted = false;
                int organisationID = Request.Form.AllKeys.Contains("OrganizationID") ? Convert.ToInt32(Request["OrganizationID"]) : 0;

                if (Request.Form["faxNumOrg"].Trim() == "")
                {
                    organization.FaxNumber = null;
                }
                else
                {
                    organization.FaxNumber = Request.Form.AllKeys.Contains("faxNumOrg") ? Convert.ToDecimal(Request.Form["faxNumOrg"]) : 0;
                }

                ///result varianle if saved
                var organisationResult = "";
                var participantResult = "";
                #endregion End form values

                #region ServerCAll Organisation Add
                #region variables
                ///participant form values
                Participant participant = new Participant();
                participant.DealID = dealID;
                participant.ParticipantRoleID = Convert.ToInt16(Request["ptRoleExistOrg"]);
                #endregion end of variables

                object addOrganisation = Models.ParticipantModel.SaveOrganisation(organization, participant);
                #endregion End ServerCALL Organisation Add

                //check if rows does exist
                if (Convert.ToInt32(addOrganisation) != 0)
                {
                    organisationResult = addOrganisation.ToString();
                    int.TryParse(organisationResult, out organisationID);

                    ///save organization if organisationID > 0
                    if (organisationID > 0)
                    {
                        #region variables
                        participant.ParticipantTypeID = 20; // 20 = organisation
                        participant.OrganizationID = organisationID;
                        participant.EntityID = entityID;
                        #endregion end of variables

                        #region ServerCAll particiapnt Add
                        object addParticipant = Models.ParticipantModel.SaveParticipant(userID.ToString(), participant);
                        participantResult = addParticipant.ToString();
                        #endregion ServerCAll particiapnt Add

                        if (Convert.ToInt32(participantResult) > 0)
                        {
                            TempData["ToasterParticipantMsgOrg"] = Notifications.GetToastrMessage(Enums.MessageType.success.ToString(), "Participant has been successfully added");
                        }
                    }
                }
                else
                {
                    //dispay warning "Rows existed"
                    TempData["ToasterParticipantMsg"] = Notifications.GetToastrMessage(Enums.MessageType.warning.ToString(), "Participant Existed");
                }

                #region get bank & address count per participant
                object participantBankAddressCount = Models.ParticipantModel.GetParticipantInfo(dealID, null);
                ViewData["participantBankAddressCount"] = participantBankAddressCount;
                #endregion end of get bank & address count per participant

                var lastActionByResult =  new DealController().LastActionBy(dealID);
                if (((JsonResult)lastActionByResult).Data != null)
                {
                    ViewData["LastActionBy"] = ((JsonResult)lastActionByResult).Data;
                }
                    return View("PartialParticipant");
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
        /// update organisation Ajax
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateOrganisation()
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

                //get userid
                string aspNetUsersId = HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value;
                object userID = Models.UserModel.GetUserIDByAspNetID(aspNetUsersId);
                //end of get user id

                #region form Values
                /// organisation form values
                Organization organization = new Organization();
                organization.Name = Request["organizationName"];
                organization.OrganizationTypeID = Convert.ToInt32(Request["organisationType"]);
                organization.LegalName = Request["legalName"];
                organization.RegistrationNumber = Request["registrationNum"];
                decimal telNumber = 0;
                Decimal.TryParse(Request["tellNum"], out telNumber);
                organization.Telephone = telNumber;
                organization.Email = Request["email"];
                organization.Verified = false;
                organization.Deleted = false;
                int organisationID = Request.Form.AllKeys.Contains("OrganizationID") ? Convert.ToInt32(Request["OrganizationID"]) : 0;
                organization.OrganizationID = organisationID;
                ///result varianle if saved
                var participantResult = "";
                organization.FaxNumber = (decimal?)Common.CString.ReturnNullOrObject<string>(Request.Form["faxNumOrg"].Trim());
                #endregion End form values

                Guid participantGUID = Guid.Empty;
                participantGUID = new Guid(Request["ParticipantGUID"].Trim());
                object participantID = Models.ParticipantModel.GetParticipantIDByParticipantGUID(participantGUID);

                #region variables
                ///participant form values
                Participant participant = new Participant();
                participant.DealID = dealID;
                ///to check from database
                participant.ParticipantTypeID = 20; // 20 = organisation
                participant.OrganizationID = organisationID;
                participant.ParticipantRoleID = Convert.ToInt16(Request["ptRoleExistOrg"]);
                participant.ParticipantID = Convert.ToInt32(participantID);
                #endregion end of variables

                #region ServerCAll Organisation Add
                object updateOrganisation = Models.ParticipantModel.UpdateOrganisation(participant, organization);
                #endregion End ServerCALL Organisation Add

                ///save organization if organisationID > 0
                #region ServerCAll particiapnt Add
                if (Convert.ToInt32(updateOrganisation) == 1)
                {
                    object updateParticipant = Models.ParticipantModel.UpdateParticipant(userID.ToString(), participant);
                    participantResult = updateParticipant.ToString();
                    TempData["ToasterParticipantMsg"] = Notifications.GetToastrMessage(Enums.MessageType.success.ToString(), "Participant has been successfully updated");
                }
                else
                {
                    TempData["ToasterParticipantMsg"] = Notifications.GetToastrMessage(Enums.MessageType.warning.ToString(), "Participant already existed");
                }
                #endregion ServerCAll particiapnt Add

                #region get bank & address count per participant
                object participantBankAddressCount = Models.ParticipantModel.GetParticipantInfo(dealID, null);
                ViewData["participantBankAddressCount"] = participantBankAddressCount;
                #endregion end of get bank & address count per participant

                var lastActionByResult = new DealController().LastActionBy(dealID);
                if (((JsonResult)lastActionByResult).Data != null)
                {
                    ViewData["LastActionBy"] = ((JsonResult)lastActionByResult).Data;
                }

                return View("PartialParticipant");
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
        /// save existing participant pop up
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveExistingParticipant()
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

                #region variables
                string aspNetUsersId = HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value;
                object userID = Models.UserModel.GetUserIDByAspNetID(aspNetUsersId);
                #endregion end variables

                //make sure that the field is actually being selected from the dropdown 
                if (Request["participantTypeID"] != "")
                {
                    int participantTypeID = Convert.ToInt32(Request["participantTypeID"]); //invidual || company
                    int dealTypesId = 0;
                    ///result varianle if saved
                    var participantResult = "";
                    int.TryParse(Request["ptTypePersonCompany"], out dealTypesId);


                    #region form Values
                    ///particiapant form key values /chk db
                    Participant participant = new Participant();
                    participant.ParticipantRoleID = Convert.ToInt16(Request["existptRoleexist"]);
                    participant.ParticipantTypeID = participantTypeID;
                    participant.DealID = dealID;
                    #endregion End form values

                    ///verify if individual or company
                    if (participantTypeID == 10) //individual
                    {
                        participant.PersonID = dealTypesId;
                        #region ServerCAll particiapnt Add
                        object addParticipant = Models.ParticipantModel.SaveParticipant(userID.ToString(), participant);
                        participantResult = addParticipant.ToString();
                        #endregion ServerCAll particiapnt Add
                    }
                    else //company
                    {
                        participant.OrganizationID = dealTypesId;
                        /// Update company type if selected organisation form values
                        #region ServerCAll particiapnt Add
                        object addParticipant = Models.ParticipantModel.SaveParticipant(userID.ToString(), participant);
                        participantResult = addParticipant.ToString();
                        #endregion ServerCAll particiapnt Add
                    }

                    //get participant count 
                    object participantBankAddressCount = Models.ParticipantModel.GetParticipantInfo(dealID, null);
                    ViewData["participantBankAddressCount"] = participantBankAddressCount;
                    //end of participant count

                    if (Convert.ToInt32(participantResult) > 0)
                    {
                        TempData["ToasterParticipantMsg"] = Notifications.GetToastrMessage(Enums.MessageType.success.ToString(), "Participant has been successfully added");
                    }
                    else
                    {
                        //dispay warning "Rows existed"
                        TempData["ToasterParticipantMsg"] = Notifications.GetToastrMessage(Enums.MessageType.warning.ToString(), "Participant already existed");
                    }
                }
                else
                {
                    //get participant count 
                    object participantBankAddressCount = Models.ParticipantModel.GetParticipantInfo(dealID, null);
                    ViewData["participantBankAddressCount"] = participantBankAddressCount;
                    //end of participant count
                    TempData["ToasterParticipantMsg"] = Notifications.GetToastrMessage(Enums.MessageType.warning.ToString(), "Select any participant from the list");
                }

                var lastActionByResult = new DealController().LastActionBy(dealID);
                if (((JsonResult)lastActionByResult).Data != null)
                {
                    ViewData["LastActionBy"] = ((JsonResult)lastActionByResult).Data;
                }
                return View("PartialParticipant");
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
        /// call wcf for autosearch for existing participant
        /// </summary>
        /// <returns></returns>
        public ActionResult AutoSearch()
        {
            try
            {
                #region Variables
                var searchResult = Request["GivenName"];
                string aspNetUsersId = HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value;
                object userId = Models.UserModel.GetUserIDByAspNetID(aspNetUsersId);
                string entityGUID = HttpContext.Request.Cookies["REPS_EntityGUID"].Value;
                int entityID = Convert.ToInt32(Models.EntityModel.GetEntityIDByEntityGUID(entityGUID));
                #endregion end vaiables
                ///call wcf for auto search 
                object getAutoSearchResult = Models.ParticipantModel.GetAutoSearchParticipantResult(Convert.ToInt32(userId), searchResult, entityID, null, null);
                ViewData["AutoSearchResult"] = getAutoSearchResult;
                return View("AutoSearchParticipantResult");
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
        ///  get Address details per participant id to ParticipantCardAddressDetailView
        /// </summary>
        /// <param name="participantID"></param>
        /// <returns></returns>
        public ActionResult PopupAddressInfo(System.Guid participantGUID)
        {
            try
            {
                /// Call WCF to get all countries
                object[] getCountry = Models.PropertyModel.GetCountries(null, null, null);
                ViewData["Countries"] = getCountry;

                /// Call WCF to get all Address Types
                object getAddressTypes = Models.AddressModel.GetAddressType(null, null, null);
                ViewData["AddressType"] = getAddressTypes;

                ///call WCF to get participant ID by ParticipantGUID
                object participantID = Models.ParticipantModel.GetParticipantIDByParticipantGUID(participantGUID);
                ViewData["ParticipantID"] = participantID;

                ViewData["participantGUID"] = participantGUID;

                return View("PopupAddressInfo");
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
        ///  get bank details per participant id to ParticipantCardBankDetailView
        /// </summary>
        /// <param name="participantID"></param>
        /// <returns></returns>
        public ActionResult PopupAddBank(System.Guid participantGUID)
        {
            try
            {
                /// Call WCF to get all Account Types
                object getAccountTypes = Models.BankModel.GetAccountType(null, null, null);
                ViewData["AccountType"] = getAccountTypes;

                /// Call WCF to get all Banks
                object getBanks = Models.BankModel.GetBanks(null, null, null, null, null, null);
                ViewData["Banks"] = getBanks;

                ///call WCF to get participant ID by ParticipantGUID
                object participantID = Models.ParticipantModel.GetParticipantIDByParticipantGUID(participantGUID);
                ViewData["ParticipantID"] = participantID;

                ViewData["participantGUID"] = participantGUID;

                return View("PopupAddBank");
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
        /// get province details if country is selected
        /// </summary>
        /// <param name="selectedValue"></param>
        /// <returns></returns>
        public ActionResult GetProvince(string selectedValueID)
        {
            try
            {
                ///get province details if country is selected
                object getProvince = Models.AddressModel.GetProvince(Convert.ToInt32(selectedValueID), null, null, null);
                ViewData["Province"] = getProvince;
                return PartialView("PartialProvince");
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
        /// add bank details
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddParticipantBankDetail()
        {
            Guid participantGUID = Guid.Empty;
            try
            {
                #region get deal id 
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
                #endregion end of  get deal id 

                //variables 
                object AddParticipantBankDetails = null;
                participantGUID = new Guid(Request.Form["participantGUID"]);
                object participantID = Models.ParticipantModel.GetParticipantIDByParticipantGUID(participantGUID);
                ViewData["ParticipantGUID"] = participantGUID;
                //end of variables

                #region form Values
                // Bank Details
                ParticipantBankDetail PartBank = new ParticipantBankDetail();
                PartBank.ParticipantID = Convert.ToInt32(participantID);
                PartBank.BankID = Request.Form.AllKeys.Contains("bankName") ? Convert.ToInt32(Request.Form["bankName"]) : 0;
                PartBank.AccountTypeID = Request.Form.AllKeys.Contains("accountType") ? Convert.ToInt32(Request.Form["accountType"]) : 0;
                PartBank.AccountName = Request.Form.AllKeys.Contains("accountHolder") ? Request.Form["accountHolder"] : null;
                PartBank.AccountNumber = Request.Form.AllKeys.Contains("accountNum") ? Convert.ToDecimal(Request.Form["accountNum"]) : 0;
                PartBank.SortCode = Request.Form.AllKeys.Contains("branchCode") ? Convert.ToDecimal(Request.Form["branchCode"]) : 0;
                #endregion End form values

                /// check if participant was successfully created
                if ((Convert.ToInt32(participantID)) > 0)
                {
                    TempData["ToasterMsgBank"] = Notifications.GetToastrMessage(Enums.MessageType.success.ToString(), "Bank has been successfully added");
                    // Add Participant Bank Details
                    AddParticipantBankDetails = Models.ParticipantModel.AddParticipantBankDetails(PartBank, dealID);
                }
                object participantBankAddressCount = Models.ParticipantModel.GetParticipantInfo(dealID, null);
                ViewData["participantBankAddressCount"] = participantBankAddressCount;

                var lastActionByResult = new DealController().LastActionBy(dealID);
                if (((JsonResult)lastActionByResult).Data != null)
                {
                    ViewData["LastActionBy"] = ((JsonResult)lastActionByResult).Data;
                }
                return View("PartialParticipant");
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
        /// wcf call to get bank detail per participant
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditBank(string participantBankDetailID, string formParticipantGUID)
        {
            Guid participantGUID = Guid.Empty;
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

                participantGUID = new Guid(formParticipantGUID);
                object participantID = Models.ParticipantModel.GetParticipantIDByParticipantGUID(participantGUID);

                /// Call WCF to get all Account Types
                object getAccountTypes = Models.BankModel.GetAccountType(null, null, null);
                ViewData["AccountType"] = getAccountTypes;

                /// Call WCF to get all Banks
                object getBanks = Models.BankModel.GetBanks(null, null, null, null, null, null);
                ViewData["Banks"] = getBanks;

                ViewData["participantGUID"] = participantGUID;
                ViewData["ParticipantBankDetailID"] = participantBankDetailID;

                // Get all banks associated with the participant
                ViewData["ParticipantBanks"] = Models.BankModel.GetBankDetailsPerParticipant(Convert.ToInt32(participantID), null);
                // End of  Get all banks associated with the participant
                return View("EditBank");
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
        /// Update Participant Bank Details pop up ajax
        /// </summary>
        /// <param name="participantBankID"></param>
        /// <param name="participantID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateParticipantBank()
        {
            try
            {
                //variables

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



                object ParticipantBankDetails = null;
                int participantBankDetailID = Convert.ToInt32(Request.Form["participantBankDetailID"]);
                Guid participantGUID = Guid.Empty;
                participantGUID = new Guid(Request.Form["participantGUID"]);

                //get participant id
                object participantID = Models.ParticipantModel.GetParticipantIDByParticipantGUID(participantGUID);
                //end of get participant id

                //get userid
                string aspNetUsersId = HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value;
                object userID = Models.UserModel.GetUserIDByAspNetID(aspNetUsersId);
                //end of get user id

                //end of variables
                #region form Values
                // Bank Details
                ParticipantBankDetail PartBank = new ParticipantBankDetail();
                PartBank.ID = participantBankDetailID;
                PartBank.ParticipantID = Convert.ToInt32(participantID);
                PartBank.BankID = Request.Form.AllKeys.Contains("bankName") ? Convert.ToInt32(Request.Form["bankName"]) : 0;
                PartBank.AccountTypeID = Request.Form.AllKeys.Contains("accountType") ? Convert.ToInt32(Request.Form["accountType"]) : 0;
                PartBank.AccountName = Request.Form.AllKeys.Contains("accountHolder") ? Request.Form["accountHolder"] : null;
                PartBank.AccountNumber = Request.Form.AllKeys.Contains("accountNum") ? Convert.ToDecimal(Request.Form["accountNum"]) : 0;
                PartBank.SortCode = Request.Form.AllKeys.Contains("branchCode") ? Convert.ToDecimal(Request.Form["branchCode"]) : 0;
                #endregion End form values

                /// check if participant was successfully created
                if (Convert.ToInt32(participantID) > 0)
                {
                    // Add Participant Bank Details
                    ParticipantBankDetails = Models.BankModel.UpdateParticipantBankDetails(PartBank, dealID);
                    if (Convert.ToInt32(ParticipantBankDetails.ToString()) > 0)
                    {
                        TempData["ToasterMsgBank"] = Notifications.GetToastrMessage(Enums.MessageType.success.ToString(), "Bank has been successfully updated");
                    }
                }
                // Get all banks associated with the participant
                ViewData["ParticipantBanks"] = Models.BankModel.GetBankDetailsPerParticipant(Convert.ToInt32(participantID), null);
                ViewData["participantGUID"] = participantGUID;
                return View("PopupBank");
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
        /// get bank details to popup panel
        /// </summary>
        /// <param name="participantID"></param>
        /// <returns></returns>
        public ActionResult PopupBank(string participantGUIDFormValue)
        {
            Guid participantGUID = Guid.Empty;
            try
            {
                participantGUID = new Guid(participantGUIDFormValue);
                object participantID = Models.ParticipantModel.GetParticipantIDByParticipantGUID(participantGUID);

                //wcf to Get all banks associated with the participant
                ViewData["ParticipantBanks"] = Models.BankModel.GetBankDetailsPerParticipant(Convert.ToInt32(participantID), null);
                ViewData["participantGUID"] = participantGUID;
                //end of wcf to Get all banks associated with the participant
                //return RedirectToAction("Index", "Login");
                return PartialView("PopupBank");
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
        /// wcf call to get all address details in list
        /// </summary>
        /// <param name="participantGUIDFormValue"></param>
        /// <returns></returns>
        public ActionResult PopupAddress(string participantGUIDFormValue)
        {
            Guid participantGUID = Guid.Empty;
            try
            {
                participantGUID = new Guid(participantGUIDFormValue);
                object participantID = Models.ParticipantModel.GetParticipantIDByParticipantGUID(participantGUID);
                // Get all address associated with the participant
                ViewData["AddressInfo"] = Models.AddressModel.GetAddress(Convert.ToInt32(participantID), null, null, null);
                ViewData["participantGUID"] = participantGUID;
                // end of Get all address associated with the participant
                return View("PopupAddress");
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
        ///  get Address details per participant id to ParticipantCardAddressDetailView
        /// </summary>
        /// <param name="participantID"></param>
        /// <returns></returns>
        public ActionResult PopupAddAddress(System.Guid participantGUID)
        {
            try
            {
                /// Call WCF to get all countries
                object[] getCountry = Models.PropertyModel.GetCountries(null, null, null);
                ViewData["Countries"] = getCountry;

                /// Call WCF to get all Address Types
                object getAddressTypes = Models.AddressModel.GetAddressType(null, null, null);
                ViewData["AddressType"] = getAddressTypes;

                ///call WCF to get participant ID by ParticipantGUID
                object participantID = Models.ParticipantModel.GetParticipantIDByParticipantGUID(participantGUID);
                ViewData["ParticipantID"] = participantID;

                ViewData["participantGUID"] = participantGUID;
                return View("PopupAddAddress");
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
        /// Add Address
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddParticipantAddress()
        {
            Guid participantGUID = Guid.Empty;
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


                //variables
                participantGUID = new Guid(Request.Form["participantGUID"]);
                object participantID = Models.ParticipantModel.GetParticipantIDByParticipantGUID(participantGUID);
                ViewData["ParticipantID"] = participantID;

                object AddressDetails = null;
                //end of variables
                #region form Values
                // Address Details
                Address Addr = new Address();
                Addr.ParticipantID = Convert.ToInt32(participantID);
                Addr.AddressTypeID = Request.Form.AllKeys.Contains("addressType") ? Convert.ToInt32(Request.Form["addressType"]) : 0;
                Addr.AddressLine1 = Request.Form.AllKeys.Contains("addressLine1") ? Request.Form["addressLine1"] : null;
                Addr.AddressLine2 = (!string.IsNullOrEmpty(Request.Form["addressLine2"])) ? Request.Form["addressLine2"] : null;
                Addr.City = (!string.IsNullOrEmpty(Request.Form["city"])) ? Request.Form["city"] : null;
                Addr.CountryID = Request.Form.AllKeys.Contains("country") ? Convert.ToInt32(Request.Form["country"]) : 0;
                Addr.PostalCode = (!string.IsNullOrEmpty(Request.Form["zipCode"])) ? Request.Form["zipCode"] : null;

                ///province id
                if (Request.Form["province"].Trim() == "-1")
                {
                    Addr.ProvinceID = null;
                }
                else
                {
                    Addr.ProvinceID = Request.Form.AllKeys.Contains("province") ? Convert.ToInt32(Request.Form["province"]) : 0;
                }
                #endregion End form values

                ///save participant if personid > 0
                if ((Convert.ToInt32(participantID)) > 0)
                {
                    // Add Participant Address Details
                    AddressDetails = Models.AddressModel.SaveAddress(Addr, dealID);
                    if ((Convert.ToInt32(AddressDetails)) > 0)
                    {
                        TempData["ToasterParticipantMsg"] = Notifications.GetToastrMessage(Enums.MessageType.success.ToString(), "Address has been successfully added");
                    }
                }
                object participantBankAddressCount = Models.ParticipantModel.GetParticipantInfo(dealID, null);
                ViewData["participantBankAddressCount"] = participantBankAddressCount;
                var lastActionByResult = new DealController().LastActionBy(dealID);
                if (((JsonResult)lastActionByResult).Data != null)
                {
                    ViewData["LastActionBy"] = ((JsonResult)lastActionByResult).Data;
                }
                return View("PartialParticipant");
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
        /// wcf call to get Address detail per participant
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditAddress(string addressGUID, string participantGUIDFormValue)
        {
            Guid participantGUID = Guid.Empty;
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
                int addressID = 0;
                addressID = (int)Models.AddressModel.GetAddressIDByAddressGUID(Convert.ToString(Request["addressGUID"]));
                //variables
                dynamic AddressInfo = null;
                ViewData["AddressGUID"] = addressGUID;
                //get participant id
                participantGUID = new Guid(participantGUIDFormValue);
                object participantID = Models.ParticipantModel.GetParticipantIDByParticipantGUID(participantGUID);
                //end of get participant id
                //End of variables

                // Get all address associated with the participant
                ViewData["AddressInfo"] = Models.AddressModel.GetAddress(Convert.ToInt32(participantID), Convert.ToInt32(addressID), null, null);
                if (ViewData["AddressInfo"] != null)
                {
                    AddressInfo = (ViewData["AddressInfo"] as IEnumerable<dynamic>).FirstOrDefault();
                }

                //get province details if country is selected
                object getProvince = Models.AddressModel.GetProvince(AddressInfo["CountryID"], null, null, null);
                ViewData["Province"] = getProvince;

                //Call WCF to get all countries
                object[] getCountry = Models.PropertyModel.GetCountries(null, null, null);
                ViewData["Countries"] = getCountry;

                //Call WCF to get all Address Types
                object getAddressTypes = Models.AddressModel.GetAddressType(null, null, null);
                ViewData["AddressType"] = getAddressTypes;

                ViewData["participantGUID"] = participantGUIDFormValue;
                return View("EditAddress");
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
        ///  wcf to update address details pop up ajax
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateParticipantAddress()
        {
            try
            {
                //variables
                object AddressDetails = null;
                int addressID = (int)Models.AddressModel.GetAddressIDByAddressGUID(Convert.ToString(Request["addressGUID"]));
                Guid participantGUID = Guid.Empty;
                participantGUID = new Guid(Request.Form["participantGUID"]);

                //get participant id
                object participantID = Models.ParticipantModel.GetParticipantIDByParticipantGUID(participantGUID);
                //end of get participant id

                //get userid
                string aspNetUsersId = HttpContext.Request.Cookies["REPS_AspNetUsersId"].Value;
                object userID = Models.UserModel.GetUserIDByAspNetID(aspNetUsersId);
                //end of get user id

                #region get dealID by url
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
                #endregion end of get dealId by url

                //end of variables

                #region form values
                // Address Details
                Address Addr = new Address();
                Addr.AddressID = addressID;
                Addr.ParticipantID = Convert.ToInt32(participantID);
                Addr.AddressTypeID = Request.Form.AllKeys.Contains("addressType") ? Convert.ToInt32(Request.Form["addressType"]) : 0;
                Addr.AddressLine1 = Request.Form.AllKeys.Contains("addressLine1") ? Request.Form["addressLine1"] : null;
                Addr.AddressLine2 = Request.Form.AllKeys.Contains("addressLine2") ? Request.Form["addressLine2"] : null;
                Addr.City = Request.Form.AllKeys.Contains("city") ? Request.Form["city"] : null;
                Addr.CountryID = Request.Form.AllKeys.Contains("country") ? Convert.ToInt32(Request.Form["country"]) : 0;
                Addr.PostalCode = Request.Form.AllKeys.Contains("zipCode") ? Request.Form["zipCode"] : null;
                //Addr.PostalCode = Request.Form["zipCode"];
                if (Request.Form["province"].Trim() == "-1")
                {
                    Addr.ProvinceID = null;
                }
                else
                {
                    Addr.ProvinceID = Convert.ToInt32(Request.Form["province"]);
                }
                #endregion End form values

                //save participant if personid > 0
                if (Convert.ToInt32(participantID) > 0)
                {
                    // Add Participant Address Details
                    AddressDetails = Models.AddressModel.UpdateAddress(Addr, dealID);
                    TempData["ToasterParticipantMsg"] = Notifications.GetToastrMessage(Enums.MessageType.success.ToString(), "Address has been successfully updated");
                }
                // Get all address associated with the participant
                ViewData["AddressInfo"] = Models.AddressModel.GetAddress(Convert.ToInt32(participantID), null, null, null);
                ViewData["participantGUID"] = participantGUID;
                // end of Get all address associated with the participant
                ViewData["addressID"] = addressID;
                return View("PopupAddress");
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

        [HttpPost]
        public ActionResult RemoveBankParticipant(int participantBankDetailID, string formParticipantGUID)
        {
            Guid participantGUID = Guid.Empty;
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

                participantGUID = new Guid(formParticipantGUID);
                object participantID = Models.ParticipantModel.GetParticipantIDByParticipantGUID(participantGUID);

                #region Variables
                ///participant values
                ParticipantBankDetail partBank = new ParticipantBankDetail();
                partBank.ID = participantBankDetailID;
                partBank.Deleted = true;
                ///result varianle if saved
                var bankResult = "";
                #endregion end vaiables

                #region remove bank set delete to 1
                string bankDel = Models.ParticipantModel.RemoveParticipantBank(partBank, dealID);
                bankResult = bankDel.ToString();
                #endregion remove bank set delete to 1

                // Get all banks associated with the participant
                ViewData["ParticipantBanks"] = Models.BankModel.GetBankDetailsPerParticipant(Convert.ToInt32(participantID), null);

                ViewData["participantGUID"] = participantGUID;
                // End of  Get all banks associated with the participant

                //get participant bank count after remove
                object participantBankAddressCount = Models.ParticipantModel.GetParticipantInfo(dealID, null);
                ViewData["participantBankAddressCount"] = participantBankAddressCount;
                //end of get participant bank count after remove

                return View("PopupBank");
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
        /// get details to bank card
        /// </summary>
        /// <param name="participantID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ParticipantBankCard()
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

                #region get bank & address count per participant
                object participantBankAddressCount = Models.ParticipantModel.GetParticipantInfo(dealID, null);
                ViewData["participantBankAddressCount"] = participantBankAddressCount;
                #endregion end of get bank & address count per participant

                var lastActionByResult = new DealController().LastActionBy(dealID);
                if (((JsonResult)lastActionByResult).Data != null)
                {
                    ViewData["LastActionBy"] = ((JsonResult)lastActionByResult).Data;
                }
                return View("PartialParticipant");
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
        /// remove participant address
        /// </summary>
        /// <param name="addressID"></param>
        /// <param name="participantGUIDFormValue"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RemoveAddressParticipant(string addressGUID, string participantGUIDFormValue)
        {
            Guid participantGUID = Guid.Empty;
            try
            {
                int dealID = 0;
                int addressID = 0;
                addressID = (int)Models.AddressModel.GetAddressIDByAddressGUID(Convert.ToString(Request["addressGUID"]));
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

                participantGUID = new Guid(participantGUIDFormValue);
                object participantID = Models.ParticipantModel.GetParticipantIDByParticipantGUID(participantGUID);

                #region Variables
                ///participant values
                Address Address = new Address();
                Address.AddressID = addressID;
                Address.Deleted = true;
                ///result varianle if saved
                var addressResult = "";
                #endregion end vaiables

                #region remove address set delete to 1
                string addressDel = Models.ParticipantModel.RemoveParticipantAddress(Address, dealID);
                addressResult = addressDel.ToString();
                #endregion remove address set delete to 1
                // Get all address associated with the participant
                ViewData["AddressInfo"] = Models.AddressModel.GetAddress(Convert.ToInt32(participantID), null, null, null);
                ViewData["participantGUID"] = participantGUID;
                // end of Get all address associated with the participant
                if (Convert.ToInt32(addressResult) > 0)
                {
                    return View("PopupAddress");
                }
                else
                {
                    return new HttpStatusCodeResult((int)(Enums.ErrorCodeSatus.NotFound), "Participant could be removed, Please try again");
                }
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
        /// edit participant form
        /// </summary>
        /// <param name="participantID"></param>
        /// <returns></returns>
        public ActionResult PopupEditParticipant(string participantGUID)
        {
            Guid participantGUIDConvert = Guid.Empty;
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

                participantGUIDConvert = new Guid(participantGUID);
                object participantID = Models.ParticipantModel.GetParticipantIDByParticipantGUID(participantGUIDConvert);

                #region participant details
                /// Call WCF to get all participant types
                object getParticipantType = Models.ParticipantModel.GetParticipantType(null, null, null);
                ViewData["PartType"] = getParticipantType;

                /// call WCF to get all participantRole
                object getParticipantRole = Models.ParticipantModel.GetParticipantRole(null, null, null);
                ViewData["PartRole"] = getParticipantRole;
                #endregion end of participant details

                #region wcf call to get details associated with participant
                // Get Participant details
                ViewData["ParticipantInfo"] = Models.ParticipantModel.GetParticipant(dealID, Convert.ToInt32(participantID), null, null, null, null, null, null);

                /// Call WCF to get all organisation types 
                object getOrgTypes = Models.ParticipantModel.GetOrganisationType(null, null, null);
                ViewData["OrganizationTypes"] = getOrgTypes;

                //get organisation details
                ViewData["OrganisationDetails"] = Models.ParticipantModel.GetOrganisation(null, null, null);
                #endregion end of wcf call to get details associated with participant

                return View("PopupEditParticipant");
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
        /// remove participant 
        /// </summary>
        /// <param name="participantID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RemoveParticipant(string participantGUID)
        {
            Guid participantGUIDConvert = Guid.Empty;
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
                //get participant id
                participantGUIDConvert = new Guid(participantGUID);
                object participantID = Models.ParticipantModel.GetParticipantIDByParticipantGUID(participantGUIDConvert);
                //end of get participant id
                #region Variables
                ViewData["Deleted_Participant"] = "";
                var delParticipant = Request["Deleted"];
                ///participant values
                Participant participant = new Participant();
                participant.ParticipantID = Convert.ToInt16(participantID);
                participant.Deleted = true;
                ///result varianle if saved
                var participantResult = "";
                #endregion end vaiables

                #region remove participant set delete to 1
                string participantDel = Models.ParticipantModel.RemoveParticipant(participant, dealID);
                participantResult = participantDel.ToString();
                #endregion remove participant set delete to 1

                #region get bank & address count per participant
                object participantBankAddressCount = Models.ParticipantModel.GetParticipantInfo(dealID, null);
                ViewData["participantBankAddressCount"] = participantBankAddressCount;
                #endregion end of get bank & address count per participant

                var lastActionByResult = new DealController().LastActionBy(dealID);
                if (((JsonResult)lastActionByResult).Data != null)
                {
                    ViewData["LastActionBy"] = ((JsonResult)lastActionByResult).Data;
                }
                return View("PartialParticipant");
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
    }
}