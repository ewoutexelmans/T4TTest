using System.Collections.Generic;

namespace Test.Models
{
    public class Track
    {
        public Track()
        {
            MorningTalks = new List<Talk>();
            AfternoonTalks = new List<Talk>();
        }
        public List<Talk> MorningTalks { get; set; }
        public List<Talk> AfternoonTalks { get; set; }
    }
}
