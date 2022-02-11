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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AdminConsole
{
    /// <summary>
    /// Interaction logic for MapTest.xaml
    /// </summary>
    public partial class MapTest : UserControl
    {
        public Point m_absPoint { get; set; }
        private UserControls.MapPin m_mapPin;
        public MapTest()
        {
            InitializeComponent();
            PinTest.m_building = AppData.s_buildingList.First();
            UserControls.MapPin pin = new UserControls.MapPin();
            pin.m_building = AppData.s_buildingList.ElementAt(1);
            Map.Children.Add(pin);
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var result = VisualTreeHelper.HitTest(Map, e.GetPosition(Map));
            var element = result.VisualHit;
            while (element != null && !(element is UserControls.MapPin))
                element = VisualTreeHelper.GetParent(element);
            m_mapPin = (UserControls.MapPin)element;
            if (m_mapPin != null)
                m_mapPin.PinName.Opacity = 1;
        }

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            m_mapPin.PinName.Opacity = 0;
            m_mapPin = null;
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed && m_mapPin != null)
            {
                Point point = e.GetPosition(Map);
                if (point.X < (Map.MaxWidth - m_mapPin.ActualWidth) && point.Y < (Map.MaxHeight - m_mapPin.ActualHeight) && point.X > 0 && point.Y > 0)
                {
                    m_mapPin.MovePinPosition(point.X, point.Y);
                }
            }
        }
        public HitTestResultBehavior myCallback(HitTestResult result)
        {
            if(result.VisualHit.GetType() == typeof(UserControls.MapPin))
            {
                if(((UserControls.MapPin)result.VisualHit).Opacity == 1.0)
                {
                    ((UserControls.MapPin)result.VisualHit).Opacity = .4;
                    m_mapPin = result.VisualHit as UserControls.MapPin;
                }
                else
                {
                    ((UserControls.MapPin)result.VisualHit).Opacity = .4;
                }
                
            }
            return HitTestResultBehavior.Stop;
        }
        public System.Windows.Media.HitTestFilterBehavior MyFilter(DependencyObject o)
        {
            if(o.GetType() == typeof(UserControls.MapPin))
            {
                return HitTestFilterBehavior.ContinueSkipChildren;
            }
            else
            {
                return HitTestFilterBehavior.Continue;
            }    
        }

    }
}
