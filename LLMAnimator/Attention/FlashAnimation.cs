#region License
//   Copyright 2015 Brook Shi
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License. 
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;

namespace LLM.Animation
{
    public class FlashAnimation : AnimationBase
    {
        public FlashAnimation()
        {
            RepeatBehavior = new RepeatBehavior(2);
            Duration = TimeSpan.FromMilliseconds(400);
        }

        public override IAnimation PlayOn(UIElement target, Action continueWith)
        {
            var storyboard = PrepareStoryboard(continueWith);

            AddAnimationToStoryboard(storyboard, target, CreateAnimation(), "Opacity");
            
            storyboard.Begin();

            return this;
        }

        Timeline CreateAnimation()
        {
            DoubleAnimationUsingKeyFrames frames = new DoubleAnimationUsingKeyFrames();

            frames.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                EasingFunction = new SineEase()
                {
                    EasingMode = EasingMode.EaseIn
                },
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(Duration.TotalMilliseconds / 2)),
                Value = 0,
            });
            frames.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                EasingFunction = new SineEase()
                {
                    EasingMode = EasingMode.EaseIn
                },
                KeyTime = KeyTime.FromTimeSpan(Duration),
                Value = 1,
            });

            return frames;
        }
    }
}
