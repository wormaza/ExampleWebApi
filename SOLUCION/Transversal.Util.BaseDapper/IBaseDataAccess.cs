using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Transversal.Util.BaseDapper.BaseDapperGeneric;

namespace Transversal.Util.BaseDapper
{
    public interface IBaseDataAccess<T>: IBaseDataAccessGeneric
    {
        void SetConnString(string conString);
        void SetConnString(string conString, DataBaseType motor);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(dynamic Id);
        Task<IEnumerable<T>> ExecutedQuery(string sql, object param);
        Task<T> ExecutedQuerySingle(string sql, object param);
        Task<IEnumerable<T>> ExecutedGetStoredProcedure(string spname, object parameters);
        Task<T> Update(T input);
        Task<T> Insert(T input);
        Task<T> InsertOrUpdate(T input);
        Task Delete(T input);
        Task ExecuteCommand(string sql, object param);
        Task<bool> UpdateState(dynamic Id, int IdState);
        Task DeleteLogico(dynamic Id);
    }
}
