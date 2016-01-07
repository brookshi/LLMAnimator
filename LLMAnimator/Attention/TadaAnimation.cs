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
    public class TadaAnimation : AnimationBase
    {
        public TadaAnimation()
        {
            Duration = TimeSpan.FromMilliseconds(1000);
        }

        public override IAnimation PlayOn(UIElement target, Action continueWith)
        {
            var transform = (CompositeTransform)Utils.PrepareTransform(target, typeof(CompositeTransform));
            transform.CenterX = Utils.GetCenterX(target);
            transform.CenterY = Utils.GetCenterY(target);

            var storyboard = PrepareStoryboard(continueWith);

            var scaleXAnim = Utils.CreateAnimationWithValues(Duration.TotalMilliseconds, 1, 0.9f, 0.9f, 1.1f, 1.1f, 1.1f, 1.1f, 1.1f, 1.1f, 1);
            AddAnimationToStoryboard(storyboard, transform, scaleXAnim, "ScaleX");

            var scaleYAnim = Utils.CreateAnimationWithValues(Duration.TotalMilliseconds, 1, 0.9f, 0.9f, 1.1f, 1.1f, 1.1f, 1.1f, 1.1f, 1.1f, 1);
            AddAnimationToStoryboard(storyboard, transform, scaleYAnim, "ScaleY");

            var rotateAnim = Utils.CreateAnimationWithValues(Duration.TotalMilliseconds, 0, -3, -3, 3, -3, 3, -3, 3, -3, 0);
            AddAnimationToStoryboard(storyboard, transform, rotateAnim, "Rotation");

            storyboard.Begin();

            return this;
        }

    }
}
