using System.Collections.Generic;

namespace Test.Models
{
    public class Track
    {
        public IEnumerable<Talk> MorningTalks { get; set; }
        public IEnumerable<Talk> AfternoonTalks { get; set; }
    }
}
