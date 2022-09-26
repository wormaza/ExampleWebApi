using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ejemplo.Models;
using System.Net;

namespace ejemplo.netcore.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class AutorController : ControllerBase
    {        
        private readonly ILogger<AutorController> _logger;
        private readonly List<AutorModel> _autoresEjemplo;
        private readonly List<LibroModel> _librosEjemplo;
        private readonly List<TipoModel>  _tiposEjemplo;
        public AutorController(ILogger<AutorController> logger,List<AutorModel> autoresEjemplo, List<LibroModel> librosEjemplo, List<TipoModel>  tiposEjemplo)
        {
            _logger = logger;
            _autoresEjemplo = autoresEjemplo;
            _librosEjemplo = librosEjemplo;
            _tiposEjemplo = tiposEjemplo;
        }

        /// <summary>
        /// Deletes a specific TodoItem.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "id": 1,
        ///        "name": "Item #1",
        ///        "isComplete": true
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [HttpGet("")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<AutorModel>>> Get()
        {
            try
            {
                return Ok(_autoresEjemplo.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error - ...");
                return StatusCode((int)HttpStatusCode.InternalServerError, $"{ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a specific TodoItem.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "id": 1,
        ///        "name": "Item #1",
        ///        "isComplete": true
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [HttpGet("{Id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<AutorModel>>> Get(int Id)
        {
            try
            {
                if(_autoresEjemplo.Any(l => l.Id == Id))
                    return Ok(_autoresEjemplo.First(l => l.Id == Id));
                else
                    return StatusCode((int)HttpStatusCode.BadRequest, String.Format("Libro Id: {0} no encontrado",Id)); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error - ...");
                return StatusCode((int)HttpStatusCode.InternalServerError, $"{ex.Message}");
            }
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<AutorModel>> Post([FromBody] AutorModel input)
        {
            try
            {
                if(_autoresEjemplo.Any(l => l.Id == input.Id))
                    return StatusCode((int)HttpStatusCode.BadRequest, String.Format("Libro Id: {0} ya existe",input.Id));
                else
                    return Ok(input); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error - ...");
                return StatusCode((int)HttpStatusCode.InternalServerError, $"{ex.Message}");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<LibroModel>> Put(int id, [FromBody] AutorModel input)
        {
            try
            {
                if(_autoresEjemplo.Any(l => l.Id == id))
                    return Ok(input);
                else
                    return StatusCode((int)HttpStatusCode.BadRequest, String.Format("Libro Id: {0} no existe",input.Id)); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error - ...");
                return StatusCode((int)HttpStatusCode.InternalServerError, $"{ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<int>> Delete(int id)
        {
            try
            {
                if(_autoresEjemplo.Any(l => l.Id == id))
                    return Ok(id);
                else
                    return StatusCode((int)HttpStatusCode.BadRequest, String.Format("Libro Id: {0} no existe",id)); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error - ...");
                return StatusCode((int)HttpStatusCode.InternalServerError, $"{ex.Message}");
            }
        }
    }
}