using Carter;
using DynatronWebApi.Database;
using DynatronWebApi.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NotFoundExceptionDynatron = DynatronWebApi.Exceptions.NotFoundException;

namespace DynatronWebApi.Features.Customers.Queries
{
    public static class GetCustomerById
    {
        public record Query(Guid Id) : IRequest<CustomerResponse>;

        internal sealed class Handler(ApplicationDbContext dbContenxt) : IRequestHandler<Query, CustomerResponse>
        {
            public async Task<CustomerResponse> Handle(Query request, CancellationToken cancellationToken)
            {
                var customer = await dbContenxt.Customers.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (customer == null)
                {
                    throw new NotFoundExceptionDynatron("Customer", request.Id);
                }
                var customerResponse = customer.Adapt<CustomerResponse>();
                return customerResponse;
            }
        }
    }

    public class CustomerByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/customers/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetCustomerById.Query(id));
                return Results.Ok(result);
            });
        }
    }
}