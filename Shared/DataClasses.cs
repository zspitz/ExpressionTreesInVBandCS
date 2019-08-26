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

        /// <summary>OData Persons don't have a DateOfBirth property</summary>
        public void Write(bool withDateOfBirth = false) {
            var toWrite = 
                withDateOfBirth ? $"LastName: {LastName}, FirstName: {FirstName}, DOB: {DateOfBirth:D}" :
                $"LastName: {LastName}, FirstName: {FirstName}";
            Console.WriteLine(toWrite);
        }
    }

    public class Trip {
        public long Budget { get; set; }
    }
}
