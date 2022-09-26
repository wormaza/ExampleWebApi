using System.Reflection;
using Transversal.Util.BaseDBUp;

namespace Ejemplo.DBUp
{
    public class DBUpMSMigration : BaseDBUp
    {
        public DBUpMSMigration(string con, Assembly assembly, string pattern, bool? isrevert, bool? includedata, bool? includedevelop, bool? includestored, DataBaseType dataBaseType) : base(con, assembly, pattern, isrevert, includedata, includedevelop, includestored, dataBaseType) { }
        public DBUpMSMigration(string con, string path, string pattern, bool? isrevert, bool? includedata, bool? includedevelop, bool? includestored, DataBaseType dataBaseType) : base(con, path, pattern, isrevert, includedata, includedevelop, includestored, dataBaseType) { }
    }
}