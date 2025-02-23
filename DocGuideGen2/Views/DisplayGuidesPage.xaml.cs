using Microsoft.Maui.Controls;

namespace DocGuideGen2.Views
{
    public partial class DisplayGuidesPage : ContentPage
    {
        public DisplayGuidesPage()
        {
            InitializeComponent();
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
