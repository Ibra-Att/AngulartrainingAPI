using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngulartrainingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {


        [HttpPost]
        [Route("[action]")]
        public async Task<string> UploadImage(IFormFile file)
        {
            string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Images");//=>PharmaSync-API/Images

            if (file == null || file.Length == 0)
            {
                throw new Exception("Please provide a valid file");
            }
            string newFileURL = Guid.NewGuid().ToString() + "" + file.FileName;
            using (var fileInput = new FileStream(Path.Combine(uploadFolder, newFileURL), FileMode.Create))
            {
                await file.CopyToAsync(fileInput);
            }
            return newFileURL;
        }



    }
}
