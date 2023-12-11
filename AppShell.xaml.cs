using BeatCounter.View;

namespace BeatCounter;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(BPMAnalyzerView), typeof(BPMAnalyzerView));
    }
}
