// Code by Gabriel Atienza-Norris, Mobile Final, 04/20/2023
using GabrielWoWCompanionApp.View;

namespace GabrielWoWCompanionApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        // Routes
        Routing.RegisterRoute(nameof(DisciplineTalentsPage), typeof(DisciplineTalentsPage));
        Routing.RegisterRoute(nameof(HolyTalentsPage), typeof(HolyTalentsPage));
        Routing.RegisterRoute(nameof(LogPage), typeof(LogPage));
        Routing.RegisterRoute(nameof(ShadowTalentsPage), typeof(ShadowTalentsPage));
        Routing.RegisterRoute(nameof(TalentsPage), typeof(TalentsPage));
    }
}
