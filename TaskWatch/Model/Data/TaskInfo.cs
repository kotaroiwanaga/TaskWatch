using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWatch.Model.Data
{
    public struct TaskInfo
    {

    // フィールド
        public string name;
        public string comment;

    // パブリックメソッド
        public TaskInfo(string name, string comment)
        {
            this.name = name;
            this.comment = comment;
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
}
