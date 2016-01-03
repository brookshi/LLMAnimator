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
                case AnimationType.Flash:
                    return new FlashAnimation();
                case AnimationType.Pulse:
                    return new PulseAnimation();
                case AnimationType.RubberBand:
                    return new RubberBandAnimation();
                case AnimationType.Shake:
                    return new ShakeAnimation();
                case AnimationType.StandUp:
                    return new StandUpAnimation();
                case AnimationType.Swing:
                    return new SwingAnimation();
                case AnimationType.Tada:
                    return new TadaAnimation();
                case AnimationType.Wave:
                    return new WaveAnimation();
                case AnimationType.Wobble:
                    return new WobbleAnimation();
                case AnimationType.BounceIn:
                    return new BounceInAnimation();
                case AnimationType.BounceInDown:
                    return new BounceInDownAnimation();
                case AnimationType.BounceInUp:
                    return new BounceInUpAnimation();
                case AnimationType.BounceInLeft:
                    return new BounceInLeftAnimation();
                case AnimationType.BounceInRight:
                    return new BounceInRightAnimation();
            }

            return null;
        }
    }
}
