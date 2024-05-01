using static SiteClicker_Parser.SettingsStorage;
using static SiteClicker_Parser.Logger;
using static SiteClicker_Parser.WebDriverExtensions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiteClicker_Parser
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
            }
            catch (System.Exception ex)
            {
                Task.Run(() => LogInfo(ex?.ToString()));
                Task.Run(() => LogInfo(ex?.Message));
                Task.Run(() => LogInfo(ex?.InnerException?.ToString()));
            }
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.StartButton = new System.Windows.Forms.Button();
            this.TimeBox = new System.Windows.Forms.TextBox();
            this.TimeLabel = new System.Windows.Forms.Label();
            this.DebugCheckBox = new System.Windows.Forms.CheckBox();
            this.DebugHint = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(8, 12);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(361, 126);
            this.StartButton.TabIndex = 0;
            this.StartButton.Text = "Start!";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_ClickAsync);
            // 
            // TimeBox
            // 
            this.TimeBox.Location = new System.Drawing.Point(340, 141);
            this.TimeBox.Name = "TimeBox";
            this.TimeBox.Size = new System.Drawing.Size(29, 26);
            this.TimeBox.TabIndex = 1;
            this.TimeBox.Text = "3";
            // 
            // TimeLabel
            // 
            this.TimeLabel.AutoSize = true;
            this.TimeLabel.Location = new System.Drawing.Point(4, 147);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(322, 20);
            this.TimeLabel.TabIndex = 2;
            this.TimeLabel.Text = "Time to repeat request, in minutes (max: 10):";
            // 
            // DebugCheckBox
            // 
            this.DebugCheckBox.AutoSize = true;
            this.DebugCheckBox.Location = new System.Drawing.Point(8, 173);
            this.DebugCheckBox.Name = "DebugCheckBox";
            this.DebugCheckBox.Size = new System.Drawing.Size(354, 24);
            this.DebugCheckBox.TabIndex = 3;
            this.DebugCheckBox.Text = "Debug (Display Chrome window and console)";
            this.DebugCheckBox.UseVisualStyleBackColor = true;
            this.DebugCheckBox.CheckedChanged += new System.EventHandler(this.DebugCheckBox_CheckedChanged);
            // 
            // DebugHint
            // 
            this.DebugHint.AutoPopDelay = 5000;
            this.DebugHint.InitialDelay = 100;
            this.DebugHint.IsBalloon = true;
            this.DebugHint.ReshowDelay = 100;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 200);
            this.Controls.Add(this.DebugCheckBox);
            this.Controls.Add(this.TimeLabel);
            this.Controls.Add(this.TimeBox);
            this.Controls.Add(this.StartButton);
            this.MaximumSize = new System.Drawing.Size(399, 256);
            this.MinimumSize = new System.Drawing.Size(399, 256);
            this.Name = "MainForm";
            this.Text = "Mine magic app :3";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.TextBox TimeBox;
        private System.Windows.Forms.Label TimeLabel;
        private System.Windows.Forms.CheckBox DebugCheckBox;
        private System.Windows.Forms.ToolTip DebugHint;
    }
}

