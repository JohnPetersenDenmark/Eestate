using Eestate.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
    public class DownloadFileController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IWebHostEnvironment webEnviroment;

        public DownloadFileController(AppDbContext context, IWebHostEnvironment webEnviroment)
        {
            this.context = context;
            this.webEnviroment = webEnviroment;
        }


        [Authorize]
        [HttpGet]
        [Route("[action]")]
        [Produces("application/json")]
        public  List<FileAttachment> GetUploadedFilesByEstateId(int estateId)
        {

          // var x =   context.FileAttachments.Where(file => file.EstateId == estateId).ToList();

            var x = context.FileAttachments.ToList();
            return x; ;
        }

       
        [HttpGet]
        [Route("[action]")]
       
        public async Task<ActionResult> downLoadFile(int fileId)
        {
            FileAttachment attachment = await context.FileAttachments.FindAsync(fileId);

            if (attachment != null)
            {
                string uploadFolder = Path.Combine(webEnviroment.ContentRootPath, "UploadedFiles");
                string uniqueFileName = attachment.UniqueFileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);

                if(System.IO.File.Exists(filePath))
                {
                     byte[]  content= await System.IO.File.ReadAllBytesAsync(filePath);

                    // var contentType = "APPLICATION/octet-stream";
                    var contentType = "application/force-download";
                   
                    var fileName = "something.txt";
                    return File(content, contentType, attachment.UniqueFileName);
                }           
            }

            return null;
         
        }

        [HttpGet]
        [Route("[action]")]

        public async Task<ActionResult> showFile(int fileId)
        {
            FileAttachment attachment = await context.FileAttachments.FindAsync(fileId);

            if (attachment != null)
            {
                string uploadFolder = Path.Combine(webEnviroment.ContentRootPath, "UploadedFiles");
                string uniqueFileName = attachment.UniqueFileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);

                if (System.IO.File.Exists(filePath))
                {
                    byte[] content = await System.IO.File.ReadAllBytesAsync(filePath);
                
                    return File(content, attachment.ContentType);
                }
            }

            return null;

        }

    }
}
