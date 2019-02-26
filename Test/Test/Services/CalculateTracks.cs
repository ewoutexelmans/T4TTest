using System;
using System.Collections.Generic;
using System.Linq;
using Test.Models;

namespace Test.Services
{
    public class CalculateTracks : ICalculateTracks
    {
        public int Calculate(IEnumerable<Talk> talks, int morningTime, int afternoonTime)
        {
            var sumTalks = talks.Select(t => t.Length).Sum();
            var sumTrackTime = morningTime + afternoonTime;
            return (int)Math.Ceiling((double)sumTalks / sumTrackTime);
        }
    }
}
