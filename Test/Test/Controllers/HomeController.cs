using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Test.Models;
using Test.Services;

namespace Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFileReader _reader;
        private readonly ITalkParser _parser;

        public HomeController(IFileReader reader, ITalkParser parser)
        {
            _reader = reader;
            _parser = parser;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> StreamFile(IFormFile file)
        {
            var contents = (await _reader.Read(file)).ToList();
            if (!_parser.CanParse(contents)) return BadRequest("Contents of file do not match format");
            var talks = _parser.ParseStrings(contents);
            return Ok(talks);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
