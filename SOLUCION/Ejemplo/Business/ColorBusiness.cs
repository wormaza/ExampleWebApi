using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transversal.Util.Negocio;
using static Transversal.Util.BaseDapper.BaseDapperGeneric;
using Ejemplo.Models;
using Ejemplo.Models.Query;
using Ejemplo.DataAccess;


namespace Ejemplo.Business
{
    public class ColorBusiness : BaseNoBusBusiness<ColorModel>
    {
        public ColorBusiness() : base() { }

        public ColorBusiness(string Configuration) : base(Configuration, DataBaseType.SqlServer, new ColorDA()) { }
    }
}
