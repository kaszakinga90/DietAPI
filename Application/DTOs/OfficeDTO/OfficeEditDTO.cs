using Application.DTOs.AddressDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.OfficeDTO
{
    public class OfficeEditDTO
    {
        public int Id { get; set; }
        public string OfficeName { get; set; }
        public int AddressId { get; set; }
        public AddressesDTO AddressDTO { get; set; }
    }
}
