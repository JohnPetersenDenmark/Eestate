using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eestate.ViewModels
{
    public class FileAttachmentViewModel
    {
        public int Id { get; set; }
        public int EstateId { get; set; }

        public int ProfileId { get; set; }

        public int DocumentTypeId { get; set; }
        public string DocumentDescription { get; set; }
        public string DocumentTypeHelpText { get; set; }

        public string UniqueFileName { get; set; }

        public string OriginalFileName { get; set; }
        public string ContentType { get; set; }
    }
}
