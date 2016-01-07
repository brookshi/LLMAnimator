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

namespace LLM.Animation
{
    public class BounceInDownAnimation : AnimationBase
    {
        public BounceInDownAnimation()
        {
            Duration = TimeSpan.FromMilliseconds(800);
        }

        public override IAnimation PlayOn(UIElement target, Action continueWith)
        {
            var transform = (CompositeTransform)Utils.PrepareTransform(target, typeof(CompositeTransform));
            transform.TranslateY = -target.RenderSize.Height;
            target.Opacity = 0;
            var storyboard = PrepareStoryboard(continueWith);

            var opacityAnim = Utils.CreateAnimationWithValues(Duration.TotalMilliseconds/2, 1);
            AddAnimationToStoryboard(storyboard, target, opacityAnim, "Opacity");
            AddAnimationToStoryboard(storyboard, transform, CreateAnimation(), "TranslateY");

            storyboard.Begin();

            return this;
        }

        Timeline CreateAnimation()
        {
            return new DoubleAnimation()
            {
                Duration = new Duration(Duration),
                To = 0,
                EasingFunction = new BounceEase()
                {
                    Bounces = 2,
                    Bounciness = 3,
                    EasingMode = EasingMode.EaseOut
                }
            };
        }
    }
}
