using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transversal.Util.BaseDapper;
using Ejemplo.Models;

namespace Ejemplo.DataAccess
{
    public class PorteDA : BaseDapper<PorteModel>
    {
        public PorteDA() { }

        public PorteDA(string conString) : base(conString) { }

        public PorteDA(string conString, BaseDapperGeneric.DataBaseType motor) : base(conString, motor) { }
    }
}
