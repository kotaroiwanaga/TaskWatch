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
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TaskRecordManager()
        {
            confirmedRecord = new ConfirmedTaskRecord();
            temporaryRecord = new TemporaryTaskRecord();
        }

        /// <summary>
        /// カテゴリ一覧の取得
        /// </summary>
        /// <returns></returns>
        public List<CategoryData> GetCategoryDatas()
        {
            return confirmedRecord.GetCategoryDatas();
        }
        
        /// <summary>
        /// 1カテゴリ内のタスク情報一覧の取得
        /// </summary>
        /// <param name="categoryData"></param>
        /// <returns></returns>
        public List<TaskInfoData> GetTaskInfoDatas(CategoryData categoryData)
        {
            return confirmedRecord.GetTaskInfos(categoryData);
        }

        /// <summary>
        /// 1タスク情報内の時間記録一覧を取得
        /// </summary>
        /// <param name="categoryData"></param>
        /// <param name="taskInfoData"></param>
        /// <returns></returns>
        public List<TimeRecordData> GetTimeRecordDatas(CategoryData categoryData, TaskInfoData taskInfoData)
        {
            return confirmedRecord.GetTimeRecords(categoryData, taskInfoData);
        }

        /// <summary>
        /// 1タスク時間記録を保存 
        /// </summary>
        /// <param name="categoyData"></param>
        /// <param name="taskInfoData"></param>
        /// <param name="timeRecordData"></param>
        public void Save(CategoryData categoyData, TaskInfoData taskInfoData, TimeRecordData timeRecordData)
        {
            confirmedRecord.Register(categoyData, taskInfoData, timeRecordData);
        }

        /// <summary>
        /// 1タスク時間記録を一時保存
        /// </summary>
        /// <param name="categoryData"></param>
        /// <param name="taskInfoData"></param>
        /// <param name="timeRecordData"></param>
        public void SaveTemporary(CategoryData categoryData, TaskInfoData taskInfoData, TimeRecordData timeRecordData)
        {
            temporaryRecord.Register(categoryData, taskInfoData, timeRecordData);
        }

        /// <summary>
        ///  1カテゴリ分の記録をファイル出力する
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="categoryDatas"></param>
        public void Export(string fileName, List<CategoryData> categoryDatas)
        {
            confirmedRecord.Export(fileName, categoryDatas);
        }

    }
}
