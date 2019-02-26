﻿using System.Collections.Generic;
using Test.Models;

namespace Test.Services
{
    public interface ITalkParser
    {
        bool CanParse(List<string> talkStrings);
        bool CanParse(string talkString);
        Talk ParseString(string talkString);
        IEnumerable<Talk> ParseStrings(List<string> talkStrings);
    }
}