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

        private void ElementSelect(object sender, EventArgs e)
        {
            if (AppData.s_propertiesPanel.Children.Count > 1)
            {
                AppData.s_propertiesPanel.Children.RemoveAt(1);
            }
            TextBlock temp = sender as TextBlock;
            AppData.s_propertiesPanel.Children.Add(new ProfPropControl(temp.Name));
        }

        private void Binding()
        {
            Binding b_name = new Binding("professorName");
            b_name.Source = AppData.s_activeProfessor;
            BindingOperations.SetBinding(professorName, TextBlock.TextProperty, b_name);

            Binding b_dep = new Binding("professorDepartment");
            b_dep.Source = AppData.s_activeProfessor;
            BindingOperations.SetBinding(professorDepartment, TextBlock.TextProperty, b_dep);

            Binding b_office = new Binding("professorOffice");
            b_office.Source = AppData.s_activeProfessor;
            BindingOperations.SetBinding(professorOffice, TextBlock.TextProperty, b_office);

            Binding b_email = new Binding("professorEmail");
            b_email.Source = AppData.s_activeProfessor;
            BindingOperations.SetBinding(professorEmail, TextBlock.TextProperty, b_email);

            Binding b_phone = new Binding("professorPhoneNumber");
            b_phone.Source = AppData.s_activeProfessor;
            BindingOperations.SetBinding(professorPhoneNumber, TextBlock.TextProperty, b_phone);
        }
    }
}
