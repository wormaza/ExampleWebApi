using System;
using System.Collections.Generic;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Linq;
using System.Threading.Tasks;

namespace Transversal.Util.BaseDapper
{
    public abstract class BaseDapper<T> : BaseDapperGeneric, IBaseDataAccess<T> where T : class, new()
    {
        protected string TableName;
        protected const string STRING_TYPE = "String";
        protected string PrimaryKeyName;
        protected string PrimaryKeyType;
        public string StateName { get; set; } = "";

        #region FUNCIONES DE APOYO

        /// <summary>
        /// Se obtiene el nombre de la tabla desde las anotaciones dapper
        /// </summary>
        /// <returns></returns>
        protected string GetTablleName() => ((TableAttribute)typeof(T).GetCustomAttributes(typeof(TableAttribute), true)[0]).Name;

        /// <summary>
        /// Se setea el nombre de de la tabla en el objeto
        /// </summary>
        protected void SetTableName() => this.TableName = GetTablleName();

        protected string GetPrimaryKeyName()
        {
            foreach (PropertyInfo pi in typeof(T).GetProperties())
            {
                ExplicitKeyAttribute attribute = (ExplicitKeyAttribute)typeof(T)
                                                .GetProperty(pi.Name)
                                                .GetCustomAttributes(typeof(ExplicitKeyAttribute), false)
                                                .FirstOrDefault();

                if (attribute != null)
                    return pi.Name;
            }

            foreach (PropertyInfo pi in typeof(T).GetProperties())
            {
                KeyAttribute attribute = (KeyAttribute)typeof(T)
                                        .GetProperty(pi.Name)
                                        .GetCustomAttributes(typeof(KeyAttribute), false)
                                        .FirstOrDefault();

                if (attribute != null)
                    return pi.Name;
            }

            throw new AmbiguousMatchException("No se ha establecido nombre de atributo identificador");
        }

        protected void SetPrimaryKeyName() => this.PrimaryKeyName = GetPrimaryKeyName();

        protected string GetPrimaryKeyType()
        {
            foreach (PropertyInfo pi in typeof(T).GetProperties())
            {
                ExplicitKeyAttribute attribute = (ExplicitKeyAttribute)typeof(T)
                                                 .GetProperty(pi.Name)
                                                 .GetCustomAttributes(typeof(ExplicitKeyAttribute), false)
                                                 .FirstOrDefault();

                if (attribute != null)
                    return pi.PropertyType.Name;

            }

            foreach (PropertyInfo pi in typeof(T).GetProperties())
            {
                KeyAttribute attribute = (KeyAttribute)typeof(T)
                                        .GetProperty(pi.Name)
                                        .GetCustomAttributes(typeof(KeyAttribute), false)
                                        .FirstOrDefault();

                if (attribute != null)
                    return pi.PropertyType.Name;
            }

            throw new AmbiguousMatchException("No se ha establecido nombre de atributo identificador");
        }

        protected void SetPrimaryKeyType() => this.PrimaryKeyType = GetPrimaryKeyType();

        protected T SetPrimaryKey(dynamic input, T obj)
        {
            string _input = Convert.ToString(input);
            if (STRING_TYPE == this.PrimaryKeyType)
                typeof(T).GetProperty(this.PrimaryKeyName).SetValue(obj, _input);
            else
                typeof(T).GetProperty(this.PrimaryKeyName).SetValue(obj, Int32.Parse(_input));

            return obj;
        }

        protected bool ExistAttributtName(string AttributeName)
        {
            foreach (PropertyInfo pi in typeof(T).GetProperties())
                if (pi.Name.Equals(AttributeName))
                    return true;

            return false;
        }

        protected string GetColumnValueByColumnName(T obj, string ColumnName) => obj.GetType().GetProperty(ColumnName).GetValue(obj, null).ToString();

        protected string GetPrimaryKeyValue(T input) => input.GetType().GetProperty(this.PrimaryKeyName).GetValue(input, null).ToString();

        public string GetStateValue(T obj)
        {
            if (string.IsNullOrEmpty(StateName))
                throw new ArgumentException("No existe nombre para columna de estados");

            return GetColumnValueByColumnName(obj, StateName);
        }

        #endregion

        protected BaseDapper() { }

        /// <summary>
        /// Por omision el tipo de base de datos a considerar sera SqlServer
        /// </summary>
        /// <param name="conString"></param>
        protected BaseDapper(string conString) : base ()
        {
            Motor = DataBaseType.SqlServer;
            SetConnString(conString);
        }

        protected BaseDapper(string conString, DataBaseType motor) : base(conString, motor) { }
        
        public new void SetConnString(string conString)
        {
            base.SetConnString(conString);
            this.SetTableName();
            this.SetPrimaryKeyName();
            this.SetPrimaryKeyType();
        }

        public void SetConnString(string conString, DataBaseType motor)
        {
            base.SetDataBase(motor);
            base.SetConnString(conString);
            this.SetTableName();
            this.SetPrimaryKeyName();
            this.SetPrimaryKeyType();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAll()
        {
            var resultDapper = await ExecutableWrapper.ExecuteWrapperAsynConn<IEnumerable<T>>(ConString, Motor, async (_c) =>
            {
                IEnumerable<T> result = await _c.GetAllAsync<T>().ConfigureAwait(false);
                return result;
            }, GetType().FullName);

            return resultDapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public virtual async Task<T> GetById(dynamic Id)
        {
            T id = new T();
            id = this.SetPrimaryKey(Id, id);

            var idvalue = id.GetType().GetProperty(this.PrimaryKeyName).GetValue(id, null).ToString();
            var resultDapper = await ExecutableWrapper.ExecuteWrapperAsynConn<T>(ConString, Motor, async (_c) =>
            {
                if(GetPrimaryKeyType().Equals(STRING_TYPE))
                {
                    return await _c.GetAsync<T>(idvalue).ConfigureAwait(false);
                }
                else
                {
                    return await _c.GetAsync<T>(int.Parse(idvalue)).ConfigureAwait(false);
                }

                
            }, GetType().FullName);

            if (resultDapper == null)
                return null;
            else
                return resultDapper;
        }

        public virtual async Task<T> GetById(T id)
        {
            return await GetById(id.GetType().GetProperty(this.PrimaryKeyName).GetValue(id, null).ToString()).ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> ExecutedQuery(string sql, object param)
        {
            return await ExecutedBasicQuery<T>(sql, param).ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<T> ExecutedQuerySingle(string sql, Object param)
        {
            return await ExecutedBaseQuerySingle<T>(sql, param).ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spname"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> ExecutedGetStoredProcedure(string spname, object parameters)
        {
            return await ExecutedBaseStoredProcedureObject<T>(spname, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual async Task<T> Update(T input)
        {
            await ExecutableWrapper.ExecuteWrapperAsynConn<bool>(ConString, Motor, async (_c) =>
            {
                return await _db.UpdateAsync(input).ConfigureAwait(false);
            }, GetType().FullName);

            return await GetById(input).ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual async Task<T> Insert(T input)
        {
            int identity = await ExecutableWrapper.ExecuteWrapperAsynConn<int>(ConString, Motor, async (_c) =>
            {
                return await _c.InsertAsync(input).ConfigureAwait(false);
            }, GetType().FullName);

            if (identity == 0)
                return await GetById(input).ConfigureAwait(false);
            else
            {
                return await GetById(identity).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual async Task<T> InsertOrUpdate(T input)
        {
            if (await this.GetById(GetPrimaryKeyValue(input)).ConfigureAwait(false) == null)
                return await this.Insert(input).ConfigureAwait(false);
            else
                return await this.Update(input).ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public virtual async Task ExecuteCommand(string sql, object param)
        {
            await ExecuteBaseCommand(sql, param).ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual async Task Delete(T input)
        {
            await ExecutableWrapper.ExecuteWrapperAsynConn<bool>(ConString, Motor, async (_c) =>
            {
                return await _db.DeleteAsync(input).ConfigureAwait(false);
            }, GetType().FullName);
        }

        /// <summary>
        /// Actualizar estado de la entidad
        /// </summary>
        /// <param name="Id">Identificador de la entidad</param>
        /// <param name="IdState">Identificador (int) del estado</param>
        /// <returns></returns>
        public virtual Task<bool> UpdateState(dynamic Id, int IdState)
        {
            if (!ExistAttributtName(StateName))
                throw new ArgumentException("No existe atributo");

            return UpdateStateAsync(Id, IdState);
        }

        /// <summary>
        /// Realiza la accion de actualizacion del estado
        /// </summary>
        /// <param name="Id">Identificador de la entidad</param>
        /// <param name="IdState">Identificador (int) del estado</param>
        /// <returns></returns>
        public virtual async Task<bool> UpdateStateAsync(dynamic Id, int IdState)
        {
            T id = await this.GetById(Id).ConfigureAwait(false);

            typeof(T).GetProperty(StateName).SetValue(id, IdState);

            id = await Update(id).ConfigureAwait(false);

            return id.GetType().GetProperty(this.PrimaryKeyName).GetValue(id, null).ToString().Equals(Convert.ToString(Id));
        }

        public virtual async Task DeleteLogico(dynamic Id)
        {
            T m = new T();
            m = this.SetPrimaryKey(Id, m);

            string stringfalse = "0";

            if (Motor == DataBaseType.Postgresql)
                stringfalse = "'f'";
            if (Motor == DataBaseType.SqlServer)
                stringfalse = "0";


            string sql =
               STRING_TYPE == (typeof(T).GetProperty(this.PrimaryKeyName).GetValue(m, null)).GetType().FullName ?
                   string.Format("UPDATE {0} SET ACTIVO = {1} WHERE {2} = '{3}'", this.TableName, stringfalse, this.PrimaryKeyName, m.GetType().GetProperty(this.PrimaryKeyName).GetValue(m, null).ToString()) :
                   string.Format("UPDATE {0} SET ACTIVO = {1} WHERE {2} = {3}", this.TableName, stringfalse, this.PrimaryKeyName, m.GetType().GetProperty(this.PrimaryKeyName).GetValue(m, null).ToString());

            await ExecutableWrapper.ExecuteWrapperAsynConn<IEnumerable<dynamic>>(ConString, Motor, async (_c) =>
            {
                return await _c.QueryAsync(sql).ConfigureAwait(false);
            }, GetType().FullName);
        }
    }
}
