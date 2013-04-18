using System.Text.RegularExpressions;
using BowlingScoreboard.Clients.Wpf.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BowlingScoreboard.Core;
using Microsoft.Win32;

namespace BowlingScoreboard.Clients.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IFileReader _fileReader;
        private IFileFormatValidator _fileValidator;

        public MainWindow()
        {
            InitializeComponent();

            ApplicationCoreSetup();
        }

        private void ApplicationCoreSetup()
        {
            _fileReader = new LocalFileReader();
            _fileValidator = new DefaultFileFormatValidator();
        }

        private static void NotifyUser(string message, string title, MessageBoxButton button, MessageBoxImage icon)
        {
            MessageBox.Show(message, title, button, icon);
        }

        private static void NotifyError(string error)
        {
            NotifyUser(error, "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
        }

        private void LoadData_Click(object sender, RoutedEventArgs e)
        {
            var fileName = ShowOpenFileDialog();

            FilePathTextBox.Text = fileName;

            _fileReader.LoadFile(fileName);

            if (_fileReader.FileReadStatus == FileReadStatus.Loaded)
            {
                var text = _fileReader.Text;

                if (_fileValidator.IsValid(text))
                {
                    //TODO: Magic code goes here.
                }
                else
                {
                    NotifyError("Unknown file format.");
                }
            }
            else if (_fileReader.FileReadStatus == FileReadStatus.NotFound)
            {
                var msg = string.Format("The file {0} could not be found.", fileName);

                NotifyError(msg);
            }
            else if (_fileReader.FileReadStatus == FileReadStatus.NotLoaded)
            {
                var msg = string.Format("Unable to load file due to inner error.");

                NotifyError(msg);
            }
        }

        private string ShowOpenFileDialog()
        {
            var result = string.Empty;

            var dialog = new OpenFileDialog();

            var diagResult = dialog.ShowDialog();

            if (diagResult.Value)
            {
                result = dialog.FileName;
            }

            return result;
        }
    }
}
