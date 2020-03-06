using System;
using System.ServiceModel;
using Bochky.Utils.Logger;
using Bochky.FindDirectory.Common.Interfaces;
using Bochky.FindDirectory.Service.Implementation;


namespace Bochky.FindDirectory.Service
{
    public class ServiceApp
    {
        private ServiceHost _host;

        private readonly ILogger _logger;
        
        public ServiceApp()
        {
            _logger = AppServiceFactory.Current.Logger;
        }

        public void Start()
        {


            try
            {

                _host = new ServiceHost(typeof(FindService));

                _host.Description.Behaviors.Add(new ErrorHandlerExtension(_logger));

                _host.Open();


                _logger.LogInfo("Service started");

                Console.WriteLine("Service started");
            }
            catch (Exception ex)
            {

                _logger.LogError(ex);

                Console.WriteLine(ex.Message);
            }

        }

        public void Stop()
        {
            try
            {
                _host.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {

                Console.WriteLine("Service stopped");
            }
        }
    }
}
