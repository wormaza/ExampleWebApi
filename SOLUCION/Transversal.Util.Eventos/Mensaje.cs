using System;
using System.Collections.Generic;
using System.Text;

namespace Transversal.Util.Eventos
{
    public abstract class Mensaje<T> : IMensaje
                                       where T : class, new()
    {
        public string FromMS { get; set; }
        public bool Compensacion { get; set; }
        public bool OnlyWeb { get; set; }
        public T CuerpoMensaje { get; set; }
    }
}
