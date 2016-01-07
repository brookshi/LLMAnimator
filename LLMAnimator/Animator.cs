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
        //static Dictionary<AnimationType, IAnimation> _animPool = new Dictionary<AnimationType, IAnimation>();

        public static IAnimation Use(AnimationType animType)
        {
            return CreateAnimationbyType(animType);
        }

        private static IAnimation CreateAnimationbyType(AnimationType animType)
        {
           // if(!_animPool.ContainsKey(animType))
            {
                var animName = Enum.GetName(typeof(AnimationType), animType);
                var animation = (IAnimation)Activator.CreateInstance(Type.GetType(string.Format("LLM.Animation.{0}Animation,LLMAnimator", animName)));
                //_animPool[animType] = animation;
                return animation;
            }

           // return _animPool[animType];
        }
    }
}
