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
            using (var streamReader = File.OpenText(Path.GetTempFileName()))
            {
                while (!streamReader.EndOfStream)
                {
                    contents.Add(await streamReader.ReadLineAsync());
                }
            }

            return contents;
        }
    }
}
