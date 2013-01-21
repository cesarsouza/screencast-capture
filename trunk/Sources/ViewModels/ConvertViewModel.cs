// Screencast Capture, free screen recorder
// http://screencast-capture.googlecode.com
//
// Copyright © César Souza, 2012-2013
// cesarsouza at gmail.com
//
//    This program is free software; you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation; either version 2 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program; if not, write to the Free Software
//    Foundation, Inc., 675 Mass Ave, Cambridge, MA 02139, USA.
// 

namespace ScreenCapture.ViewModels
{
    using ScreenCapture.Properties;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;

    /// <summary>
    ///   Conversion ViewModel to control the conversion process.
    /// </summary>
    /// 
    public class ConvertViewModel : INotifyPropertyChanged, IDisposable
    {

        private MainViewPresenter main;
        private BackgroundWorker worker;
        private bool shouldStop;


        /// <summary>
        ///   Gets or sets the path to the file to be converted.
        /// </summary>
        /// 
        public string InputPath { get; set; }



        /// <summary>
        ///   Gets the progress of the conversion process.
        ///   This value goes from 0 to 100.
        /// </summary>
        /// 
        public int Progress { get; private set; }

        /// <summary>
        ///   Gets whether there is a conversion in progress.
        /// </summary>
        /// 
        public bool IsConverting { get; private set; }

     

        /// <summary>
        ///   Gets or sets whether to convert to OGG.
        /// </summary>
        /// 
        public bool ToOgg { get; set; }

        /// <summary>
        ///   Gets or sets whether to convert to WebM.
        /// </summary>
        /// 
        public bool ToWebM { get; set; }



        /// <summary>
        ///   Gets whether the current application status
        ///   allows the user to start a conversion process.
        /// </summary>
        /// 
        public bool CanConvert
        {
            get
            {
                if (IsConverting)
                    return false;

                if (String.IsNullOrEmpty(InputPath))
                    return false;

                string ext = Path.GetExtension(InputPath);

                if (!String.IsNullOrEmpty(ext))
                {
                    ext = ext.Remove(0, 1); // Remove the dot
                    if (OptionViewModel.SupportedContainers.Contains(ext))
                        return true;
                }

                return false;
            }
        }



        /// <summary>
        ///   Initializes a new instance of the <see cref="ConvertViewModel" /> class.
        /// </summary>
        /// 
        public ConvertViewModel(MainViewPresenter main)
        {
            if (main == null)
                throw new ArgumentNullException("main");

            this.main = main;

            this.InputPath = String.Empty;

            this.worker = new BackgroundWorker();
            this.worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            this.worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
            this.worker.WorkerReportsProgress = true;
            this.worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
        }



        /// <summary>
        ///   Starts a conversion operation.
        /// </summary>
        /// 
        public void Start()
        {
            Progress = 0;
            IsConverting = true;
            main.StatusText = Resources.Status_Converting;
            shouldStop = false;

            worker.RunWorkerAsync();
        }

        /// <summary>
        ///   Stops the current conversion.
        /// </summary>
        /// 
        public void Cancel()
        {
            shouldStop = true;   
        }



        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Progress = 100;
            IsConverting = false;
            main.StatusText = Resources.Status_Ready;
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Progress = e.ProgressPercentage;
        }


        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            List<String> formats = new List<string>();
            if (ToWebM)
                formats.Add(".webm");
            if (ToOgg)
                formats.Add(".ogg");

            string input = InputPath;
            IFormatProvider provider = CultureInfo.InvariantCulture;


            for (int i = 0; i < formats.Count; i++)
            {
                string extension = formats[i];
                string output = Path.ChangeExtension(input, extension);
                string options = String.Format(provider, "-y -i {0} -q:v 6 -an  {1}",
                    input, output);

                ProcessStartInfo info = new ProcessStartInfo
                {
                    FileName = "ffmpeg.exe",
                    Arguments = options,
                    CreateNoWindow = true,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                };

                using (Process process = new Process())
                {
                    process.StartInfo = info;
                    process.Start();

                    StreamReader reader = process.StandardError;
                    StreamWriter writer = process.StandardInput;

                    string durationMarker = "Duration: ";
                    TimeSpan duration = TimeSpan.Zero;

                    string progressMarker = "time=";
                    string bitrateMarker = "bitrate=";
                    TimeSpan current = TimeSpan.Zero;

                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();

                        if (line.Contains(durationMarker))
                        {
                            int startIndex = line.IndexOf(durationMarker,
                                StringComparison.CurrentCulture) + durationMarker.Length;
                            int endIndex = line.IndexOf(',', startIndex);
                            string strDuration = line.Substring(startIndex, endIndex - startIndex);
                            duration = TimeSpan.Parse(strDuration, provider);

                            if (duration.Ticks == 0)
                                return;
                        }

                        if (line.Contains(progressMarker))
                        {
                            int startIndex = line.IndexOf(progressMarker,
                                StringComparison.CurrentCulture) + progressMarker.Length;
                            int endIndex = line.IndexOf(bitrateMarker, StringComparison.CurrentCulture);

                            string strTime = line.Substring(startIndex, endIndex - startIndex);
                            current = TimeSpan.Parse(strTime, provider);

                            double max = duration.Ticks;
                            double cur = current.Ticks;

                            double progress = (cur / max);
                            double taskSize = 1.0 / formats.Count;

                            double start = i / (double)formats.Count;

                            worker.ReportProgress((int)((start + progress * taskSize) * 100.0));
                        }

                        if (shouldStop)
                        {
                            process.Kill();
                            return;
                        }
                    }
                }
            }
        }


        #region IDisposable implementation

        /// <summary>
        ///   Performs application-defined tasks associated with freeing, 
        ///   releasing, or resetting unmanaged resources.
        /// </summary>
        /// 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///   Releases unmanaged resources and performs other cleanup operations 
        ///   before the <see cref="ConvertViewModel"/> is reclaimed by garbage collection.
        /// </summary>
        /// 
        ~ConvertViewModel()
        {
            Dispose(false);
        }

        /// <summary>
        ///   Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// 
        /// <param name="disposing"><c>true</c> to release both managed
        /// and unmanaged resources; <c>false</c> to release only unmanaged
        /// resources.</param>
        ///
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (worker != null)
                {
                    worker.Dispose();
                    worker = null;
                }
            }
        }
        #endregion



        // The PropertyChanged event doesn't needs to be explicitly raised
        // from this application. The event raising is handled automatically
        // by the NotifyPropertyWeaver VS extension using IL injection.
        //
#pragma warning disable 0067
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067

        
    }
}
