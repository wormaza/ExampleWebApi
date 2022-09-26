using Dapper.Contrib.Extensions;

namespace Ejemplo.Models
{
    public class BordeModel
    {
        public string CodigoBorde { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
    }
}