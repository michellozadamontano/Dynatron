using Carter;
using DynatronWebApi.Database;
using DynatronWebApi.Dtos;
using DynatronWebApi.Wrappers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DynatronWebApi.Features.Customers.Queries
{
    public static class GetCustomerByNameOrEmail
    {
        public record Query(string NameOrEmail, int PageNumber, int PageSize) : IRequest<Pagination<CustomerResponse>>;

        internal sealed class Handler(ApplicationDbContext dbContext) : IRequestHandler<Query, Pagination<CustomerResponse>>
        {
            public async Task<Pagination<CustomerResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = dbContext.Customers.Where(x => x.FirstName.Contains(request.NameOrEmail) || x.LastName.Contains(request.NameOrEmail) || x.Email.Contains(request.NameOrEmail));
                var totalCount = await query.CountAsync();

                // Ensure valid pageNumber
                var pageNumber = Math.Max(1, Math.Min(request.PageNumber, (int)Math.Ceiling((double)totalCount / request.PageSize)));

                // Calculate skip
                var skip = (pageNumber - 1) * request.PageSize;

                var results = await query
                .Skip(skip)
                .Take(request.PageSize)
                .Select(x => new CustomerResponse(
                    x.Id,
                    x.FirstName,
                    x.LastName,
                    x.Email,
                    x.Created,
                    x.LastUpdated
                    )).ToListAsync();

                var totalPages = (int)Math.Ceiling((double)totalCount / request.PageSize);
                return new Pagination<CustomerResponse>
                {
                    CurrentPage = pageNumber,
                    PageSize = request.PageSize,
                    TotalPages = totalPages,
                    TotalItems = totalCount,
                    Result = results
                };
            }
        }
    }

    public class FilterCustomersEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/customers-filter", async (string nameOrEmail, int pageNumber, int pageSize, ISender sender) =>
            {
                var result = await sender.Send(new GetCustomerByNameOrEmail.Query(nameOrEmail, pageNumber, pageSize));
                return Results.Ok(result);
            });
        }
    }
}