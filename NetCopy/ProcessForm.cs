using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetCopy {
    public partial class ProcessForm : Form {

        Dictionary<FileInfo, DownloadControl> associatedBars = new Dictionary<FileInfo, DownloadControl>();

        Stack<DownloadControl> pooledControls = new Stack<DownloadControl>();
        List<DownloadControl> activeControls = new List<DownloadControl>();
        MultiCopier copier;
        Task copyTask;

        struct DownloadControl {
            public ProgressBar Bar;
            public GroupBox Group;
            public string Name {
                get {
                    return Group.Text;
                }
                set {
                    Group.Text = value;
                    Group.Refresh();
                }
            }

            public void SetProgress(float value) {
                Bar.Value = (int)Math.Floor(value * 100);
            }
        }

        public ProcessForm() {
            InitializeComponent();
            RefreshPauseButtonsState();
            //GetDownloadControl("A").SetProgress(0.2f);
            //GetDownloadControl("B").SetProgress(0.86f);
            //GetDownloadControl("C").SetProgress(0.4f);
        }

        string[] PickSourceFolders() {
            using (var fbd = new CommonOpenFileDialog("SELECT ALL SOURCE FOLDERS"){IsFolderPicker = true, Multiselect = true}) {
                CommonFileDialogResult result = fbd.ShowDialog();

                if (result == CommonFileDialogResult.Ok) {
                    return fbd.FileNames.ToArray();
                }

                return new string[0];
            }
        }

        string PickDestinationFolder() {
            using (var fbd = new CommonOpenFileDialog("Select a target ROOT folder") { IsFolderPicker = true }) {
                CommonFileDialogResult result = fbd.ShowDialog();

                if (result == CommonFileDialogResult.Ok) {
                    return fbd.FileName;
                }

                return string.Empty;
            }
        }

        private void MaxWorkerSizeUpDown_ValueChanged(object sender, EventArgs e) {
            copier.MaxWorkerSizeMB = (int)MaxWorkerSizeUpDown.Value;
        }

        private void MaxWorkersUpDown_ValueChanged(object sender, EventArgs e) {
            copier.MaxWorkerCount = (int)MaxWorkersUpDown.Value;
        }

        private void StartCopy(object sender, EventArgs e) {
            UpdateCheckBoxes();
            RefreshPauseButtonsState();
            var _ = WaitAndStartCopying(copier);
        }

        private async Task WaitAndStartCopying(MultiCopier copier) {
            try {
                copyTask = copier.StartCopying();

                await copyTask;
            }
            catch(Exception e) {
                System.Diagnostics.Debug.WriteLine(e);
                MessageBox.Show(e.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw e;
            }
        }

        private void Copier_OnFileFinishedDownload(System.IO.FileInfo obj) {
            lock (associatedBars) {
                if (associatedBars.ContainsKey(obj)) {
                    var ctrl = associatedBars[obj];
                    this.Invoke(new Action(() => KillControl(ctrl)));
                    associatedBars.Remove(obj);
                }
            }

            this.Invoke(new Action(()=> UpdateCheckBoxes()));
        }

        private void UpdateCheckBoxes() {
            for (int i = 0; i < copier.RootFolders.Count(); i++) {
                var root = copier.RootFolders.ElementAt(i);
                CopyFoldersList.Items[i] = $"{root.Truncate(10)} {((1f-copier.RemainingFilesPerRootFolder[root]/(float)copier.TotalFilesPerRootFolder[root])*100).ToString("n0")}%";
                CopyFoldersList.SetItemChecked(i, copier.RemainingFilesPerRootFolder[root] == 0);
            }
        }

        private void Copier_OnFileDownloadProgress(System.IO.FileInfo arg1, float arg2) {
            lock (associatedBars) {
                if (associatedBars.ContainsKey(arg1)) {
                    this.Invoke(new Action(() => associatedBars[arg1].SetProgress(arg2)));
                }
            }
        }

        private void Copier_OnFileStartedDownload(System.IO.FileInfo obj) {
            lock (associatedBars) {
                associatedBars.Add(obj, (DownloadControl)this.Invoke(new Func<DownloadControl>(() => GetNewDownloadControl(obj.Name))));
            }
        }

        DownloadControl GetNewDownloadControl(string name) {

            if (pooledControls.Count > 0) {
                var ctrl = pooledControls.Pop();
                ctrl.Group.Visible = true;
                ctrl.Name = name;

                lock (activeControls) {
                    activeControls.Add(ctrl);
                }

                return ctrl;
            }
            else {
                var progressBar = new ProgressBar();
                progressBar.Dock = System.Windows.Forms.DockStyle.Fill;
                progressBar.Location = new System.Drawing.Point(3, 19);
                progressBar.Size = new System.Drawing.Size(260 - 18, 28);
                progressBar.TabIndex = 0;
                progressBar.UseWaitCursor = true;

                var groupBox = new GroupBox();
                groupBox.Controls.Add(progressBar);
                groupBox.Location = new System.Drawing.Point(3, 3);
                groupBox.Size = new System.Drawing.Size(260, 40);
                groupBox.TabIndex = 0;
                groupBox.TabStop = false;
                groupBox.Text = name;
                groupBox.UseWaitCursor = true;
                groupBox.Anchor = AnchorStyles.Left | AnchorStyles.Top;

                this.DownloadsContainer.Controls.Add(groupBox);

                var ctrl = new DownloadControl() {
                    Bar = progressBar,
                    Group = groupBox
                };

                lock (activeControls) {
                    activeControls.Add(ctrl);
                }

                return ctrl;
            }
        }


        void KillControl(DownloadControl control) {
            control.Group.Visible = false;
            activeControls.Remove(control);
            pooledControls.Push(control);
        }


        private void SelectTargetsButton_Click(object sender, EventArgs e) {

            string[] sourceFolders = PickSourceFolders();
            if (sourceFolders.Length == 0) return;

            string destination = PickDestinationFolder();
            if (destination == null || destination.Length == 0) return;

            copier = new MultiCopier(sourceFolders, destination);
            CopyFoldersList.Items.Clear();
            for (int i = 0; i < copier.RootFolders.Count(); i++) {

                CopyFoldersList.Items.Add(copier.RootFolders.ElementAt(i));
                CopyFoldersList.SetItemChecked(i, false);
            }


            copier.OnFileStartedDownload += Copier_OnFileStartedDownload;
            copier.OnFileDownloadProgress += Copier_OnFileDownloadProgress;
            copier.OnFileFinishedDownload += Copier_OnFileFinishedDownload;

            MaxWorkersUpDown.Value = copier.MaxWorkerCount;
            MaxWorkersUpDown.ValueChanged += MaxWorkersUpDown_ValueChanged;
            MaxWorkerSizeUpDown.Value = copier.MaxWorkerSizeMB;
            MaxWorkerSizeUpDown.ValueChanged += MaxWorkerSizeUpDown_ValueChanged;

            StartCopy(null, null);
            //this.HandleCreated += StartCopy;
        }

        private void PauseButton_Click(object sender, EventArgs e) {
            copier.IsPaused = true;
            RefreshPauseButtonsState();
        }

        private void ResumeButton_Click(object sender, EventArgs e) {
            copier.IsPaused = false;
            RefreshPauseButtonsState();
        }
        
        private void RefreshPauseButtonsState() {
            ResumeButton.Enabled = copier != null && copier.IsPaused;
            PauseButton.Enabled = copier != null && !copier.IsPaused;
            StopButton.Enabled = copier != null;
        }

        private void StopButton_Click(object sender, EventArgs e) {
            if (copier == null) return;
            copier.Stop();
            copier = null;
            RefreshPauseButtonsState();
        }
    }
}
