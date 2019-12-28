using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using TaskWatch.Model.Data;

namespace TaskWatch
{
    partial class MainForm
    {
        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitializeDisplay()
        {
            // カテゴリコンボボックス初期化
            Category_ComboBox.Text = "";

            // タスク情報コンボボックス初期化
            Task_ComboBox.Text = "";

            RenewDisplay();
        }

        /// <summary>
        /// 画面更新
        /// </summary>
        private void RenewDisplay()
        {
            RenewCategoryComboBox();
            RenewTaskComboBox();
            RenewStatus();
        }

        /// <summary>
        /// カテゴリコンボボックス更新
        /// </summary>
        private void RenewCategoryComboBox()
        {
            Category_ComboBox.Items.Clear();
            categories = taskApp.GetCategoryDatas();

            foreach(CategoryData category in categories)
            {
                Category_ComboBox.Items.Add(category.name);
            }
        }

        /// <summary>
        /// タスクコンボボックス更新
        /// </summary>
        private void RenewTaskComboBox()
        {
            Task_ComboBox.Items.Clear();
            CategoryData categoryData = categories.Find(c => c.name == Category_ComboBox.Text);
            taskInfos = taskApp.GetTaskInfos(categoryData);

            foreach(TaskInfoData taskInfo in taskInfos)
            {
                Task_ComboBox.Items.Add(taskInfo.name);
            }
        }

        /// <summary>
        /// ステータス初期化
        /// </summary>
        private void RenewStatus()
        {
            CategoryData categoryData = categories.Find(c => c.name == Category_ComboBox.Text);
            TaskInfoData taskInfoData = taskInfos.Find(t => t.name == Task_ComboBox.Text);

            RenewTaskInfoDetail(categoryData, taskInfoData);

            ResetMessage();
        }

       
        // タスク情報の読み込み
        private void RenewTaskInfoDetail(CategoryData categoryData, TaskInfoData taskInfoData)
        {
            Dictionary<string, string> taskInfoDetail = taskApp.GetTaskInfoDetail(categoryData, taskInfoData);

            // 新規タスクの場合デフォルト表示
            if (taskInfoDetail.Count == 0)
            {
                ResetTaskInfoDetail();
                return;
            }

            // タスク情報表示の更新
            RenewTaskInfoDetail(
                taskInfoDetail["Times"]
                , taskInfoDetail["Total"]
                , taskInfoDetail["Average"]
                , taskInfoDetail["Max"]
                , taskInfoDetail["Min"]
            );
        }

        // タスク情報表示をデフォルトにする
        private void ResetTaskInfoDetail()
        {
            RenewTaskInfoDetail(EMPTY_TEXT, EMPTY_TEXT, EMPTY_TEXT, EMPTY_TEXT, EMPTY_TEXT);
        }

        // タスク情報の更新
        private void RenewTaskInfoDetail(string timesText, string averageText, string maxText, string minText, string totalText)
        {
            TimesValue_Label.Text = timesText;
            AverageValue_Label.Text = averageText;
            MaxValue_Label.Text = maxText;
            MinValue_Label.Text = minText;
            TotalValue_Label.Text = totalText;
        }

        // メッセージ表示
        private void ShowMessage(string text, Color color)
        {
            Message_Label.Text = text;
            Message_Label.ForeColor = color;
        }

        // メッセージリセット
        private void ResetMessage()
        {
            Message_Label.Text = "";
        }
    }

    
}
