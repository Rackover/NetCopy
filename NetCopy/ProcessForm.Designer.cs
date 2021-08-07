
namespace NetCopy {
    partial class ProcessForm {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.GroupBox BarsGroupBox;
            System.Windows.Forms.PictureBox pictureBox1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessForm));
            this.DownloadsContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.MaxWorkersUpDown = new System.Windows.Forms.NumericUpDown();
            this.MaxWorkerSizeUpDown = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CopyFoldersList = new System.Windows.Forms.CheckedListBox();
            this.SelectTargetsButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.PauseButton = new System.Windows.Forms.Button();
            this.ResumeButton = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            BarsGroupBox = new System.Windows.Forms.GroupBox();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            BarsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxWorkersUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxWorkerSizeUpDown)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            label1.Location = new System.Drawing.Point(453, 131);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(148, 20);
            label1.TabIndex = 2;
            label1.Text = "Max workers";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            label2.Location = new System.Drawing.Point(453, 157);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(148, 20);
            label2.TabIndex = 3;
            label2.Text = "Maximum Worker Size (MB)";
            label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BarsGroupBox
            // 
            BarsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            BarsGroupBox.AutoSize = true;
            BarsGroupBox.Controls.Add(this.DownloadsContainer);
            BarsGroupBox.Location = new System.Drawing.Point(216, 182);
            BarsGroupBox.Name = "BarsGroupBox";
            BarsGroupBox.Size = new System.Drawing.Size(446, 257);
            BarsGroupBox.TabIndex = 4;
            BarsGroupBox.TabStop = false;
            BarsGroupBox.Text = "Downloads";
            // 
            // DownloadsContainer
            // 
            this.DownloadsContainer.AutoScroll = true;
            this.DownloadsContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DownloadsContainer.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.DownloadsContainer.Location = new System.Drawing.Point(3, 16);
            this.DownloadsContainer.MinimumSize = new System.Drawing.Size(43, 0);
            this.DownloadsContainer.Name = "DownloadsContainer";
            this.DownloadsContainer.Size = new System.Drawing.Size(440, 238);
            this.DownloadsContainer.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            pictureBox1.Image = global::NetCopy.Properties.Resources.netcopy;
            pictureBox1.InitialImage = global::NetCopy.Properties.Resources.netcopy;
            pictureBox1.Location = new System.Drawing.Point(10, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(647, 113);
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 10;
            pictureBox1.TabStop = false;
            pictureBox1.WaitOnLoad = true;
            // 
            // MaxWorkersUpDown
            // 
            this.MaxWorkersUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MaxWorkersUpDown.Location = new System.Drawing.Point(606, 131);
            this.MaxWorkersUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MaxWorkersUpDown.Name = "MaxWorkersUpDown";
            this.MaxWorkersUpDown.Size = new System.Drawing.Size(51, 20);
            this.MaxWorkersUpDown.TabIndex = 0;
            this.MaxWorkersUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // MaxWorkerSizeUpDown
            // 
            this.MaxWorkerSizeUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MaxWorkerSizeUpDown.Location = new System.Drawing.Point(606, 157);
            this.MaxWorkerSizeUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.MaxWorkerSizeUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MaxWorkerSizeUpDown.Name = "MaxWorkerSizeUpDown";
            this.MaxWorkerSizeUpDown.Size = new System.Drawing.Size(51, 20);
            this.MaxWorkerSizeUpDown.TabIndex = 1;
            this.MaxWorkerSizeUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.CopyFoldersList);
            this.groupBox1.Location = new System.Drawing.Point(10, 182);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(201, 257);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Progression";
            // 
            // CopyFoldersList
            // 
            this.CopyFoldersList.CausesValidation = false;
            this.CopyFoldersList.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.CopyFoldersList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CopyFoldersList.Enabled = false;
            this.CopyFoldersList.FormattingEnabled = true;
            this.CopyFoldersList.Location = new System.Drawing.Point(3, 16);
            this.CopyFoldersList.Name = "CopyFoldersList";
            this.CopyFoldersList.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.CopyFoldersList.Size = new System.Drawing.Size(195, 238);
            this.CopyFoldersList.TabIndex = 0;
            this.CopyFoldersList.ThreeDCheckBoxes = true;
            // 
            // SelectTargetsButton
            // 
            this.SelectTargetsButton.Image = global::NetCopy.Properties.Resources.SearchFolderOpened_16x;
            this.SelectTargetsButton.Location = new System.Drawing.Point(10, 131);
            this.SelectTargetsButton.Name = "SelectTargetsButton";
            this.SelectTargetsButton.Size = new System.Drawing.Size(195, 45);
            this.SelectTargetsButton.TabIndex = 6;
            this.SelectTargetsButton.Text = "Select target folders...";
            this.SelectTargetsButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.SelectTargetsButton.UseVisualStyleBackColor = true;
            this.SelectTargetsButton.Click += new System.EventHandler(this.SelectTargetsButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Image = global::NetCopy.Properties.Resources.StatusStop_16x;
            this.StopButton.Location = new System.Drawing.Point(388, 131);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(59, 45);
            this.StopButton.TabIndex = 7;
            this.StopButton.Text = "Stop";
            this.StopButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // PauseButton
            // 
            this.PauseButton.Image = global::NetCopy.Properties.Resources.StatusPause_16x;
            this.PauseButton.Location = new System.Drawing.Point(302, 131);
            this.PauseButton.Name = "PauseButton";
            this.PauseButton.Size = new System.Drawing.Size(80, 45);
            this.PauseButton.TabIndex = 8;
            this.PauseButton.Text = "Pause";
            this.PauseButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.PauseButton.UseVisualStyleBackColor = true;
            this.PauseButton.Click += new System.EventHandler(this.PauseButton_Click);
            // 
            // ResumeButton
            // 
            this.ResumeButton.Image = global::NetCopy.Properties.Resources.StatusRun_16x;
            this.ResumeButton.Location = new System.Drawing.Point(216, 131);
            this.ResumeButton.MinimumSize = new System.Drawing.Size(80, 45);
            this.ResumeButton.Name = "ResumeButton";
            this.ResumeButton.Size = new System.Drawing.Size(80, 45);
            this.ResumeButton.TabIndex = 9;
            this.ResumeButton.Text = "Resume";
            this.ResumeButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ResumeButton.UseVisualStyleBackColor = true;
            this.ResumeButton.Click += new System.EventHandler(this.ResumeButton_Click);
            // 
            // ProcessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(672, 449);
            this.Controls.Add(this.ResumeButton);
            this.Controls.Add(pictureBox1);
            this.Controls.Add(this.PauseButton);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.SelectTargetsButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(BarsGroupBox);
            this.Controls.Add(label2);
            this.Controls.Add(label1);
            this.Controls.Add(this.MaxWorkerSizeUpDown);
            this.Controls.Add(this.MaxWorkersUpDown);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProcessForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "NetCopy";
            BarsGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxWorkersUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxWorkerSizeUpDown)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown MaxWorkersUpDown;
        private System.Windows.Forms.NumericUpDown MaxWorkerSizeUpDown;
        private System.Windows.Forms.FlowLayoutPanel DownloadsContainer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox CopyFoldersList;
        private System.Windows.Forms.Button SelectTargetsButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button PauseButton;
        private System.Windows.Forms.Button ResumeButton;
    }
}

