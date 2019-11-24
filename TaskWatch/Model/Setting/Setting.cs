using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;

namespace TaskWatch.Model.Setting
{
    public class Setting
    {
        private const string m_settingFilePath = "Setting.xml";
        private XElement m_settingFile;

     // パブリックメソッド

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Setting()
        {
            // 設定ファイル読込 失敗した時だけ設定ファイル初期化
            if(!LoadSettingFile())
            {
                InitSetting();
            }
        }

        /// <summary>
        /// ストップウォッチ起動中の通知が有効かどうかを取得
        /// </summary>
        /// <returns> true : ON, false : OFF </returns>
        public bool EnabledNotificationRunning()
        {
            // TODO: 中身の実装
            return false;
        }

        /// <summary>
        /// ストップウォッチ停止中の通知が有効かどうかを取得
        /// </summary>
        /// <returns></returns><returns> true : ON, false : OFF </returns>  
        public bool EnabledNotificationStopping()
        {
            // TODO: 中身の実装
            return false;
        }

        /// <summary>
        /// ストップウォッチ起動中の通知頻度を取得
        /// </summary>
        /// <returns>通知頻度</returns>
        public TimeSpan GetNotificationFrequencyRunning()
        {
            // TODO: 中身の実装
            return TimeSpan.Zero;
        }

        /// <summary>
        /// ストップウォッチ起動中の通知頻度を取得
        /// </summary>
        /// <returns>通知頻度</returns>
        public TimeSpan GetNotificationFrequencyStopping()
        {
            // TODO: 中身の実装
            return TimeSpan.Zero;
        }



     // プライベートメソッド

        /// <summary>
        /// 設定ファイルが存在したら読み込む
        /// </summary>
        /// <returns>
        /// true : 読み込み成功, false : 読み込み失敗
        /// </returns>
        private bool LoadSettingFile()
        {
            if (!File.Exists(m_settingFilePath))
            {
                return false;
            }

            // TODO: 設定ファイルが存在した時の処理(設定ファイル読込)
            return true;
        }

        /// <summary>
        /// 設定ファイルを初期化する(ファイルがあれば上書き、なければ作る)
        /// </summary>
        private void InitSetting()
        {
            // TODO: 初期設定ファイル作成
        }
    }
}
