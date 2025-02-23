using Microsoft.Maui.Controls;

namespace DocGuideGen2.Views
{
    public partial class AddGuidePage : ContentPage
    {
        public AddGuidePage()
        {
            InitializeComponent();
        }

        // M�todo chamado ao clicar no bot�o Close
        private void OnCloseClicked(object sender, EventArgs e)
        {
            // Se a MainPage for um FlyoutPage, navega de volta para HomeView e fecha o flyout.
            if (Application.Current.MainPage is FlyoutPage flyoutPage)
            {
                flyoutPage.Detail = new NavigationPage(new HomeView());
                flyoutPage.IsPresented = false;
            }
        }

        // M�todo para o bot�o Confirm (ainda sem implementa��o)
        private void OnConfirmClicked(object sender, EventArgs e)
        {
            // L�gica de confirma��o a ser implementada
        }
    }
}
