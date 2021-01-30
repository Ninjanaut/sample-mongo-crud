The purpose of the application is to be "cheat sheet" to MongoDB CRUD operations in C#.

*Connect (without connection string parameter, localhost is the default)*
```
var Client = new MongoClient("mongodb://localhost:27017");
```

*Get database*
```
IMongoDatabase Database = Client.GetDatabase("ApplicationOne");
```

*Create collection. It will also create a database if not exist.*
```
Database.CreateCollection("Customer");
```

*Get collection*
```
IMongoCollection<Customer> _customers = 
  Database.GetCollection<Customer>("Customer");
```

*Prepare custom object*
```
var customer = new Customer("John", "Doe");
```

*Add object*
```
_customers.InsertOne(customer); 
```

*Update object*
```
customer.AddTelephone("+123 456 789 012");
_customers.ReplaceOne(x => x.Id == customer.Id, customer);
```

*Delete object*
```
_customers.DeleteOne(x => x.Id == customer.Id);
```

*Delete all objects in collection*
```
_customers.DeleteMany(x => true);
```

*Receive all objects in collection*
```
var retrievedCustomers = _customers.Find(x => true).ToList();
```

*Receive specific object in collection*
```
var customer =
    _customers
      .Find(x => x.Id == customer.Id)
      .SingleOrDefault();
```