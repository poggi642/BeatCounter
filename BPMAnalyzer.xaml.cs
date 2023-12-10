using NAudio.Wave;
using NAudio.Mixer;

namespace BeatCounter;

public partial class BPMAnalyzer : ContentPage
{
    int count = 0;
    public WaveFormat WaveFormat { get; private set; }
    public float[] AudioData { get; private set; }

    public BPMAnalyzer()
    {
        InitializeComponent();
    }

    private void AnalyzeBPM_Clicked(object sender, EventArgs e)
    {
        AnalyzeBPM();
        
    }

    public void AnalyzeBPM()
    {
        // Open the audio file using NAudio
        AudioFileReader audioFileReader = new AudioFileReader(@".\Resources\Raw\Khiva_FeelItOut.mp3");
        WaveFormat = audioFileReader.WaveFormat;
        var wholeFile = new List<float>((int)(audioFileReader.Length / 4));
        var readBuffer = new float[audioFileReader.WaveFormat.SampleRate * audioFileReader.WaveFormat.Channels];
        int samplesRead;
        while ((samplesRead = audioFileReader.Read(readBuffer, 0, readBuffer.Length)) > 0)
        {
            wholeFile.AddRange(readBuffer.Take(samplesRead));
        }
        AudioData = wholeFile.ToArray();

        //    // Create a sample aggregator to get audio data
        //    SampleAggregator sampleAggregator = new SampleAggregator();
        //    sampleAggregator.NotificationCount = audioFileReader.WaveFormat.SampleRate / 100;
        //    sampleAggregator.PerformFFT = true;
        //    sampleAggregator.FftCalculated += SampleAggregator_FftCalculated;

        //    // Create a BPM detector
        //    BpmDetector bpmDetector = new BpmDetector(sampleAggregator);
        //    bpmDetector.BpmDetected += BpmDetector_BpmDetected;

        //    // Create a wave stream to read audio data
        //    WaveStream waveStream = WaveFormatConversionStream.CreatePcmStream(audioFileReader);
        //    List<WaveStream> waveStreams = new();
        //    waveStreams.Add(waveStream);
        //    // Create a mixer to process audio data
        //    WaveMixerStream32 mixerStream = new WaveMixerStream32(waveStreams, true);

        //    // Connect the mixer stream to the sample aggregator
        //    mixerStream.AddInputStream(sampleAggregator);

        //    // Start reading audio data
        //    mixerStream.Position = 0;
        //    mixerStream.Volume = 1.0f;
        //    mixerStream.Play();
        //}

        //private void SampleAggregator_FftCalculated(object sender, FftEventArgs e)
        //{
        //    // Perform any necessary processing on the FFT data
        //    // This event is triggered when a notification count is reached
        //}

        //private void BpmDetector_BpmDetected(object sender, BpmEventArgs e)
        //{
        //    // Retrieve the detected BPM value
        //    int bpm = e.Bpm;

        //    // Display or use the BPM value as needed
        //    // For example, show it in a label or perform further actions
    }
}

