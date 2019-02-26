using System.Collections.Generic;
using Test.Models;

namespace Test.Services
{
    public interface ITalkParser
    {
        bool CanParse(IEnumerable<string> talkStrings);
        bool CanParse(string talkString);
        Talk ParseString(string talkString);
        IEnumerable<Talk> ParseStrings(IEnumerable<string> talkStrings);
    }
}