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
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace LLM
{
    public static class Utils
    {
        public static Timeline CreateAnimationWithValues(double duration, params double[] values)
        {
            return CreateEasingAnimationWithValues(duration, EasingMode.EaseIn, values);
        }

        public static Timeline CreateEasingAnimationWithValues(double duration, EasingMode easingMode, params double[] values)
        {
            if (values.Length == 0)
                throw new ArgumentException("need one or more values");

            var divideTime = duration / values.Length;
            DoubleAnimationUsingKeyFrames frames = new DoubleAnimationUsingKeyFrames();

            for (int i = 0; i < values.Length; i++)
            {
                frames.KeyFrames.Add(new EasingDoubleKeyFrame()
                {
                    EasingFunction = new SineEase()
                    {
                        EasingMode = easingMode,
                    },
                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(divideTime * (i + 1))),
                    Value = values[i],
                });
            }

            return frames;
        }

        public static Timeline CreateDoubleAnimation(double duration, double from, double to, EasingMode easingMode)
        {
            return new DoubleAnimation()
            {
                Duration = new Duration(TimeSpan.FromMilliseconds(duration)),
                From = from,
                To = to,
                EasingFunction = new SineEase()
                {
                    EasingMode = easingMode,
                },
            };
        }

        public static Transform PrepareTransform(UIElement target, Type targetTransformType)
        {
            var renderTransform = target.RenderTransform;

            if (renderTransform == null)
            {
                target.RenderTransform = BuildTransform(targetTransformType);
                return target.RenderTransform;
            }

            if (renderTransform.GetType() == targetTransformType)
                return renderTransform;

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

        public static Projection PrepareProjection(UIElement target, Type projectionType)
        {
            if(target.Projection != null && target.Projection.GetType() == projectionType)
            {
                return target.Projection;
            }

            target.Projection = BuildProjection(projectionType);
            return target.Projection;
        }

        public static Transform BuildTransform(Type targetTransformType)
        {
            if (targetTransformType == typeof(TranslateTransform))
                return new TranslateTransform();
            if (targetTransformType == typeof(RotateTransform))
                return new RotateTransform();
            if (targetTransformType == typeof(ScaleTransform))
                return new ScaleTransform();
            if (targetTransformType == typeof(CompositeTransform))
                return new CompositeTransform();
            if (targetTransformType == typeof(SkewTransform))
                return new SkewTransform();

            throw new NotSupportedException();
        }

        public static Projection BuildProjection(Type targetProjectionType)
        {
            if (targetProjectionType == typeof(PlaneProjection))
                return new PlaneProjection();
            if (targetProjectionType == typeof(Matrix3DProjection))
                return new Matrix3DProjection();

            throw new NotSupportedException();
        }

        public static void SetCenterForScaleTransform(UIElement target, ScaleTransform transform)
        {
            transform.CenterX = GetCenterX(target);
            transform.CenterY = GetCenterY(target);
        }

        public static double GetCenterX(UIElement target)
        {
            return target.RenderSize.Width / 2;
        }

        public static double GetCenterY(UIElement target)
        {
            return target.RenderSize.Height / 2;
        }

        public static double GetBottomY(UIElement target)
        {
            return target.RenderSize.Height;
        }

        public static Point GetPointInParent(UIElement target)
        {
            var parent = VisualTreeHelper.GetParent(target) as UIElement;
            if (parent == null)
                return new Point(0, 0);

            return target.TransformToVisual(parent).TransformPoint(new Point(0, 0));
        }

        public static Size GetParentSize(UIElement target)
        {
            var parent = VisualTreeHelper.GetParent(target) as UIElement;
            if (parent == null)
                return new Size(0, 0);

            return parent.RenderSize;
        }
    }
}
