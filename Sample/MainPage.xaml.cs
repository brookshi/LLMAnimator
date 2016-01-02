using LLM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace Sample
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            InitAnimList();
        }

        private List<CustomAnim> CustomAnims = new List<CustomAnim>();

        private void InitAnimList()
        {
            CustomAnims.Add(new CustomAnim()
            {
                Name = "Bounce",
                ClickAction = ()=> {
                    Animator.Use(AnimationType.Bounce).PlayOn(AnimText);
                }
            });

            CustomAnims.Add(new CustomAnim()
            {
                Name = "Flash",
                ClickAction = () => {
                    Animator.Use(AnimationType.Flash).PlayOn(AnimText);
                }
            });

            CustomAnims.Add(new CustomAnim()
            {
                Name = "Pulse",
                ClickAction = () => {
                    Animator.Use(AnimationType.Pulse).SetRepeatBehavior(new Windows.UI.Xaml.Media.Animation.RepeatBehavior(2)).PlayOn(AnimText);
                }
            });

            CustomAnims.Add(new CustomAnim()
            {
                Name = "RubberBand",
                ClickAction = () => {
                    Animator.Use(AnimationType.RubberBand).PlayOn(AnimText);
                }
            });

            CustomAnims.Add(new CustomAnim()
            {
                Name = "Shake",
                ClickAction = () => {
                    Animator.Use(AnimationType.Shake).PlayOn(AnimText);
                }
            });

            CustomAnims.Add(new CustomAnim()
            {
                Name = "StandUp",
                ClickAction = () => {
                    Animator.Use(AnimationType.StandUp).PlayOn(AnimText);
                }
            });

            AddAnim("Swing", AnimationType.Swing);

            AddAnim("Tada", AnimationType.Tada);

            AddAnim("Wave", AnimationType.Wave);

            AnimList.ItemsSource = CustomAnims;
        }

        void AddAnim(string name, AnimationType type)
        {
            CustomAnims.Add(new CustomAnim()
            {
                Name = name,
                ClickAction = () => {
                    Animator.Use(type).PlayOn(AnimText);
                }
            });
        }

        private void AnimList_ItemClick(object sender, ItemClickEventArgs e)
        {
            (e.ClickedItem as CustomAnim).ClickAction();
        }
    }

    internal class CustomAnim
    {
        public string Name { get; set; }

        public Action ClickAction { get; set; }
    }
}
