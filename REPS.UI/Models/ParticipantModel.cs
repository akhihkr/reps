using REPS.DATA.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Script.Serialization;


namespace REPS.UI.Models
{
    public class ParticipantModel
    {
        /// <summary>
        /// wcf call get bank & address count per participant
        /// </summary>
        /// <param name="dealID"></param>
        /// <param name="participantId"></param>
        /// <returns></returns>
        public static object GetParticipantInfo(int? dealID, int? participantId)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables
                /// Call WCF to get all participant types
                #region logic : wcf call get bank & address count per participant
                using (ParticipantServiceReference.ParticipantServiceClient participantServiceClient = new ParticipantServiceReference.ParticipantServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(participantServiceClient.InnerChannel))
                    {
                        resultValidator = participantServiceClient.GetParticipantInfo(dealID, participantId);                     
                        if (resultValidator != null && resultValidator.success == true && resultValidator.output.ToString() != null)
                        {
                            var outputServalCall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                            return outputServalCall;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end of logic : wcf call get bank & address count per participant
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Call WCF to get all participant types
        /// </summary>
        /// <returns></returns>
        public static object GetParticipantType(int? participantTypeId, int? startRow, int? endRow)
        {
            try
            {
                #region variables 
                Common.CValidator resultValidator = null;
                #endregion end variables

                #region logic : Call WCF to get all participant types
                using (ParticipantServiceReference.ParticipantServiceClient participantServiceClient = new ParticipantServiceReference.ParticipantServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(participantServiceClient.InnerChannel))
                    {
                        resultValidator = participantServiceClient.GetParticipantTypes(participantTypeId == null ? null : participantTypeId, startRow == null ? null : startRow, endRow == null ? null : endRow);                       
                        if (resultValidator != null && resultValidator.success == true && resultValidator.output.ToString() != null)
                        {
                            var outputServalCall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                            return outputServalCall;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end of logic : Call WCF to get all participant types
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// get participant role
        /// </summary>
        /// <param name="participantRoleId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static object GetParticipantRole(int? participantRoleId, int? startRow, int? endRow)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables
                /// call WCF to get all participantRole
                #region logic
                using (ParticipantServiceReference.ParticipantServiceClient participantServiceClient = new ParticipantServiceReference.ParticipantServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(participantServiceClient.InnerChannel))
                    {
                        resultValidator = participantServiceClient.GetParticipantRoles(participantRoleId == null ? null : participantRoleId, startRow == null ? null : startRow, endRow == null ? null : endRow);                     
                        if (resultValidator != null && resultValidator.success == true && resultValidator.output.ToString() != null)
                        {
                            var outputServalCall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                            return outputServalCall;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion logic
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// get organisation types
        /// </summary>
        /// <param name="organisationTypeId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static object GetOrganisationType(int? organisationTypeId, int? startRow, int? endRow)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables  
                /// Call WCF to get all organisation types 
                #region logic organisation types 
                using (OrganizationServiceReference.OrganizationServiceClient organizationServiceClient = new OrganizationServiceReference.OrganizationServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(organizationServiceClient.InnerChannel))
                    {
                        resultValidator = organizationServiceClient.GetOrganisationTypes(organisationTypeId == null ? null : organisationTypeId, startRow == null ? null : startRow, endRow == null ? null : endRow);                       
                        if (resultValidator != null && resultValidator.success == true && resultValidator.output.ToString() != null)
                        {
                            var outputServalCall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                            return outputServalCall;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end logic organisation types 
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// call wcf to get organisation details
        /// </summary>
        /// <param name="organisationTypeId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static object GetOrganisation(int? organisationTypeId, int? startRow, int? endRow)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables  
                /// Call WCF to get all organisation types 
                #region logic organisation types 
                using (OrganizationServiceReference.OrganizationServiceClient organizationServiceClient = new OrganizationServiceReference.OrganizationServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(organizationServiceClient.InnerChannel))
                    {
                        resultValidator = organizationServiceClient.GetOrganisation(organisationTypeId == null ? null : organisationTypeId, startRow == null ? null : startRow, endRow == null ? null : endRow);                       
                        if (resultValidator != null && resultValidator.success == true && resultValidator.output.ToString() != null)
                        {
                            var outputServalCall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                            return outputServalCall;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end logic organisation types 
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// saveperson details and return personID
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="personID"></param>
        /// <param name="formInputPerson"></param>
        /// <returns></returns>
        public static object SavePerson(Participant participant, Person formInputPerson)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables

                #region ServerCAll Person Add
                /// Call WCF to add person
                using (PersonServiceReference.PersonServiceClient personServiceClient = new PersonServiceReference.PersonServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(personServiceClient.InnerChannel))
                    {
                        resultValidator = personServiceClient.AddPerson(participant, formInputPerson);                    
                        if (resultValidator != null && resultValidator.success == true && resultValidator.output.ToString() != null && !Common.CString.CheckNullOrEmpty(resultValidator.output))
                        {
                            var outputServalCall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                            return outputServalCall;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion End ServerCALL Person Add
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Update Person
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="formInputPerson"></param>
        /// <returns></returns>
        public static object UpdatePerson(Participant participantObj, Person formInputPerson)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables

                #region ServerCAll Person Add
                /// Call WCF to add person
                /// 
                using (PersonServiceReference.PersonServiceClient personServiceClient = new PersonServiceReference.PersonServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(personServiceClient.InnerChannel))
                    {
                        resultValidator = personServiceClient.UpdatePerson(participantObj, formInputPerson);                     
                        if (resultValidator != null && resultValidator.success == true && resultValidator.output.ToString() != null && !Common.CString.CheckNullOrEmpty(resultValidator.output))
                        {
                            var outputServalCall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                            return outputServalCall;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion End ServerCALL Person Add
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// saved particiapnt result
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="formKeysParticipant"></param>
        /// <returns></returns>
        public static object SaveParticipant(string userID, Participant formKeysParticipant)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables

                #region ServerCAll participant Add
                using (ParticipantServiceReference.ParticipantServiceClient participantServiceClient = new ParticipantServiceReference.ParticipantServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(participantServiceClient.InnerChannel))
                    {
                        resultValidator = participantServiceClient.AddParticipant(formKeysParticipant, int.Parse(userID));                       
                        if (resultValidator != null && resultValidator.success == true && resultValidator.output.ToString() != null && !Common.CString.CheckNullOrEmpty(resultValidator.output))
                        {
                            var outputServalCall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                            return outputServalCall;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }

                #endregion end participant Add
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// update organisation details 
        /// </summary>
        /// <param name="formKeysOrg"></param>
        /// <returns></returns>
        public static object UpdateOrganisation(Participant participant, Organization formKeysOrg)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables

                #region ServerCAll Organisation Add       
                /// Call WCF 
                using (OrganizationServiceReference.OrganizationServiceClient organizationServiceClient = new OrganizationServiceReference.OrganizationServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(organizationServiceClient.InnerChannel))
                    {
                        resultValidator = organizationServiceClient.UpdateOrganisation(participant, formKeysOrg);
                        if (resultValidator != null && resultValidator.success == true && resultValidator.output.ToString() != null && !Common.CString.CheckNullOrEmpty(resultValidator.output))
                        {
                            var outputServalCall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                            return outputServalCall;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion End ServerCALL Organisation Add
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// get existing participant 
        /// </summary>
        /// <param name="dealId"></param>
        /// <param name="participantId"></param>
        /// <param name="givenName"></param>
        /// <param name="familyName"></param>
        /// <param name="identityNumber"></param>
        /// <param name="participantTypeId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static object GetParticipant(int dealId, int? participantId, object givenName, object familyName, object identityNumber, int? participantTypeId, int? startRow, int? endRow)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables 
                /// Call WCF to check if we have existing participants per deal
                #region logic organisation types 
                using (ParticipantServiceReference.ParticipantServiceClient participantServiceClient = new ParticipantServiceReference.ParticipantServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(participantServiceClient.InnerChannel))
                    {
                        resultValidator = participantServiceClient.GetParticipants(dealId == 0 ? 0 : dealId, participantId == null ? null : participantId, givenName == null ? "" : givenName.ToString(), familyName == null ? "" : familyName.ToString(), identityNumber == null ? "" : identityNumber.ToString(), participantTypeId == null ? null : participantTypeId, startRow == null ? null : startRow, endRow == null ? null : endRow);                       
                        if (resultValidator != null && resultValidator.success == true && resultValidator.output.ToString() != null)
                        {
                            var outputServalCall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                            return outputServalCall;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end logic organisation types 
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Update Participant
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="formKeysParticipant"></param>
        /// <returns></returns>
        public static object UpdateParticipant(string userID, Participant formKeysParticipant)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables

                #region ServerCAll participant Add
                using (ParticipantServiceReference.ParticipantServiceClient participantServiceClient = new ParticipantServiceReference.ParticipantServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(participantServiceClient.InnerChannel))
                    {
                        resultValidator = participantServiceClient.UpdateParticipant(formKeysParticipant);                       
                        if (resultValidator != null && resultValidator.success == true && resultValidator.output.ToString() != null && !Common.CString.CheckNullOrEmpty(resultValidator.output))
                        {
                            var outputServalCall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                            return outputServalCall;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end participant Add
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// save organisation 
        /// </summary>
        /// <param name="formKeysOrg"></param>
        /// <returns></returns>
        public static object SaveOrganisation(Organization formKeysOrg, Participant participant)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables

                #region ServerCAll Organisation Add       
                /// Call WCF 
                using (OrganizationServiceReference.OrganizationServiceClient organizationServiceClient = new OrganizationServiceReference.OrganizationServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(organizationServiceClient.InnerChannel))
                    {
                        resultValidator = organizationServiceClient.AddOrganisation(formKeysOrg, participant);                       
                        if (resultValidator != null && resultValidator.success == true && resultValidator.output.ToString() != null && !Common.CString.CheckNullOrEmpty(resultValidator.output))
                        {
                            var outputServalCall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                            return outputServalCall;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion End ServerCALL Organisation Add
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// call wcf to get participant details autosearch
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="searchResult"></param>
        /// <param name="entityID"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        public static string GetAutoSearchParticipantResult(int userID, string searchResult, int entityID, int? startRow, int? endRow)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                // string result = null;
                #endregion end variables 
                ///call wcf for auto search 
                #region logic organisation types 
                using (ParticipantServiceReference.ParticipantServiceClient participantServiceClient = new ParticipantServiceReference.ParticipantServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(participantServiceClient.InnerChannel))
                    {
                        resultValidator = participantServiceClient.GetParticipantAutosearch(userID, searchResult, entityID, startRow == null ? null : startRow, endRow == null ? null : endRow);                       
                        if (resultValidator != null && resultValidator.success ==true && resultValidator.output.ToString() != null)
                        {
                            var participantexist = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                            return resultValidator.output;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end logic organisation types 
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get Participant ID by ParticipantGUID
        /// </summary>
        /// <param name="aspNetId"></param>
        /// <returns></returns>
        public static object GetParticipantIDByParticipantGUID(System.Guid participantGUID)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables 

                #region logic Get Participant ID by ParticipantGUID
                using (ParticipantServiceReference.ParticipantServiceClient participantServiceClient = new ParticipantServiceReference.ParticipantServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(participantServiceClient.InnerChannel))
                    {
                        resultValidator = participantServiceClient.GetParticipantIDByParticipantGUID(participantGUID);                       
                        if (resultValidator != null && resultValidator.success == true && resultValidator.output.ToString() != null)
                        {
                            var participantexist = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                            return participantexist[0];
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion end logic Get Participant ID by ParticipantGUID
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Add Participant Bank Details
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="formInputParticipantBank"></param>
        /// <returns></returns>
        public static object AddParticipantBankDetails(ParticipantBankDetail formInputParticipantBank,int dealID)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables

                #region logic
                /// Call WCF to Add Participant Bank Details
                using (ParticipantServiceReference.ParticipantServiceClient participantServiceClient = new ParticipantServiceReference.ParticipantServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(participantServiceClient.InnerChannel))
                    {
                        resultValidator = participantServiceClient.AddParticipantBankDetails(formInputParticipantBank, dealID);
                        if (resultValidator != null && resultValidator.success == true && resultValidator.output.ToString() != null && !Common.CString.CheckNullOrEmpty(resultValidator.output))
                        {
                            var outputServalCall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                            return outputServalCall;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion End logic
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Remove Participant Bank
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="participantInput"></param>
        /// <returns></returns>
        public static string RemoveParticipantBank(ParticipantBankDetail participantInput, int dealID)
        {
            try
            {
                #region Variables
                Common.CValidator resultValidator = null;
                #endregion end vaiables
                ///remove participant set delete to 1
                #region remove participant set delete to 1
                using (ParticipantServiceReference.ParticipantServiceClient participantServiceClient = new ParticipantServiceReference.ParticipantServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(participantServiceClient.InnerChannel))
                    {
                        resultValidator = participantServiceClient.DeleteParticipantBank(participantInput, dealID);
                        if (resultValidator != null && resultValidator.success == true && resultValidator.output.ToString() != null && !Common.CString.CheckNullOrEmpty(resultValidator.output))
                        {
                            return resultValidator.ToString();
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion remove participant set delete to 1
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Remove Address
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="addressInput"></param>
        /// <returns></returns>
        public static string RemoveParticipantAddress(Address addressInput , int dealID)
        {
            try
            {
                #region Variables
                Common.CValidator resultValidator = null;
                #endregion end vaiables
                ///remove participant set delete to 1
                #region remove participant set delete to 1
                using (AddressServiceReference.AddressServiceClient addressServiceClient = new AddressServiceReference.AddressServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(addressServiceClient.InnerChannel))
                    {
                        resultValidator = addressServiceClient.DeleteAddress(addressInput, dealID);
                        if (resultValidator != null && resultValidator.success == true && resultValidator.output.ToString() != null && !Common.CString.CheckNullOrEmpty(resultValidator.output))
                        {
                            var outputServerCall = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                            return outputServerCall.ToString();
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion remove address set delete to 1
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// remove participant set delete to 1
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="participantInput"></param>
        /// <returns></returns>
        public static string RemoveParticipant(Participant participantInput, int dealID)
        {
            try
            {
                #region Variables
                Common.CValidator resultValidator = null;
                #endregion end vaiables
                ///remove participant set delete to 1
                #region remove participant set delete to 1
                using (ParticipantServiceReference.ParticipantServiceClient participantServiceClient = new ParticipantServiceReference.ParticipantServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(participantServiceClient.InnerChannel))
                    {
                        resultValidator = participantServiceClient.DeleteParticipant(participantInput, dealID);
                        if (resultValidator != null && resultValidator.success == true && resultValidator.output.ToString() != null && !Common.CString.CheckNullOrEmpty(resultValidator.output))
                        {
                            return resultValidator.ToString();
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion remove participant set delete to 1
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// wcf call get bank & address count per participant
        /// </summary>
        /// <param name="dealID"></param>
        /// <param name="participantId"></param>
        /// <returns></returns>
        public static object GetParticipantBankAccountCount(int? dealID, int? participantId)
        {
            try
            {
                #region variables
                Common.CValidator resultValidator = null;
                #endregion end variables
                /// Call WCF to get all participant types
                #region logic
                using (ParticipantServiceReference.ParticipantServiceClient participantServiceClient = new ParticipantServiceReference.ParticipantServiceClient())
                {
                    //operation context to read headers
                    using (OperationContextScope scope = new OperationContextScope(participantServiceClient.InnerChannel))
                    {
                        resultValidator = participantServiceClient.GetParticipantBankAccountCount(dealID, participantId); 
                        if (resultValidator != null && resultValidator.success == true && resultValidator.output.ToString() != null)
                        {
                            var outputResultValidator = new JavaScriptSerializer().Deserialize<dynamic>(resultValidator.output);
                            return outputResultValidator;
                        }
                        else
                        {
                            throw new Common.CWCFException("-Method-" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--");
                        }
                    }
                }
                #endregion logic
            }
            catch (Exception ex)
            {
                Common.CLog.WriteLogErr(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                throw new Exception(ex.Message);
            }
        }
    }
}