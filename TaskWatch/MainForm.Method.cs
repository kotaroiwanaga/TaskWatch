using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace TaskWatch
{
    partial class MainForm
    {
        // タスク名コンボボックスのリストの更新
        private void RenewTaskComboBoxList()
        {
            if (!File.Exists(m_filePath))
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
            if (!File.Exists(m_filePath))
            {
                DisplayDefaultTaskInfo();
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

                        // タスク情報を計算し、表示する
                        RenewTaskInfoDisplay(
                            timeList.Count().ToString(),              // 回数
                            CalcTimeSpanAverage(timeList).ToString(), // 平均値
                            timeList.Max().ToString(),                // 最大値
                            timeList.Min().ToString()                 // 最小値
                            );

                        // 終わったら終了
                        return;
                    }
                }

                // 新規タスク名の場合デフォルト表示
                DisplayDefaultTaskInfo();
            }
        }

        // タスク情報表示をデフォルトにする
        private void DisplayDefaultTaskInfo()
        {
            RenewTaskInfoDisplay(m_taskInfoDefaultText, m_taskInfoDefaultText, m_taskInfoDefaultText, m_taskInfoDefaultText);
        }

        // タスク情報の更新
        private void RenewTaskInfoDisplay(string timesText, string averageText, string maxText, string minText)
        {
            TimesValue_Label.Text = timesText;
            AverageValue_Label.Text = averageText;
            MaxValue_Label.Text = maxText;
            MinValue_Label.Text = minText;
        }

        // TimeSpanのリストの平均値を出力
        private TimeSpan CalcTimeSpanAverage(List<TimeSpan> times)
        {
            // 合計値の計算
            TimeSpan total = new TimeSpan(0);
            times.ForEach(time => total = total.Add(time));

            return new TimeSpan(total.Ticks / times.Count());
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
