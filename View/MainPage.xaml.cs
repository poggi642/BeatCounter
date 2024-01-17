using BeatCounter.Services;
using BeatCounter.ViewModel;
using NAudio.Wave;


namespace BeatCounter.View;

public partial class MainPage : ContentPage
{
	private RealTimeAnalyzer analyzer;


	public MainPage(MediaPlayerViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		analyzer = new RealTimeAnalyzer();
		analyzer.AudioDataAvailable += OnAudioDataAvailable;
	}

	private void OnPlayButtonClicked(object sender, EventArgs e)
	{
		// Call the Play method of the MediaElement
		mediaElement.Play();
		analyzer.Start();
	}

	private void OnStopButtonClicked(object sender, EventArgs e)
	{
		mediaElement.Stop();
		analyzer.Stop();
	}


	private void OnAudioDataAvailable(object sender, AudioDataEventArgs e)
	{
		// Process the audio data here
		// e.AudioData contains the raw audio data
		// You can update the UI or perform analysis tasks
	}
}