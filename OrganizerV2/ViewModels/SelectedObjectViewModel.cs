using MySql.Data.MySqlClient;
using OrganizerV2.Models;
using System;
using System.Collections.Generic;
using Tekla.Structures.Model;

namespace OrganizerV2.ViewModels
{
    public class SelectedObjectViewModel : MainViewModel
    {
        private static readonly Events _events = new Events();
        private static readonly object _selectionEventHandlerLock = new object();
        private bool _isViewVisibleBeamProperties;
        private SelectedBeamViewModel _selectedBeamViewModel;

        public SelectedObjectViewModel()
        {
            _events.SelectionChange += Events_SelectionChangeEvent;
            _events.Register();
            IsViewVisibleBeamProperties = false;
        }
        public bool IsViewVisibleBeamProperties
        {
            get { return _isViewVisibleBeamProperties; }
            set
            {
                _isViewVisibleBeamProperties = value;
                OnPropertyChanged(nameof(IsViewVisibleBeamProperties));
            }
        }
        public SelectedBeamViewModel SelectedBeamViewModel
        {
            get { return _selectedBeamViewModel; }
            set
            {
                _selectedBeamViewModel = value;
                OnPropertyChanged(nameof(
                    ));
            }
        }
        private void Events_SelectionChangeEvent()
        {
            lock (_selectionEventHandlerLock)
            {
                ModelObjectEnumerator selectedObjects = new Tekla.Structures.Model.UI.ModelObjectSelector().GetSelectedObjects();
                if (selectedObjects.GetSize() == 0 || selectedObjects.GetSize() > 1)
                {
                    HandleNoObjectSelection();
                }
                else
                {
                    ProcessSelectedObjects(selectedObjects);
                }
            }
        }
        private void ProcessSelectedObjects(ModelObjectEnumerator selectedObjects)
        {
            while (selectedObjects.MoveNext())
            {
                var selectedObject = selectedObjects.Current;
                if (selectedObject is Assembly || selectedObject is Connection)
                {
                    HandleNoObjectSelection();
                }
                else if (selectedObject is Beam)
                {
                    SelectedBeamViewModel selectedBeamViewModel = new SelectedBeamViewModel();
                    selectedBeamViewModel.HandleBeamSelection(selectedObject as Beam);
                    IsViewVisibleBeamProperties = true;
                }
                else if (selectedObject is BoltArray)
                {
                    //Do something
                }
            }
        }
        private void HandleNoObjectSelection()
        {
            IsViewVisibleBeamProperties = false;
        }
    }
}
