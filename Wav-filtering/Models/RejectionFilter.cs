using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wav_filtering.Models.Interfaces;
using Wav_filtering.Infrastructure;
using static Wav_filtering.Infrastructure.WaveValidationContracts;
using NAudio.Wave;
using NAudio.Dsp;

namespace Wav_filtering.Models
{
    public class RejectionFilter : IWaveRejectionFilter
    {
        public RejectionFilter(int low, int high)
        {
            ThrowIfBoundariesNoFit(low, high);
            LowBoundary = low;
            HighBoundary = high;
        }

        private double deltaFreq;
        private double sizeOfWindow;
        private int sizeOfFftWindow;

        public int LowBoundary { get; private set; }

        public int HighBoundary { get; private set; }

        public async Task<IEnumerable<byte[]>> FilterWave(WaveStream waveStream)
        {            
            var sampleRate = waveStream.WaveFormat.SampleRate;
            sizeOfFftWindow = (int)Math.Log(sampleRate, 2);
            int bufferSize = (int)Math.Pow(2, sizeOfFftWindow);
            sizeOfWindow = bufferSize;
            deltaFreq = sampleRate / sizeOfWindow;
            byte[] buffer = new byte[bufferSize];

            int read;
            int offset = 0;

            do
            {
                read = await waveStream.ReadAsync(buffer, offset, bufferSize);
                offset += read;
                if (read < bufferSize)
                    read = await waveStream.ReadAsync(buffer, offset, bufferSize - read);

               var filtratedSamples = FilterDataBlock(buffer, sizeOfFftWindow, sampleRate);
                
               // make inverse fft here
                // then return ready for file creation data                
            }
            while (read > 0);

        }


        private Complex[] FilterDataBlock(byte[] buffer, int sizeOfFftWindow, int sampleRate)
        {
            Complex[] samples = new Complex[buffer.Length / 2];
            samples.Initialize();

            for(int i=0; i < buffer.Length; i+=2)
            {
                short sample = (short)((buffer[i + 1] << 8) | buffer[i]);
                samples[i].X = sample / 32768f;
            }

            FastFourierTransform.FFT(true, sizeOfFftWindow, samples);
            return Enumerable.Empty<Complex>().ToArray();
        }

        public void SetRejectionRange(int low, int high)
        {
            ThrowIfBoundariesNoFit(low, high);
            LowBoundary = low;
            HighBoundary = high;            
        }        
    }
}
