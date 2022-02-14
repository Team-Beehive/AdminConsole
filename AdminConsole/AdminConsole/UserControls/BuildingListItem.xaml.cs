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
using AdminDatabaseFramework;

namespace AdminConsole.UserControls
{
    /// <summary>
    /// Interaction logic for BuildingListItem.xaml
    /// </summary>
    public partial class BuildingListItem : UserControl
    {
        public BuildingData m_buildingData { get; set; }
        public BuildingListItem(BuildingData data)
        {
            InitializeComponent();
            m_buildingData = data;
            BuildingName.Text = m_buildingData.BuildingName;
        }
    }
}
