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
            this.label2 = new System.Windows.Forms.Label();
            this.connectBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.rbStatus = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.mapLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.bossLabel = new System.Windows.Forms.Label();
            this.portNum = new System.Windows.Forms.NumericUpDown();
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
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "Current Map:";
            // 
            // mapLabel
            // 
            this.mapLabel.AutoSize = true;
            this.mapLabel.Location = new System.Drawing.Point(126, 58);
            this.mapLabel.Name = "mapLabel";
            this.mapLabel.Size = new System.Drawing.Size(41, 12);
            this.mapLabel.TabIndex = 8;
            this.mapLabel.Text = "label6";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "Current Boss:";
            // 
            // bossLabel
            // 
            this.bossLabel.AutoSize = true;
            this.bossLabel.Location = new System.Drawing.Point(126, 80);
            this.bossLabel.Name = "bossLabel";
            this.bossLabel.Size = new System.Drawing.Size(41, 12);
            this.bossLabel.TabIndex = 10;
            this.bossLabel.Text = "label8";
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 221);
            this.Controls.Add(this.portNum);
            this.Controls.Add(this.bossLabel);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.mapLabel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.rbStatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.connectBtn);
            this.Controls.Add(this.label2);
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
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label mapLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label bossLabel;
        private System.Windows.Forms.NumericUpDown portNum;
    }
}

