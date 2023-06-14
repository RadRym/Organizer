using OrganizerV2.Models;
using OrganizerV2.TeklaModels.Views;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using TeklaModels.ClipPlanes;

namespace OrganizerV2.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private TeklaModel _teklaModel;
        private string _selectionChangeMessage;

        public string SelectionChangeMessage
        {
            get { return _selectionChangeMessage; }
            set
            {
                _selectionChangeMessage = value;
                OnPropertyChanged(nameof(SelectionChangeMessage));
            }
        }

        public MainViewModel()
        {
            _teklaModel = new TeklaModel();
        }
    }
}
