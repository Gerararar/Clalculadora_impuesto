using Newtonsoft.Json;
using Nutricion.API.Models;
using Nutricion.Models;
using System.Net.Http.Json;
using System.Text;

namespace Nutricion.Paginas;

public partial class RegistroPage : ContentPage
{
	private readonly HttpClient client = new HttpClient();
	public RegistroPage()
	{
		InitializeComponent();
	}
	private async void btnRegistros_Clicked(object sender, EventArgs e)
	{
		string url = "https://nutriciongerar.azurewebsites.net/api/Cuentas/registro";
		User usuario = new User();
		usuario.UserName = txtUsername.Text;
		usuario.Email = txtEmail.Text;
		usuario.Password = txtPassword.Text;
		String jsonUser = JsonConvert.SerializeObject(usuario);
		StringContent content = new StringContent(jsonUser, Encoding.UTF8,"application/json");

		var respuesta = await client.PostAsync(url, content);
		var tokenString = respuesta.Content.ReadAsStringAsync();
		var json = JsonConvert.DeserializeObject<UserToken>(tokenString.Result);
		if (respuesta.IsSuccessStatusCode) {
			await SecureStorage.SetAsync("token", json.Token);
		}
		else
		{
			await DisplayAlert("Error", "No se pudo registrar", "Ok");
		}
    }

	
}