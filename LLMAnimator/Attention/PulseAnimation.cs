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
            Duration = TimeSpan.FromMilliseconds(500);
        }

        public override void PlayOn(UIElement target, Action ContinueWith)
        {
            var transform = AnimUtils.PrepareTransform(target, typeof(ScaleTransform));
            ((ScaleTransform)transform).CenterX = target.RenderSize.Width / 2;
            ((ScaleTransform)transform).CenterY = target.RenderSize.Height / 2;

            Storyboard storyboard = new Storyboard();

            AddAnimationToStoryboard(storyboard, transform, CreateAnimation(), "ScaleX", null);

            AddAnimationToStoryboard(storyboard, transform, CreateAnimation(), "ScaleY", ContinueWith);

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
