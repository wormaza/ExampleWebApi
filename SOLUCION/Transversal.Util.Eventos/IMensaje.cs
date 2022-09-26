using System;
using System.Collections.Generic;
using System.Text;

namespace Transversal.Util.Eventos
{
    public interface IMensaje
    {
        string FromMS { get; set; }
        bool Compensacion { get; set; }
        bool OnlyWeb { get; set; }
    }
}
