using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Nutricion.Paginas;

namespace Nutricion
{
    public partial class App : Application
    {
        public static Repository IMCrepository { get; set; }
        public App(Repository repository)
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new AppShell());
            //MainPage = new NavigationPage(new RegistroPage());
            MainPage = new NavigationPage(new LoginPage());
            //MainPage = new NavigationPage(new LoginPage());
            AppCenter.Start("android={\"b3dff20b-e4ba-4396-8e91-f898d0981259\"};" +
                  "uwp={\"\"};" +
                  "ios={\"\"};" +
                  "macos={\"\"};",
                  typeof(Analytics), typeof(Crashes));

            IMCrepository = repository;
        }
    }
}