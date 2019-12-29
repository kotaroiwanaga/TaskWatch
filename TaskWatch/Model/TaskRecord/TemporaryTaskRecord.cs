using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TaskWatch.Model.Data;

namespace TaskWatch.Model.TaskRecord
{
    public class TemporaryTaskRecord : ITaskRecord
    {
        public Dictionary<Tuple<Category, TaskInfo>, TimeRecord> temporarySavedTime;

        public TemporaryTaskRecord()
        {
            this.temporarySavedTime = new Dictionary<Tuple<Category, TaskInfo>, TimeRecord>();
        }


        /// <summary>
        /// category, taskInfo をkeyとしたtimreRecordを格納
        /// </summary>
        /// <param name="categoryData"></param>
        /// <param name="taskInfoData"></param>
        /// <param name="timeRecordData"></param>
        public void Register(CategoryData categoryData, TaskInfoData taskInfoData, TimeRecordData timeRecordData)
        {
            // TODO: 中身の実装
            //Tuple<Category, TaskInfo> key = Tuple.Create<Category, TaskInfo>(category, taskInfo);

            //if(temporarySavedTime.ContainsKey(key))
            //{
            //    temporarySavedTime[key] = timeRecord;
            //}
            //else
            //{
            //    temporarySavedTime.Add(key, timeRecord);
            //}
        }

        /// <summary>
        /// 指定したCategory,TaskInfoの組をkeyとした計測時間を取得
        /// </summary>
        /// <param name="category"></param>
        /// <param name="taskInfo"></param>
        /// <returns>指定したKeyが存在した場合、Valueを返す / 存在しない場合、現在日時で初期化した計測時間を返す</returns>
        public TimeRecord GetTimeRecord(Category category, TaskInfo taskInfo)
        {
            Tuple<Category, TaskInfo> key = Tuple.Create<Category, TaskInfo>(category, taskInfo);

            TimeRecord timeRecord;
            if(temporarySavedTime.TryGetValue(key, out timeRecord))
            {
                return timeRecord;
            }
                return new TimeRecord(DateTime.Now);
        }
    }
}
