using System.Collections.Generic;

using TaskWatch.Model.Calculate;
using TaskWatch.Model.Data;
using TaskWatch.Model.Setting;
using TaskWatch.Model.TaskRecord;

namespace TaskWatch.Control
{
    public class TaskTimeRecordApp
    {
        private TaskRecordManager taskRecordManager;
        private Setting setting;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TaskTimeRecordApp()
        {
            taskRecordManager = new TaskRecordManager();
            setting = new Setting();
        }

        /// <summary>
        /// 全カテゴリを取得
        /// </summary>
        /// <returns></returns>
        public List<CategoryData> GetCategoryDatas()
        {
            return taskRecordManager.GetCategoryDatas();
        }

        /// <summary>
        /// 1カテゴリ内全タスク情報を取得
        /// </summary>
        /// <param name="categoryData"></param>
        /// <returns></returns>
        public List<TaskInfoData> GetTaskInfos(CategoryData categoryData)
        {
            return taskRecordManager.GetTaskInfoDatas(categoryData);
        }

        /// <summary>
        /// タスク情報詳細の取得
        /// </summary>
        /// <param name="categoryData"></param>
        /// <param name="taskInfoData"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetTaskInfoDetail(CategoryData categoryData, TaskInfoData taskInfoData)
        {
            List<TimeRecordData> timeRecordDatas = taskRecordManager.GetTimeRecordDatas(categoryData, taskInfoData);
            return Calculater.CalculateAll(timeRecordDatas);
        }

        /// <summary>
        /// 1タスク時間記録を保存
        /// </summary>
        /// <param name="categoryData"></param>
        /// <param name="taskInfoData"></param>
        /// <param name="timeRecordData"></param>
        public void Save(CategoryData categoryData, TaskInfoData taskInfoData, TimeRecordData timeRecordData)
        {
            taskRecordManager.Save(categoryData, taskInfoData, timeRecordData);
        }

        /// <summary>
        /// 1タスク時間を一時保存
        /// </summary>
        /// <param name="categoryData"></param>
        /// <param name="taskInfoData"></param>
        /// <param name="timeRecordData"></param>
        public void SaveTemporary(CategoryData categoryData, TaskInfoData taskInfoData, TimeRecordData timeRecordData)
        {
            taskRecordManager.Save(categoryData, taskInfoData, timeRecordData);
        }

        /// <summary>
        /// 1カテゴリ分の記録をファイル出力する
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="categoryDatas"></param>
        public void Export(string fileName, List<CategoryData> categoryDatas)
        {
            taskRecordManager.Export(fileName, categoryDatas);
        }
    }
}
