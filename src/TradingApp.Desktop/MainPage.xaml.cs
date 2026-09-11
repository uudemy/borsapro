using System.Net.Http.Json;

namespace TradingApp.Desktop;

public partial class MainPage : ContentPage
{
    private readonly HttpClient _httpClient = new HttpClient();
    
    // API Adresimiz
    private const string ApiUrl = "http://localhost:5000/api/Auth/login";

    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        ((Button)sender).IsEnabled = false;
        ResultLabel.Text = "Giriş yapılıyor...";
        ResultLabel.TextColor = Colors.LightGray;

        try
        {
            var loginData = new
            {
                email = EmailEntry.Text,
                password = PasswordEntry.Text
            };

            var response = await _httpClient.PostAsJsonAsync(ApiUrl, loginData);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                ResultLabel.TextColor = Colors.LightGreen;
                ResultLabel.Text = "Giriş Başarılı! Token alındı.";
                
                // Dashboard (Ana Ekran) sayfasına Token ile birlikte geçiş yap
                Application.Current.MainPage = new NavigationPage(new DashboardPage(result.Token));
            }
            else
            {
                ResultLabel.TextColor = Colors.IndianRed;
                ResultLabel.Text = "Hata: E-posta veya şifre yanlış.";
            }
        }
        catch (Exception ex)
        {
            ResultLabel.TextColor = Colors.IndianRed;
            ResultLabel.Text = "Bağlantı hatası: API çalışıyor mu? " + ex.Message;
        }
        finally
        {
            ((Button)sender).IsEnabled = true;
        }
    }
}

public class LoginResponse
{
    public string Token { get; set; }
}
