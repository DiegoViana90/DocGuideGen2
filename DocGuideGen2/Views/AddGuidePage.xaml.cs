using Microsoft.Maui.Controls;
using DocGuideGen2.ViewModels;

namespace DocGuideGen2.Views
{
    public partial class AddGuidePage : ContentPage
    {
        public AddGuidePage()
        {
            InitializeComponent();
            BindingContext = new AddGuideViewModel();
        }
    }
}
