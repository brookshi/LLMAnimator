﻿#region License
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
    public class ShakeAnimation : AnimationBase
    {
        public ShakeAnimation()
        {
            Duration = TimeSpan.FromMilliseconds(800);
        }

        public override void PlayOn(UIElement target, Action continueWith)
        {
            var transform = AnimUtils.PrepareTransform(target, typeof(TranslateTransform));

            var storyboard = CreateStoryboard(continueWith);

            var anim = AnimUtils.CreateAnimationWithValues(transform, Duration.TotalMilliseconds, 10, -10, 10, -10, 6, -6, 2, -2, 0);

            AddAnimationToStoryboard(storyboard, transform, anim, "X");
            
            storyboard.Begin();
        }
    }
}