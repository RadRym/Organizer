using Organizer.Source;
using System;
using System.Windows;
using Tekla.Structures.Model;
using Tekla.Structures;
using System.Windows.Documents;
using System.Collections.Generic;

namespace Organizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Model model = new Model();
        public MainWindow()
        {
            InitializeComponent();
            if (model.GetConnectionStatus())
            {
                ModelInfo modelInfo = model.GetInfo();
                TeklaStructuresFiles files = new TeklaStructuresFiles(modelpath: modelInfo.ModelPath);
                ColorAndTransparency_Cmbx.ItemsSource = files.GetMultiDirectoryFileList("rep", false);
            }
        }

        public string[] Phases()
        {
            string phasesText = PhasesToDisplay_TextBox.Text;
            string[] phases = phasesText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return phases;
        }
        public void HandleCheckboxChecked(object sender, RoutedEventArgs e)
        {
            var viewModel = new ViewModel();
            viewModel.UpdateButtonContent();
        }

        private void CreateClipPlanesOnAllViews_Click(object sender, RoutedEventArgs e)
        {
            ViewManipulations.CreateClipPlanesOnAllViews();
        }
        private void CreateClipPlanesOnSelectedViews_Click(object sender, RoutedEventArgs e)
        {
            ViewManipulations.CreateClipPlanesOnSelectedViews();
        }
        private void DeleteClipPlanesOnAllViews_Click(object sender, RoutedEventArgs e)
        {
            ViewManipulations.DeleteClipPlanesOnAllViews();
        }
        private void DeleteClipPlanesOnSelectedViews_Click(object sender, RoutedEventArgs e)
        {
            ViewManipulations.DeleteClipPlanesOnSelectedViews();
        }

        private void RedrawAllViews_Click(object sender, RoutedEventArgs e)
        {
            ViewManipulations.RedrawAllViews();
        }
        private void RedrawCurrentViews_Click(object sender, RoutedEventArgs e)
        {
            ViewManipulations.RedrawCurrentViews();
        }
        private void RestoreWorkPlanes_Click(object sender, RoutedEventArgs e)
        {
            ViewManipulations.RestoreWorkPlanes();
        }
        private void DisplayByPhasesInAllViews_Click(object sender, RoutedEventArgs e)
        {
            ViewVisability.ChangePhaseVisablitiyInAllViews(Phases());
        }
        private void DisplayByPhasesInSelectedViews_Click(object sender, RoutedEventArgs e)
        {
            ViewVisability.ChangePhaseVisablitiyInSelectedViews(Phases());
        }

        private void ColorAndTransparency_Cmbx_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void ChangeColorsInAllViews_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ChangeColorsInSelectedViews_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
