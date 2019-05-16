using Code9Insta.API.Core.DTO;
using Code9Insta.API.Helpers;
using Code9Insta.API.Helpers.Interfaces;
using Code9Insta.API.Infrastructure.Entities;
using Code9Insta.API.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Code9Insta.API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    public class ProfilesController : Controller
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IValidateRepository _validateRepository;
        private readonly IAuthorizationManager _authorizationManager;

        public ProfilesController(IProfileRepository profileRepository, IValidateRepository validateRepository, IAuthorizationManager authorizationManager)
        {
            _profileRepository = profileRepository;
            _validateRepository = validateRepository;
            _authorizationManager = authorizationManager;
        }

        [AllowAnonymous]
        [HttpPost("Create")]
        public IActionResult CreateProfile([FromBody]CreateProfileDto profile)
        {
            if(!_validateRepository.IsUserNameHandleUnique(profile.User.UserName, profile.Handle))
            {
                return StatusCode(409, "User allready exists"); 
            }

            var prof = AutoMapper.Mapper.Map<Profile>(profile);
            var salt = new byte[128 / 8];

            Random random = new Random();
            random.NextBytes(salt);

            prof.User.PasswordHash = _authorizationManager.GeneratePasswordHash(profile.User.Password, salt);
            prof.User.Salt = salt;

            _profileRepository.CreateProfile(prof);

            if(!_profileRepository.Save())
            {
                return StatusCode(500, "There was a problem while handling your request.");
            }

            return StatusCode(200, "Profile created");
        }
       
        [HttpGet("Get")]
        public IActionResult GetProfile()
        {
            var userId = Guid.Parse(HttpContext.User.GetUserId());

            var profile = _profileRepository.GetProfile(userId);

            if (profile == null)
            {
                return NotFound();
            }

            var profileDto = AutoMapper.Mapper.Map<GetProfileDto>(profile);

            return Ok(profileDto);
        }

        [HttpGet("{handle}")]
        public IActionResult GetProfileByHandle(string handle)
        {
            var profile = _profileRepository.GetProfileByHandle(handle);


            if (profile == null)
            {
                return NotFound();
            }

            var profileDto = AutoMapper.Mapper.Map<GetProfileDto>(profile);

            return Ok(profileDto);
        }

        [HttpGet]
        [Route("All")]
        public IActionResult GetProfileAllProfiles()
        {
            
            var profile = _profileRepository.GetAllProfiles();

            if (profile == null)
            {
                return NotFound();
            }

            var profileDto = AutoMapper.Mapper.Map<GetProfileDto>(profile);

            return Ok(profileDto);
        }
    }
}
