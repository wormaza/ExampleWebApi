using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Transversal.Util.Eventos
{
    public class BaseSender : IBusService
    {
        public string HostName { get; set; }
        public string ExchangeName { get; set; }
        public IBusControl ListenBus { get; set; }
        public string FromMS { get; set; }
        public string CnnString { get; set; }
        public IBusControl SendBus { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Task<ISendEndpoint> Sender { get; set; }

        public virtual void ServiceBusConfiguration()
        {

        }

        public virtual void ConfigSender()
        {
            Sender = SendBus.GetSendEndpoint(new Uri(string.Format("{0}/{1}", HostName, ExchangeName)));
        }
    }
}
