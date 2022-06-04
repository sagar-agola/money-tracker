using System.Net.Http;
using System.Text;
using System.Text.Json;
using MT.Client.WPF.Core;
using MT.Shared.DataTransferModels.Transaction;

namespace MT.Client.WPF.ViewModels;

public class AddTransactionViewModel : BaseViewModel
{
    #region Binding Properties

    private decimal _amount;
    public decimal Amount
    {
        get { return _amount; }
        set
        {
            _amount = value;
            OnPropertyChanged(nameof(Amount));
        }
    }

    #endregion

    #region Commands

    public RelayCommand CancelCommand { get; set; }
    public RelayCommand SaveCommand { get; set; }

    #endregion

    private readonly NavigationStore _navigationStore;

    public AddTransactionViewModel(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
        CancelCommand = new RelayCommand(data => RedirectToListPage());
        SaveCommand = new RelayCommand(data => SaveTransaction());
    }

    private void RedirectToListPage()
    {
        _navigationStore.CurrentViewModel = new TransactionListViewModel(_navigationStore);
    }

    private void SaveTransaction()
    {
        using HttpClient client = new();

        SaveTransactionModel request = new()
        {
            Amount = Amount
        };

        StringContent content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

        HttpResponseMessage response = client.PostAsync("https://localhost:44391/api/transactions/save", content).Result;

        if (response.IsSuccessStatusCode)
        {
            RedirectToListPage();
        }
    }
}
