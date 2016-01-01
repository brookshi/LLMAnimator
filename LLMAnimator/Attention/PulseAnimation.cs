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
    public class PulseAnimation : AnimationBase
    {
        public PulseAnimation()
        {
            Duration = TimeSpan.FromMilliseconds(400);
        }

        public override void PlayOn(UIElement target, Action continueWith)
        {
            var transform = AnimUtils.PrepareTransform(target, typeof(ScaleTransform));
            AnimUtils.SetCenterForScaleTransform(target, (ScaleTransform)transform);

            var storyboard = CreateStoryboard(continueWith);

            AddAnimationToStoryboard(storyboard, transform, CreateAnimation(), "ScaleX");

            AddAnimationToStoryboard(storyboard, transform, CreateAnimation(), "ScaleY");

            storyboard.Begin();
        }

        Timeline CreateAnimation()
        {
            return new DoubleAnimation()
            {
                Duration = new Duration(Duration),
                To = 1.1,
                EasingFunction = new SineEase() { EasingMode = EasingMode.EaseIn },
                AutoReverse = true,
            };
        }
    }
}
