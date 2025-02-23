using Microsoft.Maui.Controls;
using DocGuideGen2.ViewModels;

namespace DocGuideGen2.Views
{
    public partial class DisplayGuidesPage : ContentPage
    {
        private DisplayGuidesViewModel ViewModel => BindingContext as DisplayGuidesViewModel;

        public DisplayGuidesPage()
        {
            InitializeComponent();
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void OnFilterChanged(object sender, CheckedChangedEventArgs e)
        {
            if (!(sender is RadioButton radioButton) || !e.Value)
                return;

            string selectedFilter = radioButton.Content.ToString();
            ViewModel?.UpdateFilter(selectedFilter);
        }
    }
}
