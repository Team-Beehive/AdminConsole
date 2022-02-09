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
        public MapTest()
        {
            InitializeComponent();
            PinTest.m_building = AppData.s_buildingList.First();
        }

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            m_absPoint = Mouse.GetPosition(this);
            PinTest.MovePinPosition(m_absPoint.X, m_absPoint.Y);
        }
    }
}
