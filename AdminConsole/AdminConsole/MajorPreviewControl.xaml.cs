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

        //TODO: Find a better way to bind this data
        private void BindElements()
        {
            Binding b_title = new Binding("MajorName");
            b_title.Source = AppData.s_activeData;
            BindingOperations.SetBinding(tb_title, TextBlock.TextProperty, b_title);

            Binding b_classes = new Binding("Classes[0]");
            b_classes.Source = AppData.s_activeData;
            BindingOperations.SetBinding(tb_classes, TextBlock.TextProperty, b_classes);

            Binding b_camp = new Binding("campuses[0]");
            b_camp.Source = AppData.s_activeData;
            BindingOperations.SetBinding(tb_camp, TextBlock.TextProperty, b_camp);

            Binding b_type = new Binding("type[0]");
            b_type.Source = AppData.s_activeData;
            BindingOperations.SetBinding(tb_type, TextBlock.TextProperty, b_type);

            Binding b_desc = new Binding("about[0]");
            b_desc.Source = AppData.s_activeData;
            BindingOperations.SetBinding(tb_desc, TextBlock.TextProperty, b_desc);
        }
    }
}
