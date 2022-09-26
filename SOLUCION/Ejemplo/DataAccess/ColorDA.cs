using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transversal.Util.BaseDapper;
using Ejemplo.Models;

namespace Ejemplo.DataAccess
{
    public class ColorDA : BaseDapper<ColorModel>
    {
        public ColorDA() { }

        public ColorDA(string conString) : base(conString) { }

        public ColorDA(string conString, BaseDapperGeneric.DataBaseType motor) : base(conString, motor) { }
    }
}
