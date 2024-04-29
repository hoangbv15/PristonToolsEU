
namespace PristonToolsEU;

public partial class App : Application
{
	private const int Height = 850;
	private const int Width = 600;

	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}

    protected override Window CreateWindow(IActivationState? activationState)
    {
		var window = base.CreateWindow(activationState);
		window.MaximumHeight = window.Height = Height;
		window.MaximumWidth = window.Width = Width;
        return window;
    }
}
