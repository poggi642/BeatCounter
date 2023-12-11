using BeatCounter.ViewModel;


namespace BeatCounter.View;

public partial class MainPage : ContentPage
{


	public MainPage(MediaPlayerViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	private void OnPlayButtonClicked(object sender, EventArgs e)
	{
		// Call the Play method of the MediaElement
		mediaElement.Play();
	}
}