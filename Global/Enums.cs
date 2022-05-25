using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global
{
    public class Enums
    {
        public enum WCFTokenError
        {
            ErrorToastrAjax = 999,
            ErrorToastrNonAjax = 955,
            WrongTokenAjax = 888,
            WrongTokenNonAjax = 777,
            ErrorInHandling = 111
        }

        public enum FieldNameCheck
        {
            AdminRoleName,
            WorkflowName,
            WorkflowTaskName
        }

        public enum UserAccountDeactivated
        {
            deactivated
        }

        public enum MimeType
        {
            pdf,
            docx
        }

        public enum UploadType
        {
            standard,
            signed
        }

        public enum FieldType
        {
            Date = 1,
            Value = 2,
            Comment = 3,
            Type = 4,
            File = 5
        }

        public enum ValidationCheck
        {
            output = 0,
            systemCrash = 1,
            reusltOutput = 9
        }

        public enum TransactionType
        {
            Email = 1,
            New = 10,
            Edit = 20,
            Remove = 30,
            Completed = 40,
            Archived = 50
        }

        public enum WokflowTask
        {
            Deal = 1100,
            Participant = 1101,
            Property = 1102,
            Mortgage = 1103,
            Alerts = 4112,
            Email = 5113,
            SMS = 5114,
            Letter = 5115,
            GenerateDocument = 6116,
            UploadDocument = 6117,
            DownloadDocument = 6118,
            Fees = 7119
        }

        public enum Task
        {
            Fees = 119
        }

        public enum UniqueReference
        {
            Invalid = 999,
            Invalidreference = 988
        }

        public enum NullValues
        {
            [Description("[]")]
            DbNullValues = 1,

            [Description("null")]
            NullString = 2,

            [Description("Null")]
            Null = 000,

        }

        public enum WorkflowAction
        {
            AddFee = 150
        }

        public enum Wokflow
        {

            //Workflow = 3000,
            Process = 2000
        }

        public enum MessageType
        {
            info = 1,
            success = 2,
            warning = 3,
            error = 4
        }

        public enum WorkflowDocumentTypeList
        {
            stepWorkflow = 1,
        }

        public enum FolderPath
        {
            Actions = 1,
            Templates = 2,
            Documents = 3,
            Profile = 4,
            Correspondence = 5,
            Swift = 6
        }


        #region max-length varchar size
        /// <summary>
        /// set size to input type
        /// </summary>
        public enum maxLength
        {
            varcharMax = 8000,
            varcharAddress = 100,
            varcharName = 100,
            VarcharRegistrationNumber = 20,
            varcharVatID = 100,
            varcharTelephone = 18,
            varcharFax = 18,
            varcharAccount = 18,
            varcharEmail = 200,
            varchar100 = 100,
            varchar1000 = 1000,
            varchar20 = 20,
            varchar50 = 50,
            varchar255 = 255,
            varcharCity = 30,
            varcharPostalCode = 10,
            intValue = 10,
            decimalValue = 15,
            varchar200 = 200,
            varchar2000 = 2000,
            varchar250 = 250
        }
        #endregion end of max-length varchar sizeChartType

        #region error code status
        /// <summary>
        /// lis of error code staus
        /// </summary>
        public enum ErrorCodeSatus
        {
            //success
            OK = 200,
            Created = 201,
            Accepted = 202,
            NonAuthoritativeInformation = 203,
            NoContent = 204,
            ResetContent = 205,
            PartialContent = 206,
            MultiStatus = 207,
            AlreadyReported = 208,
            IMUsed = 226,
            //end of success

            //redirection
            MultipleChoices = 300,
            MovedPermanently = 301,
            Found = 302,
            SeeOther = 303,
            NotModified = 304,
            UseProxy = 305,
            SwitchProxy = 306,
            TemporaryRedirect = 307,
            PermanentRedirect = 308,
            //end of redirection

            //Client Error
            BadRequest = 400,
            Unauthorized = 401,
            PaymentRequired = 402,
            Forbidden = 403,
            NotFound = 404,
            MethodNotAllowed = 405,
            NotAcceptable = 406,
            ProxyAuthenticationRequired = 407,
            RequestTimeout = 408,
            Conflict = 409,
            Gone = 410,
            LengthRequired = 411,
            PreconditionFailed = 412,
            PayloadTooLarge = 413,
            URITooLong = 414,
            UnsupportedMediaType = 415,
            RangeNotSatisfiable = 416,
            ExpectationFailed = 417,
            Imateapot = 418,
            MisdirectedRequest = 421,
            UnprocessableEntity = 422,
            Locked = 423,
            FailedDependency = 424,
            UpgradeRequired = 426,
            PreconditionRequired = 428,
            TooManyRequests = 429,
            RequestHeaderFieldsTooLarge = 431,
            UnavailableForLegalReasons = 451,
            //End of Client Error

            //server Error
            InternalServerError = 500,
            NotImplemented = 501,
            BadGateway = 502,
            ServiceUnavailable = 503,
            GatewayTimeout = 504,
            HTTPVersionNotSupported = 505,
            VariantAlsoNegotiates = 506,
            InsufficientStorage = 507,
            LoopDetected = 508,
            NotExtended = 510,
            NetworkAuthenticationRequired = 511,
            //End of server error

            //unofficial codes
            Checkpoint = 103,
            MethodFailure = 420,
            Imafox = 419,
            EnhanceYourCalm = 420,
            BlockedbyWindowsParentalControls = 450,
            InvalidToken = 498,
            TokenRequired = 499,//OR Request has been forbidden by antivirus =499
            BandwidthLimitExceeded = 509,
            Siteisfrozen = 530,
            Networkconnecttimeouterror = 599,
            //end of unofficial codes

            //internet information services
            LoginTimeout = 440,
            RetryWith = 449,
            Redirect = 451,
            //end of internet information services
        }
        #endregion end of error code status

        #region chart types enum
        /// <summary>
        /// chart types enum
        /// </summary>
        public enum ChartType
        {
            bar,
            doughnut,
            polarArea,
            radar
        }
        #endregion end chart types enum

        #region PageNames

        public enum PageNames
        {
            Dashboard = 50,
            Deals = 100,
            Deal = 200,
            Reports = 300,
            Profile = 400,
            Administration = 500,
            CorrespondenceList = 600,
            SendEmail = 700,
            SendSMS = 800,
            AllAlerts = 900,
            TodayAlert = 1000,
            ComingAlerts = 1100,
            ArchiveAlerts = 1200,
            Workflow = 1300,
            Alerts = 1400,
            Correspondence = 1500,
            Documents = 1600,
            Audit = 1700,
            Payments = 1800,
            Users = 1900,
            TodayAlerts = 2000,
            Entity = 2100,
            Roles = 2200,
            Index = 2300,
            Security = 2400
        }

        #endregion PageNames

        #region currency 
        public enum Currency
        {
            [Description("R ")]
            ZAR = 1,
            [Description("Rs ")]
            MUR = 2
        }
        #endregion end of currency

        #region NoEntity 
        public enum NoEntity
        {
            [Description("You don't any entity assign to you.")]
            Noentity = 1,
            [Description("Please contact the Administrator")]
            ContactAdmin = 2,
            [Description("Refresh")]
            Refresh = 3,
        }
        #endregion end of NoEntity


        #region Chart Types
        public enum ChartTypes
        {
            [Description("Payment Per Deal Type")]
            PaymentDealType = 1,
            [Description("Monthly Payment Overview")]
            MonthlyPaymentOverview = 2,
            [Description("Work In Progress")]
            WorkInProgress = 3,
        }
        #endregion end of Chart Types

        public enum Document
        {
            Length  = 150,

        }

    }
}
