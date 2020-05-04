using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using TinderClone.Modules.Profiles.Model;
using TinderClone.Modules.Profiles.Service;
using TinderClone.Modules.Profiles.Shared;

namespace TinderClone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfileService _profileService;

        public ProfilesController(ProfileService profileService)
        {
            _profileService = profileService;
        }

        // POST: api/Profiles
        [HttpPost]
        public async Task<ActionResult<Profile>> PostProfile(IFormFile file, [FromForm] ProfileDto profileDto)
        {
            try
            {
                return await _profileService.Post(file, profileDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/Profiles/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Profile>> PutProfile(Guid id, IFormFile file, [FromForm] ProfileDto profileDto)
        {
            try
            {
                return await _profileService.Put(id, file, profileDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Profile>> DeleteProfile(Guid id)
        {
            try
            {
                return await _profileService.Delete(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Profile>> GetProfile(Guid id)
        {
            try
            {
                return await _profileService.GetById(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}