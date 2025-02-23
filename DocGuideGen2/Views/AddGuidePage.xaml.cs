using Microsoft.Maui.Controls;

namespace DocGuideGen2.Views
{
    public partial class AddGuidePage : ContentPage
    {
        private string selectedRole = string.Empty;

        public AddGuidePage()
        {
            InitializeComponent();
        }

        // Método chamado ao clicar no botão Close
        private void OnCloseClicked(object sender, EventArgs e)
        {
            if (Application.Current.MainPage is FlyoutPage flyoutPage)
            {
                flyoutPage.Detail = new NavigationPage(new HomeView());
                flyoutPage.IsPresented = false;
            }
        }

        // Método para o botão Confirm
        private void OnConfirmClicked(object sender, EventArgs e)
        {
            // Lógica de confirmação
            DisplayAlert("Success", "All fields are filled and confirmed!", "OK");
        }

        // Evento para validar entrada de texto
        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateForm();
        }

        // Evento para capturar seleção do RadioButton
        private void OnRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (sender is RadioButton rb && e.Value)
            {
                selectedRole = rb.Value.ToString();
            }
            else if (!e.Value)
            {
                selectedRole = string.Empty;
            }

            ValidateForm();
        }

        // Valida se todos os campos estão preenchidos
        private void ValidateForm()
        {
            bool isFormValid = !string.IsNullOrWhiteSpace(NameEntry.Text) &&
                               !string.IsNullOrWhiteSpace(RegistryEntry.Text) &&
                               !string.IsNullOrWhiteSpace(RutEntry.Text) &&
                               !string.IsNullOrWhiteSpace(selectedRole);

            ConfirmButton.IsEnabled = isFormValid;
        }
    }
}
