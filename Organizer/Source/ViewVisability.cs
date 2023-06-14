using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Filtering.Categories;
using Tekla.Structures.Filtering;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using TSM = Tekla.Structures.Model;
using TSMUI = Tekla.Structures.Model.UI;

namespace Organizer.Source
{
    public class ViewVisability
    {
        public static TSM.Model model = new TSM.Model();
        public static ModelInfo modelInfo = model.GetInfo();

        public static void ChangePhaseVisablitiyInAllViews(string[] phases)
        {
            if (!(model.GetConnectionStatus()))
                return;
            string filterName = "PMJ_AR_custom";
            ModelViewEnumerator ViewEnum = ViewHandler.GetAllViews();
            AssemblyFilterExpressions.Phase assemblyPhase = new AssemblyFilterExpressions.Phase();
            StringConstantFilterExpression phase = new StringConstantFilterExpression(phases);
            BinaryFilterExpression binaryFilterExpression = new BinaryFilterExpression(assemblyPhase, StringOperatorType.IS_EQUAL, phase);
            BinaryFilterExpressionCollection ExpressionCollection = new BinaryFilterExpressionCollection
            {
                new BinaryFilterExpressionItem(binaryFilterExpression, BinaryFilterOperatorType.BOOLEAN_OR),
            };
            Filter Filter = new Filter(ExpressionCollection);
            string fullFilterName = modelInfo.ModelPath + "/attributes/" + filterName;
            Filter.CreateFile(FilterExpressionFileType.OBJECT_GROUP_VIEW, fullFilterName);
            while (ViewEnum.MoveNext())
            {
                View view = ViewEnum.Current;
                view.ViewFilter = filterName;
                view.Modify();
            }
        }
        public static void ChangePhaseVisablitiyInSelectedViews(string[] phases)
        {
            if (!(model.GetConnectionStatus()))
                return;
            string filterName = "PMJ_AR_custom";
            ModelViewEnumerator ViewEnum = ViewHandler.GetSelectedViews();
            AssemblyFilterExpressions.Phase assemblyPhase = new AssemblyFilterExpressions.Phase();
            StringConstantFilterExpression phase = new StringConstantFilterExpression(phases);
            BinaryFilterExpression binaryFilterExpression = new BinaryFilterExpression(assemblyPhase, StringOperatorType.IS_EQUAL, phase);
            BinaryFilterExpressionCollection ExpressionCollection = new BinaryFilterExpressionCollection
            {
                new BinaryFilterExpressionItem(binaryFilterExpression, BinaryFilterOperatorType.BOOLEAN_OR),
            };
            Filter Filter = new Filter(ExpressionCollection);
            string fullFilterName = modelInfo.ModelPath + "/attributes/" + filterName;
            Filter.CreateFile(FilterExpressionFileType.OBJECT_GROUP_VIEW, fullFilterName);
            while (ViewEnum.MoveNext())
            {
                View view = ViewEnum.Current;
                view.ViewFilter = filterName;
                view.Modify();
            }
        }
    }
}
