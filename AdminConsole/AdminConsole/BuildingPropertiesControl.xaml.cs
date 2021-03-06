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
    /// Interaction logic for BuildingPropertiesControl.xaml
    /// </summary>
    public partial class BuildingPropertiesControl : UserControl
    {
        private string target;
        private AppData m_data;
        public BuildingPropertiesControl(string type, AppData data)
        {
            target = type;
            m_data = data;
            InitializeComponent();
            Binding();
        }
        public BuildingPropertiesControl()
        {
            InitializeComponent();
        }
        private void Binding()
        {
            Binding b_text = new Binding(target);
            b_text.Mode = BindingMode.TwoWay;
            b_text.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            b_text.NotifyOnSourceUpdated = true;
            b_text.Source = m_data.s_activeBuilding;
            //b_text.Source = AppData.s_activeBuilding;
            tb_prop.SourceUpdated += OnSourceUpdated;
            BindingOperations.SetBinding(tb_prop, TextBox.TextProperty, b_text);
        }
        private void OnSourceUpdated(object sender, DataTransferEventArgs e)
        {
            /*if (!AppData.s_changedBuildingList.Contains(AppData.s_activeBuilding))
            {
                AppData.s_changedBuildingList.Add(AppData.s_activeBuilding);
            }*/
            if (!m_data.s_changedBuildingList.Contains(m_data.s_activeBuilding))
            {
                m_data.s_changedBuildingList.Add(m_data.s_activeBuilding);
            }
        }
    }
}
