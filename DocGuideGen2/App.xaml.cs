using Microsoft.Maui.Controls;
using DocGuideGen2.Views; // Importa o namespace para MainFlyoutPage

namespace DocGuideGen2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new MainFlyoutPage(); // Agora o compilador encontra MainFlyoutPage
        }
    }
}
