using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDB.Layout
{
    [Table("Footer")]
    public class Footer : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string LogoURL { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }


        public Address Address { get; set; }
        public int AddressId { get; set; }
        public List<Link> Links { get; set; }
        public List<SocialMedia> SocialMedia { get; set; }


    }
}
