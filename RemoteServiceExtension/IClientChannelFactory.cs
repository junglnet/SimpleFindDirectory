
namespace RemoteServiceExtension
{
    /// <summary>
    /// Интрефейс описывает фабрику каналов
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    public interface IClientChannelFactory<TService>
    {       
        void CloseChannelFactory();
        void RestoreChannelFactory();
        TService CreateChannel();
        void CloseChannel(TService channel);
        bool IsFaulted { get; }
    }
}
