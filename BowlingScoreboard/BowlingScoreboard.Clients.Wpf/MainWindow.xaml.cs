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
using BowlingScoreboard.Clients.Wpf.ViewModels;
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
        private FramesNavigator _firstPlayerFramesNavigator;
        private FramesNavigator _secondPlayerFramesNavigator;
        private FramesNavigatorViewModel _firstPlayerNavigatorViewModel;
        private FramesNavigatorViewModel _secondPlayerNavigatorViewModel;

        public MainWindow()
        {
            InitializeComponent();

            ApplicationCoreSetup();
        }

        private void ApplicationCoreSetup()
        {
            _fileReader = new LocalFileReader();
            _fileValidator = new DefaultFileFormatValidator();
            _firstPlayerFramesNavigator = new FramesNavigator();
            _secondPlayerFramesNavigator = new FramesNavigator();
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

                    var entries = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

                    var throws = entries.Select(t => new Throw(t));

                    var t1 = new List<Throw>();
                    var t2 = new List<Throw>();

                    var playerOneThrowIndexes = new int[]
                    {
                        0, 1, 4, 5, 8, 9, 12, 13, 16, 17, 20, 21, 24, 25, 28, 29, 32, 33, 36, 37, 38
                    };

                    for (int i = 0; i < 42; i++)
                    {
                        var @throw = throws.ElementAt(i);

                        if (playerOneThrowIndexes.Contains(i))
                        {
                            t1.Add(@throw);
                        }
                        else
                        {
                            t2.Add(@throw);
                        }
                    }

                    // Convert first 21 entries into throws
                    //var firstPlayerThrows =
                    //    entries
                    //    .Select(t => new Throw(t))
                    //    .Take(21)
                    //    .ToArray();

                    //var idx = 0;

                    //var firstPlayerThrows = throws.Where(t =>
                    //    {
                    //        if (playerOneThrowIndexes.Contains(idx))
                    //        {
                    //            idx++;
                    //            return true;
                    //        }

                    //        idx++;
                    //        return false;
                    //    })
                    //    .ToArray();

                    // Convert last 21 entries into trhows
                    //var secondPlayerThrows =
                    //    entries
                    //    .Select(t => new Throw(t))
                    //    .Skip(21)
                    //    .Take(21)
                    //    .ToArray();

                    //var secondPlayerThrows = throws.Where(t =>
                    //{
                    //    if (playerOneThrowIndexes.Contains(idx))
                    //    {
                    //        idx++;
                    //        return false;
                    //    }

                    //    idx++;
                    //    return true;
                    //})
                    //    .ToArray();

                    // var secondPlayerThrows = entries.Select(t => new Throw(t)).ToArray();

                    var firstPlayerThrows = t1.ToArray();
                    var secondPlayerThrows = t2.ToArray();

                    _firstPlayerFramesNavigator.LoadData(firstPlayerThrows);

                    _firstPlayerNavigatorViewModel = new FramesNavigatorViewModel("First Player",
                                                                                  _firstPlayerFramesNavigator);

                    _secondPlayerFramesNavigator.LoadData(secondPlayerThrows);

                    _secondPlayerNavigatorViewModel = new FramesNavigatorViewModel("Second Player",
                                                                                   _secondPlayerFramesNavigator);

                    FirstPlayerNavigator.DataContext = _firstPlayerNavigatorViewModel;
                    SecondPlayerNavigator.DataContext = _secondPlayerNavigatorViewModel;
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
