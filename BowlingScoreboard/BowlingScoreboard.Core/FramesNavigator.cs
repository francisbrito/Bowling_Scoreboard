using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BowlingScoreboard.Core
{
    public class FramesNavigator
    {
        private readonly Frame[] _frames;
        private int _pivot;

        private bool _alreadyLoaded = false;

        public FramesNavigator()
        {
            _frames = new Frame[10];
            _pivot = 0;
        }

        public void LoadData(Throw[] throws)
        {
            if (!_alreadyLoaded)
            {
                _alreadyLoaded = true;
            }

            // Load up framews with throws information
            for (int i = 0; i < 10; i++)
            {
                var currentFrame = _frames[i];

                var throwsPerFrame = 2;

                // If we're on the last frame.
                if (i == 9)
                {
                    throwsPerFrame = 3;
                }

                // Le magic of Linq.
                var throwsToAssign = throws.Take(throwsPerFrame).Skip(i * throwsPerFrame).ToArray();

                currentFrame.Throws = throwsToAssign;

                currentFrame.Score = CalculatePossibleScore(throwsToAssign, currentFrame, i + 1);
            }
        }

        private int CalculatePossibleScore(Throw[] throws, Frame currentFrame, int frameNumber)
        {
            var score = throws.Sum(t => t.PinsDown); // Sum up the pins down of each throw...

            // if we're not on the last frame, then do some logic...
            if (frameNumber != 10)
            {
                // If less than ten pins were taken down.
                if (score < 10)
                {
                    currentFrame.Type = FrameType.Normal;
                }
                // If ten pins were taken down, but it took two shots.
                else if (throws[0].PinsDown != 10)
                {
                    //TODO: Add to score using spare logic.
                    currentFrame.Type = FrameType.Spare;
                }
                // If not any of the above then it was a strike.
                else
                {
                    //TODO: Add to score using strike logic.
                    currentFrame.Type = FrameType.Strike;
                }
            }

            return score;
        }

        public bool AlreadyLoaded
        {
            get { return _alreadyLoaded; }
        }

        public Frame CurrentFrame
        {
            get { return _frames[_pivot]; }
        }

        public void PreviousFrame()
        {
            // If already at first frame, can't go back.
            if (_pivot == 0)
            {
                throw new InvalidOperationException();
            }

            _pivot--;
        }

        public void NextFrame()
        {
            // If already at last frame, can't go further.
            if (_pivot == 9)
            {
                throw new InvalidOperationException();
            }

            _pivot++;
        }

        public void FrameAt(int index)
        {
            if (index < 0 || index > 9)
            {
                throw new IndexOutOfRangeException();
            }

            _pivot = index;
        }
    }
}
