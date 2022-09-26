using Dapper.Contrib.Extensions;

namespace Ejemplo.Models
{
    [Table("Colores")]
    public class ColorModel
    {
        [ExplicitKey]
        public string CodigoColor { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
    }
}