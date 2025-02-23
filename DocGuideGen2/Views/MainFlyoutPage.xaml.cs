using Microsoft.Maui.Controls;
using DocGuideGen2.Views; // Import the namespace for AddGuidePage

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
            OptionsExpander.IsExpanded = !OptionsExpander.IsExpanded;
        }

        private void AddGuideView(object sender, EventArgs e)
        {
            // Navigate to AddGuidePage and close the flyout
            Detail = new NavigationPage(new AddGuidePage());
            IsPresented = false;
        }

        private void OnOption2Clicked(object sender, EventArgs e)
        {
            // Logic for other options
        }
        private void HomeView(object sender, EventArgs e)
        {
            // Navigate to AddGuidePage and close the flyout
            Detail = new NavigationPage(new HomeView());
            IsPresented = false;
        }

        private void DisplayGuidesView(object sender, EventArgs e)
        {
            // Navigate to DisplayGuidesPage and close the flyout
            Detail = new NavigationPage(new DisplayGuidesPage());
            IsPresented = false;
        }

    }
}
