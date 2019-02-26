using System.Collections.Generic;
using System.Linq;
using Test.Models;

namespace Test.Services
{
    public class TrackFiller : ITrackFiller
    {
        public IEnumerable<Track> Fill(int trackAmount, IEnumerable<Talk> talks, int morningTime, int afternoonTimeMin, int afternoonTimeMax)
        {
            var talkList = talks.ToList();
            var trackList = new List<Track>();
            for (var j = 0; j < trackAmount; j++)
            {
                var track = new Track();
                if (talkList.Select(t => t.Length).Sum() < morningTime)
                {
                    track.MorningTalks = talkList;
                    trackList.Add(track);
                    return trackList;
                }

                if (SubsetSum.Find(talkList, morningTime))
                {
                    var indexes = SubsetSum.GetLastResult(morningTime).ToList();
                    foreach (var i in indexes)
                    {
                        track.MorningTalks.Add(talkList.ElementAt(i));

                    }

                    foreach (var i in indexes)
                    {
                        talkList.RemoveAt(i);
                    }
                }

                if (talkList.Select(t => t.Length).Sum() < afternoonTimeMin)
                {
                    track.AfternoonTalks = talkList;
                    trackList.Add(track);

                    return trackList;
                }
                if (SubsetSum.Find(talkList, afternoonTimeMin))
                {
                    var indexes = SubsetSum.GetLastResult(afternoonTimeMin).ToList();
                    foreach (var i in indexes)
                    {
                        track.AfternoonTalks.Add(talkList.ElementAt(i));

                    }


                    foreach (var i in indexes)
                    {
                        talkList.RemoveAt(i);
                    }
                }
                trackList.Add(track);

            }

            if (talkList.Any())
            {
                foreach (var talk in talkList)
                {
                    foreach (var track in trackList)
                    {
                        if (talk.Length >= afternoonTimeMax - track.AfternoonTalks.Select(t => t.Length).Sum())
                            continue;
                        track.AfternoonTalks.Add(talk);
                        break;
                    }

                }
            }
            return trackList;
        }
    }

    internal static class SubsetSum
    {
        public static readonly Dictionary<int, bool> Memo;
        private static readonly Dictionary<int, KeyValuePair<int, Talk>> Prev;

        static SubsetSum()
        {
            Memo = new Dictionary<int, bool>();
            Prev = new Dictionary<int, KeyValuePair<int, Talk>>();
        }

        public static bool Find(List<Talk> inputArray, int sum)
        {
            Memo.Clear();
            Prev.Clear();

            Memo[0] = true;
            Prev[0] = new KeyValuePair<int, Talk>(-1, null);

            for (var i = 0; i < inputArray.Count; ++i)
            {
                var num = inputArray[i];
                for (var s = sum; s >= num.Length; --s)
                {
                    if (!Memo.ContainsKey(s - num.Length) || !Memo[s - num.Length]) continue;
                    Memo[s] = true;

                    if (!Prev.ContainsKey(s))
                    {
                        Prev[s] = new KeyValuePair<int, Talk>(i, num);
                    }
                }
            }

            return Memo.ContainsKey(sum) && Memo[sum];
        }

        public static IEnumerable<int> GetLastResult(int sum)
        {
            while (Prev[sum].Key != -1)
            {
                yield return Prev[sum].Key;
                sum -= Prev[sum].Value.Length;
            }
        }
    }
}
