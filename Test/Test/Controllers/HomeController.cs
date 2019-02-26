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
        private readonly ICalculateTracks _calculator;
        private readonly ITrackFiller _filler;
        private readonly int _morningTime;
        private readonly int _afternoonTimeMax;
        private readonly int _afternoonTimeMin;


        public HomeController(IFileReader reader, ITalkParser parser, ICalculateTracks calculator, ITrackFiller filler)
        {
            _reader = reader;
            _parser = parser;
            _calculator = calculator;
            _filler = filler;
            _morningTime = 180;
            _afternoonTimeMin = 180;
            _afternoonTimeMax = 240;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UploadFile(IFormFile file)
        {
            if (file.Length == 0) return BadRequest("Empty file");

            var contents = (await _reader.Read(file)).ToList();

            if (!_parser.CanParse(contents)) return BadRequest("Contents of file do not match format");

            var talks = _parser.ParseStrings(contents).ToList();
            var trackAmount = _calculator.Calculate(talks, _morningTime, _afternoonTimeMax);
            var tracks = _filler.Fill(trackAmount, talks, _morningTime, _afternoonTimeMin, _afternoonTimeMax);

            return View(tracks);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
