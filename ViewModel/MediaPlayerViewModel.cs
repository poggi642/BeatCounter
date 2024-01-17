using BeatCounter.Interfaces;
using CommunityToolkit.Maui.Core.Primitives;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NAudio.Wave;
using System.Diagnostics;

namespace BeatCounter.ViewModel;
public partial class MediaPlayerViewModel(IAudioAnalyzer audioAnalyzer) : ObservableObject
{
    private IDispatcherTimer timer;
    private Stopwatch? stopwatch;
    private double beatInterval;
    private bool isTimerRunning = false;

    [ObservableProperty]
    private int beatCount;

    [ObservableProperty]
    private MediaElement mediaElement = new();

    [ObservableProperty]
    private string mediaSource;
    [ObservableProperty]
    private bool isPlaying;
    public double Volume => IsPlaying ? MediaElement.Volume : 1.0;

    [RelayCommand]
    private void Play()
    {
        IsPlaying = true;

        audioAnalyzer.TrackBeats();

    }

    [RelayCommand]
    private void Stop()
    {
        mediaElement.Stop();
    }

    private void MediaElement_MediaOpened(object sender, EventArgs args)
    {
        // Calculate the beat interval based on the music's tempo
        // For example, if the tempo is 120 BPM (beats per minute),
        // the beat interval would be 60 / 120 = 0.5 seconds
        int tempo = 120; // Replace with the actual tempo of your music
        beatInterval = 60.0 / tempo;

        // Start the timer
        timer.Interval = TimeSpan.FromSeconds(beatInterval);
        timer.Start();

        // Initialize the stopwatch
        stopwatch = Stopwatch.StartNew();
    }

    private void MediaElement_MediaEnded(object sender, EventArgs args)
    {
        // Stop the timer and reset the beat count
        timer.Stop();
        beatCount = 0;

        // Reset the stopwatch
        stopwatch?.Reset();
    }

    private void Timer_Tick(object sender, EventArgs args)
    {
        // Calculate the elapsed time in seconds
        double elapsedTime = stopwatch!.Elapsed.TotalSeconds;

        // Increment the beat count
        beatCount++;

        // Perform beat detection and analysis here
        // You can use the beatCount or elapsedTime to trigger actions or display information

        // You can also trigger other actions based on the beat count or elapsed time
        if (beatCount % 4 == 0)
        {
            // This is the start of a new phrase or measure
            // Perform any necessary actions here
        }
    }

    public void StartTimer()
    {
        if (!isTimerRunning)
        {
            isTimerRunning = true;
            Device.StartTimer(TimeSpan.FromSeconds(1), TimerCallback);
        }
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }

    private bool TimerCallback()
    {
        // Perform your timer-related logic here
        // This code will be executed on the UI thread

        // Return true to continue the timer, or false to stop it
        return isTimerRunning;
    }
}
