using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Test.Services
{
    public class FileReader : IFileReader
    {
        public async Task<IEnumerable<string>> Read(IFormFile file)
        {
            var contents = new List<string>();
            using (var streamReader = new StreamReader(file.OpenReadStream()))
            {
                while (!streamReader.EndOfStream)
                {
                    var line = await streamReader.ReadLineAsync();
                    contents.Add(line);
                }
            }

            return contents;
        }
    }
}
