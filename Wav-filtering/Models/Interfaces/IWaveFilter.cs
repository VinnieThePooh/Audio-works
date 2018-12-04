using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wav_filtering.Models.Interfaces
{
    interface IWaveFilter
    {
        Task<IEnumerable<byte[]>> FilterWave(WaveStream waveStream);
    }
}
