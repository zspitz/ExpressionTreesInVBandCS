using System;
using System.Collections.Generic;

namespace Shared {
    public class Person {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IEnumerable<Trip> Trips { get; set; }
        public void Write() => Console.WriteLine($"LastName: {LastName}, FirstName: {FirstName}");
    }

    public class Trip {
        public long Budget { get; set; }
    }
}
