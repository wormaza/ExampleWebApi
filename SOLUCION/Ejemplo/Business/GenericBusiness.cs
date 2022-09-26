using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transversal.Util.Negocio;
using Transversal.Util.BaseDapper;
using static Transversal.Util.BaseDapper.BaseDapperGeneric;
using Ejemplo.Models;
using Ejemplo.Models.Query;
using Ejemplo.DataAccess;


namespace Ejemplo.Business
{
    public class GenericBusiness:BaseNoBusGenericBusiness 
    {
        public GenericBusiness() : base() { }

        public GenericBusiness(string conn, DataBaseType database) : base(conn, database, new GenericDA()) { }
    }
}
