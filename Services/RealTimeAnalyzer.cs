using NAudio.Wave;
using System;

namespace BeatCounter.Services;

/// <summary>
///In this example, the `RealTimeAnalyzer` class handles the audio capture using the `WaveInEvent`
///class from NAudio.When audio data becomes available, the `OnDataAvailable` 
///event handler is called, where you can process the audio data as needed. 
///In this example, the `AudioDataAvailable` event is raised with the raw audio data.
///In the `MainPage.xaml.cs`, the `RealTimeAnalyzer` is instantiated, and the `AudioDataAvailable`
///event is subscribed to. The `OnAppearing` and `OnDisappearing` methods
///start and stop the audio capture, respectively. The `OnAudioDataAvailable` method is called
///whenever audio data is available, allowing you to perform analysis tasks or update the UI.
///Please note that this is a simplified example to demonstrate the basic concept of real-time 
///audio analysis. Depending on your specific requirements and analysis tasks, 
///you may need to perform additional processing on the audio data. 
///I hope this example helps you implement real-time music analysis in your .NET MAUI application
///using NAudio!Let me know if you have any further questions or need more assistance.
/// </summary>
public class RealTimeAnalyzer
{
    private WaveInEvent waveIn;

    public event EventHandler<AudioDataEventArgs> AudioDataAvailable;

    public RealTimeAnalyzer()
    {
        waveIn = new WaveInEvent();
        waveIn.DataAvailable += OnDataAvailable;
    }

    public void Start()
    {
        waveIn.StartRecording();
    }

    public void Stop()
    {
        waveIn.StopRecording();
    }

    private void OnDataAvailable(object sender, WaveInEventArgs e)
    {
        // Process the audio data here
        // You can raise the AudioDataAvailable event to pass the data to the UI or other components
        // For simplicity, this example just raises the event with the raw audio data
        AudioDataAvailable?.Invoke(this, new AudioDataEventArgs(e.Buffer));
    }
}

public class AudioDataEventArgs : EventArgs
{
    public byte[] AudioData { get; }

    public AudioDataEventArgs(byte[] audioData)
    {
        AudioData = audioData;
    }
}



