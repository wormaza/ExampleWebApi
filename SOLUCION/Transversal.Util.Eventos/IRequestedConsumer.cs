using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Transversal.Util.Eventos
{
    public interface IRequestedConsumer
    {
        Task Consume(ConsumeContext<IMensaje> context);
        void SetConfiguration(string FromMS, string cnnString, IBusControl SendBus, string Exchange, string Hostname);
    }
}
