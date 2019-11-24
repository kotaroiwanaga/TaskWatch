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
        private string taskRecordFilePath;
        private Dictionary<Category, HashSet<TaskInfo>> categoryTaskInfoPairs;


    // パブリックメソッド

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="filePath">タスクの記録ファイルパス</param>
        public ConfirmedTaskRecord(string filePath)
        {
            taskRecordFilePath = filePath;
            categoryTaskInfoPairs = new Dictionary<Category, HashSet<TaskInfo>>();
        }

        /// <summary>
        /// タスクの計測時間を記録する
        /// </summary>
        /// <param name="category"></param>
        /// <param name="taskInfo"></param>
        /// <param name="timeRecord"></param>
        public void Register(Category category, TaskInfo taskInfo, TimeRecord timeRecord)
        {
            // Category-TaskInfo一覧表更新
            RenewCategoryTaskInfoPairs(category, taskInfo);

            // 計測時間をファイルに記録
            AddTimeRecord(category, taskInfo, timeRecord);
        }


        /// <summary>
        /// カテゴリ一覧を取得
        /// </summary>
        /// <returns>カテゴリ一覧</returns>
        public IEnumerable<Category> GetCategories()
        {
           return categoryTaskInfoPairs.Keys.ToList<Category>();
        }

        /// <summary>
        /// タスク一覧の取得
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public IEnumerable<TaskInfo> GetTaskInfos(Category category)
        {
            HashSet<TaskInfo> taskInfos;

            if (categoryTaskInfoPairs.TryGetValue(category, out taskInfos))
            {
                return taskInfos;
            }

            return new HashSet<TaskInfo>();
        }

        /// <summary>
        /// 全カテゴリのタスク記録を集計し、ファイル出力する
        /// </summary>
        /// <param name="fileName"></param>
        public void Export(string fileName)
        {
            Export(fileName, this.GetCategories());
        }

        /// <summary>
        /// 指定したカテゴリのタスク記録を集計し、ファイル出力する
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="categories"></param>
        public void Export(string fileName, IEnumerable<Category> categories)
        {
            // TODO: 中身の実装
        }

    // プライベートメソッド

        /// <summary>
        /// タスク記録ファイルを読み込む
        /// </summary>
        /// <returns>読み込み成功：true, 読み込み失敗：false</returns>
        private void LoadRecordFile()
        {
            if (!File.Exists(taskRecordFilePath))
            {
                File.Create(taskRecordFilePath);
                return;
            }

            // TODO: ファイル読込、categoriesに格納
        }

        /// <summary>
        /// カテゴリ - タスク 一覧表の更新
        /// </summary>
        /// <param name="category"></param>
        /// <param name="taskInfo"></param>
        private void RenewCategoryTaskInfoPairs(Category category, TaskInfo taskInfo)
        {
            if (!categoryTaskInfoPairs.ContainsKey(category))
            {
                categoryTaskInfoPairs.Add(category, new HashSet<TaskInfo>());
            }

            // HashSetなので存在しない新規タスクのときだけ追加される
            categoryTaskInfoPairs[category].Add(taskInfo);
        }

        /// <summary>
        /// 計測時間をファイルに記録
        /// </summary>
        /// <param name="category"></param>
        /// <param name="taskInfo"></param>
        /// <param name="timeRecord"></param>
        private void AddTimeRecord(Category category, TaskInfo taskInfo, TimeRecord timeRecord)
        {
            // TODO: 中身の実装
        }
    }
}
