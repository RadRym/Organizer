using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;
using Tekla.Structures;

namespace ConsoleTest
{
    internal class Program
    {
        private static Tekla.Structures.Model.Events _events = new Tekla.Structures.Model.Events();
        private static object _selectionEventHandlerLock = new object();
        static void Main(string[] args)
        {
            _events.SelectionChange += Events_SelectionChangeEvent;
            _events.Register();

            Console.WriteLine("Program started. Press any key to exit.");
            Console.ReadKey();

            _events.UnRegister();
        }
        static void Events_SelectionChangeEvent()
        {
            lock (_selectionEventHandlerLock)
            {
                Model model = new Model();
                var selectedObjects = new Tekla.Structures.Model.UI.ModelObjectSelector().GetSelectedObjects();

                if (selectedObjects.GetSize() > 0)
                {
                    while (selectedObjects.MoveNext())
                    {
                        ModelObject selectedObject = selectedObjects.Current as ModelObject;
                        if (selectedObject != null)
                        {
                            Console.WriteLine("Selection changed event received. Selected object type: " + selectedObject.GetType().Name);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Selection changed event received. No objects selected.");
                }
            }
        }
    }
}
