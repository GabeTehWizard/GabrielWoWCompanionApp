namespace GabrielWoWCompanionApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		// Register the Main Page as the AppShell
		MainPage = new AppShell();
	}
}
