using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transversal.Util.BaseDapper;
using static Transversal.Util.BaseDapper.BaseDapperGeneric;

namespace Transversal.Util.Negocio
{
    public abstract class BaseNoBusGenericBusiness
    {
        protected string ConString;
        protected DataBaseType Motor;
        protected BaseDapperGeneric GenericDA;
        protected BaseNoBusGenericBusiness()
        {

        }
        public BaseNoBusGenericBusiness(string conn, DataBaseType database, BaseDapperGeneric GenericDA)
        { 
            this.GenericDA = GenericDA;
            ConString = conn;
            Motor = database;
            GenericDA.SetConnString(conn);
            GenericDA.SetDataBase(database);
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
            return await GenericDA.ExecutedBasicQuery<T>(sql,param);
        }

        /// <summary>
        /// Ejecuta un comando en la base de datos
        /// </summary>
        /// <param name="sql">Comando SQL</param>
        /// <param name="param">Parámetros del comando</param>
        /// <returns></returns>
        public virtual async Task ExecuteBaseCommand(string sql, object param)
        {
            await GenericDA.ExecuteBaseCommand(sql,param);
        }

        /// <summary>
        /// Retorna el resultado del procedimiento
        /// </summary>
        /// <typeparam name="T">Tipo de objeto de la respuesta</typeparam>
        /// <param name="spname">Nombre del procedimiento</param>
        /// <param name="parameters">Parametros del procedimiento</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> ExecutedBaseStoredProcedureObject<T>(string spname, object parameters)
        {
            return await GenericDA.ExecutedBaseStoredProcedureObject<T>(spname,parameters);
        }

        /// <summary>
        /// Retorna el resultado de la query
        /// </summary>
        /// <typeparam name="T">Tipo de objeto en que se encapsula la respuesta</typeparam>
        /// <param name="sql">Consulta SQL</param>
        /// <param name="param">Parametros de la consulta</param>
        /// <returns></returns>
        public async Task<T> ExecutedBaseQuerySingle<T>(string sql, Object param)
        {
            return await GenericDA.ExecutedBaseQuerySingle<T>(sql,param);
        }
    }
}
