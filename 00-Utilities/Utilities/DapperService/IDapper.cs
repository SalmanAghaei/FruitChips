using Dapper;
using System.Data;
using System.Data.Common;

namespace Utilities.DapperService
{
    public interface IDapper : IDisposable
    {
        DbConnection GetDbconnection();
        T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        (List<T> list, int totalCount) GetAll<T>(string sp, DynamicParameters parms, bool needTotalCount = false, string totalCountQuery = "", CommandType commandType = CommandType.Text);
        Task<(List<T> list, int totalCount)> GetAllAsync<T>(string sp, DynamicParameters parms, bool needTotalCount = false, string totalCountQuery = "", CommandType commandType = CommandType.Text);
        List<T> GetAllFromFile<T>(string queryFileName, DynamicParameters parms, CommandType commandType = CommandType.Text);
        int Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        T Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        T Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
    }
}
