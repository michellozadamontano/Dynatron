using Microsoft.EntityFrameworkCore.Storage;

namespace DynatronWebApi.UoW
{
    public interface IUnitOfWork
    {
        /// <summary>
        ///     BeginTransaction method
        /// </summary>
        /// <returns></returns>
        Task<IDbContextTransaction> BeginTransaction();

        /// <summary>
        ///     Commit method
        /// </summary>
        /// <returns></returns>
        Task<int> Commit();

        /// <summary>
        ///     Rollback method
        /// </summary>
        void Dispose();
    }
}