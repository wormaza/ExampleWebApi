using System;

namespace Ejemplo.Models
{
    public class LibroModel
    {
        public int Id {get;set;}
        public int IdTipo {get;set;}
        public string Nombre { get; set; }
        public int IdAutor { get; set; }
        public string Resumen { get; set; }
        public Int16 Year { get; set; }        
    }
}