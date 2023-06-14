using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using TeklaModels.ClipPlanes;
using ModelObjectSelector = Tekla.Structures.Model.UI.ModelObjectSelector;

namespace OrganizerV2.TeklaModels.Views
{
    public class Visability
    {
        public static Model model = new Model();
        public static void RedrawViews()
        {
            ModelViewEnumerator ViewEnum = ClipPlanes.GetViewEnum();
            while (ViewEnum.MoveNext())
            {
                View ViewSel = ViewEnum.Current;
                ViewHandler.RedrawView(ViewSel);
            }
        }
        public static void HideSelected()
        {
            if (model.GetConnectionStatus())
            {
                ModelObjectSelector modelSelector = new ModelObjectSelector();
                ModelObjectEnumerator selectedObjects = (modelSelector.GetSelectedObjects() as ModelObjectEnumerator);

                while (selectedObjects.MoveNext())
                {
                    ModelObject selectedObject = selectedObjects.Current;
                    selectedObject.Select();
                }

                model.CommitChanges();
            }
        }
    }
}
