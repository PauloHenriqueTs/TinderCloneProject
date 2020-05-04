using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinderClone.Modules.Profiles.Model;
using TinderClone.Modules.Profiles.Repository;
using TinderClone.Modules.Profiles.Shared;
using TinderClone.Modules.Users.Repository;

namespace TinderClone.Modules.Profiles.Service
{
    public class ProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _environment;

        public ProfileService(IProfileRepository profileRepository, IUserRepository userRepository, ILogger<ProfileService> logger, IWebHostEnvironment environment)
        {
            _profileRepository = profileRepository;
            _userRepository = userRepository;
            _logger = logger;
            _environment = environment;
        }

        public async Task validPostProfile(ProfileDto profileDto)
        {
            var user = await _userRepository.Read(profileDto.User);
            if (user == null)
            {
                throw new Exception();
            }
        }

        public async Task<Profile> ProfileExist(Guid id)
        {
            var profile = await _profileRepository.GetById(id);
            if (profile == null)
            {
                throw new Exception();
            }
            else
            {
                return profile;
            }
        }

        public void UpdateProfile(Profile profile, ProfileDto dto)
        {
            profile.Name = dto.Name;
            profile.Description = dto.Description;
            profile.PictureUrl = dto.PictureUrl;
        }

        public async Task<Profile> Put(Guid id, IFormFile file, ProfileDto profileDto)
        {
            try
            {
                var profile = await ProfileExist(id);
                await new ImageUploadHandler(_environment).Save(file, profileDto);
                UpdateProfile(profile, profileDto);
                await _profileRepository.Update(profile);
                return profile;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Profile> Delete(Guid id)
        {
            try
            {
                var profile = await ProfileExist(id);
                await _profileRepository.Delete(profile);
                return profile;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Profile> GetById(Guid id)
        {
            try
            {
                var profile = await ProfileExist(id);

                return profile;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Profile> Post(IFormFile file, ProfileDto profileDto)
        {
            try
            {
                await validPostProfile(profileDto);
                await new ImageUploadHandler(_environment).Save(file, profileDto);
                var profile = Profile.Factory.Create(profileDto);
                await _profileRepository.Create(profile);
                return profile;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}