using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Models.Dtos
{
    public class EmailDto
    {
        public string FromEmail { get; set; }
        public string PhoneNumber { get; set; }
        public string Body { get; set; }
    }

    public class EmailResponse : Response
    {
    }
}
