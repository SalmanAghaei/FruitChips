using Dapper;
using Microsoft.AspNetCore.Hosting;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Utilities.DapperService;
public class Dapper : IDapper
{
    private readonly AppSettings _setting;
    private readonly IWebHostEnvironment _environment;

    public Dapper(AppSettings settings, IWebHostEnvironment environment)
    {
        _setting = settings;
        _environment = environment;
    }
    public void Dispose()
    {

    }

    public int Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
    {
        throw new NotImplementedException();
    }

    public T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
    {
        using IDbConnection db = GetDbconnection();
        return db.Query<T>(sp, parms, commandType: commandType).FirstOrDefault();
    }

    public (List<T> list, int totalCount) GetAll<T>(string sp, DynamicParameters parms, bool needTotalCount = false, string totalCountQuery = "", CommandType commandType = CommandType.Text)
    {
        using IDbConnection db = GetDbconnection();
        var result =db.Query<T>(sp, parms, commandType: commandType);
        var totalCount = needTotalCount ? Get<int>(totalCountQuery, null, commandType: CommandType.Text) : 0;
        return (result.ToList(), totalCount);
    }

    public async Task<(List<T> list,int totalCount)> GetAllAsync<T>(string sp, DynamicParameters parms,bool needTotalCount=false,string totalCountQuery="", CommandType commandType = CommandType.Text)
    {
        using IDbConnection db = GetDbconnection();
        var result = await db.QueryAsync<T>(sp, parms, commandType: commandType);
        var totalCount = needTotalCount ? Get<int>(totalCountQuery,null,commandType:CommandType.Text) : 0;
        return (result.ToList(),totalCount);
    }

    public List<T> GetAllFromFile<T>(string queryFileName, DynamicParameters parms, CommandType commandType = CommandType.Text)
    {
        var query = ReadQuery(queryFileName);
        using IDbConnection db = GetDbconnection();
        return db.Query<T>(query, parms, commandType: commandType).ToList();
    }
    private string ReadQuery(string queryFileName)
    {
        var rootPath = _environment.ContentRootPath;
        var sqlPath = Path.Combine(rootPath, $@"AppData\Sql\{queryFileName}.sql");
        using var stream = new StreamReader(sqlPath);
        return stream.ReadToEnd();
    }

    public DbConnection GetDbconnection()
    {
        return new SqlConnection(_setting.Dapper.DapperCnn);
    }

    public T Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
    {
        T result;
        using IDbConnection db = GetDbconnection();
        try
        {
            if (db.State == ConnectionState.Closed)
                db.Open();

            using var tran = db.BeginTransaction();
            try
            {
                result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (db.State == ConnectionState.Open)
                db.Close();
        }

        return result;
    }

    public T Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
    {
        T result;
        using IDbConnection db = GetDbconnection();
        try
        {
            if (db.State == ConnectionState.Closed)
                db.Open();

            using var tran = db.BeginTransaction();
            try
            {
                result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (db.State == ConnectionState.Open)
                db.Close();
        }

        return result;
    }
}
