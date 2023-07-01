using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using TeklaModels.ClipPlanes;
using ModelObjectSelector = Tekla.Structures.Model.UI.ModelObjectSelector;
using Tekla.Structures;
using System.Collections.Generic;
using System;
using System.IO;
using System.Reflection;
using System.Windows;

namespace OrganizerV2.TeklaModels.Views
{
    public class Visability
    {
        public static Model model = new Model();
        public static string modelLocation = model.GetInfo().ModelPath;
        public static void RedrawViews()
        {
            ModelViewEnumerator ViewEnum = ClipPlanes.GetViewEnum();
            while (ViewEnum.MoveNext())
            {
                View ViewSel = ViewEnum.Current;
                ViewHandler.RedrawView(ViewSel);
            }
        }
        public static void ColorRepresentation(string RepresentationFileName)
        {
            if (!model.GetConnectionStatus())
                return;
            List<string> TeklaRepresentationFiles = GetTeklaRepresentationFiles();
            if (!TeklaRepresentationFiles.Contains(RepresentationFileName))
                CreateRepresentationFile(RepresentationFileName);
            ViewHandler.SetRepresentation(RepresentationFileName);
        }

        public static void CreateRepresentationFile(string fileName)
        {
            string contentOfRepresentationFile = InvokeColorByMethodFromClassicRep(fileName);
            if (contentOfRepresentationFile != string.Empty)
                CreateClassicRepresentationFile(fileName, contentOfRepresentationFile);
            else
                CreateCustomRepresentationFile(fileName);
        }
        public static void CreateClassicRepresentationFile(string fileName, string contentOfRepresentationFile)
        {
            string filePath = Path.Combine(modelLocation, "attributes", fileName + ".rep");
            File.WriteAllText(filePath, contentOfRepresentationFile);
        }
        public static void CreateCustomRepresentationFile(string fileName)
        {
            string contentOfRepresentationFile = string.Empty;
            if (fileName == "ColorByMaterial")
                contentOfRepresentationFile = CustomColorBy.ColorByMaterial(modelLocation);
            if (fileName == "ColorByProfile")
                contentOfRepresentationFile = CustomColorBy.ColorByProfile(modelLocation);

            string filePath = Path.Combine(modelLocation, "attributes", fileName + ".rep");
            File.WriteAllText(filePath, contentOfRepresentationFile);
        }
        public static string InvokeColorByMethodFromClassicRep(string methodName)
        {
            Type colorByType = typeof(DefaultColorBy);
            MethodInfo method = colorByType.GetMethod(methodName);
            if (method != null)
            {
                object result = method.Invoke(null, null);
                if (result != null)
                    return result.ToString();
            }
            return string.Empty;
        }
        public static List<string> GetTeklaRepresentationFiles()
        {
            ModelInfo modelInfo = model.GetInfo();
            TeklaStructuresFiles teklaStructuresFiles = new TeklaStructuresFiles(modelpath: modelInfo.ModelPath);
            return teklaStructuresFiles.GetMultiDirectoryFileList("rep", false);
        }
        public static void RestoreWorkPlane()
        {

        }
    }
}
