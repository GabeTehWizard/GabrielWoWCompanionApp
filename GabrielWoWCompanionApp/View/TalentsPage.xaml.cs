namespace GabrielWoWCompanionApp.View;

public partial class TalentsPage : ContentPage
{
    private TalentsPageViewModel viewModel;

    public TalentsPage(TalentsPageViewModel viewModel)
	{
		InitializeComponent();
        this.viewModel = viewModel;
		BindingContext = viewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        try
        {
            await viewModel.InitializeTalentsAsync();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Page Load Error", ex.Message, "OK");
        }
    }
}