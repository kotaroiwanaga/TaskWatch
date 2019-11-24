using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TaskWatch.Model.Data;

namespace TaskWatch.Model.Calculate
{
    public  class Calculater
    {
        public Dictionary<string, string> CalculateAll(IEnumerable<TimeRecord> timeRecords)
        {
            IEnumerable<TimeSpan> times = timeRecords.Select<TimeRecord, TimeSpan>(timeRecord => timeRecord.requiredTime);
            Dictionary<string, string> result = new Dictionary<string, string>();

            result.Add("Times", MeasuredTimes(times).ToString());
            result.Add("Total", Total(times).ToString());
            result.Add("Average", Average(times).ToString());
            result.Add("Max", Max(times).ToString());
            result.Add("Min", Min(times).ToString());

            return result;
        }

        public static int MeasuredTimes(IEnumerable<TimeSpan> times)
        {
            return times.Count();
        }

        public static TimeSpan Average(IEnumerable<TimeSpan> times)
        {
            return new TimeSpan(Total(times).Ticks / times.Count());
        }

        public static TimeSpan Max(IEnumerable<TimeSpan> times)
        {
            return times.Max();
        }

        public static TimeSpan Min(IEnumerable<TimeSpan> times)
        {
            return times.Min();
        }

        public static TimeSpan Total(IEnumerable<TimeSpan> times)
        {
            TimeSpan total = TimeSpan.Zero;
            times.ToList()
                .ForEach(time => total += time);

            return total;
        }

    }

}
