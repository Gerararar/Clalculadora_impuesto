using Newtonsoft.Json;
using Nutricion.API.Models;
using Nutricion.Models;
using System.Text;

namespace Nutricion.Paginas;

public partial class LoginPage : ContentPage
{
    private readonly HttpClient client = new HttpClient();
    public LoginPage()
	{
		InitializeComponent();
	}
    private async void btnLogin_Clicked(object sender, EventArgs e)
    {
        string url = "https://nutriciongerar.azurewebsites.net/api/Cuentas/login";
        /*
        User usuario = new User();
        usuario.UserName = txtUsername.Text;
        usuario.Email = txtEmail.Text;
        usuario.Password = txtPassword.Text;
        String jsonUser = JsonConvert.SerializeObject(usuario);
        StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");

        var respuesta = await client.PostAsync(url, content);
        if (respuesta.IsSuccessStatusCode)
        {
            await DisplayAlert("token", "Se logeo", "Ok");
        }
        else
        {
            await DisplayAlert("Error", "No se pudo logear", "Ok");
        }
        */
        User user = new User
        {
            UserName = txtUsername.Text,
            Email = txtEmail.Text,
            Password = txtPassword.Text
        };
        string jsonUser = JsonConvert.SerializeObject(user);
        StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
        var respuesta = await client.PostAsync(url, content);
        if (respuesta.IsSuccessStatusCode)
        {
            var tokenString = respuesta.Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject<UserToken>(tokenString.Result);
            if (respuesta.IsSuccessStatusCode)
            {
                await SecureStorage.SetAsync("token", json.Token);
                await Navigation.PushAsync(new InfoPage());
            }
        }
        else
        {
            await DisplayAlert("Error", "Error en los datos del usuario", "Ok");
        }
    }
}