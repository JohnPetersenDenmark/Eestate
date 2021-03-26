using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eestate.Models;
using Eestate.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net.Http;

namespace Eestate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EstatesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment webEnviroment;

        public EstatesController(AppDbContext context, IWebHostEnvironment webEnviroment)
        {
            _context = context;
            this.webEnviroment = webEnviroment;
        }

        


        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<List<EstateViewModel>>> GetEstates()
        {
 
            List<Estate> estateList = _context.Estates.ToList();

            List<EstateViewModel> estateModels = new List<EstateViewModel>();

            foreach (var estate in estateList)
            {
                EstateViewModel model = new EstateViewModel();
                model.Id = estate.Id.ToString();
                model.RegistrationNumber = estate.RegistrationNumber;
                model.Price = estate.Price.ToString();
                model.Address1 = estate.Address1;
                model.Zip = estate.Zip;
                model.City = estate.City;
                model.Areal = estate.Areal;
                model.VaegtetAreal = estate.VaegtetAreal;
                model.GrundAreal = estate.GrundAreal;
                model.EstateType = estate.EstateType;
                model.OwnerIdentityUserIds = estate.OwnerIdentityUserIds.ToString();
                model.BuyerIdentityUserIds = estate.BuyerIdentityUserIds.ToString();
                model.CreatedDate = estate.CreatedDate.ToShortDateString();
                model.ModifiedDate = estate.ModifiedDate.ToShortDateString();
                model.NoOfRooms = estate.NoOfRooms;
                model.BuildedYear = estate.BuildedYear;

                List<FileAttachment> fileAttachmentList = _context.FileAttachments.Where(attachment => attachment.EstateId == estate.Id && attachment.FileCategory == "image").ToList();

                if (fileAttachmentList.Count > 0)
                {
                    FileAttachment fileAttachment = fileAttachmentList.First();
                    model.ThumbNailFilePathAndName = fileAttachment.UniqueFileName;
                }

                estateModels.Add(model);
            }

            return estateModels;
        }



        // GET: api/Estates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Estate>> GetEstate(int id)
        {
            var estate = await _context.Estates.FindAsync(id);

            if (estate == null)
            {
                return NotFound();
            }

            return estate;
        }

        [HttpGet]
        [Route("[action]")]
        [Produces("application/json")]
        public List<EstateViewModel> GetEstateByProfileId(int profileId)
        {
            List<EstateViewModel> modelList = new();
            var estateList = _context.Estates.Where(estate => estate.OwnerIdentityUserIds == profileId).ToList();

            if (estateList == null)
            {
                return null;
            }

            foreach (var estate in estateList)
            {
                EstateViewModel model = new EstateViewModel();

                model.Id = estate.Id.ToString();
                model.Address1 = estate.Address1;
                model.Address2 = estate.Address2;
                model.Zip = estate.Zip;
                model.City = estate.City;
                model.RegistrationNumber = estate.RegistrationNumber;
                model.EstateType = estate.EstateType;
                model.Liggetid = estate.Liggetid;
                model.Price = estate.Price.ToString();
                model.EjerudgiftPrMd = estate.EjerudgiftPrMd.ToString();
                model.PrisPrM2 = estate.PrisPrM2.ToString();
                model.Areal = estate.Areal;
                model.VaegtetAreal = estate.VaegtetAreal;
                model.GrundAreal = estate.GrundAreal;
                model.NoOfRooms = estate.NoOfRooms;
                model.BuildedYear = estate.BuildedYear;
                model.OwnerIdentityUserIds = estate.OwnerIdentityUserIds.ToString();
                model.BuyerIdentityUserIds = estate.BuyerIdentityUserIds.ToString();
                model.CreatedDate = estate.CreatedDate.ToString();
                model.ModifiedDate = estate.ModifiedDate.ToString();

                modelList.Add(model);
            }
            return modelList;
        }


        // PUT: api/Estates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstate(int id, Estate estate)
        {
            if (id != estate.Id)
            {
                return BadRequest();
            }

            _context.Entry(estate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstateExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Estates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostEstate(EstateViewModel model)
        {
            Estate estate = new Estate();

            if (!string.IsNullOrEmpty(model.BuyerIdentityUserIds))
                estate.BuyerIdentityUserIds = int.Parse(model.BuyerIdentityUserIds);

            if (!string.IsNullOrEmpty(model.OwnerIdentityUserIds))
                estate.OwnerIdentityUserIds = int.Parse(model.OwnerIdentityUserIds);

            estate.RegistrationNumber = model.RegistrationNumber;

            estate.Address1 = model.Address1;
            estate.Address2 = model.Address2;
            estate.Zip = model.Zip;
            estate.City = model.City;

            estate.Price = Decimal.Parse(model.Price);
            estate.EjerudgiftPrMd = Decimal.Parse(model.EjerudgiftPrMd);
            estate.PrisPrM2 = Decimal.Parse(model.PrisPrM2);

            estate.Areal = model.Areal;
            estate.VaegtetAreal = model.VaegtetAreal;
            estate.GrundAreal = model.GrundAreal;
            estate.NoOfRooms = model.NoOfRooms;
            estate.BuildedYear = model.BuildedYear;

            estate.CreatedDate = DateTime.Now;
            estate.ModifiedDate = DateTime.Now;

            _context.Estates.Add(estate);

            await _context.SaveChangesAsync();


            return NoContent();



        }

        [HttpPut]
        public async Task<ActionResult> UpdateEstate(EstateViewModel model)
        {
            Estate estate = _context.Estates.Find(int.Parse(model.Id));
            if (estate == null)
            {
                return NoContent();
            }


            if (!string.IsNullOrEmpty(model.BuyerIdentityUserIds))
                estate.BuyerIdentityUserIds = int.Parse(model.BuyerIdentityUserIds);

            if (!string.IsNullOrEmpty(model.OwnerIdentityUserIds))
                estate.OwnerIdentityUserIds = int.Parse(model.OwnerIdentityUserIds);

            estate.RegistrationNumber = model.RegistrationNumber;

            estate.Address1 = model.Address1;
            estate.Address2 = model.Address2;
            estate.Zip = model.Zip;
            estate.City = model.City;

            estate.Price = Decimal.Parse(model.Price);
            estate.EjerudgiftPrMd = Decimal.Parse(model.EjerudgiftPrMd);
            estate.PrisPrM2 = Decimal.Parse(model.PrisPrM2);

            estate.Areal = model.Areal;
            estate.VaegtetAreal = model.VaegtetAreal;
            estate.GrundAreal = model.GrundAreal;
            estate.NoOfRooms = model.NoOfRooms;
            estate.BuildedYear = model.BuildedYear;

            estate.CreatedDate = DateTime.Now;
            estate.ModifiedDate = DateTime.Now;

            _context.Estates.Update(estate);

            await _context.SaveChangesAsync();


            return NoContent();



        }

        // DELETE: api/Estates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstate(int id)
        {
            var estate = await _context.Estates.FindAsync(id);
            if (estate == null)
            {
                return NotFound();
            }

            _context.Estates.Remove(estate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EstateExists(int id)
        {
            return _context.Estates.Any(e => e.Id == id);
        }





    }
}
