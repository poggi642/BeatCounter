using NAudio.Wave;
using NAudio.Mixer;
using BeatCounter.ViewModel;

namespace BeatCounter.View;

public partial class BPMAnalyzerView : ContentPage
{
    private object viewModel;

    public BPMAnalyzerView(BPMAnalyzerViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}

