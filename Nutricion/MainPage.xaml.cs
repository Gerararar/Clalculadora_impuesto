using Nutricion.Paginas;

namespace Nutricion
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            //ObtenerNombre();
            ObtenerNombreSecure();
        }

        private async void vtnDatos_Clicked(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            await Navigation.PushAsync(new InfoPage());
            
            //Preferences.Set("nombre",txtNombre.Text); //guarda el dato en la cache de la app y solo visible para la app

            await SecureStorage.SetAsync("nombre",txtNombre.Text); //se guarda en un ambiente general del dispositivo y se encrypta. es posible usarla para otra app
        }

        private async void ObtenerNombre()
        {
            var nombre = Preferences.Get("nombre","No existe");
            if(nombre != "No existe")
            {
                txtNombre.Text = nombre;
                await Navigation.PushAsync(new InfoPage());
                Preferences.Remove("nombre");
            }
        }private async void ObtenerNombreSecure()
        {
            var nombre = await SecureStorage.GetAsync("nombre");
            if(nombre != null)
            {
                //txtNombre.Text = nombre;
                await Navigation.PushAsync(new InfoPage());
                SecureStorage.Remove("nombre");
            }
        }
    }
}