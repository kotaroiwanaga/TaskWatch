using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWatch.Model.Data
{
    public struct TimeRecord
    {
        public DateTime startDateTime { get; private set; }
        public DateTime endDateTime { get; private set; }
        public TimeSpan requiredTime { get; private set; }


        public TimeRecord(DateTime startDateTime)
        {
            this.startDateTime = startDateTime;
            this.endDateTime = startDateTime;
            this.requiredTime = TimeSpan.Zero;

        }

        public TimeRecord(DateTime startDateTime, DateTime endDateTime, TimeSpan requiredTime)
        {
            this.startDateTime = startDateTime;
            this.endDateTime = endDateTime;
            this.requiredTime = requiredTime;
        }

        public void Reset(DateTime startDateTime)
        {
            // コンストラクタと同じ処理
            this.startDateTime = startDateTime;
            this.endDateTime = startDateTime;
            this.requiredTime = TimeSpan.Zero;
        }

        public void SetEndDateTime(DateTime endDateTime)
        {
            if(this.startDateTime.CompareTo(endDateTime) > 0)
            {
                throw new ArgumentException("開始日時より以前の日時を設定しようとしました。");
            }

            TimeSpan diffecnceTime = endDateTime - this.startDateTime;

            if (requiredTime.CompareTo(diffecnceTime) > 0)
            {
                throw new ArgumentException("開始日時～終了日時の値が所要時間より短くなってしまいます。");
            }

            this.endDateTime = endDateTime;
        }

        public void SetRequiredTime(TimeSpan requiredTime)
        {
            TimeSpan diffecnceTime = endDateTime - this.startDateTime;

            if (requiredTime.CompareTo(diffecnceTime) > 0)
            {
                throw new ArgumentException("開始日時～終了日時より大きい値を設定しようとしました。");
            }

            this.requiredTime = requiredTime;
        }
    }
}
