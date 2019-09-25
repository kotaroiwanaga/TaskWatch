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

namespace TaskWatch
{
    public partial class Form1 : Form
    {
        private Stopwatch m_stopwatch;
        private string m_filePath;
        private System.Text.Encoding m_encoding; // CSVファイルに書き込むときに使うEncoding

        public Form1()
        {
            // 各メンバ変数の初期化
            InitializeComponent();
            m_stopwatch = new Stopwatch();
            m_filePath = "TaskTimeRecord.csv";
            m_encoding = System.Text.Encoding.GetEncoding("Shift_JIS");

            // タスク名コンボボックスのリスト初期化
            RenewTaskComboBoxList();

            // タイマースタート
            TaskTimer.Start();
        }

        // タスク名コンボボックスのリストの更新
        private void RenewTaskComboBoxList()
        {
            if(!File.Exists(m_filePath))
            {
                return;
            }

            using (StreamReader streamReader = new StreamReader(m_filePath))
            {
                Task_ComboBox.Items.Clear();

                // 一行ずつ読み込む
                while (streamReader.Peek() > -1)
                {
                    string line = streamReader.ReadLine();
                    string[] values = line.Split(',');

                    Task_ComboBox.Items.Add(values[0]);
                }
            }
        }

        // タスク情報の読み込み
        private void LoadTaskInfo()
        {
            if(!File.Exists(m_filePath))
            {
                return;
            }

            using (StreamReader streamReader = new StreamReader(m_filePath))
            {
                // 一行ずつ読み込む
                while (streamReader.Peek() > -1)
                {
                    string line = streamReader.ReadLine();
                    string[] values = line.Split(',');

                    if (values[0] == Task_ComboBox.Text)
                    {
                        // valuesをリストに変換
                        List<string> tmp_valueList = new List<string>(values);

                        // valuesの先頭(i=0)はタスク名なので取り除く
                        tmp_valueList.RemoveAt(0);

                        // リストをstring→doubleに変換
                        List<TimeSpan> timeList = 
                            tmp_valueList.ConvertAll<TimeSpan>((string value) => 
                            {
                                // ※doubleに変換できると仮定してます
                                return TimeSpan.Parse(value);
                            }
                            );

                        // タスク情報を取得
                        TimesValue_Label.Text = timeList.Count().ToString();
                        AverageValue_Label.Text = CalcTimeSpanAverage(timeList).ToString();
                        MaxValue_Label.Text = timeList.Max().ToString();
                        MinValue_Label.Text = timeList.Min().ToString();

                        // 終わったら終了
                        return;
                    }
                }
            }
        }

        // TimeSpanのリストの平均値を出力
        private TimeSpan CalcTimeSpanAverage(List<TimeSpan> times)
        {
            // 合計値の計算
            TimeSpan total = new TimeSpan(0);
            times.ForEach(time => total = total.Add(time));

            return new TimeSpan(total.Ticks / times.Count());
        }

        private void StartStop_Button_Click(object sender, EventArgs e)
        {
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
        }

        private void Reset_Button_Click(object sender, EventArgs e)
        {
            m_stopwatch.Stop();
            m_stopwatch.Reset();
        }

        private void Save_Button_Click(object sender, EventArgs e)
        {
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
                if(m_stopwatch.Elapsed != TimeSpan.Zero)
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

                            // 対象の行にタスクの時間を追記する

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
                            while(streamReader.Peek() > -1)
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
                }
                else
                {
                    // TODO: ストップウォッチの値が0の時は保存しない
                }
            }
            else
            {
                // TODO: タスク名に問題があるとポップアップ表示する
            }
        }

        private void Task_ComboBox_TextChanged(object sender, EventArgs e)
        {
            LoadTaskInfo();
        }
    }
}
