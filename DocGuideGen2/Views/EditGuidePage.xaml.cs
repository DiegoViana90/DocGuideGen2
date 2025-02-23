using DocGuideGen2.Models;
using DocGuideGen2.ViewModels;

namespace DocGuideGen2.Views // Corrigido o namespace
{
    public partial class EditGuidePage : ContentPage
    {
        private readonly Func<Task> _onGuideUpdated;

        public EditGuidePage(Guide guide, Func<Task> onGuideUpdated)
        {
            InitializeComponent();
            _onGuideUpdated = onGuideUpdated; // Salva o callback
            BindingContext = new EditGuideViewModel(guide, _onGuideUpdated);
        }
    }

}
