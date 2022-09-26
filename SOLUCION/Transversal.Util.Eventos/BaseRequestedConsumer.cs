using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Transversal.Util.Eventos
{
    public abstract class BaseRequestedConsumer : IRequestedConsumer, IConsumer<IMensaje>
    {
        protected string FromMS;
        protected string cnnString;
        protected IBusControl SendBus;
        protected string Exchange;
        protected string Hostname;
        protected BaseRequestedConsumer()
        {
        }

        public void SetConfiguration(string FromMS, string cnnString, IBusControl SendBus, string Exchange, string Hostname)
        {
            this.FromMS = FromMS;
            this.cnnString = cnnString;
            this.SendBus = SendBus;
            this.Exchange = Exchange;
            this.Hostname = Hostname;
        }

        public void SetConfiguration(string FromMS)
        {
            this.FromMS = FromMS;
        }

        #pragma warning disable 1998
        public virtual async Task Consume(ConsumeContext<IMensaje> context)
        {
            if (!FromMS.Equals(context.Message.FromMS))
            {
                throw new NotImplementedException("Método para consumir servicios no implementado");
            }
        }
        #pragma warning restore 1998
    }
}
