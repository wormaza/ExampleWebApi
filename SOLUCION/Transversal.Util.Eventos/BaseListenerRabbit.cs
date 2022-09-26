using System;
using MassTransit;
using System.Threading.Tasks;

namespace Transversal.Util.Eventos
{
    public abstract class BaseListenerRabbit<TRequested> : IBaseServiceRequest<TRequested>
                          where TRequested : BaseRequestedConsumer, new()
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
        protected TRequested Requested = new TRequested();

        public virtual void ServiceBusConfiguration()
        {
            SendBus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host(new Uri(HostName), h =>
                {
                    h.Username(Username);
                    h.Password(Password);
                });
            });

            this.ConfigSender();

            this.ConfigBaseRequestedConsumer();

            ListenBus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host(new Uri(HostName), h =>
                {
                    h.Username(Username);
                    h.Password(Password);
                });

                sbc.ReceiveEndpoint(string.Format("{0}_{1}", ExchangeName, FromMS), ep =>
                {
                    ep.Bind(ExchangeName);
                    ep.Consumer(() => Requested);
                });
            });
        }

        public virtual void ConfigSender()
        {
            Sender = SendBus.GetSendEndpoint(new Uri(string.Format("{0}/{1}", HostName,ExchangeName)));
        }

        public virtual void ConfigBaseRequestedConsumer()
        {
            Requested.SetConfiguration(FromMS, CnnString, SendBus, ExchangeName, HostName);
        }
    }
}
