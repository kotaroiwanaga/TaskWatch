using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;

namespace TaskWatch
{
    partial class MainForm
    {
        private ToastNotifier m_notifier;

        // xmlPathTableの初期化
        public void InitialSettingToastNotification()
        {
            
            m_notifier = ToastNotificationManager.CreateToastNotifier("Microsoft.Windows.Computer");
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
