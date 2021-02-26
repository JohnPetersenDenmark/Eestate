﻿using Eestate.Models;
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
    [Authorize]
    public class UploadController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment webEnviroment;

        public UploadController(AppDbContext context, IWebHostEnvironment webEnviroment)
        {
            _context = context;
            this.webEnviroment = webEnviroment;
        }


        [HttpPost]
        [Authorize]
        [Route("[action]")]
        [Produces("application/json")]
        public async Task<ActionResult> uploadFile()
        {
            var formCollection = await Request.ReadFormAsync();

            var file = formCollection.Files.First();

            if (file == null)
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }


            string uploadFolder = Path.Combine(webEnviroment.ContentRootPath, "UploadedFiles");
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string filePath = Path.Combine(uploadFolder, uniqueFileName);
            file.CopyTo(new FileStream(filePath, FileMode.Create));


            string estateId = formCollection["EstateId"];
            string profileId = formCollection["profileId"];

            FileAttachment fileAttachment = new FileAttachment();
            fileAttachment.ProfileId = int.Parse(profileId);
            fileAttachment.EstateId = int.Parse(estateId);
            fileAttachment.UniqueFileName = uniqueFileName;

            _context.FileAttachments.Add(fileAttachment);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
