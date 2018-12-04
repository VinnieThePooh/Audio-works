using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wav_filtering.Models
{
    public class FreqRange
    {
        public FreqRange(int low, int high)
        {
            if (low > high)
                throw new ArgumentNullException("Нижняя граница не может быть выше средней");
        }

        public int LowBoundary { get; set; }
        public int HighBoundary { get; set; }
    }
}
