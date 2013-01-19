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
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class ConvertViewModel : INotifyPropertyChanged
    {

        public MainViewModel Main { get; private set; }

        public string InputFilePath { get; set; }

        public int Progress { get; private set; }

        public bool IsConverting { get; private set; }


        private BackgroundWorker worker;


        public bool CanConvert
        {
            get
            {
                if (Main.IsRecording)
                    return false;

                if (String.IsNullOrEmpty(InputFilePath))
                    return false;

                string ext = Path.GetExtension(InputFilePath);

                if (!String.IsNullOrEmpty(ext))
                {
                    ext = ext.Remove(0, 1); // Remove the dot
                    if (OptionViewModel.SupportedContainers.Contains(ext))
                        return true;
                }

                return false;
            }
        }

        public ConvertViewModel(MainViewModel main)
        {
            Main = main;

            InputFilePath = String.Empty;

            worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
            worker.WorkerReportsProgress = true;
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);

            Main.PropertyChanged += new PropertyChangedEventHandler(Main_PropertyChanged);
        }

        private void Main_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsRecording")
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("CanConvert"));
        }

        


        public void Convert()
        {
            Progress = 0;
            IsConverting = true;
            Main.Status = "Converting...";

            worker.RunWorkerAsync();
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Progress = 100;
            IsConverting = false;
            Main.Status = "Ready";
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Progress = e.ProgressPercentage;
        }
        


        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            string input = InputFilePath;
            string output = Path.ChangeExtension(input, ".ogg");
            string options = String.Format("-y -i {0} -q:v 5 -an  {1}",
                input, output);

            ProcessStartInfo info = new ProcessStartInfo
            {
                FileName = "avconv.exe",
                Arguments = options,
                CreateNoWindow = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
            };

            Process process = new Process();
            process.StartInfo = info;
            process.Start();

            StreamReader reader = process.StandardError;
            StreamWriter writer = process.StandardInput;

            string durationMarker = "Duration: ";
            TimeSpan duration = TimeSpan.Zero;

            string progressMarker = "time=";
            string bitrateMarker = "bitrate=";
            TimeSpan current = TimeSpan.Zero;

            StringBuilder cout = new StringBuilder();

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                cout.AppendLine(line);

                if (line.Contains(durationMarker))
                {
                    int startIndex = line.IndexOf(durationMarker) + durationMarker.Length;
                    int endIndex = line.IndexOf(',', startIndex);
                    string strDuration = line.Substring(startIndex, endIndex - startIndex);
                    duration = TimeSpan.Parse(strDuration);

                    if (duration.Ticks == 0)
                        return;
                }

                if (line.Contains(progressMarker))
                {
                    int startIndex = line.IndexOf(progressMarker) + progressMarker.Length;
                    int endIndex = line.IndexOf(bitrateMarker);

                    string strTime = line.Substring(startIndex, endIndex - startIndex);
                    current = TimeSpan.Parse(strTime);

                    double max = duration.Ticks;
                    double cur = current.Ticks;
                    worker.ReportProgress((int)((cur / max) * 100));
                }
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
