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
    public class DropOutAnimation : AnimationBase
    {
        public DropOutAnimation()
        {
            Duration = TimeSpan.FromMilliseconds(700);
        }

        public override IAnimation PlayOn(UIElement target, Action continueWith)
        {
            var transform = (CompositeTransform)Utils.PrepareTransform(target, typeof(CompositeTransform));
            transform.TranslateY = -Utils.GetPointInParent(target).Y - target.RenderSize.Height;
            var storyboard = PrepareStoryboard(continueWith);
            AddAnimationToStoryboard(storyboard, transform, CreateAnimation(), "TranslateY");

            storyboard.Begin();

            return this;
        }

        Timeline CreateAnimation()
        {
            return new DoubleAnimation()
            {
                EasingFunction = new BounceEase()
                {
                    Bounces = 3,
                    Bounciness = 3,
                    EasingMode = EasingMode.EaseOut
                },
                Duration = new Duration(Duration),
                To = 0,
            };
        }
    }
}
