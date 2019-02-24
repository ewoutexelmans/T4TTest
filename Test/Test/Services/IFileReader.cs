using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Test.Services
{
    public interface IFileReader
    {
        Task<IEnumerable<string>> Read(IFormFile file);
    }
}