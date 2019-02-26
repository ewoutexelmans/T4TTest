using System.Collections.Generic;
using Test.Models;

namespace Test.Services
{
    public interface ICalculateTracks
    {
        int Calculate(IEnumerable<Talk> talks, int morningTime, int afternoonTime);
    }
}