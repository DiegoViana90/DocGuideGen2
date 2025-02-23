using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using DocGuideGen2.Services;

namespace DocGuideGen2.ViewModels
{
    public class AddGuideViewModel : INotifyPropertyChanged
    {
        private readonly DatabaseService _databaseService;
        private string _name;
        private string _registry;
        private string _rut;
        private string _selectedRole;
        private bool _isFormValid;

        public event PropertyChangedEventHandler PropertyChanged;

        public AddGuideViewModel()
        {
            _databaseService = new DatabaseService();
            ConfirmCommand = new Command(async () => await OnConfirmClicked(), () => IsFormValid);
        }

        // Properties bound to UI
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); ValidateForm(); }
        }

        public string Registry
        {
            get => _registry;
            set { _registry = value; OnPropertyChanged(); ValidateForm(); }
        }

        public string Rut
        {
            get => _rut;
            set { _rut = value; OnPropertyChanged(); ValidateForm(); }
        }

        public string SelectedRole
        {
            get => _selectedRole;
            set
            {
                if (_selectedRole != value)
                {
                    _selectedRole = value;
                    OnPropertyChanged();
                    ValidateForm(); // Valida o formulário ao mudar o valor
                }
            }
        }

        public bool IsFormValid
        {
            get => _isFormValid;
            set
            {
                if (_isFormValid != value)
                {
                    _isFormValid = value;
                    OnPropertyChanged();
                    ((Command)ConfirmCommand).ChangeCanExecute();
                }
            }
        }

        public ICommand ConfirmCommand { get; }

        // Confirm logic moved from View
        private async Task OnConfirmClicked()
        {
            try
            {
                int guideType = SelectedRole == "Guide" ? 1 : 2;
                string guideRole = guideType == 1 ? "Guia" : "Assistente de Guia";

                // Check if the guide already exists
                bool guideExists = await _databaseService.GuideExistsAsync(Name);

                if (guideExists)
                {
                    await Application.Current.MainPage.DisplayAlert("Aviso", $"{guideRole} {Name} já está adicionado ao banco.", "OK");
                    return;
                }

                // Add the guide
                await _databaseService.AddGuideAsync(Name, Registry, Rut, guideType);

                await Application.Current.MainPage.DisplayAlert("Sucesso", $"{guideRole} {Name} adicionado ao banco.", "OK");

                // Clear fields
                Name = string.Empty;
                Registry = string.Empty;
                Rut = string.Empty;
                SelectedRole = string.Empty;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", $"Falha ao adicionar guia: {ex.Message}", "OK");
            }
        }

        private void ValidateForm()
        {
            IsFormValid = !string.IsNullOrWhiteSpace(Name) &&
                          !string.IsNullOrWhiteSpace(Registry) &&
                          !string.IsNullOrWhiteSpace(Rut) &&
                          !string.IsNullOrWhiteSpace(SelectedRole);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
