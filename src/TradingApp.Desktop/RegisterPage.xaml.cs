using System;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.Maui.Controls;

namespace TradingApp.Desktop;

public partial class RegisterPage : ContentPage
{
    private readonly HttpClient _httpClient = new HttpClient();
    private const string ApiUrl = "http://localhost:5000/api/Auth/register";

    public RegisterPage()
    {
        InitializeComponent();
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        RegisterButton.IsEnabled = false;
        ResultLabel.Text = "Kayıt olunuyor...";
        ResultLabel.TextColor = Colors.LightGray;
        
        bool isSuccess = false;

        try
        {
            if (string.IsNullOrWhiteSpace(FirstNameEntry.Text) || 
                string.IsNullOrWhiteSpace(LastNameEntry.Text) ||
                string.IsNullOrWhiteSpace(EmailEntry.Text) || 
                string.IsNullOrWhiteSpace(PasswordEntry.Text))
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    ResultLabel.TextColor = Colors.IndianRed;
                    ResultLabel.Text = "Lütfen tüm alanları doldurun.";
                    RegisterButton.IsEnabled = true;
                });
                return;
            }

            var registerData = new
            {
                firstName = FirstNameEntry.Text,
                lastName = LastNameEntry.Text,
                email = EmailEntry.Text,
                password = PasswordEntry.Text
            };

            var response = await _httpClient.PostAsJsonAsync(ApiUrl, registerData);

            if (response.IsSuccessStatusCode)
            {
                isSuccess = true;
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    ResultLabel.TextColor = Colors.LightGreen;
                    ResultLabel.Text = "Kayıt Başarılı! Yönlendiriliyorsunuz...";
                });
                
                await Task.Delay(1500); // 1.5 saniye bekle
                
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync(); // Sayfayı kapat
                });
            }
            else
            {
                var errorMsg = await response.Content.ReadAsStringAsync();
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    ResultLabel.TextColor = Colors.IndianRed;
                    ResultLabel.Text = $"Kayıt başarısız: {errorMsg}";
                });
            }
        }
        catch (Exception ex)
        {
            GlobalExceptionHandler.LogException(ex, "Register");
            MainThread.BeginInvokeOnMainThread(() =>
            {
                ResultLabel.TextColor = Colors.IndianRed;
                ResultLabel.Text = "Bağlantı hatası: API çalışıyor mu?";
            });
        }
        finally
        {
            // Yalnızca kayıt başarısız olduysa butonu tekrar aktif et.
            // Başarılı olduysa sayfa zaten kapanıyor, butona dokunmak çökertir.
            if (!isSuccess)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    RegisterButton.IsEnabled = true;
                });
            }
        }
    }

    private async void OnBackToLoginClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
