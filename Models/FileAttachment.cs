using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eestate.Models
{
    public class FileAttachment
    {
        public int Id { get; set; }
        public int EstateId { get; set; }

        public int ProfileId { get; set; }

        public string UniqueFileName { get; set; }       
    }
}
