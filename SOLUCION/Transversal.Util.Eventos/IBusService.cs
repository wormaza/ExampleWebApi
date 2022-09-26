using MassTransit;
using System.Threading.Tasks;

namespace Transversal.Util.Eventos
{
    public interface IBusService
    {
        string HostName { get; set; }
        string ExchangeName { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        IBusControl ListenBus { get; set; }
        string FromMS { get; set; }
        void ServiceBusConfiguration();
        void ConfigSender();
        IBusControl SendBus { get; set; }
        string CnnString { get; set; }
        Task<ISendEndpoint> Sender { get; set; }

    }
}
