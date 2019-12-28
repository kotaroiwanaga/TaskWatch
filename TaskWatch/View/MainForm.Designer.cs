namespace TaskWatch
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.TaskTimer = new System.Windows.Forms.Timer(this.components);
            this.TaskInfo_Panel = new System.Windows.Forms.Panel();
            this.TotalValue_Label = new System.Windows.Forms.Label();
            this.Total_Label = new System.Windows.Forms.Label();
            this.MinValue_Label = new System.Windows.Forms.Label();
            this.MaxValue_Label = new System.Windows.Forms.Label();
            this.AverageValue_Label = new System.Windows.Forms.Label();
            this.Min_Label = new System.Windows.Forms.Label();
            this.Max_Label = new System.Windows.Forms.Label();
            this.Average_Panel = new System.Windows.Forms.Label();
            this.TimesValue_Label = new System.Windows.Forms.Label();
            this.Times_Label = new System.Windows.Forms.Label();
            this.Setting_Panel = new System.Windows.Forms.Panel();
            this.Category_ComboBox = new System.Windows.Forms.ComboBox();
            this.Category_Label = new System.Windows.Forms.Label();
            this.Task_Label = new System.Windows.Forms.Label();
            this.Task_ComboBox = new System.Windows.Forms.ComboBox();
            this.StopWatch_Panel = new System.Windows.Forms.Panel();
            this.Save_Button = new System.Windows.Forms.Button();
            this.Reset_Button = new System.Windows.Forms.Button();
            this.StartStop_Button = new System.Windows.Forms.Button();
            this.Time_Label = new System.Windows.Forms.Label();
            this.Message_TableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.Message_Label = new System.Windows.Forms.Label();
            this.TaskInfo_Panel.SuspendLayout();
            this.Setting_Panel.SuspendLayout();
            this.StopWatch_Panel.SuspendLayout();
            this.Message_TableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // TaskTimer
            // 
            this.TaskTimer.Tick += new System.EventHandler(this.TaskTimer_Tick);
            // 
            // TaskInfo_Panel
            // 
            this.TaskInfo_Panel.Controls.Add(this.TotalValue_Label);
            this.TaskInfo_Panel.Controls.Add(this.Total_Label);
            this.TaskInfo_Panel.Controls.Add(this.MinValue_Label);
            this.TaskInfo_Panel.Controls.Add(this.MaxValue_Label);
            this.TaskInfo_Panel.Controls.Add(this.AverageValue_Label);
            this.TaskInfo_Panel.Controls.Add(this.Min_Label);
            this.TaskInfo_Panel.Controls.Add(this.Max_Label);
            this.TaskInfo_Panel.Controls.Add(this.Average_Panel);
            this.TaskInfo_Panel.Controls.Add(this.TimesValue_Label);
            this.TaskInfo_Panel.Controls.Add(this.Times_Label);
            this.TaskInfo_Panel.Location = new System.Drawing.Point(63, 235);
            this.TaskInfo_Panel.Name = "TaskInfo_Panel";
            this.TaskInfo_Panel.Size = new System.Drawing.Size(317, 178);
            this.TaskInfo_Panel.TabIndex = 8;
            // 
            // TotalValue_Label
            // 
            this.TotalValue_Label.AutoSize = true;
            this.TotalValue_Label.Location = new System.Drawing.Point(115, 147);
            this.TotalValue_Label.Name = "TotalValue_Label";
            this.TotalValue_Label.Size = new System.Drawing.Size(39, 15);
            this.TotalValue_Label.TabIndex = 9;
            this.TotalValue_Label.Text = "----";
            // 
            // Total_Label
            // 
            this.Total_Label.AutoSize = true;
            this.Total_Label.Location = new System.Drawing.Point(29, 147);
            this.Total_Label.Name = "Total_Label";
            this.Total_Label.Size = new System.Drawing.Size(39, 15);
            this.Total_Label.TabIndex = 8;
            this.Total_Label.Text = "Total";
            // 
            // MinValue_Label
            // 
            this.MinValue_Label.AutoSize = true;
            this.MinValue_Label.Location = new System.Drawing.Point(115, 112);
            this.MinValue_Label.Name = "MinValue_Label";
            this.MinValue_Label.Size = new System.Drawing.Size(39, 15);
            this.MinValue_Label.TabIndex = 7;
            this.MinValue_Label.Text = "----";
            // 
            // MaxValue_Label
            // 
            this.MaxValue_Label.AutoSize = true;
            this.MaxValue_Label.Location = new System.Drawing.Point(115, 75);
            this.MaxValue_Label.Name = "MaxValue_Label";
            this.MaxValue_Label.Size = new System.Drawing.Size(39, 15);
            this.MaxValue_Label.TabIndex = 6;
            this.MaxValue_Label.Text = "----";
            // 
            // AverageValue_Label
            // 
            this.AverageValue_Label.AutoSize = true;
            this.AverageValue_Label.Location = new System.Drawing.Point(115, 40);
            this.AverageValue_Label.Name = "AverageValue_Label";
            this.AverageValue_Label.Size = new System.Drawing.Size(39, 15);
            this.AverageValue_Label.TabIndex = 5;
            this.AverageValue_Label.Text = "----";
            // 
            // Min_Label
            // 
            this.Min_Label.AutoSize = true;
            this.Min_Label.Location = new System.Drawing.Point(29, 112);
            this.Min_Label.Name = "Min_Label";
            this.Min_Label.Size = new System.Drawing.Size(29, 15);
            this.Min_Label.TabIndex = 4;
            this.Min_Label.Text = "Min";
            // 
            // Max_Label
            // 
            this.Max_Label.AutoSize = true;
            this.Max_Label.Location = new System.Drawing.Point(29, 75);
            this.Max_Label.Name = "Max_Label";
            this.Max_Label.Size = new System.Drawing.Size(32, 15);
            this.Max_Label.TabIndex = 3;
            this.Max_Label.Text = "Max";
            // 
            // Average_Panel
            // 
            this.Average_Panel.AutoSize = true;
            this.Average_Panel.Location = new System.Drawing.Point(29, 40);
            this.Average_Panel.Name = "Average_Panel";
            this.Average_Panel.Size = new System.Drawing.Size(58, 15);
            this.Average_Panel.TabIndex = 2;
            this.Average_Panel.Text = "Average";
            // 
            // TimesValue_Label
            // 
            this.TimesValue_Label.AutoSize = true;
            this.TimesValue_Label.Location = new System.Drawing.Point(115, 11);
            this.TimesValue_Label.Name = "TimesValue_Label";
            this.TimesValue_Label.Size = new System.Drawing.Size(39, 15);
            this.TimesValue_Label.TabIndex = 1;
            this.TimesValue_Label.Text = "----";
            // 
            // Times_Label
            // 
            this.Times_Label.AutoSize = true;
            this.Times_Label.Location = new System.Drawing.Point(29, 11);
            this.Times_Label.Name = "Times_Label";
            this.Times_Label.Size = new System.Drawing.Size(45, 15);
            this.Times_Label.TabIndex = 0;
            this.Times_Label.Text = "Times";
            // 
            // Setting_Panel
            // 
            this.Setting_Panel.Controls.Add(this.Category_ComboBox);
            this.Setting_Panel.Controls.Add(this.Category_Label);
            this.Setting_Panel.Controls.Add(this.Task_Label);
            this.Setting_Panel.Controls.Add(this.Task_ComboBox);
            this.Setting_Panel.Location = new System.Drawing.Point(63, 24);
            this.Setting_Panel.Name = "Setting_Panel";
            this.Setting_Panel.Size = new System.Drawing.Size(317, 177);
            this.Setting_Panel.TabIndex = 9;
            // 
            // Category_ComboBox
            // 
            this.Category_ComboBox.FormattingEnabled = true;
            this.Category_ComboBox.Location = new System.Drawing.Point(32, 38);
            this.Category_ComboBox.Name = "Category_ComboBox";
            this.Category_ComboBox.Size = new System.Drawing.Size(233, 23);
            this.Category_ComboBox.TabIndex = 1;
            this.Category_ComboBox.TextChanged += new System.EventHandler(this.Category_ComboBox_TextChanged);
            // 
            // Category_Label
            // 
            this.Category_Label.AutoSize = true;
            this.Category_Label.Location = new System.Drawing.Point(21, 12);
            this.Category_Label.Name = "Category_Label";
            this.Category_Label.Size = new System.Drawing.Size(64, 15);
            this.Category_Label.TabIndex = 10;
            this.Category_Label.Text = "Category";
            // 
            // Task_Label
            // 
            this.Task_Label.AutoSize = true;
            this.Task_Label.Location = new System.Drawing.Point(21, 76);
            this.Task_Label.Name = "Task_Label";
            this.Task_Label.Size = new System.Drawing.Size(37, 15);
            this.Task_Label.TabIndex = 9;
            this.Task_Label.Text = "Task";
            // 
            // Task_ComboBox
            // 
            this.Task_ComboBox.FormattingEnabled = true;
            this.Task_ComboBox.Location = new System.Drawing.Point(32, 103);
            this.Task_ComboBox.Name = "Task_ComboBox";
            this.Task_ComboBox.Size = new System.Drawing.Size(233, 23);
            this.Task_ComboBox.TabIndex = 2;
            this.Task_ComboBox.TextChanged += new System.EventHandler(this.Task_ComboBox_TextChanged);
            // 
            // StopWatch_Panel
            // 
            this.StopWatch_Panel.Controls.Add(this.Save_Button);
            this.StopWatch_Panel.Controls.Add(this.Reset_Button);
            this.StopWatch_Panel.Controls.Add(this.StartStop_Button);
            this.StopWatch_Panel.Controls.Add(this.Time_Label);
            this.StopWatch_Panel.Location = new System.Drawing.Point(402, 24);
            this.StopWatch_Panel.Name = "StopWatch_Panel";
            this.StopWatch_Panel.Size = new System.Drawing.Size(351, 177);
            this.StopWatch_Panel.TabIndex = 10;
            // 
            // Save_Button
            // 
            this.Save_Button.Location = new System.Drawing.Point(257, 102);
            this.Save_Button.Name = "Save_Button";
            this.Save_Button.Size = new System.Drawing.Size(75, 23);
            this.Save_Button.TabIndex = 5;
            this.Save_Button.Text = "Save";
            this.Save_Button.UseVisualStyleBackColor = true;
            this.Save_Button.Click += new System.EventHandler(this.Save_Button_Click);
            // 
            // Reset_Button
            // 
            this.Reset_Button.Location = new System.Drawing.Point(164, 103);
            this.Reset_Button.Name = "Reset_Button";
            this.Reset_Button.Size = new System.Drawing.Size(75, 23);
            this.Reset_Button.TabIndex = 4;
            this.Reset_Button.Text = "Reset";
            this.Reset_Button.UseVisualStyleBackColor = true;
            this.Reset_Button.Click += new System.EventHandler(this.Reset_Button_Click);
            // 
            // StartStop_Button
            // 
            this.StartStop_Button.Location = new System.Drawing.Point(45, 102);
            this.StartStop_Button.Name = "StartStop_Button";
            this.StartStop_Button.Size = new System.Drawing.Size(97, 23);
            this.StartStop_Button.TabIndex = 3;
            this.StartStop_Button.Text = "Start/Stop";
            this.StartStop_Button.UseVisualStyleBackColor = true;
            this.StartStop_Button.Click += new System.EventHandler(this.StartStop_Button_Click);
            // 
            // Time_Label
            // 
            this.Time_Label.AutoSize = true;
            this.Time_Label.Font = new System.Drawing.Font("MS UI Gothic", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Time_Label.Location = new System.Drawing.Point(39, 38);
            this.Time_Label.Name = "Time_Label";
            this.Time_Label.Size = new System.Drawing.Size(139, 33);
            this.Time_Label.TabIndex = 6;
            this.Time_Label.Text = "00:00:00";
            // 
            // Message_TableLayoutPanel
            // 
            this.Message_TableLayoutPanel.ColumnCount = 1;
            this.Message_TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.Message_TableLayoutPanel.Controls.Add(this.Message_Label, 0, 0);
            this.Message_TableLayoutPanel.Location = new System.Drawing.Point(402, 235);
            this.Message_TableLayoutPanel.Name = "Message_TableLayoutPanel";
            this.Message_TableLayoutPanel.RowCount = 1;
            this.Message_TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.Message_TableLayoutPanel.Size = new System.Drawing.Size(351, 178);
            this.Message_TableLayoutPanel.TabIndex = 12;
            // 
            // Message_Label
            // 
            this.Message_Label.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Message_Label.Location = new System.Drawing.Point(3, 0);
            this.Message_Label.Name = "Message_Label";
            this.Message_Label.Size = new System.Drawing.Size(329, 139);
            this.Message_Label.TabIndex = 2;
            this.Message_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.StopWatch_Panel);
            this.Controls.Add(this.Setting_Panel);
            this.Controls.Add(this.TaskInfo_Panel);
            this.Controls.Add(this.Message_TableLayoutPanel);
            this.Name = "MainForm";
            this.Text = "TaskWatch";
            this.TaskInfo_Panel.ResumeLayout(false);
            this.TaskInfo_Panel.PerformLayout();
            this.Setting_Panel.ResumeLayout(false);
            this.Setting_Panel.PerformLayout();
            this.StopWatch_Panel.ResumeLayout(false);
            this.StopWatch_Panel.PerformLayout();
            this.Message_TableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer TaskTimer;
        private System.Windows.Forms.Panel TaskInfo_Panel;
        private System.Windows.Forms.Label MinValue_Label;
        private System.Windows.Forms.Label MaxValue_Label;
        private System.Windows.Forms.Label AverageValue_Label;
        private System.Windows.Forms.Label Min_Label;
        private System.Windows.Forms.Label Max_Label;
        private System.Windows.Forms.Label Average_Panel;
        private System.Windows.Forms.Label TimesValue_Label;
        private System.Windows.Forms.Label Times_Label;
        private System.Windows.Forms.Panel Setting_Panel;
        private System.Windows.Forms.ComboBox Task_ComboBox;
        private System.Windows.Forms.Label Task_Label;
        private System.Windows.Forms.Panel StopWatch_Panel;
        private System.Windows.Forms.Button Reset_Button;
        private System.Windows.Forms.Button StartStop_Button;
        private System.Windows.Forms.Label Time_Label;
        private System.Windows.Forms.TableLayoutPanel Message_TableLayoutPanel;
        private System.Windows.Forms.Label Message_Label;
        private System.Windows.Forms.Label TotalValue_Label;
        private System.Windows.Forms.Label Total_Label;
        private System.Windows.Forms.ComboBox Category_ComboBox;
        private System.Windows.Forms.Label Category_Label;
        private System.Windows.Forms.Button Save_Button;
    }
}

