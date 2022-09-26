using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Transversal.Util.BaseDapper
{
    public interface IBaseDataAccessGeneric
    {
        Task<IEnumerable<T>> ExecutedBasicQuery<T>(string sql, object param);
        Task ExecuteBaseCommand(string sql, object param);
        Task<IEnumerable<T>> ExecutedBaseStoredProcedureObject<T>(string spname, object parameters);
        Task<T> ExecutedBaseQuerySingle<T>(string sql, Object param);
    }
}
