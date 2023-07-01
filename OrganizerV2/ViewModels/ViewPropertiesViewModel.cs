using OrganizerV2.TeklaModels.Views;
using System.Windows.Input;
using TeklaModels.ClipPlanes;

namespace OrganizerV2.ViewModels
{
    public class ViewPropertiesViewModel : MainViewModel
    {
        private bool _isOnlySelectedViewsChecked;
        private string _onlySelectedViewsCheckboxContent;
        public ICommand CreateClipPlanesCommand { get; private set; }
        public ICommand DeleteClipPlanesCommand { get; private set; }
        public ICommand RedrawViewsCommand { get; private set; }
        public ICommand RestoreWorkPlaneCommand { get; private set; }
        public ICommand ColorByPhaseCommand { get; private set; }
        public ICommand ColorByClassCommand { get; private set; }
        public ICommand ColorByMaterialCommand { get; private set; }
        public ICommand ColorByProfileCommand { get; private set; }

        public ViewPropertiesViewModel()
        {
            IsOnlySelectedViewsChecked = false;
            CreateClipPlanesCommand = new RelayCommand(ExecuteCreateClipPlanes);
            DeleteClipPlanesCommand = new RelayCommand(ExecuteDeleteClipPlanes);
            RedrawViewsCommand = new RelayCommand(ExecuteRedrawViews);
            RestoreWorkPlaneCommand = new RelayCommand(ExecuteRestoreWorkPlane);
            ColorByPhaseCommand = new RelayCommand(ExecuteColorByPhase);
            ColorByClassCommand = new RelayCommand(ExecuteColorByClass);
            ColorByMaterialCommand = new RelayCommand(ExecuteColorByMaterial);
            ColorByProfileCommand = new RelayCommand(ExecuteColorByProfile);
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
        private void ExecuteRestoreWorkPlane(object parameter)
        {
            Visability.RestoreWorkPlane();
        }
        private void ExecuteColorByPhase(object parameter)
        {
            Visability.ColorRepresentation("ColorByPhase");
        }
        private void ExecuteColorByClass(object parameter)
        {
            Visability.ColorRepresentation("ColorByClass");
        }        
        private void ExecuteColorByMaterial(object parameter)
        {
            Visability.ColorRepresentation("ColorByMaterial");
        }        
        private void ExecuteColorByProfile(object parameter)
        {
            Visability.ColorRepresentation("ColorByProfile");
        }

    }
}
