using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DocGuideGen2.Models;
using DocGuideGen2.Services;

namespace DocGuideGen2.ViewModels
{
    public class DisplayGuidesViewModel : INotifyPropertyChanged
    {
        private readonly DatabaseService _databaseService;
        private string _selectedFilter;
        private string _placeholderText;
        private bool _isEmpty;

        public ObservableCollection<Guide> Guides { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public DisplayGuidesViewModel()
        {
            _databaseService = new DatabaseService();
            Guides = new ObservableCollection<Guide>();
            SelectedFilter = "Todos";
            PlaceholderText = "(Exibindo todos os guias e assistentes...)";
            LoadGuidesAsync(SelectedFilter);
        }

        public string SelectedFilter
        {
            get => _selectedFilter;
            set
            {
                if (_selectedFilter != value)
                {
                    _selectedFilter = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PlaceholderText
        {
            get => _placeholderText;
            set
            {
                if (_placeholderText != value)
                {
                    _placeholderText = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsEmpty
        {
            get => _isEmpty;
            set
            {
                if (_isEmpty != value)
                {
                    _isEmpty = value;
                    OnPropertyChanged();
                }
            }
        }

        public async void UpdateFilter(string filter)
        {
            SelectedFilter = filter;
            await LoadGuidesAsync(filter);

            switch (filter)
            {
                case "Todos":
                    PlaceholderText = "(Exibindo todos os guias e assistentes...)";
                    break;
                case "Guia":
                    PlaceholderText = "(Exibindo apenas guias...)";
                    break;
                case "Assistente de Guia":
                    PlaceholderText = "(Exibindo apenas assistentes de guia...)";
                    break;
            }
        }

        private async Task LoadGuidesAsync(string filter)
        {
            var guides = await _databaseService.GetGuidesByFilterAsync(filter);

            Guides.Clear();
            foreach (var guide in guides)
            {
                guide.DisplayType = guide.Type == 1 ? "Guia" : "Assistente de Guia";
                Guides.Add(guide);
            }

            IsEmpty = Guides.Count == 0;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
