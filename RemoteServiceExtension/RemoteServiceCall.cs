using System;
using System.Threading.Tasks;

namespace RemoteServiceExtension
{
    public static class RemoteServiceCall<TService>
    {
        
        
        public static TResult RemoteCall<TResult>(
            IClientChannelFactory<TService> channelFactory, 
            Func<TService, TResult> func)
        {

           
            TService channel = channelFactory.CreateChannel();                        

            try
            {
               
                return func(channel);

            }
            catch (Exception ex)
            {

                channelFactory.RestoreChannelFactory();             

                throw new Exception(ex.Message, ex);
            }
            finally
            {
                channelFactory.CloseChannel(channel);
            }

        }
        

        public static async Task<TResult> RemoteCall<TResult>(
            IClientChannelFactory<TService> channelFactory, 
            Func<TService, Task<TResult>> func)
        {

            TService channel = channelFactory.CreateChannel();            

            try
            {
                return await func(channel);
            }
            catch (Exception ex)
            {
                
                channelFactory.CloseChannelFactory();

                throw new Exception(ex.Message, ex);
            }
            finally
            {
                channelFactory.CloseChannel(channel);
            }
        }


        public static async Task RemoteCall(
            IClientChannelFactory<TService> channelFactory, 
            Func<TService, Task> action)
        {

            TService channel = channelFactory.CreateChannel();

            try
            {
                await action(channel);
            }
            catch (Exception ex)
            {

                channelFactory.CloseChannelFactory();

                throw new Exception(ex.Message, ex);
            }
            finally
            {
              channelFactory.CloseChannel(channel);
            }
        }

    }
}
