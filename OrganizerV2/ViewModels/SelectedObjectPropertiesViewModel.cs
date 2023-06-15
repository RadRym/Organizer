using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tekla.Structures.Model;

namespace OrganizerV2.ViewModels
{
    public class SelectedObjectPropertiesViewModel : MainViewModel
    {
        private static Events _events = new Events();
        private static readonly object _selectionEventHandlerLock = new object();

        private bool _isViewVisibleBeamProperties;
        private string _beamName;
        private string _beamProfile;
        private string _beamClass;
        private string _beamPhase;
        private string _beamMaterial;
        private string _beamIdentifier;
        private string _beamPartStartNumber;
        private string _beamAssemblyStartNumber;
        private string _beamEndPoint;
        private string _beamStartPoint;
        private string _beamFinish;
        private string _beamPosition;
        private string _beamIsUpToDate;
        private string _beamDeformingData;
        private string _beamModificationDate;


        public SelectedObjectPropertiesViewModel()
        {
            _events = new Events();
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
        public string BeamProfile
        {
            get { return _beamProfile; }
            set
            {
                _beamProfile = value;
                OnPropertyChanged(nameof(BeamProfile));
            }
        }
        public string BeamName
        {
            get { return _beamName; }
            set
            {
                _beamName = value;
                OnPropertyChanged(nameof(BeamName));
            }
        }
        public string BeamClass
        {
            get { return _beamClass; }
            set
            {
                _beamClass = value;
                OnPropertyChanged(nameof(BeamClass));
            }
        }
        public string BeamPhase
        {
            get { return _beamPhase; }
            set
            {
                _beamPhase = value;
                OnPropertyChanged(nameof(BeamPhase));
            }
        }
        public string BeamMaterial
        {
            get { return _beamMaterial; }
            set
            {
                _beamMaterial = value;
                OnPropertyChanged(nameof(BeamMaterial));
            }
        }
        public string BeamIdentifier
        {
            get { return _beamIdentifier; }
            set
            {
                _beamIdentifier = value;
                OnPropertyChanged(nameof(BeamIdentifier));
            }
        }
        public string BeamPartStartNumber
        {
            get { return _beamPartStartNumber; }
            set
            {
                _beamPartStartNumber = value;
                OnPropertyChanged(nameof(BeamPartStartNumber));
            }
        }
        public string BeamAssemblyStartNumber
        {
            get { return _beamAssemblyStartNumber; }
            set
            {
                _beamAssemblyStartNumber = value;
                OnPropertyChanged(nameof(BeamAssemblyStartNumber));
            }
        }
        public string BeamFinish
        {
            get { return _beamFinish; }
            set
            {
                _beamFinish = value;
                OnPropertyChanged(nameof(BeamFinish));
            }
        }
        public string BeamPosition
        {
            get { return _beamPosition; }
            set
            {
                _beamPosition = value;
                OnPropertyChanged(nameof(BeamPosition));
            }
        }
        public string BeamIsUpToDate
        {
            get { return _beamIsUpToDate; }
            set
            {
                _beamIsUpToDate = value;
                OnPropertyChanged(nameof(BeamIsUpToDate));
            }
        }
        public string BeamDeformingData
        {
            get { return _beamDeformingData; }
            set
            {
                _beamDeformingData = value;
                OnPropertyChanged(nameof(BeamDeformingData));
            }
        }
        public string BeamModificationDate
        {
            get { return _beamModificationDate; }
            set
            {
                _beamModificationDate = value;
                OnPropertyChanged(nameof(BeamModificationDate));
            }
        }
        public string BeamEndPoint
        {
            get { return _beamEndPoint; }
            set
            {
                _beamEndPoint = value;
                OnPropertyChanged(nameof(BeamEndPoint));
            }
        }
        public string BeamStartPoint
        {
            get { return _beamStartPoint; }
            set
            {
                _beamStartPoint = value;
                OnPropertyChanged(nameof(BeamStartPoint));
            }
        }
        private void Events_SelectionChangeEvent()
        {
            lock (_selectionEventHandlerLock)
            {
                var selectedObjects = new Tekla.Structures.Model.UI.ModelObjectSelector().GetSelectedObjects();
                if (selectedObjects.GetSize() == 0 || selectedObjects.GetSize() > 1)
                    IsViewVisibleBeamProperties = false;
                else
                {
                    while (selectedObjects.MoveNext())
                    {
                        var selectedObject = selectedObjects.Current;
                        if(selectedObject is Assembly || selectedObject is Component)
                            IsViewVisibleBeamProperties = false;
                        else if (selectedObject is Beam)
                        {
                            Beam beam = selectedObject as Beam;
                            BeamProfile = beam.Profile.ProfileString;
                            BeamName = beam.Name;
                            BeamClass = beam.Class;

                            beam.GetPhase(out Phase beamPhaseTemp);
                            BeamPhase = beamPhaseTemp.PhaseNumber.ToString();
                            BeamMaterial = beam.Material.MaterialString;
                            BeamIdentifier = beam.Identifier.ToString();
                            BeamPartStartNumber = beam.PartNumber.Prefix + "/" + beam.PartNumber.StartNumber.ToString();
                            BeamAssemblyStartNumber = beam.AssemblyNumber.Prefix + "/" + beam.AssemblyNumber.StartNumber.ToString();
                            BeamFinish = beam.Finish.ToString();
                            BeamIsUpToDate = beam.IsUpToDate.ToString();
                            BeamModificationDate = beam.ModificationTime.ToString();
                            BeamStartPoint = Math.Round(beam.StartPoint.X, 2).ToString() + "; " + Math.Round(beam.StartPoint.Y, 2).ToString() + "; " + Math.Round(beam.StartPoint.Z, 2).ToString();
                            BeamEndPoint = Math.Round(beam.EndPoint.X, 2).ToString() + "; " + Math.Round(beam.EndPoint.Y, 2).ToString() + "; " + Math.Round(beam.EndPoint.Z, 2).ToString();

                            ArrayList sNames = new ArrayList();
                            sNames.Add("NAME");
                            sNames.Add("SCREW_NAME");
                            sNames.Add("SCREW_TYPE");
                            sNames.Add("TYPE");
                            sNames.Add("TYPE1");
                            sNames.Add("TYPE2");
                            sNames.Add("TYPE3");
                            sNames.Add("TYPE4");
                            sNames.Add("STANDARD");
                            sNames.Add("SHORT_NAME");
                            sNames.Add("MATERIAL");
                            sNames.Add("FINISH");
                            sNames.Add("GRADE");
                            ArrayList iNames = new ArrayList();
                            iNames.Add("DATE");
                            iNames.Add("FATHER_ID");
                            iNames.Add("GROUP_ID");
                            iNames.Add("HIERARCHY_LEVEL");
                            iNames.Add("MODEL_TOTAL");
                            ArrayList dNames = new ArrayList();
                            dNames.Add("EXTRA_LENGTH");
                            dNames.Add("FLANGE_THICKNESS");
                            dNames.Add("FLANGE_WIDTH");
                            dNames.Add("HEIGHT");
                            dNames.Add("LENGTH");
                            dNames.Add("PRIMARYWEIGHT");
                            dNames.Add("PROFILE_WEIGHT");
                            dNames.Add("ROUNDING_RADIUS");
                            dNames.Add("LENGTH");
                            dNames.Add("DIAMETER");
                            dNames.Add("WEIGHT");
                            dNames.Add("HEAD_DIAMETER");
                            dNames.Add("THICKNESS");
                            dNames.Add("WASHER.THICKNESS");
                            dNames.Add("WASHER.INNER_DIAMETER");
                            dNames.Add("WASHER.OUTER_DIAMETER");
                            dNames.Add("WASHER.THICKNESS1");
                            dNames.Add("WASHER.INNER_DIAMETER1");
                            dNames.Add("WASHER.OUTER_DIAMETER1");
                            dNames.Add("WASHER.THICKNESS2");
                            dNames.Add("WASHER.INNER_DIAMETER2");
                            dNames.Add("WASHER.OUTER_DIAMETER2");
                            dNames.Add("NUT.THICKNESS");
                            dNames.Add("NUT.INNER_DIAMETER");
                            dNames.Add("NUT.OUTER_DIAMETER");
                            dNames.Add("NUT.THICKNESS2");
                            dNames.Add("NUT.OUTER_DIAMETER2");

                            Hashtable sValues = new Hashtable(sNames.Count + dNames.Count + iNames.Count);
                            if (selectedObject.GetAllReportProperties(sNames, dNames, iNames, ref sValues))
                            {
                                string filePath = "output.txt";

                                using (StreamWriter writer = new StreamWriter(filePath))
                                {
                                    foreach (DictionaryEntry value in sValues)
                                    {
                                        writer.WriteLine(value.Key.ToString() + " : " + value.Value.ToString());
                                    }
                                }

                                // Otwieranie pliku
                                System.Diagnostics.Process.Start(filePath);
                            }

                            IsViewVisibleBeamProperties = true;
                        }
                        break;
                    }
                }
            }
        }
    }
}
