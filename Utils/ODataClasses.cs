using System;
using System.Collections.Generic;

namespace Utils {
    public class Person {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public ICollection<Trip> Trips { get; set; }
        public void Write() => Console.WriteLine($"LastName: {LastName}, FirstName: {FirstName}");
    }

    public class Trip {
        public long Budget { get; set; }
    }
}
