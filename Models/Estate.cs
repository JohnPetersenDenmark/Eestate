using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eestate.Models
{
    public class Estate
    {
        public int Id { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string RegistrationNumber { get; set; }

        public string EstateType { get; set; }
        public string Liggetid { get; set; }
        public Decimal Price { get; set; }
        public Decimal EjerudgiftPrMd { get; set; }
        public Decimal PrisPrM2 { get; set; }
        public string Areal { get; set; }
        public string VaegtetAreal { get; set; }
        public string GrundAreal { get; set; }
       
        public int OwnerIdentityUserIds { get; set; }
        public int BuyerIdentityUserIds { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
