using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eestate.ViewModels
{
    public class EstateViewModel
    {
        public string Id { get; set; }
        public string RegistrationNumber { get; set; }

        public string OwnerIdentityUserIds { get; set; }
        public string BuyerIdentityUserIds { get; set; }

        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
    }
}
