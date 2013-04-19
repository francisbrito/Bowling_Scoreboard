using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BowlingScoreboard.Core;

namespace BowlingScoreboard.Clients.Wpf.ViewModels
{
    public class FrameViewModel : BaseViewModel
    {
        private readonly Frame _model;

        public FrameViewModel()
        {
            _model = new Frame();
        }

        public FrameViewModel(Frame currentFrame)
        {
            _model = currentFrame;
        }

        public Throw FirstThrow
        {
            get { return _model.Throws[0]; }
        }

        public Throw SecondThrow
        {
            get { return _model.Throws[1]; }
        }

        public Throw ThirdThrow
        {
            get { return _model.Throws[2]; }
        }

        public int Score
        {
            get { return _model.Score; }
        }

        public FrameType Type
        {
            get { return _model.Type; }
        }


    }
}
