using System;
using System.Collections.Generic;
namespace payupApi.Domain
{
    public class Employee
    {
        public Employee()
        {
            Dependents = new List<Dependent>();
        }
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Dependent> Dependents { get; set; }
    }
}