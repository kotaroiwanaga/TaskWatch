﻿using System;
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

namespace TaskWatch
{
    public partial class MainForm : Form
    {
        private Stopwatch m_stopwatch;
        private string m_filePath;
        private System.Text.Encoding m_encoding; // CSVファイルに書き込むときに使うEncoding
        private const string m_taskInfoDefaultText = "----"; // 新規タスクのタスク情報デフォルト表示テキスト
        private TimeSpan m_noticeTimeSpanRunning; // 通知間隔時間(ストップウォッチ起動時)
        private TimeSpan m_noticeTimeSpanStopping; // 通知間隔時間(ストップウォッチ停止時)
        private TimeSpan m_nextNoticeTimeSpanRunning; // 前回ストップウォッチ経過通知時間
        private DateTime m_prevNoticeTimeStopping; // 直前のストップウォッチ停止中通知時間
        private bool m_toastNotificationSwitch = false;

        public MainForm()
        {
            // 各メンバ変数の初期化
            InitializeComponent();
            m_stopwatch = new Stopwatch();
            m_filePath = "TaskTimeRecord.csv";
            m_encoding = System.Text.Encoding.GetEncoding("UTF-8");
            m_noticeTimeSpanRunning = new TimeSpan(0, 10, 0);
            m_noticeTimeSpanStopping = new TimeSpan(0, 10, 0);
            m_nextNoticeTimeSpanRunning = TimeSpan.Zero.Add(m_noticeTimeSpanRunning);
            m_prevNoticeTimeStopping = DateTime.Now;

            // メッセージラベルのサイズを親コンポーネントに合わせる
            Message_Label.Width = Message_TableLayoutPanel.Width;
            Message_Label.Height = Message_TableLayoutPanel.Height;

            // メッセージリセット
            ResetMessage();

            // タスク名コンボボックスのリスト初期化
            RenewTaskComboBoxList();

            // タスク情報表示の初期化
            DisplayDefaultTaskInfo();

            // トースト通知機能設定の初期化
            InitialSettingToastNotification();

            // タイマースタート
            TaskTimer.Start();
        }


        private void StartStop_Button_Click(object sender, EventArgs e)
        {
            ResetMessage();

            // 停止中通知時間リセット
            m_prevNoticeTimeStopping = DateTime.Now;

            // ストップウォッチ起動中
            if (m_stopwatch.IsRunning)
            {
                m_stopwatch.Stop();
            }
            // ストップウォッチ停止中
            else
            {
                m_stopwatch.Start();
            }
        }

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

        private void Save_Button_Click(object sender, EventArgs e)
        {
            // メッセージリセット
            ResetMessage();

            if(Task_ComboBox.Text != "")
            {
                m_stopwatch.Stop();

                // ファイルがない場合は作成する
                if(!File.Exists(m_filePath))
                {
                    FileStream fileStream =  File.Create(m_filePath);
                    fileStream.Close();
                }

                // 一時ファイルの作成
                string tmp_filePath = "tmp_" + m_filePath;
                if(!File.Exists(tmp_filePath))
                {
                    FileStream fileStream =  File.Create(tmp_filePath);
                    fileStream.Close();
                }


                string targetLine = ""; // 書き込む値のリスト

                // ストップウォッチが0でないとき
                if (m_stopwatch.Elapsed != TimeSpan.Zero)
                {
                    // CSVファイル読み込み
                    using (StreamReader streamReader = new System.IO.StreamReader(m_filePath))
                    {
                        // 一次ファイルを開く
                        using (StreamWriter streamWriter = new System.IO.StreamWriter(tmp_filePath, false, m_encoding))
                        {
                            // 一行ずつ読み込む
                            while (streamReader.Peek() > -1)
                            {
                                string line = streamReader.ReadLine();
                                string[] values = line.Split(',');

                                if (values[0] == Task_ComboBox.Text)
                                {
                                    targetLine = line;
                                    break;
                                }

                                streamWriter.WriteLine(line);
                            }


                            // 新規タスク
                            if (targetLine == "")
                            {
                                streamWriter.Write(Task_ComboBox.Text);
                                streamWriter.Write(",");
                                streamWriter.WriteLine(m_stopwatch.Elapsed.ToString());
                            }
                            // 既存タスク
                            else
                            {
                                targetLine += "," + m_stopwatch.Elapsed.ToString();
                                streamWriter.WriteLine(targetLine);
                            }

                            // 残りの行を一時ファイルに書き込む
                            while (streamReader.Peek() > -1)
                            {
                                string line = streamReader.ReadLine();
                                streamWriter.WriteLine(line);
                            }
                        }
                    }


                    // 一時ファイルとファイル名を入れ替える
                    string bu_filePath = "backup_" + m_filePath;
                    File.Copy(m_filePath, bu_filePath, true); // 第3引数は上書きtrue

                    File.Copy(tmp_filePath, m_filePath, true);
                    File.Delete(tmp_filePath);

                    // 保存できたらストップウォッチをリセット
                    m_stopwatch.Reset();

                    // タスク情報の更新
                    LoadTaskInfo();

                    // タスク名リストの更新
                    RenewTaskComboBoxList();

                    // 起動中経過時間リセット
                    m_nextNoticeTimeSpanRunning = TimeSpan.Zero.Add(m_noticeTimeSpanRunning);

                    // 停止中経過時間リセット
                    m_prevNoticeTimeStopping = DateTime.Now;
                }
                else
                {
                    // ストップウォッチの値が0の時は保存しないことをメッセージ表示する
                    ShowMessage("タスク時間未計測です", Color.Red);
                }
            }
            else
            {
                    // タスク名に問題があるとメッセージ表示する
                    ShowMessage("タスク名を入力してください", Color.Red);
            }
        }

        private void Task_ComboBox_TextChanged(object sender, EventArgs e)
        {
            ResetMessage();
            LoadTaskInfo();
        }
    }
}
