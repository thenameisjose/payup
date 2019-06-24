using System;
using System.Collections.Generic;
namespace payupApi.Models
{
    public class DependentVM
    {
        public Guid? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Relationship { get; set; }
    }
}