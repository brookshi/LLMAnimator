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
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace LLM.Attention
{
    public class BounceAnimation : AnimationBase
    {
        public BounceAnimation()
        {
            Duration = TimeSpan.FromMilliseconds(800);
        }

        public override void PlayOn(UIElement target, Action ContinueWith)
        {
            var transform = AnimUtils.PrepareTransform(target, typeof(TranslateTransform));
            Storyboard storyboard = new Storyboard();
            DoubleAnimationUsingKeyFrames frames = new DoubleAnimationUsingKeyFrames();

            frames.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                EasingFunction = new SineEase()
                {
                    EasingMode = EasingMode.EaseIn
                },
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(100)),
                Value = -8,
            });
            frames.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                EasingFunction = new BounceEase()
                {
                    Bounces = 2,
                    Bounciness = 1.3,
                    EasingMode = EasingMode.EaseOut
                },
                KeyTime = KeyTime.FromTimeSpan(Duration),
                Value = 0,
            });

            storyboard.Children.Add(frames);
            Storyboard.SetTarget(frames, transform);
            Storyboard.SetTargetProperty(frames, "Y");
            storyboard.Completed += (s, e) =>
              {
                  if (ContinueWith != null)
                      ContinueWith();
              };
            storyboard.BeginTime = Delay;
            storyboard.RepeatBehavior = RepeatBehavior;
            storyboard.Begin();
        }
    }
}
