using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DocGuideGen2.Models;
using DocGuideGen2.Services;
using Microsoft.Maui.Controls;

namespace DocGuideGen2.ViewModels
{
    public class EditGuideViewModel : INotifyPropertyChanged
    {
        private Guide _guide;
        private readonly Func<Task> _onGuideUpdated;

        public Guide Guide
        {
            get => _guide;
            set
            {
                _guide = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public EditGuideViewModel(Guide guide, Func<Task> onGuideUpdated)
        {
            Guide = guide;
            _onGuideUpdated = onGuideUpdated;

            SaveCommand = new Command(Save);
            CancelCommand = new Command(Cancel);
        }

        private async void Save()
        {
            // Verifica se os campos obrigatórios estão preenchidos
            if (string.IsNullOrWhiteSpace(Guide.Name) || string.IsNullOrWhiteSpace(Guide.Registry))
            {
                await Application.Current.MainPage.DisplayAlert("Erro", "Preencha todos os campos obrigatórios.", "OK");
                return;
            }

            // Atualiza o guia no banco de dados
            var databaseService = new DatabaseService();
            bool success = await databaseService.UpdateGuideAsync(Guide);

            if (success)
            {
                // Exibe a mensagem e aguarda o clique do usuário
                await Application.Current.MainPage.DisplayAlert("Sucesso", "Guia atualizado com sucesso!", "OK");

                // Chama o callback para atualizar a lista
                if (_onGuideUpdated != null)
                    await _onGuideUpdated.Invoke();

                // Fecha o modal após atualizar a lista
                await Application.Current.MainPage.Navigation.PopModalAsync();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Erro", "Falha ao atualizar o guia.", "OK");
            }
        }

        private async void Cancel()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
