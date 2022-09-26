using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Transversal.Util.Negocio
{
    interface IBaseBusiness<TModel>
    {
        Task<IEnumerable<TModel>> GetAll();
        Task<TModel> GetById(dynamic Id);
        Task DeleteLogico(string Id);
        Task<TModel> Update(TModel input);
        Task<TModel> Insert(TModel input);
        Task<TModel> InsertOrUpdate(TModel input);
    }
}
