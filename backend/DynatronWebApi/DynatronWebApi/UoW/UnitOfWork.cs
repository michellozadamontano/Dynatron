using DynatronWebApi.Database;
using Microsoft.EntityFrameworkCore.Storage;

namespace DynatronWebApi.UoW
{
    public class UnitOfWork(ApplicationDbContext context) : IDisposable, IUnitOfWork
    {
        /// <summary>
        ///     Rollback method
        /// </summary>
        public void Dispose()
        {
            context.Dispose();
        }

        /// <summary>
        ///     ApplicationDbContext
        /// </summary>
        /// <returns></returns>
        public async Task<IDbContextTransaction> BeginTransaction()
        {
            return await context.Database.BeginTransactionAsync();
        }

        /// <summary>
        ///     Commit method
        /// </summary>
        public Task<int> Commit()
        {
            return context.SaveChangesAsync();
        }
    }
}