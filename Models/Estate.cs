using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eestate.Models
{
    public class Estate
    {
        public int Id { get; set; }
        public string RegistrationNumber { get; set; }
       
        public int OwnerIdentityUserIds { get; set; }
        public int BuyerIdentityUserIds { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
