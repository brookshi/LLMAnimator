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

namespace LLM
{
    public static class AnimUtils
    {
        public static Timeline CreateAnimationWithValues(DependencyObject target, double duration, params double[] values)
        {
            if (values.Length == 0)
                throw new ArgumentException("need one or more values");

            var divideTime = duration / values.Length;
            DoubleAnimationUsingKeyFrames frames = new DoubleAnimationUsingKeyFrames();

            for(int i=0; i < values.Length; i++)
            {
                frames.KeyFrames.Add(new EasingDoubleKeyFrame()
                {
                    EasingFunction = new SineEase()
                    {
                        EasingMode = EasingMode.EaseIn
                    },
                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(divideTime * (i + 1))),
                    Value = values[i],
                });
            }

            return frames;
        }

        public static Transform PrepareTransform(UIElement target, Type targetTransformType)
        {
            var renderTransform = target.RenderTransform;
            if (renderTransform.GetType() == targetTransformType)
                return renderTransform;

            if (renderTransform == null)
            {
                target.RenderTransform = BuildTransform(targetTransformType);
                return target.RenderTransform;
            }

            var transformGroup = renderTransform as TransformGroup;
            var transform = BuildTransform(targetTransformType);

            if (transformGroup == null)
            {
                transformGroup = new TransformGroup();
                transformGroup.Children.Add(renderTransform);
                transformGroup.Children.Add(transform);
                target.RenderTransform = transformGroup;
                return transform;
            }

            transform = transformGroup.Children.SingleOrDefault(o => o.GetType() == targetTransformType);

            if (transform == null)
            {
                transform = BuildTransform(targetTransformType);
                transformGroup.Children.Add(transform);
            }

            return transform;
        }

        public static Transform BuildTransform(Type targetTransformType)
        {
            if (targetTransformType == typeof(TranslateTransform))
                return new TranslateTransform();
            if (targetTransformType == typeof(RotateTransform))
                return new RotateTransform();
            if (targetTransformType == typeof(ScaleTransform))
                return new ScaleTransform();

            throw new NotSupportedException();
        }

        public static void SetCenterForScaleTransform(UIElement target, ScaleTransform transform)
        {
            transform.CenterX = target.RenderSize.Width / 2;
            transform.CenterY = target.RenderSize.Height / 2;
        }
    }
}
