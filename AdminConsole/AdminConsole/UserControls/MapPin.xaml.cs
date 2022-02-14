﻿using System;
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
using AdminDatabaseFramework;

namespace AdminConsole.UserControls
{
    /// <summary>
    /// Interaction logic for MapPin.xaml
    /// </summary>
    public partial class MapPin : UserControl
    {
        public BuildingData m_building { get; set; }
        public UIElement m_Map { get; set; }
        public Point m_MapPoint { get; set; }
        public Point m_newPoint { get; set; }
        public double m_leftPercent { get; set; }
        public double m_topPercent { get; set; }

        public MapPin()
        {
            InitializeComponent();
            InitialPosition();
        }
        public MapPin(BuildingData building)
        {
            InitializeComponent();
            m_building = building;
            PinName.Text = m_building.BuildingName;
            PinName.IsEnabled = false;
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            PinName.Text = m_building.BuildingName;
        }

        private void InitialPosition()
        {
            m_Map = VisualTreeHelper.GetParent(this) as UIElement;
            m_MapPoint = this.TranslatePoint(new Point(0, 0), m_Map);
        }

        public void MovePinPosition(double x, double y)
        {
            m_leftPercent = x / (this.Parent as Canvas).ActualWidth;

            m_topPercent = y / (this.Parent as Canvas).ActualHeight;

            this.Margin = new Thickness(x, y, 0, 0);
        }
        public System.Windows.Media.HitTestResult HitTestCore(System.Windows.Media.PointHitTestParameters hitTestParameters)
        {
            return new PointHitTestResult(this, hitTestParameters.HitPoint);
        }

    }
}
