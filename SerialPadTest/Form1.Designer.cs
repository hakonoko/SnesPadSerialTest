namespace SerialPadTest {
    partial class Form1 {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
            this.comboBoxSelectPort = new System.Windows.Forms.ComboBox();
            this.Button_Connect = new System.Windows.Forms.Button();
            this.Button_UP = new System.Windows.Forms.Button();
            this.Button_LEFT = new System.Windows.Forms.Button();
            this.Button_RIGHT = new System.Windows.Forms.Button();
            this.Button_DOWN = new System.Windows.Forms.Button();
            this.Button_SELECT = new System.Windows.Forms.Button();
            this.Button_START = new System.Windows.Forms.Button();
            this.button_Disconnect = new System.Windows.Forms.Button();
            this.Button_X = new System.Windows.Forms.Button();
            this.Button_Y = new System.Windows.Forms.Button();
            this.Button_A = new System.Windows.Forms.Button();
            this.Button_B = new System.Windows.Forms.Button();
            this.Button_R = new System.Windows.Forms.Button();
            this.Button_L = new System.Windows.Forms.Button();
            this.button_ReloadSeralPort = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.XInputConnectionLabel = new System.Windows.Forms.Label();
            this.XInputButtonLabel = new System.Windows.Forms.Label();
            this.lblTL = new System.Windows.Forms.Label();
            this.lblTR = new System.Windows.Forms.Label();
            this.lblBR = new System.Windows.Forms.Label();
            this.lblBL = new System.Windows.Forms.Label();
            this.lblWeight = new System.Windows.Forms.Label();
            this.WiimoteConnect = new System.Windows.Forms.Button();
            this.ResetZeroPoint = new System.Windows.Forms.Button();
            this.BWBRect = new System.Windows.Forms.PictureBox();
            this.BWBPosition = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.BWBRect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BWBPosition)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxSelectPort
            // 
            this.comboBoxSelectPort.FormattingEnabled = true;
            this.comboBoxSelectPort.Location = new System.Drawing.Point(12, 12);
            this.comboBoxSelectPort.Name = "comboBoxSelectPort";
            this.comboBoxSelectPort.Size = new System.Drawing.Size(121, 20);
            this.comboBoxSelectPort.TabIndex = 0;
            // 
            // Button_Connect
            // 
            this.Button_Connect.Location = new System.Drawing.Point(139, 9);
            this.Button_Connect.Name = "Button_Connect";
            this.Button_Connect.Size = new System.Drawing.Size(75, 23);
            this.Button_Connect.TabIndex = 1;
            this.Button_Connect.Text = "Connect";
            this.Button_Connect.UseVisualStyleBackColor = true;
            this.Button_Connect.Click += new System.EventHandler(this.Button_Connect_Click);
            // 
            // Button_UP
            // 
            this.Button_UP.Location = new System.Drawing.Point(49, 125);
            this.Button_UP.Name = "Button_UP";
            this.Button_UP.Size = new System.Drawing.Size(75, 23);
            this.Button_UP.TabIndex = 2;
            this.Button_UP.Text = "UP";
            this.Button_UP.UseVisualStyleBackColor = true;
            this.Button_UP.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_UP_MouseDown);
            this.Button_UP.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_UP_MouseDown);
            // 
            // Button_LEFT
            // 
            this.Button_LEFT.Location = new System.Drawing.Point(12, 154);
            this.Button_LEFT.Name = "Button_LEFT";
            this.Button_LEFT.Size = new System.Drawing.Size(75, 23);
            this.Button_LEFT.TabIndex = 3;
            this.Button_LEFT.Text = "LEFT";
            this.Button_LEFT.UseVisualStyleBackColor = true;
            this.Button_LEFT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_LEFT_MouseDown);
            this.Button_LEFT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_LEFT_MouseDown);
            // 
            // Button_RIGHT
            // 
            this.Button_RIGHT.Location = new System.Drawing.Point(93, 154);
            this.Button_RIGHT.Name = "Button_RIGHT";
            this.Button_RIGHT.Size = new System.Drawing.Size(75, 23);
            this.Button_RIGHT.TabIndex = 4;
            this.Button_RIGHT.Text = "RIGHT";
            this.Button_RIGHT.UseVisualStyleBackColor = true;
            this.Button_RIGHT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_RIGHT_MouseDown);
            this.Button_RIGHT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_RIGHT_MouseDown);
            // 
            // Button_DOWN
            // 
            this.Button_DOWN.Location = new System.Drawing.Point(49, 183);
            this.Button_DOWN.Name = "Button_DOWN";
            this.Button_DOWN.Size = new System.Drawing.Size(75, 23);
            this.Button_DOWN.TabIndex = 5;
            this.Button_DOWN.Text = "DOWN";
            this.Button_DOWN.UseVisualStyleBackColor = true;
            this.Button_DOWN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_DOWN_MouseDown);
            this.Button_DOWN.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_DOWN_MouseDown);
            // 
            // Button_SELECT
            // 
            this.Button_SELECT.Location = new System.Drawing.Point(212, 154);
            this.Button_SELECT.Name = "Button_SELECT";
            this.Button_SELECT.Size = new System.Drawing.Size(75, 23);
            this.Button_SELECT.TabIndex = 6;
            this.Button_SELECT.Text = "SELECT";
            this.Button_SELECT.UseVisualStyleBackColor = true;
            this.Button_SELECT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_SELECT_Click);
            this.Button_SELECT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_SELECT_Click);
            // 
            // Button_START
            // 
            this.Button_START.Location = new System.Drawing.Point(293, 154);
            this.Button_START.Name = "Button_START";
            this.Button_START.Size = new System.Drawing.Size(75, 23);
            this.Button_START.TabIndex = 7;
            this.Button_START.Text = "START";
            this.Button_START.UseVisualStyleBackColor = true;
            this.Button_START.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_START_Click);
            this.Button_START.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_START_Click);
            // 
            // button_Disconnect
            // 
            this.button_Disconnect.Location = new System.Drawing.Point(220, 9);
            this.button_Disconnect.Name = "button_Disconnect";
            this.button_Disconnect.Size = new System.Drawing.Size(75, 23);
            this.button_Disconnect.TabIndex = 8;
            this.button_Disconnect.Text = "Disconnect";
            this.button_Disconnect.UseVisualStyleBackColor = true;
            this.button_Disconnect.Click += new System.EventHandler(this.Button_Disconnect_Click);
            // 
            // Button_X
            // 
            this.Button_X.Location = new System.Drawing.Point(455, 125);
            this.Button_X.Name = "Button_X";
            this.Button_X.Size = new System.Drawing.Size(75, 23);
            this.Button_X.TabIndex = 9;
            this.Button_X.Text = "X";
            this.Button_X.UseVisualStyleBackColor = true;
            this.Button_X.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_X_Click);
            this.Button_X.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_X_Click);
            // 
            // Button_Y
            // 
            this.Button_Y.Location = new System.Drawing.Point(414, 154);
            this.Button_Y.Name = "Button_Y";
            this.Button_Y.Size = new System.Drawing.Size(75, 23);
            this.Button_Y.TabIndex = 10;
            this.Button_Y.Text = "Y";
            this.Button_Y.UseVisualStyleBackColor = true;
            this.Button_Y.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_Y_Click);
            this.Button_Y.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_Y_Click);
            // 
            // Button_A
            // 
            this.Button_A.Location = new System.Drawing.Point(495, 154);
            this.Button_A.Name = "Button_A";
            this.Button_A.Size = new System.Drawing.Size(75, 23);
            this.Button_A.TabIndex = 11;
            this.Button_A.Text = "A";
            this.Button_A.UseVisualStyleBackColor = true;
            this.Button_A.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_A_Click);
            this.Button_A.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_A_Click);
            // 
            // Button_B
            // 
            this.Button_B.Location = new System.Drawing.Point(455, 183);
            this.Button_B.Name = "Button_B";
            this.Button_B.Size = new System.Drawing.Size(75, 23);
            this.Button_B.TabIndex = 12;
            this.Button_B.Text = "B";
            this.Button_B.UseVisualStyleBackColor = true;
            this.Button_B.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_B_Click);
            this.Button_B.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_B_Click);
            // 
            // Button_R
            // 
            this.Button_R.Location = new System.Drawing.Point(355, 96);
            this.Button_R.Name = "Button_R";
            this.Button_R.Size = new System.Drawing.Size(75, 23);
            this.Button_R.TabIndex = 13;
            this.Button_R.Text = "R";
            this.Button_R.UseVisualStyleBackColor = true;
            this.Button_R.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_R_Click);
            this.Button_R.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_R_Click);
            // 
            // Button_L
            // 
            this.Button_L.Location = new System.Drawing.Point(153, 96);
            this.Button_L.Name = "Button_L";
            this.Button_L.Size = new System.Drawing.Size(75, 23);
            this.Button_L.TabIndex = 14;
            this.Button_L.Text = "L";
            this.Button_L.UseVisualStyleBackColor = true;
            this.Button_L.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_L_Click);
            this.Button_L.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_L_Click);
            // 
            // button_ReloadSeralPort
            // 
            this.button_ReloadSeralPort.Location = new System.Drawing.Point(355, 9);
            this.button_ReloadSeralPort.Name = "button_ReloadSeralPort";
            this.button_ReloadSeralPort.Size = new System.Drawing.Size(75, 23);
            this.button_ReloadSeralPort.TabIndex = 15;
            this.button_ReloadSeralPort.Text = "Reload";
            this.button_ReloadSeralPort.UseVisualStyleBackColor = true;
            this.button_ReloadSeralPort.Click += new System.EventHandler(this.button_ReloadSeralPort_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 236);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(558, 202);
            this.richTextBox1.TabIndex = 16;
            this.richTextBox1.Text = "";
            // 
            // XInputConnectionLabel
            // 
            this.XInputConnectionLabel.AutoSize = true;
            this.XInputConnectionLabel.Location = new System.Drawing.Point(579, 15);
            this.XInputConnectionLabel.Name = "XInputConnectionLabel";
            this.XInputConnectionLabel.Size = new System.Drawing.Size(121, 12);
            this.XInputConnectionLabel.TabIndex = 17;
            this.XInputConnectionLabel.Text = "XInputConnectionLabel";
            // 
            // XInputButtonLabel
            // 
            this.XInputButtonLabel.AutoSize = true;
            this.XInputButtonLabel.Location = new System.Drawing.Point(579, 38);
            this.XInputButtonLabel.Name = "XInputButtonLabel";
            this.XInputButtonLabel.Size = new System.Drawing.Size(98, 12);
            this.XInputButtonLabel.TabIndex = 18;
            this.XInputButtonLabel.Text = "XInputButtonLabel";
            // 
            // lblTL
            // 
            this.lblTL.AutoSize = true;
            this.lblTL.Location = new System.Drawing.Point(579, 165);
            this.lblTL.Name = "lblTL";
            this.lblTL.Size = new System.Drawing.Size(30, 12);
            this.lblTL.TabIndex = 19;
            this.lblTL.Text = "lblTL";
            // 
            // lblTR
            // 
            this.lblTR.AutoSize = true;
            this.lblTR.Location = new System.Drawing.Point(645, 165);
            this.lblTR.Name = "lblTR";
            this.lblTR.Size = new System.Drawing.Size(32, 12);
            this.lblTR.TabIndex = 19;
            this.lblTR.Text = "lblTR";
            // 
            // lblBR
            // 
            this.lblBR.AutoSize = true;
            this.lblBR.Location = new System.Drawing.Point(645, 194);
            this.lblBR.Name = "lblBR";
            this.lblBR.Size = new System.Drawing.Size(33, 12);
            this.lblBR.TabIndex = 19;
            this.lblBR.Text = "lblBR";
            // 
            // lblBL
            // 
            this.lblBL.AutoSize = true;
            this.lblBL.Location = new System.Drawing.Point(579, 194);
            this.lblBL.Name = "lblBL";
            this.lblBL.Size = new System.Drawing.Size(31, 12);
            this.lblBL.TabIndex = 19;
            this.lblBL.Text = "lblBL";
            // 
            // lblWeight
            // 
            this.lblWeight.AutoSize = true;
            this.lblWeight.Location = new System.Drawing.Point(605, 139);
            this.lblWeight.Name = "lblWeight";
            this.lblWeight.Size = new System.Drawing.Size(51, 12);
            this.lblWeight.TabIndex = 19;
            this.lblWeight.Text = "lblWeight";
            // 
            // WiimoteConnect
            // 
            this.WiimoteConnect.Location = new System.Drawing.Point(581, 102);
            this.WiimoteConnect.Name = "WiimoteConnect";
            this.WiimoteConnect.Size = new System.Drawing.Size(96, 23);
            this.WiimoteConnect.TabIndex = 20;
            this.WiimoteConnect.Text = "WiimoteConnect";
            this.WiimoteConnect.UseVisualStyleBackColor = true;
            this.WiimoteConnect.Click += new System.EventHandler(this.WiimoteConnect_Click);
            // 
            // ResetZeroPoint
            // 
            this.ResetZeroPoint.Location = new System.Drawing.Point(683, 102);
            this.ResetZeroPoint.Name = "ResetZeroPoint";
            this.ResetZeroPoint.Size = new System.Drawing.Size(96, 23);
            this.ResetZeroPoint.TabIndex = 21;
            this.ResetZeroPoint.Text = "ResetZeroPoint";
            this.ResetZeroPoint.UseVisualStyleBackColor = true;
            this.ResetZeroPoint.Click += new System.EventHandler(this.ResetZeroPoint_Click);
            // 
            // BWBRect
            // 
            this.BWBRect.BackColor = System.Drawing.SystemColors.ControlDark;
            this.BWBRect.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.BWBRect.Location = new System.Drawing.Point(581, 236);
            this.BWBRect.Name = "BWBRect";
            this.BWBRect.Size = new System.Drawing.Size(200, 100);
            this.BWBRect.TabIndex = 22;
            this.BWBRect.TabStop = false;
            // 
            // BWBPosition
            // 
            this.BWBPosition.Location = new System.Drawing.Point(673, 274);
            this.BWBPosition.Name = "BWBPosition";
            this.BWBPosition.Size = new System.Drawing.Size(10, 10);
            this.BWBPosition.TabIndex = 23;
            this.BWBPosition.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BWBPosition);
            this.Controls.Add(this.BWBRect);
            this.Controls.Add(this.ResetZeroPoint);
            this.Controls.Add(this.WiimoteConnect);
            this.Controls.Add(this.lblBR);
            this.Controls.Add(this.lblTR);
            this.Controls.Add(this.lblWeight);
            this.Controls.Add(this.lblBL);
            this.Controls.Add(this.lblTL);
            this.Controls.Add(this.XInputButtonLabel);
            this.Controls.Add(this.XInputConnectionLabel);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button_ReloadSeralPort);
            this.Controls.Add(this.Button_L);
            this.Controls.Add(this.Button_R);
            this.Controls.Add(this.Button_B);
            this.Controls.Add(this.Button_A);
            this.Controls.Add(this.Button_Y);
            this.Controls.Add(this.Button_X);
            this.Controls.Add(this.button_Disconnect);
            this.Controls.Add(this.Button_START);
            this.Controls.Add(this.Button_SELECT);
            this.Controls.Add(this.Button_DOWN);
            this.Controls.Add(this.Button_RIGHT);
            this.Controls.Add(this.Button_LEFT);
            this.Controls.Add(this.Button_UP);
            this.Controls.Add(this.Button_Connect);
            this.Controls.Add(this.comboBoxSelectPort);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.BWBRect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BWBPosition)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxSelectPort;
        private System.Windows.Forms.Button Button_Connect;
        private System.Windows.Forms.Button Button_UP;
        private System.Windows.Forms.Button Button_LEFT;
        private System.Windows.Forms.Button Button_RIGHT;
        private System.Windows.Forms.Button Button_DOWN;
        private System.Windows.Forms.Button Button_SELECT;
        private System.Windows.Forms.Button Button_START;
        private System.Windows.Forms.Button button_Disconnect;
        private System.Windows.Forms.Button Button_X;
        private System.Windows.Forms.Button Button_Y;
        private System.Windows.Forms.Button Button_A;
        private System.Windows.Forms.Button Button_B;
        private System.Windows.Forms.Button Button_R;
        private System.Windows.Forms.Button Button_L;
        private System.Windows.Forms.Button button_ReloadSeralPort;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label XInputConnectionLabel;
        private System.Windows.Forms.Label XInputButtonLabel;
        private System.Windows.Forms.Label lblTL;
        private System.Windows.Forms.Label lblTR;
        private System.Windows.Forms.Label lblBR;
        private System.Windows.Forms.Label lblBL;
        private System.Windows.Forms.Label lblWeight;
        private System.Windows.Forms.Button WiimoteConnect;
        private System.Windows.Forms.Button ResetZeroPoint;
        private System.Windows.Forms.PictureBox BWBRect;
        private System.Windows.Forms.PictureBox BWBPosition;
    }
}

