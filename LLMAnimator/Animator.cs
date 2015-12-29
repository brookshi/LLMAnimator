using LLM.Attention;
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
            switch(animType)
            {
                case AnimationType.Bounce:
                    return new BounceAnimation();
            }

            return null;
        }
    }
}
