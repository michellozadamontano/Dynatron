using Carter;
using DynatronWebApi.Database;
using MediatR;
using NotFoundExceptionDynatron = DynatronWebApi.Exceptions.NotFoundException;

namespace DynatronWebApi.Features.Customers.Commands
{
    /// <summary>
    /// Represents a command to delete a customer.
    /// </summary>
    public static class DeleteCustomer
    {
        /// <summary>
        /// Represents a command to delete a customer.
        /// </summary>
        /// <param name="Id">The ID of the customer to delete.</param>
        public record Command(Guid Id) : IRequest<Unit>;

        /// <summary>
        /// Handler for deleting a customer.
        /// </summary>
        internal sealed class Handler(ApplicationDbContext dbContext) : IRequestHandler<Command, Unit>
        {
            /// <summary>
            /// Handles the request to delete a customer.
            /// </summary>
            /// <param name="request">The delete request.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A task representing the completion of the request.</returns>
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                // Find the customer to delete
                var customer = await dbContext.Customers.FindAsync(request.Id);
                if (customer == null)
                {
                    // Throw an exception if the customer is not found
                    throw new NotFoundExceptionDynatron("Customer", request.Id);
                }

                // Remove the customer from the database
                dbContext.Customers.Remove(customer);

                return Unit.Value;
            }
        }
    }

    public class DeleteCustomerEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("api/customers/{id}", async (Guid id, ISender sender) =>
            {
                var command = new DeleteCustomer.Command(id);

                var result = await sender.Send(command);

                return Results.Ok(result);
            });
        }
    }
}