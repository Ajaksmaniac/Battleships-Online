namespace BattleshipsPlayer1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStart = new System.Windows.Forms.Button();
            this.lbIP = new System.Windows.Forms.Label();
            this.pnlBoard = new System.Windows.Forms.Panel();
            this.pnlOponent = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbLogBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(15, 415);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(166, 23);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start looking for an opponent";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbIP
            // 
            this.lbIP.AutoSize = true;
            this.lbIP.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbIP.Location = new System.Drawing.Point(12, 399);
            this.lbIP.Name = "lbIP";
            this.lbIP.Size = new System.Drawing.Size(223, 13);
            this.lbIP.TabIndex = 3;
            this.lbIP.Text = "Opponent should connect to this IP address:  ";
            // 
            // pnlBoard
            // 
            this.pnlBoard.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pnlBoard.Location = new System.Drawing.Point(15, 12);
            this.pnlBoard.Name = "pnlBoard";
            this.pnlBoard.Size = new System.Drawing.Size(239, 222);
            this.pnlBoard.TabIndex = 4;
            this.pnlBoard.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlBoard_Paint);
            this.pnlBoard.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlBoard_MouseMove);
            // 
            // pnlOponent
            // 
            this.pnlOponent.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pnlOponent.Location = new System.Drawing.Point(539, 12);
            this.pnlOponent.Name = "pnlOponent";
            this.pnlOponent.Size = new System.Drawing.Size(249, 222);
            this.pnlOponent.TabIndex = 5;
            this.pnlOponent.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlOponent_Paint);
            this.pnlOponent.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlOponent_MouseMove);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label1.Location = new System.Drawing.Point(536, 237);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Oponent Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label2.Location = new System.Drawing.Point(624, 237);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "NONE";
            // 
            // tbUsername
            // 
            this.tbUsername.Location = new System.Drawing.Point(104, 342);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(100, 20);
            this.tbUsername.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(12, 345);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Enter Username:";
            // 
            // tbLogBox
            // 
            this.tbLogBox.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tbLogBox.ForeColor = System.Drawing.Color.Green;
            this.tbLogBox.Location = new System.Drawing.Point(279, 12);
            this.tbLogBox.Multiline = true;
            this.tbLogBox.Name = "tbLogBox";
            this.tbLogBox.ReadOnly = true;
            this.tbLogBox.Size = new System.Drawing.Size(233, 222);
            this.tbLogBox.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GrayText;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tbLogBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbUsername);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlOponent);
            this.Controls.Add(this.pnlBoard);
            this.Controls.Add(this.lbIP);
            this.Controls.Add(this.btnStart);
            this.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lbIP;
        private System.Windows.Forms.Panel pnlBoard;
        private System.Windows.Forms.Panel pnlOponent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbLogBox;
    }
}

