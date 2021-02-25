using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eestate.ViewModels
{
    public class UploadFileViewModel
    {

        //public int Id { get; set; }
        //public int EstateId { get; set; }

        public IFormFile uploadedFile { get; set; }
    }
}
