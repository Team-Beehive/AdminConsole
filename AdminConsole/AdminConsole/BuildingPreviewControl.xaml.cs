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
    /// Interaction logic for BuildingPreviewControl.xaml
    /// </summary>
    public partial class BuildingPreviewControl : UserControl
    {
        public BuildingPreviewControl()
        {
            InitializeComponent();
            BindElements();
        }

        private void ElementSelect(object sender, EventArgs e)
        {
            if (AppData.s_propertiesPanel.Children.Count > 1)
            {
                AppData.s_propertiesPanel.Children.RemoveAt(1);
            }
            TextBlock tbElement = sender as TextBlock;
            Label lElement = sender as Label;
            if (tbElement != null)
            {
                AppData.s_propertiesPanel.Children.Add(new BuildingPropertiesControl(tbElement.Name));
            }
            else
            {
                AppData.s_propertiesPanel.Children.Add(new BuildingPropertiesControl(lElement.Name));
            }
        }

        private void BindElements()
        {
            Binding b_name = new Binding("BuildingName");
            b_name.Source = AppData.s_activeBuilding;
            //BindingOperations.SetBinding(BuildingName, TextBlock.TextProperty, b_name);
            BindingOperations.SetBinding(BuildingName, Label.ContentProperty, b_name);

            Binding b_desc = new Binding("BuildingName_Info");
            b_desc.Source = AppData.s_activeBuilding;
            BindingOperations.SetBinding(BuildingName_Info, TextBlock.TextProperty, b_desc);
            //BindingOperations.SetBinding(BuildingName_Info, Label.ContentProperty, b_desc);

            Binding b_year = new Binding("BuildingConstructionYear");
            b_year.Source = AppData.s_activeBuilding;
            //BindingOperations.SetBinding(BuildingConstructionYear, TextBlock.TextProperty, b_year);
            BindingOperations.SetBinding(BuildingConstructionYear, Label.ContentProperty, b_year);
        }
    }
}
