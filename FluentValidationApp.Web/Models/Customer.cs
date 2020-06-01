using System;
using System.Collections;
using System.Collections.Generic;

namespace FluentValidationApp.Web.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public DateTime? BirthDay { get; set; }
        public List<Address> Addresses { get; set; } //= new List<Address>();
        public Gender Gender { get; set; }
    }
}