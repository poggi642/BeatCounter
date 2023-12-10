using CommunityToolkit.Maui.Core.Primitives;
using System.Diagnostics;

namespace BeatCounter;

public partial class MediaPlayer : ContentPage
{
	private IDispatcherTimer timer;
	private Stopwatch? stopwatch;
	private double beatInterval;
	private int beatCount;

	public MediaPlayer()
	{
		InitializeComponent();
		
		// Subscribe to the MediaOpened event
		mediaElement.MediaOpened += MediaElement_MediaOpened;

		// Subscribe to the MediaEnded event
		mediaElement.MediaEnded += MediaElement_MediaEnded;
		//Initialize timer
		timer = Dispatcher.CreateTimer();


	}

	void OnPlayPauseButtonClicked(object sender, EventArgs args)
	{
		if (mediaElement.CurrentState == MediaElementState.Stopped ||
			mediaElement.CurrentState == MediaElementState.Paused)
		{
			mediaElement.Play();
			// Initialize the timer
			timer.Tick += Timer_Tick;
		}
		else if (mediaElement.CurrentState == MediaElementState.Playing)
		{
			mediaElement.Pause();
		}
	}

	void OnStopButtonClicked(object sender, EventArgs args)
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

		// For example, let's display the beat count in a label
		beatCountLabel.Text = $"Beat Count: {beatCount}";

		// You can also trigger other actions based on the beat count or elapsed time
		if (beatCount % 4 == 0)
		{
			// This is the start of a new phrase or measure
			// Perform any necessary actions here
		}
	}
	   
}