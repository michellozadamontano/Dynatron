using DynatronWebApi.Database;
using DynatronWebApi.Entities;

namespace DynatronWebApi.Seeding
{
    public class SeedData
    {
        private readonly ApplicationDbContext _dbContext;

        public SeedData(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            SeedCustomers();
        }

        private void SeedCustomers()
        {
            if (!_dbContext.Customers.Any())
            {
                var customers = new[]
                {
                    new Customer("John", "Doe", "john@example"),
                    new Customer("Jane", "Smith", "jane.smith@example.com"),
                    new Customer("Michael", "Johnson", "michael.johnson@example.com"),
                    new Customer("Emily", "Brown", "emily.brown@example.com"),
                    new Customer("David", "Williams", "david.williams@example.com"),
                    new Customer("Sophia", "Taylor", "sophia.taylor@example.com"),
                    new Customer("Daniel", "Lee", "daniel.lee@example.com"),
                    new Customer("Olivia", "Harris", "olivia.harris@example.com"),
                    new Customer("James", "Martin", "james.martin@example.com"),
                    new Customer("Mia", "Clark", "mia.clark@example.com"),
                    new Customer("Ethan", "Lewis", "ethan.lewis@example.com"),
                    new Customer("Isabella", "Walker", "isabella.walker@example.com"),
                    new Customer("Matthew", "Hall", "matthew.hall@example.com"),
                    new Customer("Ava", "Allen", "ava.allen@example.com"),
                    new Customer("Noah", "Young", "noah.young@example.com")
                };
                _dbContext.Customers.AddRange(customers);
                _dbContext.SaveChanges();
            }
        }
    }
}