using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eestate.ViewModels
{
    public class EstateViewModel
    {
        public string Id { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string RegistrationNumber { get; set; }

        public string EstateType { get; set; }
        public string Liggetid { get; set; }
        public string Price { get; set; }
        public string EjerudgiftPrMd { get; set; }
        public string PrisPrM2 { get; set; }
        public string Areal { get; set; }
        public string VaegtetAreal { get; set; }
        public string GrundAreal { get; set; }
        public string ThumbNailFilePathAndName { get; set; }
        public string OwnerIdentityUserIds { get; set; }
        public string BuyerIdentityUserIds { get; set; }

        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
       
    }
}
