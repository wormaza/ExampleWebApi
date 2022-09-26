using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transversal.Util.BaseDapper;
using static Transversal.Util.BaseDapper.BaseDapperGeneric;

namespace Transversal.Util.Negocio
{
    public abstract class BaseNoBusBusiness<TModel> : IBaseBusiness<TModel>
               where TModel : class, new()
    {
        protected IBaseDataAccess<TModel> AccesoDapper;

        protected string conString;

        protected BaseNoBusBusiness()
        {

        }

        protected BaseNoBusBusiness(IBaseDataAccess<TModel> dapper)
        {
            AccesoDapper = dapper;
        }

        protected BaseNoBusBusiness(string conString, DataBaseType motor, IBaseDataAccess<TModel> dapper)
        {
            AccesoDapper = dapper;
            AccesoDapper.SetConnString(conString, motor);
            this.conString = conString;
        }

        protected BaseNoBusBusiness(string conString, IBaseDataAccess<TModel> dapper)
        {
            AccesoDapper = dapper;
            AccesoDapper.SetConnString(conString);
            this.conString = conString;
        }
        
        public void SetConfiguration(string conString, DataBaseType motor, IBaseDataAccess<TModel> dapper)
        {
            AccesoDapper = dapper;
            AccesoDapper.SetConnString(conString, motor);
            this.conString = conString;
        }

        public void SetConfiguration(string conString, IBaseDataAccess<TModel> dapper)
        {
            AccesoDapper = dapper;
            AccesoDapper.SetConnString(conString);
            this.conString = conString;
        }

        public virtual async Task<IEnumerable<TModel>> GetAll()
        {
            return await AccesoDapper.GetAll().ConfigureAwait(false);
        }

        public virtual async Task<TModel> GetById(dynamic Id)
        {
            return await AccesoDapper.GetById(Id).ConfigureAwait(false) ?? new TModel();
        }

        public virtual async Task DeleteLogico(string Id)
        {
            await AccesoDapper.DeleteLogico(Id).ConfigureAwait(false);
        }

        public virtual async Task<TModel> Update(TModel input)
        {
            return await AccesoDapper.Update(input).ConfigureAwait(false);
        }

        public virtual async Task<TModel> Insert(TModel input)
        {
            return await AccesoDapper.Insert(input).ConfigureAwait(false);
        }

        public virtual async Task<TModel> InsertOrUpdate(TModel input)
        {
            return await AccesoDapper.InsertOrUpdate(input).ConfigureAwait(false);
        }
    }
}
