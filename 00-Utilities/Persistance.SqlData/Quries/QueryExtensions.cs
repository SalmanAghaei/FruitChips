using Core.Application.Queries;

namespace Persistance.SqlData.Quries
{
    public static class QueryExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="tableName">Table Name</param>
        /// <returns></returns>
        public static string GetAllQueryGenerate(this IPageQuery input, string tableName, string condition)
        {

            var query = $"select * from {tableName} where Isdeleted=0 {condition}  {input.SetOrderBy()}  {input.SetSkipAndTakeQuery()}" ;

            return query;
        }


        public static string GetTableCount(this IPageQuery query,string tableName,string condition="")
        {
            return $"select count(id) from {tableName} where  Isdeleted=0 {condition}";
        }
        public static string SetSkipAndTakeQuery(this IPageQuery pageQuery)
        {
            var str = $" OFFSET {pageQuery.SkipCount} ROWS  FETCH NEXT {pageQuery.PageSize} ROWS ONLY";
            return str;
        }

        public static string SetOrderBy(this IPageQuery pageQuery)
        {
            string sortByAsendingOrDesnding = pageQuery.SortAscending ? $" asc" : " desc";
            string orderByType = string.IsNullOrEmpty(pageQuery.SortBy) ? $" order by Id {sortByAsendingOrDesnding} " : $" order by {pageQuery.SortBy}  {sortByAsendingOrDesnding} ";
            return orderByType;
        }
    }
}
