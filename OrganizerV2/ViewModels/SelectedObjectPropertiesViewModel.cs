using System;
using System.Collections.Generic;
using Tekla.Structures.Model;

namespace OrganizerV2.ViewModels
{
    public class SelectedObjectPropertiesViewModel : MainViewModel
    {
        private static Events _events = new Events();
        private static readonly object _selectionEventHandlerLock = new object();

        private bool _isViewVisibleBeamProperties;
        private Dictionary<string, string> _beamProperties;

        public SelectedObjectPropertiesViewModel()
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
        public Dictionary<string, string> BeamProperties
        {
            get { return _beamProperties; }
            set
            {
                _beamProperties = value;
                OnPropertyChanged(nameof(BeamProperties));
            }
        }
        private void Events_SelectionChangeEvent()
        {
            lock (_selectionEventHandlerLock)
            {
                ModelObjectEnumerator selectedObjects = new Tekla.Structures.Model.UI.ModelObjectSelector().GetSelectedObjects();
                if (selectedObjects.GetSize() == 0 || selectedObjects.GetSize() > 1)
                {
                    HandleNoBeamSelection();
                }
                else
                {
                    ProcessSelectedObjects(selectedObjects);
                }
            }
        }
        private void HandleNoBeamSelection()
        {
            IsViewVisibleBeamProperties = false;
            BeamProperties = null;
        }
        private void ProcessSelectedObjects(ModelObjectEnumerator selectedObjects)
        {
            while (selectedObjects.MoveNext())
            {
                var selectedObject = selectedObjects.Current;
                if (selectedObject is Assembly || selectedObject is Connection)
                {
                    HandleNoBeamSelection();
                }
                else if (selectedObject is Beam)
                {
                    HandleBeamSelection(selectedObject as Beam);
                }
            }
        }
        private void HandleBeamSelection(Beam beam)
        {
            var properties = GetBeamProperties(beam);
            IsViewVisibleBeamProperties = true;
            BeamProperties = properties;
        }
        private Dictionary<string, string> GetBeamProperties(Beam beam)
        {
            var properties = new Dictionary<string, string>();
            string beamProfile = beam.Profile.ProfileString;
            properties.Add("BeamProfile", beamProfile);
            properties.Add("BeamName", beam.Name);
            properties.Add("BeamClass", beam.Class);
            beam.GetPhase(out Phase beamPhaseTemp);
            properties.Add("BeamPhase", beamPhaseTemp.PhaseNumber.ToString());
            properties.Add("BeamMaterial", beam.Material.MaterialString);
            properties.Add("BeamIdentifier", beam.Identifier.ToString());
            properties.Add("BeamPartStartNumber", beam.PartNumber.Prefix + "/" + beam.PartNumber.StartNumber.ToString());
            properties.Add("BeamAssemblyStartNumber", beam.AssemblyNumber.Prefix + "/" + beam.AssemblyNumber.StartNumber.ToString());
            properties.Add("BeamFinish", beam.Finish.ToString());
            properties.Add("BeamIsUpToDate", beam.IsUpToDate.ToString());
            properties.Add("BeamModificationDate", beam.ModificationTime.ToString());
            properties.Add("BeamStartPoint", Math.Round(beam.StartPoint.X, 2).ToString() + "; " + Math.Round(beam.StartPoint.Y, 2).ToString() + "; " + Math.Round(beam.StartPoint.Z, 2).ToString());
            properties.Add("BeamEndPoint", Math.Round(beam.EndPoint.X, 2).ToString() + "; " + Math.Round(beam.EndPoint.Y, 2).ToString() + "; " + Math.Round(beam.EndPoint.Z, 2).ToString());
            return properties;
        }
    }
}
