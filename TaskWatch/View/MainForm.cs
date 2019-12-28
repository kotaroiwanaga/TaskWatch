using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

using TaskWatch.Control;
using TaskWatch.Model.Data;

namespace TaskWatch
{
    struct S { public int a; };

    public partial class MainForm : Form
    {
        private const string EMPTY_TEXT = "----"; // 新規タスクのタスク情報デフォルト表示テキスト

        //private CategoryData categoryData;
        //private TaskInfoData taskInfoData;
        private List<CategoryData> categories;
        private List<TaskInfoData> taskInfos;
        private TimeRecordData timeRecordData;
        private Stopwatch m_stopwatch;
        //private string EMPTY_TEXT;
        private System.Text.Encoding m_encoding; // CSVファイルに書き込むときに使うEncoding
        private TimeSpan m_noticeTimeSpanRunning; // 通知間隔時間(ストップウォッチ起動時)
        private TimeSpan m_noticeTimeSpanStopping; // 通知間隔時間(ストップウォッチ停止時)
        private TimeSpan m_nextNoticeTimeSpanRunning; // 前回ストップウォッチ経過通知時間
        private DateTime m_prevNoticeTimeStopping; // 直前のストップウォッチ停止中通知時間
        private bool m_toastNotificationSwitch = false;

        private TaskTimeRecordApp taskApp; // TaskWatch処理担当

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainForm()
        {
            // 各メンバ変数の初期化
            InitializeComponent();

            taskApp = new TaskTimeRecordApp();

            categories = new List<CategoryData>();
            taskInfos = new List<TaskInfoData>();
            timeRecordData = new TimeRecordData();

            m_stopwatch = new Stopwatch();
            //EMPTY_TEXT = "TaskTimeRecord.csv";
            m_encoding = System.Text.Encoding.GetEncoding("UTF-8");
            m_noticeTimeSpanRunning = new TimeSpan(0, 10, 0);
            m_noticeTimeSpanStopping = new TimeSpan(0, 10, 0);
            m_nextNoticeTimeSpanRunning = TimeSpan.Zero.Add(m_noticeTimeSpanRunning);
            m_prevNoticeTimeStopping = DateTime.Now;

            // メッセージラベルのサイズを親コンポーネントに合わせる
            Message_Label.Width = Message_TableLayoutPanel.Width;
            Message_Label.Height = Message_TableLayoutPanel.Height;

            // 画面表示初期化
            InitializeDisplay();

            // トースト通知機能設定の初期化
            InitialSettingToastNotification();

            // タイマースタート
            TaskTimer.Start();
        }

        /// <summary>
        /// スタート/ストップボタン クリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartStop_Button_Click(object sender, EventArgs e)
        {
            ResetMessage();

            // 停止中通知時間リセット
            m_prevNoticeTimeStopping = DateTime.Now;

            // ストップウォッチ起動中
            if (m_stopwatch.IsRunning)
            {
                m_stopwatch.Stop();
                timeRecordData.endDateTime = DateTime.Now;
                timeRecordData.requiredTime = m_stopwatch.Elapsed;
            }
            // ストップウォッチ停止中
            else
            {
                if(m_stopwatch.ElapsedTicks == 0L)
                {
                    timeRecordData.startDateTime = DateTime.Now;
                }
                m_stopwatch.Start();
            }
        }

        /// <summary>
        /// 時間経過イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TaskTimer_Tick(object sender, EventArgs e)
        {
            Time_Label.Text = m_stopwatch.Elapsed.ToString();
            

            // ストップウォッチ起動中
            if (m_stopwatch.IsRunning)
            {
                // 計測時間が一定時間経過
                if (m_stopwatch.Elapsed > m_nextNoticeTimeSpanRunning)
                {
                    // トースト通知の表示
                    NoticeElapsedTime(m_nextNoticeTimeSpanRunning);

                    // 次の通知時間セット
                    m_nextNoticeTimeSpanRunning = m_nextNoticeTimeSpanRunning.Add(m_noticeTimeSpanRunning);
                }
            }
            // ストップウォッチ停止中
            else
            {
                TimeSpan elapsedTimeSpan = DateTime.Now - m_prevNoticeTimeStopping;

                // 前回の通知から一定時間経過
                if (elapsedTimeSpan > m_noticeTimeSpanStopping)
                {
                    // トースト通知の表示
                    NoticeStopWatchStopping();

                    // 通知時間リセット
                    m_prevNoticeTimeStopping = DateTime.Now;
                }
            }

        }

        /// <summary>
        /// リセットボタン クリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Reset_Button_Click(object sender, EventArgs e)
        {
            ResetMessage();
            m_stopwatch.Stop();
            m_stopwatch.Reset();

            // 起動中経過時間リセット
            m_nextNoticeTimeSpanRunning = TimeSpan.Zero.Add(m_noticeTimeSpanRunning);

            // 停止中経過時間リセット
            m_prevNoticeTimeStopping = DateTime.Now;
        }

        /// <summary>
        /// セーブボタン クリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Button_Click(object sender, EventArgs e)
        {
            ResetMessage();


            // カテゴリー未入力
            if (Category_ComboBox.Text == "")
            {
                ShowMessage("カテゴリー名を入力してください", Color.Red);
                return;
            }

            // タスク名未入力
            if (Task_ComboBox.Text == "")
            {
                ShowMessage("タスク名を入力してください", Color.Red);
                return;
            }
                    
            // ストップウォッチが0のとき
            if (m_stopwatch.Elapsed == TimeSpan.Zero)
            {
                ShowMessage("タスク時間未計測です", Color.Red);
                return;
            }

            m_stopwatch.Stop();
            timeRecordData.endDateTime = DateTime.Now;
            timeRecordData.requiredTime = m_stopwatch.Elapsed;

            // 保存
            taskApp.Save(new CategoryData(Category_ComboBox.Text), new TaskInfoData(Task_ComboBox.Text), timeRecordData);

            // 保存できたらストップウォッチをリセット
            m_stopwatch.Reset();

            // 画面更新
            RenewDisplay();

            // 起動中経過時間リセット
            m_nextNoticeTimeSpanRunning = TimeSpan.Zero.Add(m_noticeTimeSpanRunning);

            // 停止中経過時間リセット
            m_prevNoticeTimeStopping = DateTime.Now;
            
        }

        /// <summary>
        /// タスク名選択 変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Task_ComboBox_TextChanged(object sender, EventArgs e)
        {
            RenewStatus();
        }

        /// <summary>
        /// カテゴリ名選択 変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Category_ComboBox_TextChanged(object sender, EventArgs e)
        {
            RenewTaskComboBox();
            RenewStatus();
        }
    }
}
