using Dapper.Contrib.Extensions;

namespace Ejemplo.Models
{
    [Table("Formas")]
    public class FormaModel
    {
        [ExplicitKey]
        public string CodigoForma { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
    }
}