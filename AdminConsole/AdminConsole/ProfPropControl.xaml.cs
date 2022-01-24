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
    /// Interaction logic for ProfPropControl.xaml
    /// </summary>
    public partial class ProfPropControl : UserControl
    {
        private string target;
        public ProfPropControl(string type)
        {
            target = type;
            InitializeComponent();
            Binding();
        }

        public ProfPropControl()
        {
            InitializeComponent();
        }

        private void Binding()
        {
            Binding b_text = new Binding(target);
            b_text.Mode = BindingMode.TwoWay;
            b_text.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            b_text.NotifyOnSourceUpdated = true;
            b_text.Source = AppData.s_activeProfessor;
            BindingOperations.SetBinding(tb_prop, TextBox.TextProperty, b_text);
        }

        private void OnSourceUpdated(object sender, DataTransferEventArgs e)
        {
            if (!AppData.s_changedProf.Contains(AppData.s_activeProfessor))
            {
                AppData.s_changedProf.Add(AppData.s_activeProfessor);
            }
        }
    }
}
