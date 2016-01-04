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
    public class WobbleAnimation : AnimationBase
    {
        public WobbleAnimation()
        {
            Duration = TimeSpan.FromMilliseconds(1000);
        }

        public override void PlayOn(UIElement target, Action continueWith)
        {
            var transform = (CompositeTransform)AnimUtils.PrepareTransform(target, typeof(CompositeTransform));
            transform.CenterX = AnimUtils.GetCenterX(target);
            transform.CenterY = AnimUtils.GetCenterY(target);

            var storyboard = CreateStoryboard(continueWith);
            float WidthPerUnit = (float)(target.DesiredSize.Width / 100.0);

            var translationAnim = AnimUtils.CreateAnimationWithValues(Duration.TotalMilliseconds, 0, -25 * WidthPerUnit, 20 * WidthPerUnit, -15 * WidthPerUnit, 10 * WidthPerUnit, -5 * WidthPerUnit, 0, 0);
            AddAnimationToStoryboard(storyboard, transform, translationAnim, "TranslateX");

            var rotateAnim = AnimUtils.CreateAnimationWithValues(Duration.TotalMilliseconds, 0, -5, 3, -3, 2, -1, 0);
            AddAnimationToStoryboard(storyboard, transform, rotateAnim, "Rotation");

            storyboard.Begin();
        }
    }
}
