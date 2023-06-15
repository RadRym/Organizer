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
        private static readonly object _selectionEventHandlerLock = new object();
        private static readonly Events _events = new Events();
        static void Main()
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
                        if (!(selectedObjects.Current is ModelObject selectedObject))
                            return;
                        if (selectedObject is Beam)
                        {
                            Beam selectedBeam = selectedObject as Beam;
                            Console.WriteLine("Selection changed event received. Selected object: ");
                            Console.WriteLine("Assembly Number Prefix: " + selectedBeam.AssemblyNumber.Prefix);
                            Console.WriteLine("Assembly Number Start Number: " + selectedBeam.AssemblyNumber.StartNumber);
                            Console.WriteLine("Assembly Number: " + selectedBeam.AssemblyNumber.ToString());
                            Console.WriteLine("Name: " + selectedBeam.Name);
                            Console.WriteLine("Class: " + selectedBeam.Class);
                            Console.WriteLine("Finish: " + selectedBeam.Finish);
                        }
                        else
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
