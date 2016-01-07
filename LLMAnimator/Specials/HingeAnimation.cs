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
    public class HingeAnimation : AnimationBase
    {
        private Storyboard fallStoryboard = new Storyboard();

        public HingeAnimation()
        {
            Duration = TimeSpan.FromMilliseconds(1500);
        }

        public override IAnimation PlayOn(UIElement target, Action continueWith)
        {
            var transform = (CompositeTransform)Utils.PrepareTransform(target, typeof(CompositeTransform));

            fallStoryboard.Completed += (s, e) => { if (continueWith != null) continueWith(); };
            var rotateStoryboard = PrepareStoryboard(fallStoryboard.Begin);

            var opacityAnim = Utils.CreateAnimationWithValues(Duration.TotalMilliseconds / 3, 0);
            AddAnimationToStoryboard(rotateStoryboard, transform, CreateRotateAnimation(), "Rotation");
            AddAnimationToStoryboard(fallStoryboard, target, opacityAnim, "Opacity");
            AddAnimationToStoryboard(fallStoryboard, transform, CreateFallAnimation(), "TranslateY");

            rotateStoryboard.Begin();

            return this;
        }

        public override void Stop()
        {
            fallStoryboard.Stop();
            base.Stop();
        }

        Timeline CreateRotateAnimation()
        {
            return new DoubleAnimation()
            {
                EasingFunction = new ElasticEase()
                {
                    Oscillations = 2,
                    Springiness = 3,
                    EasingMode = EasingMode.EaseOut
                },
                Duration = new Duration(TimeSpan.FromMilliseconds(Duration.TotalMilliseconds / 1.5)),
                To = 45,
            };
        }

        Timeline CreateFallAnimation()
        {
            return new DoubleAnimation()
            {
                EasingFunction = new QuinticEase(),
                Duration = new Duration(TimeSpan.FromMilliseconds(Duration.TotalMilliseconds / 3)),
                To = 700,
            };
        }
    }
}