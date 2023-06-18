using MySql.Data.MySqlClient;
using OrganizerV2.Models;
using System;
using System.Collections.Generic;
using Tekla.Structures.Model;

namespace OrganizerV2.ViewModels
{
    public class SelectedObjectPropertiesViewModel : MainViewModel
    {
        private static readonly Events _events = new Events();
        private static readonly object _selectionEventHandlerLock = new object();

        private bool _isViewVisibleBeamProperties;
        private Dictionary<string, string> _beamProperties;
        private MyProfil _profileGeometry;

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
        public MyProfil ProfileGeometry
        {
            get { return _profileGeometry; }
            set
            {
                _profileGeometry = value;
                OnPropertyChanged(nameof(ProfileGeometry));
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

            string profileName = MoveThirdLetterToEnd(properties["BeamProfile"]);
            ProfileGeometry = GetProfileGeometryByName(profileName);
        }
        public string MoveThirdLetterToEnd(string input)
        {
            if (input != null && input.StartsWith("HE") && input.Length >= 3)
            {
                string thirdLetter = input.Substring(2, 1);
                string remainingLetters = input.Substring(0, 2) + input.Substring(3);
                return remainingLetters + thirdLetter;
            }
            else
            {
                return input;
            }
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
        public MyProfil GetProfileGeometryByName(string profileName)
        {
            MyProfil profile = null;
            string connectionString = "Server=localhost;Port=3306;Database=steeldb;Uid=root;Pwd=Zbyszek.45;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM steeldb.iprofiles WHERE Name = @Name";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", profileName);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        profile = new MyProfil()
                        {
                            Ibeam_G = Math.Round(reader.GetFloat("g"), 2),
                            Ibeam_h = Math.Round(reader.GetFloat("h"), 2),
                            Ibeam_b = Math.Round(reader.GetFloat("b"), 2),
                            Ibeam_tf = Math.Round(reader.GetFloat("tf"), 2),
                            Ibeam_tw = Math.Round(reader.GetFloat("tw"), 2),
                            Ibeam_r = Math.Round(reader.GetFloat("r"), 2),
                            Ibeam_A = Math.Round(reader.GetFloat("A"), 2),
                            Ibeam_hi = Math.Round(reader.GetFloat("hi"), 2),
                            Ibeam_d = Math.Round(reader.GetFloat("d"), 2),
                            Ibeam_phi = reader.GetString("phi"),
                            Ibeam_pmin = Math.Round(reader.GetFloat("pmin"), 2),
                            Ibeam_pmax = Math.Round(reader.GetFloat("pmax"), 2),
                            Ibeam_AL = Math.Round(reader.GetFloat("AL"), 2),
                            Ibeam_AG = Math.Round(reader.GetFloat("AG"), 2),
                            Ibeam_Iy = Math.Round(reader.GetFloat("Iy"), 2),
                            Ibeam_Wely = Math.Round(reader.GetFloat("Wely"), 2),
                            Ibeam_Wply = Math.Round(reader.GetFloat("Wply"), 2),
                            Ibeam_iy = Math.Round(reader.GetFloat("iy"), 2),
                            Ibeam_Avz = Math.Round(reader.GetFloat("Avz"), 2),
                            Ibeam_Iz = Math.Round(reader.GetFloat("Iz"), 2),
                            Ibeam_Welz = Math.Round(reader.GetFloat("Welz"), 2),
                            Ibeam_Wplz = Math.Round(reader.GetFloat("Wplz"), 2),
                            Ibeam_iz = Math.Round(reader.GetFloat("iz"), 2),
                            Ibeam_It = Math.Round(reader.GetFloat("It"), 2),
                            Ibeam_Iw = Math.Round(reader.GetFloat("Iw"), 2),
                        };
                    }
                }
            }
            return profile;
        }
    }
}
