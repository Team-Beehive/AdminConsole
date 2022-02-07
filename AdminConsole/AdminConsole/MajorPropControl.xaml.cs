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
    /// Interaction logic for MajorPropControl.xaml
    /// </summary>
    public partial class MajorPropControl : UserControl
    {
        private string target;

        public MajorPropControl(string type)
        {
            target = type;
            InitializeComponent();
            Binding();
        }

        public MajorPropControl()
        {
            InitializeComponent();
        }

        private void Binding()
        {
            switch (target)
            {
                case "Classes":
                    target += "[0]";
                    break;
                case "campuses":
                    target += "[0]";
                    break;
                case "type":
                    target += "[0]";
                    break;
                case "about":
                    target += "[0]";
                    break;
            }

            Binding b_text = new Binding(target);
            b_text.Mode = BindingMode.TwoWay;
            b_text.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            b_text.NotifyOnSourceUpdated = true;
            b_text.Source = AppData.s_activeData;
            BindingOperations.SetBinding(tb_prop, TextBox.TextProperty, b_text);
        }

        private void OnSourceUpdated(object sender, DataTransferEventArgs e)
        {
            if (!AppData.s_changedList.Contains(AppData.s_activeData))
            {
                AppData.s_changedList.Add(AppData.s_activeData);
            }
        }
    }
}
