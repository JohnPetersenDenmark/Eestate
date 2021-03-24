using Eestate.Models;
using Eestate.ViewModels;
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
        public  List<FileAttachmentViewModel> GetUploadedFilesByEstateId(int estateId)
        {

            List<FileAttachmentViewModel> model = new List<FileAttachmentViewModel>();

            List<FileAttachment> fileAttachmentList = context.FileAttachments.Where(attachment => attachment.EstateId == estateId).ToList();

            foreach (var fileAttachment in fileAttachmentList)
            {
                FileAttachmentViewModel fileAttachmentModel = new FileAttachmentViewModel();

                fileAttachmentModel.Id = fileAttachment.Id;
                fileAttachmentModel.EstateId = fileAttachment.EstateId;
                fileAttachmentModel.ProfileId = fileAttachment.ProfileId;
                fileAttachmentModel.FileCategory = fileAttachment.FileCategory;
                fileAttachmentModel.DocumentTypeId = fileAttachment.DocumentTypeId;
                fileAttachmentModel.OriginalFileName = fileAttachment.OriginalFileName;
                fileAttachmentModel.UniqueFileName = fileAttachment.UniqueFileName;
                fileAttachmentModel.ContentType = fileAttachment.ContentType;
               
                if (fileAttachment.DocumentTypeId != 0)
                {
                    DocumentType docType = context.DocumentTypes.Find(fileAttachment.DocumentTypeId);
                    if (docType != null)
                    {
                        fileAttachmentModel.DocumentTypeHelpText = docType.HelpText;
                        fileAttachmentModel.DocumentDescription = docType.Description;
                        fileAttachmentModel.Category = docType.Category;
                    }
                }

                model.Add(fileAttachmentModel);
            }
                return model;
        }

       
        [HttpGet]
        [Route("[action]")]
       
        public async Task<ActionResult> downLoadFile(int fileId)
        {
            FileAttachment attachment = await context.FileAttachments.FindAsync(fileId);

            if (attachment != null)
            {
                string uploadFolder = Path.Combine(webEnviroment.ContentRootPath, "Services/UploadedFiles");
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

        public async Task<List<DocumentTypeViewModel>> documentTypes(int fileId)
        {
            List<DocumentType> docTypes = context.DocumentTypes.ToList();

            List<DocumentTypeViewModel> model = new List<DocumentTypeViewModel>();

            foreach( var docType in docTypes)
            {
                DocumentTypeViewModel docTypeModel = new DocumentTypeViewModel();
                docTypeModel.Id = docType.Id.ToString();
                docTypeModel.Description = docType.Description;
                docTypeModel.HelpText = docType.HelpText;

                model.Add(docTypeModel);
            }

            return model;

        }

        [HttpGet]
        [Route("[action]")]

        public async Task<ActionResult> showFile(int fileId)
        {
            FileAttachment attachment = await context.FileAttachments.FindAsync(fileId);

            if (attachment != null)
            {
                string uploadFolder = Path.Combine(webEnviroment.ContentRootPath, "Services/UploadedFiles");
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

        [HttpGet]
        [Route("[action]")]
        [Produces("application/json")]

        public async Task<ActionResult> deleteFile(int fileId)
        {
            FileAttachment attachment = await context.FileAttachments.FindAsync(fileId);

            if (attachment != null)
            {
                string uploadFolder = Path.Combine(webEnviroment.ContentRootPath, "Services/UploadedFiles");
                string uniqueFileName = attachment.UniqueFileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);                   
                }

                context.FileAttachments.Remove(attachment);
                return StatusCode(StatusCodes.Status200OK);
            }

            return StatusCode(StatusCodes.Status200OK);

        }
    }
}
