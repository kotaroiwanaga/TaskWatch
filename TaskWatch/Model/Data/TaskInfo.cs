using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWatch.Model.Data
{
    public class TaskInfo
    {

    // フィールド
        public string name;
        public string description;
        public List<TimeRecord> timeRecords;

    // パブリックメソッド
        public TaskInfo(string name)
        {
            this.name = name;
            this.description = "";
            this.timeRecords = new List<TimeRecord>();
        }

        public TaskInfo(string name, string description)
        {
            this.name = name;
            this.description = description;
            this.timeRecords = new List<TimeRecord>();
        }

        public TaskInfo(TaskInfoData taskInfoData)
        {
            this.name = taskInfoData.name;
            this.description = taskInfoData.description;
            this.timeRecords = new List<TimeRecord>();
        }

        public TaskInfoData ToStruct()
        {
            return new TaskInfoData(name, description);
        }

        /// <summary>
        /// Equalsオーバーライド 同一型オブジェクトの場合のみ変更
        /// </summary>
        /// <param name="obj">タスク名が同一の場合、true</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if(obj.GetType() == this.GetType())
            {
                TaskInfo taskInfo = (TaskInfo)obj;
                return taskInfo.name == this.name;
            }

            return base.Equals(obj);
        }

    }

    public struct TaskInfoData
    {
        public string name;
        public string description;

        public TaskInfoData(string name = "", string description ="")
        {
            this.name = name;
            this.description = description;
        }
    }
}
