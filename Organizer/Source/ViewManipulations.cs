using System.Collections.Generic;
using System.Linq;
using TSM = Tekla.Structures.Model;
using TSMUI = Tekla.Structures.Model.UI;
using Tekla.Structures.Model.UI;
using Tekla.Structures.Model;
using Tekla.Structures.Geometry3d;
using System.Collections;

namespace Organizer.Source
{
    public class ViewManipulations
    {
        internal static int clipPlanesOffset = 400;
        public static Model model = new TSM.Model();

        public static void DeleteClipPlanesOnAllViews()
        {
            if (!(model.GetConnectionStatus()))
                return;
            ModelViewEnumerator ViewEnum = ViewHandler.GetVisibleViews();
            while (ViewEnum.MoveNext())
            {
                View ActiveView = ViewEnum.Current;
                ClipPlaneCollection ClipPlanes = ActiveView.GetClipPlanes();
                if (ClipPlanes.Count == 0)
                    return;
                IEnumerator PlaneEnum = ClipPlanes.GetEnumerator();
                while (PlaneEnum.MoveNext())
                {
                    (PlaneEnum.Current as ClipPlane).Delete();
                }
            }
        }

        public static void DeleteClipPlanesOnSelectedViews()
        {
            if (!(model.GetConnectionStatus()))
                return;
            ModelViewEnumerator ViewEnum = ViewHandler.GetSelectedViews();
            while (ViewEnum.MoveNext())
            {
                View ActiveView = ViewEnum.Current;
                ClipPlaneCollection ClipPlanes = ActiveView.GetClipPlanes();
                if (ClipPlanes.Count == 0)
                    return;
                IEnumerator PlaneEnum = ClipPlanes.GetEnumerator();
                while (PlaneEnum.MoveNext())
                {
                    (PlaneEnum.Current as ClipPlane).Delete();
                }
            }
        }

        public static void CreateClipPlanesOnAllViews()
        {
            if (!(model.GetConnectionStatus()))
                return;
            DeleteClipPlanesOnAllViews();
            List<Solid> solids = GetSolidsFromSelected();
            GetMinMaxValues(
                solids, 
                out double maxX, 
                out double minX, 
                out double maxY, 
                out double minY, 
                out double maxZ, 
                out double minZ);

            ModelViewEnumerator ViewEnum = ViewHandler.GetVisibleViews();
            while (ViewEnum.MoveNext())
            {
                View ActiveView = ViewEnum.Current;
                ClipPlane CPlane = new ClipPlane
                {
                    View = ActiveView,
                    UpVector = new Vector(1, 0, 0),
                    Location = new Point(maxX + clipPlanesOffset, (maxY + minY) / 2, (maxZ + minZ) / 2)
                };
                CPlane.Insert();
                CPlane.UpVector = new Vector(-1, 0, 0);
                CPlane.Location = new Point(minX - clipPlanesOffset, (maxY + minY) / 2, (maxZ + minZ) / 2);
                CPlane.Insert();
                CPlane.UpVector = new Vector(0, 1, 0);
                CPlane.Location = new Point((maxX + minX) / 2, maxY + clipPlanesOffset, (maxZ + minZ) / 2);
                CPlane.Insert();
                CPlane.UpVector = new Vector(0, -1, 0);
                CPlane.Location = new Point((maxX + minX) / 2, minY - clipPlanesOffset, (maxZ + minZ) / 2);
                CPlane.Insert();
                CPlane.UpVector = new Vector(0, 0, 1);
                CPlane.Location = new Point((maxX + minX) / 2, (maxY + minY) / 2, maxZ + clipPlanesOffset);
                CPlane.Insert();
                CPlane.UpVector = new Vector(0, 0, -1);
                CPlane.Location = new Point((maxX + minX) / 2, (maxY + minY) / 2, minZ - clipPlanesOffset);
                CPlane.Insert();
            }
        }

        public static void CreateClipPlanesOnSelectedViews()
        {
            if (!(model.GetConnectionStatus()))
                return;
            DeleteClipPlanesOnSelectedViews();
            List<Solid> solids = GetSolidsFromSelected();
            GetMinMaxValues(
                solids,
                out double maxX,
                out double minX,
                out double maxY,
                out double minY,
                out double maxZ,
                out double minZ);

            ModelViewEnumerator ViewEnum = ViewHandler.GetSelectedViews();
            while (ViewEnum.MoveNext())
            {
                View ActiveView = ViewEnum.Current;
                ClipPlane CPlane = new ClipPlane
                {
                    View = ActiveView,
                    UpVector = new Vector(1, 0, 0),
                    Location = new Point(maxX + clipPlanesOffset, (maxY + minY) / 2, (maxZ + minZ) / 2)
                };
                CPlane.Insert();
                CPlane.UpVector = new Vector(-1, 0, 0);
                CPlane.Location = new Point(minX - clipPlanesOffset, (maxY + minY) / 2, (maxZ + minZ) / 2);
                CPlane.Insert();
                CPlane.UpVector = new Vector(0, 1, 0);
                CPlane.Location = new Point((maxX + minX) / 2, maxY + clipPlanesOffset, (maxZ + minZ) / 2);
                CPlane.Insert();
                CPlane.UpVector = new Vector(0, -1, 0);
                CPlane.Location = new Point((maxX + minX) / 2, minY - clipPlanesOffset, (maxZ + minZ) / 2);
                CPlane.Insert();
                CPlane.UpVector = new Vector(0, 0, 1);
                CPlane.Location = new Point((maxX + minX) / 2, (maxY + minY) / 2, maxZ + clipPlanesOffset);
                CPlane.Insert();
                CPlane.UpVector = new Vector(0, 0, -1);
                CPlane.Location = new Point((maxX + minX) / 2, (maxY + minY) / 2, minZ - clipPlanesOffset);
                CPlane.Insert();
            }
        }

        private static List<Solid> GetSolidsFromSelected()
        {
            TSMUI.ModelObjectSelector modelSelector = new TSMUI.ModelObjectSelector();
            ModelObjectEnumerator selectedObjects = (modelSelector.GetSelectedObjects() as ModelObjectEnumerator);
            List<Solid> solidPartsList = new List<Solid>();
            while (selectedObjects.MoveNext())
            {
                if (selectedObjects.Current is Part part)
                    solidPartsList.Add(GetSolidFromPart(part));
                else if (selectedObjects.Current is Assembly assembly)
                    solidPartsList.AddRange(GetSolidsFromAssembly(assembly));
                else continue;
            }
            return solidPartsList;
        }

        private static Solid GetSolidFromPart(Part part)
        {
            return part.GetSolid();
        }

        private static List<Solid> GetSolidsFromAssembly(Assembly assembly)
        {
            List<Solid> solidList = new List<Solid>();
            foreach (var secondaries in assembly.GetSecondaries())
            {
                if (secondaries is Part part)
                    solidList.Add(GetSolidFromPart(part));
            }
            solidList.Add(GetSolidFromPart(assembly.GetMainPart() as Part));
            return solidList;
        }

        private static void GetMinMaxValues(List<Solid> solidPartsList, out double maxX, out double minX, out double maxY, out double minY, out double maxZ, out double minZ)
        {
            if (solidPartsList.Count == 0)
            {
                maxX = 0;
                minX = 0;
                maxY = 0;
                minY = 0;
                maxZ = 0;
                minZ = 0;
                return;
            }

            List<double> maxXList = new List<double>();
            List<double> minXList = new List<double>();
            List<double> maxYList = new List<double>();
            List<double> minYList = new List<double>();
            List<double> maxZList = new List<double>();
            List<double> minZList = new List<double>();

            foreach (Solid solid in solidPartsList)
            {
                double maxPointX = solid.MaximumPoint.X;
                maxXList.Add(maxPointX);
                double maxPointY = solid.MaximumPoint.Y;
                maxYList.Add(maxPointY);
                double maxPointZ = solid.MaximumPoint.Z;
                maxZList.Add(maxPointZ);
                double minPointX = solid.MinimumPoint.X;
                minXList.Add(minPointX);
                double minPointY = solid.MinimumPoint.Y;
                minYList.Add(minPointY);
                double minPointZ = solid.MinimumPoint.Z;
                minZList.Add(minPointZ);
            }

            maxX = maxXList.Max<double>();
            minX = minXList.Min<double>();
            maxY = maxYList.Max<double>();
            minY = minYList.Min<double>();
            maxZ = maxZList.Max<double>();
            minZ = minZList.Min<double>();
        }
    
        public static void RedrawAllViews()
        {
            if (!(model.GetConnectionStatus()))
                return;
            ModelViewEnumerator ViewEnum = ViewHandler.GetAllViews();
            while (ViewEnum.MoveNext())
            {
                View ViewSel = ViewEnum.Current;
                ViewHandler.RedrawView(ViewSel);
            }
        }

        public static void RedrawCurrentViews()
        {
            ModelViewEnumerator currentViewEnum = ViewHandler.GetSelectedViews();
            while (currentViewEnum.MoveNext())
            {
                View ViewSel = currentViewEnum.Current;
                ViewHandler.RedrawView(ViewSel);
            }
        }

        public static void RestoreWorkPlanes()
        {
            if (model.GetConnectionStatus())
            {
                TransformationPlane GlobalPlane = new TransformationPlane();
                model.GetWorkPlaneHandler().SetCurrentTransformationPlane(GlobalPlane);
                ViewHandler.RedrawWorkplane();
            }
        }

    }
}
