using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DocGuideGen2.Models
{
    public class Guide : INotifyPropertyChanged
    {
        private int _type;
        private string _displayType;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Registry { get; set; }
        public string Rut { get; set; }

        public int Type
        {
            get => _type;
            set
            {
                if (_type != value)
                {
                    _type = value;
                    DisplayType = _type == 1 ? "Guia" : "Assistente de Guia"; // Sincroniza o DisplayType
                    OnPropertyChanged();
                }
            }
        }

        public string DisplayType
        {
            get => _displayType;
            set
            {
                if (_displayType != value)
                {
                    _displayType = value;
                    Type = _displayType == "Guia" ? 1 : 2; // Atualiza o Type baseado no texto
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
