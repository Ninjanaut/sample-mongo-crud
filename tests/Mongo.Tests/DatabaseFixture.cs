using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;

namespace Mongo.Tests
{
    public class DatabaseFixture : IDisposable
    {
        public DatabaseFixture()
        {
            // http://mongodb.github.io/mongo-csharp-driver/2.0/reference/bson/mapping/
            BsonClassMap.RegisterClassMap<Customer>();

            var Client = new MongoClient("mongodb://localhost:27017");

            Database = Client.GetDatabase("DemoApplication");

            if (Database.GetCollection<BsonDocument>("Customer") != null)
            {
                Database.DropCollection("Customer");
            }

            // If database does not exist, it will be created with the collection.
            Database.CreateCollection("Customer");
        }

        public void Dispose()
        {
            // http://mongodb.github.io/mongo-csharp-driver/2.0/reference/driver/connecting/

            // It is recommended to store a MongoClient instance in a global place, 
            // either as a static variable or in an IoC container with a singleton lifetime.

            // A MongoClient object will be the root object. 
            // It is thread-safe and is all that is needed to handle connecting to servers

            // If the database contains only this collection, it will be also dropped.
            Database.DropCollection("Customer");
        }

        public IMongoDatabase Database { get; private set; }
    }
}
