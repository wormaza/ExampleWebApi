using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transversal.Util.BaseDapper
{
    public abstract class BaseDapperGeneric: IBaseDataAccessGeneric
    {
        /// <summary>
        /// Los motores que pueden ser utilizados para instanciar los metodos base
        /// </summary>
        public enum DataBaseType
        {
            SqlServer,
            Postgresql
        };

        protected IDbConnection Db;

        public IDbConnection _db => Db;

        protected string ConString;
        protected DataBaseType Motor;

        protected BaseDapperGeneric(){ }

        public void SetDataBase(DataBaseType motor)
        {
            Motor = motor;
        }

        protected BaseDapperGeneric(string conString, DataBaseType motor)
        {
            SetConnString(conString);
            Motor = motor;
        }

        public void SetConnString(string conString)
        {
            switch (Motor)
            {
                case DataBaseType.SqlServer:
                    Db = new SqlConnection(conString);
                    break;
                case DataBaseType.Postgresql:
                    Db = new NpgsqlConnection(conString);
                    break;
                default:
                    throw new ArgumentException("No existe implementacion para motor identificado");
            }
            this.ConString = conString;
        }

        /// <summary>
        /// Ejecuta una query y obtiene una lista de objetos de tipo T
        /// </summary>
        /// <typeparam name="T">Tipo de los objetos de la respuesta</typeparam>
        /// <param name="sql">Consulta SQL</param>
        /// <param name="param">Parametros de la consulta</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> ExecutedBasicQuery<T>(string sql, object param)
        {
            var resultDapper = await ExecutableWrapper.ExecuteWrapperAsynConn<IEnumerable<T>>(ConString, Motor, async (_c) =>
            {
                return await _c.QueryAsync<T>(sql, param).ConfigureAwait(false);
            }, GetType().FullName);

            if (!resultDapper.Any())
                return Enumerable.Empty<T>();
            else
                return resultDapper;
        }

        /// <summary>
        /// Ejecuta un comando en la base de datos
        /// </summary>
        /// <param name="sql">Comando SQL</param>
        /// <param name="param">Parámetros del comando</param>
        /// <returns></returns>
        public virtual async Task ExecuteBaseCommand(string sql, object param)
        {
            await ExecutableWrapper.ExecuteWrapperAsynConn<IEnumerable<dynamic>>(ConString, Motor, async (_c) =>
            {
                return await _c.QueryAsync(sql, param).ConfigureAwait(false);
            }, GetType().FullName);
        }

        /// <summary>
        /// Ejecuta un procedimiento almacenado y retorna una lista de objetos de tipo T
        /// </summary>
        /// <typeparam name="T">Tipo de objeto de la respuesta</typeparam>
        /// <param name="spname">Nombre del procedimiento</param>
        /// <param name="parameters">Parametros del procedimiento</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> ExecutedBaseStoredProcedureObject<T>(string spname, object parameters)
        {
            var resultDapper = await ExecutableWrapper.ExecuteWrapperAsynConn<IEnumerable<T>>(ConString, Motor, async (_c) =>
            {
                return await _c.QueryAsync<T>(spname
                                                    , parameters
                                                    , commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            }, GetType().FullName).ConfigureAwait(false);

            return resultDapper;
        }

        /// <summary>
        /// Ejecuta una query y retonar un objeto del tipo T
        /// </summary>
        /// <typeparam name="T">Tipo de objeto en que se encapsula la respuesta</typeparam>
        /// <param name="sql">Consulta SQL</param>
        /// <param name="param">Parametros de la consulta</param>
        /// <returns></returns>
        public async Task<T> ExecutedBaseQuerySingle<T>(string sql, Object param)
        {
            IEnumerable<T> result = await ExecutedBasicQuery<T>(sql, param).ConfigureAwait(false);

            if (result == null || !result.Any())
                return default;

            return result.FirstOrDefault();
        }
    }
}
