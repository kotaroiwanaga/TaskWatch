using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using System.Xml.Linq;
using System.Drawing;

namespace TaskWatch
{
    partial class MainForm
    {
        private ToastNotifier m_notifier;
        private const string m_toastNotificationConfigPath = "ToastNotificationConfig.xml";

        // Toast通知の初期化
        public void InitialSettingToastNotification()
        {
            m_notifier = ToastNotificationManager.CreateToastNotifier("Microsoft.Windows.Computer");
            LoadToastNotificationConfig();
        }

        // 設定ファイル読込
        public void LoadToastNotificationConfig()
        {
            if(System.IO.File.Exists(m_toastNotificationConfigPath))
            {
                try
                {
                    XElement configXml = XElement.Load(m_toastNotificationConfigPath);
                    IEnumerable<XElement> configs = from item
                                                   in configXml.Elements("ToastNotification")
                                                    select item;

                    foreach (XElement config in configs)
                    {
                        m_toastNotificationSwitch = System.Convert.ToBoolean(config.Element("ON").Value);

                        if (m_toastNotificationSwitch)
                        {
                            XElement runningConfig = config.Element("RunningElapseTimeSpan");
                            int hourRunning = int.Parse(runningConfig.Element("Hour").Value);
                            int minitRunning = int.Parse(runningConfig.Element("Minit").Value);

                            XElement stoppingConfig = config.Element("StoppingElapseTimeSpan");
                            int hourStopping = int.Parse(stoppingConfig.Element("Hour").Value);
                            int minitStopping = int.Parse(stoppingConfig.Element("Minit").Value);

                            m_noticeTimeSpanRunning = new TimeSpan(hourRunning, minitRunning, 0);
                            m_nextNoticeTimeSpanRunning = new TimeSpan(hourRunning, minitRunning, 0);
                            m_noticeTimeSpanStopping = new TimeSpan(hourStopping, minitStopping, 0);
                        }
                    }
                }
                catch (Exception)
                {
                    m_toastNotificationSwitch = false;
                    m_noticeTimeSpanRunning = new TimeSpan(0, 10, 0);
                    m_nextNoticeTimeSpanRunning = new TimeSpan(0, 10, 0);
                    m_noticeTimeSpanStopping = new TimeSpan(0, 10, 0);
                    ShowMessage("設定ファイルの読み込みに失敗しました", Color.Red);
                }           
            }
        }

        // トースト通知
        private void ToastNotification(String message)
        {
            ToastTemplateType type = ToastTemplateType.ToastText01;
            XmlDocument content = ToastNotificationManager.GetTemplateContent(type);
            IXmlNode text = content.GetElementsByTagName("text").First();
            text.AppendChild(content.CreateTextNode(message));

            m_notifier.Show(new ToastNotification(content));
        }

        // 経過時間の通知(ストップウォッチ起動中) time:経過時間
        public void NoticeElapsedTime(TimeSpan time)
        {
            ToastNotification(time.ToString() + " 経過");
        }

        // ストップウォッチ起動のリマインド(ストップウォッチの停止中)
        public void NoticeStopWatchStopping()
        {
            ToastNotification("TaskWatch待機中");
        }
    }
}
