namespace AsyncDeadlock
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
            this.btn_AsyncDeadlock = new System.Windows.Forms.Button();
            this.btn_ConfigureWait = new System.Windows.Forms.Button();
            this.btn_async_atw = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_AsyncDeadlock
            // 
            this.btn_AsyncDeadlock.Location = new System.Drawing.Point(75, 58);
            this.btn_AsyncDeadlock.Name = "btn_AsyncDeadlock";
            this.btn_AsyncDeadlock.Size = new System.Drawing.Size(160, 23);
            this.btn_AsyncDeadlock.TabIndex = 0;
            this.btn_AsyncDeadlock.Text = "Async Blocking (deadlocks)";
            this.btn_AsyncDeadlock.UseVisualStyleBackColor = true;
            this.btn_AsyncDeadlock.Click += new System.EventHandler(this.Btn_AsyncDeadlock_Click);
            // 
            // btn_ConfigureWait
            // 
            this.btn_ConfigureWait.Location = new System.Drawing.Point(77, 98);
            this.btn_ConfigureWait.Name = "btn_ConfigureWait";
            this.btn_ConfigureWait.Size = new System.Drawing.Size(158, 36);
            this.btn_ConfigureWait.TabIndex = 1;
            this.btn_ConfigureWait.Text = "Async ConfigureAwait(false)";
            this.btn_ConfigureWait.UseVisualStyleBackColor = true;
            this.btn_ConfigureWait.Click += new System.EventHandler(this.Btn_ConfigureWait_Click);
            // 
            // btn_async_atw
            // 
            this.btn_async_atw.Location = new System.Drawing.Point(78, 149);
            this.btn_async_atw.Name = "btn_async_atw";
            this.btn_async_atw.Size = new System.Drawing.Size(157, 23);
            this.btn_async_atw.TabIndex = 2;
            this.btn_async_atw.Text = "Async all-the-way";
            this.btn_async_atw.UseVisualStyleBackColor = true;
            this.btn_async_atw.Click += new System.EventHandler(this.Btn_async_atw_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 450);
            this.Controls.Add(this.btn_async_atw);
            this.Controls.Add(this.btn_ConfigureWait);
            this.Controls.Add(this.btn_AsyncDeadlock);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_AsyncDeadlock;
        private System.Windows.Forms.Button btn_ConfigureWait;
        private System.Windows.Forms.Button btn_async_atw;
    }
}

