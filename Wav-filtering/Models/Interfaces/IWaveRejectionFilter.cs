namespace Wav_filtering.Models.Interfaces
{
    interface IWaveRejectionFilter: IWaveFilter
    {        
        int LowBoundary { get; }
        int HighBoundary { get; }
        void SetRejectionRange(int low, int high);        
    }
}
