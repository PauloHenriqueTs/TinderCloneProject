using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TinderClone.Modules.Profiles.Shared
{
    public class ImageUploadHandler
    {
        private readonly IWebHostEnvironment _environment;

        public ImageUploadHandler(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task Save(IFormFile file, ProfileDto profileDto)
        {
            try
            {
                var userId = profileDto.UserId;
                var uploads = Path.Combine(_environment.WebRootPath, "uploads");

                if (file.Length > 0)
                {
                    var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                    var fileName = userId.ToString() + extension;
                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    string baseUrl = Environment.GetEnvironmentVariable("APPLICATION_URL");

                    profileDto.PictureUrl = baseUrl + "/MyImages/" + fileName;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}