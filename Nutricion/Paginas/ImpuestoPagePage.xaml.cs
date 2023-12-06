namespace Nutricion.Paginas;
using Nutricion.Models;
using Newtonsoft.Json;
using Nutricion.API.Models;
using System.Text;

public partial class ImpuestoPagePage : ContentPage
{
    private HttpClient client = new HttpClient();
    public ImpuestoPagePage()
	{
		InitializeComponent();
        imgResultado.Source = ImageSource.FromUri(new Uri("https://www.eleconomista.com.mx/__export/1594260272636/sites/eleconomista/img/2020/07/08/tasa-corporativa_090720.png_1074402576.png"));
    }
    private void btnCalcular_Clicked(object sender, EventArgs e)
    {
        if (TxtCantidad.Text == null || TxtPorcentaje.Text == null)
        {
            DisplayAlert("Alerta", "Rellene los datos", "Ok");
        }
        else {
            float Ccantidad, Porcentaje, resultado;
            Ccantidad = float.Parse(TxtCantidad.Text);
            Porcentaje = float.Parse(TxtPorcentaje.Text);
            resultado = Ccantidad * (Porcentaje / 100);

            Impuesto imp = new Impuesto();
            imp.Cantidad = Ccantidad;
            imp.Procentage = Porcentaje;
            imp.Total= resultado;
            App.IMCrepository.AgregarRegistro(imp);
            lblGuardar.Text = App.IMCrepository.StatusMessage;
            RegistrarApi(resultado);
        }
    }
    private async void RegistrarApi(float resultado)
    {
        string url = "https://impuestoserver.database.windows.net/api/Impuestos/guardar_Impuesto";
        Impuesto imp = new Impuesto();
        imp.Cantidad = float.Parse(TxtCantidad.Text);
        imp.Procentage = float.Parse(TxtPorcentaje.Text);
        imp.Total = resultado;
        imp.Fecha = DateTime.Now;

        String jsonImp = JsonConvert.SerializeObject(imp);
        StringContent content = new StringContent(jsonImp, Encoding.UTF8, "application/json");

        var respuesta = await client.PostAsync(url, content);
        if (respuesta.IsSuccessStatusCode)
        {
            await DisplayAlert("registro", "se ha registrado correctamente", "ok");
        }
        else
        {
            await DisplayAlert("Error", "No se pudo registrar", "Ok");
        }
    }
}