using Microsoft.Maui.Controls;

namespace DocGuideGen2.Views
{
    public partial class AddGuidePage : ContentPage
    {
        public AddGuidePage()
        {
            InitializeComponent();
        }

        // Método chamado ao clicar no botão Close
        private void OnCloseClicked(object sender, EventArgs e)
        {
            // Se a MainPage for um FlyoutPage, navega de volta para HomeView e fecha o flyout.
            if (Application.Current.MainPage is FlyoutPage flyoutPage)
            {
                flyoutPage.Detail = new NavigationPage(new HomeView());
                flyoutPage.IsPresented = false;
            }
        }

        // Método para o botão Confirm (ainda sem implementação)
        private void OnConfirmClicked(object sender, EventArgs e)
        {
            // Lógica de confirmação a ser implementada
        }
    }
}
