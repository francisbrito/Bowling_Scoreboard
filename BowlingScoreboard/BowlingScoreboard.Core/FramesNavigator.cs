using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BowlingScoreboard.Core
{
    public class FramesNavigator
    {
        private Frame[] _frames;
        private int _pivot;

        private bool _alreadyLoaded = false;

        public FramesNavigator()
        {
            InitializeFrames();

            _pivot = 0;
        }

        private void InitializeFrames()
        {
            _frames = new Frame[10];

            for (int i = 0; i < 10; i++)
            {
                _frames[i] = new Frame();
            }
        }

        public void LoadData(Throw[] throws)
        {
            if (!_alreadyLoaded)
            {
                _alreadyLoaded = true;
            }
            
            var throwsPerFrame = 2;

            const int throwsToSkip = 2;

            // Load up framews with throws information
            for (int i = 0; i < 10; i++)
            {
                var currentFrame = _frames[i];

                // If we're on the last frame.
                if (i == 9)
                {
                    throwsPerFrame = 3;
                }

                // Le magic of LINQ.
                var throwsToAssign = throws.Skip(i * throwsToSkip).Take(throwsPerFrame).ToArray();

                currentFrame.Throws = throwsToAssign;
            }

            // Holds the value of the last frame's score.
            var lastScore = 0;

            // Calculate scores for each frame.
            for (int i = 0; i < 10; i++)
            {
                var currentFrame = _frames[i];

                currentFrame.Score = CalculatePossibleScore(currentFrame, i) + lastScore;

                // Update last score value.
                lastScore = currentFrame.Score; 
            }
        }

        private int CalculatePossibleScore(Frame currentFrame, int frameIndex)
        {
            var throws = currentFrame.Throws;

            var score = throws.Sum(t => t.PinsDown); // Sum up the pins down of each throw...

            // if we're not on the last frame, then do some logic...
            if (frameIndex != 9)
            {
                // If less than ten pins were taken down.
                if (score < 10)
                {
                    currentFrame.Type = FrameType.Normal;
                }
                // If ten pins were taken down, but it took two shots.
                else if (throws[0].PinsDown != 10)
                {
                    currentFrame.Type = FrameType.Spare;

                    // If it was an Spare then you should add as many pins down were in the next frame's first throw.
                    score += _frames[frameIndex + 1].Throws[0].PinsDown;
                }
                // If not any of the above then it was a strike.
                else
                {
                    currentFrame.Type = FrameType.Strike;

                    // Apply spare logic.
                    score += _frames[frameIndex + 1].Throws[0].PinsDown;

                    // This works. It's magic. Don't ask why. 
                    if (_frames[frameIndex + 1].Throws[0].PinsDown == 10 && frameIndex < 8)
                    {
                        score += _frames[frameIndex + 2].Throws[0].PinsDown;
                    }
                    else
                    {
                        score += _frames[frameIndex + 1].Throws[1].PinsDown;
                    }
                }
            }
            // 10th Frame is a special case, so it should be handled with care. :)
            else
            {
                // If 10 pins were taken down in the first throw, its a strike.
                if (_frames[frameIndex].Throws[0].PinsDown == 10)
                {
                    currentFrame.Type = FrameType.Strike;
                }
                // If 10 pins were taken down in two shots, its a spare.
                else if (_frames[frameIndex].Throws.Sum(t => t.PinsDown) - _frames[frameIndex].Throws[2].PinsDown == 10)
                {
                    currentFrame.Type = FrameType.Spare;
                }
                // Otherwise, it was a normal shot.
                else
                {
                    currentFrame.Type = FrameType.Normal;
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
