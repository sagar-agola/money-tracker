using MT.Client.WPF.Core;

namespace MT.Client.WPF.ViewModels;

public class TransactionListViewModel : BaseViewModel
{
    #region Commands

    public RelayCommand AddTransactionCommand { get; set; }

    #endregion

    private readonly NavigationStore _navigationStore;

    public TransactionListViewModel(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;

        AddTransactionCommand = new RelayCommand(data => _navigationStore.CurrentViewModel = new AddTransactionViewModel(navigationStore));
    }
}
