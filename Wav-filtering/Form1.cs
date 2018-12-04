using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Wav_filtering.Models;

namespace Wav_filtering
{
    public partial class Form1 : Form
    {
        private readonly string BaseDataDirectory;        

        IList<FileData> WavFiles { get; set; } = new BindingList<FileData>();

        public FileData SelectedFile { get; set; }

        public Form1()
        {
            InitializeComponent();
            BaseDataDirectory = EnsureDataDirectory();
            InitBindings();
        }


        private string EnsureDataDirectory()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");            
            Directory.CreateDirectory(path);
            return path;           
        }


        private void InitBindings()
        {
            lboxSource.DataSource = WavFiles;
            lboxSource.DisplayMember = nameof(FileData.FileName);
        }


        private void downloadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "MP3 Files (*.mp3)|*.mp3|WAV Files (*.wav)|*.wav";
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            var fileData = new FileData(ofd.FileName);

            if (WavFiles.Contains(fileData))
            {
                MessageBox.Show(this, "Данный файл уже был добавлен", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            WavFiles.Add(fileData);

        }
    }
}
