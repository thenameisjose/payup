using System;
using System.Collections.Generic;
namespace payupApi.Domain
{
    public class Dependent
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Relationship { get; set; }
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}