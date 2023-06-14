using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;

namespace Organizer
{
    public class ViewModel : BaseModel
    {
        private bool _isCheckboxChecked;
        public bool IsCheckboxChecked
        {
            get { return _isCheckboxChecked; }
            set
            {
                if (_isCheckboxChecked != value)
                {
                    _isCheckboxChecked = value;
                    OnPropertyChanged(nameof(IsCheckboxChecked));
                    UpdateButtonContent();
                }
            }
        }
        private string _buttonContent;
        public string ButtonContent
        {
            get { return _buttonContent; }
            set
            {
                if (_buttonContent != value)
                {
                    _buttonContent = value;
                    OnPropertyChanged(nameof(ButtonContent));
                }
            }
        }
        private void HandleCheckboxChecked(object sender, RoutedEventArgs e)
        {
            IsCheckboxChecked = ((CheckBox)sender).IsChecked.GetValueOrDefault();
        }

        public void UpdateButtonContent()
        {
            ButtonContent = IsCheckboxChecked ? "X" : "Y";
        }

        public ICommand ButtonCommand { get; private set; }

        public ViewModel()
        {
            ButtonCommand = new RelayCommand(ButtonClick);
        }
        private void ButtonClick()
        {
            if (IsCheckboxChecked)
            {
                DisplayPrompt("Y");
            }
            else
            {
                ButtonContent = "X";
            }
        }
        public static void DisplayPrompt( string s)
        {
            System.Windows.MessageBox.Show("To jest komunikat " + s);
        }
    }
}
