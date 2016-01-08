using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace LLM
{
    public class Animator
    {
        public static IAnimation Use(AnimationType animType)
        {
            return CreateAnimationbyType(animType);
        }

        private static IAnimation CreateAnimationbyType(AnimationType animType)
        {
            var animName = Enum.GetName(typeof(AnimationType), animType);
            var animation = (IAnimation)Activator.CreateInstance(Type.GetType(string.Format("LLM.Animation.{0}Animation,LLMAnimator", animName)));
            return animation;
        }
    }
}
