using Microsoft.Maui.Controls;

namespace DocGuideGen2.Views
{
    public partial class MainFlyoutPage : FlyoutPage
    {
        public MainFlyoutPage()
        {
            InitializeComponent();
        }

        private void OnToggleExpanderClicked(object sender, EventArgs e)
        {
            // Alterna o estado do Expander sem fechar o flyout
            OptionsExpander.IsExpanded = !OptionsExpander.IsExpanded;
        }

        private void OnOption1Clicked(object sender, EventArgs e)
        {
            // Lógica para Option 1
        }

        private void OnOption2Clicked(object sender, EventArgs e)
        {
            // Lógica para Option 2
        }
    }
}
