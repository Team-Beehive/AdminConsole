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
        private Storyboard loadingBar;

        public LoadingBar()
        {
            InitializeComponent();
        }
        private void startAnimation()
        {
            var Dot1 = new DoubleAnimation();
            Dot1.From = 1.0;
            Dot1.To = 0.0;
            Dot1.Duration = new Duration(TimeSpan.FromSeconds(1));
            Dot1.AutoReverse = true;
            Dot1.RepeatBehavior = RepeatBehavior.Forever;

            loadingBar = new Storyboard();
            loadingBar.Children.Add(Dot1);
            Storyboard.SetTargetName(Dot1, ell_loadDot1.Name);
            Storyboard.SetTargetProperty(Dot1, new PropertyPath(Ellipse.OpacityProperty));
            ell_loadDot1.BeginAnimation(Ellipse.OpacityProperty, Dot1);
        }
        public void Show()
        {
            startAnimation();
        }

    }
}
