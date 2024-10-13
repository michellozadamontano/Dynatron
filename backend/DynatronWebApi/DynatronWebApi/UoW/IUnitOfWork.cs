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
        Task<int> Commit(CancellationToken cancellationToken = default);

        /// <summary>
        ///     Rollback method
        /// </summary>
        void Dispose();
    }
}