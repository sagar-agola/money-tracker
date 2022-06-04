using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MT.Client.WPF.Core;
using MT.Shared.DataTransferModels;
using MT.Shared.DataTransferModels.Pagination;
using MT.Shared.DataTransferModels.Transaction;

namespace MT.Client.WPF.ViewModels;

public class TransactionListViewModel : BaseViewModel
{
    #region Binding Properties

    private ObservableCollection<TransactionModel> _transactions;
    public ObservableCollection<TransactionModel> Transactions
    {
        get { return _transactions; }
        set
        {
            _transactions = value;
            OnPropertyChanged(nameof(Transactions));
        }
    }

    #endregion

    #region Commands

    public RelayCommand AddTransactionCommand { get; set; }

    #endregion

    private readonly NavigationStore _navigationStore;

    public TransactionListViewModel(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;

        AddTransactionCommand = new RelayCommand(data => _navigationStore.CurrentViewModel = new AddTransactionViewModel(navigationStore));

        GetTransactions();
    }

    private void GetTransactions()
    {
        using HttpClient client = new();

        GetAllTransactionsRequestModel request = new()
        {
            PageNumber = 1,
            PageSize = 10
        };

        StringContent content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
        HttpResponseMessage response = client.PostAsync("https://localhost:44391/api/transactions", content).GetAwaiter().GetResult();

        if (response.IsSuccessStatusCode)
        {
            string responseData = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            ApiResponse<PaginatedResponse<TransactionModel>> apiResponse = JsonSerializer.Deserialize<ApiResponse<PaginatedResponse<TransactionModel>>>(responseData);

            if (apiResponse.StatusCode == HttpStatusCode.OK)
            {
                Transactions = new ObservableCollection<TransactionModel>(apiResponse.Data.Data);
            }
        }
    }
}
