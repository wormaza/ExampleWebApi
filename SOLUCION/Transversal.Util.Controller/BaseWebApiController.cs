using System.Text;
using Microsoft.Extensions.Logging;
using Transversal.Util.Eventos;
using Transversal.Util.Negocio;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Transversal.Util.BaseDapper;

namespace Transversal.Util.Controller
{
    [Route("api/[controller]")]
    public abstract class BaseWebApiController<TBusiness, TModel> : BaseWebApiNoBusController<TBusiness, TModel>
                where TBusiness : BaseBusiness<TModel>, new()
                where TModel : class, new()
    {
        protected IBusService BusServicios;

        protected BaseWebApiController(string conString, IBusService serviceBus, IBaseDataAccess<TModel> dapper, ILogger logger) : base(logger)
        {
            Negocio = new TBusiness();
            Negocio.SetConfiguration(conString, serviceBus, dapper);
            BusServicios = serviceBus;
            this.conString = conString;
        }

        protected BaseWebApiController(string conString, IBusService serviceBus, IBaseDataAccess<TModel> dapper, IConfiguration configuration, ILogger logger) : base(logger)
        {
            Negocio = new TBusiness();
            Negocio.SetConfiguration(conString, serviceBus, dapper);
            BusServicios = serviceBus;
            this.conString = conString;
        }
    }
}
