using System;
using System.Diagnostics.Contracts;
using System.ServiceModel; 

namespace RemoteServiceExtension
{
    /// <summary>
    /// Класс описывает фабрику каналов
    /// </summary>
    /// <typeparam name="TService">Сервис</typeparam>
    public class ClientChannelFactory<TService> : IClientChannelFactory<TService>
    {
        private readonly string _endpointName;

        private Lazy<ChannelFactory<TService>> _channelFactory;

        public ClientChannelFactory(string endpointName)
        {
            Contract.Requires(!string.IsNullOrEmpty(endpointName));

            _endpointName = endpointName;

            CreateChannelFactory();            

            _channelFactory.Value.Faulted += (object sender, EventArgs e) =>
            {
                IsFaulted = true;
            };

        }

        private void CreateChannelFactory()
        {
            
            _channelFactory
                = new Lazy<ChannelFactory<TService>>(
                    () => new ChannelFactory<TService>(_endpointName));
                       

            if (_channelFactory.Value.State != CommunicationState.Faulted)
            {

                IsFaulted = false;

            }

        }

        public void RestoreChannelFactory()
        {
            try
            {

                _channelFactory.Value.Close();

            }
            finally
            {

                CreateChannelFactory();

            }
        }

        public void CloseChannelFactory() =>        
            _channelFactory.Value.Close();

        public TService CreateChannel()
        {

            try
            {
                return _channelFactory.Value.CreateChannel();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public void CloseChannel(TService channel) =>
            ((ICommunicationObject)channel).Close();

        public bool IsFaulted { get; private set; }

    }
}
