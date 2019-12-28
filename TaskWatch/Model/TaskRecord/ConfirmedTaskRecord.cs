using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using TaskWatch.Model.Data;

namespace TaskWatch.Model.TaskRecord
{
    public class ConfirmedTaskRecord : ITaskRecord
    {
        private const string CATEGORY_FILE_PATH = "Category.csv";
        private const string TASK_INFO_FILE_PATH = "TaskInfo.csv";
        private const string TIME_RECORD_FILE_PATH = "TimeRecord.csv";
        private List<Category> categories;


    // パブリックメソッド

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="filePath">タスクの記録ファイルパス</param>
        public ConfirmedTaskRecord()
        {
            this.categories = new List<Category>();
            LoadRecordFile();
        }

        /// <summary>
        /// タスクの計測時間を記録する
        /// </summary>
        /// <param name="category"></param>
        /// <param name="taskInfo"></param>
        /// <param name="timeRecord"></param>
        public void Register(CategoryData categoryData, TaskInfoData taskInfoData, TimeRecordData timeRecordData)
        {
            Category category = categories.Find(c => c.name == categoryData.name);

            if(category == null)
            {
                category = new Category(categoryData);
                categories.Add(category);
                RegisterCategory(category);
            }

            TaskInfo taskInfo = category.taskInfos.Find(ti => ti.name == taskInfoData.name);

            if (taskInfo == null)
            {
                taskInfo = new TaskInfo(taskInfoData);
                category.taskInfos.Add(taskInfo);
                RegisterTaskInfo(category.name, taskInfo);
            }

            TimeRecord timeRecord = new TimeRecord(timeRecordData);

            taskInfo.timeRecords.Add(timeRecord);
            RegisterTimeRecord(category.name, taskInfo.name, timeRecord);

        }


        /// <summary>
        /// カテゴリ一覧を取得
        /// </summary>
        /// <returns>カテゴリ一覧</returns>
        public List<CategoryData> GetCategoryDatas()
        {
            return categories.Select(category => category.ToStruct())
                             .ToList();
        }

        /// <summary>
        /// タスク一覧の取得
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public List<TaskInfoData> GetTaskInfos(CategoryData categoryData)
        {
            Category category = categories.Find(c => c.name == categoryData.name);

            if (category != null)
            {
                return category.taskInfos.Select(taskInfo => taskInfo.ToStruct())
                                         .ToList();
            }

            return new List<TaskInfoData>();
        }

        /// <summary>
        /// 時間記録一覧の取得
        /// </summary>
        /// <param name="categoryData"></param>
        /// <param name="taskInfoData"></param>
        /// <returns></returns>
        public List<TimeRecordData> GetTimeRecords(CategoryData categoryData, TaskInfoData taskInfoData)
        {
            Category category = categories.Find(c => c.name == categoryData.name);

            if (category != null)
            {
                TaskInfo taskInfo = category.taskInfos.Find(ti => ti.name == taskInfoData.name);

                if(taskInfo != null)
                {
                    return taskInfo.timeRecords.Select(tr => tr.ToStruct()).ToList();
                }
            }

            return new List<TimeRecordData>();
        }

        /// <summary>
        /// 全カテゴリのタスク記録を集計し、ファイル出力する
        /// </summary>
        /// <param name="fileName"></param>
        public void Export(string fileName)
        {
            Export(fileName, this.GetCategoryDatas());
        }

        /// <summary>
        /// 指定したカテゴリのタスク記録を集計し、ファイル出力する
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="categories"></param>
        public void Export(string fileName, List<CategoryData> categoryDatas)
        {
            // TODO: 中身の実装
        }

    // プライベートメソッド

        /// <summary>
        /// タスク記録ファイルを読み込む
        /// </summary>
        private void LoadRecordFile()
        {
            LoadCategory();
            LoadTaskInfo();
            LoadTimeRecord();
        }

        /// <summary>
        /// カテゴリファイルを読み込む
        /// </summary>
        private void LoadCategory()
        {
            LoadCsv(CATEGORY_FILE_PATH, values =>
            {
                Category category = new Category(values[0], values[1]);
                categories.Add(category);
            });
        }

        /// <summary>
        /// タスク情報ファイルを読み込む
        /// </summary>
        private void LoadTaskInfo()
        {
            LoadCsv(TASK_INFO_FILE_PATH, values => 
            {
                Category category = categories.Find(c => c.name == values[0]);

                if(category != null)
                {
                    TaskInfo taskInfo = new TaskInfo(values[1], values[2]);
                    category.taskInfos.Add(taskInfo);
                }
            });
        }

        /// <summary>
        /// 工数記録ファイルを読み込む
        /// </summary>
        private void LoadTimeRecord()
        {
            LoadCsv(TIME_RECORD_FILE_PATH, values =>
            {
                Category category = categories.Find(c => c.name == values[0]);

                if(category != null)
                {
                    TaskInfo taskInfo = category.taskInfos.Find(ti => ti.name == values[1]);

                    if(taskInfo != null)
                    {
                        DateTime startDateTime = DateTime.Parse(values[2]);
                        DateTime endDateTime = DateTime.Parse(values[3]);
                        TimeSpan requiredTime = TimeSpan.Parse(values[4]);
                        string comment = values[5];

                        taskInfo.timeRecords.Add(new TimeRecord(startDateTime, endDateTime, requiredTime, comment));
                    }
                }
            });
        }

        /// <summary>
        /// csvファイルを読み込み、各行に対して指定した操作を行う
        /// </summary>
        /// <param name="action"></param>
        private void LoadCsv(string filePath, Action<string[]> action)
        {
            if (!File.Exists(filePath))
            {
                using (File.Create(filePath)) 
                return;
            }

            List<string[]> csv = new List<string[]>();

            // csv読み込み
            using (StreamReader reader = new StreamReader(filePath))
            {
                while (reader.Peek() > -1)
                {
                    string line = reader.ReadLine();
                    csv.Add(line.Split(','));
                }
            }

            // 各行に指定した操作を行う
            csv.ForEach(action);
        }

        /// <summary>
        /// カテゴリーファイルに新規カテゴリー追記
        /// </summary>
        /// <param name="category">カテゴリー</param>
        private void RegisterCategory(Category category)
        {
            string[] values = { category.name, category.description };
            InsertRecordCsv(CATEGORY_FILE_PATH, values);
        }

        /// <summary>
        /// タスク情報ファイルに新規タスク追記
        /// </summary>
        /// <param name="categoryName">カテゴリー名</param>
        /// <param name="taskInfo">タスク情報</param>
        private void RegisterTaskInfo(string categoryName, TaskInfo taskInfo)
        {
            string[] values = { categoryName, taskInfo.name, taskInfo.description };
            InsertRecordCsv(TASK_INFO_FILE_PATH, values);
        }

        /// <summary>
        /// 工数記録ファイルに新規タスク追記
        /// </summary>
        /// <param name="categoryName">カテゴリー名</param>
        /// <param name="taskName">タスク名</param>
        /// <param name="timeRecord">工数記録</param>
        private void RegisterTimeRecord(string categoryName, string taskName, TimeRecord timeRecord)
        {
            string[] values = {categoryName, taskName, timeRecord.startDateTime.ToString(), timeRecord.endDateTime.ToString(),
                               timeRecord.requiredTime.ToString(), timeRecord.comment};
            InsertRecordCsv(TIME_RECORD_FILE_PATH, values);
        }

        /// <summary>
        /// csvにレコードを1行追加する
        /// </summary>
        /// <param name="filePath">csvファイルパス</param>
        /// <param name="values">登録する各値</param>
        private void InsertRecordCsv(string filePath, string[] values)
        {
            using (StreamWriter writer = new StreamWriter(new FileStream(filePath, FileMode.Append)))
            {
                for (int i = 0; i < values.Length; i++)
                {
                    writer.Write(values[i]);

                    if(i == values.Length - 1)
                    {
                        writer.WriteLine();
                    }
                    else
                    {
                        writer.Write(",");
                    }
                }
            }
        }
    }
}
