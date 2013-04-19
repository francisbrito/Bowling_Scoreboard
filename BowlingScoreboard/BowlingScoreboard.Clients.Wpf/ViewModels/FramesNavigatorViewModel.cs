using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using BowlingScoreboard.Clients.Wpf.Annotations;
using BowlingScoreboard.Core;

namespace BowlingScoreboard.Clients.Wpf.ViewModels
{
    public class FramesNavigatorViewModel : BaseViewModel
    {
        private string _title;
        private FramesNavigator _navigator;
        private FrameViewModel _currentFrame;

        public FramesNavigatorViewModel(string title, FramesNavigator navigator)
        {
            _title = title;
            _navigator = navigator;

            _currentFrame = null;
        }

        public FramesNavigatorViewModel()
            : this(string.Empty, new FramesNavigator())
        {
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        public FrameViewModel CurrentFrame
        {
            get
            {
                return new FrameViewModel(_navigator.CurrentFrame);
            }
            set
            {
                _currentFrame = value;
                OnPropertyChanged("CurrentFrame");
            }
        }

        public bool AlreadyLoaded
        {
            get { return _navigator.AlreadyLoaded; }
        }

        public void NextFrame()
        {
            try
            {
                // Attempt to navigate to next frame.
                _navigator.NextFrame();

                CurrentFrame = new FrameViewModel(_navigator.CurrentFrame);
            }
            catch (InvalidOperationException)
            {
            }
        }

        public void PreviousFrame()
        {
            try
            {
                // Attempt to navigate to past frame.
                _navigator.PreviousFrame();

                CurrentFrame = new FrameViewModel(_navigator.CurrentFrame);
            }
            catch (InvalidOperationException)
            {
            }
        }
    }

    

    
}
