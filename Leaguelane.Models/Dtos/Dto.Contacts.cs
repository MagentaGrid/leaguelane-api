using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Models.Dtos
{
    public class ContactDto
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string MapEmbedUrl { get; set; }
        public string ImageUrl { get; set; }
    }

    public class ContactsResponse : Response
    {
        public ContactDto? Contacts { get; set; }
    }
}
