using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetCopy {
    public class MultiCopier {

        class FileToCopy {
            public FileInfo OriginFile;
            public string VPath;
            public string RootFolder;

            public FileToCopy(FileInfo originFile, string vPath) {
                OriginFile = originFile;
                VPath = vPath;

                var dirSegments = vPath.Split(new char[] { Path.DirectorySeparatorChar });
                RootFolder = dirSegments[0];
            }
        }

        public event Action<FileInfo> OnFileStartedDownload;
        public event Action<FileInfo, float> OnFileDownloadProgress;
        public event Action<FileInfo> OnFileFinishedDownload;

        public IEnumerable<string> RootFolders => TotalFilesPerRootFolder.Keys;

        // Just for "stats" purpose
        public Dictionary<string, int> RemainingFilesPerRootFolder = new Dictionary<string, int>();
        public Dictionary<string, int> TotalFilesPerRootFolder = new Dictionary<string, int>();

        public bool IsPaused { get; set; } = false;


        public int MaxWorkerCount = 10;
        public int MaxWorkerSizeMB = 5;

        Queue<FileToCopy> filesToCopy = new Queue<FileToCopy>();
        string destinationRoot;
        System.Threading.CancellationTokenSource tokenSource = new System.Threading.CancellationTokenSource();

        public MultiCopier(string[] targetPrimitiveFolders, string destinationRoot) {
            List<Task> statsTasks = new List<Task>();

            foreach (var folder in targetPrimitiveFolders) {
                string folderName = Path.GetFileName(folder);
                TotalFilesPerRootFolder[folderName] = 0;
                RemainingFilesPerRootFolder[folderName] = 0;

                var task = new Task(() => {

                    var files = Directory.EnumerateFiles(folder, "*.*", SearchOption.AllDirectories);

                    foreach (var file in files) {
                        var info = new FileInfo(file);

                        lock (filesToCopy) {
                            filesToCopy.Enqueue(new FileToCopy(info, file.Substring(folder.Length - folderName.Length)));
                        }

                        lock (RemainingFilesPerRootFolder) {
                            TotalFilesPerRootFolder[folderName]++;
                            RemainingFilesPerRootFolder[folderName]++;
                        }
                    }
                });
                statsTasks.Add(task);
                task.Start();
            }

            Task.WaitAll(statsTasks.ToArray());
            statsTasks.Clear();

            filesToCopy = new Queue<FileToCopy>(
                filesToCopy
                .OrderBy(o => o.RootFolder)
                .ThenBy(o => o.OriginFile.Length)
            );


            this.destinationRoot = destinationRoot;
        }

        public void Stop() {
            tokenSource.Cancel();
            filesToCopy.Clear();
        }

        public async Task StartCopying() {
            var token = tokenSource.Token;
            List<Task> workers = new List<Task>();
            IsPaused = false;

            if (token.IsCancellationRequested) {
                throw new TaskCanceledException();
            }

            while (filesToCopy.Count > 0) {
                while (
                    workers.Count < MaxWorkerCount &&
                    filesToCopy.Count > 0 && 
                    (filesToCopy.Peek().OriginFile.Length < MaxWorkerSizeMB * 1000 * 1000 || workers.Count == 0)
                ) {
                    if (token.IsCancellationRequested) {
                        throw new TaskCanceledException();
                    }

                    if (IsPaused) {
                        await Task.Delay(500);
                        continue;
                    }

                    lock (filesToCopy) {
                        var file = filesToCopy.Dequeue();
                        var task = DownloadFile(file);

                        lock (workers) {
                            workers.Add(task);
                        }
                    }

                }

                if (token.IsCancellationRequested) {
                    throw new TaskCanceledException();
                }

                await Task.Run(() => Task.WaitAny(workers.ToArray()));

                lock (workers) {
                    var finished = workers.RemoveAll(o => o.IsCompleted);
                }
            }
        }

        async Task DownloadFile(FileToCopy file) {
            OnFileStartedDownload?.Invoke(file.OriginFile);
            await Copy(file);
            lock (RemainingFilesPerRootFolder) {
                RemainingFilesPerRootFolder[Path.GetFileName(Path.GetFileName(file.RootFolder))]--;
            }
            OnFileFinishedDownload?.Invoke(file.OriginFile);
            System.Diagnostics.Debug.WriteLine($"Done with {file.OriginFile}!");
        }

        private async Task Copy(FileToCopy file) {
            System.Diagnostics.Debug.WriteLine($"Started copy of file {file.OriginFile.FullName}");

            DateTime start = DateTime.Now;

            if (file.VPath.Contains(Path.DirectorySeparatorChar)) {
                var dir = Path.GetDirectoryName(file.VPath);

                if (dir.Length > 0) {
                    Directory.CreateDirectory(Path.Combine(destinationRoot, dir));
                }
            }

            //var tcs = new TaskCompletionSource<object>();

            using (var webClient = new WebClient()) {
                tokenSource.Token.Register(webClient.CancelAsync);
                webClient.DownloadProgressChanged += (object sender, DownloadProgressChangedEventArgs e) => OnFileDownloadProgress?.Invoke(file.OriginFile, e.ProgressPercentage / 100f);
                await webClient.DownloadFileTaskAsync(new Uri(file.OriginFile.FullName), Path.Combine(destinationRoot, file.VPath));
            }

            System.Diagnostics.Debug.WriteLine($"Finished copy of file {file.OriginFile.FullName} in {(DateTime.Now-start).ToString("g")}");
        }
    }
}
