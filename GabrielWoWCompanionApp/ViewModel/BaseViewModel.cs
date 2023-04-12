namespace GabrielWoWCompanionApp.ViewModel;

public partial class BaseViewModel : ObservableObject
{ 
    [ObservableProperty]
    //[NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy; 

    [ObservableProperty]
    string title; 

    public BaseViewModel()
    {

    }

    //public bool IsNotBusy => !IsBusy;
}
