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
    public class SlideOutRightAnimation : AnimationBase
    {
        public SlideOutRightAnimation()
        {
            Duration = TimeSpan.FromMilliseconds(1000);
        }

        public override void PlayOn(UIElement target, Action continueWith)
        {
            var transform = (CompositeTransform)AnimUtils.PrepareTransform(target, typeof(CompositeTransform));
            target.Opacity = 1;
            transform.TranslateX = 0;
            var storyboard = CreateStoryboard(continueWith);

            var opacityAnim = AnimUtils.CreateAnimationWithValues(Duration.TotalMilliseconds, 0);
            var translateAnim = AnimUtils.CreateAnimationWithValues(Duration.TotalMilliseconds, AnimUtils.GetPointInParent(target).X + target.RenderSize.Width);
            AddAnimationToStoryboard(storyboard, target, opacityAnim, "Opacity");
            AddAnimationToStoryboard(storyboard, transform, translateAnim, "TranslateX");

            storyboard.Begin();
        }
    }
}