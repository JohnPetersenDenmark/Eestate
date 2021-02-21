using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eestate.Models;
using Microsoft.AspNetCore.Identity;
using Eestate.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;

namespace Eestate.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private const string SECRET_KEY = "TQvgjeABMPOwCycOqah5EQu5yyVjpmVG";
        public static readonly SymmetricSecurityKey SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));


        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public ProfilesController(AppDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost]
        [Authorize]
        [Route("[action]")]
        [Produces("application/json")]
        public async Task<ActionResult<Profile>> saveEditedProfile(EditProfileViewModel editedProfile)
        {
            if ( editedProfile.Id != 0)
            {
                Profile profile = await _context.Profiles.FindAsync(editedProfile.Id);
                if (profile != null)
                {
                    profile.FullName = editedProfile.FullName;
                    profile.Address1 = editedProfile.Address1;
                    _context.Profiles.Update(profile);
                    await _context.SaveChangesAsync();
                    return StatusCode(StatusCodes.Status200OK);
                }               
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost]
        public async Task<ActionResult<Profile>> CreateUser(CreateProfileViewModel createProfile)
        {
            var userExists = await userManager.FindByNameAsync(createProfile.Email);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }

            IdentityUser newUser = new IdentityUser();
            newUser.Email = createProfile.Email;
            newUser.UserName = createProfile.Email;
            var result = await userManager.CreateAsync(newUser, createProfile.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            Profile profile = new Profile();
            profile.IdentityUserId = newUser.Id;
            profile.Email = createProfile.Email;
            profile.CreatedDate = DateTime.Now;
            profile.ModifiedDate = DateTime.Now;

            _context.Profiles.Add(profile);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK);

        }

        // GET: api/Profiles
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profile>>> GetProfiles()
        {
            return await _context.Profiles.ToListAsync();
        }

        [Authorize]
        [HttpGet]
        [Route("[action]")]
        [Produces("application/json")]
        public async Task<ActionResult<Profile>> GetMyProfileAsync()
        {                     
          if (User.Identity.IsAuthenticated)
            {
                string email = User.FindFirstValue(ClaimTypes.Email);
                if(! string.IsNullOrEmpty(email))
                {
                    IdentityUser curUser = await userManager.FindByEmailAsync(email);
                    if (curUser != null)
                    {
                        var x = _context.Profiles.Where(profile => profile.IdentityUserId.Equals(curUser.Id)).ToList();
                        Profile profile = x.First();
                        if (profile != null)
                        {
                            return profile;
                        }
                    }
                }     
            }

            return null;
        }


        [HttpPost]
        [Route("[action]")]
        [Produces("application/json")]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (result.Succeeded)
            {
                var userExists = await userManager.FindByNameAsync(model.Email);
                if (userExists == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);

                }

                else

                { 
                    // return new ObjectResult(GenerateToken(model.Email));

                   // return new ObjectResult(GenerateToken(model.Email)));

                    return StatusCode(StatusCodes.Status200OK, GenerateToken(model.Email));
                }
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }


        private string GenerateToken(string username)
        {
            var token = new JwtSecurityToken(
                claims: new Claim[] { new Claim(ClaimTypes.Email, username) },
                notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                expires: new DateTimeOffset(DateTime.Now.AddMinutes(60)).DateTime,
                signingCredentials: new SigningCredentials(SIGNING_KEY,
                                                    SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
