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

namespace AdminConsole
{
    /// <summary>
    /// Interaction logic for MajorPreviewControl.xaml
    /// </summary>
    /// 

    public partial class MajorPreviewControl : UserControl
    {
        public MajorPreviewControl()
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
            Label lElement = sender as Label;
            TextBlock tbElement = sender as TextBlock;
            if (lElement != null)
            {
                AppData.s_propertiesPanel.Children.Add(new MajorPropControl(lElement.Name));
            } 
            else if (tbElement != null)
            {
                AppData.s_propertiesPanel.Children.Add(new MajorPropControl(tbElement.Name));
            }
        }

        //TODO: Find a better way to bind this data
        private void BindElements()
        {
            Binding b_title = new Binding("MajorName");
            b_title.Source = AppData.s_activeData;
            BindingOperations.SetBinding(MajorName, Label.ContentProperty, b_title);

            Binding b_classes = new Binding("Classes[0]");
            b_classes.Source = AppData.s_activeData;

            Binding b_camp = new Binding("campuses[0]");
            b_camp.Source = AppData.s_activeData;

            Binding b_type = new Binding("type[0]");
            b_type.Source = AppData.s_activeData;
            BindingOperations.SetBinding(type, Label.ContentProperty, b_type);

            Binding b_desc = new Binding("about[0]");
            b_desc.Source = AppData.s_activeData;
            BindingOperations.SetBinding(about, TextBlock.TextProperty, b_desc);
        }
    }
}
