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
    /// Interaction logic for ProfessorsPreviewControl.xaml
    /// </summary>
    public partial class ProfessorsPreviewControl : UserControl
    {
        public ProfessorsPreviewControl()
        {
            InitializeComponent();
            Binding();
        }

        private void Binding()
        {
            Binding b_name = new Binding("professorName");
            b_name.Source = AppData.s_activeProfessor;
            BindingOperations.SetBinding(professorName, TextBlock.TextProperty, b_name);

            Binding b_dep = new Binding("professorDepartment");
            b_name.Source = AppData.s_activeProfessor;
            BindingOperations.SetBinding(professorDepartment, TextBlock.TextProperty, b_dep);
        }
    }
}
