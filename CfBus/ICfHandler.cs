using System.Threading.Tasks;

namespace CfBus
{
    public interface ICfHandler
    {
        Task Initialize(IHostList hostList);

        void Configure(IConfigurationProvider configurationProvider);

        void ListenQueue();
    }
}