//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/// Class used to call DOcfusion Azure
/// Main use is to generae document based on a docx template and pass a json data to be filled in the template placeholders
/// Auth: HKR
/// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.IO;
using Global;

namespace Docfusion
{
    public class CDocfusionLite
    {

        #region Global Vars
        
        // The Client ID is used by the application to uniquely identify itself to Azure AD.
        // The App Key is a credential used by the application to authenticate to Azure AD.
        // The Tenant is the name of the Azure AD tenant in which this application is registered.
        // The AAD Instance is the instance of Azure, for example public Azure or Azure China.
        // The Authority is the sign-in URL of the tenant.
        //
        private static string aadInstance = null;
        private static string tenant = null;
        private static string clientId = null;
        private static string appKey = null;
        private static string resourceId = null;

        static string authority = null;

        private static AuthenticationContext authContext = null;
        private static ClientCredential clientCredential = null;

        static string URL = null;
        public static PDFDocumentOutputSettings pdfSettings = null;

        #endregion Global Vars

        /// <summary>
        /// Test Function for Connect to docfusion
        /// </summary>
        public static void ConnectToDocfusion()
        {
            aadInstance = ConfigurationManager.AppSettings["df:AADInstance"];
            tenant = ConfigurationManager.AppSettings["df:Tenant"];
            clientId = ConfigurationManager.AppSettings["df:ClientId"];
            appKey = ConfigurationManager.AppSettings["df:AppKey"];
            resourceId = ConfigurationManager.AppSettings["df:ResourceId"];

            authority = String.Format(CultureInfo.InvariantCulture, aadInstance, tenant);

            authContext = new AuthenticationContext(authority);
            clientCredential = new ClientCredential(clientId, appKey);
            
            URL  = ConfigurationManager.AppSettings["df:URL"];

            #region testexamplecode
            // Start the getauthentication method.
            //Task<AuthenticationResult> task = CDocfuisonLite.GetAuthentication();

            //docfusion example trial
            byte[] template = System.IO.File.ReadAllBytes(@"D:\E4Projects\UKPROPERTY\Docs\DF Lite Azure\DF Lite Azure\DF Lite Azure Test.docx"); 
            string jsonData = "{'CustomerDetails':{'FirstName':'Anthony','Surname':'Bloom'}}";
    
            TimeSpan? timeout = null;
            string templateVersion = "1";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            //parameters.Add("","");
            PDFDocumentOutputSettings pdfSettings = new PDFDocumentOutputSettings();
            pdfSettings.ApplyPDFSecurity = false;
            //pdfSettings.Compliance = PdfDocumentCompliance.Pdf15;
            pdfSettings.IsEmbedTrueTypeFontsForAsciiChars = false;
            pdfSettings.JpegQuality = 100;
            //pdfSettings.TextCompression = PdfDocumentTextCompression.Flate;

            pdfSettings.Properties = new PDFDocumentProperties();
            pdfSettings.Properties.Author = string.Empty;
            pdfSettings.Properties.Company = string.Empty;
            pdfSettings.Properties.Subject = string.Empty;
            pdfSettings.Properties.Title = string.Empty;
            pdfSettings.Properties.Category = string.Empty;
            pdfSettings.Properties.Comments = string.Empty;
            pdfSettings.Properties.Keywords = string.Empty;
            pdfSettings.Properties.NameOfApplication = string.Empty;

            pdfSettings.Watermark = new DocumentWatermark();

            //pdfSettings.Watermark.Format = WatermarkFormat.None;
            pdfSettings.Watermark.ImageWatermark = new ImageWatermark();
            pdfSettings.Watermark.WatermarkFontName = "Arial";
            pdfSettings.Watermark.WatermarkText = "Draft e4";
            //pdfSettings.Watermark.WatermarkTextColorName = "LightGray";
            pdfSettings.Watermark.Width = 200;
            pdfSettings.Watermark.Height = 100;
            pdfSettings.Watermark.Rotation = -45;
            //pdfSettings.Watermark.HorizontalAlignment = WatermarkHorizontalAlignment.Center;
            //pdfSettings.Watermark.VerticalAlignment = WatermarkVerticalAlignment.Center;
            pdfSettings.PreserveFormFields = true;
            pdfSettings.ApplyPDFSecurity = false;
            pdfSettings.OwnerPassword = string.Empty;
            pdfSettings.UserPassword = string.Empty;
            //pdfSettings.EncryptionAlgorithm = PdfDocumentEncryption.RC4_128;
            //pdfSettings.Permissions = PdfPermissionFlags.AllowPrinting;
            pdfSettings.PreserveFormFields = true;
            pdfSettings.ProcessFormFieldPlaceHolders = true;



            //Task<object> generationResult = CDocfuisonLite.GenerateDocument(template, jsonData, timeout, templateVersion, parameters, pdfSettings);

            #endregion end_testexamplecode
        }

        /// <summary>
        /// Set Default Parameters
        /// </summary>
        /// <param name="mimetype"></param>
        public static void SetDocfusionParams(Enums.MimeType mimetype)
        {
            aadInstance = ConfigurationManager.AppSettings["df:AADInstance"];
            tenant = ConfigurationManager.AppSettings["df:Tenant"];
            clientId = ConfigurationManager.AppSettings["df:ClientId"];
            appKey = ConfigurationManager.AppSettings["df:AppKey"];
            resourceId = ConfigurationManager.AppSettings["df:ResourceId"];

            authority = String.Format(CultureInfo.InvariantCulture, aadInstance, tenant);

            authContext = new AuthenticationContext(authority);
            clientCredential = new ClientCredential(clientId, appKey);

            URL = ConfigurationManager.AppSettings["df:URL"];



            Dictionary<string, string> parameters = new Dictionary<string, string>();

            #region PDFDocumentOutputSettings
           
                pdfSettings = new PDFDocumentOutputSettings();
                pdfSettings.ApplyPDFSecurity = false;
                //pdfSettings.Compliance = PdfDocumentCompliance.Pdf15;
                pdfSettings.IsEmbedTrueTypeFontsForAsciiChars = false;
                pdfSettings.JpegQuality = 100;
                //pdfSettings.TextCompression = PdfDocumentTextCompression.Flate;

                pdfSettings.Properties = new PDFDocumentProperties();
                pdfSettings.Properties.Author = string.Empty;
                pdfSettings.Properties.Company = string.Empty;
                pdfSettings.Properties.Subject = string.Empty;
                pdfSettings.Properties.Title = string.Empty;
                pdfSettings.Properties.Category = string.Empty;
                pdfSettings.Properties.Comments = string.Empty;
                pdfSettings.Properties.Keywords = string.Empty;
                pdfSettings.Properties.NameOfApplication = string.Empty;

                pdfSettings.Watermark = new DocumentWatermark();

                //pdfSettings.Watermark.Format = WatermarkFormat.None;
                pdfSettings.Watermark.ImageWatermark = new ImageWatermark();
                pdfSettings.Watermark.WatermarkFontName = "Arial";
                pdfSettings.Watermark.WatermarkText = "Draft e4";
                //pdfSettings.Watermark.WatermarkTextColorName = "LightGray";
                pdfSettings.Watermark.Width = 200;
                pdfSettings.Watermark.Height = 100;
                pdfSettings.Watermark.Rotation = -45;
                //pdfSettings.Watermark.HorizontalAlignment = WatermarkHorizontalAlignment.Center;
                //pdfSettings.Watermark.VerticalAlignment = WatermarkVerticalAlignment.Center;
                pdfSettings.PreserveFormFields = true;
                pdfSettings.ApplyPDFSecurity = false;
                pdfSettings.OwnerPassword = string.Empty;
                pdfSettings.UserPassword = string.Empty;
                //pdfSettings.EncryptionAlgorithm = PdfDocumentEncryption.RC4_128;
                //pdfSettings.Permissions = PdfPermissionFlags.AllowPrinting;
                pdfSettings.PreserveFormFields = true;
                pdfSettings.ProcessFormFieldPlaceHolders = true;
            
            #endregion pdfPDFDocumentOutputSettings




    }


        /// <summary>
        /// Authenticate to Azure with API Key
        /// </summary>
        /// <returns></returns>
        private static async Task<AuthenticationResult> GetAuthentication()
            {
                //
                // Get an access token from Azure AD using client credentials.
                // If the attempt to get a token fails because the server is unavailable, retry twice after 3 seconds each.
                //
                AuthenticationResult result = null;
                int retryCount = 0;
                bool retry = false;

                do
                {
                    retry = false;
                    try
                    {
                        // ADAL includes an in memory cache, so this call will only send a message to the server if the cached token is expired.
                        result = await authContext.AcquireTokenAsync(resourceId, clientCredential);
                    }
                    catch (AdalException ex)
                    {
                        if (ex.ErrorCode == "temporarily_unavailable")
                        {
                            retry = true;
                            retryCount++;
                            Thread.Sleep(3000);
                        }

                        result = null;
                    }

                } while ((retry == true) && (retryCount < 3));
            
                return result;
            }


        /// <summary>
        /// This returns a GenerationResult
        /// </summary>
        /// <param name="template">File Template byte array</param>
        /// <param name="jsonData">Data to be filled in to template</param>
        /// <param name="timeout">timeout</param>
        /// <param name="templateVersion">Versioning</param>
        /// <param name="parameters">Any parrameters maily null</param>
        /// <param name="pdfSettings">Set Null for docx Version</param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<GenerationResult> GenerateDocument(
                    byte[] template,
                    string jsonData,
                    TimeSpan? timeout,
                    string templateVersion = null,
                    Dictionary<string, string> parameters = null,
                    PDFDocumentOutputSettings pdfSettings = null,   
                    string id = "")
        {

            if (timeout == null)
                timeout = TimeSpan.FromMinutes(3);

            try
            {
                AuthenticationResult auth = await GetAuthentication();
                if (auth == null)
                    throw new Exception("Unable to get security token");

                if (parameters == null)
                    parameters = new Dictionary<string, string>();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(URL);
                    client.DefaultRequestHeaders.ExpectContinue = false; // Obscure, but somehow some proxy servers choke with HTTP Error (417) Expectation Failed. Disable this header to fix

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth.AccessToken); // !!! Authentication

                    HttpResponseMessage response = await client.PostAsJsonAsync("GenerateDocument",
                        new GenerationRequest
                        {
                            Parameters = parameters,
                            TemplateData = template,
                            RequestData = jsonData,
                            TemplateVersion = templateVersion,
                            TemplateGuid = "",
                            TimeoutMilliseconds = Convert.ToInt32(timeout.Value.TotalMilliseconds),
                            PDFSettings = pdfSettings,
                        });


                    response.EnsureSuccessStatusCode();

                    string body = await response.Content.ReadAsStringAsync();

                    //Byte[] bytes = Convert.FromBase64String(JsonConvert.DeserializeObject<GenerationResult>(body).DocumentData);
                    //File.WriteAllBytes("d:\\log\\test.pdf", bytes);

                    return JsonConvert.DeserializeObject<GenerationResult>(body);
                }
            }
            catch (Exception ex)
            {
                return new GenerationResult
                {
                    GenerationLog = String.Format("Error while connecting to DocFusion. Error Message: {0}", ex.Message),
                    Succeeded = false,
                };
            }
        }

    }

    #region ModelsUsed
        public class PDFDocumentProperties
        {
            public string Author { get; set; }
            public string Company { get; set; }
            public string Subject { get; set; }
            public string Title { get; set; }
            public string Category { get; set; }
            public string Comments { get; set; }
            public string Keywords { get; set; }
            public string NameOfApplication { get; set; }
        }
        public class GenerationResult
        {
            public string GenerationLog { get; set; }
            public bool Succeeded { get; set; }
            public string DocumentData { get; set; }

            public string GenerationErrorCode { get; set; }
        }
        public class GenerationRequest
        {
            public string TemplateGuid { get; set; }
            public string TemplateVersion { get; set; }
            public byte[] TemplateData { get; set; }
            public string RequestData { get; set; }
            public int TimeoutMilliseconds { get; set; }
            public PDFDocumentOutputSettings PDFSettings { get; set; }
            public object Parameters { get; set; }
        }
        public class DocumentWatermark
        {
            public string WatermarkFontName { get; set; }//(string, optional),
            public string VerticalAlignment { get; set; }//(string, optional) = ['None', 'Default', 'Top', 'Center', 'Bottom', 'Inside', 'Outside', 'Inline'],
            public string HorizontalAlignment { get; set; }//(string, optional) = ['Default', 'None', 'Left', 'Center', 'Right', 'Inside', 'Outside'],
            public ImageWatermark ImageWatermark { get; set; }//(ImageWatermark, optional),
            public string Format { get; set; }//(string, optional) = ['None', 'Text', 'Image'],
            public int Width { get; set; }//(number, optional),
            public int Height { get; set; }//(number, optional),
            public int Rotation { get; set; }//(number, optional);
            public string WatermarkText { get; set; }//(string, optional),
            string WatermarkTextColorName { get; set; }//(string, optional),
            public int WatermarkTextColorInt { get; set; }//(integer, optional),
            public string WatermarkFont { get; set; }//(string, optional),
            public string WatermarkTextColor { get; set; }//(string, optional)
        }
        public class ImageWatermark
        {
            public string ImageData { get; set; }//(string, optional)
        }
        public class PDFDocumentOutputSettings
        {
            public PDFDocumentProperties Properties { get; set; }
            public DocumentWatermark Watermark { get; set; }
            public int JpegQuality { get; set; }//(integer, optional),
            public string Compliance { get; set; }//(string, optional) = ['Pdf15', 'PdfA1b'],
            public bool IsEmbedTrueTypeFontsForAsciiChars { get; set; }//(boolean, optional),
            public bool ApplyPDFSecurity { get; set; }//(boolean, optional),
            public string TextCompression { get; set; }//(string, optional) = ['None', 'Flate'],
            public bool PreserveFormFields { get; set; }//(boolean, optional),
            public string EncryptionAlgorithm { get; set; }//(string, optional) = ['RC4_40', 'RC4_128'],
            public string Permissions { get; set; }//(string, optional) = ['None', 'AllowPrinting', 'AllowModifyContents', 'AllowContentCopy', 'AllowModifyAnnotations', 'AllowFillIn', 'AllowContentCopyForAccessibility', 'AllowDocumentAssembly', 'AllowHighResolutionPrinting', 'All'],
            public string OwnerPassword { get; set; }//(string, optional),
            public string UserPassword { get; set; }//(string, optional),
            public bool ProcessFormFieldPlaceHolders { get; set; }//(boolean, optional)
        }
    #endregion ModelsUsed



}
