using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel;
using System;
using Bochky.FindDirectory.Common.Interfaces;
using System.ServiceModel.Description;
using System.Collections.ObjectModel;

namespace Bochky.FindDirectory.Service
{
    public class ErrorHandlerExtension : IServiceBehavior
    {
        private readonly ILogger _logger;
        public ErrorHandlerExtension(ILogger logger)
        {
            _logger = logger;

        }

        public void AddBindingParameters(
            ServiceDescription serviceDescription,
            ServiceHostBase serviceHostBase,
            Collection<ServiceEndpoint> endpoints,
            BindingParameterCollection bindingParameters)
        {



        }

        public void ApplyDispatchBehavior(
            ServiceDescription serviceDescription,
            ServiceHostBase serviceHostBase)
        {

            var handler = new ErrorHandler(_logger);

            foreach (var item in serviceHostBase.ChannelDispatchers)
            {

                var chDisp = item as ChannelDispatcher;

                if (chDisp != null)
                {
                    chDisp.ErrorHandlers.Add(handler);
                }

            }
        }

        public void Validate(
            ServiceDescription serviceDescription,
            ServiceHostBase serviceHostBase)
        {

        }
    }
}
