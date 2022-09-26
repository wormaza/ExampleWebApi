using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ejemplo.Models;
using Ejemplo.Business;
using System.Net;
using Transversal.Util.BaseDapper;
using Transversal.Util.Negocio;

namespace ejemplo.netcore.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class BordesController : ControllerBase
    {        
        private readonly ILogger<BordesController> _logger;
        private readonly string _conn;
        private readonly BaseDapperGeneric.DataBaseType _databasetype;

        public BordesController(ILogger<BordesController> logger,string conString)
        {
            _logger = logger;
            _conn = conString;
            _databasetype = BaseDapperGeneric.DataBaseType.SqlServer;
        }

        /// <summary>
        /// Obtiene el listado de los elementos
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Si encuentra el listado</response>
        /// <response code="500">Problemas al obtener los datos</response>
        [HttpGet("")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<BordeModel>>> Get()
        {
            try
            {
                var GB = new GenericBusiness(_conn,_databasetype);
                return Ok(await GB.ExecutedBasicQuery<BordeModel>("SELECT CodigoBorde,Nombre,Activo FROM Bordes",null));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error - ...");
                return StatusCode((int)HttpStatusCode.InternalServerError, $"{ex.Message}");
            }
        } 
    }
}