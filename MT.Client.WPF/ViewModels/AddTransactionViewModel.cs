using MT.Client.WPF.Core;

namespace MT.Client.WPF.ViewModels;

public class AddTransactionViewModel : BaseViewModel
{
    #region Commands

    public RelayCommand CancelCommand { get; set; }

    #endregion

    private readonly NavigationStore _navigationStore;

    public AddTransactionViewModel(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
        CancelCommand = new RelayCommand(data => RedirectToListPage());
    }

    private void RedirectToListPage()
    {
        _navigationStore.CurrentViewModel = new TransactionListViewModel(_navigationStore);
    }
}
