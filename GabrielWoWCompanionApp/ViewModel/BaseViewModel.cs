// Code by Gabriel Atienza-Norris, Mobile Final, 04/20/2023
namespace GabrielWoWCompanionApp.ViewModel;

public partial class BaseViewModel : ObservableObject
{ 
    [ObservableProperty]
    bool isBusy; 

    [ObservableProperty]
    string title; 

    public BaseViewModel()
    {

    }
}
