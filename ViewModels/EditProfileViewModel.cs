using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eestate.ViewModels
{
    public class EditProfileViewModel
    {
        public int Id { get; set; }
        public string IdentityUserId { get; set; }

        public string Email { get; set; }
        public string FullName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }

        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
    }
}
