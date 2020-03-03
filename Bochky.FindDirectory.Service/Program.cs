using Topshelf;

namespace Bochky.FindDirectory.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<ServiceApp>(s =>
                {
                    s.ConstructUsing(cons => new ServiceApp());
                    s.WhenStarted(service => service.Start());
                    s.WhenStopped(service => service.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("Bochky Find Directory Service");
                x.SetDisplayName("BochkyFindDirectory.ServiceApp");
                x.SetServiceName("BochkyFindDirectory.ServiceApp");

            });
        }
    }
}
