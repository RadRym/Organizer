using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tekla.Structures.Model;
using Tekla.Structures.Model.Operations;

namespace OrganizerV2.Models
{
    public class TeklaModel
    {
        private Events _events = new Events();
        private Model model = new Model();
        private object _selectionEventHandlerLock = new object();

        public event Action SelectionChange;

        public TeklaModel()
        {
            _events.SelectionChange += Events_SelectionChangeEvent;
            _events.Register();

            _events.UnRegister();
        }

        private void Events_SelectionChangeEvent()
        {
            lock (_selectionEventHandlerLock)
            {
                var selectedObjects = new Tekla.Structures.Model.UI.ModelObjectSelector().GetSelectedObjects();

                if (selectedObjects.GetSize() > 0)
                {
                    while (selectedObjects.MoveNext())
                    {
                        ModelObject selectedObject = selectedObjects.Current as ModelObject;
                        if (selectedObject != null)
                        {
                            Operation.DisplayPrompt("Selection changed event received. Selected object type: " + selectedObject.GetType().Name);
                        }
                    }
                }
                else
                {
                    Operation.DisplayPrompt("Selection changed event received. No objects selected.");
                }
            }
        }
    }
}
