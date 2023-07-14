namespace GabrielWoWCompanionApp;
// Code by Gabriel Atienza-Norris

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		// Register the Main Page as the AppShell
		MainPage = new AppShell();
	}
}
