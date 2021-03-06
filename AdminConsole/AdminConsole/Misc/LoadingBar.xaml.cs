using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AdminConsole.Misc
{
    /// <summary>
    /// Interaction logic for LoadingBar.xaml
    /// </summary>
    public partial class LoadingBar : UserControl
    {
        /* Loading indicator
         * 
         * Show() | show indicator and begin animation
         * Hide() } hide indicator
         * 
         */
        public LoadingBar()
        {
            InitializeComponent();
            Opacity = 0;
            this.Height = 25;
            this.Width = 25;
           //SetLoadingColor();
        }
        private void startAnimation()
        {
            var Dot1 = new DoubleAnimation();
            Dot1.From = 0;
            Dot1.To = 360;
            Dot1.Duration = new Duration(TimeSpan.FromSeconds(1));
            Dot1.RepeatBehavior = RepeatBehavior.Forever;
            RotateTransform rt = new RotateTransform();
            LoadingBall.RenderTransform = rt;
            LoadingBall.RenderTransformOrigin = new Point(.5, .5);
            rt.BeginAnimation(RotateTransform.AngleProperty, Dot1);
        }
        public void SetLoadingColor()
        {
            LinearGradientBrush linearGradientBrush =
                new LinearGradientBrush();

            linearGradientBrush.StartPoint = new Point(0, 0);
            linearGradientBrush.EndPoint = new Point(1, 1);
            linearGradientBrush.GradientStops.Add(
    new GradientStop(Colors.Yellow, 0.0));
            linearGradientBrush.GradientStops.Add(
                new GradientStop(Colors.Red, 0.25));
            linearGradientBrush.GradientStops.Add(
                new GradientStop(Colors.Blue, 0.75));
            linearGradientBrush.GradientStops.Add(
                new GradientStop(Colors.LimeGreen, 1.0));

            LoadingBall.Fill = linearGradientBrush;
        }
        public void Show()
        {
            Opacity = 1;
            startAnimation();
        }
        public void Hide()
        {
            Opacity = 0;
        }

    }
}
