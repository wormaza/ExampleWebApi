using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.Extensions.Logging;
using Ejemplo.Business;
using Ejemplo.DataAccess;
using Ejemplo.Models;
using Transversal.Util.Controller;
using static Transversal.Util.BaseDapper.BaseDapperGeneric;

namespace Ejemplo.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class ColorController : BaseWebApiNoBusController<ColorBusiness, ColorModel>
    {
        public ColorController(string conString, ILogger<ColorController> logger) : base(conString, DataBaseType.SqlServer, new ColorDA(), logger ) { }

        /// <summary>
        /// Obtiene todos los tipos de organismo
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sample request: TO DO
        /// </remarks>
        /// <response code="200">Returns ...</response>
        [HttpGet("")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ColorModel>>> GetColor() => await base.Get();

        /// <summary>
        /// Obtiene el tipos de organismo en base a su identificador
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request: TO DO
        /// </remarks>
        /// <response code="200">Returns ...</response>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<ColorModel>> GetColorById(string id) => await base.GetById(id);

        /// <summary>
        /// Ingresa nuevos tipos de organismo
        /// </summary>
        /// <param name="value">Informacion del tipo de documento</param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request: TO DO
        /// </remarks>
        /// <response code="200">Returns ...</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<ColorModel>> PostColor([FromBody]ColorModel value) => await base.Post(value);

        /// <summary>
        /// Actualiza informaci??n del tipo de organismo
        /// </summary>
        /// <param name="value">Informaci??n del sub tipo de documento</param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request: TO DO
        /// </remarks>
        /// <response code="200">Returns ...</response>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<ColorModel>> PutColor([FromBody]ColorModel value) => await base.Put(value);

        /// <summary>
        /// Elimina logicamente un registro
        /// </summary>
        /// <param name="id">Identificador del registro a eliminar</param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request: TO DO
        /// </remarks>
        /// <response code="200">Returns ...</response>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<ColorModel>> DeleteColor(string id) => await base.Delete(id);
    }
}