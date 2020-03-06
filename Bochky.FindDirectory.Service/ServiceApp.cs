using System;
using System.ServiceModel;
using Bochky.FindDirectory.Common.Interfaces;
using Bochky.FindDirectory.Service.Implementation;

namespace Bochky.FindDirectory.Service
{
    public class ServiceApp
    {
        private ServiceHost _host;

        private IServiceFactoryServer _serviceFactoryServer;
        
        public ServiceApp()
        {

           

           

        }

        public void Start()
        {
            
            try
            {
                              

                _host = new ServiceHost(typeof(FindService));

                _serviceFactoryServer = AppServiceFactory.GetInstance();

                _host.Description.Behaviors.Add(new ErrorHandlerExtension(_serviceFactoryServer.Logger));

                _host.Open();


                _serviceFactoryServer.Logger.LogInfo("Service started");

                Console.WriteLine("Service started");
            }
            catch (Exception ex)
            {

                _serviceFactoryServer.Logger.LogError(ex);

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
