using System;
using System.Threading.Tasks;
using Transversal.Util.Eventos;
using MassTransit;
using Transversal.Util.BaseDapper;
using static Transversal.Util.BaseDapper.BaseDapperGeneric;

namespace Transversal.Util.Negocio
{
    public abstract class BaseBusiness<TModel> :  BaseNoBusBusiness<TModel>
                where TModel : class, new()
    {
        protected IBusService BusServicios;

       protected BaseBusiness()
        {
            
        }

        protected BaseBusiness( string conString, 
                                IBusControl _bus, 
                                string FromMS, 
                                string Exchange, 
                                string Hostname,
                                DataBaseType motor,
                                IBaseDataAccess<TModel> dapper)
        {
            AccesoDapper = dapper;
            AccesoDapper.SetConnString(conString, motor);
            this.conString = conString;
            this.BusServicios = new BaseSender
            {
                SendBus = _bus,
                HostName = Hostname,
                ExchangeName = Exchange,
                FromMS = FromMS
            };
            this.BusServicios.ConfigSender();
        }

        protected BaseBusiness(string conString,
                                IBusControl _bus,
                                string FromMS,
                                string Exchange,
                                string Hostname,
                                IBaseDataAccess<TModel> dapper)
        {
            AccesoDapper = dapper;
            AccesoDapper.SetConnString(conString);
            this.conString = conString;
            this.BusServicios = new BaseSender
            {
                SendBus = _bus,
                HostName = Hostname,
                ExchangeName = Exchange,
                FromMS = FromMS
            };
            this.BusServicios.ConfigSender();
        }

        public void SetConfiguration(   string conString, 
                                        IBusService _bus,
                                        IBaseDataAccess<TModel> dapper)
        {

            base.SetConfiguration(conString, dapper);
            this.BusServicios = _bus;
            BusServicios.ConfigSender();
            
        }

        public void SetConfiguration(string conString,
                                        IBusService _bus,
                                        DataBaseType motor,
                                        IBaseDataAccess<TModel> dapper)
        {

            base.SetConfiguration(conString, motor, dapper);
            this.BusServicios = _bus;
            BusServicios.ConfigSender();

        }

        public async Task<Task> SendMensajeToDO(IMensaje mensajeToDo)
        {
            var _sender = await BusServicios.Sender;
            await _sender.Send<IMensaje>(null);
            return Task.CompletedTask;
        }
    }
}
