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
            this.cbComputer = new System.Windows.Forms.CheckBox();
            this.cbBoss1 = new System.Windows.Forms.CheckBox();
            this.cbBoss3 = new System.Windows.Forms.CheckBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.t1 = new System.Windows.Forms.Label();
            this.t2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbSideCh = new System.Windows.Forms.CheckBox();
            this.debugLog = new System.Windows.Forms.TextBox();
            this.debugArea = new System.Windows.Forms.CheckBox();
            this.cbASG = new System.Windows.Forms.CheckBox();
            this.cbTM = new System.Windows.Forms.CheckBox();
            this.cbIGT = new System.Windows.Forms.CheckBox();
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
            this.cbBossStart.Size = new System.Drawing.Size(192, 16);
            this.cbBossStart.TabIndex = 12;
            this.cbBossStart.Text = "Split when BOSS music STARTS";
            this.cbBossStart.UseVisualStyleBackColor = true;
            // 
            // cbBossEnd
            // 
            this.cbBossEnd.AutoSize = true;
            this.cbBossEnd.Checked = true;
            this.cbBossEnd.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbBossEnd.Location = new System.Drawing.Point(15, 75);
            this.cbBossEnd.Name = "cbBossEnd";
            this.cbBossEnd.Size = new System.Drawing.Size(180, 16);
            this.cbBossEnd.TabIndex = 13;
            this.cbBossEnd.Text = "Split when BOSS music ENDS";
            this.cbBossEnd.UseVisualStyleBackColor = true;
            // 
            // musicLabel
            // 
            this.musicLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.musicLabel.AutoSize = true;
            this.musicLabel.Location = new System.Drawing.Point(13, 237);
            this.musicLabel.Name = "musicLabel";
            this.musicLabel.Size = new System.Drawing.Size(41, 12);
            this.musicLabel.TabIndex = 14;
            this.musicLabel.Text = "label1";
            // 
            // cbBoss
            // 
            this.cbBoss.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbBoss.AutoSize = true;
            this.cbBoss.Enabled = false;
            this.cbBoss.Location = new System.Drawing.Point(15, 252);
            this.cbBoss.Name = "cbBoss";
            this.cbBoss.Size = new System.Drawing.Size(84, 16);
            this.cbBoss.TabIndex = 15;
            this.cbBoss.Text = "BOSS event";
            this.cbBoss.UseVisualStyleBackColor = true;
            // 
            // cbComputer
            // 
            this.cbComputer.AutoSize = true;
            this.cbComputer.Checked = true;
            this.cbComputer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbComputer.Location = new System.Drawing.Point(15, 97);
            this.cbComputer.Name = "cbComputer";
            this.cbComputer.Size = new System.Drawing.Size(216, 16);
            this.cbComputer.TabIndex = 16;
            this.cbComputer.Text = "Split when the computer is found";
            this.cbComputer.UseVisualStyleBackColor = true;
            // 
            // cbBoss1
            // 
            this.cbBoss1.AutoSize = true;
            this.cbBoss1.Checked = true;
            this.cbBoss1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbBoss1.Location = new System.Drawing.Point(15, 119);
            this.cbBoss1.Name = "cbBoss1";
            this.cbBoss1.Size = new System.Drawing.Size(168, 16);
            this.cbBoss1.TabIndex = 17;
            this.cbBoss1.Text = "Split when Miru despawns";
            this.cbBoss1.UseVisualStyleBackColor = true;
            // 
            // cbBoss3
            // 
            this.cbBoss3.AutoSize = true;
            this.cbBoss3.Location = new System.Drawing.Point(402, 141);
            this.cbBoss3.Name = "cbBoss3";
            this.cbBoss3.Size = new System.Drawing.Size(276, 16);
            this.cbBoss3.TabIndex = 19;
            this.cbBoss3.Text = "Split when Noah 3 HP = 1, ignore her music";
            this.cbBoss3.UseVisualStyleBackColor = true;
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(279, 256);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(41, 12);
            this.linkLabel1.TabIndex = 20;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "GitHub";
            // 
            // t1
            // 
            this.t1.AutoSize = true;
            this.t1.Location = new System.Drawing.Point(498, 37);
            this.t1.Name = "t1";
            this.t1.Size = new System.Drawing.Size(41, 12);
            this.t1.TabIndex = 21;
            this.t1.Text = "label1";
            // 
            // t2
            // 
            this.t2.AutoSize = true;
            this.t2.Location = new System.Drawing.Point(694, 36);
            this.t2.Name = "t2";
            this.t2.Size = new System.Drawing.Size(41, 12);
            this.t2.TabIndex = 22;
            this.t2.Text = "label4";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(232, 256);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 24;
            this.label4.Text = "v0.2.0";
            // 
            // cbSideCh
            // 
            this.cbSideCh.AutoSize = true;
            this.cbSideCh.Checked = true;
            this.cbSideCh.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSideCh.Location = new System.Drawing.Point(15, 141);
            this.cbSideCh.Name = "cbSideCh";
            this.cbSideCh.Size = new System.Drawing.Size(162, 16);
            this.cbSideCh.TabIndex = 25;
            this.cbSideCh.Text = "Ignore the Side Chapter";
            this.cbSideCh.UseVisualStyleBackColor = true;
            // 
            // debugLog
            // 
            this.debugLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.debugLog.Location = new System.Drawing.Point(402, 163);
            this.debugLog.MinimumSize = new System.Drawing.Size(10, 10);
            this.debugLog.Multiline = true;
            this.debugLog.Name = "debugLog";
            this.debugLog.Size = new System.Drawing.Size(10, 86);
            this.debugLog.TabIndex = 26;
            // 
            // debugArea
            // 
            this.debugArea.AutoSize = true;
            this.debugArea.Location = new System.Drawing.Point(500, 14);
            this.debugArea.Name = "debugArea";
            this.debugArea.Size = new System.Drawing.Size(78, 16);
            this.debugArea.TabIndex = 27;
            this.debugArea.Text = "debugArea";
            this.debugArea.UseVisualStyleBackColor = true;
            // 
            // cbASG
            // 
            this.cbASG.AutoSize = true;
            this.cbASG.Checked = true;
            this.cbASG.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbASG.Location = new System.Drawing.Point(15, 163);
            this.cbASG.Name = "cbASG";
            this.cbASG.Size = new System.Drawing.Size(306, 16);
            this.cbASG.TabIndex = 28;
            this.cbASG.Text = "Ignore the next \"SUDDEN DEATH\" (Ignore Alius I)";
            this.cbASG.UseVisualStyleBackColor = true;
            // 
            // cbTM
            // 
            this.cbTM.AutoSize = true;
            this.cbTM.Checked = true;
            this.cbTM.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTM.Location = new System.Drawing.Point(15, 185);
            this.cbTM.Name = "cbTM";
            this.cbTM.Size = new System.Drawing.Size(282, 16);
            this.cbTM.TabIndex = 29;
            this.cbTM.Text = "Split when Town Member +2 or Nixie despawns";
            this.cbTM.UseVisualStyleBackColor = true;
            // 
            // cbIGT
            // 
            this.cbIGT.AutoSize = true;
            this.cbIGT.Location = new System.Drawing.Point(15, 207);
            this.cbIGT.Name = "cbIGT";
            this.cbIGT.Size = new System.Drawing.Size(132, 16);
            this.cbIGT.TabIndex = 30;
            this.cbIGT.Text = "Track In-Game Time";
            this.cbIGT.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 280);
            this.Controls.Add(this.cbIGT);
            this.Controls.Add(this.cbTM);
            this.Controls.Add(this.cbASG);
            this.Controls.Add(this.debugArea);
            this.Controls.Add(this.debugLog);
            this.Controls.Add(this.cbSideCh);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.t2);
            this.Controls.Add(this.t1);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.cbBoss3);
            this.Controls.Add(this.cbBoss1);
            this.Controls.Add(this.cbComputer);
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
        private System.Windows.Forms.CheckBox cbComputer;
        private System.Windows.Forms.CheckBox cbBoss1;
        private System.Windows.Forms.CheckBox cbBoss3;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label t1;
        private System.Windows.Forms.Label t2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbSideCh;
        private System.Windows.Forms.TextBox debugLog;
        private System.Windows.Forms.CheckBox debugArea;
        private System.Windows.Forms.CheckBox cbASG;
        private System.Windows.Forms.CheckBox cbTM;
        private System.Windows.Forms.CheckBox cbIGT;
    }
}

