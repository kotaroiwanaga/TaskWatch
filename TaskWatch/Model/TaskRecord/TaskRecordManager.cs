using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TaskWatch.Model.Data;

namespace TaskWatch.Model.TaskRecord
{
    public class TaskRecordManager
    {
        private static readonly string taskTimeRecordFilePath = "TaskTimeRecord.csv";

        private ConfirmedTaskRecord confirmedRecord;
        private TemporaryTaskRecord temporaryRecord;

     // パブリックメソッド
        public TaskRecordManager()
        {
            confirmedRecord = new ConfirmedTaskRecord(taskTimeRecordFilePath);
            temporaryRecord = new TemporaryTaskRecord();
        }

        public IEnumerable<Category> GetCategories()
        {
            return confirmedRecord.GetCategories();
        }
        
        public IEnumerable<string> GetTaskNames(Category category)
        {
            return confirmedRecord.GetTaskInfos(category)
                .Select<TaskInfo, string>(taskInfo => taskInfo.name)
                .ToList();
        }

        public void Save(Category category, TaskInfo taskInfo, TimeRecord timeRecord)
        {
            confirmedRecord.Register(category, taskInfo, timeRecord);
        }

        public void SaveTemporary(Category category, TaskInfo taskInfo, TimeRecord timeRecord)
        {
            temporaryRecord.Register(category, taskInfo, timeRecord);
        }

        public void Export(string fileName, IEnumerable<string> categoryNames)
        {
            IEnumerable<Category> categories
                = confirmedRecord.GetCategories()
                .Where<Category>(category => categoryNames.Contains<string>(category.name))
                .ToList();

            confirmedRecord.Export(fileName, categories);
        }

    }
}
