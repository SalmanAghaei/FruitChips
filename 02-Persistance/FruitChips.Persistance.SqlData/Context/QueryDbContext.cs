using Microsoft.EntityFrameworkCore;
using Persistance.SqlData.Context;

namespace FruitChips.Persistance.SqlData.Context
{
    public class QueryDbContext : BaseQueryDbContext
    {
        public QueryDbContext(DbContextOptions<QueryDbContext> options) : base(options)
        {
        }
    }

    
}
