using DynatronWebApi.UoW;
using MediatR;
using System.Transactions;

namespace DynatronWebApi.Behaviours;

/// <summary>
///     Unit of work behavior
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public class UnitOfWorkBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<UnitOfWorkBehavior<TRequest, TResponse>> _logger;
    private readonly IUnitOfWork _uow;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="uow"></param>
    public UnitOfWorkBehavior(ILogger<UnitOfWorkBehavior<TRequest, TResponse>> logger, IUnitOfWork uow)
    {
        _uow = uow;
        _logger = logger;
    }

    /// <summary>
    ///     Handle request
    /// </summary>
    /// <param name="request"></param>
    /// <param name="next"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (IsNotCommand()) return await next();
        using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var response = await next();
        await _uow.Commit();
        transactionScope.Complete();
        return response!;
    }

    private static bool IsNotCommand()
    {
        return !typeof(TRequest).Name.Contains("Command");
    }
}