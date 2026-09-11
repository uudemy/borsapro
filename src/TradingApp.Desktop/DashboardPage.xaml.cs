using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TradingApp.Desktop;

public partial class DashboardPage : ContentPage
{
    private readonly HttpClient _httpClient;
    private readonly string _token;
    private const string ApiBaseUrl = "http://localhost:5000/api";

    public DashboardPage(string token)
    {
        InitializeComponent();
        _token = token;
        
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadDashboardData();
    }

    private async Task LoadDashboardData()
    {
        await LoadWalletBalance();
        await LoadPortfolio();
    }

    private async Task LoadWalletBalance()
    {
        try
        {
            var response = await _httpClient.GetAsync($"{ApiBaseUrl}/Wallet/balance");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                using var document = JsonDocument.Parse(content);
                var balance = document.RootElement.GetProperty("balance").GetDecimal();
                BalanceLabel.Text = $"${balance:N2}";
            }
        }
        catch (Exception)
        {
            BalanceLabel.Text = "Hata";
        }
    }

    private async Task LoadPortfolio()
    {
        try
        {
            var response = await _httpClient.GetAsync($"{ApiBaseUrl}/Portfolio");
            if (response.IsSuccessStatusCode)
            {
                var portfolioItems = await response.Content.ReadFromJsonAsync<List<PortfolioItemDto>>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                PortfolioListView.ItemsSource = portfolioItems;
            }
        }
        catch (Exception)
        {
            // Sessizce hatayı yut veya kullanıcıya göster
        }
    }

    private async void OnBuyClicked(object sender, EventArgs e)
    {
        await ExecuteTrade(1); // 1 = Buy
    }

    private async void OnSellClicked(object sender, EventArgs e)
    {
        await ExecuteTrade(2); // 2 = Sell
    }

    private async Task ExecuteTrade(int side)
    {
        TradeResultLabel.Text = "İşlem yapılıyor...";
        TradeResultLabel.TextColor = Colors.LightGray;

        try
        {
            if (string.IsNullOrWhiteSpace(SymbolEntry.Text) || !decimal.TryParse(QuantityEntry.Text, out decimal qty))
            {
                TradeResultLabel.Text = "Geçerli sembol ve miktar girin.";
                TradeResultLabel.TextColor = Colors.IndianRed;
                return;
            }

            var orderRequest = new
            {
                assetSymbol = SymbolEntry.Text.ToUpper(),
                side = side,
                quantity = qty
            };

            var response = await _httpClient.PostAsJsonAsync($"{ApiBaseUrl}/Order/market", orderRequest);

            if (response.IsSuccessStatusCode)
            {
                TradeResultLabel.Text = side == 1 ? "Alım Başarılı!" : "Satış Başarılı!";
                TradeResultLabel.TextColor = Colors.LightGreen;
                await LoadDashboardData(); // Bakiyeyi ve portföyü yenile
            }
            else
            {
                var errorMsg = await response.Content.ReadAsStringAsync();
                TradeResultLabel.Text = $"Hata: {errorMsg}";
                TradeResultLabel.TextColor = Colors.IndianRed;
            }
        }
        catch (Exception ex)
        {
            TradeResultLabel.Text = "Bağlantı hatası.";
            TradeResultLabel.TextColor = Colors.IndianRed;
        }
    }

    private async void OnDepositClicked(object sender, EventArgs e)
    {
        DepositResultLabel.Text = "";
        try
        {
            if (decimal.TryParse(DepositEntry.Text, out decimal amount) && amount > 0)
            {
                var request = new { amount = amount };
                var response = await _httpClient.PostAsJsonAsync($"{ApiBaseUrl}/Wallet/deposit", request);
                
                if (response.IsSuccessStatusCode)
                {
                    DepositResultLabel.Text = "Başarılı!";
                    DepositResultLabel.TextColor = Colors.LightGreen;
                    DepositEntry.Text = "";
                    await LoadWalletBalance();
                }
            }
        }
        catch
        {
            DepositResultLabel.Text = "Hata";
            DepositResultLabel.TextColor = Colors.IndianRed;
        }
    }

    private async void OnRefreshClicked(object sender, EventArgs e)
    {
        await LoadDashboardData();
    }

    private void OnLogoutClicked(object sender, EventArgs e)
    {
        // Login sayfasına geri dön
        Application.Current.MainPage = new NavigationPage(new MainPage());
    }
}

public class PortfolioItemDto
{
    public string AssetId { get; set; }
    public string Symbol { get; set; }
    public decimal TotalQuantity { get; set; }
    public decimal AverageBuyPrice { get; set; }
    public decimal CurrentPrice { get; set; }
    public decimal ProfitLoss { get; set; }

    [JsonIgnore]
    public Color ProfitLossColor => ProfitLoss >= 0 ? Colors.LightGreen : Colors.IndianRed;
}
