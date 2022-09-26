using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transversal.Util.BaseDapper;
using Ejemplo.Models;

namespace Ejemplo.DataAccess
{
    public class FormaDA : BaseDapper<FormaModel>
    {
        public FormaDA() { }

        public FormaDA(string conString) : base(conString) { }

        public FormaDA(string conString, BaseDapperGeneric.DataBaseType motor) : base(conString, motor) { }
    }
}
