using OrganizerV2.TeklaModels.Views;
using System.Windows.Input;
using TeklaModels.ClipPlanes;

namespace OrganizerV2.ViewModels
{
    public class ViewPropertiesViewModel : ViewModelBase
    {
        private bool _isOnlySelectedViewsChecked;
        private string _onlySelectedViewsCheckboxContent;
        public ICommand CreateClipPlanesCommand { get; private set; }
        public ICommand DeleteClipPlanesCommand { get; private set; }
        public ICommand RedrawViewsCommand { get; private set; }

        public ViewPropertiesViewModel()
        {
            IsOnlySelectedViewsChecked = false;
            CreateClipPlanesCommand = new RelayCommand(ExecuteCreateClipPlanes);
            DeleteClipPlanesCommand = new RelayCommand(ExecuteDeleteClipPlanes);
            RedrawViewsCommand = new RelayCommand(ExecuteRedrawViews);
        }

        public bool IsOnlySelectedViewsChecked
        {
            get { return _isOnlySelectedViewsChecked; }
            set
            {
                _isOnlySelectedViewsChecked = value;
                OnlySelectedViewsCheckboxContent = value ? "Only Selected Views" : "All Views";
                ClipPlanes.IsOnlySelectedViewsChecked = value;
                OnPropertyChanged(nameof(OnlySelectedViewsCheckboxContent));
            }
        }

        public string OnlySelectedViewsCheckboxContent
        {
            get { return _onlySelectedViewsCheckboxContent; }
            set
            {
                _onlySelectedViewsCheckboxContent = value;
                OnPropertyChanged(nameof(OnlySelectedViewsCheckboxContent));
            }
        }

        private void ExecuteCreateClipPlanes(object parameter)
        {
            ClipPlanes.DeleteClipPlanes();
            ClipPlanes.CreateClipPlanes();
        }
        private void ExecuteDeleteClipPlanes(object parameter)
        {
            ClipPlanes.DeleteClipPlanes();
        }
        private void ExecuteRedrawViews(object parameter)
        {
            Visability.RedrawViews();
        }
    }
}
