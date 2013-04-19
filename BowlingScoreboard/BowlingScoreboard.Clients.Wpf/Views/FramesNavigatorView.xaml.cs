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

namespace BowlingScoreboard.Clients.Wpf.Views
{
    /// <summary>
    /// Interaction logic for FrameNavigatorView.xaml
    /// </summary>
    public partial class FramesNavigatorView : UserControl
    {
        public FramesNavigatorView()
        {
            InitializeComponent();
        }

        private void PreviousFrameButton_Click(object sender, RoutedEventArgs e)
        {
            var dc = this.DataContext as FramesNavigatorViewModel;

            dc.PreviousFrame();
        }

        private void NextFrameButton_Click(object sender, RoutedEventArgs e)
        {
            var dc = this.DataContext as FramesNavigatorViewModel;

            dc.NextFrame();
        }
    }
}
