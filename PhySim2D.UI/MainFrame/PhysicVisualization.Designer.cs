namespace PhySim2D.UI
{
    partial class PhysicVisualization
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
            this.BtnStart = new System.Windows.Forms.Button();
            this.BtnRestart = new System.Windows.Forms.Button();
            this.BtnStep = new System.Windows.Forms.Button();
            this.BtnQuit = new System.Windows.Forms.Button();
            this.lblTime = new System.Windows.Forms.Label();
            this.ChckListBxDebugFlags = new System.Windows.Forms.CheckedListBox();
            this.DebugScene = new PhySim2D.UI.Components.PhysDebugViz();
            this.SuspendLayout();
            // 
            // BtnStart
            // 
            this.BtnStart.Location = new System.Drawing.Point(835, 318);
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(75, 23);
            this.BtnStart.TabIndex = 0;
            this.BtnStart.Text = "Start";
            this.BtnStart.UseVisualStyleBackColor = true;
            this.BtnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // BtnRestart
            // 
            this.BtnRestart.Location = new System.Drawing.Point(835, 376);
            this.BtnRestart.Name = "BtnRestart";
            this.BtnRestart.Size = new System.Drawing.Size(75, 23);
            this.BtnRestart.TabIndex = 1;
            this.BtnRestart.Text = "Restart";
            this.BtnRestart.UseVisualStyleBackColor = true;
            // 
            // BtnStep
            // 
            this.BtnStep.Location = new System.Drawing.Point(835, 347);
            this.BtnStep.Name = "BtnStep";
            this.BtnStep.Size = new System.Drawing.Size(75, 23);
            this.BtnStep.TabIndex = 2;
            this.BtnStep.Text = "Step";
            this.BtnStep.UseVisualStyleBackColor = true;
            this.BtnStep.Click += new System.EventHandler(this.BtnStep_Click);
            // 
            // BtnQuit
            // 
            this.BtnQuit.Location = new System.Drawing.Point(835, 405);
            this.BtnQuit.Name = "BtnQuit";
            this.BtnQuit.Size = new System.Drawing.Size(75, 23);
            this.BtnQuit.TabIndex = 3;
            this.BtnQuit.Text = "Quit";
            this.BtnQuit.UseVisualStyleBackColor = true;
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(848, 302);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(47, 13);
            this.lblTime.TabIndex = 4;
            this.lblTime.Text = "0.000ms";
            // 
            // ChckListBxDebugFlags
            // 
            this.ChckListBxDebugFlags.FormattingEnabled = true;
            this.ChckListBxDebugFlags.Location = new System.Drawing.Point(778, 130);
            this.ChckListBxDebugFlags.Name = "ChckListBxDebugFlags";
            this.ChckListBxDebugFlags.Size = new System.Drawing.Size(157, 169);
            this.ChckListBxDebugFlags.TabIndex = 6;
            this.ChckListBxDebugFlags.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ChckListBxDebugFlags_ItemCheck);
            // 
            // DebugScene
            // 
            this.DebugScene.BackColor = System.Drawing.Color.Black;
            this.DebugScene.ForeColor = System.Drawing.SystemColors.ControlText;
            this.DebugScene.Location = new System.Drawing.Point(12, 12);
            this.DebugScene.Name = "DebugScene";
            this.DebugScene.Size = new System.Drawing.Size(760, 528);
            this.DebugScene.TabIndex = 5;
            this.DebugScene.TimeStep += new System.EventHandler<Components.TimeStepEventArgs>(this.DebugScene_TimeStep);
            // 
            // PhysicVisualization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(947, 552);
            this.Controls.Add(this.ChckListBxDebugFlags);
            this.Controls.Add(this.DebugScene);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.BtnQuit);
            this.Controls.Add(this.BtnStep);
            this.Controls.Add(this.BtnRestart);
            this.Controls.Add(this.BtnStart);
            this.Name = "PhysicVisualization";
            this.Text = "PhysicEngine :D";
            this.Load += new System.EventHandler(this.PhysicVisualization_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnStart;
        private System.Windows.Forms.Button BtnRestart;
        private System.Windows.Forms.Button BtnStep;
        private System.Windows.Forms.Button BtnQuit;
        private System.Windows.Forms.Label lblTime;
        private Components.PhysDebugViz DebugScene;
        private System.Windows.Forms.CheckedListBox ChckListBxDebugFlags;
    }
}

