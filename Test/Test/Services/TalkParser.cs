using System.Collections.Generic;
using System.Linq;
using Test.Models;

namespace Test.Services
{
    public class TalkParser : ITalkParser
    {
        public bool CanParse(string talkString)
        {
            return CheckString(talkString);
        }

        public bool CanParse(IEnumerable<string> talkStrings)
        {
            var list = talkStrings.ToList();
            return list.Any() && list.Select(CheckString).All(b => b);
        }

        public Talk ParseString(string talkString)
        {
            return StringToTalk(talkString);
        }

        public IEnumerable<Talk> ParseStrings(IEnumerable<string> talkStrings)
        {
            return talkStrings.Select(StringToTalk).Where(talk => talk != null).ToList();
        }

        private static bool CheckString(string talkString)
        {
            if (talkString.Length < 3) return false;
            var regular = talkString.Substring(talkString.Length - 3);
            var lightning = talkString.Substring(talkString.Length - 9);
            return regular == "min" || lightning == "lightning";
        }

        private static Talk StringToTalk(string talkString)
        {
            if (talkString.Substring(talkString.Length - 3) == "min")
            {
                var s = "";
                for (var i = talkString.Length - 6; i < talkString.Length - 3; i++)
                {
                    if (char.IsNumber(talkString, i))
                    {
                        s += talkString.ElementAt(i);
                    }
                }
                return new Talk { Title = talkString, Length = int.Parse(s) };
            }

            if (talkString.Substring(talkString.Length - 9) == "lightning")
            {
                return new Talk { Title = talkString, Length = 5 };
            }

            return null;

        }
    }
}
