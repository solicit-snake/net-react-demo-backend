using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net_react_demo_backend.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public string Street { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }
    }
}
