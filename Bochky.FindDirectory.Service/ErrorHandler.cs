using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel;
using System;
using Bochky.Utils.Logger;


namespace Bochky.FindDirectory.Service
{
    public class ErrorHandler : IErrorHandler
    {

        private readonly ILogger _logger;
        public ErrorHandler(ILogger logger)
        {
            _logger = logger;

        }

        public bool HandleError(Exception error)
        {

            _logger.LogError(error);

            return true;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {

            if (error is FaultException)
                return;

            FaultException faultException
              = new FaultException(error.Message, new FaultCode("InnerServiceException"));

            MessageFault msg = faultException.CreateMessageFault();

            fault = Message.CreateMessage(version, msg, "null");
        }
    }
}
