using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;


using System.IO;
using System.Reflection;
using System.Threading;


using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using YoutubeExtractor;

namespace mp3player
{

    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        
        string kek;
        private void button1_Click(object sender, EventArgs e)
        {
            tekstilaatikko.Clear();
            System.IO.File.WriteAllText(@"D:\c#\filez\search.txt", textBox2.Text);

            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = @"D:\c#\youtubething\youtubething\bin\Debug\youtubething.exe";
            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();
                string result = System.IO.File.ReadAllText(@"D:\c#\filez\result.txt");
                
                string kek = "https://www.youtube.com/watch?v=" + result;
                tekstilaatikko.Text += kek;
                // axWindowsMediaPlayer1.URL = kek;

            }


            //listBox1.Items.Add(result);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            kekfuntion();
        }
        public void kekfuntion()
        {
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            IEnumerable<VideoInfo> videos = DownloadUrlResolver.GetDownloadUrls(tekstilaatikko.Text);
            VideoInfo video = videos.First(p => p.VideoType == VideoType.Mp4 && p.Resolution == Convert.ToInt32(comboBox1.Text));
            if (video.RequiresDecryption)
                DownloadUrlResolver.DecryptDownloadUrl(video);
            VideoDownloader downloader = new VideoDownloader(video, Path.Combine(@"D:\musiikkia\", video.Title + video.VideoExtension));
            downloader.DownloadProgressChanged += Downloader_DownloadProgressChanged;
            Thread thread = new Thread(() => { downloader.Execute(); }) { IsBackground = true };
            thread.Start();
        }

        private void Downloader_DownloadProgressChanged(object sender, ProgressEventArgs e)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                progressBar1.Value = (int)e.ProgressPercentage;
                label3.Text = $"{string.Format("{0:0.##}", e.ProgressPercentage)}%";
                progressBar1.Update();
            }));
        }


       

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string[] fileEntries = Directory.GetFiles(@"D:\musiikkia\");
            foreach (string fileName in fileEntries)
            {
                 kek = Path.GetFullPath(fileName);
                //xtBox2.Text += kek;
                
                listBox1.Items.Add(kek);
                
                
                
                

                // do something with fileName

            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

           // string anaalivisva = listBox1.SelectedItem.ToString();
            axWindowsMediaPlayer1.URL = listBox1.SelectedItem.ToString();
        }
    }
    }
       
    
