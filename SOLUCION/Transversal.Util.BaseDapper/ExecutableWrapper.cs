using System;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using static Transversal.Util.BaseDapper.BaseDapperGeneric;
using Npgsql;

namespace Transversal.Util.BaseDapper
{
    public static class ExecutableWrapper
    {
        /// <summary>
        /// Encapsula las llamadas a la base de datos
        /// </summary>
        /// <typeparam name="TResult">Resultado de la llamada</typeparam>
        /// <param name="conn">SqlConnection</param>
        /// <param name="func">Función a ejecutar</param>
        /// <param name="ContextName">Nombre del método desde donde se esta llamando</param>
        /// <returns>Resultado de la llamada a la base de datos</returns>
        public static async Task<TResult> ExecuteWrapper<TResult>(IDbConnection conn, Func<IDbConnection, Task<TResult>> func, string ContextName = "DapperBasic")
        {
            TResult result;
            try
            {
                conn.Open();
                result = await func(conn);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException(String.Format("{0}.ExecuteWrapper() TimeOut: " + ex.InnerException, ContextName), ex);
            }
            catch (SqlException ex)
            {
                throw new EvaluateException(String.Format("{0}.ExecuteWrapper() SqlException: " + ex.InnerException, ContextName));
            }
            catch (Exception ex)
            {
                throw new DataException(String.Format("{0}.ExecuteWrapper() Exception: " + ex.InnerException, ContextName), ex);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        /// <summary>
        /// Encapsula las llamadas a la base de datos, generando una conexión asincrona a la misma
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="conn"></param>
        /// <param name="func"></param>
        /// <param name="ContextName"></param>
        /// <returns></returns>
        public static async Task<TResult> ExecuteWrapperAsynConn<TResult>(string conn, DataBaseType motor, Func<IDbConnection, Task<TResult>> func, string ContextName = "DapperBasic")
        {
            TResult result;
            try
            {
                switch (motor)
                {
                    case DataBaseType.SqlServer:
                        using (var connection = new SqlConnection(conn))
                        {
                            await connection.OpenAsync();
                            result = await func(connection);
                        }
                        break;
                    case DataBaseType.Postgresql: 
                        using (var connection = new NpgsqlConnection(conn))
                        {
                            await connection.OpenAsync();
                            result = await func(connection);
                        }
                        break;
                    default: throw new ArgumentException("Motor no identificado");
                }
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException(String.Format("{0}.ExecuteWrapper() TimeOut: {1}", ContextName, ex.Message), ex);
            }
            catch (SqlException ex)
            {
                throw new EvaluateException(String.Format("{0}.ExecuteWrapper() SqlException: {1}", ContextName, ex.Message), ex);
            }
            catch (NpgsqlException ex)
            {
                throw new EvaluateException(String.Format("{0}.ExecuteWrapper() NpgsqlException: {1}", ContextName, ex.Message), ex);
            }
            catch (Exception ex)
            {
                throw new DataException(String.Format("{0}.ExecuteWrapper() Exception: {1}", ContextName, ex.Message), ex);
            }

            return result;
        }
    }
}
