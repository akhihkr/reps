///---------------------------------------------------------------------------------------------------------------///
///PURPOSE : the following class is used to intercept WCF call and add a header token  
/// --------------------------------------------------------------------------------------------------------------///
/// In web config e.g
/// ----------------------
///<!-- behaviors  -->
//<behaviors>
//  <endpointBehaviors>
//    < !--JSON-- >
//    < behavior name="jsonBehavior">
//      <webHttp defaultBodyStyle = "Wrapped" defaultOutgoingResponseFormat="Json" helpEnabled="true" />
//      <tokenMessageInjector/>
//    </behavior>
//  </endpointBehaviors>
//</behaviors>
//<!-- End behaviors -->
//<!-- Extensions to inject token in header -->
//<extensions>
//  <behaviorExtensions>
//    <add name = "tokenMessageInjector" type="REPS.UI.Models.CustomTokenBehaviorExtensionElement, REPS.UI"/>
//  </behaviorExtensions>
//</extensions>
//<!-- End Extensions -->
///-----------------------------------------------------------------------------------------------------------///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Configuration;

namespace REPS.UI.Models
{
    public class REPSMessageInspector : IClientMessageInspector
    {
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            /// throw new NotImplementedException();
        }

        public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)
        {
            HttpRequestMessageProperty httpRequest = request.Properties[HttpRequestMessageProperty.Name] as HttpRequestMessageProperty;
            if (httpRequest != null)
            {
                if (HttpContext.Current.Request.Cookies["Bearer"] != null)
                {
                    httpRequest.Headers.Remove("Bearer");//to prevent double insertion of bearer if exist (when service wcf is called twice)
                    httpRequest.Headers.Add("Bearer", HttpContext.Current.Request.Cookies["Bearer"].Value);
                }
            }
            return null;
        }
    }

    public class TokenCustomEndpointBehavior : IEndpointBehavior
    {
        /// <summary>
        /// Implements a modification or extension of the client across an endpoint.
        /// </summary>
        /// <param name="endpoint">The endpoint that is to be customized.</param>
        /// <param name="clientRuntime">The client runtime to be customized.</param>
        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.ClientMessageInspectors.Add(new REPSMessageInspector());
        }

        /// <summary>
        /// Implement to pass data at runtime to bindings to support custom behavior.
        /// </summary>
        /// <param name="endpoint">The endpoint to modify.</param>
        /// <param name="bindingParameters">The objects that binding elements require to support the behavior.</param>
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            // Nothing special here
        }

        /// <summary>
        /// Implements a modification or extension of the service across an endpoint.
        /// </summary>
        /// <param name="endpoint">The endpoint that exposes the contract.</param>
        /// <param name="endpointDispatcher">The endpoint dispatcher to be modified or extended.</param>
        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            // Nothing special here
        }

        /// <summary>
        /// Implement to confirm that the endpoint meets some intended criteria.
        /// </summary>
        /// <param name="endpoint">The endpoint to validate.</param>
        public void Validate(ServiceEndpoint endpoint)
        {
            // Nothing special here
        }
    }

    public class CustomTokenBehaviorExtensionElement : BehaviorExtensionElement
    {
        protected override object CreateBehavior()
        {
            return new TokenCustomEndpointBehavior();
        }

        public override Type BehaviorType
        {
            get
            {
                return typeof(TokenCustomEndpointBehavior);
            }
        }
    }
}