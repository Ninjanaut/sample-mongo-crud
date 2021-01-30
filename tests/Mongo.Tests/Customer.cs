using System;
using System.Collections.Generic;

namespace Mongo.Tests
{
    public class Customer
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public List<string> Telephones { get; private set; }

        public Customer(string firstName, string lastName)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Telephones = new List<string>();
        }

        public void AddTelephone(string telephone)
        {
            Telephones.Add(telephone);
        }
    }
}
