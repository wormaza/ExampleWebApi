using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Transversal.Util.Negocio;
using Transversal.Util.BaseDapper;
using static Transversal.Util.BaseDapper.BaseDapperGeneric;

namespace Transversal.Util.Controller
{
    [Route("api/[controller]")]
    //[Authorize]
    public abstract class BaseWebApiNoBusController<TBusiness, TModel> : ControllerBase, IBaseWebApiController<TBusiness, TModel>
                where TBusiness : BaseNoBusBusiness<TModel>, new()
                where TModel : class, new()
    {
        protected TBusiness Negocio;
        protected string conString;

        protected readonly ILogger _logger;

        protected BaseWebApiNoBusController(ILogger logger)
        {
            _logger = logger;
        }

        protected BaseWebApiNoBusController(string conString, IBaseDataAccess<TModel> dapper, ILogger logger)
        {
            _logger = logger;
            Negocio = new TBusiness();
            Negocio.SetConfiguration(conString, dapper);
            this.conString = conString;
        }

        protected BaseWebApiNoBusController(string conString, DataBaseType motor, IBaseDataAccess<TModel> dapper, ILogger logger)
        {
            _logger = logger;
            Negocio = new TBusiness();
            Negocio.SetConfiguration(conString, motor, dapper);
            this.conString = conString;
        }

        /// <summary>
        /// Retorna listado de todos los elementos
        /// </summary>
        /// <returns>List<typeparamref name="TModel"/></returns>
        protected virtual async Task<ActionResult<IEnumerable<TModel>>> Get()
        {
            try
            {
                return Ok(await Negocio.GetAll().ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Util Controller - BaseWebApiNoBusController - Get: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, $"{ex.Message}");
            }
        }

        /// <summary>
        /// Información asociada al Id ingresado
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns><typeparamref name="TModel"/></returns>
        protected virtual async Task<ActionResult<TModel>> GetById(string id)
        {
            try
            {
                return Ok(await Negocio.GetById(id).ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Util Controller - BaseWebApiNoBusController - GetById: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, $"{ex.Message}");
            }
        }

        /// <summary>
        /// Inserción de información
        /// </summary>
        /// <param name="value">TModel</param>
        /// <returns><typeparamref name="TModel"/></returns>
        protected virtual async Task<ActionResult<TModel>> Post([FromBody]TModel value)
        {
            try
            {
                return Ok(await Negocio.Insert(value).ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Util Controller - BaseWebApiNoBusController - Post: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, $"{ex.Message}");
            }
        }

        /// <summary>
        /// Actualizaciones 
        /// </summary>
        /// <param name="value"></param>
        /// <returns><typeparamref name="TModel"/></returns>
        protected virtual async Task<ActionResult<TModel>> Put([FromBody]TModel value)
        {
            try
            {
                return Ok(await Negocio.Update(value).ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Util Controller - BaseWebApiNoBusController - Put: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, $"{ex.Message}");
            }
        }

        /// <summary>
        /// Eliminación lógica del registro. True si se elimino, False e.o.c
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns>bool</returns>
        protected virtual async Task<ActionResult<TModel>> Delete(string id)
        {
            try
            {
                await Negocio.DeleteLogico(id).ConfigureAwait(false);
                return Ok(await Negocio.GetById(id).ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Util Controller - BaseWebApiNoBusController - Delete: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, $"{ex.Message}");
            }
        }
    }
}
