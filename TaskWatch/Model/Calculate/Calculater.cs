using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TaskWatch.Model.Data;

namespace TaskWatch.Model.Calculate
{
    public static class Calculater
    {
        public static Dictionary<string, string> CalculateAll(List<TimeRecordData> timeRecordDatas)
        {
            List<TimeSpan> times = timeRecordDatas.Select<TimeRecordData, TimeSpan>(timeRecordData => timeRecordData.requiredTime).ToList();

            Dictionary<string, string> result = new Dictionary<string, string>();

            if(times.Count() > 0)
            {
                result.Add("Times", MeasuredTimes(times).ToString());
                result.Add("Total", Total(times).ToString());
                result.Add("Average", Average(times).ToString());
                result.Add("Max", Max(times).ToString());
                result.Add("Min", Min(times).ToString());
            }
            return result;
        }

        public static int MeasuredTimes(List<TimeSpan> times)
        {
            return times.Count();
        }

        public static TimeSpan Average(List<TimeSpan> times)
        {
            return new TimeSpan(Total(times).Ticks / times.Count());
        }

        public static TimeSpan Max(List<TimeSpan> times)
        {
            return times.Max();
        }

        public static TimeSpan Min(List<TimeSpan> times)
        {
            return times.Min();
        }

        public static TimeSpan Total(List<TimeSpan> times)
        {
            TimeSpan total = TimeSpan.Zero;
            times.ForEach(time => total += time);

            return total;
        }

    }

}
