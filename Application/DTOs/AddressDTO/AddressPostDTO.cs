using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.AddressDTO
{
    public class AddressPostDTO
    {
        public string City { get; set; }
        public int CountryStateId { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
        public string LocalNo { get; set; }
    }
}
