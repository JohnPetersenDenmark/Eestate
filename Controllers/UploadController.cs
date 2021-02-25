using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Eestate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> OnPostUpload()
        {
            var formCollection = await Request.ReadFormAsync();
            var file = formCollection.Files.First();

         

            //long size = files.Sum(f => f.Length);

            //foreach (var formFile in files)
            //{
            //    if (formFile.Length > 0)
            //    {
            //        var filePath = Path.GetTempFileName();

            //        using (var stream = System.IO.File.Create(filePath))
            //        {
            //            await formFile.CopyToAsync(stream);
            //        }
            //    }
            //}

            //// Process uploaded files
            //// Don't rely on or trust the FileName property without validation.

            //return Ok(new { count = files.Count, size });

            return Ok();
        }
    }
}
