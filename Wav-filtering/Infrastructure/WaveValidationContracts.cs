using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wav_filtering.Infrastructure
{
    public static class WaveValidationContracts
    {
        public static void ThrowIfBoundariesNoFit(int low, int high)
        {
            if (low > high)
                throw new ArgumentException("Low boundary is higher than high");
        }
    }
}
