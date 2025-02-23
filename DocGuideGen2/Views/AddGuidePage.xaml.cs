using Microsoft.Maui.Controls;
using DocGuideGen2.Services;
using System;

namespace DocGuideGen2.Views
{
    public partial class AddGuidePage : ContentPage
    {
        private string _selectedRole = string.Empty;
        private readonly DatabaseService _databaseService;

        public AddGuidePage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
        }

        private void OnCloseClicked(object sender, EventArgs e)
        {
            if (Application.Current.MainPage is FlyoutPage flyoutPage)
            {
                flyoutPage.Detail = new NavigationPage(new HomeView());
                flyoutPage.IsPresented = false;
            }
        }

        private async void OnConfirmClicked(object sender, EventArgs e)
        {
            try
            {
                int guideType = _selectedRole == "Guide" ? 1 : 2;

                await _databaseService.AddGuideAsync(NameEntry.Text, RegistryEntry.Text, RutEntry.Text, guideType);

                await DisplayAlert("Success", "Guia Adicionado ao banco", "OK");

                // Clear fields after adding
                NameEntry.Text = string.Empty;
                RegistryEntry.Text = string.Empty;
                RutEntry.Text = string.Empty;
                GuideRadioButton.IsChecked = false;
                AssistantRadioButton.IsChecked = false;
                _selectedRole = string.Empty;

                ValidateForm();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to add guide: {ex.Message}", "OK");
            }
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateForm();
        }

        private void OnRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (sender is RadioButton radioButton && e.Value)
            {
                _selectedRole = radioButton.Value.ToString();
            }
            else if (!e.Value)
            {
                _selectedRole = string.Empty;
            }

            ValidateForm();
        }

        private void ValidateForm()
        {
            bool isFormValid = !string.IsNullOrWhiteSpace(NameEntry.Text) &&
                               !string.IsNullOrWhiteSpace(RegistryEntry.Text) &&
                               !string.IsNullOrWhiteSpace(RutEntry.Text) &&
                               !string.IsNullOrWhiteSpace(_selectedRole);

            ConfirmButton.IsEnabled = isFormValid;
        }
    }
}
