using MongoDB.Driver;
using Xunit;

namespace Mongo.Tests
{
    // https://xunit.net/docs/shared-context

    public class DatabaseTests : IClassFixture<DatabaseFixture>
    {
        IMongoCollection<Customer> _customers;

        public DatabaseTests(DatabaseFixture fixture)
        {
            _customers = fixture.Database.GetCollection<Customer>("Customer");
        }

        [Fact]
        public void Insert_Should_Succeed()
        {
            // Arrange
            var customer = new Customer("John", "Doe");

            // Act
            _customers.InsertOne(customer);

            // Assert
            var insertedCustomer =
                _customers
                    .Find(x => x.Id == customer.Id)
                    .SingleOrDefault();

            Assert.NotNull(insertedCustomer);
            Assert.Equal(customer.LastName, insertedCustomer.LastName);
            Assert.Equal(customer.LastName, insertedCustomer.LastName);
        }

        [Fact]
        public void Update_Should_Succeed()
        {
            // Arrange
            var customer = new Customer("John", "Doe");

            _customers.InsertOne(customer);

            // Act
            customer.AddTelephone("+123 456 789 012");

            _customers.ReplaceOne(x => x.Id == customer.Id, customer);

            // Assert
            var updatedCustomer =
                _customers
                    .Find(x => x.Id == customer.Id)
                    .SingleOrDefault();

            Assert.NotNull(updatedCustomer);
            Assert.Equal(customer.FirstName, updatedCustomer.FirstName);
            Assert.Equal(customer.LastName, updatedCustomer.LastName);
            Assert.NotNull(updatedCustomer.Telephones);
            Assert.Contains(customer.Telephones[0], updatedCustomer.Telephones);
        }

        [Fact]
        public void Delete_Should_Succeed()
        {
            // Arrange
            var customer = new Customer("John", "Doe");

            _customers.InsertOne(customer);

            // Act
            _customers.DeleteOne(x => x.Id == customer.Id);

            // Assert
            var deletedCustomer =
                _customers
                    .Find(x => x.Id == customer.Id)
                    .SingleOrDefault();

            Assert.Null(deletedCustomer);
        }

        [Fact]
        public void FindAll_Should_Succeed()
        {
            // Arrange
            _customers.DeleteMany(x => true);
            var customerOne = new Customer("John", "Doe");
            var customerTwo = new Customer("Jane", "Doe");

            _customers.InsertOne(customerOne);
            _customers.InsertOne(customerTwo);

            // Act
            var retrievedCustomers = _customers.Find(x => true).ToList();

            // Assert
            Assert.NotNull(retrievedCustomers);
            Assert.Equal(2, retrievedCustomers.Count);
        }
    }
}
