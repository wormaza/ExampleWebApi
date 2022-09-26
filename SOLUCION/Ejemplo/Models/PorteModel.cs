using Dapper.Contrib.Extensions;

namespace Ejemplo.Models
{
    [Table("Portes")]
    public class PorteModel
    {
        [ExplicitKey]
        public string CodigoPorte { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
    }
}