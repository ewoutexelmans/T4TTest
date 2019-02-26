using System.Collections.Generic;
using Test.Models;

namespace Test.Services
{
    public interface ITrackFiller
    {
        IEnumerable<Track> Fill(int trackAmount, IEnumerable<Talk> talks, int morningTime, int afternoonTimeMin, int afternoonTimeMax);
    }
}