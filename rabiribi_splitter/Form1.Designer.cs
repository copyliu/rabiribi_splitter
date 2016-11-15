namespace rabiribi_splitter
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label2 = new System.Windows.Forms.Label();
            this.connectBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.rbStatus = new System.Windows.Forms.Label();
            this.portNum = new System.Windows.Forms.NumericUpDown();
            this.cbBossStart = new System.Windows.Forms.CheckBox();
            this.cbBossEnd = new System.Windows.Forms.CheckBox();
            this.musicLabel = new System.Windows.Forms.Label();
            this.cbBoss = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.portNum)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "LiveSplit Server Port:";
            // 
            // connectBtn
            // 
            this.connectBtn.Location = new System.Drawing.Point(212, 8);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(75, 23);
            this.connectBtn.TabIndex = 4;
            this.connectBtn.Text = "Connect";
            this.connectBtn.UseVisualStyleBackColor = true;
            this.connectBtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "Rabi-Ribi Status:";
            // 
            // rbStatus
            // 
            this.rbStatus.AutoSize = true;
            this.rbStatus.Location = new System.Drawing.Point(126, 37);
            this.rbStatus.Name = "rbStatus";
            this.rbStatus.Size = new System.Drawing.Size(41, 12);
            this.rbStatus.TabIndex = 6;
            this.rbStatus.Text = "label4";
            // 
            // portNum
            // 
            this.portNum.Location = new System.Drawing.Point(149, 9);
            this.portNum.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.portNum.Name = "portNum";
            this.portNum.Size = new System.Drawing.Size(57, 21);
            this.portNum.TabIndex = 11;
            this.portNum.Value = new decimal(new int[] {
            16834,
            0,
            0,
            0});
            // 
            // cbBossStart
            // 
            this.cbBossStart.AutoSize = true;
            this.cbBossStart.Location = new System.Drawing.Point(15, 53);
            this.cbBossStart.Name = "cbBossStart";
            this.cbBossStart.Size = new System.Drawing.Size(198, 16);
            this.cbBossStart.TabIndex = 12;
            this.cbBossStart.Text = "Split when BOSS music STARTED";
            this.cbBossStart.UseVisualStyleBackColor = true;
            // 
            // cbBossEnd
            // 
            this.cbBossEnd.AutoSize = true;
            this.cbBossEnd.Checked = true;
            this.cbBossEnd.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbBossEnd.Location = new System.Drawing.Point(15, 75);
            this.cbBossEnd.Name = "cbBossEnd";
            this.cbBossEnd.Size = new System.Drawing.Size(192, 16);
            this.cbBossEnd.TabIndex = 13;
            this.cbBossEnd.Text = "Split when BOSS music STOPED";
            this.cbBossEnd.UseVisualStyleBackColor = true;
            // 
            // musicLabel
            // 
            this.musicLabel.AutoSize = true;
            this.musicLabel.Location = new System.Drawing.Point(15, 98);
            this.musicLabel.Name = "musicLabel";
            this.musicLabel.Size = new System.Drawing.Size(41, 12);
            this.musicLabel.TabIndex = 14;
            this.musicLabel.Text = "label1";
            // 
            // cbBoss
            // 
            this.cbBoss.AutoSize = true;
            this.cbBoss.Enabled = false;
            this.cbBoss.Location = new System.Drawing.Point(15, 113);
            this.cbBoss.Name = "cbBoss";
            this.cbBoss.Size = new System.Drawing.Size(138, 16);
            this.cbBoss.TabIndex = 15;
            this.cbBoss.Text = "BOSS music playing!";
            this.cbBoss.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 221);
            this.Controls.Add(this.cbBoss);
            this.Controls.Add(this.musicLabel);
            this.Controls.Add(this.cbBossEnd);
            this.Controls.Add(this.cbBossStart);
            this.Controls.Add(this.portNum);
            this.Controls.Add(this.rbStatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.connectBtn);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Irisu is watching you";
            ((System.ComponentModel.ISupportInitialize)(this.portNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button connectBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label rbStatus;
        private System.Windows.Forms.NumericUpDown portNum;
        private System.Windows.Forms.CheckBox cbBossStart;
        private System.Windows.Forms.CheckBox cbBossEnd;
        private System.Windows.Forms.Label musicLabel;
        private System.Windows.Forms.CheckBox cbBoss;
    }
}

