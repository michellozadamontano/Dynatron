using Carter;
using DynatronWebApi.Database;
using DynatronWebApi.Dtos;
using DynatronWebApi.Entities;
using FluentValidation;
using Mapster;
using MediatR;
using ValidationExceptionDynatron = DynatronWebApi.Exceptions.ValidationException;

namespace DynatronWebApi.Features.Customers.Commands
{
    public static class CreateCustomer
    {
        public record Command(
            string FirstName,
            string LastName,
            string Email
            ) : IRequest<Unit>;

        /// <summary>
        ///     Validator
        /// </summary>
        public class Validator : AbstractValidator<Command>
        {
            /// <summary>
            ///     Validator Constructor
            /// </summary>
            public Validator()
            {
                RuleFor(x => x.FirstName).NotEmpty();
                RuleFor(x => x.LastName).NotEmpty();
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
            }
        }

        public sealed class Handler(ApplicationDbContext dbContext, IValidator<Command> validator, ILogger<Handler> logger)
       : IRequestHandler<Command, Unit>
        {
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = await validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                {
                    logger.LogError("Create Customer failed : {Errors}", validationResult.Errors);
                    throw new ValidationExceptionDynatron("Incorrect data", validationResult);
                }
                var customer = new Customer(request.FirstName, request.LastName, request.Email);

                await dbContext.Customers.AddAsync(customer, cancellationToken);

                return Unit.Value;
            }
        }
    }

    public class CreateCustomerEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/customers", async (CustomerRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateCustomer.Command>();
                var result = await sender.Send(command);
                return Results.Ok(result);
            });
        }
    }
}