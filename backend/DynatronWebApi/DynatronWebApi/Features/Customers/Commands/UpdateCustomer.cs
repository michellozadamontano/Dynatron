using Carter;
using DynatronWebApi.Database;
using DynatronWebApi.Dtos;
using FluentValidation;
using MediatR;
using NotFoundExceptionDynatron = DynatronWebApi.Exceptions.NotFoundException;
using ValidationExceptionDynatron = DynatronWebApi.Exceptions.ValidationException;

namespace DynatronWebApi.Features.Customers.Commands
{
    public static class UpdateCustomer
    {
        /// <summary>
        /// Represents a command to update a customer.
        /// </summary>
        /// <remarks>
        /// This class is used to encapsulate the data needed to update a customer.
        /// It is a record type, which means it is immutable and has value-based equality semantics.
        /// </remarks>
        public record Command(
            /// <summary>
            /// The ID of the customer to be updated.
            /// </summary>
            Guid Id,

            /// <summary>
            /// The updated customer data.
            /// </summary>
            CustomerRequest Customer
        ) : IRequest<Unit>;

        /// <summary>
        /// Validates the UpdateCustomer command.
        /// </summary>
        public class Validator : AbstractValidator<Command>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Validator"/> class.
            /// </summary>
            public Validator()
            {
                // FirstName must not be empty
                RuleFor(x => x.Customer.FirstName)
                    .NotEmpty()
                    .WithMessage("FirstName cannot be empty");

                // LastName must not be empty
                RuleFor(x => x.Customer.LastName)
                    .NotEmpty()
                    .WithMessage("LastName cannot be empty");

                // Email must not be empty and must be a valid email address
                RuleFor(x => x.Customer.Email)
                    .NotEmpty()
                    .WithMessage("Email cannot be empty")
                    .EmailAddress()
                    .WithMessage("Email must be a valid email address");
            }
        }

        /// <summary>
        /// Handler for updating a customer.
        /// </summary>
        internal sealed class Handler(ApplicationDbContext dbContext, IValidator<Command> validator, ILogger<Handler> logger)
       : IRequestHandler<Command, Unit>
        {
            /// <summary>
            /// Handles the request to update a customer.
            /// </summary>
            /// <param name="request">The update request.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A task representing the completion of the request.</returns>
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                // Validate the request
                var validationResult = await validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                {
                    logger.LogError("Update Customer failed : {Errors}", validationResult.Errors);
                    throw new ValidationExceptionDynatron("Incorrect data", validationResult);
                }
                // Find the customer to update
                var customer = await dbContext.Customers.FindAsync(request.Id);

                if (customer == null)
                {
                    throw new NotFoundExceptionDynatron("Customer", request.Id);
                }

                customer.Update(request.Customer.FirstName, request.Customer.LastName, request.Customer.Email);

                return Unit.Value;
            }
        }
    }

    /// <summary>
    /// Represents an endpoint for updating a customer.
    /// </summary>
    public class UpdateCustomerEndpoint : ICarterModule
    {
        /// <summary>
        /// Adds routes to the specified endpoint route builder.
        /// </summary>
        /// <param name="app">The endpoint route builder.</param>
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            // Map a PUT request to update a customer with the specified ID
            app.MapPut("api/customers/{id}", async (Guid id, CustomerRequest request, ISender sender) =>
            {
                // Send a command to update the customer with the specified ID
                var result = await sender.Send(new UpdateCustomer.Command(id, request));

                // Return the result as an OK response
                return Results.Ok(result);
            });
        }
    }
}