using System;
using System.ServiceModel;
using Bochky.Utils.Logger;
using Bochky.FindDirectory.Service.Implementation;


namespace Bochky.FindDirectory.Service
{
    public class ServiceApp
    {
        private ServiceHost _host;

        private static ILogger logger;

        public void Start()
        {

            logger = new NLogLogger("FindDirectory");            

            try
            {

                _host = new ServiceHost(typeof(FindService));

                _host.Description.Behaviors.Add(new ErrorHandlerExtension(logger));

                _host.Open();


                logger.LogInfo("Service started");

                Console.WriteLine("Service started");
            }
            catch (Exception ex)
            {

                logger.LogError(ex);

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
