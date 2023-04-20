namespace GabrielWoWCompanionApp;

public partial class App : Application
{
	public static int sharedTotalCount = 0;
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
