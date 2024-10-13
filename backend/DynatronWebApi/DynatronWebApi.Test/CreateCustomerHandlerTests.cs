using DynatronWebApi.Behaviours;
using DynatronWebApi.Database;
using DynatronWebApi.Features.Customers.Commands;
using DynatronWebApi.UoW;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shouldly;

namespace DynatronWebApi.Test
{
    public class CreateCustomerHandlerTests
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Mock<IValidator<CreateCustomer.Command>> _mockValidator;

        //private readonly Mock<ILogger<CreateCustomer.Handler>> _mockLogger;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        private readonly IMediator _mediator;

        public CreateCustomerHandlerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _dbContext = new ApplicationDbContext(options);
            _mockValidator = new Mock<IValidator<CreateCustomer.Command>>();
            //_mockLogger = new Mock<ILogger<CreateCustomer.Handler>>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();

            var services = new ServiceCollection();
            services.AddLogging();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCustomer.Handler).Assembly));
            services.AddTransient(provider => _dbContext);
            services.AddTransient(_ => _mockValidator.Object);
            //services.AddTransient(_ => _mockLogger.Object);
            services.AddTransient(_ => _mockUnitOfWork.Object);
            services.AddTransient<CreateCustomer.Handler>();
            services.AddTransient<IUnitOfWork>(provider => _mockUnitOfWork.Object);

            // Register the UnitOfWorkBehavior
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));
            var serviceProvider = services.BuildServiceProvider();

            _mediator = serviceProvider.GetRequiredService<IMediator>();
        }

        [Fact]
        public async Task Handle_ShouldCreateCustomer_WhenValidationSucceeds()
        {
            // Arrange
            var command = new CreateCustomer.Command("John", "Doe", "john.doe@example.com");
            _mockValidator.Setup(v => v.ValidateAsync(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());

            // Act
            await _mediator.Send(command);

            // Assert
            _dbContext.Customers.ShouldContain(c => c.FirstName == "John" && c.LastName == "Doe" && c.Email == "john.doe@example.com");
        }
    }
}