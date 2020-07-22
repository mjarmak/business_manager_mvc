using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using business_manager_common_library;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;

namespace business_manager_api.Controllers
{
    [Route("logo")]
    [ApiController]
    //[Authorize]
    public class LogoController : Controller
    {
        private readonly DefaultContext _context;

        public LogoController (DefaultContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetLogo(long id)
        {
            var logoModel = await _context.Logo.FindAsync(id);

            if (logoModel == null)
            {
                return NotFound();
            }

            string imageStringBase64 = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(logoModel.ImageData));
            return Ok(new
            {
                //status = response.StatusCode,
                data = imageStringBase64
            });
        }

        [HttpGet("business/{id}")]
        public ActionResult GetBusinessLogo(long id)
        {
            LogoModel logoModel;
            try
            {
                logoModel = _context.Logo.Where(i => i.EntityId == id).Single();
            } catch (AmbiguousMatchException)
            {
                return NoContent();
            }
            if (logoModel == null)
            {
                return NoContent();
            }

            return Ok(new
            {
                //status = response.StatusCode,
                data = EnvelopeOfLogo(logoModel)
            });
        }

        [HttpGet("business/{id}/jpeg")]
        public string GetBusinessLogoJpeg(long id)
        {
            LogoModel logoModel;
            try
            {
                logoModel = _context.Logo.Where(i => i.EntityId == id).Single();
            }
            catch (AmbiguousMatchException)
            {
                return null;
            }
            if (logoModel == null)
            {
                return null;
            }

            return System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(logoModel.ImageData));
        }

        private ImageModel EnvelopeOfLogo(LogoModel logo)
        {
            return new ImageModel(logo.Id, logo.EntityId, System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(logo.ImageData)));
        }

        [HttpPost("business/{id}")]
        [Consumes("multipart/form-data")]
        [RequestSizeLimit(100_000_000)]
        public async Task<ActionResult> PostBusinessLogo(long id, IFormFile logo)
        {
            if (!BusinessDataModelExists(id))
            {
                return NotFound("Business does not exist");
            }
            if (!logo.ContentType.Contains("image") && !logo.ContentType.Contains("jpeg") && !logo.ContentType.Contains("jpg"))
            {
                return BadRequest("File must be an image");
            }

            //var ms = new MemoryStream();
            //logo.OpenReadStream().CopyTo(ms);
            //byte[] ba = ms.ToArray();

            Stream stream = logo.OpenReadStream();
            StreamReader reader = new StreamReader(stream);
            string imageString = reader.ReadToEnd();
            string imageStringBase64 = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(imageString));
            _context.Logo.Add(new LogoModel(id, imageStringBase64));
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BusinessDataModelExists(long id)
        {
            return _context.BusinessDataModel.Any(e => e.Id == id);
        }

        [HttpPost()]
        public async Task<ActionResult<LogoModel>> PostLogo(LogoModel logo)
        {
            _context.Logo.Add(logo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
