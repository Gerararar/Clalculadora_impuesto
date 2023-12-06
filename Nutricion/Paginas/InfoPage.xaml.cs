using Newtonsoft.Json;
using Nutricion.Models;
using System.Net.Http.Headers;

namespace Nutricion.Paginas;

public partial class InfoPage : ContentPage
{
    private HttpClient client = new HttpClient();
    //public DatosPage(string nombre)
    public InfoPage()
    {
        InitializeComponent();
        ObtenerDatos();
    }
    private async void vtnIMX_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ImpuestoPagePage());
    }
    private void Obtener()
    {
        List<Impuesto> registros2 = App.IMCrepository.ObtenerRegistros();
        lstRegistros.ItemsSource = registros2;
        DisplayAlert("Consulta", App.IMCrepository.StatusMessage, "OK");
    }

    private async void ObtenerDatos()
    {
        string url = "https://nutriciongerar.azurewebsites.net/api/IMC/lista";
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await SecureStorage.GetAsync("token"));
        var respuesta = client.GetAsync(url);
        if (!respuesta.Result.IsSuccessStatusCode)
        {
            await DisplayAlert("Error", "No se pudo obtener los datos", "ok");
        }
        var json = await respuesta.Result.Content.ReadAsStringAsync();
        List<Impuesto> lista = JsonConvert.DeserializeObject<List<Impuesto>>(json);

        lstRegistros.ItemsSource = lista;
        if (lista.Count == 0)
        {
            await DisplayAlert("Mensaje", "No existe datos para mostrar", "ok");
        }

    }
    private void btnIrImpuesto_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ImpuestoPagePage());
    }
}